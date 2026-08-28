# UT-051-10: Không tạo trùng Mockup khi gửi lại idempotency key

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không tạo trùng Mockup khi gửi lại idempotency key
- **Ghi chú**: Kịch bản deterministic - khi client gửi lại cùng idempotency key, trả về mockup đã tạo trước đó thay vì tạo mới.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-10
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.CreateMockup(request, idempotencyKey)
- **Loại**: Determinism
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database đã có mockup được tạo với idempotency_key = "idem-123"
  - Request mới có cùng idempotency_key = "idem-123"
- **Input**:
  ```
  POST /api/mockups
  Header: X-Idempotency-Key: idem-123
  Content-Type: multipart/form-data
  {
    "name": "Mockup Test",
    "description": "Mô tả mockup",
    "image": [file upload]
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200 (hoặc HTTP 201 - không tạo mới)
  Response body:
  {
    "value": {
      "id": "<existing-mockup-id>",
      "name": "Mockup Test",
      "description": "Mô tả mockup",
      "image_url": "https://storage.example.com/mockups/existing.jpg",
      ...
    }
  }
  
  Kiểm tra:
  - Chỉ có 1 mockup trong database với idempotency_key này
  - Không tạo mockup mới
  
  Lý do: Theo BR-051-08, chống duplicate bằng idempotency key. Khi gửi lại cùng key, trả về record đã tạo trước đó.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng idempotency được enforce đúng - gửi lại cùng key không tạo duplicate.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-08
- **Ghi chú**: Liên kết đến Business Rule về idempotency key.
