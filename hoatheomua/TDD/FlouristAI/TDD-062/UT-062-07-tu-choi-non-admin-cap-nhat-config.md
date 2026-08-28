# UT-062-07: Từ chối non-Admin cập nhật Config

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối non-Admin cập nhật Config
- **Ghi chú**: Kịch bản authorization - chỉ Admin mới được phép cập nhật config.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigController.UpdateConfig (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Staff (không phải Admin)
- **Input**:
  ```
  PUT /api/v1/configs/550e8400-e29b-41d4-a716-446655440001
  {
    "value": { "base_price": 120000 }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 403
  Response body:
  {
    "error": {
      "code": "ACCESS_DENIED",
      "message": "Bạn không có quyền thực hiện thao tác này."
    }
  }
  
  Lý do: Theo BR-062-01, chỉ Admin mới có quyền cập nhật card config. Staff phải bị chặn với HTTP 403.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce - chỉ Admin mới được phép cập nhật Card Config.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: BR-062-01
- **Ghi chú**: Liên kết đến Business Rule về quyền Admin.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Ví dụ 5 - Authorization: Non-admin user
- **Ghi chú**: Liên kết đến API Contract example.
