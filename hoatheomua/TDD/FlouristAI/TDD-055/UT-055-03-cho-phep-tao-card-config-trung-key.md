# UT-055-03: Cho phép tạo Card Config trùng key

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cho phép tạo Card Config trùng key
- **Ghi chú**: Kịch bản happy path - không check trùng key, cho phép tạo nhiều config cùng key.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.CreateConfigAsync (idempotency)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database đã có config:
    - config-existing: id="uuid-existing", key="size_A", group="card_size"
- **Input**:
  ```
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "kind": "Setting",
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
  HTTP 201
  Response body:
  {
    "value": {
      "id": "550e8400-e29b-41d4-a716-446655440002",
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
      "is_deleted": false
    }
  }
  
  Kiểm tra database:
  - config-existing vẫn tồn tại (key="size_A" cũ)
  - Tạo record mới với key="size_A" (mới)
  - Có 2 records với key="size_A"
  
  Lý do: Theo BR-055-06, không check trùng key - cho phép tạo nhiều config cùng key.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng không check trùng key, cho phép tạo nhiều config cùng key.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-06
- **Ghi chú**: Liên kết đến Business Rule về không check trùng key.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 3 - Happy path: Tạo trùng key
- **Ghi chú**: Liên kết đến API Contract example.
