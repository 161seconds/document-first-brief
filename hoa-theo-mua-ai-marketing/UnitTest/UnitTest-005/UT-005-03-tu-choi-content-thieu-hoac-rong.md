# UT-005-03: Từ chối content thiếu hoặc rỗng

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối content thiếu hoặc rỗng
- **Ghi chú**: Kiểm tra quy tắc BR-041: Chặn cập nhật khi content rỗng hoặc chỉ chứa khoảng trắng.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-03
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
  - Quản trị viên đã xác thực quyền Admin.
  - System Prompt ID tồn tại.
- **Các trường hợp cần kiểm tra**:
  - Trường hợp 1: Request body { "content": "" }
  - Trường hợp 2: Request body { "content": "     " } (chỉ chứa space)
  - Trường hợp 3: Request body không có field content { }
- **Input**:
  ```text
PUT /api/v1/system-prompts/{id}
{
  "content": "   "
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
      "message": "Content không được để trống."
    }
  ]
}
Dữ liệu trong CSDL giữ nguyên, không thay đổi.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: System Prompt rỗng sẽ khiến mô hình AI không nhận được hướng dẫn nghiệp vụ và sinh lỗi runtime.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 5.1 Lỗi Content không hợp lệ (422 Unprocessable Entity)
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: EXC-01 & AC-003 (Chặn lưu khi nội dung trống)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
