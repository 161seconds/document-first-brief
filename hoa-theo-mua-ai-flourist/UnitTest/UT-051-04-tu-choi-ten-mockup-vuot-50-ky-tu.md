# UT-051-04: Từ chối tên Mockup vượt 50 ký tự

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tên Mockup vượt 50 ký tự
- **Ghi chú**: Kịch bản boundary - tên có hơn 50 ký tự phải bị từ chối.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-04
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
  - Request có name = "Mockup Sinh Nhật 1 cho bó hoa tươi rực rỡ với nhiều màu sắc đẹp mắt" (51 ký tự)
  - Image file valid
- **Input**:
  ```
  POST /api/mockups
  Content-Type: multipart/form-data
  {
    "name": "Mockup Sinh Nhật 1 cho bó hoa tươi rực rỡ với nhiều màu sắc đẹp mắt",
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
      "message": "Tên Mockup không được vượt quá 50 ký tự."
    }
  }
  
  Lý do: Theo BR-051-01, tên Mockup từ 1 đến 50 ký tự. 51 ký tự vượt quá giới hạn.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng validation độ dài tên được enforce - tối đa 50 ký tự.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-01
- **Ghi chú**: Liên kết đến Business Rule về giới hạn độ dài tên.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: Ví dụ 5 - VALIDATION_ERROR: Tên quá 50 ký tự
- **Ghi chú**: Liên kết đến API Contract example.
