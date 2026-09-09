# UT-051-11: Tạo Mockup với trạng thái mặc định đúng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo Mockup với trạng thái mặc định đúng
- **Ghi chú**: Kịch bản happy path - mockup mới phải có is_active=true và is_deleted=false.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-11
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật ghi nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Request có dữ liệu hợp lệ
  - File upload thành công
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
  HTTP 201
  Response body:
  {
    "value": {
      ...
      "is_active": true,
      "is_deleted": false,
      ...
    }
  }
  
  Kiểm tra database:
  - mockup.is_active = true
  - mockup.is_deleted = false
  - mockup.created_at = <current timestamp>
  - mockup.updated_at = null
  
  Lý do: Theo BR-051-06, mockup mới có is_active=true. Theo BR-051-07, is_deleted=false.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng mockup mới được tạo với trạng thái mặc định đúng - is_active=true và is_deleted=false.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-06, BR-051-07
- **Ghi chú**: Liên kết đến Business Rules về trạng thái mặc định.
