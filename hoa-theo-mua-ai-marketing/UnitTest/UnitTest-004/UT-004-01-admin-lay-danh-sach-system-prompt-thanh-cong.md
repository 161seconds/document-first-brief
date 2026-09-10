# UT-004-01: Admin lấy danh sách System Prompt thành công

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Admin lấy danh sách System Prompt thành công
- **Ghi chú**: Kiểm tra luồng happy path khi Admin truy cập danh sách System Prompt.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-01
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptController.GetSystemPrompt`
- **Loại**: Happy
- **Precondition / Mock setup**:
  - Quản trị viên (Admin) đã đăng nhập vào hệ thống với token JWT hợp lệ.
  - Cơ sở dữ liệu hiện có 3 bản ghi System Prompt với các type khác nhau:
  - Prompt 1: type = 0 (Flower), content = "...", is_deleted = false
  - Prompt 2: type = 1 (Card), content = "...", is_deleted = false
  - Prompt 3: type = 2 (Post), content = "...", is_deleted = false
- **Các trường hợp cần kiểm tra**:
  - Gửi request GET danh sách có kèm Bearer token hợp lệ của Admin.
  - Kiểm tra danh sách trả về đầy đủ các bản ghi với đúng cấu trúc DTO.
- **Input**:
  ```text
GET /api/v1/system-prompts
Authorization: Bearer <Admin_Valid_JWT_Token>
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 200 OK.
Response body:
{
  "value": [
    { "id": "uuid-1", "type": 1 },
    { "id": "uuid-2", "type": 2 },
    { "id": "uuid-3", "type": 0 }
  ],
  "isSuccess": true,
  "isFailed": false,
  "error": null
}
Hệ thống KHÔNG trả trường 'content', 'created_at', 'updated_at' trong List DTO này theo quy định thiết kế nhằm tối ưu hóa tải dữ liệu.
  ```

## Phân loại và trách nhiệm

- **Suite**: SMOKE
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Đây là tính năng cốt lõi giúp Admin có cái nhìn tổng quan về tất cả các System Prompt hiện hành trong hệ thống.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 5.1 Endpoint Lấy danh sách System Prompt
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: AC-001 (Hiển thị danh sách System Prompt)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
