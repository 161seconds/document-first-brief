# UT-005-05: Từ chối control character không hỗ trợ

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối control character không hỗ trợ
- **Ghi chú**: Kiểm tra quy tắc an toàn BR-042: Chặn các ký tự điều khiển ẩn (invisible control characters) như NUL, BEL, BS.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-05
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptController.UpdateSystemPrompt`
- **Loại**: Error
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập với quyền Admin.
  - System Prompt ID tồn tại.
- **Các trường hợp cần kiểm tra**:
  - Gửi request chứa ký tự điều khiển NUL '\u0000' hoặc BEL '\u0007'.
- **Input**:
  ```text
PUT /api/v1/system-prompts/{id}
{
  "content": "Nội dung hợp lệ\u0000 chứa mã độc"
}
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 422 Unprocessable Entity.
Response body:
{
  "title": "Unprocessable Entity",
  "status": 422,
  "detail": "Nội dung System Prompt không hợp lệ.",
  "messageCode": "VALIDATION_ERROR",
  "errors": [
    {
      "field": "content",
      "message": "Nội dung chứa ký tự điều khiển không được hỗ trợ."
    }
  ]
}
Không cho phép lưu vào cơ sở dữ liệu.
  ```

## Phân loại và trách nhiệm

- **Suite**: SECURITY
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Ngăn chặn tấn công injection, lỗi parsing JSON hoặc gây lỗi tokenizer của mô hình AI.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 2.2 Goals & BR-042
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: AC-001 (Kiểm tra ký tự điều khiển)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
