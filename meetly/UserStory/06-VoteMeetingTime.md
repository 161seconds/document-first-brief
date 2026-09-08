# US-06: Bình chọn thời gian họp

## Metadata

- **Story**: Với vai trò là người tham gia sự kiện, tôi muốn đánh dấu những khoảng thời gian mình rảnh bằng cách kéo thả hoặc chọn trực tiếp trên heatmap để hệ thống ghi nhận thời gian phù hợp của tôi và hỗ trợ người tổ chức tìm ra thời gian phù hợp cho sự kiện.
- **Context**: Người tham gia đã định danh trong sự kiện thực hiện chọn các khung giờ mà họ có thể tham gia. Màn hình bình chọn cung cấp một Heatmap tương tác hỗ trợ cả 2 thao tác: nhấp chuột (click) từng ô hoặc nhấn giữ kéo thả (drag-and-drop) để chọn dải giờ nhanh chóng. Hệ thống hỗ trợ 2 chế độ đánh dấu: ĐÁNH DẤU THỜI GIAN RẢNH và ĐÁNH DẤU THỜI GIAN BẬN (để hỗ trợ người dùng có lịch linh hoạt hoặc người bận ít/nhiều). Dữ liệu cuối cùng được chuẩn hóa và lưu trữ chỉ dưới dạng THỜI GIAN RẢNH. Người dùng có thể quay lại chỉnh sửa bình chọn bất cứ lúc nào trước khi sự kiện kết thúc.
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
- Người dùng đã hoàn tất bước Định danh hợp lệ trong sự kiện (US-04).
- Sự kiện vẫn đang mở (chưa vượt quá ngày cuối cùng bình chọn).

### Trigger

- Người dùng nhấn nút "Bình chọn thời gian" (hoặc "Chỉnh sửa lịch của tôi") trên giao diện Dashboard sự kiện.

---

## Flow

### Main Flow

1. Người dùng đã định danh nhấn chọn tính năng "Bình chọn thời gian".
2. Hệ thống kiểm tra điều kiện định danh và tính hợp lệ của thời gian sự kiện.
3. Hệ thống mở giao diện tương tác bình chọn:
   - Nếu bình chọn lần đầu: Heatmap cá nhân hiển thị trạng thái ban đầu là Heatmap trắng.
   - Nếu chỉnh sửa: Hiển thị lại toàn bộ các khoảng thời gian rảnh đã lưu trước đó của người dùng.
4. Hệ thống cung cấp bộ chuyển đổi 2 chế độ:
   - Chế độ 1: "ĐÁNH DẤU THỜI GIAN RẢNH" (mặc định tô màu xanh các ô được chọn).
   - Chế độ 2: "ĐÁNH DẤU THỜI GIAN BẬN" (xóa/đảo ngược các ô được chọn thành bận).
5. Người dùng thực hiện chọn thời gian bằng một trong hai cách:
   - Click chuột / chạm trực tiếp vào từng ô 15 phút.
   - Kéo thả (drag-and-drop) chuột / ngón tay qua nhiều ô liên tiếp (hỗ trợ chọn xuyên ngày nếu sự kiện cho phép).
6. Người dùng kiểm tra lại các khoảng thời gian đã được đánh dấu rảnh trên lưới thời gian.
7. Người dùng nhấn nút "Lưu bình chọn".
8. Hệ thống tính toán, quy đổi toàn bộ trạng thái đã chọn thành danh sách các khoảng THỜI GIAN RẢNH (không lưu mode BẬN như trạng thái cuối cùng).
9. Hệ thống gửi dữ liệu lên máy chủ để cập nhật hoặc ghi đè kết quả bình chọn duy nhất của username đó.
10. Hệ thống hiển thị thông báo thành công: *"Lịch rảnh của bạn đã được ghi nhận thành công."*
11. Hệ thống tự động chuyển người dùng trở về giao diện xem Heatmap tổng hợp trên Dashboard.

### Alternative Flow

- **ALT-01 — Chỉnh sửa lại bình chọn đã lưu**:
  1. Người dùng đã bình chọn trước đó muốn đổi lịch rảnh.
  2. Người dùng mở lại tính năng bình chọn.
  3. Hệ thống tải lại trạng thái các khung giờ rảnh đã lưu trước đó.
  4. Người dùng tiếp tục click hoặc kéo thả để thêm/bớt các khoảng thời gian.
  5. Người dùng nhấn "Lưu".
  6. Hệ thống cập nhật kết quả mới và thay thế hoàn toàn kết quả bình chọn cũ của người dùng trong sự kiện này.
