# UT-056-01: Xóa Card Config thành công

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Xóa Card Config thành công
- **Ghi chú**: Kịch bản happy path - soft-delete config với IsDeleted=true.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.DeleteConfigAsync(id)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="550e8400-e29b-41d4-a716-446655440001", key="size_A", is_deleted=false
- **Input**:
  ```
  DELETE /api/v1/configs/550e8400-e29b-41d4-a716-446655440001
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "success": true,
      "message": "Config deleted successfully."
    }
  }
  
  Kiểm tra database:
  - config.is_deleted = true
  - config.updated_at = <current timestamp>
  - config record vẫn tồn tại (không xóa vật lý)
  
  Lý do: Theo BR-056-03 (xóa mềm với IsDeleted=true), BR-056-04 (cập nhật UpdatedAt).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-delete hoạt động đúng - config có IsDeleted=true nhưng vẫn tồn tại trong database.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: BR-056-03, BR-056-04
- **Ghi chú**: Liên kết đến Business Rules về soft-delete.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Ví dụ 1 - Happy path: Xóa thành công
- **Ghi chú**: Liên kết đến API Contract example.
