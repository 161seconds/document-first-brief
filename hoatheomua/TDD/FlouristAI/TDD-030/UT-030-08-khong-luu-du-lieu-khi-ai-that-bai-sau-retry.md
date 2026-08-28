# UT-030-08: Không lưu dữ liệu khi AI thất bại sau retry

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không lưu dữ liệu khi AI thất bại sau retry
- **Ghi chú**: Kịch bản error - AI không trả ảnh hợp lệ sau 2 lần retry, không tạo record và không trừ quota.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-08
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (AI failure handling)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo tồn tại và khả dụng
  - Mockup tồn tại, is_active=true, is_deleted=false
  - Quota: đã tạo 1/3 lượt trong ngày
  - AI Module thất bại: không trả ảnh hợp lệ sau 2 lần retry
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "550e8400-e29b-41d4-a716-446655440001",
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
    "user_input": { "name": "Bó hoa sinh nhật" }
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
  - Không tạo generated_flowers record
  - Không tạo client_histories record
  - Quota vẫn là 1/3 (không trừ thêm)
  
  Lý do: Theo BR-030-06 (retry tối đa 2 lần), BR-030-11 (AI thất bại không lưu và không trừ quota).
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi AI thất bại sau retry, không tạo record và không trừ quota.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-06
- **Ghi chú**: Liên kết đến Business Rule về retry.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-11
- **Ghi chú**: Liên kết đến Business Rule về không lưu khi AI thất bại.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Ví dụ 4 - AI thất bại sau retry
- **Ghi chú**: Liên kết đến API Contract example.
