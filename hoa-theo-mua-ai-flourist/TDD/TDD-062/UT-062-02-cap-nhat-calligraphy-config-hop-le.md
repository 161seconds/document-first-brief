# UT-062-02: Cập nhật Calligraphy Config hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cập nhật Calligraphy Config hợp lệ
- **Ghi chú**: Kịch bản happy path - cập nhật phụ phí calligraphy words.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-02
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
    - config-002: id="uuid-config", key="calligraphy_words", group="card_config", kind="Setting", is_public=true, is_deleted=false
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": [
      { "min_words": 0, "max_words": 35, "extra_price": 10000 },
      { "min_words": 36, "max_words": 70, "extra_price": 49000 },
      { "min_words": 71, "max_words": 100, "extra_price": 79000 }
    ]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "uuid-config",
      "key": "calligraphy_words",
      "value": [
        { "min_words": 0, "max_words": 35, "extra_price": 10000 },
        { "min_words": 36, "max_words": 70, "extra_price": 49000 },
        { "min_words": 71, "max_words": 100, "extra_price": 79000 }
      ],
      "group": "card_config",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false,
      "updated_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - config.value đã được cập nhật với extra_price mới
  - config.key và config.group không thay đổi
  
  Lý do: Theo BR-062-03 (Key và Group không đổi), BR-062-04 (chỉ cập nhật value).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng cập nhật calligraphy config hoạt động đúng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-03, BR-062-04
- **Ghi chú**: Liên kết đến Business Rules về cập nhật config.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Ví dụ 2 - Happy path: Cập nhật phụ phí calligraphy
- **Ghi chú**: Liên kết đến API Contract example.
