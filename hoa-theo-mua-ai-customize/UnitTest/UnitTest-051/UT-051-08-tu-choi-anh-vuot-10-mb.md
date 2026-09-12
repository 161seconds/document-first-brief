# UT-051-08: Từ chối ảnh vượt 10 MB

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối ảnh vượt 10 MB
- **Ghi chú**: Kịch bản boundary - ảnh có kích thước lớn hơn 10MB phải bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-08
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
  - Request có image với định dạng PNG nhưng kích thước file = 11MB (> 10MB)
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup",
    "image": [file.png - 11MB]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Kích thước ảnh Mockup không được vượt quá 10MB."
    }
  }
  
  Lý do: Theo BR-051-05, kích thước file ảnh tối đa 10MB. 11MB vượt quá giới hạn.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation kích thước ảnh được enforce - tối đa 10MB.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-05
- **Ghi chú**: Liên kết đến Business Rule về kích thước ảnh tối đa.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 9 - VALIDATION_ERROR: Kích thước ảnh quá lớn
- **Ghi chú**: Liên kết đến API Contract example.
