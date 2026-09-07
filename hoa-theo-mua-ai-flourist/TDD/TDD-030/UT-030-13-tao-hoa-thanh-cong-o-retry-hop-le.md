# UT-030-13: Tạo hoa thành công ở retry hợp lệ

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo hoa thành công ở retry hợp lệ
- **Ghi chú**: Kịch bản happy path - AI thất bại lần đầu nhưng thành công ở retry thứ 2.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-13
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (retry success)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Quota: đã tạo 1/3 lượt trong ngày
  - AI Module:
    - Lần 1: thất bại (không trả ảnh hợp lệ)
    - Lần 2: thành công (trả ảnh hợp lệ)
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
      "image_url": "https://...",
      ...
    }
  }
  
  Kiểm tra:
  - Gọi AI 2 lần (retry 1 lần)
  - Tạo generated_flowers và client_histories
  - Trừ quota: quota.count = 2
  
  Lý do: Theo BR-030-06, retry tối đa 2 lần với interval 2 giây. Retry thành công thì vẫn tạo kết quả.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng retry thành công vẫn tạo kết quả và trừ quota.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-06
- **Ghi chú**: Liên kết đến Business Rule về retry.
