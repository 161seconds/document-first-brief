# UT-055-05: Từ chối thiếu field bắt buộc

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối thiếu field bắt buộc
- **Ghi chú**: Kịch bản error - thiếu một trong các field bắt buộc (key, group, kind).

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-05
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.CreateConfigAsync (validation)
- **Loại**: Error
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database trống
- **Input** (test 3 trường hợp):
  ```
  Test 1: Thiếu key
  POST /api/v1/configs
  {
    "group": "card_size",
    "kind": "Setting",
    "value": { "name": "A" }
  }
  
  Test 2: Thiếu group
  POST /api/v1/configs
  {
    "key": "size_A",
    "kind": "Setting",
    "value": { "name": "A" }
  }
  
  Test 3: Thiếu kind
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "value": { "name": "A" }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 400
  Response body:
  {
    "error": {
      "code": "VALIDATION_ERROR",
      "message": "<Field> is required."
    }
  }
  
  Messages:
  - Test 1: "Key is required."
  - Test 2: "Group is required."
  - Test 3: "Kind is required."
  
  Kiểm tra database:
  - Không tạo record nào
  
  Lý do: Theo API Contract, key, group, kind là các field bắt buộc.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng thiếu field bắt buộc bị từ chối với HTTP 400.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 5 - Validation: Missing key
- **Ghi chú**: Liên kết đến API Contract example.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 6 - Validation: Missing group
- **Ghi chú**: Liên kết đến API Contract example.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: Ví dụ 7 - Validation: Missing kind
- **Ghi chú**: Liên kết đến API Contract example.
