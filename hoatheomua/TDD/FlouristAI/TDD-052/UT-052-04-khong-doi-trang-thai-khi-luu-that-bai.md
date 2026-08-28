# UT-052-04: Không đổi trạng thái khi lưu thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không đổi trạng thái khi lưu thất bại
- **Ghi chú**: Kịch bản error - khi save thất bại, trạng thái không thay đổi trong database.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-052-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.ToggleMockupStatus(id)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup:
    - mockup-001: is_active=true, updated_at=null
  - SaveChangesAsync mock throw exception khi update
- **Input**:
  ```
  PATCH /api/mockups/550e8400-e29b-41d4-a716-446655440001/status
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
  - mockup.is_active vẫn = true
  - mockup.updated_at vẫn = null
  - Không có thay đổi được commit
  
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
- **Mã**: TDD-052
- **Section**: Mã lỗi - INTERNAL_SERVER_ERROR
- **Ghi chú**: Liên kết đến mã lỗi khi persistence thất bại.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Ví dụ 6 - Lỗi hệ thống
- **Ghi chú**: Liên kết đến API Contract example.
