# UT-035-11: Từ chối form_type không hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối form_type không hợp lệ
- **Ghi chú**: Kịch bản error - form_type không nằm trong danh sách go_may, calligraphy.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-11
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (form_type validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template và Size config hợp lệ
  - form_type "invalid" không phải là giá trị hợp lệ (chỉ go_may, calligraphy)
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "A",
    "form_type": "invalid",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ",
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
      "message": "Form type không hợp lệ. Vui lòng chọn go_may hoặc calligraphy."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: form_type phải là một trong các giá trị: go_may, calligraphy. Giá trị khác bị từ chối với lỗi VALIDATION_ERROR.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng form_type không hợp lệ được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: API Contract - Error Example 12
- **Ghi chú**: Liên kết đến ví dụ API về lỗi VALIDATION_ERROR khi form_type không hợp lệ.
