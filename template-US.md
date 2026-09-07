<!-- Mỗi file chứa một tài liệu. Thay mã STORY-001 và nội dung ví dụ; giữ nguyên heading và nhãn in đậm.
Priority: Must / Should / Could / Won't. Status: Todo / In Progress / Blocked / Done.
Tham chiếu dùng mã tài liệu, có thể thêm /section và : ghi chú. Xoá dòng tham chiếu không dùng.
Creator và Assignee chỉ là tên trong Markdown; phân công tài khoản và phê duyệt thực hiện trên giao diện sau import.
Sơ đồ thuộc TDD. Các trường quản trị, giả định và câu hỏi mở bổ sung trên giao diện.
-->

# STORY-001

## Metadata

- **Story**: Là một người dùng, tôi muốn [chức năng] để [lợi ích].
- **Context**: [Bối cảnh và vấn đề cần giải quyết]
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: [Tên người tạo]
- **Assignee**:
  - Backend: [Tên người phụ trách]
  - QA: [Tên người kiểm thử]

## Conditions

### Preconditions

- [Điều kiện cần có trước khi bắt đầu]

### Trigger

[Sự kiện khởi động luồng]

## Flow

### Main Flow

1. [Người dùng thực hiện thao tác]
2. [Hệ thống kiểm tra và xử lý]
3. [Hệ thống trả kết quả]

### Alternative Flow

#### ALT-01

[Điều kiện chuyển sang luồng thay thế]

1. [Bước xử lý thay thế]
2. [Điểm quay lại luồng chính hoặc kết thúc]

### Exception Flow

#### EXC-01

[Điều kiện xảy ra lỗi]

1. [Hệ thống thông báo lỗi và cách khôi phục]

## Acceptance Criteria

#### AC-001

- **Given**: [Bối cảnh và điều kiện ban đầu]
- **When**: [Thao tác hoặc sự kiện]
- **Then**: [Kết quả quan sát, kiểm thử được]
- **And**: [Điều kiện bổ sung]

## References

### TDDs

- TDD-001

### Rules

- BR-001

### Dependencies

## Non-Functional

- [Yêu cầu hiệu năng, bảo mật hoặc khả năng truy cập đo được]

## Out of Scope

- [Nội dung không triển khai trong Story này]
