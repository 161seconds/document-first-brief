# UT-050-03: Không trả Mockup đã xóa mềm

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không trả Mockup đã xóa mềm
- **Ghi chú**: Kịch bản boundary - mockups có is_deleted=true không được trả về trong danh sách.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: nullable, page: int, page_size: int)
- **Loại**: Boundary
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có 4 mockups:
    - mockup-001: name="Active Mockup 1", is_active=true, is_deleted=false
    - mockup-002: name="Active Mockup 2", is_active=true, is_deleted=false
    - mockup-003: name="Deleted Active Mockup", is_active=true, is_deleted=true (đã xóa mềm)
    - mockup-004: name="Inactive Mockup", is_active=false, is_deleted=true (đã xóa mềm)
- **Input**:
  ```
  GET /api/mockups
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      { "id": "mockup-001", "name": "Active Mockup 1", "is_deleted": false },
      { "id": "mockup-002", "name": "Active Mockup 2", "is_deleted": false }
    ],
    "total_count": 2,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Theo BR-050-01, chỉ mockups có is_deleted=false mới được trả về. mockup-003 và mockup-004 có is_deleted=true nên không xuất hiện trong kết quả.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-delete được enforce đúng - mockups đã xóa không xuất hiện trong danh sách dù có is_active=true hay false.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-01
- **Ghi chú**: Liên kết đến Business Rule về soft-delete.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: BR-053-01, BR-053-04
- **Ghi chú**: Liên kết đến TDD xóa Mockup về soft-delete và không hiển thị mockups đã xóa.
