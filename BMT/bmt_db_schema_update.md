# Đề xuất cập nhật DB Schema BMT Decor (Dựa trên Website thực tế)

## 1. Bối cảnh & Lý do cập nhật

Đối chiếu schema rút gọn (7 bảng) với frontend đang chạy tại `https://bmt-fe-six.vercel.app/` và CMS Portal (`/admin/*`), phát hiện một số khoảng trống dữ liệu khiến FE không render đủ thông tin thẻ hoặc CMS không lưu đủ dữ liệu khách gửi.

Mô hình nâng cấp từ **7 bảng → 8 bảng** (bổ sung bảng `users` phục vụ login admin) và mở rộng các cột còn thiếu trong các bảng hiện tại.

---

## 2. Chi tiết các nội dung cập nhật

### 2.1. Thêm bảng `users` (Quản trị viên CMS)
- **Vấn đề:** Web có màn hình `/admin/login` (`admin@bmtdecor.vn` / `BMT@123456`) và hệ thống CMS bảo mật, nhưng schema cũ không có bảng user nào để lưu hash mật khẩu và xác thực.
- **Cấu trúc mới:**
  ```dbml
  Table users {
    id uuid [pk]
    email varchar(255) [not null, unique]
    password_hash varchar(255) [not null]
    full_name varchar(150) [not null]
    is_active boolean [not null, default: true]
    created_at timestamptz [not null]
    updated_at timestamptz [not null]
  }
  ```

---

### 2.2. Bổ sung trường cho `projects` (Thẻ dự án)
- **Vấn đề:** Carousel Trang chủ và danh sách `/projects` hiển thị Diện tích, Phong cách và Năm hoàn thành của từng dự án. Schema cũ chỉ có `card_title`, `card_category`, `card_image_url`, `detail_content`.
- **Bổ sung:**
  + `card_area varchar(50)`: Diện tích công trình (vd: `120 m²`, `450 m²`).
  + `card_style varchar(100)`: Phong cách thiết kế (vd: `Hiện đại`, `Wabi Sabi`, `Tân cổ điển`).
  + `card_year smallint`: Năm hoàn thành (vd: `2024`, `2025`, `2026`).

---

### 2.3. Bổ sung trường cho `news` (Tin tức)
- **Vấn đề:** Trang `/news` có thanh filter phân loại danh mục bài viết. Schema cũ chưa có cột danh mục.
- **Bổ sung:**
  + `category varchar(100) [not null]`: Danh mục bài viết (vd: `Kiến trúc`, `Nội thất`, `Phong thủy`, `Kinh nghiệm xây nhà`).
  + `published_at timestamptz`: Thời điểm xuất bản bài viết (hỗ trợ hẹn giờ hoặc hiển thị ngày thực tế).

---

### 2.4. Bổ sung trường cho `jobs` (Tuyển dụng)
- **Vấn đề:** Tuyển dụng cần đóng/mở nhận hồ sơ và hiển thị hạn ứng tuyển.
- **Bổ sung:**
  + `is_active boolean [not null, default: true]`: Cờ trạng thái đang tuyển / đã đóng tuyển.
  + `deadline date`: Hạn chót nhận hồ sơ.

---

### 2.5. Mở rộng bảng `form_submissions` (Khách gửi liên hệ & Báo giá)
- **Vấn đề:** 
  + Form `/contact` có nhập `email`, `service_type` (dịch vụ quan tâm), `message` (lời nhắn).
  + Form `/quotation` gửi kèm toàn bộ thông số dự toán (diện tích, gói, ngân sách, giá ước tính).
  + Nút "Ứng tuyển" ở `/careers` cần gửi đính kèm CV (`attachment_url`).
  + Admin xem chi tiết tại `/admin/contact-submissions` cần biết nguồn gửi từ trang nào.
- **Cấu trúc mở rộng:**
  ```dbml
  Enum submission_status {
    pending      // Mới tiếp nhận
    contacted    // Đã liên hệ
    done         // Đã xử lý / Hoàn thành
  }

  Table form_submissions {
    id uuid [pk]
    customer_name varchar(200) [not null]
    phone varchar(30) [not null]
    email varchar(255)
    service_type varchar(100)           // Dịch vụ quan tâm (nếu có)
    message text                        // Lời nhắn / yêu cầu
    source_page varchar(100)            // '/', '/contact', '/quotation', '/careers'
    estimate_data jsonb [default: `{}`] // Thông số dự toán từ trang Báo giá
    attachment_url varchar(1200)        // URL file CV nếu ứng tuyển
    status submission_status [not null, default: 'pending']
    created_at timestamptz [not null]
  }
  ```

---

### 2.6. Bổ sung `quotation_pricing` vào `site_settings`
- **Vấn đề:** Bảng giá thị trường (4 loại hình × 4 gói dịch vụ) hiện đang để trong tài liệu tĩnh.
- **Bổ sung:** Thêm key `quotation_pricing jsonb` vào `site_settings` để admin có thể cập nhật khoảng giá thị trường linh hoạt trên CMS mà không cần sửa code FE/BE.

