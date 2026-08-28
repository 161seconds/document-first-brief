# UT-056-04: Cập nhật UpdatedAt khi xóa Config

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cập nhật UpdatedAt khi xóa Config
- **Ghi chú**: Kịch bản happy path - kiểm tra UpdatedAt được cập nhật khi soft-delete.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.DeleteConfigAsync(id) (UpdatedAt tracking)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="uuid-config", updated_at="2026-08-01T10:00:00Z", is_deleted=false
- **Input**:
  ```
  DELETE /api/v1/configs/uuid-config
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
  - config.updated_at = <current timestamp> (KHÁC với giá trị cũ 2026-08-01T10:00:00Z)
  
  Lý do: Theo BR-056-04, UpdatedAt được cập nhật khi xóa để track thời điểm xóa.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng UpdatedAt được cập nhật khi soft-delete config.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: BR-056-04
- **Ghi chú**: Liên kết đến Business Rule về UpdatedAt.
