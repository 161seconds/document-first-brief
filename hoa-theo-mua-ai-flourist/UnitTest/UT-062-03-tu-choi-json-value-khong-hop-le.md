# UT-062-03: Từ chối JSON value không hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối JSON value không hợp lệ
- **Ghi chú**: Kịch bản error - value không phải JSON hợp lệ bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.UpdateConfigAsync(id, request) (validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="uuid-config", key="size_A", is_public=true, is_deleted=false
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": "not valid json {"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Value must be a valid JSON."
    }
  }
  
  Kiểm tra database:
  - config.value không thay đổi
  
  Lý do: Theo BR-062-05, value phải là JSON hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng value không phải JSON bị từ chối với HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-05
- **Ghi chú**: Liên kết đến Business Rule về validation JSON.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Ví dụ 3 - Validation: Invalid JSON
- **Ghi chú**: Liên kết đến API Contract example.
