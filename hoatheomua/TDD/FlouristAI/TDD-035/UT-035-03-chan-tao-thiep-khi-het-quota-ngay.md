# UT-035-03: Chặn tạo thiệp khi hết quota ngày

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn tạo thiệp khi hết quota ngày
- **Ghi chú**: Kịch bản error - quota tạo thiệp đã đạt 10 lượt/ngày.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (quota check)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template và Size config hợp lệ
  - Quota tạo thiệp của user: 10/10 lượt trong ngày (đã hết quota)
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
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 403
  Response body:
  {
    "error": {
      "code": "FORBIDDEN",
      "message": "Bạn đã sử dụng hết 10 lượt tạo thiệp AI trong ngày."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: count vẫn = 10/10 (không thay đổi)
  
  Lý do: Theo BR-035-07, hệ thống kiểm tra quota trước khi gọi AI. Khi quota đã hết, trả lỗi 403 và không gọi AI.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi hết quota, hệ thống chặn tạo thiệp và không gọi AI.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-07
- **Ghi chú**: Liên kết đến Business Rule về kiểm tra quota trước khi gọi AI.
