# Hệ Thống Hoa Theo Mùa — Kho Tài Liệu Đặc Tả (Document-First Repository)

Chào mừng bạn đến với kho lưu trữ tài liệu đặc tả yêu cầu nghiệp vụ (**User Stories**), thiết kế kỹ thuật (**Technical Design Documents - TDD**), từ điển dữ liệu (**Data Dictionary**) và kịch bản kiểm thử (**Unit Test Suite**) cho toàn bộ hệ sinh thái **Hoa Theo Mùa**.

Kho lưu trữ áp dụng phương pháp tiếp cận **Document-First** (Tài liệu đi trước mã nguồn), giúp làm rõ toàn bộ luồng nghiệp vụ, giao diện, API Contract, và kịch bản kiểm thử trước khi tiến hành lập trình.

---

## 🏢 Danh Mục Phân Hệ Dự Án

Hệ sinh thái **Hoa Theo Mùa** được chia tách thành 3 phân hệ dự án độc lập tương ứng với hệ thống quản lý công việc:

| # | Mã dự án (Key) | Tên dự án | Thư mục tài liệu | Mô tả phân hệ |
| :-: | :--- | :--- | :--- | :--- |
| **1** | `hoa-theo-mua-ai-customize` | **HTM_Flourist_AI** | [`hoa-theo-mua-ai-customize/`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/README.md) | Phân hệ cá nhân hóa mua sắm bằng AI: Tạo mẫu hoa độc bản, tạo thiệp AI tại checkout, thiệp handmade, quản lý mockup và cấu hình thiệp. |
| **2** | `hoa-theo-mua-ai-marketing` | **HTM_Marketing_AI** | [`hoa-theo-mua-ai-marketing/`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/README.md) | Phân hệ Marketing & Truyền thông AI: Tự động hóa copywriting, sinh bài viết mạng xã hội (Facebook/Instagram), ghép ảnh hoa AI theo sự kiện và mùa lễ hội. |
| **3** | `htm-material-management` | **HTM_Material_Management** | [`htm-material-management/`](file:///d:/VNZ/document-first-brief/htm-material-management/README.md) | Phân hệ Quản lý Vật liệu & Định lượng: Bóc tách nguyên vật liệu thô, công thức combo hoa (BOM), tính toán số lượng có thể bán thời gian thực, vòng đời reservation kho. |

---

## 📂 Cấu Trúc Tổng Thể Kho Lưu Trữ

```text
document-first-brief/
├── .gitignore
├── README.md                                  # Cổng tra cứu tổng quan kết nối 3 dự án
│
├── hoa-theo-mua-ai-customize/                 # [Dự án 1] HTM_Flourist_AI
│   ├── README.md                              # Tài liệu tổng quan phân hệ
│   ├── BusinessRules/                         # Quy tắc nghiệp vụ (Quota, AI Retry, Calligraphy rules)
│   ├── ConfirmedDoc/                          # API & hợp đồng kỹ thuật đã chốt
│   ├── Context/                               # Sơ đồ CSDL, Data Dictionary, Context
│   │   ├── AI_CUSTOMIZE_DB.dbdiagram
│   │   ├── AI_DB_Data_Dictionary.md
│   │   ├── AI_DB_Diagram.md
│   │   ├── AI_Flower_Context.md
│   │   ├── AI_Card_Context.md
│   │   └── AI_Mockup_Context.md
│   ├── UserStory/                             # 17 User Stories (US-003, US-030 -> US-048)
│   └── TDD/                                   # 12 TDDs (TDD-030 -> TDD-062, TDD-017, COVERAGE.md)
│
├── hoa-theo-mua-ai-marketing/                 # [Dự án 2] HTM_Marketing_AI
│   ├── README.md                              # Tài liệu tổng quan phân hệ
│   ├── BusinessRules/                         # Quy tắc nghiệp vụ phân hệ Marketing
│   ├── ConfirmedDoc/                          # Hợp đồng API
│   ├── Context/                               # Sơ đồ CSDL (generated_posts, post_histories)
│   │   └── Marketing_AI_Context.md
│   ├── UserStory/                             # Đặc tả yêu cầu người dùng
│   ├── TDD/                                   # Thiết kế kỹ thuật
│   └── UnitTest/                              # Kịch bản kiểm thử
│
└── htm-material-management/                   # [Dự án 3] HTM_Material_Management
    ├── README.md                              # Tài liệu tổng quan phân hệ
    ├── BusinessRules/                         # BR-013, BR-014, BR-018, BR-019
    ├── ConfirmedDoc/                          # DanhSach_API.md, HTM_MATERIAL_API.md
    ├── Context/                               # Ngữ cảnh kiến trúc vật liệu
    ├── UserStory/                             # 13 User Stories (US-002, US-006 -> US-019)
    ├── TDD/                                   # TDD-010, TDD-011, DiagramUS002, DiagramUS006
    ├── UnitTest/                              # 11 Test Cases chuẩn hóa Form Web
    └── Code/                                  # Mã nguồn trích xuất phục vụ kiểm thử (Product.cs)
```

---

## 📑 Danh Mục Chi Tiết Theo Phân Hệ

### 1. Phân hệ `HTM_Flourist_AI` (`hoa-theo-mua-ai-customize`)
* **Thư mục:** [`hoa-theo-mua-ai-customize/`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/)
* **Tài liệu kiến trúc cốt lõi:**
  - [`AI_CUSTOMIZE_DB.dbdiagram`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/Context/AI_CUSTOMIZE_DB.dbdiagram) — Sơ đồ CSDL AI Customize
  - [`AI_DB_Data_Dictionary.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/Context/AI_DB_Data_Dictionary.md) — Từ điển dữ liệu toàn diện & giải thích nghiệp vụ
  - [`AI_Flower_Context.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/Context/AI_Flower_Context.md) — Ngữ cảnh tạo hoa AI
  - [`AI_Card_Context.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/Context/AI_Card_Context.md) — Ngữ cảnh tạo thiệp AI tại Checkout
  - [`AI_Mockup_Context.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/Context/AI_Mockup_Context.md) — Ngữ cảnh quản lý Mockup
* **TDD & Unit Test Suite (121 Test Cases — [`COVERAGE.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/COVERAGE.md)):**
  - [`TDD-030`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-030/TDD-030_tao-yeu-cau-va-mau-hoa-ai.md): Tạo mẫu hoa AI (18 UTs)
  - [`TDD-035`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-035/TDD-035_tao-thep-thiet-ke-ai.md): Tạo thiệp thiết kế AI (19 UTs)
  - [`TDD-036`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-036/TDD-036_tao-lai-thep-tu-lich-su.md): Tạo lại thiệp từ lịch sử (12 UTs)
  - [`TDD-017`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-017/TDD-017-chon-va-tao-thiep-handmade.md): Chọn và tạo thiệp handmade
  - [`TDD-050`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-050/TDD-050_lay-danh-sach-mockup.md) $\rightarrow$ [`TDD-053`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-053/TDD-053_xoa-mockup.md): Quản lý Mockup (37 UTs)
  - [`TDD-054`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-054/TDD-054_lay-danh-sach-card-configs.md) $\rightarrow$ [`TDD-062`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/TDD/TDD-062/TDD-062_cap-nhat-card-config.md): Quản lý Card Configs (35 UTs)

---

### 2. Phân hệ `HTM_Marketing_AI` (`hoa-theo-mua-ai-marketing`)
* **Thư mục:** [`hoa-theo-mua-ai-marketing/`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/)
* **Tài liệu kiến trúc:**
  - [`Marketing_AI_Context.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/Context/Marketing_AI_Context.md) — Kiến trúc dữ liệu `generated_posts`, `post_histories` và liên kết đa hình với `client_histories`.

---

### 3. Phân hệ `HTM_Material_Management` (`htm-material-management`)
* **Thư mục:** [`htm-material-management/`](file:///d:/VNZ/document-first-brief/htm-material-management/)
* **Quy tắc nghiệp vụ (Business Rules):**
  - [`BR-013.md`](file:///d:/VNZ/document-first-brief/htm-material-management/BusinessRules/BR-013.md): Giữ dòng tham chiếu vật liệu ngừng kinh doanh.
  - [`BR-014.md`](file:///d:/VNZ/document-first-brief/htm-material-management/BusinessRules/BR-014.md): Thứ tự hiển thị theo vai trò.
  - [`BR-018.md`](file:///d:/VNZ/document-first-brief/htm-material-management/BusinessRules/BR-018.md): Hoa phụ không giới hạn khả năng bán.
  - [`BR-019.md`](file:///d:/VNZ/document-first-brief/htm-material-management/BusinessRules/BR-019.md): Combo không đủ điều kiện thì số lượng bán bằng 0.
* **Tài liệu API đã chốt:**
  - [`DanhSach_API.md`](file:///d:/VNZ/document-first-brief/htm-material-management/ConfirmedDoc/DanhSach_API.md): Tổng hợp danh sách API và xử lý Service.
  - [`HTM_MATERIAL_API.md`](file:///d:/VNZ/document-first-brief/htm-material-management/ConfirmedDoc/HTM_MATERIAL_API.md): Đặc tả request/response của các API Material.
* **Thiết kế kỹ thuật (TDD):**
  - [`TDD-010.md`](file:///d:/VNZ/document-first-brief/htm-material-management/TDD/TDD-010.md): Tìm kiếm & lọc sản phẩm có nhãn vật liệu (`GET /api/v2/products`).
  - [`TDD-011.md`](file:///d:/VNZ/document-first-brief/htm-material-management/TDD/TDD-011.md): Xem công thức định lượng combo hoa (`GET /api/v1/products/{id}/combo-specification`).
* **Kịch bản kiểm thử (UnitTest):**
  - [`All_TestCase_Templates.md`](file:///d:/VNZ/document-first-brief/htm-material-management/UnitTest/All_TestCase_Templates.md): 11 kịch bản kiểm thử chuẩn hóa form web.

---

## 🛠 Hướng Dẫn Đóng Góp

1. Khi thêm mới hoặc cập nhật tài liệu, truy cập trực tiếp vào thư mục phân hệ tương ứng (`hoa-theo-mua-ai-customize/`, `hoa-theo-mua-ai-marketing/`, hoặc `htm-material-management/`).
2. Luôn duy trì tính nhất quán giữa **UserStory**, **TDD**, **BusinessRules** và **UnitTest**.
3. Thư mục mã nguồn backend trích xuất phục vụ kiểm thử (`**/Code/`) đã được cấu hình tự động bỏ qua trong [.gitignore](file:///d:/VNZ/document-first-brief/.gitignore).
