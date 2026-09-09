# STORY-015: Xóa lịch đăng bài tự động

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa lịch đăng bài tự động không còn cần thiết để ngăn lịch tiếp tục được thực thi và giữ kế hoạch đăng bài chính xác
- **Context**: Quản trị viên có thể cần loại bỏ lịch đăng bài không còn phù hợp để tránh việc hệ thống tự động đăng bài lên mạng xã hội theo lịch cũ. Trước khi xóa, hệ thống phải kiểm tra quyền, trạng thái lịch và yêu cầu Quản trị viên xác nhận. Chức năng này chỉ xử lý xóa lịch đăng bài; không bao gồm xem, tạo, sửa hoặc xóa bài đã đăng trên các nền tảng mạng xã hội của bên thứ ba.
- **Quy định trạng thái cho phép xóa**:
  - **Được phép xóa khi**:
    - Lịch còn tồn tại trong cơ sở dữ liệu.
    - Lịch đang ở trạng thái **"Đã lên lịch"** (BR-010).
    - Cron Job / Background Worker chưa bắt đầu thực hiện tác vụ đăng bài.
  - **Không được phép xóa khi**:
    - Lịch không còn tồn tại hoặc đã bị xóa trước đó.
    - Lịch đang được hệ thống thực hiện đăng bài (Job đang trong trạng thái xử lý/gọi API nền tảng).
    - Lịch đã ở trạng thái **"Đăng thành công"**.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.2
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 09/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Cần thêm định nghĩa thế nào là trạng thái cho phép xoá. Chưa có AC cho EXC - 02, 03. Nếu Admin xác nhận xóa đúng lúc job chạy, bài có được đăng không?"* — Nguyễn Đức Bình · 09:43 03/09/2026
  - **Phản hồi & Cập nhật**:
    1. **Định nghĩa trạng thái cho phép xóa**: Đã nêu rõ tại Context & BR-010: Chỉ được xóa khi lịch ở trạng thái **"Đã lên lịch"** và Job chưa chạy. Khi lịch ở trạng thái "Đang xử lý" hoặc "Đăng thành công" thì tuyệt đối không được xóa.
    2. **Xử lý tình huống bấm xóa đúng lúc Job chạy**: Đã bổ sung cơ chế kiểm tra khóa (Mutex/Lock) tại **EXC-03** và **AC-006**: Nếu Job đã nhận việc và đang gọi API mạng xã hội, hệ thống **từ chối xóa và bài viết vẫn được đăng** nhằm đảm bảo tính toàn vẹn dữ liệu (tránh bài đã xuất bản trên mạng xã hội nhưng database bị mất dấu vết). Hệ thống sẽ thông báo: *"Lịch đăng bài đang được thực thi, không thể xóa"*.
    3. **Bổ sung Acceptance Criteria**: Đã liên kết đầy đủ **AC-004** (cho **EXC-02**: Lỗi hệ thống khi xóa), **AC-005** (cho **EXC-01**: Lịch không hợp lệ), và **AC-006** (cho **EXC-03**: Xung đột Job chạy đồng thời).

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Lịch đăng bài tồn tại và đang ở trạng thái được phép xóa ("Đã lên lịch").
- **Trigger**: 
  - Quản trị viên chọn nút "Xóa" tại một bản ghi lịch đăng bài.

---

## Flow

### Main Flow — Xóa lịch đăng bài tự động
1. Quản trị viên chọn lịch đăng bài cần xóa trong danh sách.
2. Quản trị viên nhấn nút "Xóa".
3. Hệ thống kiểm tra lịch còn tồn tại và đang ở trạng thái được phép xóa ("Đã lên lịch") (BR-010).
4. Hệ thống hiển thị pop-up cảnh báo xác nhận xóa gồm tên bài đăng/content, nền tảng và thời gian đăng.
5. Quản trị viên chọn "Xác nhận xóa".
6. Hệ thống tiến hành hủy Cron Job / tác vụ lên lịch nền tương ứng (BR-011).
7. Sau khi Cron Job được hủy thành công, hệ thống xóa lịch đăng bài trong cơ sở dữ liệu.
8. Hệ thống hiển thị thông báo: *"Xóa lịch đăng bài thành công"* và cập nhật lại danh sách.

### Alternative Flow
- **ALT-01 — Quản trị viên hủy thao tác xóa**:
  - Tại bước 5 của Main Flow, nếu Quản trị viên chọn "Hủy" hoặc đóng pop-up xác nhận.
  - Hệ thống đóng pop-up, không hủy Job và giữ nguyên vẹn lịch đăng bài.

### Exception Flow
- **EXC-01 — Lịch không còn hợp lệ để xóa**:
  - Tại bước 3, nếu lịch không tồn tại hoặc trạng thái lịch đã chuyển sang "Đang xử lý" hoặc "Đăng thành công".
  - Hệ thống dừng thao tác, thông báo lý do lịch không thể xóa và tải lại dữ liệu mới nhất.
- **EXC-02 — Không thể hoàn tất xóa lịch (Lỗi hệ thống)**:
  - Tại bước 6-7, nếu hệ thống không thể hủy Job hoặc gặp lỗi cơ sở dữ liệu.
  - Thao tác xóa không được ghi nhận là thành công, hệ thống giữ nguyên lịch và thông báo: *"Không thể xóa lịch đăng bài. Vui lòng thử lại"*.
