# UT-054-02: Chặn xem Card Config không có quyền

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn xem Card Config không có quyền
- **Ghi chú**: Kịch bản authorization - chỉ Admin và Staff mới được phép xem card configs.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigController.GetCardConfigs (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Customer (không phải Admin hoặc Staff)
- **Input**:
  ```
  GET /api/v1/configs/content
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 403
  Response body:
  {
    "error": {
      "code": "ACCESS_DENIED",
      "message": "Bạn không có quyền xem danh sách cấu hình card."
    }
  }
  
  Lý do: Theo BR-054-01, chỉ Admin và Staff mới có quyền xem. Customer phải bị chặn với HTTP 403.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce - chỉ Admin và Staff mới được phép xem Card Configs.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: BR-054-01
- **Ghi chú**: Liên kết đến Business Rule về quyền truy cập.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Mã lỗi - ACCESS_DENIED
- **Ghi chú**: Liên kết đến mã lỗi authorization.
