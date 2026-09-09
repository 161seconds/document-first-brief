# UT-054-04: Admin xem được Card Config

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Admin xem được Card Config
- **Ghi chú**: Kịch bản happy path - Admin có quyền xem card configs.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigController.GetCardConfigs (Admin authorization)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có card configs:
    - config-001: id="uuid-001", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false
- **Input**:
  ```
  GET /api/v1/configs/content
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
        "is_public": true,
        "is_deleted": false
      }
    ]
  }
  
  Lý do: Theo BR-054-01, Admin có quyền xem danh sách card configs.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng Admin có quyền xem Card Configs.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-01
- **Ghi chú**: Liên kết đến Business Rule về quyền Admin.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: API Contract - Quyền: Admin, staff
- **Ghi chú**: Liên kết đến API Contract.
