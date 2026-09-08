# US-05: Xem kết quả qua Heatmap

## Metadata

- **Story**: Với vai trò là người tham gia sự kiện, tôi muốn xem heatmap để trực quan biết được những khoảng thời gian có nhiều người rảnh, từ đó lựa chọn thời gian phù hợp.
- **Context**: Heatmap là tính năng hiển thị trực quan quan trọng nhất của Meetly, tổng hợp lịch rảnh của tất cả người tham gia sự kiện. Heatmap có trục dọc là Times (các khung giờ, vạch chia nhỏ nhất 15 phút) và trục ngang là Dates (các ngày khảo sát, vạch chia nhỏ nhất 1 ngày). Màu sắc các ô biểu thị mật độ người rảnh (ô trắng là bận/chưa có người chọn, màu xanh từ nhạt đến đậm thể hiện số người rảnh từ ít đến nhiều). Khi hover chuột vào từng ô, hệ thống hiển thị số lượng và danh sách người rảnh tương ứng. Các ngày trong quá khứ được thể hiện màu nhạt (disabled) không thể bình chọn.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: Product Owner
- **Assignee**:
  - Backend: Chưa chỉ định
  - Frontend: Chưa chỉ định
  - QA: Chưa chỉ định

---

## Conditions

### Preconditions

- Người dùng đã tham gia vào sự kiện thành công.
- Sự kiện có dữ liệu cấu hình ngày và khung giờ khảo sát.

### Trigger

- Người dùng truy cập vào màn hình Dashboard của sự kiện.

---

## Flow

### Main Flow

1. Người dùng đã tham gia truy cập vào màn hình Dashboard sự kiện.
2. Hệ thống kiểm tra tính hợp lệ của phiên truy cập sự kiện.
3. Hệ thống tải toàn bộ dữ liệu cấu hình sự kiện và kết quả bình chọn hiện có của các thành viên.
4. Hệ thống hiển thị ma trận Heatmap:
   - Cột dọc (Trục Y): Hiển thị Times, vạch chia nhỏ nhất là 15 phút.
   - Cột ngang (Trục X): Hiển thị Dates, vạch chia nhỏ nhất là 1 ngày.
   - Thanh chú giải (Legend): Giải thích ý nghĩa sắc độ màu từ trắng đến xanh đậm.
5. Mỗi ô trong ma trận được tô màu tương ứng với số lượng người rảnh tại khung giờ đó:
   - Ô màu trắng: Không có ai rảnh (0 người).
   - Màu xanh nhạt $\rightarrow$ đậm dần: Tỷ lệ người rảnh tăng dần.
6. Người dùng hover chuột (hoặc chạm trên màn hình cảm ứng) vào một ô thời gian cụ thể.
7. Hệ thống hiển thị Tooltip cung cấp:
   - Thời gian chi tiết của khung giờ (ví dụ: `Thứ Ba, 15/09/2026 09:00 - 09:15`).
   - Tổng số người rảnh (ví dụ: `4/6 người rảnh`).
   - Danh sách tên cụ thể của những người rảnh.

### Alternative Flow

- **ALT-01 — Sự kiện mới chưa có lượt bình chọn nào**:
  1. Tại bước 4 của Main Flow, sự kiện vừa được tạo và chưa có ai gửi kết quả bình chọn.
  2. Hệ thống hiển thị toàn bộ các ô trên Heatmap dưới dạng màu trắng.
- **ALT-02 — Cập nhật realtime khi có người bình chọn mới**:
  1. Khi có người tham gia gửi hoặc chỉnh sửa bình chọn của họ.
  2. Hệ thống tự động làm mới số lượng người rảnh và cập nhật lại sắc độ màu trên các ô Heatmap tương ứng.
- **ALT-03 — Hiển thị các ngày trong quá khứ**:
  1. Danh sách ngày của sự kiện có những ngày/khung giờ đã trôi qua so với thời gian hiện tại.
  2. Hệ thống hiển thị các ngày/ô này với màu nhạt, đánh dấu trạng thái vô hiệu hóa (disabled) không thể bình chọn.

### Exception Flow

- **EXC-01 — Người dùng chưa vào sự kiện**:
  1. Người dùng chưa tham gia sự kiện tìm cách truy cập đường link Dashboard trực tiếp.
  2. Hệ thống chặn hiển thị Heatmap và yêu cầu tham gia sự kiện trước.
- **EXC-02 — Lỗi tải dữ liệu Heatmap**:
  1. Hệ thống gặp sự cố tải dữ liệu bình chọn từ máy chủ.
  2. Hệ thống hiển thị thông báo lỗi kèm nút "Tải lại" (Retry).

---

## Acceptance Criteria

#### AC-001 — Quyền xem Heatmap của người tham gia
- **Given**: Người dùng đã tham gia vào sự kiện thành công.
- **When**: Người dùng truy cập Dashboard của sự kiện.
- **Then**: Người dùng có thể xem ma trận Heatmap của sự kiện đó.

