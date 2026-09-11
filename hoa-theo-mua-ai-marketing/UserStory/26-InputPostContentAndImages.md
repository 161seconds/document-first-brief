# STORY-026: Nhập nội dung và hình ảnh cho bài đăng

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn nhập nội dung gốc, hashtag và hình ảnh cho bài đăng để chuẩn bị dữ liệu dùng chung trước khi tạo nội dung riêng cho từng nền tảng.
- **Context**: Quản trị viên cần chuẩn bị một bài đăng đa nền tảng từ cùng một nguồn dữ liệu. Màn hình tạo bài đăng cho phép nhập tiêu đề, nội dung, hashtag và tải ảnh trước khi chuyển sang bước cấu hình riêng cho Facebook, Instagram hoặc Zalo OA.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 11/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**:
  - FE: Minh Nguyễn
  - BE: Tân Trần
- **Creator**: Hồ Hoàng Nam
- **Thống kê tài liệu**: Rules: 10 | Unit Tests: 0 | System Tests: 0

---

## Conditions

### Preconditions
- Quản trị viên đã đăng nhập.
- Quản trị viên có quyền sử dụng chức năng Tạo bài đăng.

### Trigger
- Quản trị viên chọn chức năng “Tạo bài đăng”.

---

## Flow

### Main Flow — Nhập dữ liệu dùng chung cho bài đăng
1. Quản trị viên mở màn hình tạo bài đăng mới.
2. Hệ thống hiển thị các trường Tiêu đề, Nội dung, Hashtag và khu vực Hình ảnh.
3. Quản trị viên nhập tiêu đề và nội dung.
4. Quản trị viên nhập hashtag.
5. Quản trị viên chọn hoặc kéo thả một hay nhiều ảnh vào khu vực tải ảnh.
6. Hệ thống kiểm tra định dạng và dung lượng của từng ảnh.
7. Hệ thống hiển thị danh sách ảnh hợp lệ đã được chọn.
8. Quản trị viên tiếp tục sang bước chọn và cấu hình nền tảng.

### Alternative Flow

#### ALT-01 — Quản trị viên tạo bài đăng không có hình ảnh
- Quản trị viên nhập đầy đủ tiêu đề và nội dung nhưng không tải ảnh.
- Hệ thống cho phép tiếp tục cấu hình nền tảng.

#### ALT-02 — Quản trị viên thay đổi danh sách ảnh trước khi tiếp tục
- Quản trị viên thêm ảnh mới hoặc xóa một ảnh đã chọn.
- Hệ thống cập nhật danh sách ảnh nguồn hiện tại.
- Quản trị viên tiếp tục từ bước 8 của Luồng chính.

#### ALT-03 — Quản trị viên đặt lại biểu mẫu
- Quản trị viên chọn “Đặt lại”.
- Hệ thống xóa dữ liệu đang nhập và danh sách ảnh đang chọn khỏi biểu mẫu.
- Hệ thống hiển thị lại biểu mẫu trống.

#### ALT-04 — Quản trị viên tạo bài đăng không có content
- Quản trị viên tải đầy đủ ảnh nhưng không có content.
- Hệ thống cho phép tiếp tục cấu hình nền tảng.

### Exception Flow

#### EXC-02 — Ảnh được chọn không đúng định dạng hoặc vượt quá 10 MB
- Hệ thống không thêm ảnh không hợp lệ vào danh sách ảnh nguồn.
- Hệ thống thông báo điều kiện ảnh không hợp lệ.
- Quản trị viên có thể chọn ảnh khác.

---

## Acceptance Criteria

- **AC-001 — Hiển thị biểu mẫu tạo bài đăng**:
  - **Given**: Quản trị viên đã đăng nhập và có quyền tạo bài đăng.
  - **When**: Quản trị viên mở chức năng “Tạo bài đăng”.
  - **Then**: Hệ thống hiển thị các trường Tiêu đề, Nội dung, Hashtag và khu vực tải ảnh.

