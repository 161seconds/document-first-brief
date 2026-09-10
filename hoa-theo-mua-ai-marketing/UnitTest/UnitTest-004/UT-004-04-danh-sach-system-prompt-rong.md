# UT-004-04: Danh sách System Prompt rỗng

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Danh sách System Prompt rỗng
- **Ghi chú**: Kiểm tra trường hợp biên khi hệ thống chưa có dữ liệu System Prompt nào (Empty State).

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-04
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
  - Quản trị viên đã đăng nhập với quyền Admin.
  - Bảng system_prompts trong cơ sở dữ liệu hoàn toàn rỗng (0 bản ghi).
- **Các trường hợp cần kiểm tra**:
  - Gửi request GET /api/v1/system-prompts khi CSDL rỗng.
- **Input**:
  ```text
GET /api/v1/system-prompts
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống trả về HTTP 200 OK.
Response body:
{
  "value": [],
  "isSuccess": true,
  "isFailed": false,
  "error": null
}
Hệ thống không báo lỗi 404 hoặc 500, trả mảng rỗng để giao diện hiển thị trạng thái Empty State ("Chưa có System Prompt").
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Xử lý mượt mà trạng thái không có dữ liệu mà không làm sập ứng dụng hoặc gây hiểu nhầm cho người dùng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 5.1 Ví dụ Danh sách rỗng
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: ALT-01 & AC-003 (Trạng thái danh sách rỗng)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
