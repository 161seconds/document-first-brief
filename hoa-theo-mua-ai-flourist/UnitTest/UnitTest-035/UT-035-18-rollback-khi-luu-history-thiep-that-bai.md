# UT-035-18: Rollback khi lưu history thiệp thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Rollback khi lưu history thiệp thất bại
- **Ghi chú**: Kịch bản error - lưu client_histories thất bại, generated_cards cũng rollback.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-18
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (transaction rollback)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Card template và Size config hợp lệ
  - Quota: 3/10 lượt
  - AI Module trả ảnh hợp lệ
  - Lưu generated_cards thành công
  - Lưu client_histories THẤT BẠI (exception)
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
  HTTP 500
  Response body:
  {
    "error": {
      "code": "INTERNAL_SERVER_ERROR",
      "message": "An unexpected error occurred."
    }
  }
  
  Kiểm tra database:
  - generated_cards record đã tạo bị rollback (không tồn tại)
  - client_histories record không tạo
  - Quota vẫn là 3/10 (không trừ)
  
  Lý do: Transaction rollback khi lưu client_histories thất bại. Toàn bộ operation được revert bao gồm cả quota.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi lưu thất bại, toàn bộ transaction được rollback.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: BR-035-08, BR-035-10
- **Ghi chú**: Liên kết đến Business Rule về tạo client_histories và transaction handling.
