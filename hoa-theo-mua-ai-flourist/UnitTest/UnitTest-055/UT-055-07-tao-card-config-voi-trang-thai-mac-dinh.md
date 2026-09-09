# UT-055-07: Tạo Card Config với trạng thái mặc định

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo Card Config với trạng thái mặc định
- **Ghi chú**: Kịch bản happy path - kiểm tra các giá trị mặc định khi tạo config mới.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.CreateConfigAsync (default values)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database trống
  - User ID: "user-uuid-001"
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
      "id": "<generated-uuid>",
      "key": "size_A",
      "value": { ... },
      "group": "card_size",
      "kind": "Setting",
      "is_public": true,
      "is_deleted": false,
      "user_id": "user-uuid-001",
      "created_at": "<current-timestamp>",
      "updated_at": null
    }
  }
  
  Kiểm tra database:
  - kind = "Setting" (luôn luôn, BR-055-03)
  - is_public = true (mặc định, BR-055-09)
  - is_deleted = false (mặc định, BR-055-10)
  - user_id = ID của user đang đăng nhập
  - created_at = timestamp hiện tại
  - updated_at = null (chưa được cập nhật bao giờ)
  
  Lý do: Theo BR-055-09 (IsPublic=true), BR-055-10 (IsDeleted=false).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng các giá trị mặc định được set đúng khi tạo config.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-03
- **Ghi chú**: Kind luôn là "Setting".

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-09
- **Ghi chú**: IsPublic = true khi tạo mới.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-10
- **Ghi chú**: IsDeleted = false khi tạo mới.