- **ALT-02 — Hủy thao tác bình chọn**:
  1. Người dùng đang thao tác trên màn hình bình chọn nhưng chọn nút "Hủy" hoặc "Quay lại".
  2. Hệ thống không lưu các thay đổi vừa thao tác và đưa người dùng trở về giao diện xem Heatmap trên Dashboard.
- **ALT-03 — Chọn mốc sự kiện xuyên ngày**:
  1. Người dùng kéo chọn khung giờ kéo dài qua nửa đêm (ví dụ: từ 21h tối hôm trước đến 3h sáng hôm sau).
  2. Hệ thống ghi nhận và phân tách thành các slot thời gian rảnh hợp lệ qua hai ngày liên tiếp.

### Exception Flow

- **EXC-01 — Chưa định danh**:
  1. Người dùng chưa định danh nhấn nút bình chọn.
  2. Hệ thống chặn mở form bình chọn và chuyển hướng sang màn hình Đăng ký định danh (US-04).
- **EXC-02 — Đã vượt quá ngày cuối cùng khảo sát**:
  1. Thời gian hiện tại đã vượt quá ngày cuối cùng trong danh sách bình chọn của sự kiện.
  2. Hệ thống vô hiệu hóa nút bình chọn và hiển thị thông báo: *"Sự kiện đã kết thúc thời gian bình chọn."*
- **EXC-03 — Cố tình chọn ngày trong quá khứ**:
  1. Người dùng cố tình thao tác trên các ô thuộc về ngày/giờ quá khứ.
  2. Hệ thống vô hiệu hóa các ô này (disabled) và không ghi nhận thao tác chọn.
- **EXC-04 — Lỗi kết nối khi lưu bình chọn**:
  1. Tại bước 9 của Main Flow, mạng bị ngắt hoặc máy chủ trả về lỗi.
  2. Hệ thống hiển thị thông báo lỗi chi tiết và giữ nguyên trạng thái các ô đã đánh dấu trên giao diện để người dùng có thể bấm lưu lại mà không bị mất dữ liệu.

---

## Acceptance Criteria

#### AC-001 — Điều kiện định danh hợp lệ trước khi bình chọn
- **Given**: Người dùng đang ở trong sự kiện.
- **When**: Người dùng muốn thực hiện bình chọn ngày rảnh.
- **Then**: Chỉ người dùng đã định danh hợp lệ trong sự kiện mới có thể thực hiện bình chọn.

#### AC-002 — Trạng thái Heatmap khi bắt đầu bình chọn mới
- **Given**: Người dùng đã định danh và chưa từng bình chọn cho sự kiện này.
- **When**: Người dùng chọn tính năng bình chọn.
- **Then**: Heatmap hiển thị để bình chọn là một Heatmap trắng (chưa có ô nào được chọn).

#### AC-003 — Chọn thời gian bằng cách kéo thả
- **Given**: Người dùng đang ở giao diện Heatmap bình chọn.
- **When**: Người dùng nhấn giữ và kéo chuột/ngón tay qua một hoặc nhiều ô thời gian.
- **Then**: Hệ thống đánh dấu toàn bộ các ô nằm trong vùng kéo thả theo chế độ đang chọn.

#### AC-004 — Chọn thời gian bằng cách Click trực tiếp
- **Given**: Người dùng đang ở giao diện Heatmap bình chọn.
- **When**: Người dùng nhấp (click) trực tiếp vào một ô thời gian.
- **Then**: Hệ thống đổi trạng thái đánh dấu của ô đó (bật/tắt trạng thái rảnh).

#### AC-005 — Lựa chọn nhiều khoảng thời gian trong phạm vi cho phép
- **Given**: Giao diện bình chọn đang mở.
- **When**: Người dùng thực hiện chọn thời gian.
- **Then**: Người dùng có thể lựa chọn một hoặc nhiều khoảng thời gian khác nhau trong phạm vi các ngày/khung giờ mà sự kiện cho phép bình chọn.

#### AC-006 — Cung cấp 2 chế độ bình chọn (RẢNH và BẬN)
- **Given**: Giao diện bình chọn thời gian.
- **When**: Người dùng quan sát thanh công cụ bình chọn.
- **Then**: Hệ thống cung cấp rõ ràng 2 mode bình chọn: "ĐÁNH DẤU THỜI GIAN RẢNH" và "ĐÁNH DẤU THỜI GIAN BẬN".

#### AC-007 — Chuẩn hóa lưu trữ dữ liệu thời gian RẢNH
- **Given**: Người dùng sử dụng một hoặc cả hai mode bình chọn (RẢNH hoặc BẬN) trong phiên thao tác.
- **When**: Người dùng nhấn Lưu thành công.
- **Then**: Hệ thống hiển thị lại trạng thái bình chọn dựa trên thời gian rảnh đã được lưu, và không lưu mode BẬN như một trạng thái dữ liệu cuối cùng.

