# UT-055-04: Từ chối value không phải JSON

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối value không phải JSON
- **Ghi chú**: Kịch bản error - value không phải JSON hợp lệ bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.CreateConfigAsync (validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database trống
- **Input**:
  ```
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "kind": "Setting",
    "value": "this is not valid json {"
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
  - Không tạo record nào
  - Database vẫn trống
  
  Lý do: Theo BR-055-08, value phải là JSON hợp lệ khi tạo.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng value không phải JSON bị từ chối với HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-08
- **Ghi chú**: Liên kết đến Business Rule về validation JSON.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 4 - Validation: Invalid JSON
- **Ghi chú**: Liên kết đến API Contract example.
