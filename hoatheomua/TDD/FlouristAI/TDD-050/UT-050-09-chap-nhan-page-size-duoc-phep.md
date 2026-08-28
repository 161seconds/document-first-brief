# UT-050-09: Chấp nhận page_size được phép

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chấp nhận page_size được phép
- **Ghi chú**: Kịch bản boundary - các giá trị page_size được phép: 5, 10, 20, 30, 40, 50.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-09
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
  - Database có 50 mockups (nhiều hơn tất cả page_size options)
  - Tất cả mockups có is_deleted=false
- **Input**:
  ```
  GET /api/mockups?page_size=20
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [ /* 20 mockups đầu tiên */ ],
    "total_count": 50,
    "page": 1,
    "page_size": 20
  }
  
  Lý do: Theo BR-050-04, các giá trị page_size được phép là: 5, 10, 20, 30, 40, 50. Giá trị 20 nằm trong danh sách được phép nên được chấp nhận.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng page_size=20 (nằm trong danh sách được phép) được chấp nhận và áp dụng đúng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-04
- **Ghi chú**: Liên kết đến Business Rule về các giá trị page_size được phép.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 3 - Happy path: Kết hợp filter và pagination
- **Ghi chú**: Liên kết đến API Contract example với page_size=5.
