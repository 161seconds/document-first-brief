# UT-005-04: Từ chối content vượt 20.000 ký tự

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối content vượt 20.000 ký tự
- **Ghi chú**: Kiểm tra giá trị biên BR-041: Độ dài tối đa cho phép của content là 20.000 ký tự.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-04
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
  - Gửi request PUT với content có độ dài chính xác 20.001 ký tự.
- **Input**:
  ```text
PUT /api/v1/system-prompts/{id}
{
  "content": "<chuỗi gồm 20.001 ký tự 'a'>"
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
      "message": "Độ dài content không được vượt quá 20.000 ký tự."
    }
  ]
}
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Giới hạn kích thước prompt tránh làm tràn context window của mô hình AI và tăng chi phí token bất hợp lý.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 2.2 Goals & BR-041
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: EXC-01 & AC-001 (Kiểm tra độ dài tối đa)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