- **AC-002 — Cho phép tạo bài đăng không có ảnh nguồn**:
  - **Given**: Quản trị viên đã nhập tiêu đề và nội dung hợp lệ.
  - **When**: Quản trị viên không tải hình ảnh và tiếp tục.
  - **Then**: Hệ thống cho phép chuyển sang bước cấu hình nền tảng.
  - **And**: Bài đăng được ghi nhận là không có ảnh nguồn.

- **AC-003 — Tải lên ảnh hợp lệ**:
  - **Given**: Quản trị viên đang tạo bài đăng.
  - **When**: Quản trị viên chọn một hoặc nhiều ảnh PNG, JPG hoặc WEBP không vượt quá 10 MB mỗi ảnh.
  - **Then**: Hệ thống thêm các ảnh hợp lệ vào danh sách ảnh nguồn.
  - **And**: Mỗi ảnh hiển thị tên và thông tin dung lượng.

- **AC-004 — Chặn ảnh không đúng định dạng hoặc quá dung lượng**:
  - **Given**: Quản trị viên chọn ảnh không đúng định dạng hoặc vượt quá 10 MB.
  - **When**: Hệ thống kiểm tra ảnh.
  - **Then**: Hệ thống không thêm ảnh không hợp lệ vào danh sách.
  - **And**: Hệ thống thông báo rõ điều kiện không hợp lệ.

- **AC-005 — Đặt lại biểu mẫu về ban đầu**:
  - **Given**: Biểu mẫu đang có dữ liệu.
  - **When**: Quản trị viên chọn “Đặt lại”.
  - **Then**: Hệ thống đưa biểu mẫu về trạng thái ban đầu.
  - **And**: Dữ liệu chưa được gửi để tạo nội dung theo nền tảng.

- **AC-006 — Cho phép tạo bài đăng không có content**:
  - **Given**: Quản trị viên đã nhập tiêu đề và nội dung hợp lệ.
  - **When**: Quản trị viên không nhập content và tiếp tục.
  - **Then**: Hệ thống cho phép chuyển sang bước cấu hình nền tảng.
  - **And**: Bài đăng được ghi nhận là không có content.

---

## References

### Rules
- [BR-001: Định dạng file ảnh đính kèm bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-001.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f47d6261-cae6-4ed8-b879-249b26d17464))
- [BR-018: Định dạng bắt đầu của Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-018.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8b93f9ae-a327-48be-8e1d-78f40bdd9c23))
- [BR-019: Không chứa khoảng trắng trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-019.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/2dd59a4b-f4ed-489b-8788-ab3684cd7da4))
- [BR-020: Độ dài cho phép của một Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-020.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8c0f117b-25fe-4070-9e3f-682a955ddd4c))
- [BR-021: Ký tự hợp lệ trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-021.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/af6d57ef-0350-4009-a454-7f8f04f37eaf))
- [BR-022: Loại bỏ Hashtag trùng lặp](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-022.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1b8d95f0-954e-48d3-8a45-fee5ddcfe830))
- [BR-025: Giới hạn độ dài nội dung Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-025.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/421d8701-2950-41f8-908f-4a6838c5b7cb))
- [BR-032: Định dạng file ảnh gốc (Core Image)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-032.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/422cb56a-ad99-48c9-8598-7d518003905c))
- [BR-033: Dung lượng ảnh gốc tối đa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-033.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a35a33f1-0c45-4939-97fd-6c9b231db023))
- [BR-046: Nội dung hợp lệ của lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-046.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a90db8e1-0a6f-4bac-90d1-637449f6c024))

---

## Non-Functional

- Nội dung đang nhập phải được giữ nguyên khi Quản trị viên thêm hoặc xóa ảnh.
- Chức năng tải ảnh phải hỗ trợ chọn tệp và kéo thả bằng giao diện.

---

## Out of Scope

- Không tạo content hoặc hình ảnh bằng AI trong Story này.
- Không đăng ngay hoặc lên lịch đăng bài trong Story này.
- Không quản lý danh sách bài đăng đã tạo.
