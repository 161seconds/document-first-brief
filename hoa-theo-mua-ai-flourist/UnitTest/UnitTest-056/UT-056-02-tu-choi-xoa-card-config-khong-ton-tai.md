# UT-056-02: Từ chối xóa Card Config không tồn tại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối xóa Card Config không tồn tại
- **Ghi chú**: Kịch bản error - config không tồn tại trong database.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.DeleteConfigAsync(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database không có config với id="00000000-0000-0000-0000-000000000000"
- **Input**:
  ```
  DELETE /api/v1/configs/00000000-0000-0000-0000-000000000000
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 404
  Response body:
  {
    "error": {
      "code": "NOT_FOUND",
      "message": "Config not found."
    }
  }
  
  Lý do: Config không tồn tại trong database, trả về HTTP 404.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation tồn tại được enforce - config không tồn tại trả HTTP 404.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Ví dụ 2 - Not found: ID không tồn tại
- **Ghi chú**: Liên kết đến API Contract example.
