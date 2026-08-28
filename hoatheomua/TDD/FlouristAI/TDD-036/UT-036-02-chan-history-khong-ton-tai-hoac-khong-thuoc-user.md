# UT-036-02: Chặn history không tồn tại hoặc không thuộc user

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn history không tồn tại hoặc không thuộc user
- **Ghi chú**: Kịch bản error - thiệp nguồn không tồn tại hoặc thuộc user khác.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (source card validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - Test 1: Thiệp với id "00000000-0000-0000-0000-000000000000" không tồn tại
  - Test 2: User A đăng nhập, thiệp thuộc User B
- **Input**:
  ```
  Test 1:
  POST /api/ai-cards/00000000-0000-0000-0000-000000000000/regenerate
  
  Test 2:
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440003/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  Test 1: HTTP 404
  Response body:
  {
    "error": {
      "code": "CARD_NOT_FOUND",
      "message": "Thiệp không tồn tại."
    }
  }
  
  Test 2: HTTP 403
  Response body:
  {
    "error": {
      "code": "ACCESS_DENIED",
      "message": "Bạn không có quyền truy cập thiệp này."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo BR-036-03, generated_card_id phải tồn tại và thuộc về khách hàng hiện tại.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng thiệp không tồn tại hoặc không thuộc user được phát hiện và từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-03, API Contract - Error Examples 3, 4
- **Ghi chú**: Liên kết đến Business Rule về quyền sở hữu thiệp và ví dụ lỗi.
