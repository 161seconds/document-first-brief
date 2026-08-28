# UT-035-14: Tính giá Calligraphy tại các cận rule

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tính giá Calligraphy tại các cận rule
- **Ghi chú**: Kịch bản boundary - kiểm tra extra_price tại các điểm biên của calligraphy words config.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-14
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (calligraphy pricing boundary)
- **Loại**: Boundary
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Card template và Size config hợp lệ
  - Calligraphy words config: [1-35 từ: extra_price=0], [36-70 từ: extra_price=39000], [71-150 từ: extra_price=65000]
  - Test cases:
    - 1 từ → extra_price = 0
    - 35 từ → extra_price = 0
    - 36 từ → extra_price = 39000
    - 70 từ → extra_price = 39000
    - 71 từ → extra_price = 65000
    - 150 từ → extra_price = 65000
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "B",
    "form_type": "calligraphy",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "<N từ>",
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  Test 1: 1 từ → extra_price = 0, total_price = 150000
  Test 2: 35 từ → extra_price = 0, total_price = 150000
  Test 3: 36 từ → extra_price = 39000, total_price = 189000
  Test 4: 70 từ → extra_price = 39000, total_price = 189000
  Test 5: 71 từ → extra_price = 65000, total_price = 215000
  Test 6: 150 từ → extra_price = 65000, total_price = 215000
  
  Kiểm tra database:
  - word_config_snapshot chứa đúng rule được áp dụng
  - extra_price và total_price tính đúng theo rule
  
  Lý do: Theo BR-035-04, tìm rule có min_words <= word_count <= max_words. Tại các điểm biên, rule chính xác phải được áp dụng.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng giá được tính đúng tại các điểm biên của calligraphy rules.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-04, BR-035-05
- **Ghi chú**: Liên kết đến Business Rule về tìm rule và cách tính tổng giá.
