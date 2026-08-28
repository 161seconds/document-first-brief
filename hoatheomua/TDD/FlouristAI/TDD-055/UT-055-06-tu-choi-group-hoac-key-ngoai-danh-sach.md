# UT-055-06: Từ chối group hoặc key ngoài danh sách

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Từ chối group hoặc key ngoài danh sách
- **Ghi chú**: Kịch bản validation - group và key phải thuộc danh sách predefined.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-055-06
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
  Test 1: group không hợp lệ
  POST /api/v1/configs
  {
    "group": "invalid_group",
    "key": "size_A",
    "kind": "Setting",
    "value": { "name": "A" }
  }
  
  Test 2: key không hợp lệ
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "invalid_key",
    "kind": "Setting",
    "value": { "name": "A" }
  }
  
  Test 3: kind không phải "Setting"
  POST /api/v1/configs
  {
    "group": "card_size",
    "key": "size_A",
    "kind": "Other",
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
      "message": "Group/Key/Kind is not valid."
    }
  }
  
  Kiểm tra database:
  - Không tạo record nào
  
  Lý do: Theo BR-055-03 (Kind="Setting"), BR-055-04 (group predefined), BR-055-05 (key predefined).
  
  Note: Nếu BR-055-05 và BR-055-06 nói "không check trùng key" nhưng không nói rõ về validation group/key, thì test case này cần xác nhận lại với team.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng group/key/kind ngoài danh sách bị từ chối.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-055
- **Section**: BR-055-03, BR-055-04, BR-055-05
- **Ghi chú**: Liên kết đến Business Rules về predefined values.
