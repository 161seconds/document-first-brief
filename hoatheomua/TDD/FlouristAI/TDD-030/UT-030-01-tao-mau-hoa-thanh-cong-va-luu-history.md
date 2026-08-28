# UT-030-01: Tạo mẫu hoa thành công và lưu history

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Tạo mẫu hoa thành công và lưu history
- **Ghi chú**: Kịch bản happy path - tạo ảnh hoa AI thành công, gắn logo, lưu history và trừ quota.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo tồn tại và khả dụng (product_id hợp lệ)
  - Mockup tồn tại, is_active=true, is_deleted=false
  - Quota: đã tạo 0/3 lượt trong ngày
  - AI Module trả ảnh hợp lệ
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "550e8400-e29b-41d4-a716-446655440001",
    "mockup_id": "550e8400-e29b-41d4-a716-446655440002",
    "user_input": {
      "name": "Bó hoa sinh nhật mẹ",
      "occasion": "sinh_nhat",
      "style": "dang_cap",
      "budget": 500000,
      "note": "Ưa thích hoa hồng và hoa lan"
    }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 201
  Response body:
  {
    "value": {
      "id": "generated-flower-uuid",
      "image_url": "https://storage.example.com/flowers/xxx.png",
      "user_id": "user-uuid",
      "created_at": "<current-timestamp>"
    }
  }
  
  Kiểm tra database:
  - Tạo generated_flowers record mới với image_url (đã gắn logo)
  - Tạo client_histories record với type="flower", input=JSON request, metadata=Mockup snapshot
  - Trừ quota: quota.count = 1
  - Không tạo flower_requests hoặc flower_ai_jobs
  
  Lý do: Theo BR-030-05 (prompt priority), BR-030-07 (trừ quota sau thành công), BR-030-08 (gắn logo), BR-030-09 (lưu history).
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng tạo hoa AI thành công tạo generated_flowers, client_histories và trừ quota.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-05, BR-030-07, BR-030-08, BR-030-09
- **Ghi chú**: Liên kết đến Business Rules về tạo hoa thành công.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Ví dụ 1 - Tạo mẫu hoa thành công
- **Ghi chú**: Liên kết đến API Contract example.
