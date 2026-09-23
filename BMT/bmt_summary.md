# Tóm tắt kiến trúc & dữ liệu BMT Decor

## 1. Nguyên tắc cốt lõi (Bất biến)

- **Frontend cố định hoàn toàn:** Route, layout, thứ tự section, số slot item, button label/target, CTA, aspect ratio và kích thước ảnh.
- **Admin chỉ sửa nội dung:** Chỉ cập nhật text và ảnh vào các slot/khung có sẵn. Không được tạo/xóa page/section/item, không nhập slug/URL, không chỉnh layout.
- **Xử lý ảnh:** Client gửi file → Backend upload Cloudinary → DB chỉ lưu URL (string). DB không lưu `width`, `height`, `ratio`, `alt`.
- **Hệ thống rút gọn v2:** Gồm 9 bảng. Không có role, session phức tạp, audit log, revision, page builder hay media library.

---

## 2. Mô hình 9 bảng (PostgreSQL v2 - Đã chốt)

| STT | Bảng | Mục đích | Cấu trúc & Quy tắc chính |
|---|---|---|---|
| **1** | `pages` | Nội dung các trang tĩnh / landing | **Cây đệ quy seed sẵn trong 1 bảng**:<br>- Root: `parent_id IS NULL`, có `code` (`page_code`), `node_kind = 'page'`.<br>- Child: `parent_id` trỏ node cha, `code IS NULL`, `node_key` ổn định do FE định nghĩa (`hero`, `journey`, `item_01`...).<br>- `value jsonb`: Chỉ lưu text/ảnh được whitelist. Admin chỉ `UPDATE value`. |
| **2** | `projects` | Danh sách & chi tiết dự án | - Thẻ danh sách: `card_title`, `card_category`, `card_image_url`, `is_featured` (tối đa 8/danh mục).<br>- Chi tiết: `detail_content jsonb` (chứa các block cố định: `overview`, `survey`, `solution`, `renders`, `process`, `comparisons`, `contactForm`). |
| **3** | `news` | Bài viết tin tức | - `title`, `excerpt`, `image_url` (desktop), `content jsonb` (`body` rich text).<br>- Cờ hiển thị: `is_featured` (trang tin, tối đa 5), `highlight_home` (trang chủ, tối đa 4). |
| **4** | `jobs` | Tin tuyển dụng | - Thông tin cơ bản: `title`, `department`, `location`, `schedule`, `compensation`, `summary`, `image_url`.<br>- `content jsonb`: Bắt buộc đủ 2 key HTML `responsibilities` và `benefits`. |
| **5** | `content_slugs` | Lịch sử slug Project & News | - `slug` sinh tự động từ title, unique toàn hệ thống.<br>- Có đúng 1 trong 2 FK `project_id` hoặc `news_id`.<br>- Đổi title → sinh slug mới (`is_current = true`), slug cũ giữ nguyên (`is_current = false`) để redirect HTTP 301. Xóa record thì xóa hết slug. |
| **6** | `site_settings` | Cấu hình dùng chung (Header, Footer, Partners) | **Bảng duy nhất 1 row (`id = 1`)**:<br>- `header jsonb`: URL logo.<br>- `partners jsonb`: Tiêu đề + 6 logo đối tác (`partner1LogoImage`..`partner6LogoImage`).<br>- `footer jsonb`: Logo, ảnh widget fanpage, thông tin liên hệ, và 4 dịch vụ (`service1Label`/`service1PageName`..`service4Label`/`service4PageName` - chỉ lưu tên trang, FE tự map sang route). |
| **7** | `form_submissions` | Khách gửi form liên hệ / báo giá | - Chỉ lưu `customer_name`, `phone`, `status` (`pending` / `done`), `created_at`.<br>- Mọi form liên hệ và báo giá đều dùng chung; không lưu payload tính toán. |
| **8** | `priceRanges` | 16 khoảng giá thị trường cho Báo giá | - `building_type` (4 loại hình), `service_type` (4 gói dịch vụ).<br>- `unit_price_min`, `unit_price_max` (VND/m²).<br>- Unique `(building_type, service_type)`. Admin sửa min/max trên FE, FE tự tính dự toán tại trình duyệt. |
| **9** | `admin_users` | Tài khoản đăng nhập CMS Admin | - `email` (unique, lowercase), `password_hash`, `display_name`, `is_active`, `created_at`, `updated_at`.<br>- Xác thực đăng nhập `/admin/login` và bảo vệ các endpoint PATCH. |

---

## 3. Các `page_code` cố định trong `pages`

1. `home` (Trang chủ)
2. `about` (Giới thiệu)
3. `services` (Tổng quan dịch vụ)
4. `service_turnkey` (Xây dựng trọn gói)
5. `service_architecture_interior` (Thiết kế kiến trúc & nội thất)
6. `service_construction` (Thi công xây dựng)
7. `service_renovation` (Cải tạo & sửa chữa)
8. `projects` (Dự án - lưu các section tĩnh của trang danh sách)
9. `news` (Tin tức - lưu các section tĩnh của trang tin)
10. `recruitment` (Tuyển dụng - lưu banner/intro)
11. `quotation` (Báo giá)
12. `contact` (Liên hệ)
13. `capability_profile` (Hồ sơ năng lực)

---

## 4. Công thức ước tính chi phí (Trang Báo giá)

### Luồng 5 bước
`01 Loại hình` → `02 Diện tích` → `03 Ngân sách` → `04 Gói` → `05 Ước tính`

### Công thức tính
- `[giaMin, giaMax] = BANG_GIA[loaiHinh][goi]`
- `min = dienTich * giaMin`
- `max = dienTich * giaMax`
- `donGia = (giaMin + giaMax) / 2`
- Làm tròn `min`, `max`, `donGia` đến 1.000 đ.

### Bảng giá thị trường (đ/m² sàn)

| Loại hình | Xây dựng trọn gói | Thiết kế KT & NT | Thi công xây dựng | Cải tạo & sửa chữa |
|---|---|---|---|---|
| **Nhà ở** | 5.2 - 6.5 tr (tb 5.85) | 270k - 300k (tb 285k) | 3.35 - 4.3 tr (tb 3.825) | 2.0 - 5.0 tr (tb 3.5) |
| **Văn phòng** | 6.75 - 7.15 tr (tb 6.95) | 100k - 260k (tb 180k) | 3.0 - 4.0 tr (tb 3.5) | 2.0 - 6.0 tr (tb 4.0) |
| **Thẩm mỹ viện, showroom** | 5.0 - 10.0 tr (tb 7.5) | 140k - 350k (tb 245k) | 3.0 - 4.0 tr (tb 3.5) | 2.0 - 6.0 tr (tb 4.0) |
| **Nhà hàng, khách sạn** | 5.2 - 7.5 tr (tb 6.35) | 250k - 300k (tb 275k) | 3.4 - 4.2 tr (tb 3.8) | 5.0 - 8.0 tr (tb 6.5) |

### So sánh ngân sách khách nhập
- `nganSach < min` → `thieu`: báo thấp hơn min khoảng `|tyLe|%`.
- `min <= nganSach <= max` → `phu_hop`: nằm trong khoảng min - max.
- `nganSach > max` → `du`: cao hơn max khoảng `tyLe%`.
