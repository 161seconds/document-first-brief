# UT-004-02: Sắp xếp danh sách theo updated_at giảm dần

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Sắp xếp danh sách theo updated_at giảm dần
- **Ghi chú**: Kiểm tra quy tắc nghiệp vụ BR-040: danh sách System Prompt phải sắp xếp giảm dần theo updated_at.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-02
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
  - Quản trị viên đã xác thực quyền Admin.
  - Cơ sở dữ liệu có 3 System Prompt với các mốc thời gian cập nhật khác nhau:
  - Prompt A: id = 'uuid-a', type = 0, updated_at = 2026-09-01T08:00:00Z
  - Prompt B: id = 'uuid-b', type = 1, updated_at = 2026-09-03T10:00:00Z (mới nhất)
  - Prompt C: id = 'uuid-c', type = 2, updated_at = 2026-09-02T12:00:00Z
- **Các trường hợp cần kiểm tra**:
  - Gọi hàm GetSystemPrompt từ SystemPromptService.
  - Kiểm tra thứ tự các phần tử trả về theo giá trị updated_at.
- **Input**:
  ```text
SystemPromptService.GetSystemPromptAsync()
  ```
- **Expected output (bắt buộc)**:
  ```text
Hàm trả về danh sách được sắp xếp chính xác theo thứ tự:
1. Prompt B (updated_at: 2026-09-03T10:00:00Z)
2. Prompt C (updated_at: 2026-09-02T12:00:00Z)
3. Prompt A (updated_at: 2026-09-01T08:00:00Z)
Đáp ứng đúng quy tắc BR-040 (ORDER BY updated_at DESC).
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P1
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Đảm bảo Admin luôn nhìn thấy các Prompt được chỉnh sửa gần đây nhất ở đầu danh sách.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 2.2 Goals & BR-040
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: AC-002 (Thứ tự sắp xếp mặc định theo thời gian cập nhật)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
