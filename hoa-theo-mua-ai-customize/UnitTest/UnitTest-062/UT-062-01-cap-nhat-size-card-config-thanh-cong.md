# UT-062-01: Cập nhật Size Card Config thành công

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cập nhật Size Card Config thành công
- **Ghi chú**: Kịch bản happy path - cập nhật value của size config.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.UpdateConfigAsync(id, request)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="uuid-config", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false, value={base_price: 100000}
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": {
      "name": "A",
      "width": 12,
      "height": 18,
      "base_price": 120000,
      "max_words": 120
    }
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
      "value": {
        "name": "A",
        "width": 12,
        "height": 18,
        "base_price": 120000,
        "max_words": 120
      },
      "group": "card_size",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false,
      "updated_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - config.value đã được cập nhật với base_price mới (120000)
  - config.key và config.group không thay đổi
  - config.updated_at = <current timestamp>
  
  Lý do: Theo BR-062-03 (Key và Group không đổi), BR-062-04 (chỉ cập nhật value), BR-062-06 (UpdatedAt được cập nhật).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng cập nhật size config hoạt động đúng, chỉ thay đổi value và UpdatedAt.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-03, BR-062-04, BR-062-06
- **Ghi chú**: Liên kết đến Business Rules về cập nhật config.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Ví dụ 1 - Happy path: Cập nhật giá size_A
- **Ghi chú**: Liên kết đến API Contract example.
