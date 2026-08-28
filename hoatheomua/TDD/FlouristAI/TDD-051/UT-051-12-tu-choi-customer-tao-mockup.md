# UT-051-12: Từ chối Customer tạo Mockup

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối Customer tạo Mockup
- **Ghi chú**: Kịch bản authorization - chỉ Admin mới được phép tạo mockup.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-12
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupController.CreateMockup (authorization)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Customer (không phải Admin)
  - Request có dữ liệu hợp lệ
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup",
    "image": [file upload]
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
  
  Lý do: Theo API Contract TDD-051, chỉ Admin mới có quyền thêm Mockup. Customer phải bị chặn với HTTP 403.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authorization được enforce - chỉ Admin mới được phép tạo mockup.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Quyền: Admin
- **Ghi chú**: Liên kết đến API Contract về quyền truy cập.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 11 - ACCESS_DENIED
- **Ghi chú**: Liên kết đến API Contract example.
