# UT-062-06: Từ chối cập nhật Config không public

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối cập nhật Config không public
- **Ghi chú**: Kịch bản error - config có IsPublic=false không thể cập nhật.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.UpdateConfigAsync(id, request)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config không public:
    - config-inactive: id="uuid-config", is_public=false, is_deleted=false
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": { "base_price": 120000 }
  }
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
  
  Lý do: Theo BR-062-08, không thể cập nhật config có IsPublic=false (không còn hoạt động).
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng configs không public không thể cập nhật.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-08
- **Ghi chú**: Liên kết đến Business Rule về không cập nhật config không public.
