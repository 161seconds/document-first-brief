# UT-050-01: Lấy danh sách Mockup active có phân trang

## Thông tin tài liệu
- **Tiêu đề (bắt buộc)**: Lấy danh sách Mockup active có phân trang
- **Ghi chú**: Kịch bản happy path cơ bản - lấy danh sách Mockup active với phân trang mặc định. Không filter theo is_active.

## Metadata quản trị tài liệu
- **Mã tài liệu**: UT-050-01
- **Phiên bản**: v0.1
- **Author (bắt buộc)**: Codex
- **Reviewer**: Chưa chỉ định
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Nhóm Hoa Theo Mùa
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử
- **Module (bắt buộc)**: MOCKUP
- **Unit under test (bắt buộc)**: MockupService.GetMockups(is_active: nullable, page: int, page_size: int)
- **Loại**: Happy
- **Precondition / Mock setup**:
  - User đã đăng nhập với quyền Admin
  - Database có 3 mockups:
    - mockup-001: name="Mockup Sinh Nhật 1", is_active=true, is_deleted=false, created_at=2026-08-27T10:00:00Z
    - mockup-002: name="Mockup Cưới Hỏi 1", is_active=false, is_deleted=false, created_at=2026-08-26T10:00:00Z
    - mockup-003: name="Mockup Tang Lễ 1", is_active=true, is_deleted=false, created_at=2026-08-25T10:00:00Z
  - Không có mockup nào bị soft-delete (is_deleted=true)
- **Input**:
  ```
  GET /api/mockups
  ```
- **Expected output (bắt buộc)**:
  ```
  HTTP 200
  Response body:
  {
    "value": [
      { "id": "mockup-001", "name": "Mockup Sinh Nhật 1", "is_active": true, "is_deleted": false, "created_at": "2026-08-27T10:00:00Z" },
      { "id": "mockup-002", "name": "Mockup Cưới Hỏi 1", "is_active": false, "is_deleted": false, "created_at": "2026-08-26T10:00:00Z" },
      { "id": "mockup-003", "name": "Mockup Tang Lễ 1", "is_active": true, "is_deleted": false, "created_at": "2026-08-25T10:00:00Z" }
    ],
    "total_count": 3,
    "page": 1,
    "page_size": 10
  }
  
  Lý do: Theo BR-050-01, chỉ hiển thị mockups có is_deleted=false. Theo BR-050-03, danh sách sắp xếp theo created_at giảm dần (mới nhất trước). Theo BR-050-04 và BR-050-05, page_size mặc định là 10.
  ```

## Phân loại và trách nhiệm
- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Nhóm Hoa Theo Mùa
- **Rationale (bắt buộc)**: Xác nhận rằng lấy danh sách Mockup không filter trả về tất cả mockups chưa bị xóa, sắp xếp đúng thứ tự và phân trang đúng mặc định.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: BR-050-01, BR-050-03, BR-050-04, BR-050-05
- **Ghi chú**: Liên kết đến Business Rules về soft-delete, sort và pagination mặc định.

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-050
- **Section**: Ví dụ 1 - Happy path: Lấy tất cả Mockup
- **Ghi chú**: Liên kết đến API Contract example.