- **EXC-03 — Xung đột thời điểm Job bắt đầu thực thi (Race Condition)**:
  - Quản trị viên nhấn "Xác nhận xóa" đúng thời điểm Cron Job bắt đầu kích hoạt đăng bài.
  - Hệ thống kiểm tra khóa (lock/mutex): nếu Job đã nhận tác vụ và đang gọi API mạng xã hội, hệ thống từ chối lệnh xóa, giữ nguyên tiến trình đăng để đảm bảo tính nhất quán dữ liệu, và thông báo: *"Lịch đăng bài đang được thực thi, không thể xóa"*.

---

## Acceptance Criteria

- **AC-001 — Hiển thị pop-up xác nhận xóa**:
  - **Given**: Lịch đăng bài tồn tại và đang ở trạng thái được phép xóa ("Đã lên lịch").
  - **When**: Quản trị viên chọn "Xóa".
  - **Then**: Hệ thống hiển thị pop-up cảnh báo xác nhận xóa.
  - **And**: Lịch vẫn chưa bị xóa cho tới khi Quản trị viên nhấn "Xác nhận xóa".

- **AC-002 — Hủy job và xóa lịch thành công**:
  - **Given**: Lịch chưa tới thời điểm đăng (Job chưa chạy) và Quản trị viên đang mở pop-up xác nhận.
  - **When**: Quản trị viên chọn "Xác nhận xóa".
  - **Then**: Hệ thống hủy Cron Job tương ứng và xóa bản ghi lịch (BR-011).
  - **And**: Lịch biến mất khỏi danh sách quản lý.
  - **And**: Hiển thị thông báo: *"Xóa lịch đăng bài thành công"*.

- **AC-003 — Hủy thao tác xóa (ALT-01)**:
  - **Given**: Pop-up xác nhận xóa đang hiển thị.
  - **When**: Quản trị viên chọn "Hủy" hoặc đóng pop-up.
  - **Then**: Hệ thống đóng pop-up xác nhận.
  - **And**: Lịch đăng bài và Cron Job được giữ nguyên vẹn.

- **AC-004 — Xử lý khi không thể hoàn tất xóa (EXC-02)**:
  - **Given**: Lịch đăng bài đang ở trạng thái được phép xóa và Quản trị viên đã xác nhận xóa.
  - **When**: Hệ thống xảy ra lỗi mạng hoặc lỗi cơ sở dữ liệu trong quá trình hủy job hoặc xóa bản ghi.
  - **Then**: Hệ thống rollback, không ghi nhận thao tác xóa thành công.
  - **And**: Lịch đăng bài vẫn được giữ nguyên trong hệ thống.
  - **And**: Hệ thống hiển thị thông báo: *"Không thể xóa lịch đăng bài. Vui lòng thử lại."*
  - **And**: Lịch vẫn hiển thị trong danh sách sau khi dữ liệu được tải lại.

- **AC-005 — Chặn xóa khi lịch không còn hợp lệ (EXC-01)**:
  - **Given**: Lịch đã chuyển sang trạng thái "Đang xử lý" hoặc "Đăng thành công", hoặc đã bị xóa bởi Admin khác.
  - **When**: Quản trị viên bấm "Xóa".
  - **Then**: Hệ thống không hiển thị pop-up xóa thành công.
  - **And**: Hiển thị thông báo lỗi nêu rõ lịch không còn ở trạng thái cho phép xóa (BR-010).
  - **And**: Tải lại danh sách lịch với trạng thái cập nhật nhất.

- **AC-006 — Xử lý xung đột khi Job kích hoạt đồng thời (EXC-03)**:
  - **Given**: Quản trị viên bấm "Xác nhận xóa" ngay tại thời điểm Job đến hạn và bắt đầu chạy.
  - **When**: Hệ thống kiểm tra trạng thái khóa thực thi của Job.
  - **Then**: Nếu Job đã bắt đầu gửi dữ liệu lên nền tảng, hệ thống chặn lệnh xóa và cho phép bài đăng tiếp tục hoàn tất.
  - **And**: Thông báo cho Quản trị viên: *"Lịch đăng bài đang được thực thi, không thể xóa"*.

---

## References

### Business Rules
- [BR-010: Trạng thái hợp lệ để xóa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-010.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/70d8230c-9045-4024-9853-595928975dfc))
- [BR-011: Xóa lệnh Cron Job khi xóa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-011.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/eab55803-cba0-42a1-bf12-6f66349b3055))

---

## Non-Functional
- **Tính nguyên tử (Atomicity)**: Thao tác hủy Cron Job và xóa lịch trong CSDL phải đảm bảo tính nguyên tử (Transactional), tuyệt đối không để xảy ra trường hợp lịch đã xóa trên UI nhưng Job nền vẫn âm thầm tự động đăng bài.
- **Bảo mật**: Backend bắt buộc phải kiểm tra quyền hạn của Quản trị viên trước khi tiếp nhận và thực thi lệnh xóa.

---

## Out of Scope
- Không bao gồm tạo hoặc tự động đăng bài.
- Không bao gồm xem danh sách hoặc chỉnh sửa lịch.
- Không bao gồm xóa bài viết đã được xuất bản thành công trên các nền tảng mạng xã hội bên thứ ba.
