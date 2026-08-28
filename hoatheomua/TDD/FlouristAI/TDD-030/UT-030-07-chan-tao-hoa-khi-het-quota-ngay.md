# UT-030-07: Chặn tạo hoa khi hết quota ngày

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn tạo hoa khi hết quota ngày
- **Ghi chú**: Kịch bản error - đã tạo 3 lượt trong ngày, không cho phép tạo thêm.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (quota check)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo tồn tại và khả dụng
  - Mockup tồn tại, is_active=true, is_deleted=false
  - Quota: đã tạo 3/3 lượt trong ngày (hết quota)
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
  HTTP 403
  Response body:
  {
    "error": {
      "code": "FORBIDDEN",
      "message": "Bạn đã sử dụng hết 3 lượt tạo mẫu hoa AI trong ngày."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Quota không thay đổi (vẫn là 3/3)
  
  Lý do: Theo BR-030-04, tối đa 3 lượt tạo hoa mỗi ngày, kiểm tra trước khi gọi AI.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng hết quota bị chặn với HTTP 403.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-04
- **Ghi chú**: Liên kết đến Business Rule về quota.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Ví dụ 3 - Hết quota tạo hoa trong ngày
- **Ghi chú**: Liên kết đến API Contract example.
