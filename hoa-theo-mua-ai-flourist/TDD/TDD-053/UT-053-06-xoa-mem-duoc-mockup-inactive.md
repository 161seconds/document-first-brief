# UT-053-06: Xóa mềm được Mockup inactive

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Xóa mềm được Mockup inactive
- **Ghi chú**: Kịch bản happy path - mockup inactive vẫn có thể xóa mềm được.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-053-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.DeleteMockup(id)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup inactive:
    - mockup-001: id="550e8400-e29b-41d4-a716-446655440001", name="Mockup Cũ", is_active=false, is_deleted=false
- **Input**:
  ```
  DELETE /api/mockups/550e8400-e29b-41d4-a716-446655440001
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "550e8400-e29b-41d4-a716-446655440001",
      "name": "Mockup Cũ",
      "is_deleted": true
    }
  }
  
  Kiểm tra database:
  - mockup.is_deleted = true
  - mockup.is_active vẫn = false (không thay đổi)
  
  Lý do: Theo BR-053-06, Mockup đã xóa có thể ở trạng thái Active hoặc Inactive. Không có ràng buộc về is_active khi xóa.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng cả mockup active và inactive đều có thể xóa mềm được.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: BR-053-06
- **Ghi chú**: Liên kết đến Business Rule về trạng thái khi xóa.
