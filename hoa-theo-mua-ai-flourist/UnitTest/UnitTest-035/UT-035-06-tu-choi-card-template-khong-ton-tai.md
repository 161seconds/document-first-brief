# UT-035-06: Từ chối card template không tồn tại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối card template không tồn tại
- **Ghi chú**: Kịch bản error - card_template_id không tồn tại hoặc đã bị xóa.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (card template validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template với id "00000000-0000-0000-0000-000000000000" không tồn tại
  - Size config và Product hợp lệ
  - Quota: 0/10 lượt
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "00000000-0000-0000-0000-000000000000",
    "size": "A",
    "form_type": "go_may",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ",
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 404
  Response body:
  {
    "error": {
      "code": "NOT_FOUND",
      "message": "Card template không tồn tại."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi (không gọi AI)
  
  Lý do: Hệ thống kiểm tra card template trước khi gọi AI. Template không tồn tại trả lỗi 404.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng card template không tồn tại được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: API Contract - Error Example 7
- **Ghi chú**: Liên kết đến ví dụ API về lỗi NOT_FOUND card template.
