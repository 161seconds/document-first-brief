# UT-051-09: Không tạo Mockup khi upload ảnh thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không tạo Mockup khi upload ảnh thất bại
- **Ghi chú**: Kịch bản error - khi upload ảnh thất bại, không tạo mockup và không lưu dữ liệu.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-09
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Request có dữ liệu hợp lệ (name, description, image)
  - File upload service mock throw exception khi upload
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup",
    "image": [file upload]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 500
  Response body:
  {
    "error": {
      "code": "INTERNAL_SERVER_ERROR",
      "message": "Không thể tải ảnh lên. Vui lòng thử lại."
    }
  }
  
  Kiểm tra side effect:
  - Database không có mockup mới được tạo
  - Không có partial record
  
  Lý do: Khi upload thất bại, không tạo mockup trong database để tránh orphan record với image_url null hoặc không hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi upload thất bại, không tạo mockup - đảm bảo data consistency và không có orphan records.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 10 - Upload ảnh thất bại
- **Ghi chú**: Liên kết đến API Contract example với INTERNAL_SERVER_ERROR.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Mã lỗi - INTERNAL_SERVER_ERROR
- **Ghi chú**: Liên kết đến mã lỗi khi upload thất bại.
