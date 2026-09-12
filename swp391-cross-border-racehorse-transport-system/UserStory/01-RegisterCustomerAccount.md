+3# STORY-001: Đăng ký tài khoản khách hàng

## Metadata

- **Story**: Là một Khách hàng (Chủ ngựa hoặc đại diện Câu lạc bộ đua), tôi muốn tự đăng ký tài khoản trực tuyến với thông tin cá nhân và tổ chức để có thể truy cập hệ thống và khởi tạo các yêu cầu vận chuyển ngựa đua.
- **Context**: Hệ thống logistics ngựa đua phục vụ cả khách hàng cá nhân (chủ sở hữu cá thể ngựa giá trị cao) và các tổ chức đua ngựa chuyên nghiệp (Racing Clubs, Trang trại nhân giống). Khách hàng cần một tài khoản định danh hợp lệ trên hệ thống để ký gửi yêu cầu, tải hồ sơ kiểm dịch và ký số biên bản bàn giao. Nhân sự nội bộ (Logistics Manager, Specialist, Coordinator, Driver) không tự đăng ký mà do quản trị viên cấp phát.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng truy cập vào cổng thông tin hệ thống qua giao diện Web hoặc Mobile.
- Người dùng chưa đăng nhập tài khoản nào trên trình duyệt hiện tại.
- Số điện thoại và Email đăng ký chưa tồn tại trên hệ thống ở trạng thái hoạt động (`ACTIVE`).

### Trigger

- Người dùng nhấp chọn liên kết hoặc nút "Đăng ký tài khoản" (`Register`) trên trang đăng nhập.

## Flow

### Main Flow

1. Người dùng chọn loại hình khách hàng: Cá nhân (`Individual Owner`) hoặc Câu lạc bộ/Doanh nghiệp (`Racing Club/Stable`).
2. Người dùng nhập các thông tin bắt buộc: Họ và tên, Email, Số điện thoại liên hệ, Mật khẩu, Xác nhận mật khẩu, Tên CLB/Trại ngựa (nếu là tổ chức) và Mã số thuế (tùy chọn).
3. Người dùng đồng ý với Điều khoản dịch vụ và Chính sách bảo mật vận chuyển động vật sống.
4. Người dùng nhấn nút "Đăng ký".
5. Hệ thống kiểm tra tính hợp lệ của dữ liệu đầu vào và kiểm tra trùng lặp Email, Số điện thoại trong cơ sở dữ liệu.
6. Hệ thống tạo bản ghi người dùng với trạng thái `PENDING_VERIFICATION`, đồng thời sinh mã xác thực OTP (6 chữ số ngẫu nhiên) có hiệu lực trong 5 phút.
7. Hệ thống gửi mã OTP tới Email và Số điện thoại đã đăng ký.
8. Hệ thống chuyển người dùng sang màn hình "Xác thực tài khoản" kèm bộ đếm ngược thời gian 300 giây.
9. Người dùng nhập mã OTP 6 chữ số và nhấn "Xác nhận".
10. Hệ thống kiểm tra mã OTP khớp và còn hiệu lực.
11. Hệ thống kích hoạt tài khoản (`status: ACTIVE`), khởi tạo bản ghi `CustomerProfile` tương ứng và tự động đăng nhập cấp JWT token cho người dùng.
12. Hệ thống hiển thị thông báo "Đăng ký tài khoản thành công" và điều hướng người dùng vào Trang chủ dành cho Khách hàng (`Customer Dashboard`).

### Alternative Flow

#### ALT-01: Người dùng yêu cầu gửi lại mã OTP

1. Tại bước 8 của Luồng chính, nếu chưa nhận được mã OTP sau 60 giây, người dùng nhấn nút "Gửi lại mã OTP" (`Resend OTP`).
2. Hệ thống vô hiệu hóa mã OTP cũ, tạo mã OTP mới và gửi lại qua Email/SMS.
3. Hệ thống đặt lại bộ đếm thời gian gửi lại (60 giây) và gia hạn thời gian hết hạn OTP (5 phút).
4. Người dùng tiếp tục từ bước 9 của Luồng chính.

### Exception Flow

#### EXC-01: Email hoặc Số điện thoại đã tồn tại trong hệ thống

1. Tại bước 5 của Luồng chính, hệ thống phát hiện Email hoặc Số điện thoại đã được đăng ký cho một tài khoản khác.
2. Hệ thống giữ nguyên các thông tin đã nhập trên biểu mẫu (ngoại trừ mật khẩu), hiển thị thông báo lỗi nổi bật: "Email hoặc Số điện thoại này đã được sử dụng. Vui lòng đăng nhập hoặc sử dụng chức năng Quên mật khẩu".
3. Người dùng có thể chỉnh sửa thông tin hoặc chọn chuyển hướng sang trang "Đăng nhập".

