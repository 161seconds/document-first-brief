# UT-004-05: Admin lấy chi tiết System Prompt thành công

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Admin lấy chi tiết System Prompt thành công
- **Ghi chú**: Kiểm tra chức năng xem chi tiết đầy đủ nội dung của một System Prompt theo ID.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-05
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptService.GetSystemPrompt`
- **Loại**: Happy
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập với quyền Admin.
  - Bản ghi System Prompt tồn tại trong DB:
  - id = 'bd3d8b61-77b4-4896-bf1d-0fdbae881a28'
  - type = 1 (Card)
  - content = "Bạn là chuyên gia sáng tạo lời chúc thiệp hoa..."
  - is_deleted = false
- **Các trường hợp cần kiểm tra**:
  - Gửi request GET chi tiết theo ID tồn tại.
- **Input**:
  ```text
GET /api/v1/system-prompts/bd3d8b61-77b4-4896-bf1d-0fdbae881a28
Authorization: Bearer <Admin_Valid_Token>
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 200 OK.
Response body:
{
  "value": {
    "id": "bd3d8b61-77b4-4896-bf1d-0fdbae881a28",
    "type": 1,
    "content": "Bạn là chuyên gia sáng tạo lời chúc thiệp hoa..."
  },
  "isSuccess": true,
  "isFailed": false,
  "error": null
}
Toàn bộ nội dung content được trả về nguyên vẹn ở chế độ chỉ xem (Read-only).
  ```

## Phân loại và trách nhiệm

- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Admin cần đọc toàn bộ nội dung chỉ dẫn AI của prompt trước khi quyết định chỉnh sửa.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 5.1 Endpoint Lấy chi tiết System Prompt
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: AC-004 (Xem chi tiết System Prompt)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
