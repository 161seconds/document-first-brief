# UT-036-01: Tạo lại thiệp thành công từ history

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo lại thiệp thành công từ history
- **Ghi chú**: Kịch bản happy path - tạo lại thiệp go_may từ thiệp nguồn.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (happy path)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Thiệp nguồn với id "550e8400-e29b-41d4-a716-446655440001" tồn tại và thuộc về user
  - Thiệp nguồn có form_type="go_may", size_key="size_A", sender_name="Nguyễn An", receiver_name="Trần Bình"
  - Thiệp nguồn có snapshot: size_name="A", size_width=10, size_height=15, size_base_price=100000
  - Quota tạo thiệp của user: 3/10 lượt trong ngày
  - AI Module trả ảnh hợp lệ
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "new-card-uuid",
      "form_type": "go_may",
      "size_key": "size_A",
      "sender_name": "Nguyễn An",
      "receiver_name": "Trần Bình",
      "message_content": "Chúc bạn sinh nhật vui vẻ",
      "size_name": "A",
      "size_width": 10,
      "size_height": 15,
      "size_base_price": 100000,
      "word_config_snapshot": null,
      "is_confirmed": false,
      "base_price": 100000,
      "extra_price": 0,
      "total_price": 100000
    }
  }
  
  Kiểm tra database:
  - generated_cards: tạo record MỚI với snapshot từ thiệp nguồn
  - client_histories: tạo record với base_id = base_id cũ, output_id = card mới
  - Thiệp nguồn: KHÔNG bị sửa đổi
  - Quota: count = 4/10 (đã trừ 1 lượt)
  
  Lý do: Theo BR-036-01, BR-036-04, BR-036-05, hệ thống tạo thiệp mới từ snapshot thiệp nguồn. Theo BR-036-11, thiệp nguồn không bị ghi đè.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận kịch bản tạo lại thiệp thành công từ history với snapshot được copy.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-01, BR-036-04, BR-036-05, BR-036-11
- **Ghi chú**: Liên kết đến Business Rules về tạo lại, snapshot và không ghi đè thiệp nguồn.
