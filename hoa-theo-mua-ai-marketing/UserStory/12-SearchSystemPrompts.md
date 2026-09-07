# STORY-012: Tìm kiếm System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn tìm kiếm System Prompt bằng từ khóa để có thể nhanh chóng tìm được Prompt cần kiểm tra hoặc quản lý mà không phải xem toàn bộ danh sách
- **Context**: Khi hệ thống có nhiều System Prompt thuộc các chức năng khác nhau, Quản trị viên cần có khả năng tìm kiếm nhanh System Prompt cần thao tác theo tên hoặc mã định danh. System Prompt được quản lý theo từng chức năng; khi chỉnh sửa, hệ thống tạo phiên bản mới và chỉ sử dụng phiên bản hiện tại, không sử dụng các phiên bản trong lịch sử để xử lý.
- **Sprint**: S1
- **Priority**: Won't
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đã duyệt
- **Cập nhật**: 04/09/2026
- **Author**: Nguyễn Anh Quân
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Nguyễn Đức Bình
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Story này Won't hả?"* — Nguyễn Đức Bình · 09:05 03/09/2026

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin với quyền hạn phù hợp.
- **Trigger**: 
  - Quản trị viên nhập nội dung từ khóa vào ô "Tìm kiếm System Prompt".

---

## Flow

### Main Flow
1. Quản trị viên đang ở màn hình danh sách System Prompt.
2. Hệ thống hiển thị ô tìm kiếm tại vị trí đầu bảng.
3. Quản trị viên nhập từ khóa cần tìm (theo tên hoặc mã/ID của Prompt).
4. Quản trị viên thực hiện tìm kiếm (nhấn Enter hoặc hệ thống debounce tự động).
5. Hệ thống truy vấn các System Prompt có mã hoặc tên phù hợp với từ khóa (tìm kiếm không phân biệt hoa thường).
6. Hệ thống trả về danh sách các kết quả phù hợp.
7. Hệ thống hiển thị tổng số kết quả tìm được.
8. Quản trị viên xem danh sách kết quả.

### Alternative Flow
- **ALT-01 — Không có kết quả phù hợp**:
  - Tại bước 5, không có System Prompt nào khớp với từ khóa đã nhập.
  - Hệ thống trả về danh sách rỗng.
  - Hệ thống hiển thị thông báo: *"Không tìm thấy System Prompt phù hợp"*.
- **ALT-02 — Xóa từ khóa tìm kiếm**:
  - Quản trị viên xóa toàn bộ từ khóa trong ô tìm kiếm (hoặc nhấn biểu tượng xóa nhanh).
  - Hệ thống tự động tải lại và hiển thị toàn bộ danh sách System Prompt mặc định.

### Exception Flow
- **EXC-01 — Lỗi truy vấn tìm kiếm**:
  - Xảy ra lỗi kết nối máy chủ khi thực hiện tìm kiếm.
  - Hệ thống thông báo: *"Không thể thực hiện tìm kiếm, vui lòng thử lại"* và cho phép Quản trị viên thao tác lại.

---

## Acceptance Criteria

- **AC-001 — Không tìm thấy kết quả phù hợp (ALT-01)**:
  - **Given**: Quản trị viên nhập một từ khóa không khớp với bất kỳ System Prompt nào trong hệ thống.
  - **When**: Hệ thống hoàn tất truy vấn tìm kiếm.
  - **Then**: Hệ thống hiển thị danh sách rỗng.
  - **And**: Hiển thị thông báo: *"Không tìm thấy System Prompt phù hợp"*.

- **AC-002 — Tìm kiếm theo mã hoặc một phần mã Prompt**:
  - **Given**: Hệ thống có tồn tại System Prompt với mã tương ứng.
  - **When**: Quản trị viên nhập mã đầy đủ hoặc một phần mã vào ô tìm kiếm.
  - **Then**: Hệ thống trả về danh sách các System Prompt có mã khớp với chuỗi tìm kiếm.

- **AC-003 — Tìm kiếm thành công theo từ khóa**:
  - **Given**: Quản trị viên nhập từ khóa tìm kiếm phù hợp với một hoặc nhiều System Prompt.
  - **When**: Quản trị viên thực hiện tìm kiếm.
  - **Then**: Hệ thống hiển thị danh sách System Prompt phù hợp với từ khóa tìm kiếm.
  - **And**: Hiển thị tổng số lượng kết quả tìm được.

- **AC-004 — Khôi phục danh sách khi xóa từ khóa (ALT-02)**:
  - **Given**: Bảng dữ liệu đang hiển thị kết quả tìm kiếm theo từ khóa.
  - **When**: Quản trị viên xóa toàn bộ nội dung trong ô tìm kiếm.
  - **Then**: Hệ thống hiển thị lại toàn bộ danh sách System Prompt ban đầu theo thứ tự sắp xếp mặc định.

---

## Non-Functional
- **Tốc độ phản hồi**: Thời gian trả kết quả tìm kiếm dưới 1 giây trong điều kiện tải bình thường.
- **Tính tiện dụng**: Hỗ trợ tìm kiếm không phân biệt hoa thường (Case-insensitive).

---

## Out of Scope
- Không bao gồm các thao tác lọc nâng cao theo trạng thái, chỉnh sửa, xóa, hoặc tạo mới System Prompt.
