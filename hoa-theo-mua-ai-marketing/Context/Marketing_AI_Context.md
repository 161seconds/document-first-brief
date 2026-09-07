# Marketing AI Context & Architecture — HTM_Marketing_AI

> **Project**: Hoa Theo Mùa (`hoa-theo-mua-ai-marketing`)  
> **Subsystem**: `HTM_Marketing_AI` (Phân hệ Marketing & Truyền thông AI)  
> **Document Status**: Draft / Initial Specification  

---

## 1. TỔNG QUAN PHÂN HỆ

Phân hệ **Marketing AI** (`HTM_Marketing_AI`) hỗ trợ đội ngũ Marketing và Quản trị viên tự động hóa việc tạo nội dung truyền thông cho thương hiệu Hoa Theo Mùa:
- **Sinh bài viết mạng xã hội (Social Media Posts)**: Tự động soạn thảo nội dung caption/post giới thiệu mẫu hoa mới, chương trình khuyến mãi theo mùa hoặc các ngày lễ (Valentine, 8/3, 20/10, Tết...).
- **Gắn kết hình ảnh sản phẩm & Hoa AI**: Tự động ghép nối hình ảnh mẫu hoa (từ sản phẩm catalog hoặc mẫu hoa AI đã sinh) vào bài viết tiếp thị.
- **Tạo thông điệp & slogan tiếp thị**: Gợi ý các thông điệp chúc mừng, câu chuyện ý nghĩa của từng loài hoa dựa trên bộ dữ liệu phong cách thương hiệu.

---

## 2. THIẾT KẾ CƠ SỞ DỮ LIỆU LIÊN QUAN

Dựa trên thiết kế kiến trúc cốt lõi từ hệ thống CSDL trung tâm, phân hệ quản lý các thực thể sau:

### 2.1. Bảng `generated_posts` (Bài viết do AI tạo)
Lưu trữ nội dung bài viết và liên kết media đã được sinh ra bởi AI:

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Ý Nghĩa |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh duy nhất bài viết |
| `content` | `text` | `NOT NULL` | Nội dung văn bản bài viết truyền thông |
| `image_url` | `varchar` | `NULLABLE` | Đường dẫn ảnh đính kèm bài viết |
| `user_id` | `uuid` | `NOT NULL` | ID người dùng / Admin tạo bài viết |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo bài viết |

### 2.2. Bảng `post_histories` (Lịch sử tạo bài viết)
Lưu lại toàn bộ tham số prompt đầu vào, hệ thống prompt và snapshot metadata:

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Ý Nghĩa |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | `PRIMARY KEY` | Định danh bản ghi lịch sử |
| `input` | `text` | `NOT NULL` | Chuỗi JSON chứa prompt yêu cầu và tham số cấu hình |
| `base_id` | `uuid` | `NOT NULL` | ID tham chiếu gốc (`products.id` hoặc `base_posts.id`) |
| `output_id` | `uuid` | `NOT NULL` | Trỏ tới `generated_posts.id` |
| `system_prompt_id` | `uuid` | `NULLABLE` | Trỏ tới `system_prompts.id` áp dụng cho văn phong Marketing |
| `metadata` | `json` | `NOT NULL` | Snapshot bất biến lưu cấu hình và ngữ cảnh khi tạo |
| `created_at` | `timestamp` | `DEFAULT now()` | Thời điểm tạo |

### 2.3. Tích hợp đa hình với `client_histories`
Khi bài viết được tạo trong luồng người dùng:
- `client_histories.type = 'post'`
- `client_histories.base_id = base_posts.id` (hoặc `products.id`)
- `client_histories.output_id = generated_posts.id`

---

## 3. CÁC TÍNH NĂNG DỰ KIẾN (ROADMAP)

1. **Sinh nội dung bài đăng Facebook / Instagram theo mẫu hoa**:
   - Chọn mẫu hoa hoặc sản phẩm combo -> Chọn dịp (Sinh nhật, Khai trương, Lễ tình nhân) -> AI tạo 3 biến thể caption.
2. **Quản lý System Prompts theo Persona**:
   - Định nghĩa phong cách viết (Sang trọng, Ấm áp, Hài hước, Tinh tế).
3. **Lên lịch đăng bài và xuất bản đa kênh**:
   - Tích hợp webhook kết nối Fanpage / Instagram Business.
