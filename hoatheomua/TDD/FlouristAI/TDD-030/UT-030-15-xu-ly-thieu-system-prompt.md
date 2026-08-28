# UT-030-15: Xử lý thiếu system prompt

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Xử lý thiếu system prompt
- **Ghi chú**: Kịch bản error - không tìm thấy system prompt type=flower.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-15
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (system prompt validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Quota: đã tạo 0/3 lượt trong ngày
  - Không có system prompt với type="flower" trong database
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
  - Quota vẫn là 0/3
  
  Lý do: Khi thiếu system prompt, không thể ghép prompt đầy đủ cho AI. Hệ thống nên trả lỗi 500.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi thiếu system prompt, không tạo record và không trừ quota.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Notes
- **Ghi chú**: AI Module nhận system_prompts.content với type="flower".
