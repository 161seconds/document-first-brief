# UT-053-03: Từ chối xóa Mockup không tồn tại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối xóa Mockup không tồn tại
- **Ghi chú**: Kịch bản error - mockup không tồn tại trong database.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-053-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.DeleteMockup(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database không có mockup với id="00000000-0000-0000-0000-000000000000"
- **Input**:
  ```
  DELETE /api/mockups/00000000-0000-0000-0000-000000000000
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 404
  Response body:
  {
    "error": {
      "code": "NOT_FOUND",
      "message": "Mockup không tồn tại."
    }
  }
  
  Lý do: Mockup không tồn tại trong database, trả về HTTP 404.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation tồn tại được enforce - mockup không tồn tại trả HTTP 404.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Ví dụ 2 - Mockup không tồn tại
- **Ghi chú**: Liên kết đến API Contract example.
