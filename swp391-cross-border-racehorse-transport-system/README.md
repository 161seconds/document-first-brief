# Hệ Thống Vận Chuyển Ngựa Đua Xuyên Biên Giới (`swp391-cross-border-racehorse-transport-system`)

> **Mã dự án (Key)**: `swp391-cross-border-racehorse-transport-system`  
> **Tên dự án**: `Cross-Border Racehorse Transport System` (Hệ thống quản lý và vận chuyển ngựa đua qua biên giới)  
> **Khóa học/Đề tài**: SWP391  
> **Phương pháp tiếp cận**: Document-First System Specification (BDD Acceptance Criteria)  
> **Trạng thái**: Khởi tạo cấu trúc thư mục đặc tả chuẩn  
> **Cập nhật gần nhất**: 2026-09-08  

---

## 📖 1. Tổng quan Dự án

**Cross-Border Racehorse Transport System** là hệ thống số chuyên biệt quản lý quy trình logistics, kiểm dịch, thủ tục hải quan và theo dõi hành trình vận chuyển ngựa đua qua biên giới giữa các quốc gia, đảm bảo an toàn sức khỏe sinh học tối đa và tuân thủ các công ước quốc tế.

### 🎯 Các phân vùng năng lực dự kiến:
1. **Quản lý Hồ sơ & Y tế Ngựa Đua (Racehorse Passport & Health Cert)**:
   - Hồ sơ định danh (chip, ADN, phả hệ, lịch sử tiêm chủng và kết quả xét nghiệm dịch tễ).
2. **Quản lý Đơn Vận & Lộ trình Vận chuyển (Transport Booking & Itinerary)**:
   - Đặt lịch xe/máy bay chuyên dụng, lộ trình di chuyển, trạm dừng kiểm tra sức khỏe và chuồng cách ly trung chuyển.
3. **Thủ tục Hải quan & Giấy phép Xuyên Biên giới (Customs & Border Permits)**:
   - Cấp phép xuất/nhập cảnh tạm thời phục vụ thi đấu, kiểm dịch động vật sống tại cửa khẩu.
4. **Theo dõi Trạng thái Thời gian Thực (Real-time Health & Telemetry Tracking)**:
   - Giám sát nhiệt độ, nhịp tim, tình trạng chuồng và vị trí GPS suốt hành trình vận chuyển.

---

## 📂 2. Cấu trúc Thư mục Phân hệ

```text
swp391-cross-border-racehorse-transport-system/
├── README.md                              # Cổng thông tin & Cẩm nang tra cứu tổng thể dự án
├── BusinessRules/                         # Quy tắc nghiệp vụ (Kiểm dịch, Hải quan, Giới hạn vận chuyển)
├── ConfirmedDoc/                          # Hợp đồng API & Tài liệu kỹ thuật đã phê duyệt
├── Context/                               # Ngữ cảnh kiến trúc, mô hình CSDL (ERD) & Từ điển dữ liệu
├── UserStory/                             # User Stories đặc tả yêu cầu nghiệp vụ theo chuẩn BDD
├── SystemTest/                            # Kịch bản kiểm thử hệ thống chuẩn hóa (chia dòng)
├── TDD/                                   # Thiết kế kỹ thuật chi tiết (Technical Design Documents)
└── UnitTest/                              # Kịch bản kiểm thử đơn vị & Ma trận Test Cases
```

---

## 📐 3. Biểu Mẫu Chuẩn Áp Dụng (Document-First Templates)

- **User Story**: [`../template-US.md`](file:///d:/VNZ/document-first-brief/template-US.md)
- **Business Rule**: [`../template-BR.md`](file:///d:/VNZ/document-first-brief/template-BR.md)
- **System Test**: [`../template-SystemTest.md`](file:///d:/VNZ/document-first-brief/template-SystemTest.md)
- **Technical Design Document**: [`../template-TDD.md`](file:///d:/VNZ/document-first-brief/template-TDD.md)
- **Unit Test**: [`../template-UnitTest.md`](file:///d:/VNZ/document-first-brief/template-UnitTest.md)
