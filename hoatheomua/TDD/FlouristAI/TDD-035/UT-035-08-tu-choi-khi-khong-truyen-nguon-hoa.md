# UT-035-08: Từ chối khi không truyền nguồn hoa

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối khi không truyền nguồn hoa
- **Ghi chú**: Kịch bản error - không có product_id hoặc generated_flower_id.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-035-08
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.CreateAiCardAsync (nguồn hoa validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Card template và Size config hợp lệ
  - Không có product_id hoặc generated_flower_id trong request
- **Input**:
  ```
  POST /api/ai-cards
  {
    "card_template_id": "550e8400-e29b-41d4-a716-446655440001",
    "size": "A",
    "form_type": "go_may",
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc bạn sinh nhật vui vẻ"
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "Phải cung cấp product_id hoặc generated_flower_id."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo API Contract, phải cung cấp product_id (cho hoa thường) hoặc generated_flower_id (cho hoa AI), không được cả hai và cũng không được thiếu cả hai.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng thiếu nguồn hoa được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: API Contract - Error Example 9
- **Ghi chú**: Liên kết đến ví dụ API về lỗi VALIDATION_ERROR khi không truyền nguồn hoa.
