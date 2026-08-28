# UT-035-19: Từ chối generated flower không thuộc user

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối generated flower không thuộc user
- **Ghi chú**: Kịch bản error - generated_flower_id thuộc về user khác.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-19
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (generated_flower ownership validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User A đã đăng nhập
  - Generated flower với id "880e8400-e29b-41d4-a716-446655440005" thuộc về User B (không phải User A)
  - Card template và Size config hợp lệ
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "A",
    "form_type": "go_may",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ",
    "generated_flower_id": "880e8400-e29b-41d4-a716-446655440005"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 404
  Response body:
  {
    "error": {
      "code": "NOT_FOUND",
      "message": "Generated flower không tồn tại hoặc không thuộc về bạn."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: User chỉ có thể tạo thiệp cho bó hoa AI thuộc về chính họ. Generated flower của user khác không được phép sử dụng.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng generated flower không thuộc user được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-12
- **Ghi chú**: Liên kết đến Business Rule về base_id trong client_histories và quyền sở hữu.
