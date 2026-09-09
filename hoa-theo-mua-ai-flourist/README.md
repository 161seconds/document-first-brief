# HTM_Flourist_AI — Phân hệ Thiết kế Hoa & Thiệp AI (`hoa-theo-mua-ai-customize`)

> **Mã dự án (Key)**: `hoa-theo-mua-ai-customize`  
> **Tên dự án**: `HTM_Flourist_AI`  
> **Phương pháp tiếp cận**: Document-First System Specification  

---

## 📖 Tổng quan Phân hệ

**HTM_Flourist_AI** là phân hệ AI cá nhân hóa trải nghiệm mua sắm hoa cho khách hàng:
- **Tạo mẫu hoa với AI**: Sinh hình ảnh phối hoa độc bản từ mô tả của khách hàng, gắn logo thương hiệu Hoa Theo Mùa và quản lý quota tạo hoa (3 lượt/ngày).
- **Tạo thiệp chúc mừng AI tại Checkout**: Cá nhân hóa thiệp tặng kèm (`gõ máy` miễn phí, `calligraphy` tính phí theo quy tắc độ dài), quota 5 lượt/ngày, lưu vết lịch sử.
- **Tái tạo thiệp từ lịch sử (Regenerate)**: Tái sinh mẫu thiệp từ đơn hàng cũ với thông số lưu trữ bất biến.
- **Thiệp Handmade (Thủ công)**: Lựa chọn mẫu thiệp viết tay không gọi AI.
- **Quản trị Mockup & Cấu hình Thiệp**: Admin quản lý thư viện mockup bình hoa/hộp hoa và các gói kích thước, giá thiệp calligraphy.

---

## 📂 Cấu trúc Thư mục

```text
hoa-theo-mua-ai-customize/
├── README.md                              # Tài liệu tổng quan phân hệ
├── BusinessRules/                         # Quy tắc nghiệp vụ (Quota, AI Retry, Calligraphy rules)
├── ConfirmedDoc/                          # Tài liệu & API đã chốt
├── Context/                               # Sơ đồ CSDL, Từ điển dữ liệu & Kiến trúc
│   ├── AI_CUSTOMIZE_DB.dbdiagram          # DB Diagram định dạng dbml/dbdiagram
│   ├── AI_DB_Data_Dictionary.md           # Từ điển dữ liệu & giải thích chi tiết các bảng
│   ├── AI_DB_Diagram.md                   # Sơ đồ quan hệ thực thể (ERD) dạng Markdown
│   ├── AI_Flower_Context.md               # Ngữ cảnh nghiệp vụ Tạo mẫu hoa AI
│   ├── AI_Card_Context.md                 # Ngữ cảnh nghiệp vụ Tạo thiệp AI
│   └── AI_Mockup_Context.md               # Ngữ cảnh nghiệp vụ Quản lý Mockup
├── UserStory/                             # 17 Tài liệu User Stories
├── TDD/                                       # 24 Tài liệu Thiết kế Kỹ thuật (TDD)
│   ├── COVERAGE.md                            # Ma trận bao phủ kiểm thử (Test Coverage Matrix)
│   ├── README.md                              # Danh mục tài liệu TDD
│   ├── TDD-006-tao-thiep-thiet-ke-ai.md       # TDD Tạo thiệp thiết kế AI (v1.3)
│   ├── TDD-007-tao-lai-thiep-tu-lich-su.md    # TDD Tạo lại thiệp từ lịch sử (v1.3)
│   ├── TDD-008-lay-danh-sach-card-configs.md  # TDD Lấy danh sách Card Configs
│   ├── TDD-009-tao-card-config.md             # TDD Tạo Card Config
│   ├── TDD-010-cap-nhat-card-config.md        # TDD Cập nhật Card Config
│   ├── TDD-011-xoa-card-config.md             # TDD Xóa Card Config
│   ├── TDD-012-lay-danh-sach-mockup.md        # TDD Lấy danh sách Mockup
│   ├── TDD-013-tao-mockup.md                  # TDD Tạo Mockup
│   ├── TDD-014-chuyen-trang-thai-mockup.md    # TDD Chuyển trạng thái Mockup
│   ├── TDD-015-xoa-mockup.md                  # TDD Xóa Mockup
│   ├── TDD-016-tao-yeu-cau-va-mau-hoa-ai.md   # TDD Tạo và tạo lại mẫu hoa AI (v1.1)
│   ├── TDD-017-chon-va-tao-thiep-handmade.md  # TDD Chọn và tạo thiệp handmade
│   ├── TDD-027-tao-lai-thiep-ai-giu-thiet-ke-goc.md # TDD Tạo lại thiệp AI giữ thiết kế gốc (v1.5)
│   ├── TDD-030_tao-yeu-cau-va-mau-hoa-ai.md   # TDD Tạo mẫu hoa AI (chi tiết)
│   ├── TDD-035_tao-thep-thiet-ke-ai.md        # TDD Tạo thiệp thiết kế AI (chi tiết)
│   ├── TDD-036_tao-lai-thep-tu-lich-su.md     # TDD Tạo lại thiệp từ lịch sử (chi tiết)
│   └── ... (các TDD CRUD Mockup & Card Config)
└── UnitTest/                                  # 121 Kịch bản kiểm thử đơn vị (Unit Tests)
    ├── README.md                              # Danh mục ma trận 121 Unit Test Cases
    └── UT-*.md                                # 121 Unit Test Cases độc lập
```

