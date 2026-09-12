# AI Customize Database - Data Dictionary & Field Explanation

> **Project**: Hoa Theo Mùa (`hoa-theo-mua-ai-customize`)  
> **Subsystem**: `HTM_AI_Customize` (Phân hệ Thiết kế Hoa & Thiệp AI)  
> **Document Reference**: [AI_DB_Diagram.md](file:///d:/VNZ/document-first-brief/docs%20%281%29/AI_DB_Diagram.md) | [AI_Flower_Context.md](file:///d:/VNZ/document-first-brief/docs%20%281%29/AI_Flower_Context.md) | [AI_Card_Context.md](file:///d:/VNZ/document-first-brief/docs%20%281%29/AI_Card_Context.md)  
> **Last Updated**: 2026-09-05

---

## 1. NGUYÊN TẮC THIẾT KẾ DỮ LIỆU CỐT LÕI

1. **Không có bảng tạm (No Request / Job Tables)**: Flow tạo hoa AI và thiệp AI là xử lý đồng bộ hoặc bán đồng bộ trong một chu trình API duy nhất. Không có các bảng trung gian như `flower_requests` hay `flower_ai_jobs`. Dữ liệu chỉ được ghi vào cơ sở dữ liệu khi AI sinh ảnh thành công và vượt qua xác thực.
2. **Bảo tồn lịch sử đa hình (`client_histories`)**: Bảng trung tâm điều hướng quan hệ phả hệ (lineage) giữa Sản phẩm gốc (`products`), Mẫu hoa AI (`generated_flowers`), Thiệp AI / Thiệp thủ công (`generated_cards`) và Bài viết (`generated_posts`).
3. **Đóng băng đầu vào bất biến (Immutable Snapshots)**: Cả `generated_flowers.input_snapshot` và `client_histories.metadata` đều lưu snapshot phiên bản 2 (`schema_version = 2`). Khi dữ liệu danh mục gốc (Product, Mockup, Template, Config) bị sửa đổi hoặc xóa mềm (`is_deleted = true`), lịch sử đã tạo của người dùng vẫn tự hoàn chỉnh, toàn vẹn và không bị ảnh hưởng.
4. **Giá trị dẫn xuất thời gian thực (Derived Values)**: Trường `sellableQuantity` (số lượng có thể bán) của Combo hoa không phải là một cột vật lý trong database, mà được tính toán động tại thời điểm truy vấn thông qua hàm nghiệp vụ `CalculateAvailableQty`.

---

## 2. BẢNG TRUNG TÂM: `client_histories` (LỊCH SỬ KHÁCH HÀNG ĐA HÌNH)

Lưu vết toàn bộ kết quả tạo thành công và phả hệ nguồn gốc.

| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất toàn cục của bản ghi lịch sử. |
| `user_id` | `uuid` | `NOT NULL` | ID của khách hàng sở hữu bản ghi lịch sử này. Phục vụ kiểm tra quyền sở hữu (ownership) khi xem, tải ảnh hoặc tạo lại (regenerate). |
| `input` | `text` | `NOT NULL` | Chuỗi JSON ghi nhận thông tin request thực tế:<br>• **Khi Create**: Lưu toàn bộ JSON request gốc.<br>• **Khi Regenerate**: Chỉ ghi nhận hành động thực tế `{ operation, source_flower_id, mockup_id }`. Không đóng vai trò là bảng tác vụ tạm. |
| `type` | `varchar` | `NOT NULL` | Định danh phân loại kết quả (discriminator chuỗi mở rộng):<br>• `"flower"`: Mẫu hoa AI.<br>• `"card"`: Thiệp thiết kế AI.<br>• `"handmade_card"`: Thiệp viết tay thủ công.<br>• `"post"`: Bài viết truyền thông mạng xã hội. |
| `system_prompt_id` | `uuid` | `NULLABLE` | Khóa ngoại trỏ đến `system_prompts.id` đã sử dụng. Bằng `null` đối với thiệp thủ công (`handmade_card`) vì không gọi AI. |
| `metadata` | `json` | `NOT NULL` | **Snapshot bất biến** lưu toàn bộ nguồn gốc (Product, Mockup, ảnh đóng băng, prompt đầy đủ, cờ `system_prompt_source`, trạng thái tại thời điểm tạo). |
| `base_id` | `uuid` | `NOT NULL` | **Khóa ngoại đa hình nguồn (Polymorphic Base ID)**:<br>• Khi `type = "flower"`: trỏ tới `products.id` (Combo/biến thể gốc).<br>• Khi `type = "card"` hoặc `"handmade_card"` (từ hoa thường): trỏ tới `products.id`.<br>• Khi `type = "card"` hoặc `"handmade_card"` (từ hoa AI): trỏ tới `generated_flowers.id`. |
| `output_id` | `uuid` | `NOT NULL` | **Khóa ngoại đa hình đích (Polymorphic Output ID)**:<br>• Khi `type = "flower"`: trỏ tới `generated_flowers.id`.<br>• Khi `type = "card"` hoặc `"handmade_card"`: trỏ tới `generated_cards.id`.<br>• Khi `type = "post"`: trỏ tới `generated_posts.id`. |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo bản ghi lịch sử thành công. |

---

## 3. CÁC BẢNG KẾT QUẢ SẢN PHẨM AI & THIỆP

### 3.1. Bảng `generated_flowers` (Mẫu hoa AI)
Lưu kết quả tạo hoa thành công sau khi đã gọi AI Worker và đóng logo thương hiệu.

| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của mẫu hoa AI được sinh ra. |
| `image_url` | `varchar` | `NOT NULL` | Đường dẫn URL của ảnh hoa hoàn thiện (đã được watermark logo theo cấu hình hiện hành). **Tuyệt đối không dùng URL ảnh này làm input để tạo lại mẫu hoa.** |
| `user_id` | `uuid` | `NOT NULL` | ID khách hàng sở hữu mẫu hoa. |
| `input_snapshot` | `json` | `NOT NULL` | Toàn bộ dữ liệu đầu vào đã đóng băng gửi sang AI Module (Product, Mockup, hash ảnh, prompt, user input). Đảm bảo tính độc lập khi danh mục live thay đổi. |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm mẫu hoa được tạo thành công. |

---

### 3.2. Bảng `generated_cards` (Thiệp chúc mừng AI & HandMade)
Lưu trữ cả thiệp cá nhân hóa sinh bằng AI và thiệp viết tay thư pháp thủ công (HandMade).

| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của thiệp đã tạo. |
| `content` | `text` | `NULLABLE` | Nội dung lời chúc trên thiệp do AI đề xuất hoặc nội dung hoàn chỉnh. |
| `image_url` | `varchar` | `NOT NULL` | URL ảnh thiệp thành phẩm chính thức (định dạng PNG chất lượng cao, không gắn logo). |
| `user_id` | `uuid` | `NOT NULL` | ID khách hàng sở hữu thiệp. |
| `card_type` | `varchar` | `NOT NULL` | Phân loại thiệp bắt buộc: `"ai"` hoặc `"handmade"`. Bất biến sau khi tạo. |
| `form_type` | `form_type` | `NOT NULL` | Hình thức thể hiện: `"go_may"` (in/gõ máy) hoặc `"calligraphy"` (thư pháp/viết tay). Thiệp `handmade` luôn bắt buộc là `"calligraphy"`. |
| `size_key` | `varchar` | `NOT NULL` | Khóa định danh kích thước thiệp (trích xuất từ bảng `Config`, ví dụ: `size_standard_a6`). |
| `sender_name` | `varchar` | `NOT NULL` | Tên người gửi lời chúc (tối đa 20 từ). |
| `receiver_name` | `varchar` | `NOT NULL` | Tên người nhận lời chúc (tối đa 20 từ). |
| `message_content`| `text` | `NOT NULL` | Nội dung lời chúc do khách hàng soạn thảo (tối đa 100 từ). |
| `attached_image_url` | `varchar` | `NULLABLE` | URL ảnh cá nhân đính kèm do khách hàng tải lên (với thiệp `handmade`, trường này luôn là `null`). |
| `size_name` | `varchar` | `NOT NULL` | Tên hiển thị của kích thước tại thời điểm tạo (ví dụ: "Khổ A6 Tiêu chuẩn"). |
| `size_width` | `decimal` | `NOT NULL` | Chiều rộng vật lý của thiệp (cm). |
| `size_height` | `decimal` | `NOT NULL` | Chiều cao vật lý của thiệp (cm). |
| `size_base_price` | `decimal` | `NOT NULL` | Giá gốc của kích thước thiệp tại thời điểm tạo (lưu đối soát audit). |
| `size_max_words` | `int` | `NOT NULL` | Số lượng từ tối đa được phép hiển thị trên kích thước thiệp này. |
| `word_count` | `int` | `NOT NULL` | Số từ thực tế trong lời chúc (tính theo chuẩn tách khoảng trắng). |
| `word_config_snapshot` | `json` | `NOT NULL` | Snapshot định mức và đơn giá phụ phí viết tay chữ calligraphy theo các bậc số từ. |
| `base_price` | `decimal` | `NOT NULL` | Giá cơ sở của thiệp. Với thiệp `handmade`, `base_price = 0`. |
| `extra_price` | `decimal` | `NOT NULL` | Phụ phí phát sinh (tiền công viết calligraphy vượt định mức). |
| `total_price` | `decimal` | `NOT NULL` | Tổng giá tiền của thiệp (`total_price = base_price + extra_price`). |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo thiệp. |

---

## 4. CÁC BẢNG DANH MỤC THAM CHIẾU & MẪU THIẾT KẾ

### 4.1. Bảng `card_templates` (Mẫu phôi thiệp)
| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của mẫu phôi thiệp. |
| `name` | `varchar` | `NOT NULL` | Tên mẫu thiệp (ví dụ: "Thiệp Hoa Hồng Vintage", "Thiệp Pastel Sinh Nhật"). |
| `description` | `text` | `NULLABLE` | Mô tả kiểu dáng, chủ đề và dịp sử dụng phù hợp. |
| `image_url` | `varchar` | `NOT NULL` | URL ảnh phôi mẫu thiệp chất lượng cao. |
| `metadata` | `json` | `NULLABLE` | Cấu hình kỹ thuật của template (tọa độ in text, font chữ, màu sắc mặc định). |
| `template_type` | `varchar` | `NOT NULL` | Phân loại mẫu: `"ai"` hoặc `"handmade"`. Bất biến, không thể thay đổi sau khi tạo. |
| `is_active` | `boolean` | `DEFAULT true`| Trạng thái khả dụng (`true`: đang mở cho khách hàng chọn; `false`: tạm ngưng). |
| `is_deleted` | `boolean` | `DEFAULT false`| Cờ đánh dấu xóa mềm (`true`: đã xóa). |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo mẫu thiệp. |
| `updated_at` | `timestamp` | `DEFAULT now()` | Thời điểm cập nhật mẫu thiệp gần nhất. |

---

### 4.2. Bảng `mockup` (Bố cục dáng cắm hoa)
| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của Mockup cắm hoa. |
| `name` | `varchar` | `NOT NULL` | Tên kiểu dáng (ví dụ: "Bó tròn Hàn Quốc", "Lẵng hoa khai trương", "Giỏ hoa để bàn"). |
| `description` | `text` | `NULLABLE` | Hướng dẫn tạo hình, bố cục và tỷ lệ hoa cho AI. |
| `image_url` | `varchar` | `NOT NULL` | URL ảnh mẫu bố cục chuẩn để người dùng và AI tham chiếu. |
| `is_active` | `boolean` | `DEFAULT true`| Trạng thái kích hoạt (`true`: hiển thị trên danh sách lựa chọn). |
| `is_deleted` | `boolean` | `DEFAULT false`| Cờ xóa mềm. |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo mockup. |
| `updated_at` | `timestamp` | `DEFAULT now()` | Thời điểm chỉnh sửa mockup. |

---

## 5. BẢNG CẤU HÌNH VÀ CHỈ THỊ PROMPT

### 5.1. Bảng `system_prompts` (Prompt hệ thống cho AI)
| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của câu lệnh prompt hệ thống. |
| `type` | `system_prompt_types` | `NOT NULL, UNIQUE` | Loại prompt (`flower`, `card`, `post`). **Mỗi type có duy nhất đúng một bản ghi hiện hành**. |
| `content` | `text` | `NOT NULL` | Nội dung câu lệnh kỹ thuật chi tiết hướng dẫn AI Engine sinh ảnh/văn bản. |

---

### 5.2. Bảng `Config` (Bảng cấu hình linh hoạt Core)
Dùng cho kích thước thiệp (`card_size`), phụ phí thư pháp (`card_config`), cấu hình đóng dấu logo (`flower_branding`),...

| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh bản ghi cấu hình. |
| `key` | `varchar` | `NOT NULL` | Tên khóa cấu hình (ví dụ: `standard`, `extra_words_tier1`, `watermark_rule`). |
| `value` | `jsonb` | `NOT NULL` | Dữ liệu cấu hình chi tiết (giá tiền, thông số kích thước, vị trí tọa độ, cờ bật tắt). |
| `is_public` | `boolean` | `DEFAULT false`| Cho phép phía Frontend/Client đọc trực tiếp (`true`) hay chỉ dùng nội bộ Backend (`false`). |
| `group` | `varchar` | `NOT NULL` | Nhóm cấu hình (ví dụ: `card_size`, `card_config`). Cho phép nhiều key trong cùng group; tối đa một bản ghi active (`is_public=true`, `is_deleted=false`) cho mỗi cặp `(group, key)`. |
| `kind` | `varchar` | `NOT NULL` | Thể loại cấu hình (ví dụ: `"Setting"`, `"Parameter"`). |
| `user_id` | `uuid` | `NULLABLE` | ID của Admin thực hiện cấu hình. |
| `is_deleted` | `boolean` | `DEFAULT false`| Cờ xóa mềm. |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo cấu hình. |
| `updated_at` | `timestamp` | `DEFAULT now()` | Thời điểm cập nhật cấu hình. |

---

## 6. BẢNG SẢN PHẨM CORE & GIÁ TRỊ DẪN XUẤT

### Bảng `products`
| Tên Cột (Field) | Kiểu dữ liệu | Ràng buộc | Ý nghĩa & Giải thích nghiệp vụ chi tiết |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất của Combo hoa hoặc Biến thể (Variant) con. |
| `is_active` | `boolean` | `DEFAULT true`| Trạng thái kinh doanh của sản phẩm (`true`: đang bán). |
| `is_deleted` | `boolean` | `DEFAULT false`| Cờ xóa mềm. |
| *(sellableQuantity)* | *(Derived)* | *(Tính động)* | **Không phải column trong DB**. Là giá trị tính toán theo thời gian thực bằng `CalculateAvailableQty` dựa trên công thức định lượng nguyên vật liệu và tồn kho khả dụng từ Nhanh.vn. |

---

## 7. PHÂN HỆ BÀI VIẾT TRUYỀN THÔNG (`Posts`)

| Bảng | Tên Cột (Field) | Kiểu dữ liệu | Ý nghĩa chi tiết |
| :--- | :--- | :--- | :--- |
| `platform_standards` | `id`<br>`platform`<br>`description` | `uuid [PK]`<br>`platform_type`<br>`text` | Định chuẩn văn phong, độ dài và định dạng bài viết theo từng nền tảng: Zalo, Facebook, Instagram. |
| `base_posts` | `id`<br>`content`<br>`image_url` | `uuid [PK]`<br>`text`<br>`varchar` | Bài viết gốc làm dữ liệu mẫu/chủ đề đầu vào. |
| `generated_posts` | `id`<br>`content`<br>`image_url`<br>`user_id`<br>`created_at` | `uuid [PK]`<br>`text`<br>`varchar`<br>`uuid`<br>`timestamp` | Kết quả bài viết và hình ảnh hoàn thiện do AI sinh ra. |
| `post_histories` | `id`<br>`input`<br>`base_id`<br>`output_id`<br>`system_prompt_id`<br>`metadata`<br>`created_at` | `uuid [PK]`<br>`text`<br>`uuid`<br>`uuid`<br>`uuid`<br>`json`<br>`timestamp` | Lịch sử sinh bài viết truyền thông (`base_id` trỏ `base_posts.id`, `output_id` trỏ `generated_posts.id`). |

---

## 8. CÁC KIỂU ENUM (LIỆT KÊ) HỆ THỐNG

### 8.1. `platform_type`
- `zalo`: Mạng xã hội Zalo.
- `facebook`: Mạng xã hội Facebook.
- `instagram`: Mạng xã hội Instagram.

### 8.2. `system_prompt_types`
- `flower`: Dành riêng cho phân hệ sinh mẫu hoa AI.
- `card`: Dành riêng cho phân hệ sinh nội dung và thiệp AI.
- `post`: Dành cho phân hệ sinh bài viết mạng xã hội.

### 8.3. `form_type`
- `go_may`: Định dạng chữ in chuẩn, gõ máy vi tính.
- `calligraphy`: Định dạng chữ nghệ thuật viết tay thư pháp.

---

## 9. CẤU TRÚC CHI TIẾT CỦA CÁC TRƯỜNG JSON CON

### 9.1. Cấu trúc `generated_flowers.input_snapshot` & `client_histories.metadata` (Schema v2)
```json
{
  "schema_version": 2,
  "source_product_id": "uuid-cua-variant-hoac-combo",
  "product": {
    "name": "Combo Hoàng Hôn Rực Rỡ",
    "attributes": { "tone": "cam_do", "size": "lon" },
    "image": {
      "url": "https://storage.domain.vn/products/p1.jpg",
      "object_key": "products/p1.jpg",
      "version_id": "v20",
      "sha256": "3fa85f6457174562b3fc2c963f66afa6..."
    },
    "observed_status": {
      "is_active": true,
      "is_deleted": false,
      "sellable_quantity": 4
    }
  },
  "user_input": {
    "name": "Bó hoa tặng đối tác",
    "occasion": "khai_truong",
    "style": "sang_trong",
    "budget": 1200000,
    "note": "Ưu tiên hoa hồng cam và hoa hướng dương"
  },
  "mockup": {
    "id": "uuid-cua-mockup",
    "name": "Mockup Bó Tròn Hiện Đại",
    "image": {
      "url": "https://storage.domain.vn/mockups/m1.jpg",
      "object_key": "mockups/m1.jpg",
      "version_id": "v5",
      "sha256": "4b227777d4dd1fc61c6f884f48641d02..."
    },
    "observed_status": {
      "is_active": true,
      "is_deleted": false
    }
  },
  "system_prompt": {
    "id": "uuid-cua-prompt",
    "content": "Bạn là chuyên gia thiết kế hoa cao cấp...",
    "sha256": "f2ca1bb6c7e907d06dafe4687e579fce..."
  },
  "operation": "create",
  "regenerated_from_flower_id": null,
  "system_prompt_source": "current",
  "logo_config": {
    "enabled": true,
    "position": "bottom-right",
    "version": "2026.1"
  },
  "validated_at": "2026-09-05T04:20:00Z"
}
```

### 9.2. Cấu trúc `client_histories.input` khi Regenerate
```json
{
  "operation": "regenerate",
  "source_flower_id": "550e8400-e29b-41d4-a716-446655440001",
  "mockup_id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
}
```
*(Trường `mockup_id` mang giá trị `null` nếu khách hàng không thay đổi Mockup)*.

---

## 10. BẢNG TRA CỨU QUAN HỆ ĐA HÌNH & TRUY VẾT DÒNG DÕI (LINEAGE)

| `client_histories.type` | `base_id` trỏ đến | `output_id` trỏ đến | Cách truy vết đến Product gốc để kiểm kho Order |
| :--- | :--- | :--- | :--- |
| `flower` | `products.id` | `generated_flowers.id` | `base_id` chính là `products.id`. |
| `card` (từ hoa thường) | `products.id` | `generated_cards.id` | `base_id` chính là `products.id`. |
| `card` (từ hoa AI) | `generated_flowers.id` | `generated_cards.id` | Tìm bản ghi `client_histories(type='flower')` có `output_id = card.base_id` $\rightarrow$ Lấy ra `flower.base_id` (`products.id`). |
| `handmade_card` (từ hoa thường) | `products.id` | `generated_cards.id` | `base_id` chính là `products.id`. |
| `handmade_card` (từ hoa AI) | `generated_flowers.id` | `generated_cards.id` | Tìm bản ghi `client_histories(type='flower')` có `output_id = handmade.base_id` $\rightarrow$ Lấy ra `flower.base_id` (`products.id`). |
| `post` | `base_posts.id` | `generated_posts.id` | Không liên quan đến Order bán hoa. |
