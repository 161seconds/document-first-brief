# UT-030-17: Không vượt quota khi tranh luột cùng lúc

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không vượt quota khi tranh luột cùng lúc
- **Ghi chú**: Kịch bản concurrency - hai request cùng lúc, chỉ một được thực hiện.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-030-17
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI FLOWER
- **Unit under test (bắt buộc)**: AiFlowerService.CreateAiFlowerAsync (concurrency)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập, tài khoản đang hoạt động
  - Combo và Mockup hợp lệ
  - Quota: đã tạo 2/3 lượt trong ngày
  - Hai request gửi cùng lúc:
    - Request A: tạo thành công (lần thứ 3)
    - Request B: kiểm tra quota → 3/3 → bị chặn
- **Input**:
  ```
  Request A + Request B gửi đồng thời:
  POST /api/ai-flowers
  {
    "product_id": "combo-uuid",
    "mockup_id": "mockup-uuid",
    "user_input": { "name": "Bó hoa sinh nhật" }
  }
  ```
- **Expected output (bắt buộc)**:
  ```
  Request A: HTTP 201 (thành công)
  Request B: HTTP 403 (hết quota)
  
  Kiểm tra database:
  - Request A: quota.count = 3
  - Request B: quota.count vẫn = 3 (không tăng)
  
  Lý do: Theo BR-030-04, kiểm tra quota trước khi gọi AI. Không được vượt quá 3 lượt/ngày.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng quota không bị vượt do race condition.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: BR-030-04
- **Ghi chú**: Liên kết đến Business Rule về quota.
