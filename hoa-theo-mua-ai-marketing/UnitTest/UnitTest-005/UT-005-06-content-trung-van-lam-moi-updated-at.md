# UT-005-06: Content trùng vẫn làm mới updated_at

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Content trùng vẫn làm mới updated_at
- **Ghi chú**: Kiểm tra trường hợp Admin cập nhật lại đúng nội dung hiện tại: hệ thống vẫn ghi nhận thời điểm updated_at mới.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-005-06
- **Phiên bản**: v0
- **Author (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner (bắt buộc)**: Phùng Nguyễn Thiên Hào
- **Cập nhật gần nhất**: 2026-09-09

## Đơn vị kiểm thử

- **Module (bắt buộc)**: SYSTEM_PROMPT - Quản lý System Prompt
- **Unit under test (bắt buộc)**: `SystemPromptService.UpdateAsync`
- **Loại**: Happy
- **Precondition / Mock setup**:
  - Quản trị viên đã đăng nhập quyền Admin.
  - System Prompt có content = "Prompt hiện tại", updated_at ban đầu là T0 (ví dụ 2026-09-01T00:00:00Z).
- **Các trường hợp cần kiểm tra**:
  - Gửi request PUT với content giữ nguyên y hệt "Prompt hiện tại".
- **Input**:
  ```text
SystemPromptService.UpdateAsync(id, "Prompt hiện tại")
  ```
- **Expected output (bắt buộc)**:
  ```text
Hàm thực thi thành công và trả về Entity đã cập nhật.
Giá trị updated_at trong CSDL được cập nhật thành mốc thời gian mới T1 (T1 > T0).
Không từ chối request vì lý do nội dung không đổi.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Ghi nhận thời điểm Admin kiểm tra và xác nhận lại Prompt đang dùng.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-004
- **Section**: Mục 4.1 Data Dictionary & Cập nhật updated_at
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-005
- **Section**: AC-002 (Lưu vết kiểm toán)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
