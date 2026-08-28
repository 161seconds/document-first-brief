# UT-050-04: Sắp xếp Mockup mới nhất trước

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Sắp xếp Mockup mới nhất trước
- **Ghi chú**: Kịch bản deterministic - kết quả phải được sắp xếp theo created_at giảm dần, thứ tự ổn định giữa các lần gọi.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: nullable, page: int, page_size: int)
- **Loại**: Determinism
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có 3 mockups với created_at khác nhau:
    - mockup-old: name="Old Mockup", created_at=2026-08-20T10:00:00Z
    - mockup-middle: name="Middle Mockup", created_at=2026-08-25T10:00:00Z
    - mockup-new: name="New Mockup", created_at=2026-08-28T10:00:00Z
  - Tất cả có is_deleted=false
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
      { "id": "mockup-new", "name": "New Mockup", "created_at": "2026-08-28T10:00:00Z" },      // index 0
      { "id": "mockup-middle", "name": "Middle Mockup", "created_at": "2026-08-25T10:00:00Z" }, // index 1
      { "id": "mockup-old", "name": "Old Mockup", "created_at": "2026-08-20T10:00:00Z" }     // index 2
    ],
    "total_count": 3,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Theo BR-050-03, danh sách được sắp xếp theo created_at giảm dần (mới nhất trước). Khi gọi lại API, thứ tự phải giữ nguyên (deterministic).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng sort theo created_at desc được apply đúng, và kết quả ổn định giữa các lần gọi.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-03
- **Ghi chú**: Liên kết đến Business Rule về sort order.