---

## 📑 Danh mục User Stories & TDD

| Mã Story | Nhóm tính năng | TDD liên quan | Mô tả |
| :--- | :--- | :--- | :--- |
| **STORY-003** | Duyệt sản phẩm | — | Xem danh sách Combo hoa khả dụng |
| **STORY-030** | Khởi tạo yêu cầu | [TDD-016](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-016-tao-yeu-cau-va-mau-hoa-ai.md), [TDD-030](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-030_tao-yeu-cau-va-mau-hoa-ai.md) | Khởi tạo form yêu cầu tạo mẫu hoa AI |
| **STORY-033** | Sinh ảnh AI | [TDD-016](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-016-tao-yeu-cau-va-mau-hoa-ai.md), [TDD-030](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-030_tao-yeu-cau-va-mau-hoa-ai.md) | Tạo mẫu hoa với AI, retry & đóng logo |
| **STORY-034** | Tải ảnh | [TDD-030](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-030_tao-yeu-cau-va-mau-hoa-ai.md) | Tải ảnh mẫu hoa AI độ phân giải cao |
| **STORY-035** | Thiệp chúc mừng | [TDD-006](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-006-tao-thiep-thiet-ke-ai.md), [TDD-035](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-035_tao-thep-thiet-ke-ai.md) | Tạo thiệp cá nhân hóa tại trang Checkout |
| **STORY-036** | Tái tạo thiệp | [TDD-007](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-007-tao-lai-thiep-tu-lich-su.md), [TDD-036](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-036_tao-lai-thep-tu-lich-su.md) | Tạo lại thiệp từ lịch sử đơn hàng cũ |
| **STORY-038** | Checkout | — | Khởi tạo Checkout từ mẫu hoa AI |
| **STORY-039** | Thanh toán | — | Hoàn tất đặt hàng và thanh toán |
| **STORY-040** | Lịch sử hoa AI | — | Xem lịch sử các mẫu hoa AI của khách hàng |
| **STORY-041** | Tải ảnh thiệp | — | Tải ảnh thiệp AI đã tạo |
| **STORY-042** | Chọn thiệp cũ | — | Chọn thiệp từ lịch sử gán vào đơn hàng mới |
| **STORY-043** | Admin xem thiệp | — | Xem chi tiết thiệp AI trong trang Quản trị |
| **STORY-044** | Admin DS thiệp | — | Xem danh sách toàn bộ thiệp AI đã tạo |
| **STORY-045** | DS thiệp User | — | Xem lịch sử các thiệp chúc mừng cá nhân |
| **STORY-046** | Admin tải thiệp | — | Quản trị viên tải file ảnh thiệp phục vụ in ấn |
| **STORY-047** | Admin DS hoa AI | — | Quản trị viên duyệt danh sách mẫu hoa AI |
| **STORY-048** | Admin chi tiết hoa | — | Xem chi tiết prompt & tham số sinh hoa AI |
| — | Tái tạo giữ gốc | [TDD-027](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-027-tao-lai-thiep-ai-giu-thiet-ke-goc.md) | Tạo Card AI mới giữ nguyên thiết kế/size, đổi nội dung |
| — | Thiệp Handmade | [TDD-017](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-017-chon-va-tao-thiep-handmade.md) | Quy trình chọn và tạo thiệp thủ công |
| — | Quản lý Mockup | [TDD-050](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-050_lay-danh-sach-mockup.md), [TDD-051](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-051_tao-mockup.md), [TDD-052](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-052_chuyen-trang-thai-mockup.md), [TDD-053](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-053_xoa-mockup.md) | CRUD Mockup |
| — | Quản lý Card Config | [TDD-054](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-054_lay-danh-sach-card-configs.md), [TDD-055](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-055_tao-card-config.md), [TDD-056](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-056_xoa-card-config.md), [TDD-062](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-flourist/TDD/TDD-062_cap-nhat-card-config.md) | Cấu hình kích thước & quy tắc giá thiệp |
