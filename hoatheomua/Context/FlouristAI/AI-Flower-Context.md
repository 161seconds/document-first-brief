# AI Flower Context - HTM_Flourist_AI

> Document First Project: hoa-theo-mua-ai-customize  
> Date: 2026-08-27  
> Last Updated: 2026-08-27

---

## 1. TỔNG QUAN

### Mô tả

Hệ thống tạo mẫu hoa AI cho phép khách hàng tạo ảnh mẫu hoa từ Combo, Mockup và thông tin đã nhập. STORY-030 và STORY-033 cùng mô tả một lần gọi API tạo hoa; không có bước lưu yêu cầu nháp hay tạo AI Job riêng.

### Luồng chính

```text
Combo Card + Mockup + thông tin người dùng nhập
                    |
                    v
        POST /api/ai-flowers (STORY-030 + STORY-033)
                    |
                    v
      Check quota -> gọi AI/retry -> nhận ảnh kết quả
                    |
                    v
  Thành công: generated_flowers + client_histories -> Checkout/Order
  Thất bại: trả lỗi, không lưu request/job/history
```

### Stories liên quan

| Story | Vai trò trong flow chung | Sprint | Priority | Status |
|---|---|---:|---|---|
| **STORY-030** | Nhận dữ liệu để tạo mẫu hoa | 1 | Must | Draft |
| **STORY-033** | Gọi AI và trả mẫu hoa đã tạo | 1 | Must | Draft |
| **STORY-038** | Khởi tạo Checkout từ mẫu hoa | 1 | Must | Draft |
| **STORY-039** | Hoàn tất Checkout, tạo và thanh toán Order | 1 | Must | Draft |

---

## 2. LUỒNG NGHIỆP VỤ

### STORY-030 và STORY-033: Tạo mẫu hoa AI

#### Preconditions

- Khách hàng đã đăng nhập và tài khoản đang hoạt động.
- Combo hợp lệ để dùng làm nguồn tạo hoa.
- Mockup được chọn có `is_active = true` và `is_deleted = false`.
- Khách hàng còn quota tạo hoa trong ngày.

#### Trigger

Khách hàng chọn Combo Card, chọn Mockup, nhập thông tin trên form và nhấn tạo mẫu hoa.

#### Input và xử lý

- API nhận toàn bộ dữ liệu form cùng `product_id` của Combo và `mockup_id`.
- Dữ liệu đó được truyền trực tiếp cho AI Module; không tạo `flower_request` hay `flower_ai_job`.
- AI Module lấy ảnh/thông tin Combo, Mockup và system prompt `type = "flower"`, sau đó tạo ảnh.
- API trả ảnh tạo thành công trong cùng flow. Khi cần tạo lại, client gửi một request tạo hoa mới với dữ liệu cần dùng; không có endpoint regenerate theo `flower_request_id`.

#### Lưu kết quả

- Chỉ khi AI trả ảnh hợp lệ, tạo một dòng `generated_flowers` và một dòng `client_histories` có `type = "flower"`.
- `client_histories.input` lưu JSON request gốc của lần thành công để phục vụ history; không phải trạng thái tạm của yêu cầu tạo hoa.
- `client_histories.base_id = products.id`; `output_id = generated_flowers.id`.
- `metadata` lưu snapshot Mockup đã dùng.
- Khi AI thất bại sau retry, không tạo `generated_flowers`, `client_histories`, `flower_requests` hoặc `flower_ai_jobs`.

### STORY-038: Khởi tạo Checkout

- Checkout là phiên tạm, được mở từ mẫu hoa đã tạo hoặc sản phẩm thường.
- Refresh giữ Checkout; rời luồng, đóng tab, đăng xuất hoặc crash thì Checkout bị hủy.
- Tối đa một thiệp được chọn trong Checkout.

### STORY-039: Hoàn tất Checkout & Order

| Trạng thái | Mô tả |
|---|---|
| PENDING | Chờ thanh toán (24 giờ) |
| PAID | Đã thanh toán |
| CANCELLED | Hủy do hết hạn hoặc khách hủy |
| REFUNDED | Hoàn tiền |

---

## 3. DATABASE SCHEMA

### 3.1 Enums

```sql
Enum generation_type {
  flower
  card
  post
}

Enum system_prompt_types {
  flower
  card
  post
}
```

### 3.2 Tables liên quan

#### products

```sql
Table products {
  id uuid [pk]
  -- Các field khác thuộc Core Database
}
```

#### mockup

Mockup là nguồn tham chiếu cho kiểu dáng, bố cục. Cấu trúc và quy tắc quản lý được giữ theo AI Mockup Context.

```sql
Table mockup {
  id uuid [pk]
  name varchar
  description text
  image_url varchar
  is_active boolean
  is_deleted boolean
  created_at timestamp
  updated_at timestamp
}
```

