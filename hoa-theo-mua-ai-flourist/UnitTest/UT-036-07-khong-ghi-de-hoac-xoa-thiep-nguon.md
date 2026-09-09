# UT-036-07: Không ghi đè hoặc xóa thiệp nguồn

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Không ghi đè hoặc xóa thiệp nguồn
- **Ghi chú**: Kịch bản branch - thiệp nguồn phải được giữ nguyên sau khi tạo lại.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-07
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (source card preservation)
- **Loại**: Branch
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại với các field: sender_name="Nguyễn An", image_url="https://old-image..."
  - Quota hợp lệ, AI Module trả ảnh mới
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body chứa:
  {
    "id": "new-card-uuid",
    "image_url": "https://new-image..."
  }
  
  Kiểm tra database:
  - Thiệp nguồn: KHÔNG bị sửa đổi, KHÔNG bị xóa, image_url vẫn là "https://old-image..."
  - Thiệp mới: có id mới, image_url mới
  
  Lý do: Theo BR-036-11, thiệp nguồn KHÔNG bị xóa hoặc ghi đè. Tạo record mới, không cập nhật record cũ.
  ```

## Phân loại và trách nhiệm
- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng thiệp nguồn được giữ nguyên, không bị ghi đè.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-11
- **Ghi chú**: Liên kết đến Business Rule về không ghi đè thiệp nguồn.
