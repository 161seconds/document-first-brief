# STORY-013: Lọc System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn lọc danh sách System Prompt theo các tiêu chí: chức năng áp dụng, trạng thái và thời gian cập nhật để có thể thu hẹp danh sách theo các thuộc tính đã được hệ thống quản lý
- **Context**: Khi cần kiểm tra một nhóm Prompt cụ thể, việc xem toàn bộ danh sách gây mất thời gian. Vì vậy, Quản trị viên cần chức năng lọc để thu hẹp danh sách theo các thuộc tính đã được hệ thống quản lý (chức năng nghiệp vụ, trạng thái kích hoạt, khoảng thời gian cập nhật).
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
  - Quản trị viên mở khu vực Bộ lọc trên màn hình danh sách System Prompt.

---

## Flow

### Main Flow
1. Quản trị viên truy cập màn hình danh sách System Prompt.
2. Quản trị viên mở khu vực bộ lọc.
3. Hệ thống hiển thị các tiêu chí lọc:
   - Chức năng áp dụng (Tạo bài viết, Paraphrase, Gợi ý slogan, v.v.).
   - Trạng thái (Đang sử dụng, Không sử dụng).
   - Khoảng thời gian cập nhật (Từ ngày - Đến ngày).
4. Quản trị viên lựa chọn một hoặc nhiều điều kiện lọc.
5. Quản trị viên nhấn "Áp dụng bộ lọc".
6. Hệ thống kiểm tra và hợp thức hóa các điều kiện lọc đã chọn.
7. Hệ thống truy vấn và hiển thị danh sách System Prompt thỏa mãn điều kiện.
8. Hệ thống hiển thị tổng số kết quả sau khi lọc.

### Alternative Flow
- **ALT-01 — Không có System Prompt phù hợp**:
  - Tại bước 7, không có System Prompt nào thỏa mãn đồng thời các tiêu chí lọc.
  - Hệ thống hiển thị danh sách rỗng kèm thông báo: *"Không có System Prompt phù hợp với bộ lọc"*.
- **ALT-02 — Xóa toàn bộ bộ lọc**:
  - Quản trị viên nhấn nút "Xóa bộ lọc" (Reset filter).
  - Hệ thống xóa bỏ các tiêu chí đã chọn và hoàn trả danh sách System Prompt đầy đủ ban đầu.

### Exception Flow
- **EXC-01 — Lỗi tải dữ liệu lọc**:
  - Máy chủ hoặc cơ sở dữ liệu gặp sự cố khi áp dụng bộ lọc.
  - Hệ thống thông báo lỗi: *"Không thể áp dụng bộ lọc, vui lòng thử lại"* và giữ nguyên danh sách trước đó.

---

## Acceptance Criteria

- **AC-001 — Lọc theo chức năng áp dụng**:
  - **Given**: Danh sách đang có các System Prompt thuộc nhiều chức năng khác nhau.
  - **When**: Quản trị viên chọn một chức năng áp dụng cụ thể và thực hiện lọc.
  - **Then**: Hệ thống chỉ hiển thị các System Prompt thuộc chức năng đã chọn.

- **AC-002 — Lọc kết hợp nhiều tiêu chí thành công**:
  - **Given**: Danh sách có các System Prompt thuộc nhiều chức năng, trạng thái hoặc thời gian cập nhật khác nhau.
  - **When**: Quản trị viên chọn đồng thời nhiều điều kiện lọc (chức năng, trạng thái, thời gian) và bấm áp dụng.
  - **Then**: Hệ thống trả về danh sách System Prompt thỏa mãn đồng thời tất cả các điều kiện lọc.
  - **And**: Hiển thị chính xác tổng số kết quả lọc được.

- **AC-003 — Không có kết quả phù hợp (ALT-01)**:
  - **Given**: Quản trị viên áp dụng bộ lọc với các tiêu chí không khớp với bất kỳ System Prompt nào.
  - **When**: Hệ thống hoàn tất truy vấn.
  - **Then**: Hệ thống hiển thị trạng thái danh sách rỗng.
  - **And**: Thông báo: *"Không có System Prompt phù hợp với bộ lọc"*.

- **AC-004 — Xóa bộ lọc (ALT-02)**:
  - **Given**: Danh sách đang được lọc theo các tiêu chí đã chọn.
  - **When**: Quản trị viên chọn "Xóa bộ lọc".
  - **Then**: Hệ thống hủy bỏ tất cả điều kiện lọc và hiển thị lại toàn bộ danh sách mặc định.

---

## Non-Functional
- **Tốc độ phản hồi**: Thời gian lọc và hiển thị danh sách dưới 1 giây trong điều kiện bình thường.

---

## Out of Scope
- Không bao gồm tìm kiếm từ khóa tự do, chỉnh sửa, xóa hoặc tạo mới System Prompt.
