# UT-054-01: Lấy Card Config theo group

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lấy Card Config theo group
- **Ghi chú**: Kịch bản happy path - lọc card configs theo groupName.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.GetCardConfigsAsync (filter by group)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có card configs:
    - config-001: id="uuid-001", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false
    - config-002: id="uuid-002", key="size_B", group="card_size", kind="Setting", is_public=true, is_deleted=false
    - config-003: id="uuid-003", key="calligraphy_words", group="card_config", kind="Setting", is_public=true, is_deleted=false
- **Input**:
  ```
  GET /api/v1/configs/content?groupName=card_size
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      {
        "id": "uuid-001",
        "key": "size_A",
        "group": "card_size",
        "kind": "Setting",
        "is_public": true
      },
      {
        "id": "uuid-002",
        "key": "size_B",
        "group": "card_size",
        "kind": "Setting",
        "is_public": true
      }
    ]
  }
  
  Kiểm tra:
  - Chỉ trả về 2 configs có group="card_size"
  - Không trả về config-003 vì group="card_config"
  - Chỉ trả về configs có is_public=true và is_deleted=false
  
  Lý do: Theo BR-054-03 (Kind="Setting"), BR-054-04 (IsDeleted=false), BR-054-05 (IsPublic=true), BR-054-06 (filter theo group).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng filter theo group hoạt động đúng với exact match.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-03, BR-054-04, BR-054-05, BR-054-06
- **Ghi chú**: Liên kết đến Business Rules về filter và điều kiện trả về.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Ví dụ 2 - Filter theo group = card_size
- **Ghi chú**: Liên kết đến API Contract example.
