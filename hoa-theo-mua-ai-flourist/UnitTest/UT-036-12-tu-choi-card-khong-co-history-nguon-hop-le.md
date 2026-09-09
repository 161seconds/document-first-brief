# UT-036-12: Từ chối card không có history nguồn hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối card không có history nguồn hợp lệ
- **Ghi chú**: Kịch bản error - card không có client_histories hợp lệ để xác định base_id.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-12
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (history validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Generated card tồn tại và thuộc về user
  - Generated card KHÔNG có client_history (bất thường - có thể tạo trước khi có history tracking)
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
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
  - Không tạo generated_cards record mới
  - Không tạo client_histories record
  - Quota: không thay đổi
  
  Lý do: Theo BR-036-04, cần base_id từ client_histories để xác định thiệp mới. Khi không có history, không thể tạo lại.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng card không có history nguồn hợp lệ không thể tạo lại.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-04
- **Ghi chú**: Liên kết đến Business Rule về base_id từ client_histories.
