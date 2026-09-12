# UT-052-02: Từ chối đổi trạng thái Mockup không tồn tại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối đổi trạng thái Mockup không tồn tại
- **Ghi chú**: Kịch bản error - mockup không tồn tại trong database.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-052-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.ToggleMockupStatus(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database không có mockup với id="00000000-0000-0000-0000-000000000000"
- **Input**:
  ```
  PATCH /api/mockups/00000000-0000-0000-0000-000000000000/status
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
  
  Lý do: Theo BR-052-04, Mockup không tồn tại trả về lỗi 404.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation tồn tại được enforce - mockup không tồn tại trả HTTP 404.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: BR-052-04
- **Ghi chú**: Liên kết đến Business Rule về not found.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Ví dụ 3 - Mockup không tồn tại
- **Ghi chú**: Liên kết đến API Contract example.
