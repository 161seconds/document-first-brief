# UT-050-08: Lấy đúng Mockup ở trang hai

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lấy đúng Mockup ở trang hai
- **Ghi chú**: Kịch bản boundary - pagination hoạt động đúng với page > 1.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-08
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
  - Database có 5 mockups:
    - mockup-001: name="Mockup 1", created_at=2026-08-28 (index 0)
    - mockup-002: name="Mockup 2", created_at=2026-08-27 (index 1)
    - mockup-003: name="Mockup 3", created_at=2026-08-26 (index 2)
    - mockup-004: name="Mockup 4", created_at=2026-08-25 (index 3)
    - mockup-005: name="Mockup 5", created_at=2026-08-24 (index 4)
  - Tất cả có is_deleted=false
- **Input**:
  ```
  GET /api/mockups?page=2&page_size=2
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      { "id": "mockup-003", "name": "Mockup 3", "created_at": "2026-08-26T..." }, // index 0 của page 2
      { "id": "mockup-004", "name": "Mockup 4", "created_at": "2026-08-25T..." }  // index 1 của page 2
    ],
    "total_count": 5,
    "page": 2,
    "page_size": 2
  }
  
  Lý do: Page 2 với page_size=2 sẽ skip (2-1)*2 = 2 items đầu tiên và lấy 2 items tiếp theo. mockup-001 và mockup-002 ở trang 1, mockup-003 và mockup-004 ở trang 2, mockup-005 ở trang 3.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng pagination hoạt động đúng - skip và take đúng số lượng, sort order được giữ nguyên qua các trang.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-04
- **Ghi chú**: Liên kết đến Business Rule về pagination.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 5 - Happy path: Phân trang với page 2
- **Ghi chú**: Liên kết đến API Contract example với pagination page 2.
