# UT-054-06: Lọc Card Config theo key

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lọc Card Config theo key
- **Ghi chú**: Kịch bản branch - lọc card configs theo key với exact match.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.GetCardConfigsAsync (filter by key)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có card configs:
    - config-001: id="uuid-001", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false
    - config-002: id="uuid-002", key="size_B", group="card_size", kind="Setting", is_public=true, is_deleted=false
    - config-003: id="uuid-003", key="calligraphy_words", group="card_config", kind="Setting", is_public=true, is_deleted=false
- **Input**:
  ```
  GET /api/v1/configs/content?key=calligraphy_words
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      {
        "id": "uuid-003",
        "key": "calligraphy_words",
        "group": "card_config",
        "kind": "Setting",
        "is_public": true,
        "is_deleted": false,
        "created_at": "2026-08-26T10:00:00Z"
      }
    ]
  }
  
  Kiểm tra:
  - Chỉ trả về 1 config có key="calligraphy_words"
  - config-001 và config-002 không được trả về vì key khác
  
  Lý do: Theo BR-054-06, filter theo key với exact match.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng filter theo key hoạt động đúng với exact match.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-06
- **Ghi chú**: Liên kết đến Business Rule về filter theo key.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Ví dụ 3 - Filter theo key
- **Ghi chú**: Liên kết đến API Contract example.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Notes
- **Ghi chú**: Filter theo group, key sử dụng exact match.
