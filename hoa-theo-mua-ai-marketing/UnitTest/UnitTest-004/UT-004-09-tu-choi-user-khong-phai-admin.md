# UT-004-09: Từ chối user không phải Admin

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Từ chối user không phải Admin
- **Ghi chú**: Kiểm tra phân quyền: Chặn người dùng có tài khoản hợp lệ nhưng không mang quyền Admin (vd: Customer, Staff).

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-09
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
  - Người dùng đăng nhập tài khoản khách hàng (Role = Customer).
  - Token JWT hợp lệ nhưng không có claim/role Admin.
- **Các trường hợp cần kiểm tra**:
  - Gửi request kèm Bearer token của Customer.
- **Input**:
  ```text
GET /api/v1/system-prompts
Authorization: Bearer <Customer_Token>
  ```
- **Expected output (bắt buộc)**:
  ```text
Hệ thống từ chối yêu cầu và trả về HTTP 403 Forbidden.
Người dùng không có quyền truy cập hoặc xem danh sách/chi tiết System Prompt.
  ```

## Phân loại và trách nhiệm

- **Suite**: SECURITY
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Đảm bảo chỉ có Quản trị viên mới được phép xem các chỉ thị Prompt nội bộ của hệ thống AI.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 3. Sequence Diagram (Xác thực quyền Admin)
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: AC-010 (Từ chối quyền truy cập)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
