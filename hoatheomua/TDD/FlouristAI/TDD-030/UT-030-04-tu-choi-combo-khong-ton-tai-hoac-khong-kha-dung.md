# UT-030-04: Từ chối Combo không tồn tại hoặc không khả dụng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối Combo không tồn tại hoặc không khả dụng
- **Ghi chú**: Kịch bản error - Combo không tồn tại hoặc không còn khả dụng.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-04
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (Combo validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - product_id không tồn tại hoặc Combo không còn khả dụng trong Core Database
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "00000000-0000-0000-0000-000000000000",
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
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
      "message": "Combo không tồn tại hoặc không còn khả dụng."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-02, product_id phải xác định một Combo còn khả dụng.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng Combo không hợp lệ bị từ chối với HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-02
- **Ghi chú**: Liên kết đến Business Rule về Combo phải khả dụng.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Ví dụ 2 - Combo hoặc Mockup không hợp lệ
- **Ghi chú**: Liên kết đến API Contract example.
