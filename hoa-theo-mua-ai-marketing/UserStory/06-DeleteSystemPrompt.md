# STORY-006: Xóa System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa System Prompt không còn sử dụng để danh sách Prompt được quản lý gọn gàng và tránh sử dụng nhầm các Prompt không còn phù hợp
- **Context**: Trong trường hợp hệ thống không còn nhu cầu sử dụng một System Prompt do Prompt đã được thay thế hoặc yêu cầu nghiệp vụ tương ứng không còn tồn tại, Quản trị viên có thể thực hiện xóa System Prompt khỏi danh sách quản lý.
- **Sprint**: S1
- **Priority**: Won't
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đã duyệt
- **Cập nhật**: 04/09/2026
- **Author**: Hồ Hoàng Nam
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
  - Quản trị viên đã đăng nhập vào trang admin với quyền quản trị phù hợp.
- **Trigger**: 
  - Quản trị viên nhấn nút "Xóa" tại một System Prompt cụ thể.

---

## Flow

### Main Flow
1. Quản trị viên chọn System Prompt muốn xóa từ danh sách quản lý.
2. Quản trị viên nhấn nút "Xóa".
3. Hệ thống kiểm tra System Prompt có đang được cấu hình cho chức năng AI nào đang hoạt động hay không để xác định quyền được phép xóa.
4. Nếu Prompt đáp ứng điều kiện xóa, hệ thống hiển thị pop-up xác nhận xóa.
5. Pop-up hiển thị tên/thông tin System Prompt và cảnh báo về thao tác xóa (thao tác không thể hoàn tác).
6. Quản trị viên chọn "Xác nhận xóa".
7. Hệ thống thực hiện xóa System Prompt khỏi hệ thống.
8. Hệ thống cập nhật lại danh sách System Prompt.
9. Hệ thống hiển thị thông báo: *"Xóa System Prompt thành công"*.

### Alternative Flow
- **ALT-01 — Hủy xóa**:
  - Quản trị viên nhấn nút "Xóa", pop-up xác nhận hiển thị.
  - Quản trị viên chọn "Hủy".
  - Hệ thống đóng pop-up và giữ nguyên toàn bộ dữ liệu.
- **ALT-02 — Đóng pop-up xác nhận**:
  - Quản trị viên nhấn nút đóng (biểu tượng X) hoặc click ra ngoài vùng pop-up nếu giao diện cho phép.
  - Hệ thống ghi nhận đây là thao tác hủy bỏ, đóng pop-up và giữ nguyên System Prompt.

### Exception Flow
- **EXC-01 — Lỗi hệ thống khi xóa**:
  - Xảy ra sự cố API/máy chủ/cơ sở dữ liệu trong quá trình thực thi xóa.
  - Hệ thống rollback, không ghi nhận thao tác xóa là thành công.
  - Hệ thống thông báo lỗi: *"Không thể xóa System Prompt, vui lòng thử lại"*.
- **EXC-02 — System Prompt đang được sử dụng**:
  - Tại bước 3, nếu System Prompt đang được cấu hình làm prompt mặc định cho một chức năng AI.
  - Hệ thống chặn thao tác xóa, không hiển thị pop-up xác nhận và hiển thị thông báo lý do không thể xóa.

---

## Acceptance Criteria

- **AC-001 — Hiển thị pop-up xác nhận xóa**:
  - **Given**: Quản trị viên đang xem một System Prompt có thể xóa được.
  - **When**: Quản trị viên chọn nút "Xóa".
  - **Then**: Hệ thống phải hiển thị pop-up cảnh báo xác nhận trước khi thực hiện xóa.

- **AC-002 — Hủy thao tác xóa (ALT-01, ALT-02)**:
  - **Given**: Pop-up xác nhận xóa đang được hiển thị.
  - **When**: Quản trị viên chọn "Hủy" hoặc đóng pop-up.
  - **Then**: Hệ thống không xóa System Prompt.
  - **And**: Bản ghi và dữ liệu trong danh sách được giữ nguyên vẹn.

- **AC-003 — Xóa System Prompt thành công**:
  - **Given**: Pop-up xác nhận xóa System Prompt đang được hiển thị.
  - **When**: Quản trị viên chọn "Xác nhận xóa".
  - **Then**: Hệ thống xóa thành công System Prompt.
  - **And**: System Prompt không còn hiển thị trong danh sách quản lý.

- **AC-004 — Đảm bảo hoạt động của AI sau khi xóa**:
  - **Given**: System Prompt đã được xóa thành công và không còn được sử dụng bởi bất kỳ chức năng AI nào.
  - **When**: Chức năng AI trong hệ thống được người dùng kích hoạt.
  - **Then**: Hệ thống sử dụng System Prompt đang được cấu hình hợp lệ hiện tại của chức năng AI tương ứng.

- **AC-005 — Xử lý lỗi hệ thống khi xóa (EXC-01)**:
  - **Given**: Quản trị viên đã xác nhận xóa trên pop-up.
  - **When**: Quá trình xóa gặp lỗi kết nối hoặc lỗi cơ sở dữ liệu.
  - **Then**: Hệ thống rollback dữ liệu, không xóa dở dang.
  - **And**: Hiển thị thông báo: *"Không thể xóa System Prompt, vui lòng thử lại"*.

---

## Non-Functional
- **Tốc độ phản hồi**: Thao tác kiểm tra ràng buộc nghiệp vụ và thực thi xóa phải phản hồi dưới 2 giây trong điều kiện hoạt động bình thường.
- **Toàn vẹn hệ thống**: Thao tác xóa phải đảm bảo không làm gián đoạn hoặc gây crash các tiến trình AI đang chạy dở dang.

---

## Out of Scope
- Không bao gồm các thao tác xem chi tiết hoặc chỉnh sửa nội dung System Prompt.
- Không bao gồm tính năng khôi phục (Restore) System Prompt sau khi đã xóa vĩnh viễn.
