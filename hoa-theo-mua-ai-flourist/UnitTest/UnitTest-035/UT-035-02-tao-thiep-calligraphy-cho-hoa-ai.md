# UT-035-02: Tạo thiệp Calligraphy cho hoa AI

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo thiệp Calligraphy cho hoa AI
- **Ghi chú**: Kịch bản happy path - tạo thiệp calligraphy cho bó hoa AI (generated_flower_id).

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (calligraphy với generated_flower_id)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template với id "550e8400-e29b-41d4-a716-446655440001" tồn tại, IsDeleted = false
  - Size config "size_B" tồn tại (name="B", width=15, height=20, base_price=150000, max_words=150), IsDeleted = false, IsPublic = true
  - Generated flower với id "880e8400-e29b-41d4-a716-446655440005" tồn tại, thuộc về user
  - Quota tạo thiệp của user: 0/10 lượt trong ngày
  - Calligraphy words config tồn tại với các rule: [1-35 từ: extra_price=0], [36-70 từ: extra_price=39000], [71-150 từ: extra_price=65000]
  - Message content có 42 từ (thuộc range 36-70)
  - AI Module trả ảnh hợp lệ
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "B",
    "form_type": "calligraphy",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Nhân dịp sinh nhật bạn, tôi xin gửi đến bạn những lời chúc tốt đẹp nhất. Chúc bạn luôn hạnh phúc, khỏe mạnh và thành công trong cuộc sống.",
    "attached_image_url": "https://storage.example.com/images/flower-001.jpg",
    "generated_flower_id": "880e8400-e29b-41d4-a716-446655440005"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "generated-card-uuid",
      "form_type": "calligraphy",
      "size_key": "size_B",
      "size_name": "B",
      "size_width": 15,
      "size_height": 20,
      "size_base_price": 150000,
      "size_max_words": 150,
      "word_count": 42,
      "word_config_snapshot": {
        "min_words": 36,
        "max_words": 70,
        "extra_price": 39000
      },
      "is_confirmed": false,
      "base_price": 150000,
      "extra_price": 39000,
      "total_price": 189000
    }
  }
  
  Kiểm tra database:
  - generated_cards: tạo record mới với form_type="calligraphy", base_price=150000, extra_price=39000, word_config_snapshot đầy đủ
  - client_histories: tạo record với type="card", base_id=generated_flower_id, output_id=generated_card_id
  - Quota: count = 1/10 (đã trừ 1 lượt)
  
  Lý do: Theo BR-035-04, tìm rule có min_words <= 42 <= max_words → rule [36-70] với extra_price=39000. Theo BR-035-05, total_price = 150000 + 39000 = 189000.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận kịch bản tạo thiệp Calligraphy cho hoa AI với extra_price được tính đúng theo số từ.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-02, BR-035-04, BR-035-05
- **Ghi chú**: Liên kết đến Business Rule về calligraphy config, tìm rule, và cách tính tổng giá.
