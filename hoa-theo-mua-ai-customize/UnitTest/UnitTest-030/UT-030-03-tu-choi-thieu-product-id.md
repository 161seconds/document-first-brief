# UT-030-03: Từ chối thiếu product_id

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối thiếu product_id
- **Ghi chú**: Kịch bản validation - product_id bắt buộc.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-03
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
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  (thiếu product_id)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "product_id is required."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-02, product_id bắt buộc và phải xác định một Combo còn khả dụng.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng product_id bắt buộc, không có thì trả HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-02
- **Ghi chú**: Liên kết đến Business Rule về product_id bắt buộc.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Mã lỗi - VALIDATION_ERROR
- **Ghi chú**: Liên kết đến mã lỗi.
