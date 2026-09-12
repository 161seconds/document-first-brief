# UT-036-04: Từ chối tạo lại khi chưa đăng nhập

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tạo lại khi chưa đăng nhập
- **Ghi chú**: Kịch bản error - user chưa xác thực không được tạo lại thiệp.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (auth check)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User chưa đăng nhập (không có token hoặc token không hợp lệ)
  - Không có thông tin user từ ICurrentUserService
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
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
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: API yêu cầu xác thực người dùng trước khi thực hiện thao tác tạo lại thiệp.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng user chưa đăng nhập không thể tạo lại thiệp.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: API Contract - Error Example 8
- **Ghi chú**: Liên kết đến ví dụ lỗi UNAUTHORIZED.
