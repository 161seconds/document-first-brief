# US-04: Đăng ký tài khoản định danh trong sự kiện

## Metadata

- **Story**: Với vai trò là một người tham gia sự kiện, tôi muốn đăng ký thông tin định danh trong phạm vi một sự kiện để hệ thống phân biệt tôi với những người tham gia khác và ghi nhận lượt bình chọn của tôi.
- **Context**: Meetly áp dụng mô hình định danh độc lập theo từng sự kiện (Event-Scoped Identity). Người tham gia không cần phải đăng ký một tài khoản hệ thống toàn cầu phức tạp; thay vào đó, người dùng chỉ cần đăng ký Username (bắt buộc) và Mật khẩu (tùy chọn - không bắt buộc) trong phạm vi sự kiện đó. Tài khoản này cho phép hệ thống phân biệt người bình chọn, bảo vệ dữ liệu bình chọn của cá nhân và hiển thị tên trên danh sách rảnh của Heatmap.
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

- Người dùng đã tham gia vào sự kiện thành công (qua URL hoặc ShortCode) và đang có mặt tại Dashboard sự kiện.

### Trigger

- Người dùng nhấn nút "Định danh" (Identity / Sign in) hoặc nhấn nút "Bình chọn" khi chưa có thông tin định danh trong sự kiện.

---

## Flow

### Main Flow

1. Người dùng chọn chức năng đăng ký định danh hoặc thực hiện bình chọn lần đầu.
2. Hệ thống kiểm tra phiên truy cập, xác nhận người dùng chưa định danh trong sự kiện này.
3. Hệ thống hiển thị biểu mẫu Định danh yêu cầu:
   - Username (bắt buộc).
   - Password (tùy chọn / không bắt buộc - dùng để bảo vệ lượt bình chọn không bị người khác sửa).
4. Người dùng nhập username theo đúng format quy định (và mật khẩu nếu muốn).
5. Người dùng nhấn "Xác nhận".
6. Hệ thống kiểm tra tính duy nhất của username trong phạm vi sự kiện hiện tại:
   - Username chưa từng tồn tại trong sự kiện này.
7. Hệ thống tạo tài khoản định danh gắn liền với sự kiện hiện tại.
8. Hệ thống lưu trạng thái định danh vào phiên làm việc (Session/Token) của người dùng.
9. Hệ thống thông báo định danh thành công và cho phép người dùng tiến hành bình chọn.

### Alternative Flow

- **ALT-01 — Username đã tồn tại (Đăng nhập lại)**:
  1. Tại bước 6 của Main Flow, username người dùng vừa nhập đã có trong danh sách người tham gia sự kiện.
  2. Hệ thống hiển thị thông báo: *"Username này đã được sử dụng trong sự kiện. Nếu đây là bạn, vui lòng nhập mật khẩu để đăng nhập."*
  3. Người dùng nhập mật khẩu (nếu tài khoản đã đặt mật khẩu trước đó).
  4. Hệ thống xác thực mật khẩu. Nếu chính xác, hệ thống khôi phục phiên làm việc và lượt bình chọn trước đó của người dùng.
- **ALT-02 — Định danh khi sự kiện đã kết thúc**:
  1. Người dùng vào sự kiện đã kết thúc và thực hiện định danh.
  2. Hệ thống vẫn cho phép định danh để xem thông tin cá nhân và kết quả, nhưng không cho phép tạo mới lượt bình chọn.

### Exception Flow

- **EXC-01 — Chưa tham gia sự kiện**:
  1. Người dùng cố tình gọi trực tiếp API đăng ký định danh mà không có Event ID hợp lệ hoặc chưa tham gia sự kiện.
  2. Hệ thống từ chối yêu cầu và chặn truy cập.
- **EXC-02 — Định dạng Username không hợp lệ**:
  1. Người dùng nhập username rỗng, quá ngắn, quá dài hoặc chứa ký tự đặc biệt vi phạm quy định.
  2. Hệ thống hiển thị thông báo lỗi cụ thể ngay dưới trường username và chặn xác nhận.
- **EXC-03 — Nhập sai mật khẩu khi đăng nhập lại**:
  1. Username đã tồn tại và có mật khẩu bảo vệ, nhưng người dùng nhập sai mật khẩu.
  2. Hệ thống hiển thị cảnh báo: *"Mật khẩu không chính xác. Vui lòng thử lại hoặc chọn tên khác."*

---

## Acceptance Criteria

