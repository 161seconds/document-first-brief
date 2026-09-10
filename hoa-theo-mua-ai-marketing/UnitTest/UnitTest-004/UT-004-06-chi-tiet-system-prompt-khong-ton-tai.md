# UT-004-06: Chi tiết System Prompt không tồn tại

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Chi tiết System Prompt không tồn tại
- **Ghi chú**: Kiểm tra xử lý lỗi khi Admin xem một System Prompt với ID không có trong CSDL hoặc đã bị xóa.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-06
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
  - ID '00000000-0000-0000-0000-000000000999' không tồn tại trong bảng system_prompts.
- **Các trường hợp cần kiểm tra**:
  - Gửi request GET /api/v1/system-prompts/00000000-0000-0000-0000-000000000999
- **Input**:
  ```text
GET /api/v1/system-prompts/00000000-0000-0000-0000-000000000999
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 404 Not Found.
Response body:
{
  "title": "Not Found",
  "status": 404,
  "detail": "System Prompt không còn tồn tại",
  "messageCode": "NOT_FOUND",
  "errors": {
    "detail": "System Prompt không còn tồn tại"
  }
}
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Xử lý trường hợp prompt đã bị xóa hoặc link trực tiếp sai ID mà không làm sập ứng dụng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 5.1 Phản hồi Không tìm thấy Prompt (404 Not Found)
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: EXC-03 & AC-009 (System Prompt không còn tồn tại)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
