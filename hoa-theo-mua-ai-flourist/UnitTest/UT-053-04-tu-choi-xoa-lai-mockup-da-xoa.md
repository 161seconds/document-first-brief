# UT-053-04: Từ chối xóa lại Mockup đã xóa

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối xóa lại Mockup đã xóa
- **Ghi chú**: Kịch bản error - mockup có is_deleted=true không thể xóa lại.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-053-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật ghi nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.DeleteMockup(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup đã xóa:
    - mockup-deleted: id="550e8400-e29b-41d4-a716-446655440099", is_deleted=true
- **Input**:
  ```
  DELETE /api/mockups/550e8400-e29b-41d4-a716-446655440099
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "BAD_REQUEST",
      "message": "Mockup đã bị xóa trước đó."
    }
  }
  
  Lý do: Theo BR-053-07, Mockup đã bị xóa (is_deleted=true) không thể xóa lại.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-deleted mockups không thể xóa lại.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: BR-053-07
- **Ghi chú**: Liên kết đến Business Rule về không xóa lại.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Ví dụ 3 - Mockup đã bị xóa trước đó
- **Ghi chú**: Liên kết đến API Contract example.
