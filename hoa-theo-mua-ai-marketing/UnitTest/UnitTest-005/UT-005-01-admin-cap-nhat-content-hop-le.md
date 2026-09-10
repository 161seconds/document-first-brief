# UT-005-01: Admin cập nhật content hợp lệ

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Admin cập nhật content hợp lệ
- **Ghi chú**: Kiểm tra happy path khi Admin cập nhật nội dung System Prompt với chuỗi hợp lệ.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-01
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
  - System Prompt ID 'bd3d8b61-77b4-4896-bf1d-0fdbae881a28' tồn tại, type = 1 (Card), is_deleted = false.
- **Các trường hợp cần kiểm tra**:
  - Gửi request PUT với content hợp lệ (độ dài 50 ký tự).
- **Input**:
  ```text
PUT /api/v1/system-prompts/bd3d8b61-77b4-4896-bf1d-0fdbae881a28
Authorization: Bearer <Admin_Token>
Content-Type: application/json

{
  "content": "Bạn là chuyên gia sáng tạo lời chúc thiệp hoa mừng sinh nhật."
}
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 200 OK.
Response body:
{
  "value": {
    "id": "bd3d8b61-77b4-4896-bf1d-0fdbae881a28",
    "type": 1,
    "content": "Bạn là chuyên gia sáng tạo lời chúc thiệp hoa mừng sinh nhật."
  },
  "isSuccess": true,
  "isFailed": false,
  "error": null
}
Cơ sở dữ liệu lưu chính xác nội dung mới và updated_at được làm mới về thời gian UTC hiện tại.
  ```

## Phân loại và trách nhiệm

- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Đảm bảo tính năng cập nhật cốt lõi hoạt động chính xác khi nhận dữ liệu hợp lệ.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 5.1 Endpoint Cập nhật nội dung System Prompt
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: AC-001 & Main Flow
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
