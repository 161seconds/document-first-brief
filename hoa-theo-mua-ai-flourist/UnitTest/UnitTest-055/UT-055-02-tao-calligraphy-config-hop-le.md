# UT-055-02: Tạo Calligraphy Config hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo Calligraphy Config hợp lệ
- **Ghi chú**: Kịch bản happy path - tạo calligraphy words config với value array JSON.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.CreateConfigAsync
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database trống
- **Input**:
  ```
  POST /api/v1/configs
  {
    "group": "card_config",
    "key": "calligraphy_words",
    "kind": "Setting",
    "value": [
      { "min_words": 0, "max_words": 35, "extra_price": 0 },
      { "min_words": 36, "max_words": 70, "extra_price": 39000 },
      { "min_words": 71, "max_words": 100, "extra_price": 69000 }
    ]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "550e8400-e29b-41d4-a716-446655440001",
      "key": "calligraphy_words",
      "value": [
        { "min_words": 0, "max_words": 35, "extra_price": 0 },
        { "min_words": 36, "max_words": 70, "extra_price": 39000 },
        { "min_words": 71, "max_words": 100, "extra_price": 69000 }
      ],
      "group": "card_config",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false
    }
  }
  
  Kiểm tra:
  - Value array được lưu đúng format
  - kind = "Setting" (luôn luôn)
  - is_public = true
  - is_deleted = false
  
  Lý do: Theo BR-055-03 (Kind="Setting"), BR-055-04 (group="card_config"), BR-055-08 (value JSON hợp lệ).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng tạo calligraphy config hoạt động đúng với value array JSON.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-03, BR-055-04, BR-055-08
- **Ghi chú**: Liên kết đến Business Rules về calligraphy config.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 2 - Happy path: Tạo calligraphy words config
- **Ghi chú**: Liên kết đến API Contract example.
