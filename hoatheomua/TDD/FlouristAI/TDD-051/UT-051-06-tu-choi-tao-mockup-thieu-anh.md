# UT-051-06: Từ chối tạo Mockup thiếu ảnh

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tạo Mockup thiếu ảnh
- **Ghi chú**: Kịch bản error - ảnh mockup là bắt buộc, không được phép thiếu.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-06
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
  - Request không có field "image" trong multipart form
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Ảnh Mockup bắt buộc."
    }
  }
  
  Lý do: Theo BR-051-04, ảnh Mockup bắt buộc. Không có ảnh trong request phải trả HTTP 400.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation ảnh bắt buộc được enforce - không ảnh sẽ bị từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-04
- **Ghi chú**: Liên kết đến Business Rule về ảnh bắt buộc.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 7 - VALIDATION_ERROR: Thiếu ảnh
- **Ghi chú**: Liên kết đến API Contract example.
