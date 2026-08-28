# UT-051-02: Từ chối tên Mockup rỗng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tên Mockup rỗng
- **Ghi chú**: Kịch bản error - tên mockup rỗng (empty string) phải bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-02
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
  - Request có name = "" (empty string)
  - Image file valid
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "",
    "description": "Mô tả mockup",
    "image": [file upload]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Tên Mockup bắt buộc, từ 1 đến 50 ký tự."
    }
  }
  
  Lý do: Theo BR-051-01, tên Mockup bắt buộc. Empty string không hợp lệ và phải trả HTTP 400.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation tên rỗng được enforce - empty string không được chấp nhận.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-01
- **Ghi chú**: Liên kết đến Business Rule về tên bắt buộc.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 3 - VALIDATION_ERROR
- **Ghi chú**: Liên kết đến API Contract example với validation error.
