# UT-036-10: Rollback khi lưu history tạo lại thất bại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Rollback khi lưu history tạo lại thất bại
- **Ghi chú**: Kịch bản error - lưu client_histories thất bại, generated_cards và quota cũng rollback.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-10
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (transaction rollback)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại
  - Quota: 3/10 tạo thiệp, 1/3 mẫu hoa
  - AI Module trả ảnh hợp lệ
  - Lưu generated_cards thành công
  - Lưu client_histories THẤT BẠI (exception)
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
  - generated_cards record đã tạo bị rollback (không tồn tại)
  - client_histories record không tạo
  - Quota tạo thiệp: vẫn là 3/10 (hoàn)
  - Quota mẫu hoa: vẫn là 1/3 (hoàn)
  
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
- **Mã**: TDD-036
- **Section**: BR-036-08, BR-036-10
- **Ghi chú**: Liên kết đến Business Rules về tạo client_histories và transaction handling.
