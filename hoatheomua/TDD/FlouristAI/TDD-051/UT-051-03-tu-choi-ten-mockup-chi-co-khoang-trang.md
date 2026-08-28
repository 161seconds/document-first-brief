# UT-051-03: Từ chối tên Mockup chỉ có khoảng trắng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tên Mockup chỉ có khoảng trắng
- **Ghi chú**: Kịch bản boundary - tên chỉ gồm whitespace (sau khi trim sẽ rỗng) phải bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request)
- **Loại**: Boundary
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Request có name = "   " (chỉ có khoảng trắng)
  - Image file valid
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "   ",
    "description": "Mô tả mockup",
    "image": [file upload]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Tên Mockup không hợp lệ."
    }
  }
  
  Lý do: Theo BR-051-02, tên Mockup không chỉ chứa khoảng trắng. Sau khi trim, "   " trở thành empty string nên không hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation whitespace-only name được enforce - không chấp nhận tên chỉ gồm khoảng trắng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-02
- **Ghi chú**: Liên kết đến Business Rule về whitespace-only validation.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 4 - VALIDATION_ERROR: Tên chỉ chứa khoảng trắng
- **Ghi chú**: Liên kết đến API Contract example.
