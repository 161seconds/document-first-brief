# UT-004-03: Khởi tạo created_at và updated_at cho System Prompt

## Thông tin tài liệu

- **Tiêu đề (bắt buộc)**: Khởi tạo created_at và updated_at cho System Prompt
- **Ghi chú**: Kiểm tra tính toàn vẹn dữ liệu: các trường created_at và updated_at luôn được khởi tạo giá trị hợp lệ trong DB nhưng không bị rò rỉ ra DTO.

## Metadata quản trị tài liệu

- **Mã tài liệu**: UT-004-03
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
  - Hệ thống có bản ghi System Prompt vừa được seed / khởi tạo.
  - Cột created_at có default là now(), updated_at không được null.
- **Các trường hợp cần kiểm tra**:
  - Kiểm tra bản ghi trong DB có đầy đủ created_at và updated_at.
  - Kiểm tra Controller mapping không đưa 2 trường này vào List DTO.
- **Input**:
  ```text
GET /api/v1/system-prompts
  ```
- **Expected output (bắt buộc)**:
  ```text
Entity trong cơ sở dữ liệu có giá trị created_at và updated_at hợp lệ (không null).
Controller trả về List DTO chỉ bao gồm 'id' và 'type'. Cả 2 trường timestamp đều không xuất hiện trong payload response.
  ```

## Phân loại và trách nhiệm

- **Suite**: REGRESSION
- **Priority**: P2
- **Owner**: Phùng Nguyễn Thiên Hào
- **Rationale (bắt buộc)**: Bảo vệ thông tin nội bộ, tuân thủ nguyên tắc thiết kế DTO tối giản của TDD-003.

## TEST_LINKS

**Link 1**
- **Loại**: TDD
- **Mã**: TDD-003
- **Section**: Mục 4.1 Data Dictionary & Data Model
- **Ghi chú**: Quy tắc kỹ thuật trực tiếp kiểm thử trong tài liệu này.

**Link 2**
- **Loại**: Story
- **Mã**: STORY-004
- **Section**: AC-001 (Hiển thị danh sách System Prompt)
- **Ghi chú**: Tiêu chí chấp nhận (Acceptance Criteria) liên quan.