#### AC-001 — Đăng ký tài khoản tham gia bình chọn
- **Given**: Người dùng đã tham gia sự kiện nhưng chưa có tài khoản định danh trong sự kiện đó.
- **When**: Người dùng mở chức năng đăng ký định danh.
- **Then**: Hệ thống cho phép người dùng đăng ký thông tin định danh để tham gia bình chọn.

#### AC-002 — Điều kiện tham gia sự kiện trước khi định danh
- **Given**: Người dùng chưa tham gia vào sự kiện.
- **When**: Người dùng tìm cách đăng ký định danh.
- **Then**: Hệ thống yêu cầu người dùng phải tham gia sự kiện thành công trước thì mới được phép thực hiện định danh.

#### AC-003 — Quy cách định dạng Username
- **Given**: Người dùng đang ở màn hình nhập thông tin định danh.
- **When**: Người dùng nhập username.
- **Then**: Hệ thống yêu cầu và kiểm tra username theo đúng format được quy định (độ dài, ký tự hợp lệ).

#### AC-004 — Tạo tài khoản thành công khi Username chưa tồn tại
- **Given**: Dữ liệu nhập hợp lệ và username chưa từng tồn tại trong sự kiện hiện tại.
- **When**: Người dùng nhấn xác nhận đăng ký.
- **Then**: Hệ thống tạo thành công tài khoản định danh cho người dùng trong sự kiện đó.

#### AC-005 — Xử lý khi Username đã tồn tại
- **Given**: Người dùng nhập username đã tồn tại trong sự kiện.
- **When**: Hệ thống kiểm tra trùng lặp.
- **Then**: Hệ thống thông báo username đã được sử dụng và yêu cầu nhập mật khẩu (nếu tài khoản có mật khẩu) để đăng nhập.

#### AC-006 — Phạm vi hiệu lực cục bộ của tài khoản
- **Given**: Người dùng đã đăng ký tài khoản định danh trong một sự kiện A.
- **When**: Người dùng truy cập sang sự kiện B khác.
- **Then**: Tài khoản định danh đó chỉ có hiệu lực trong sự kiện A mà người dùng đăng ký, không được xem là tài khoản dùng chung cho toàn hệ thống hay sự kiện B.

#### AC-007 — Chặn truy cập Dashboard khi chưa tham gia sự kiện
- **Given**: Người dùng chưa hoàn tất tham gia vào sự kiện.
- **When**: Người dùng cố tình truy cập Dashboard hoặc các nội dung thuộc sự kiện đó.
- **Then**: Hệ thống từ chối truy cập và yêu cầu người dùng tham gia sự kiện trước.

#### AC-008 — Cho phép tham gia kể cả khi sự kiện đã kết thúc
- **Given**: Sự kiện đã qua ngày kết thúc bình chọn.
- **When**: Người dùng truy cập và tham gia sự kiện.
- **Then**: Hệ thống vẫn cho phép tham gia và xem kết quả bình chọn của sự kiện.

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng đăng ký định danh trong sự kiện đã hoàn thành.
- [ ] 2. Đã implement validation cho các trường bắt buộc (Username bắt buộc, Password tùy chọn).
- [ ] 3. Đã xử lý trường hợp tạo thành công / trùng username / đăng nhập lại với mật khẩu.
- [ ] 4. Unit test đã được viết và pass toàn bộ.
- [ ] 5. Code đã được code review và phê duyệt.
- [ ] 6. QA đã test và pass toàn bộ 8 tiêu chí Acceptance Criteria.
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story.
- [ ] 8. Đã merge code vào branch theo quy định của team.
- [ ] 9. Đã deploy lên môi trường cần thiết (Staging/UAT).

---

## References

### Rules
- [`BR-06`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-06.md): Username phải là độc nhất trong phạm vi sự kiện.
- [`BR-08`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-08.md): Username và mật khẩu chỉ tồn tại trong phạm vi sự kiện, không dùng chung cho toàn hệ thống.
- [`BR-10`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-10.md): Khi đăng ký tài khoản định danh, username là bắt buộc, mật khẩu là không bắt buộc.

### Dependencies
- [`US-03`](file:///d:/VNZ/document-first-brief/meetly/UserStory/03-JoinEvent.md): Tham gia sự kiện.

---

## Notes

- Để tối ưu UX, nếu người tham gia chỉ muốn bình chọn nhanh mà không cần đặt mật khẩu, hệ thống chỉ cần 1 trường nhập "Tên của bạn" (Username).
