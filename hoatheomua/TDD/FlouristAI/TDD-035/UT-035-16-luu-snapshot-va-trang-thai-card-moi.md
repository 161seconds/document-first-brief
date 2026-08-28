# UT-035-16: Lưu snapshot và trạng thái card mới

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lưu snapshot và trạng thái card mới
- **Ghi chú**: Kịch bản branch - xác nhận snapshot được lưu đúng và is_confirmed = false.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-16
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (snapshot và is_confirmed)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Card template tồn tại
  - Size config "size_C" với name="C", width=20, height=25, base_price=200000, max_words=200
  - Calligraphy config có rule phù hợp với message 50 từ (extra_price=39000)
  - AI Module trả ảnh hợp lệ
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "C",
    "form_type": "calligraphy",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "<50 từ lời chúc>",
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body chứa:
  {
    "size_name": "C",
    "size_width": 20,
    "size_height": 25,
    "size_base_price": 200000,
    "size_max_words": 200,
    "word_config_snapshot": {
      "min_words": 36,
      "max_words": 70,
      "extra_price": 39000
    },
    "is_confirmed": false,
    "base_price": 200000,
    "extra_price": 39000,
    "total_price": 239000
  }
  
  Kiểm tra database:
  - generated_cards có đầy đủ snapshot: size_name, size_width, size_height, size_base_price, size_max_words, word_config_snapshot
  - is_confirmed = false (theo BR-035-11)
  - client_histories được tạo với metadata chứa card_template_info (theo BR-035-14)
  
  Lý do: Theo BR-035-06, snapshot phải lưu tất cả thông tin ảnh hưởng đến giá. Theo BR-035-11, is_confirmed mặc định = false.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng snapshot được lưu đầy đủ và is_confirmed mặc định = false.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-06, BR-035-11, BR-035-14
- **Ghi chú**: Liên kết đến Business Rule về snapshot và is_confirmed.