#### system_prompts

```sql
Table system_prompts {
  id uuid [pk]
  type system_prompt_types
  content text
}
```

#### generated_flowers

Chỉ lưu ảnh kết quả AI thành công. Không lưu lại các field input hoặc liên kết đến request/job trung gian.

```sql
Table generated_flowers {
  id uuid [pk]
  image_url varchar
  user_id uuid
  created_at timestamp
}
```

#### client_histories

```sql
Table client_histories {
  id uuid [pk]
  user_id uuid
  input text                    -- JSON request gốc của lần tạo thành công
  type generation_type          -- flower | card | post
  system_prompt_id uuid
  metadata json                 -- Snapshot Mockup khi type = flower
  created_at timestamp

  -- Polymorphic references:
  -- type = flower: base_id -> products.id, output_id -> generated_flowers.id
  -- type = card: base_id -> products.id hoặc generated_flowers.id,
  --              output_id -> generated_cards.id
  base_id uuid
  output_id uuid
}
```

### 3.3 Bảng không tồn tại trong flow này

`flower_requests` và `flower_ai_jobs` không thuộc schema. Chúng không cần thiết vì API không có trạng thái request nháp, queue hay job cần lưu lâu dài.

### 3.4 Quan hệ và JSON

```text
products.id --(base_id)--> client_histories(type=flower)
client_histories.output_id --> generated_flowers.id
client_histories.system_prompt_id --> system_prompts.id
```

`metadata` khi tạo hoa:

```json
{
  "mockup_id": "uuid",
  "mockup_name": "Tên mockup",
  "mockup_image_url": "url"
}
```

---

## 4. QUY TẮC NGHIỆP VỤ

### 4.1 Nguồn tạo hoa

- `base_id` là `products.id` của Combo đủ điều kiện theo Core Database.
- Thông tin Combo và Mockup được lấy tại thời điểm gọi API rồi truyền cho AI Module.
- Mockup phải còn active và chưa soft-delete tại thời điểm gọi API.

### 4.2 Thứ tự ưu tiên Prompt

`Combo > Mockup > Ghi chú/Yêu cầu thêm > Phong cách`

### 4.3 Input cho AI Module

| Input | Nguồn |
|---|---|
| Dữ liệu người dùng nhập | Request API; chỉ dùng trong lần gọi AI |
| Thông tin và ảnh Combo | `products` / Core Database |
| Mockup | `mockup` được chọn |
| System prompt | `system_prompts` với `type = "flower"` |

### 4.4 Kết quả ảnh

- Ảnh được gắn logo theo cấu hình trước khi lưu.
- Chỉ ảnh kết quả hợp lệ mới được lưu trong `generated_flowers`.

---

## 5. QUOTA RULES

| Loại | Giới hạn | Đơn vị |
|---|---:|---|
| Tạo mẫu hoa | 3 lượt | /ngày/khách hàng |

1. Check quota trước khi gọi AI.
2. Chỉ trừ quota khi nhận kết quả thành công.
3. AI Module retry tối đa 2 lần, interval 2 giây.
4. Nếu vẫn thất bại, không lưu kết quả/history và quota không bị trừ.

---

## 6. API ENDPOINTS

### FLOWER – Tạo mẫu hoa AI

| Stories | Chức năng | Method | Route | Ghi chú |
|---|---|---|---|---|
| STORY-030, STORY-033 | Tạo mẫu hoa AI | POST | `/api/ai-flowers` | Nhận input, gọi AI và trả kết quả trong một flow |
| STORY-030 | Lấy danh sách Mockup | GET | `/api/mockups` | Dùng Mockup active, chưa soft-delete |

Không có endpoint `/api/flower-requests`, endpoint generate/regenerate theo `flower_request_id`, hoặc endpoint kết quả theo request id.

### FLOWER – Checkout & Order

| Story | Chức năng | Method | Route |
|---|---|---|---|
| STORY-038 | Tạo Order | POST | `/api/orders` |
| STORY-038 | Lấy thông tin Order | GET | `/api/orders/{id}` |
| STORY-039 | Thanh toán Order | POST | `/api/orders/{id}/pay` |
| STORY-039 | Hủy Order | POST | `/api/orders/{id}/cancel` |

---

## GHI CHÚ QUAN TRỌNG

- AI Module tách riêng; context này chỉ mô tả contract và lưu kết quả sau khi AI thành công.
- `generated_flowers` không chứa `flower_request_id`, `occasion`, `style`, `budget` hay `note`.
- `flower_requests` và `flower_ai_jobs` đã được loại bỏ hoàn toàn.
- Context Card, Mockup và các file TDD không bị chỉnh sửa.
