# UT-052-03: Từ chối đổi trạng thái Mockup đã xóa

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối đổi trạng thái Mockup đã xóa
- **Ghi chú**: Kịch bản error - mockup có is_deleted=true không thể đổi trạng thái.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-052-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.ToggleMockupStatus(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup đã xóa:
    - mockup-deleted: id="550e8400-e29b-41d4-a716-446655440099", is_deleted=true
- **Input**:
  ```
  PATCH /api/mockups/550e8400-e29b-41d4-a716-446655440099/status
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "BAD_REQUEST",
      "message": "Mockup không hợp lệ hoặc đã bị xóa."
    }
  }
  
  Lý do: Theo BR-052-03, Mockup đã bị xóa (is_deleted=true) không thể chuyển trạng thái.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-deleted mockups không thể toggle trạng thái.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: BR-052-03
- **Ghi chú**: Liên kết đến Business Rule về soft-delete.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Ví dụ 4 - Mockup đã bị xóa
- **Ghi chú**: Liên kết đến API Contract example.
