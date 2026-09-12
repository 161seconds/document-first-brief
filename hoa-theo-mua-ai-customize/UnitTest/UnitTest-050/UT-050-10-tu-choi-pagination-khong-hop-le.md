# UT-050-10: Từ chối pagination không hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối pagination không hợp lệ
- **Ghi chú**: Kịch bản error - các giá trị page_size không nằm trong danh sách được phép sẽ bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-10
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: nullable, page: int, page_size: int)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockups
- **Input**:
  ```
  GET /api/mockups?page_size=15
  (15 không nằm trong danh sách: 5, 10, 20, 30, 40, 50)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Page size không hợp lệ. Giá trị được phép: 5, 10, 20, 30, 40, 50."
    }
  }
  
  Lý do: Theo BR-050-04, chỉ các giá trị page_size: 5, 10, 20, 30, 40, 50 được chấp nhận. Giá trị 15 không hợp lệ và phải trả HTTP 400 với mã lỗi VALIDATION_ERROR.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation page_size được enforce - chỉ chấp nhận các giá trị trong whitelist: 5, 10, 20, 30, 40, 50.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-04
- **Ghi chú**: Liên kết đến Business Rule về các giá trị page_size được phép.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Mã lỗi - VALIDATION_ERROR
- **Ghi chú**: Liên kết đến mã lỗi validation trong TDD.
