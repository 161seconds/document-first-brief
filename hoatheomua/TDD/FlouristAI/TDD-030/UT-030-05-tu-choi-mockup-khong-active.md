# UT-030-05: Từ chối Mockup không active

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối Mockup không active
- **Ghi chú**: Kịch bản error - Mockup có is_active=false không được sử dụng.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-05
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (Mockup validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo tồn tại và khả dụng
  - Mockup tồn tại nhưng is_active=false (không còn hoạt động)
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "550e8400-e29b-41d4-a716-446655440001",
    "mockup_id": "inactive-mockup-uuid",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "BAD_REQUEST",
      "message": "Mockup không còn hoạt động."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-03, Mockup phải có is_active=true tại thời điểm gọi API.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng Mockup không active bị từ chối với HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-03
- **Ghi chú**: Liên kết đến Business Rule về Mockup phải active.
