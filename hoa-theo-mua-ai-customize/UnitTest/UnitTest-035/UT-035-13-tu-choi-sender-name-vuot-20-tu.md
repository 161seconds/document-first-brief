# UT-035-13: Từ chối sender_name vượt 20 từ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối sender_name vượt 20 từ
- **Ghi chú**: Kịch bản error - số từ trong sender_name vượt quá 20 từ.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-13
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (sender_name validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template và Size config hợp lệ
  - sender_name có hơn 20 từ
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "A",
    "form_type": "go_may",
    "sender_name": "Nguyễn Văn A B C D E F G H I J K L M N O P Q R S T U V W X Y Z",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ",
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
      "message": "Tên người gửi không được vượt quá 20 từ."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo API Contract, sender_name tối đa 20 từ. Vượt quá bị từ chối với lỗi VALIDATION_ERROR.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng sender_name vượt 20 từ được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: API Contract - Error Example 14
- **Ghi chú**: Liên kết đến ví dụ API về lỗi VALIDATION_ERROR khi sender_name quá dài.