---

## 3. Tổng hợp DBML hoàn chỉnh sau chỉnh sửa (8 bảng)

```dbml
// BMT Decor - Updated Schema (8 tables)
// Đồng bộ với Website https://bmt-fe-six.vercel.app/ & CMS Admin

Enum submission_status {
  pending
  contacted
  done
}

Enum page_code {
  home
  about
  services
  service_turnkey
  service_architecture_interior
  service_construction
  service_renovation
  projects
  news
  recruitment
  quotation
  contact
  capability_profile
}

// 1. users (Quản trị viên đăng nhập CMS)
Table users {
  id uuid [pk]
  email varchar(255) [not null, unique]
  password_hash varchar(255) [not null]
  full_name varchar(150) [not null]
  is_active boolean [not null, default: true]
  created_at timestamptz [not null]
  updated_at timestamptz [not null]
}

// 2. pages (Cây nội dung trang tĩnh seed sẵn)
Table pages {
  id uuid [pk]
  parent_id uuid [ref: > pages.id]
  code page_code
  node_key varchar(100) [not null]
  node_kind varchar(20) [not null]
  value jsonb [not null, default: `{}`]
  updated_at timestamptz [not null]

  indexes {
    parent_id
    (parent_id, node_key) [unique]
  }
}

// 3. projects (Dự án)
Table projects {
  id uuid [pk]
  card_title varchar(300) [not null]
  card_category varchar(100) [not null]
  card_image_url varchar(1200)
  card_area varchar(50)           // Mới: Diện tích (vd: 120 m²)
  card_style varchar(100)         // Mới: Phong cách (vd: Wabi Sabi)
  card_year smallint              // Mới: Năm hoàn thành (vd: 2024)
  detail_content jsonb [not null, default: `{}`]
  is_featured boolean [not null, default: false]
  created_at timestamptz [not null]
  updated_at timestamptz [not null]

  indexes {
    card_category
    (card_category, is_featured)
  }
}

// 4. news (Tin tức)
Table news {
  id uuid [pk]
  title varchar(300) [not null]
  category varchar(100) [not null] // Mới: Danh mục bài viết
  image_url varchar(1200)
  content jsonb [not null, default: `{}`]
  is_featured boolean [not null, default: false]
  highlight_home boolean [not null, default: false]
  excerpt text
  published_at timestamptz         // Mới: Ngày xuất bản
  created_at timestamptz [not null]
  updated_at timestamptz [not null]

  indexes {
    category
    (is_featured, created_at)
    (highlight_home, created_at)
  }
}

// 5. jobs (Tin tuyển dụng)
Table jobs {
  id uuid [pk]
  title varchar(300) [not null]
  department varchar(150)
  location varchar(255)
  schedule varchar(255)
  compensation varchar(255)
  summary text
  image_url varchar(1200)
  content jsonb [not null]
  is_active boolean [not null, default: true] // Mới: Cờ đóng/mở tuyển
  deadline date                               // Mới: Hạn nộp hồ sơ
  created_at timestamptz [not null]
  updated_at timestamptz [not null]

  indexes {
    department
    is_active
  }
}

// 6. content_slugs (Lịch sử slug Project & News, redirect 301)
Table content_slugs {
  id uuid [pk]
  slug varchar(255) [not null, unique]
  project_id uuid [ref: > projects.id]
  news_id uuid [ref: > news.id]
  is_current boolean [not null, default: true]
  created_at timestamptz [not null]

  indexes {
    project_id
    news_id
  }
}

// 7. site_settings (Cấu hình dùng chung 1 row id=1)
Table site_settings {
  id smallint [pk, default: 1]
  header jsonb [not null, default: `{}`]
  partners jsonb [not null, default: `{}`]
  footer jsonb [not null, default: `{}`]
  quotation_pricing jsonb [not null, default: `{}`] // Mới: Bảng giá 4x4
  updated_at timestamptz [not null]
}

// 8. form_submissions (Form Liên hệ, Báo giá, Ứng tuyển)
Table form_submissions {
  id uuid [pk]
  customer_name varchar(200) [not null]
  phone varchar(30) [not null]
  email varchar(255)                               // Mới: Email khách
  service_type varchar(100)                        // Mới: Dịch vụ quan tâm
  message text                                     // Mới: Lời nhắn
  source_page varchar(100)                         // Mới: Nguồn form
  estimate_data jsonb [not null, default: `{}`]    // Mới: Dự toán Báo giá
  attachment_url varchar(1200)                     // Mới: CV ứng tuyển
  status submission_status [not null, default: 'pending']
  created_at timestamptz [not null]

  indexes {
    (status, created_at)
    phone
    source_page
  }
}
```
