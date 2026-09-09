# UT-030-09: Ghép prompt theo thứ tự ưu tiên

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Ghép prompt theo thứ tự ưu tiên
- **Ghi chú**: Kịch bản happy path - prompt ưu tiên Combo > Mockup > Ghi chú/Yêu cầu thêm > Phong cách.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-09
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (prompt building)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo có thông tin mô tả
  - Mockup có thông tin mô tả
  - User input có note và style
  - System prompt tồn tại với type="flower"
- **Input**:
  ```
  POST /api/ai-flowers
  {
    "product_id": "combo-uuid",
    "mockup_id": "mockup-uuid",
    "user_input": {
      "name": "Bó hoa sinh nhật",
      "occasion": "sinh_nhat",
      "style": "dang_cap",
      "note": "Ưa thích hoa hồng"
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
      ...
    }
  }
  
  Kiểm tra:
  - Prompt được ghép đúng thứ tự ưu tiên: Combo + Mockup + note + style
  - AI nhận được prompt đầy đủ thông tin
  
  Lý do: Theo BR-030-05, prompt ưu tiên theo thứ tự Combo > Mockup > Ghi chú/Yêu cầu thêm > Phong cách.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng prompt được ghép đúng thứ tự ưu tiên.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-05
- **Ghi chú**: Liên kết đến Business Rule về prompt priority.
