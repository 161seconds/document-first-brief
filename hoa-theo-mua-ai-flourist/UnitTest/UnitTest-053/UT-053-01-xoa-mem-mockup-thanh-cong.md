# UT-053-01: Xóa mềm Mockup thành công

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Xóa mềm Mockup thành công
- **Ghi chú**: Kịch bản happy path - soft-delete mockup với is_deleted=true.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-053-01
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
  - Database có mockup:
    - mockup-001: id="550e8400-e29b-41d4-a716-446655440001", name="Mockup Sinh Nhật 1", is_active=true, is_deleted=false
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
      "name": "Mockup Sinh Nhật 1",
      "is_deleted": true,
      "deleted_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - mockup.is_deleted = true
  - mockup.deleted_at = <current timestamp>
  - mockup record vẫn tồn tại (không xóa vật lý)
  
  Lý do: Theo BR-053-01, sử dụng xóa mềm với is_deleted=true. Theo BR-053-02 và BR-053-03, không xóa vật lý dữ liệu hay ảnh.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-delete hoạt động đúng - mockup có is_deleted=true nhưng vẫn tồn tại trong database.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: BR-053-01, BR-053-02, BR-053-03
- **Ghi chú**: Liên kết đến Business Rules về soft-delete.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Ví dụ 1 - Happy path: Xóa Mockup thành công
- **Ghi chú**: Liên kết đến API Contract example.
