# UT-035-17: Tạo thiệp thành công sau retry

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo thiệp thành công sau retry
- **Ghi chú**: Kịch bản branch - AI thất bại lần đầu, thành công lần thứ hai.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-17
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (AI retry success)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Card template và Size config hợp lệ
  - Quota tạo thiệp: 5/10 lượt
  - AI Module thất bại lần 1, thành công lần 2
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
  HTTP 200
  Response body chứa:
  {
    "id": "generated-card-uuid",
    "image_url": "https://...",
    "is_confirmed": false
  }
  
  Kiểm tra database:
  - generated_cards: tạo record với ảnh từ lần retry thành công
  - client_histories: tạo record
  - Quota: count = 6/10 (chỉ trừ 1 lượt cho lần thành công)
  
  Lý do: Theo BR-035-08, khi tạo thành công (có ảnh hợp lệ), hệ thống trừ 1 lượt quota và tạo client_histories. Chỉ gọi AI đến khi có ảnh hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng retry logic hoạt động đúng và quota chỉ trừ 1 lần cho lần thành công.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-08, BR-035-09
- **Ghi chú**: Liên kết đến Business Rule về trừ quota và retry logic.
