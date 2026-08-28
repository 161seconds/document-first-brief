# UT-036-08: Không tự gắn thiệp tạo lại vào Checkout

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không tự gắn thiệp tạo lại vào Checkout
- **Ghi chú**: Kịch bản branch - xác nhận order_id = null sau khi tạo lại.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-08
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (no auto checkout)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại
  - Quota hợp lệ, AI Module trả ảnh thành công
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
    "order_id": null,
    "is_confirmed": false
  }
  
  Kiểm tra database:
  - generated_cards: order_id = null (không tự gắn vào checkout)
  - client_histories: được tạo bình thường
  
  Lý do: Theo BR-036-12, kết quả mới không tự động gắn vào Checkout vì thao tác từ lịch sử.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng thiệp tạo lại không tự gắn vào Checkout.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-12
- **Ghi chú**: Liên kết đến Business Rule về không tự gắn vào Checkout.
