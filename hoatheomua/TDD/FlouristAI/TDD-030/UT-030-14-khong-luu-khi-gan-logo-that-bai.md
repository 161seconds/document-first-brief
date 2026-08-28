# UT-030-14: Không lưu khi gắn logo thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không lưu khi gắn logo thất bại
- **Ghi chú**: Kịch bản error - gắn logo thất bại, không tạo record và không trừ quota.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-14
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (logo attachment failure)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Quota: đã tạo 1/3 lượt trong ngày
  - AI Module trả ảnh hợp lệ
  - Gắn logo thất bại (không thể xử lý ảnh)
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
  - Không tạo generated_flowers record
  - Không tạo client_histories record
  - Quota vẫn là 1/3 (không trừ)
  
  Lý do: Theo BR-030-08, chỉ khi gắn logo thành công mới lưu kết quả. Gắn logo thất bại thì không lưu và không trừ quota.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi gắn logo thất bại, không tạo record và không trừ quota.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-08
- **Ghi chú**: Liên kết đến Business Rule về gắn logo.
