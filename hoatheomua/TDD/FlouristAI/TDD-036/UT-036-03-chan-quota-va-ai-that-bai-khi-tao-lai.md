# UT-036-03: Chặn quota và AI thất bại khi tạo lại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn quota và AI thất bại khi tạo lại
- **Ghi chú**: Kịch bản error - hết quota tạo thiệp hoặc hết quota mẫu hoa.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (quota validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại và thuộc về user
  - Test 1: Quota tạo thiệp = 10/10 (hết quota ngày)
  - Test 2: Quota tạo thiệp = 5/10, Quota mẫu hoa = 3/3 (hết quota mẫu hoa)
- **Input**:
  ```
  Test 1:
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  
  Test 2:
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  Test 1: HTTP 403
  Response body:
  {
    "error": {
      "code": "FORBIDDEN",
      "message": "Bạn đã sử dụng hết 10 lượt tạo thiệp AI trong ngày."
    }
  }
  
  Test 2: HTTP 403
  Response body:
  {
    "error": {
      "code": "FORBIDDEN",
      "message": "Bạn đã sử dụng hết số lượt tạo thiệp cho mẫu hoa này."
    }
  }
  
  Kiểm tra database:
  - Không tạo generated_cards record
  - Không tạo client_histories record
  - Quota: không thay đổi (không gọi AI)
  
  Lý do: Theo BR-036-06, BR-036-07, hệ thống kiểm tra quota trước khi gọi AI.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng hết quota được phát hiện và từ chối trước khi gọi AI.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-06, BR-036-07, API Contract - Error Examples 5, 6
- **Ghi chú**: Liên kết đến Business Rules về kiểm tra quota và ví dụ lỗi.
