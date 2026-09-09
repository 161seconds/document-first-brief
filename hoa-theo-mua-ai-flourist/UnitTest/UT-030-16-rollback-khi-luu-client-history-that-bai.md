# UT-030-16: Rollback khi lưu client_history thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Rollback khi lưu client_history thất bại
- **Ghi chú**: Kịch bản error - khi lưu client_histories thất bại, generated_flowers cũng rollback.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-16
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (transaction rollback)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Quota: đã tạo 1/3 lượt trong ngày
  - AI Module trả ảnh hợp lệ
  - Gắn logo thành công
  - Lưu generated_flowers thành công
  - Lưu client_histories THẤT BẠI (exception)
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "combo-uuid",
    "mockup_id": "mockup-uuid",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 500
  Response body:
  {
    "error": {
      "code": "INTERNAL_SERVER_ERROR",
      "message": "An unexpected error occurred."
    }
  }
  
  Kiểm tra database:
  - generated_flowers record đã tạo bị rollback (không tồn tại)
  - client_histories record không tạo
  - Quota vẫn là 1/3 (không trừ)
  
  Lý do: Khi lưu client_histories thất bại, transaction rollback và không trừ quota.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi lưu thất bại, toàn bộ transaction được rollback.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-09
- **Ghi chú**: Liên kết đến Business Rule về tạo cùng flow.
