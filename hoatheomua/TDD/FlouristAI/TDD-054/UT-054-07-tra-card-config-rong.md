# UT-054-07: Trả Card Config rỗng

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Trả Card Config rỗng
- **Ghi chú**: Kịch bản happy path - khi không có dữ liệu phù hợp, trả về mảng rỗng.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-054-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: CARD CONFIG
- **Unit under test (bắt buộc)**: ConfigService.GetCardConfigsAsync (empty result)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có card configs:
    - config-001: id="uuid-001", key="size_A", group="card_size", kind="Setting", is_public=true, is_deleted=false
  - Không có config nào có key="nonexistent_key"
- **Input**:
  ```
  GET /api/v1/configs/content?key=nonexistent_key
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": []
  }
  
  Lý do: Khi không có dữ liệu phù hợp với filter, trả về mảng rỗng thay vì lỗi.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng khi không có dữ liệu, trả về mảng rỗng với HTTP 200.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-054
- **Section**: Ví dụ 5 - Không có dữ liệu
- **Ghi chú**: Liên kết đến API Contract example.
