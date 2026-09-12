# UT-036-11: Cho phép tạo lại ở ngày quota mới

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Cho phép tạo lại ở ngày quota mới
- **Ghi chú**: Kịch bản boundary - quota reset theo ngày, cho phép tạo lại vào ngày mới.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-036-11
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: AI CARD
- **Unit under test (bắt buộc)**: AICardService.RegenerateAiCardAsync (quota reset)
- **Loại**: Boundary
- **Precondition / Mock setup**:
  - User đã đăng nhập
  - Thiệp nguồn tồn tại
  - Hôm qua: đã tạo 3/3 lượt mẫu hoa (hết quota mẫu hoa ngày hôm qua)
  - Hôm nay: quota reset về 0/3 mẫu hoa
  - Quota tạo thiệp ngày hôm nay: 0/10
- **Input**:
  ```
  POST /api/ai-cards/550e8400-e29b-41d4-a716-446655440001/regenerate
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": {
      "id": "new-card-uuid"
    }
  }
  
  Kiểm tra database:
  - Quota tạo thiệp hôm nay: count = 1/10
  - Quota mẫu hoa hôm nay: count = 1/3
  - Quota mẫu hoa ngày hôm qua: vẫn là 3/3 (không thay đổi)
  
  Lý do: Quota reset theo ngày. Hôm nay được phép tạo lại bất kể quota ngày hôm qua.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng quota reset theo ngày và cho phép tạo lại vào ngày mới.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-036
- **Section**: BR-036-07
- **Ghi chú**: Liên kết đến Business Rule về quota 3 lượt/mẫu hoa/ngày.
