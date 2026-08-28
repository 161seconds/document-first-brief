# UT-036-09: Tạo lại thiệp thành công sau retry

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo lại thiệp thành công sau retry
- **Ghi chú**: Kịch bản branch - AI thất bại lần đầu, thành công lần thứ hai.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-09
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (retry success)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại
  - Quota tạo thiệp: 4/10, Quota mẫu hoa: 1/3
  - AI Module thất bại lần 1, thành công lần 2
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body chứa:
  {
    "id": "new-card-uuid",
    "image_url": "https://new-image..."
  }
  
  Kiểm tra database:
  - generated_cards: tạo record với ảnh từ lần retry thành công
  - client_histories: tạo record
  - Quota tạo thiệp: count = 5/10 (chỉ trừ 1 lượt)
  - Quota mẫu hoa: count = 2/3
  
  Lý do: Theo BR-036-08, khi tạo thành công, trừ quota. Chỉ gọi AI đến khi có ảnh hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng retry logic hoạt động đúng và quota chỉ trừ 1 lần.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-08, BR-036-09
- **Ghi chú**: Liên kết đến Business Rules về trừ quota và retry logic.
