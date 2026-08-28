# UT-062-09: Không thay đổi field bất biến khi cập nhật

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không thay đổi field bất biến khi cập nhật
- **Ghi chú**: Kịch bản happy path - kiểm tra Key và Group không thay đổi khi cập nhật.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-09
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.UpdateConfigAsync(id, request) (immutable fields)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="uuid-config", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false, value={base_price: 100000}
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": { "base_price": 120000 }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "uuid-config",
      "key": "size_A",
      "group": "card_size",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false,
      "value": { "base_price": 120000 },
      "updated_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - config.key = "size_A" (KHÔNG THAY ĐỔI)
  - config.group = "card_size" (KHÔNG THAY ĐỔI)
  - config.kind = "Setting" (KHÔNG THAY ĐỔI)
  - config.is_public = true (KHÔNG THAY ĐỔI)
  - config.is_deleted = false (KHÔNG THAY ĐỔI)
  - config.value = {base_price: 120000} (ĐÃ THAY ĐỔI)
  - config.updated_at = <current timestamp> (ĐÃ THAY ĐỔI)
  
  Lý do: Theo BR-062-03, Key và Group KHÔNG được phép thay đổi khi cập nhật.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng Key và Group không thay đổi khi cập nhật value.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-03
- **Ghi chú**: Liên kết đến Business Rule về Key và Group không đổi.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Notes
- **Ghi chú**: Key và Group không được truyền trong request - không thể thay đổi.
