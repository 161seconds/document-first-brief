# UT-030-18: Cho phép tạo hoa ở ngày quota mới

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cho phép tạo hoa ở ngày quota mới
- **Ghi chú**: Kịch bản happy path - quota reset theo ngày.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-18
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (quota reset)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Hôm qua: đã tạo 3/3 lượt (hết quota ngày hôm qua)
  - Hôm nay: quota reset về 0/3
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "combo-uuid",
    "mockup_id": "mockup-uuid",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "generated-flower-uuid",
      ...
    }
  }
  
  Kiểm tra database:
  - Quota hôm nay: count = 1
  - Quota ngày hôm qua: vẫn là 3 (không thay đổi)
  
  Lý do: Theo BR-030-04, quota tối đa 3 lượt tạo hoa MỖI NGÀY. Quota reset theo ngày.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng quota reset theo ngày.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-04
- **Ghi chú**: Liên kết đến Business Rule về quota mỗi ngày.
