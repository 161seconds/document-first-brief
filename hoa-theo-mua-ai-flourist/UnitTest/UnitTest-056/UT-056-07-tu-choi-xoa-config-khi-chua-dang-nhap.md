# UT-056-07: Từ chối xóa Config khi chưa đăng nhập

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối xóa Config khi chưa đăng nhập
- **Ghi chú**: Kịch bản authentication - user chưa đăng nhập không được phép xóa config.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigController.DeleteConfig (authentication)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User chưa đăng nhập (không có token hoặc token không hợp lệ)
- **Input**:
  ```
  DELETE /api/v1/configs/550e8400-e29b-41d4-a716-446655440001
  (không có Authorization header)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 401
  Response body:
  {
    "error": {
      "code": "UNAUTHORIZED",
      "message": "Bạn cần đăng nhập để thực hiện thao tác này."
    }
  }
  
  Lý do: User chưa đăng nhập phải nhận HTTP 401 trước khi kiểm tra authorization.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authentication được enforce - user chưa đăng nhập bị từ chối với HTTP 401.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Mã lỗi - UNAUTHORIZED
- **Ghi chú**: Liên kết đến mã lỗi authentication.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Ví dụ 5 - Chưa đăng nhập
- **Ghi chú**: Liên kết đến API Contract example.
