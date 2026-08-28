# UT-051-07: Từ chối định dạng ảnh không hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối định dạng ảnh không hợp lệ
- **Ghi chú**: Kịch bản error - chỉ chấp nhận PNG và JPG, các định dạng khác bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Request có name = "Mockup Test" (hợp lệ)
  - Request có image với định dạng GIF (không phải PNG/JPG)
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup",
    "image": [file.gif]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Ảnh Mockup phải có định dạng PNG hoặc JPG."
    }
  }
  
  Lý do: Theo BR-051-04, ảnh Mockup bắt buộc có định dạng PNG hoặc JPG. GIF không nằm trong danh sách được chấp nhận.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation định dạng ảnh được enforce - chỉ PNG và JPG được chấp nhận.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-04
- **Ghi chú**: Liên kết đến Business Rule về định dạng ảnh.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 8 - VALIDATION_ERROR: Định dạng ảnh không hợp lệ
- **Ghi chú**: Liên kết đến API Contract example.
