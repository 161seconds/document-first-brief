# UT-030-11: Từ chối thiếu mockup_id

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối thiếu mockup_id
- **Ghi chú**: Kịch bản validation - mockup_id bắt buộc.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-11
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
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  (thiếu mockup_id)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "mockup_id is required."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-03, mockup_id bắt buộc.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng mockup_id bắt buộc, không có thì trả HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-03
- **Ghi chú**: Liên kết đến Business Rule về mockup_id bắt buộc.
