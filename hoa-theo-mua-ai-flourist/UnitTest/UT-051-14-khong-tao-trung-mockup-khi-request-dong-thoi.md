# UT-051-14: Không tạo trùng Mockup khi request đồng thời

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không tạo trùng Mockup khi request đồng thời
- **Ghi chú**: Kịch bản deterministic - khi nhiều request với cùng idempotency key gửi đồng thời, chỉ một mockup được tạo.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-051-14
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
  - 3 request đồng thời với cùng idempotency_key = "idem-simultaneous"
  - Tất cả request đều có dữ liệu hợp lệ
- **Input**:
  ```
  POST /api/mockups (3 request đồng thời)
  Header: X-Idempotency-Key: idem-simultaneous
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "<single-mockup-id>",
      ...
    }
  }
  
  Kiểm tra:
  - Database chỉ có 1 mockup với idempotency_key này
  - Tất cả 3 response trả về cùng một mockup_id
  - Không có duplicate records
  
  Lý do: Idempotency key phải hoạt động đúng cả trong trường hợp request đồng thời - đảm bảo chỉ tạo 1 record.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng idempotency hoạt động đúng trong trường hợp race condition - không tạo duplicate khi request đồng thời.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-051
- **Section**: BR-051-08
- **Ghi chú**: Liên kết đến Business Rule về idempotency key.