#### AC-002 — Phạm vi thời gian hiển thị trên Heatmap
- **Given**: Sự kiện đã được cấu hình danh sách ngày và khung giờ khảo sát.
- **When**: Heatmap hiển thị lên màn hình.
- **Then**: Ma trận hiển thị đầy đủ các khoảng thời gian nằm trong phạm vi bình chọn mà sự kiện cho phép.

#### AC-003 — Hiển thị số người rảnh khi Hover
- **Given**: Heatmap đang hiển thị dữ liệu tổng hợp.
- **When**: Người dùng hover chuột vào mỗi khoảng thời gian trên Heatmap.
- **Then**: Hệ thống hiển thị tooltip với số lượng người đang rảnh tương ứng tại khung giờ đó.

#### AC-004 — Điều kiện bắt buộc để xem Heatmap
- **Given**: Một người dùng bất kỳ.
- **When**: Người dùng cố gắng xem Heatmap của sự kiện.
- **Then**: Chỉ người dùng đã vào được sự kiện thành công mới có thể xem Heatmap.

#### AC-005 — Cập nhật Heatmap khi dữ liệu bình chọn thay đổi
- **Given**: Dữ liệu bình chọn của người tham gia trong sự kiện có sự thay đổi (thêm mới hoặc chỉnh sửa).
- **When**: Dữ liệu mới được cập nhật lên hệ thống.
- **Then**: Số lượng người rảnh và màu sắc tương ứng trên Heatmap được cập nhật đồng bộ theo dữ liệu mới.

#### AC-006 — Cấu trúc cột dọc của Heatmap (Times)
- **Given**: Giao diện Heatmap của sự kiện.
- **When**: Kiểm tra trục tung (cột dọc).
- **Then**: Heatmap được hiển thị có cột dọc là Times, vạch chia nhỏ nhất là 15 phút.

#### AC-007 — Cấu trúc cột ngang của Heatmap (Dates)
- **Given**: Giao diện Heatmap của sự kiện.
- **When**: Kiểm tra trục hoành (cột ngang).
- **Then**: Heatmap được hiển thị có cột ngang là Dates, vạch chia nhỏ nhất là 1 ngày.

#### AC-008 — Trạng thái mặc định ban đầu của Heatmap
- **Given**: Sự kiện chưa có bất kỳ người tham gia nào bình chọn.
- **When**: Mở xem Heatmap lần đầu.
- **Then**: Trạng thái mặc định ban đầu của Heatmap là toàn bộ màu trắng.

#### AC-009 — Chú thích ý nghĩa màu sắc (Legend)
- **Given**: Giao diện hiển thị Heatmap.
- **When**: Người dùng quan sát khu vực xung quanh Heatmap.
- **Then**: Hệ thống cung cấp phần chú thích rõ ràng về ý nghĩa màu sắc (ô trắng là bận/chưa ai chọn, màu xanh nhạt đến đậm thể hiện số người rảnh từ ít đến nhiều).

#### AC-010 — Hiển thị các ngày trong quá khứ
- **Given**: Sự kiện có chứa các ngày/thời điểm nằm trong quá khứ so với thời gian hiện tại.
- **When**: Heatmap hiển thị lên giao diện.
- **Then**: Các ngày trong quá khứ được thể hiện bằng màu sắc nhạt, đánh dấu không thể bình chọn tiếp và ở trạng thái vô hiệu hóa (disabled).

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng hiển thị Heatmap và tooltip đã hoàn thành.
- [ ] 2. Đã implement ma trận chia 15 phút (cột dọc) và 1 ngày (cột ngang).
- [ ] 3. Đã xử lý tính toán dải màu sắc (từ trắng đến xanh đậm) dựa trên tỷ lệ người rảnh.
- [ ] 4. Unit test cho logic tổng hợp dữ liệu Heatmap đã được viết và pass.
- [ ] 5. Code đã được code review và phê duyệt.
- [ ] 6. QA đã test và pass toàn bộ 10 tiêu chí Acceptance Criteria.
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story.
- [ ] 8. Đã merge code vào branch theo quy định của team.
- [ ] 9. Đã deploy lên môi trường cần thiết (Staging/UAT).

---

## References

### Rules
- [`BR-14`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-14.md): Quy ước màu sắc trên Heatmap (trắng thể hiện bận, xanh nhạt đến đậm thể hiện số người rảnh).
- [`BR-15`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-15.md): Không thể bình chọn cho các ngày trong quá khứ (hiển thị nhạt, disabled).

### Dependencies
- [`US-03`](file:///d:/VNZ/document-first-brief/meetly/UserStory/03-JoinEvent.md): Tham gia sự kiện.

---

## Notes

- Trên thiết bị di động (Mobile màn hình nhỏ), Heatmap cần hỗ trợ vuốt ngang mượt mà (Horizontal Scroll) và chế độ xem tóm tắt các khung giờ có đông người rảnh nhất (Best Times).
