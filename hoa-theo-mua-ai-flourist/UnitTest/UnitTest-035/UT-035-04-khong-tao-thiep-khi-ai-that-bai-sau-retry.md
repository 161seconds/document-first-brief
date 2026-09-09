# UT-035-04: Không tạo thiệp khi AI thất bại sau retry

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không tạo thiệp khi AI thất bại sau retry
- **Ghi chú**: Kịch bản error - AI thất bại 2 lần, không tạo record và hoàn quota.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (AI retry failure)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template và Size config hợp lệ
  - Quota tạo thiệp của user: 2/10 lượt trong ngày
  - AI Module thất bại 2 lần (retry), trả exception
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
  HTTP 500
  Response body:
  {
    "error": {
      "code": "INTERNAL_SERVER_ERROR",
      "message": "Không thể tạo ảnh thiệp. Vui lòng thử lại sau."
    }
  }
  
  Kiểm tra database:
  - generated_cards: không tạo record
  - client_histories: không tạo record
  - Quota: vẫn là 2/10 (đã hoàn lại quota)
  
  Lý do: Theo BR-035-09, khi AI thất bại sau 2 lần retry, hệ thống hoàn quota đã trừ. Theo BR-035-10, client_histories chỉ được tạo khi có ảnh hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi AI thất bại, quota được hoàn và không tạo record.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-09, BR-035-10
- **Ghi chú**: Liên kết đến Business Rule về hoàn quota và điều kiện tạo client_histories.
