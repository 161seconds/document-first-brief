# UT-056-05: Từ chối xóa Card Config đã xóa

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối xóa Card Config đã xóa
- **Ghi chú**: Kịch bản error - config có IsDeleted=true không thể xóa lại.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-05
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
  - Database có config đã xóa:
    - config-deleted: id="uuid-config", is_deleted=true
- **Input**:
  ```
  DELETE /api/v1/configs/uuid-config
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
  
  Lý do: Theo BR-056-07, không thể xóa config đã bị xóa trước đó (IsDeleted=true). Config đã xóa không được tìm thấy.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng soft-deleted configs không thể xóa lại.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: BR-056-07
- **Ghi chú**: Liên kết đến Business Rule về không xóa lại.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Ví dụ 3 - Not found: Đã bị xóa trước đó
- **Ghi chú**: Liên kết đến API Contract example.
