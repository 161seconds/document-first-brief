# UT-005-08: Từ chối request không được cấp quyền

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối request không được cấp quyền
- **Ghi chú**: Kiểm tra phân quyền: Chặn bất kỳ request cập nhật nào không xuất phát từ tài khoản Quản trị viên.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-08
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
  - Request gửi từ user không có Role Admin (hoặc chưa đăng nhập / token hết hạn).
- **Các trường hợp cần kiểm tra**:
  - Gửi PUT /api/v1/system-prompts/{id} với token không phải Admin.
- **Input**:
  ```text
PUT /api/v1/system-prompts/{id}
Authorization: Bearer <Non_Admin_Token>
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống từ chối yêu cầu và trả về HTTP 403 Forbidden.
Tầng Service chặn thực thi hoặc Controller chặn ngay từ tầng Policy.
Cơ sở dữ liệu hoàn toàn không bị thay đổi.
  ```

## Phân loại và trách nhiệm

- **Suite**: SECURITY
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Bảo vệ chặt chẽ các chỉ thị AI cốt lõi, chỉ duy nhất Quản trị viên hệ thống mới có quyền điều chỉnh.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 3. Sequence Diagram & Activity Diagram
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: AC-010 (Từ chối quyền truy cập)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
