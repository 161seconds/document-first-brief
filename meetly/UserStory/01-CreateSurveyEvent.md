# US-01: Tạo sự kiện cần khảo sát

## Metadata

- **Story**: Với vai trò là người tổ chức, tôi muốn tạo một sự kiện phục vụ việc khảo sát để có thể thu thập thời gian rảnh của những người tham dự.
- **Context**: Người tổ chức cần thiết lập một sự kiện khảo sát thời gian họp/gặp mặt mới. Quá trình tạo yêu cầu cung cấp các thông tin thiết yếu gồm tên sự kiện, loại sự kiện, và danh sách các ngày đề xuất bình chọn. Sau khi tạo thành công, hệ thống tự động sinh ID sự kiện, liên kết chia sẻ (URL) và yêu cầu người tổ chức đăng ký tài khoản định danh (username, password) làm Quản trị viên (Admin) của sự kiện, sau đó đưa người tổ chức đến màn hình khảo sát.
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

- Người tổ chức đã truy cập vào hệ thống Meetly trên trình duyệt web.
- Hệ thống đang trong trạng thái hoạt động bình thường.

### Trigger

- Người tổ chức nhấn nút "Tạo sự kiện" (Create Event) từ màn hình chính của Meetly.

---

## Flow

### Main Flow

1. Người tổ chức mở chức năng "Tạo sự kiện".
2. Hệ thống hiển thị biểu mẫu tạo sự kiện với các trường thông tin: Tên sự kiện, Loại sự kiện, Danh sách các ngày để bình chọn.
3. Người tổ chức nhập đầy đủ thông tin hợp lệ:
   - Tên sự kiện (bắt buộc).
   - Loại sự kiện (bắt buộc).
   - Các ngày để bình chọn (bắt buộc chọn ít nhất 1 ngày hợp lệ).
4. Người tổ chức nhấn nút xác nhận "Tạo sự kiện".
5. Hệ thống kiểm tra tính hợp lệ của toàn bộ dữ liệu đầu vào.
6. Hệ thống tạo và lưu trữ sự kiện mới vào cơ sở dữ liệu.
7. Hệ thống sinh mã định danh duy nhất (Event ID / ShortCode) và đường dẫn truy cập sự kiện (URL).
8. Hệ thống hiển thị form yêu cầu người tổ chức đăng ký thông tin quản trị: Username và Mật khẩu.
9. Người tổ chức nhập username và mật khẩu hợp lệ rồi xác nhận.
10. Hệ thống lưu tài khoản admin gắn liền với sự kiện vừa tạo.
11. Hệ thống cung cấp link sự kiện và Event ID/ShortCode để người tổ chức sao chép và chia sẻ cho người tham gia.
12. Hệ thống tự động chuyển hướng người tổ chức đến màn hình khảo sát / Dashboard của sự kiện.

### Alternative Flow

- **ALT-01 — Người tổ chức hủy thao tác tạo sự kiện**:
  1. Tại bước 3 của Main Flow, người tổ chức chọn "Hủy" hoặc đóng màn hình tạo.
  2. Hệ thống không lưu bất kỳ thông tin nào và đưa người dùng trở lại màn hình trước đó.

### Exception Flow

- **EXC-01 — Thiếu thông tin bắt buộc**:
  1. Tại bước 4 của Main Flow, người tổ chức để trống Tên sự kiện hoặc Loại sự kiện.
  2. Hệ thống chặn thao tác gửi dữ liệu.
  3. Hệ thống hiển thị thông báo lỗi cụ thể ngay dưới từng trường dữ liệu bị thiếu và yêu cầu người dùng điền đầy đủ.
- **EXC-02 — Định dạng hoặc ngày bình chọn không hợp lệ**:
  1. Người tổ chức nhập thông tin sai định dạng hoặc chọn ngày bình chọn trong quá khứ.
  2. Hệ thống hiển thị thông báo lỗi định dạng cụ thể, giữ nguyên các thông tin đã điền hợp lệ để người tổ chức chỉnh sửa.
- **EXC-03 — Lỗi hệ thống khi lưu trữ**:
  1. Tại bước 6 hoặc bước 10 của Main Flow, hệ thống gặp sự cố kết nối máy chủ hoặc cơ sở dữ liệu.
  2. Hệ thống hiển thị thông báo lỗi: *"Không thể tạo sự kiện lúc này. Vui lòng thử lại sau."*
  3. Dữ liệu đã nhập trên form không bị xóa mất.

---

## Acceptance Criteria

#### AC-001 — Mở chức năng Tạo sự kiện
- **Given**: Người tổ chức đang ở trang chủ hoặc màn hình chính của Meetly.
- **When**: Người tổ chức nhấn nút "Tạo sự kiện".
- **Then**: Hệ thống mở màn hình/biểu mẫu Tạo sự kiện với đầy đủ các trường nhập liệu cần thiết.