#### EXC-02: Mật khẩu không đáp ứng tiêu chuẩn an toàn

1. Tại bước 5 của Luồng chính, mật khẩu nhập vào không đạt độ dài tối thiểu (8 ký tự) hoặc thiếu các thành phần bắt buộc (chữ hoa, chữ thường, số, ký tự đặc biệt).
2. Hệ thống hiển thị cảnh báo chi tiết dưới ô nhập mật khẩu và chặn thao tác gửi yêu cầu.

#### EXC-03: Nhập sai mã OTP quá số lần quy định

1. Tại bước 10 của Luồng chính, người dùng nhập sai mã OTP quá 5 lần liên tiếp.
2. Hệ thống khóa phiên xác thực đăng ký hiện tại, hiển thị thông báo: "Bạn đã nhập sai mã OTP quá 5 lần. Phiên xác thực bị hủy, vui lòng đăng ký lại từ đầu".
3. Hệ thống xóa mã OTP tạm thời và điều hướng về biểu mẫu Đăng ký.

#### EXC-04: Mã OTP đã hết hạn

1. Người dùng nhập mã OTP sau khi thời gian 5 phút đã kết thúc.
2. Hệ thống hiển thị lỗi: "Mã xác thực đã hết hạn. Vui lòng nhấn 'Gửi lại mã' để nhận mã mới".

## Acceptance Criteria

#### AC-001: Đăng ký thành công tài khoản khách hàng cá nhân

- **Given**: Khách hàng ở màn hình Đăng ký, nhập đầy đủ Họ tên hợp lệ, Email chưa từng đăng ký, SĐT chưa từng đăng ký, mật khẩu hợp lệ và đồng ý điều khoản.
- **When**: Khách hàng nhấn nút "Đăng ký" và nhập đúng mã OTP được gửi về Email/SMS trong thời hạn hiệu lực.
- **Then**: Hệ thống tạo thành công bản ghi trong bảng `User` với vai trò `CUSTOMER` và `status = 'ACTIVE'`, đồng thời tạo bản ghi `CustomerProfile`.
- **And**: Trình duyệt lưu trữ JWT token và chuyển hướng tới `Customer Dashboard`.

#### AC-002: Chặn đăng ký khi trùng lặp Email hoặc SĐT

- **Given**: Một tài khoản với email `owner@equine.com` đã tồn tại trong hệ thống ở trạng thái `ACTIVE`.
- **When**: Người dùng mới nhập biểu mẫu đăng ký với email `owner@equine.com` và nhấn "Đăng ký".
- **Then**: Hệ thống không tạo tài khoản mới, không gửi OTP.
- **And**: Hiển thị thông báo lỗi "Email này đã được sử dụng trong hệ thống".

#### AC-003: Xác thực OTP hết hạn

- **Given**: Mã OTP được sinh lúc 10:00:00 và hết hạn lúc 10:05:00.
- **When**: Khách hàng nhập mã OTP vào lúc 10:05:01 và nhấn "Xác nhận".
- **Then**: Hệ thống từ chối kích hoạt tài khoản và hiển thị thông báo "Mã xác thực đã hết hiệu lực".

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Quy định định dạng mật khẩu và an toàn tài khoản
- BR-002: Quy định xác thực 2 bước (OTP) và chống lạm dụng gửi mã (Rate Limiting)

### Dependencies

- Dịch vụ gửi Email giao dịch (SMTP/SendGrid)
- Dịch vụ gửi tin nhắn SMS / OTP (Twilio hoặc nhà mạng viễn thông)

## Non-Functional

- Thời gian phản hồi gửi OTP: $\le 3$ giây kể từ khi nhấn Đăng ký.
- Mật khẩu lưu trữ trong CSDL bắt buộc băm bằng thuật toán an toàn (`Argon2id` hoặc `BCrypt` với Work Factor $\ge 12$).
- Áp dụng cơ chế Rate Limiting: Tối đa 3 yêu cầu đăng ký/gửi lại OTP từ cùng một địa chỉ IP trong vòng 1 phút.

## Out of Scope

- Tự đăng ký tài khoản cho các vai trò nhân sự nội bộ (Logistics Manager, Specialist, Coordinator, Driver) — các vai trò này do Admin hệ thống tạo trực tiếp từ bảng điều khiển quản trị.
- Đăng nhập thông qua mạng xã hội của bên thứ ba (Google OAuth, Facebook Login) trong phạm vi Sprint 1.
