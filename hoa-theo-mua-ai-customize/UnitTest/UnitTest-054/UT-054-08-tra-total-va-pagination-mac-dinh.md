# UT-054-08: Trả total và pagination mặc định

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Trả total và pagination mặc định
- **Ghi chú**: Kịch bản happy path - pagination với page=1, page_size=20 và trả về total_count.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-08
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.GetCardConfigsAsync (pagination)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có 5 card configs, tất cả có kind="Setting", is_public=true, is_deleted=false
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
      { "id": "uuid-001", "key": "size_A", ... },
      { "id": "uuid-002", "key": "size_B", ... },
      ...
      { "id": "uuid-20", "key": "size_T", ... }
    ],
    "total_count": 5,
    "page": 1,
    "page_size": 20
  }
  
  Kiểm tra:
  - Trả về tất cả 5 configs (vì 5 < 20)
  - total_count = 5 (tổng số bản ghi)
  - page = 1 (mặc định)
  - page_size = 20 (mặc định)
  
  Lý do: Theo BR-054-07 (pagination mặc định), BR-054-08 (trả về tổng số bản ghi).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng pagination hoạt động đúng với giá trị mặc định và trả về total_count.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-07
- **Ghi chú**: Liên kết đến Business Rule về pagination mặc định.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-08
- **Ghi chú**: Liên kết đến Business Rule về total_count.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Ví dụ 4 - Kết hợp filter và pagination
- **Ghi chú**: Liên kết đến API Contract example.
