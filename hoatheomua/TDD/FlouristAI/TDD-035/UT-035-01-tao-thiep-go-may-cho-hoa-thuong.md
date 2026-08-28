# UT-035-01: Tạo thiệp Gõ máy cho hoa thường

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo thiệp Gõ máy cho hoa thường
- **Ghi chú**: Kịch bản happy path - tạo thiệp gõ máy cho bó hoa bình thường (product_id).

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (go_may với product_id)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template với id "550e8400-e29b-41d4-a716-446655440001" tồn tại, IsDeleted = false
  - Size config "size_A" tồn tại (name="A", width=10, height=15, base_price=100000, max_words=100), IsDeleted = false, IsPublic = true
  - Product với id "440e8400-e29b-41d4-a716-446655440010" tồn tại
  - Quota tạo thiệp của user: 2/10 lượt trong ngày
  - AI Module trả ảnh hợp lệ
  - Calligraphy words config tồn tại nhưng không áp dụng cho go_may
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
    "attached_image_url": null,
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "generated-card-uuid",
      "form_type": "go_may",
      "size_key": "size_A",
      "size_name": "A",
      "size_width": 10,
      "size_height": 15,
      "size_base_price": 100000,
      "size_max_words": 100,
      "word_count": 5,
      "word_config_snapshot": null,
      "is_confirmed": false,
      "base_price": 100000,
      "extra_price": 0,
      "total_price": 100000
    }
  }
  
  Kiểm tra database:
  - generated_cards: tạo record mới với form_type="go_may", base_price=100000, extra_price=0
  - client_histories: tạo record với type="card", base_id=product_id, output_id=generated_card_id, metadata=card_template_info
  - Quota: count = 3/10 (đã trừ 1 lượt)
  
  Lý do: Theo BR-035-03, Gõ máy không phụ phí calligraphy. Theo BR-035-05, total_price = base_price + extra_price = 100000 + 0.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận kịch bản tạo thiệp Gõ máy cho hoa thường với giá = base_price, không có extra_price.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-03, BR-035-05
- **Ghi chú**: Liên kết đến Business Rule về giá gõ máy và cách tính tổng giá.
