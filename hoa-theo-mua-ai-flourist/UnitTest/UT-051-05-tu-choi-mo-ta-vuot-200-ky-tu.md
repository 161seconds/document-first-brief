# UT-051-05: Từ chối mô tả vượt 200 ký tự

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối mô tả vượt 200 ký tự
- **Ghi chú**: Kịch bản boundary - mô tả có hơn 200 ký tự phải bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-05
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
  - Request có name = "Mockup Test" (hợp lệ)
  - Request có description = "Mô tả..." (201 ký tự)
  - Image file valid
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "<201 ký tự>",
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
      "message": "Mô tả Mockup không được vượt quá 200 ký tự."
    }
  }
  
  Lý do: Theo BR-051-03, mô tả tùy chọn, tối đa 200 ký tự. 201 ký tự vượt quá giới hạn.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation độ dài mô tả được enforce - tối đa 200 ký tự.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-03
- **Ghi chú**: Liên kết đến Business Rule về giới hạn độ dài mô tả.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 6 - VALIDATION_ERROR: Mô tả quá 200 ký tự
- **Ghi chú**: Liên kết đến API Contract example.
