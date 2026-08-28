# UT-050-07: Trả danh sách Mockup rỗng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Trả danh sách Mockup rỗng
- **Ghi chú**: Kịch bản boundary - khi không có mockups hoặc filter không match, trả về mảng rỗng với total_count=0.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: bool?, page: int, page_size: int)
- **Loại**: Boundary
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database không có mockups nào (empty table)
- **Input**:
  ```
  GET /api/mockups
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [],
    "total_count": 0,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Khi không có dữ liệu, vẫn trả về HTTP 200 với mảng rỗng và total_count=0. Không trả HTTP 404 vì đây là danh sách, không phải resource cụ thể.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng response envelope đúng khi không có dữ liệu - HTTP 200 với mảng rỗng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 4 - Happy path: Danh sách rỗng
- **Ghi chú**: Liên kết đến API Contract example với empty result.
