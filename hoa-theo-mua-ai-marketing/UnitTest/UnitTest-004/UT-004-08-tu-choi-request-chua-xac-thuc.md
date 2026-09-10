# UT-004-08: Từ chối request chưa xác thực

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối request chưa xác thực
- **Ghi chú**: Kiểm tra bảo mật: Chặn người dùng chưa đăng nhập hoặc không gửi Bearer token.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-08
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
  - Request không có Authorization header hoặc token không hợp lệ / hết hạn.
- **Các trường hợp cần kiểm tra**:
  - Gửi request GET /api/v1/system-prompts không có Authorization header.
- **Input**:
  ```text
GET /api/v1/system-prompts
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống chặn request tại Authorization Middleware.
Hệ thống trả về HTTP 401 Unauthorized.
Không gọi xuống tầng Service hay truy vấn cơ sở dữ liệu.
  ```

## Phân loại và trách nhiệm

- **Suite**: SECURITY
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Ngăn chặn truy cập trái phép vào cấu hình AI prompt của hệ thống.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 3. Architecture & Security
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: Điều kiện Preconditions
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
