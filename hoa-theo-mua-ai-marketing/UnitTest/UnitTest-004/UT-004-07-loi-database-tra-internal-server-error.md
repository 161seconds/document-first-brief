# UT-004-07: Lỗi database trả INTERNAL_SERVER_ERROR

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Lỗi database trả INTERNAL_SERVER_ERROR
- **Ghi chú**: Kiểm tra xử lý lỗi ngoại lệ hệ thống khi CSDL gặp sự cố mất kết nối hoặc truy vấn thất bại.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-07
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptController.GetSystemPrompt`
- **Loại**: Error
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập với quyền Admin.
  - Cơ sở dữ liệu bị mất kết nối hoặc DbContext throw DbUpdateException / SqlException.
- **Các trường hợp cần kiểm tra**:
  - Mock Service hoặc DbContext throw Exception khi Controller gọi xử lý.
- **Input**:
  ```text
GET /api/v1/system-prompts
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống bắt ngoại lệ và trả về HTTP 500 Internal Server Error.
Response body:
{
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred.",
  "messageCode": "INTERNAL_SERVER_ERROR",
  "errors": null
}
Hệ thống không lộ chuỗi kết nối (connection string) hoặc stack trace nội bộ ra bên ngoài.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Bảo đảm an toàn bảo mật và chuẩn hóa định dạng lỗi hệ thống theo chuẩn RFC-7807.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 5.1 Lỗi hệ thống (500 Internal Server Error)
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: EXC-01, EXC-02 & AC-006, AC-007
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
