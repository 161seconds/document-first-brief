# UT-035-15: Từ chối Calligraphy không có rule giá

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối Calligraphy không có rule giá
- **Ghi chú**: Kịch bản error - số từ vượt ngoài tất cả các rule calligraphy.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-15
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (calligraphy rule validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Card template và Size config hợp lệ
  - Calligraphy words config: [1-35 từ: extra_price=0], [36-70 từ: extra_price=39000], [71-150 từ: extra_price=65000]
  - Message content có 200 từ (vượt ngoài tất cả các rule)
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "B",
    "form_type": "calligraphy",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "<200 từ>",
    "product_id": "440e8400-e29b-41d4-a716-446655440010"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Số từ trong lời chúc không nằm trong giới hạn cho phép của hình thức calligraphy."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo BR-035-04, phải tìm được rule phù hợp. Khi không có rule nào phù hợp (số từ vượt ngoài tất cả các rule), trả lỗi VALIDATION_ERROR.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng số từ vượt ngoài các rule calligraphy được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-04
- **Ghi chú**: Liên kết đến Business Rule về tìm rule calligraphy.
