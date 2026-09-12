# UT-052-01: Chuyển trạng thái Mockup hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Chuyển trạng thái Mockup hợp lệ
- **Ghi chú**: Kịch bản happy path - toggle trạng thái từ active sang inactive.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-052-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.ToggleMockupStatus(id)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có mockup:
    - mockup-001: id="550e8400-e29b-41d4-a716-446655440001", name="Mockup Sinh Nhật 1", is_active=true, is_deleted=false
- **Input**:
  ```
  PATCH /api/mockups/550e8400-e29b-41d4-a716-446655440001/status
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "550e8400-e29b-41d4-a716-446655440001",
      "name": "Mockup Sinh Nhật 1",
      "is_active": false,
      "is_deleted": false,
      "updated_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - mockup.is_active = false
  - mockup.updated_at = <current timestamp>
  
  Lý do: Theo BR-052-01, Mockup Active có thể chuyển sang Inactive. updated_at được cập nhật.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng toggle trạng thái hoạt động đúng - mockup chuyển từ active sang inactive.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: BR-052-01
- **Ghi chú**: Liên kết đến Business Rule về toggle active sang inactive.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Ví dụ 1 - Happy path: Chuyển từ Active sang Inactive
- **Ghi chú**: Liên kết đến API Contract example.
