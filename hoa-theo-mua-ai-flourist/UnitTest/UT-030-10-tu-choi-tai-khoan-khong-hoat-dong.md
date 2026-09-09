# UT-030-10: Từ chối tài khoản không hoạt động

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối tài khoản không hoạt động
- **Ghi chú**: Kịch bản authorization - tài khoản không hoạt động không được phép tạo hoa.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-10
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (account status check)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập nhưng tài khoản không còn hoạt động (bị khóa/vô hiệu hóa)
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
  HTTP 401
  Response body:
  {
    "error": {
      "code": "UNAUTHORIZED",
      "message": "Tài khoản không còn hoạt động."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-01, chỉ tài khoản đang hoạt động mới được gọi API.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng tài khoản không hoạt động bị từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-01
- **Ghi chú**: Liên kết đến Business Rule về tài khoản hoạt động.
