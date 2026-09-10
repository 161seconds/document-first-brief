# UT-005-02: Lưu nguyên vẹn Unicode, Markdown và whitespace

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Lưu nguyên vẹn Unicode, Markdown và whitespace
- **Ghi chú**: Kiểm tra quy tắc BR-042: Giữ nguyên vẹn 100% tiếng Việt có dấu, ký tự xuống dòng, thụt lề tab và cú pháp Markdown.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-02
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptController.UpdateSystemPrompt`
- **Loại**: Happy
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập với quyền Admin.
  - System Prompt ID tồn tại và hợp lệ.
- **Các trường hợp cần kiểm tra**:
  - Gửi request PUT chứa chuỗi có thụt lề tab '\t', xuống dòng '\n', emoji và tiêu đề Markdown '# Heading'.
- **Input**:
  ```text
PUT /api/v1/system-prompts/{id}
{
  "content": "  # Hướng dẫn tạo thiệp 🌸\n\t- Bước 1: Chọn mẫu hoa.\n\t- Bước 2: Nhập lời chúc.\n"
}
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 200 OK.
Nội dung lưu trong CSDL và trả về trong response body giữ nguyên vẹn từng ký tự khoảng trắng đầu/cuối dòng, ký tự tab và emoji.
Hệ thống không tự động trim hoặc format lại nội dung bên trong của Admin.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Prompt chỉ dẫn AI phụ thuộc rất lớn vào định dạng cấu trúc Markdown và cách thụt dòng để phân chia ngữ cảnh.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 2.2 Goals & BR-042
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: AC-005 (Bảo toàn định dạng văn bản)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
