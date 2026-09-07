# UT-036-05: Tạo lại thiệp Calligraphy từ hoa AI

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo lại thiệp Calligraphy từ hoa AI
- **Ghi chú**: Kịch bản happy path - tạo lại thiệp calligraphy từ mẫu hoa AI.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-05
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (calligraphy từ flower)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn với form_type="calligraphy", có flower base
  - Client history cũ có base_id = generated_flower_id
  - Quota tạo thiệp: 2/10, Quota mẫu hoa: 1/3
  - AI Module trả ảnh hợp lệ
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440002/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body chứa:
  {
    "id": "new-card-uuid",
    "form_type": "calligraphy",
    "base_price": 150000,
    "extra_price": 39000,
    "total_price": 189000
  }
  
  Kiểm tra database:
  - generated_cards: tạo record MỚI với snapshot từ thiệp nguồn
  - client_histories: tạo record với base_id = base_id cũ (generated_flower_id)
  - Quota tạo thiệp: count = 3/10
  - Quota mẫu hoa: count = 2/3
  
  Lý do: Theo BR-036-13, base_id mới = base_id cũ. Theo BR-036-08, trừ cả quota tạo thiệp và quota mẫu hoa.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận kịch bản tạo lại thiệp calligraphy từ mẫu hoa với quota được trừ đúng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-08, BR-036-13, API Contract - Example 2
- **Ghi chú**: Liên kết đến Business Rules về trừ quota và base_id.
