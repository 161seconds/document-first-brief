# UT-036-06: Không nhận dữ liệu chỉnh sửa khi tạo lại

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không nhận dữ liệu chỉnh sửa khi tạo lại
- **Ghi chú**: Kịch bản branch - xác nhận dữ liệu từ thiệp nguồn được giữ nguyên.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-06
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (no edit form)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn có dữ liệu: sender_name="Nguyễn An", receiver_name="Trần Bình", message_content="Chúc mừng", attached_image_url="https://..."
  - User không truyền body request (endpoint không nhận body)
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body chứa:
  {
    "sender_name": "Nguyễn An",
    "receiver_name": "Trần Bình",
    "message_content": "Chúc mừng",
    "attached_image_url": "https://..."
  }
  
  Kiểm tra database:
  - generated_cards: tạo record với dữ liệu từ thiệp nguồn, KHÔNG phải từ request body
  
  Lý do: Theo BR-036-02, hệ thống KHÔNG mở form cho khách hàng chỉnh sửa - dùng nguyên dữ liệu thiệp nguồn.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng dữ liệu thiệp nguồn được giữ nguyên, không có form chỉnh sửa.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-02
- **Ghi chú**: Liên kết đến Business Rule về không mở form chỉnh sửa.
