# UT-035-07: Từ chối khi không có size config

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối khi không có size config
- **Ghi chú**: Kịch bản error - size config không tồn tại trong hệ thống.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (size config validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template tồn tại
  - Size config với key "size_Z" không tồn tại trong database
  - Quota: 0/10 lượt
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "Z",
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
      "message": "Size config không tồn tại."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo BR-035-01, size config phải tồn tại với IsDeleted=false và IsPublic=true. Khi không tìm thấy, trả lỗi 404.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng size config không tồn tại được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-01, API Contract - Error Example 8
- **Ghi chú**: Liên kết đến Business Rule về size config và ví dụ lỗi NOT_FOUND.
