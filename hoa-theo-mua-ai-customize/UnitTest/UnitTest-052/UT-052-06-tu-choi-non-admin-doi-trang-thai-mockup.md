# UT-052-06: Từ chối non-admin đổi trạng thái Mockup

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối non-admin đổi trạng thái Mockup
- **Ghi chú**: Kịch bản authorization - chỉ Admin mới được phép toggle trạng thái.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-052-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupController.ToggleMockupStatus (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Customer (không phải Admin)
- **Input**:
  ```
  PATCH /api/mockups/550e8400-e29b-41d4-a716-446655440001/status
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
  
  Lý do: Theo API Contract TDD-052, chỉ Admin mới có quyền toggle Mockup. Customer phải bị chặn với HTTP 403.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce - chỉ Admin mới được phép toggle trạng thái Mockup.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Quyền: Admin
- **Ghi chú**: Liên kết đến API Contract về quyền truy cập.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Ví dụ 5 - ACCESS_DENIED
- **Ghi chú**: Liên kết đến API Contract example.
