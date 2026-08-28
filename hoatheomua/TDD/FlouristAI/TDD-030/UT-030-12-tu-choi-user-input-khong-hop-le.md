# UT-030-12: Từ chối user_input không hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối user_input không hợp lệ
- **Ghi chú**: Kịch bản validation - user_input phải hợp lệ.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-12
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "550e8400-e29b-41d4-a716-446655440001",
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
    "user_input": { }
  }
  (user_input trống hoặc không hợp lệ)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "user_input is required or invalid."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-02, dữ liệu người dùng nhập phải hợp lệ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng user_input không hợp lệ bị từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Mã lỗi - VALIDATION_ERROR
- **Ghi chú**: Liên kết đến mã lỗi.
