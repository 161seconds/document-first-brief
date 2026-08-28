# UT-062-10: Không thay đổi Config khi lưu thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không thay đổi Config khi lưu thất bại
- **Ghi chú**: Kịch bản error - khi save thất bại, config giữ nguyên value cũ.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-062-10
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.UpdateConfigAsync(id, request)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có config:
    - config-001: id="uuid-config", value={base_price: 100000}
  - SaveChangesAsync mock throw exception khi update
- **Input**:
  ```
  PUT /api/v1/configs/uuid-config
  {
    "value": { "base_price": 120000 }
  }
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
  - config.value vẫn = {base_price: 100000}
  - config.updated_at không thay đổi
  
  Lý do: Khi save thất bại, transaction rollback và config giữ nguyên trạng thái cũ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi save thất bại, dữ liệu không bị thay đổi (rollback).

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-062
- **Section**: Mã lỗi - INTERNAL_SERVER_ERROR
- **Ghi chú**: Liên kết đến mã lỗi khi persistence thất bại.
