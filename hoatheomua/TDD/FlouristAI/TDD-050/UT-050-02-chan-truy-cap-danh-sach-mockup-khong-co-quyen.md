# UT-050-02: Chặn truy cập danh sách Mockup không có quyền

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn truy cập danh sách Mockup không có quyền
- **Ghi chú**: Kịch bản authorization - user không có quyền Admin không được phép xem danh sách Mockup.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupController.GetMockups (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Customer (không phải Admin)
  - Database có mockups nhưng user không có quyền truy cập
- **Input**:
  ```
  GET /api/mockups
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 403
  Response body:
  {
    "error": {
      "code": "ACCESS_DENIED",
      "message": "Bạn không có quyền thực hiện thao tác này."
    }
  }
  
  Lý do: Theo API Contract TDD-050, chỉ Admin mới có quyền xem danh sách Mockup. User có quyền khác (Customer, Staff) phải bị chặn với HTTP 403.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce đúng - chỉ Admin mới được phép truy cập endpoint lấy danh sách Mockup.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Mã lỗi - ACCESS_DENIED
- **Ghi chú**: Liên kết đến mã lỗi authorization trong TDD.
