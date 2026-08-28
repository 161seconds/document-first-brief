# UT-055-01: Tạo Size Card Config hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo Size Card Config hợp lệ
- **Ghi chú**: Kịch bản happy path - tạo size config với value JSON hợp lệ.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-01
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
  - Database trống (không có config nào tồn tại)
- **Input**:
  ```
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "kind": "Setting",
    "value": {
      "name": "A",
      "width": 10,
      "height": 15,
      "base_price": 100000,
      "max_words": 100
    }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "550e8400-e29b-41d4-a716-446655440001",
      "key": "size_A",
      "value": {
        "name": "A",
        "width": 10,
        "height": 15,
        "base_price": 100000,
        "max_words": 100
      },
      "group": "card_size",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false,
      "created_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - Tạo record mới với đầy đủ fields
  - kind = "Setting" (luôn luôn)
  - is_public = true (mặc định khi tạo)
  - is_deleted = false (mặc định khi tạo)
  
  Lý do: Theo BR-055-02 (sử dụng bảng Config), BR-055-03 (Kind="Setting"), BR-055-07 (value JSON), BR-055-09 (IsPublic=true), BR-055-10 (IsDeleted=false).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng tạo size config hoạt động đúng với value JSON hợp lệ.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-02, BR-055-03, BR-055-07, BR-055-09, BR-055-10
- **Ghi chú**: Liên kết đến Business Rules về tạo config.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 1 - Happy path: Tạo size config
- **Ghi chú**: Liên kết đến API Contract example.
