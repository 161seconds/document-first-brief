# UT-054-03: Không trả Card Config không public

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không trả Card Config không public
- **Ghi chú**: Kịch bản branch - configs có is_public=false không được trả về.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.GetCardConfigsAsync (filter is_public)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có card configs:
    - config-001: id="uuid-001", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false
    - config-002: id="uuid-002", key="size_B", group="card_size", kind="Setting", is_public=false, is_deleted=false
    - config-003: id="uuid-003", key="size_C", group="card_size", kind="Setting", is_public=true, is_deleted=false
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
        "is_public": true
      },
      {
        "id": "uuid-003",
        "key": "size_C",
        "is_public": true
      }
    ]
  }
  
  Kiểm tra:
  - config-001 (is_public=true) - TRẢ VỀ
  - config-002 (is_public=false) - KHÔNG TRẢ VỀ
  - config-003 (is_public=true) - TRẢ VỀ
  
  Lý do: Theo BR-054-05, chỉ trả về configs có IsPublic = true (đang hoạt động).
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng configs không public không được trả về.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-05
- **Ghi chú**: Liên kết đến Business Rule về IsPublic.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Notes
- **Ghi chú**: IsPublic xác định config có đang hoạt động để áp dụng cho người dùng chọn.
