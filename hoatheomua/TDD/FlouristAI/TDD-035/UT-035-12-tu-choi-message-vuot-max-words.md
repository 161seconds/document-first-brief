# UT-035-12: Từ chối message vượt max_words

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối message vượt max_words
- **Ghi chú**: Kịch bản error - số từ trong message_content vượt quá max_words của size.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-12
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (message validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template tồn tại
  - Size config "size_A" có max_words = 100
  - Message content có hơn 100 từ
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "A",
    "form_type": "go_may",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ với rất nhiều lời chúc tốt đẹp và ý nghĩa cho một ngày đặc biệt trong năm mà chúng ta cùng nhau đón mừng và chia sẻ niềm vui với gia đình và bạn bè thân yêu của mình trong suốt quãng đời dài phía trước",
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Số từ trong lời chúc vượt quá giới hạn cho phép."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo BR-035-01, size config chứa max_words. Message có số từ vượt quá max_words bị từ chối với lỗi VALIDATION_ERROR.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng message vượt max_words được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-01, API Contract - Error Example 13
- **Ghi chú**: Liên kết đến Business Rule về max_words và ví dụ lỗi VALIDATION_ERROR.
