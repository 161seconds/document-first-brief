# UT-005-07: Prompt không tồn tại trả NOT_FOUND

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Prompt không tồn tại trả NOT_FOUND
- **Ghi chú**: Kiểm tra xử lý lỗi khi cập nhật System Prompt không tồn tại hoặc đã bị xóa mềm.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-07
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptService.UpdateAsync`
- **Loại**: Error
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập quyền Admin.
  - ID '00000000-0000-0000-0000-000000000999' không tồn tại trong CSDL hoặc có is_deleted = true.
- **Các trường hợp cần kiểm tra**:
  - Gửi request PUT cập nhật cho ID không tồn tại.
- **Input**:
  ```text
PUT /api/v1/system-prompts/00000000-0000-0000-0000-000000000999
{
  "content": "Nội dung mới hợp lệ"
}
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 404 Not Found.
Response body:
{
  "title": "Not Found",
  "status": 404,
  "detail": "System Prompt không tồn tại",
  "messageCode": "NOT_FOUND",
  "errors": null
}
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Thông báo rõ ràng khi tài nguyên không còn tồn tại để Admin quay lại danh sách.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 5.1 Prompt không tồn tại (404 Not Found)
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: EXC-03
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
