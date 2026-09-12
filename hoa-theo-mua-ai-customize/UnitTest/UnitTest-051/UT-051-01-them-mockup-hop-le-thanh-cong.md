# UT-051-01: Thêm Mockup hợp lệ thành công

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Thêm Mockup hợp lệ thành công
- **Ghi chú**: Kịch bản happy path - tạo Mockup mới với dữ liệu hợp lệ.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database trống (không có mockup nào)
  - File upload service mock trả về URL thành công: "https://storage.example.com/mockups/test.jpg"
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Sinh Nhật 1",
    "description": "Mockup bó hoa sinh nhật với màu sắc tươi sáng",
    "image": [file upload - test.jpg]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "<generated-uuid>",
      "name": "Mockup Sinh Nhật 1",
      "description": "Mockup bó hoa sinh nhật với màu sắc tươi sáng",
      "image_url": "https://storage.example.com/mockups/test.jpg",
      "is_active": true,
      "is_deleted": false,
      "created_at": "<current-timestamp>",
      "updated_at": null
    }
  }
  
  Lý do: Theo BR-051-06, mockup mới được tạo với is_active=true. Theo BR-051-07, is_deleted=false. Mockup được lưu với thông tin đầy đủ.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng mockup mới được tạo thành công với các trường bắt buộc và trạng thái mặc định đúng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-06, BR-051-07
- **Ghi chú**: Liên kết đến Business Rules về trạng thái mặc định.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 1 - Happy path: Thêm Mockup thành công
- **Ghi chú**: Liên kết đến API Contract example.
