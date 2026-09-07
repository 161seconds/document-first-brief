# UT-050-06: Lọc Mockup inactive

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lọc Mockup inactive
- **Ghi chú**: Kịch bản branch - filter theo is_active=false chỉ trả về mockups inactive.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: bool?, page: int, page_size: int)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có 3 mockups:
    - mockup-001: name="Active Mockup", is_active=true, is_deleted=false, created_at=2026-08-27
    - mockup-002: name="Inactive Mockup 1", is_active=false, is_deleted=false, created_at=2026-08-26
    - mockup-003: name="Inactive Mockup 2", is_active=false, is_deleted=false, created_at=2026-08-25
- **Input**:
  ```
  GET /api/mockups?is_active=false
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      { "id": "mockup-002", "name": "Inactive Mockup 1", "is_active": false },
      { "id": "mockup-003", "name": "Inactive Mockup 2", "is_active": false }
    ],
    "total_count": 2,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Theo BR-050-06, filter theo trạng thái is_active (tùy chọn). Khi is_active=false, chỉ trả về mockups có is_active=false và is_deleted=false.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng filter is_active=false hoạt động đúng, không trả về mockups active.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-06
- **Ghi chú**: Liên kết đến Business Rule về filter is_active.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 2 - Happy path: Filter theo is_active = true
- **Ghi chú**: Liên kết đến API Contract example với filter.