#### AC-008 — Cho phép chỉnh sửa lại bình chọn đã lưu
- **Given**: Người dùng đã từng lưu kết quả bình chọn trước đó.
- **When**: Người dùng mở lại chức năng bình chọn.
- **Then**: Hệ thống tải lại trạng thái đã lưu và cho phép người dùng tiếp tục click hoặc kéo thả trên Heatmap để thay đổi.

#### AC-009 — Cập nhật và thay thế kết quả bình chọn cũ
- **Given**: Người dùng tiến hành chỉnh sửa và chọn Lưu lại.
- **When**: Dữ liệu được gửi lên hệ thống.
- **Then**: Hệ thống cập nhật kết quả bình chọn mới và thay thế toàn bộ kết quả bình chọn trước đó của người dùng đó.

#### AC-010 — Thông báo khi lưu thành công
- **Given**: Người dùng nhấn nút Lưu bình chọn.
- **When**: Hệ thống ghi nhận dữ liệu thành công vào cơ sở dữ liệu.
- **Then**: Hệ thống thông báo rõ ràng cho người dùng biết bình chọn đã được ghi nhận / cập nhật thành công.

#### AC-011 — Điều hướng về Dashboard sau khi Lưu hoặc Hủy
- **Given**: Người dùng đang ở giao diện bình chọn.
- **When**: Người dùng lưu thành công hoặc chọn Cancel/Hủy việc bình chọn.
- **Then**: Hệ thống đóng màn hình bình chọn và trả về giao diện xem Heatmap tổng hợp trên Dashboard.

#### AC-012 — Bảo toàn trạng thái bình chọn khi gặp lỗi lưu
- **Given**: Người dùng đã đánh dấu các khoảng thời gian trên Heatmap và nhấn Lưu.
- **When**: Xảy ra lỗi trong quá trình gửi hoặc lưu dữ liệu lên máy chủ.
- **Then**: Hệ thống thông báo lỗi cụ thể và không làm mất trạng thái bình chọn mà người dùng đang chỉnh sửa.

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng bình chọn tương tác (Click & Drag-and-drop) đã hoàn thành.
- [ ] 2. Đã implement bộ chuyển đổi 2 mode (Đánh dấu Rảnh / Đánh dấu Bận) và thuật toán chuẩn hóa dữ liệu sang mốc Rảnh.
- [ ] 3. Đã implement chặn bình chọn quá hạn và chặn chọn ngày quá khứ.
- [ ] 4. Unit test cho logic lưu trữ và chuẩn hóa dữ liệu bình chọn đã được viết và pass.
- [ ] 5. Code đã được code review và phê duyệt.
- [ ] 6. QA đã test và pass toàn bộ 12 tiêu chí Acceptance Criteria.
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story.
- [ ] 8. Đã merge code vào branch theo quy định của team.
- [ ] 9. Đã deploy lên môi trường cần thiết (Staging/UAT).

---

## References

### Rules
- [`BR-09`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-09.md): Mỗi username trong sự kiện chỉ có một kết quả bình chọn duy nhất.
- [`BR-11`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-11.md): Khi bình chọn một sự kiện, cần phải đăng nhập vào tài khoản định danh.
- [`BR-12`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-12.md): Không được phép bình chọn ngày khi hiện tại đã vượt quá ngày cuối cùng trong danh sách bình chọn.
- [`BR-13`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-13.md): Dữ liệu bình chọn được lưu trữ dưới dạng thời gian RẢNH. Mode BẬN chỉ là phương thức nhập liệu.
- [`BR-14`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-14.md): Quy ước màu sắc Heatmap (trắng là bận, xanh nhạt đến đậm là số người rảnh).
- [`BR-15`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-15.md): Không thể bình chọn cho các ngày trong quá khứ.
- [`BR-16`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-16.md): Người tham gia sự kiện có thể chọn các mốc sự kiện xuyên ngày (ví dụ: 21h - 3h hôm sau).

### Dependencies
- [`US-04`](file:///d:/VNZ/document-first-brief/meetly/UserStory/04-RegisterParticipantIdentity.md): Đăng ký tài khoản định danh trong sự kiện.
- [`US-05`](file:///d:/VNZ/document-first-brief/meetly/UserStory/05-ViewResultsViaHeatmap.md): Xem kết quả qua Heatmap.

---

## Notes

- Trải nghiệm kéo thả trên màn hình cảm ứng di động cần xử lý mượt mà sự khác biệt giữa vuốt để cuộn trang (scroll) và kéo để chọn ô thời gian (touch select). Nên có nút toggle "Chạm để chọn" (Tap mode) riêng trên mobile.
