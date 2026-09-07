# UT-056-03: Xóa Config đang được card snapshot tham chiếu

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Xóa Config đang được card snapshot tham chiếu
- **Ghi chú**: Kịch bản happy path - xóa config có generated_cards đang tham chiếu vẫn thành công (snapshot).

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-056-03
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.DeleteConfigAsync(id) (FK constraint)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có:
    - config-001: id="uuid-config", key="size_A", is_deleted=false
    - generated_card-001: id="uuid-card", size_key="size_A", size_name="A", size_base_price=100000 (snapshot)
- **Input**:
  ```
  DELETE /api/v1/configs/uuid-config
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "success": true,
      "message": "Config deleted successfully."
    }
  }
  
  Kiểm tra:
  - config.is_deleted = true
  - generated_card vẫn tồn tại với đầy đủ snapshot data
  - Không có lỗi FK constraint
  
  Lý do: Theo BR-056-05 và BR-056-06, không check ràng buộc với generated_cards vì các thiệp đã dùng snapshot nên không bị ảnh hưởng.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng config có generated_cards tham chiếu vẫn có thể xóa được (snapshot pattern).

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: BR-056-05
- **Ghi chú**: Liên kết đến Business Rule về không check FK constraint.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: BR-056-06
- **Ghi chú**: Liên kết đến Business Rule về snapshot pattern.

**Link 3**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Notes
- **Ghi chú**: generated_cards dùng snapshot nên khi config bị xóa, các thiệp đã tạo vẫn giữ nguyên thông tin giá.