#### AC-002 — Nhập đầy đủ thông tin hợp lệ và tạo thành công
- **Given**: Người tổ chức đang ở màn hình Tạo sự kiện.
- **When**: Người tổ chức nhập đầy đủ thông tin hợp lệ gồm tên sự kiện, loại sự kiện, danh sách các ngày để bình chọn và nhấn xác nhận tạo.
- **Then**: Hệ thống tạo mới một sự kiện trong hệ thống.
- **And**: Tên sự kiện và Loại sự kiện là các trường bắt buộc phải có giá trị.

#### AC-003 — Báo lỗi khi thiếu thông tin hoặc sai format
- **Given**: Người tổ chức đang ở màn hình Tạo sự kiện.
- **When**: Người tổ chức để trống trường bắt buộc hoặc nhập dữ liệu sai định dạng quy định.
- **Then**: Hệ thống hiển thị thông báo lỗi cụ thể tại từng trường vi phạm và không cho phép tiến hành tạo.

#### AC-004 — Lưu trữ sự kiện vào hệ thống
- **Given**: Toàn bộ thông tin sự kiện đã được kiểm tra tính hợp lệ.
- **When**: Hệ thống hoàn tất xử lý xác nhận tạo.
- **Then**: Dữ liệu sự kiện được lưu trữ an toàn và đầy đủ vào cơ sở dữ liệu.

#### AC-005 — Điều hướng đến màn hình khảo sát
- **Given**: Sự kiện đã được tạo thành công và tài khoản quản trị đã được thiết lập.
- **When**: Quy trình khởi tạo hoàn tất.
- **Then**: Hệ thống tự động chuyển hướng người tổ chức đến màn hình khảo sát / Dashboard của sự kiện đó.

#### AC-006 — Bắt buộc đăng ký tài khoản Admin sự kiện
- **Given**: Sự kiện vừa được khởi tạo thành công vào hệ thống.
- **When**: Hệ thống chuyển sang bước tiếp theo.
- **Then**: Hệ thống bắt buộc người tổ chức phải đăng ký username và password cho tài khoản quản trị của mình trước khi tiếp tục.

#### AC-007 — Nhận liên kết sự kiện và ID để chia sẻ
- **Given**: Sự kiện đã được tạo và kích hoạt thành công.
- **When**: Người tổ chức hoàn tất quy trình tạo.
- **Then**: Hệ thống hiển thị rõ ràng đường link truy cập sự kiện (URL) và ID/ShortCode kèm nút sao chép (Copy) tiện lợi để chia sẻ cho người tham dự.

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng tạo sự kiện đã hoàn thành.
- [ ] 2. Đã implement validation cho các trường bắt buộc (Tên sự kiện, Loại sự kiện, Ngày bình chọn).
- [ ] 3. Đã xử lý trường hợp tạo thành công / thất bại và hiển thị feedback rõ ràng.
- [ ] 4. Unit test đã được viết và pass toàn bộ (tối thiểu đạt độ bao phủ theo tiêu chuẩn dự án).
- [ ] 5. Code đã được code review và phê duyệt bởi Tech Lead / Peer Reviewer.
- [ ] 6. QA đã kiểm thử và pass toàn bộ các tiêu chí Acceptance Criteria (AC-001 đến AC-007).
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story này.
- [ ] 8. Đã merge code vào branch chính theo quy định phát triển của team.
- [ ] 9. Đã deploy thành công lên môi trường kiểm thử (Staging/UAT).

---

## References

### Rules
- [`BR-01`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-01.md): Quy định đặt tên sự kiện và độ dài ký tự hợp lệ.
- [`BR-02`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-02.md): Danh mục các loại sự kiện hỗ trợ và quy tắc cấu hình.
- [`BR-03`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-03.md): Quy tắc chọn danh sách ngày khảo sát (không chọn ngày quá khứ, giới hạn số ngày).
- [`BR-04`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-04.md): Quy tắc sinh Event ID và ShortCode duy nhất.
- [`BR-05`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-05.md): Quy tắc bảo mật đường link sự kiện (URL Token/Slug).
- [`BR-06`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-06.md): Quy định định dạng username và mật khẩu quản trị sự kiện.

### Dependencies
- Không phụ thuộc User Story trước đó (Đây là entry-point của luồng tổ chức sự kiện).

---

## Notes

- Giao diện tạo sự kiện cần tối ưu hóa trải nghiệm người dùng (UX), cho phép chọn nhanh các ngày trong tuần hoặc dải ngày liên tiếp.
- Hỗ trợ nút sao chép nhanh đường dẫn (Copy Link) và chia sẻ qua QR Code hoặc mạng xã hội.
