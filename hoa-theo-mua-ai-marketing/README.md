# HTM_Marketing_AI — Phân hệ Marketing & Truyền thông AI (`hoa-theo-mua-ai-marketing`)

> **Mã dự án (Key)**: `hoa-theo-mua-ai-marketing`  
> **Tên dự án**: `HTM_Marketing_AI`  
> **Phương pháp tiếp cận**: Document-First System Specification  

---

## 📖 Tổng quan Phân hệ

**HTM_Marketing_AI** là phân hệ trí tuệ nhân tạo phục vụ hoạt động truyền thông và tiếp thị nội dung của thương hiệu Hoa Theo Mùa:
- **Tự động tạo nội dung bài viết tiếp thị (AI Copywriting)**: Sinh bài viết giới thiệu sản phẩm hoa theo mùa, sự kiện đặc biệt, ngày lễ (Valentine, 8/3, Ngày của Mẹ, Ngày Nhà giáo...), các câu chuyện ý nghĩa về từng loài hoa.
- **Ghép nối hình ảnh sản phẩm & Mẫu hoa AI**: Tự động liên kết hình ảnh mẫu hoa (từ catalog sản phẩm hoặc các mẫu hoa độc bản do khách hàng/AI đã sinh ra) kèm thông điệp truyền thông.
- **Đa dạng hóa giọng văn thương hiệu (Brand Tone & Personas)**: Hệ thống Prompts cho phép tạo nhiều phong cách viết khác nhau (thơ mộng, ấm áp, trang trọng, trẻ trung, dí dỏm).
- **Lưu trữ & Truy vết phả hệ nội dung**: Dữ liệu bài viết được lưu trữ trong bảng `generated_posts` và `post_histories`, liên kết đa hình qua bảng `client_histories`.

---

## 📂 Cấu trúc Thư mục

```text
hoa-theo-mua-ai-marketing/
├── README.md                              # Tài liệu tổng quan phân hệ
├── BusinessRules/                         # Quy tắc nghiệp vụ (Giới hạn ký tự, bản quyền ảnh, kiểm duyệt nội dung)
├── ConfirmedDoc/                          # Hợp đồng API & tài liệu đã chốt
├── Context/                               # Ngữ cảnh kiến trúc & Sơ đồ CSDL
│   └── Marketing_AI_Context.md            # Tài liệu kiến trúc dữ liệu và bảng generated_posts / post_histories
├── UserStory/                             # Đặc tả yêu cầu người dùng (User Stories)
├── TDD/                                   # Thiết kế kỹ thuật chi tiết (Technical Design Documents)
└── UnitTest/                              # Kịch bản kiểm thử đơn vị
```

---

## 📑 Danh mục Tài liệu Kỹ thuật

- **[Marketing_AI_Context.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/Context/Marketing_AI_Context.md)**: Ngữ cảnh kiến trúc phân hệ Marketing AI, đặc tả bảng CSDL `generated_posts`, `post_histories` và liên kết với `client_histories`.
- **User Stories & TDDs**: Sẽ được bổ sung và cập nhật theo kế hoạch phát triển của phân hệ.
