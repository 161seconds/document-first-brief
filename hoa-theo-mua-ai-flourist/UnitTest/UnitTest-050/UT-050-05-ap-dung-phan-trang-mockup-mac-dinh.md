# UT-050-05: Áp dụng phân trang Mockup mặc định

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Áp dụng phân trang Mockup mặc định
- **Ghi chú**: Kịch bản boundary - khi không truyền pagination params, sử dụng giá trị mặc định (page=1, page_size=10).

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-05
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
  - Database có 15 mockups (nhiều hơn page_size mặc định là 10)
  - Tất cả mockups có is_deleted=false
- **Input**:
  ```
  GET /api/mockups
  (không truyền page hay page_size)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [ /* 10 mockups đầu tiên */ ],
    "total_count": 15,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Theo BR-050-04, hỗ trợ pagination với page_size: 5, 10, 20, 30, 40, 50. Theo BR-050-05, mặc định page=1 và page_size=10. Khi không truyền params, sử dụng giá trị mặc định.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng pagination mặc định được apply đúng khi không truyền params.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-04, BR-050-05
- **Ghi chú**: Liên kết đến Business Rules về pagination.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 1 - Happy path: Lấy tất cả Mockup
- **Ghi chú**: Liên kết đến API Contract example với pagination mặc định.
