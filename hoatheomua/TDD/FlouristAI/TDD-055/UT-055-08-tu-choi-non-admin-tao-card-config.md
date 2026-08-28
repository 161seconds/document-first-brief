# UT-055-08: Từ chối non-Admin tạo Card Config

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối non-Admin tạo Card Config
- **Ghi chú**: Kịch bản authorization - chỉ Admin mới được phép tạo config.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-08
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigController.CreateConfig (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Staff (không phải Admin)
- **Input**:
  ```
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "kind": "Setting",
    "value": { "name": "A" }
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
  
  Kiểm tra database:
  - Không tạo record nào
  
  Lý do: Theo BR-055-01, chỉ Admin mới có quyền tạo card config.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce - chỉ Admin mới được phép tạo Card Config.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-01
- **Ghi chú**: Liên kết đến Business Rule về quyền Admin.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 8 - Authorization: Non-admin user
- **Ghi chú**: Liên kết đến API Contract example.
