# UT-030-02: Chặn tạo hoa khi chưa đăng nhập

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chặn tạo hoa khi chưa đăng nhập
- **Ghi chú**: Kịch bản authentication - user chưa đăng nhập không được phép tạo hoa.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-02
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerController.CreateAiFlower (authentication)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User chưa đăng nhập (không có token hoặc token không hợp lệ)
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "550e8400-e29b-41d4-a716-446655440001",
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  (không có Authorization header)
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 401
  Response body:
  {
    "error": {
      "code": "UNAUTHORIZED",
      "message": "Bạn cần đăng nhập để thực hiện thao tác này."
    }
  }
  
  Kiểm tra:
  - Không gọi AI Module
  - Không tạo record nào
  - Không trừ quota
  
  Lý do: Theo BR-030-01, chỉ khách hàng đã đăng nhập mới được gọi API.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng authentication được enforce - user chưa đăng nhập bị từ chối với HTTP 401.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-01
- **Ghi chú**: Liên kết đến Business Rule về authentication.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Mã lỗi - UNAUTHORIZED
- **Ghi chú**: Liên kết đến mã lỗi.
