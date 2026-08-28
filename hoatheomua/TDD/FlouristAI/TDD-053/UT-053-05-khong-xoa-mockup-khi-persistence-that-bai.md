# UT-053-05: Không xóa Mockup khi persistence thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không xóa Mockup khi persistence thất bại
- **Ghi chú**: Kịch bản error - khi save thất bại, mockup giữ nguyên is_deleted=false.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-053-05
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.DeleteMockup(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup:
    - mockup-001: is_deleted=false
  - SaveChangesAsync mock throw exception khi update
- **Input**:
  ```
  DELETE /api/mockups/550e8400-e29b-41d4-a716-446655440001
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 500
  Response body:
  {
    "error": {
      "code": "INTERNAL_SERVER_ERROR",
      "message": "Đã xảy ra lỗi. Vui lòng thử lại sau."
    }
  }
  
  Kiểm tra database:
  - mockup.is_deleted vẫn = false
  - mockup record không thay đổi
  
  Lý do: Khi save thất bại, transaction rollback và mockup giữ nguyên trạng thái cũ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi save thất bại, dữ liệu không bị thay đổi (rollback).

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Mã lỗi - INTERNAL_SERVER_ERROR
- **Ghi chú**: Liên kết đến mã lỗi khi persistence thất bại.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-053
- **Section**: Ví dụ 5 - Lỗi hệ thống
- **Ghi chú**: Liên kết đến API Contract example.
