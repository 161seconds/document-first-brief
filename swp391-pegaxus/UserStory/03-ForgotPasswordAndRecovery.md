# STORY-003: Quên và khôi phục mật khẩu

## Metadata

- **Story**: Là một Người dùng hệ thống, tôi muốn yêu cầu khôi phục quyền truy cập tài khoản khi bị quên mật khẩu thông qua mã xác thực gửi về Email hoặc Số điện thoại để có thể thiết lập mật khẩu mới một cách an toàn.
- **Context**: Việc quên mật khẩu là tình huống phát sinh thường xuyên của cả khách hàng và các bên điều hành hiện trường. Quy trình khôi phục yêu cầu kiểm tra danh tính thông qua OTP đa kênh (Email/SMS) và token khôi phục dùng một lần (`Reset Token`) có thời hạn ngắn để ngăn chặn nguy cơ chiếm quyền tài khoản trái phép.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã có tài khoản tồn tại trên hệ thống và tài khoản đang ở trạng thái `ACTIVE`.
- Người dùng không đăng nhập và đang ở trang "Quên mật khẩu" (`/forgot-password`).

### Trigger

- Người dùng nhấn vào liên kết "Quên mật khẩu?" (`Forgot password?`) trên màn hình đăng nhập.

## Flow

### Main Flow

1. Người dùng nhập Email hoặc Số điện thoại đã đăng ký tài khoản.
2. Người dùng nhấn nút "Gửi mã khôi phục" (`Send Reset Code`).
3. Hệ thống kiểm tra định dạng và truy vấn xem thông tin có thuộc về tài khoản hợp lệ nào hay không.
4. Nếu tài khoản tồn tại và hợp lệ:
   - Hệ thống sinh mã xác thực OTP 6 chữ số với thời hạn 5 phút.
   - Hệ thống lưu bản ghi vào bảng `OtpVerification` với `otpType = 'PASSWORD_RESET'`.
   - Hệ thống gửi OTP tới Email hoặc Số điện thoại tương ứng.
5. Hệ thống chuyển hướng người dùng sang màn hình "Xác thực mã khôi phục" và kích hoạt đồng hồ đếm ngược 300 giây.
6. Người dùng nhập mã OTP 6 chữ số và nhấn "Xác minh".
7. Hệ thống xác minh mã OTP hợp lệ, chưa được sử dụng (`isUsed = false`) và chưa hết hạn.
8. Hệ thống đánh dấu OTP đã sử dụng (`isUsed = true`), đồng thời cấp một `PasswordResetToken` ngắn hạn (thời hạn 10 phút) cho phiên trình duyệt hiện tại.
9. Hệ thống chuyển người dùng sang màn hình "Đặt lại mật khẩu mới" (`/reset-password`).
10. Người dùng nhập Mật khẩu mới và Nhập lại mật khẩu mới.
11. Người dùng nhấn nút "Cập nhật mật khẩu".
12. Hệ thống kiểm tra mật khẩu mới thỏa mãn chính sách bảo mật và không trùng với mật khẩu gần nhất.
13. Hệ thống băm mật khẩu mới và cập nhật vào trường `passwordHash` của bảng `User`.
14. Hệ thống hủy tất cả các phiên đăng nhập (Tokens) hiện tại của tài khoản trên mọi thiết bị.
15. Hệ thống hiển thị thông báo thành công: "Mật khẩu đã được thay đổi thành công. Vui lòng đăng nhập bằng mật khẩu mới" và chuyển hướng về màn hình Đăng nhập.

### Alternative Flow

#### ALT-01: Yêu cầu gửi lại mã OTP khôi phục

1. Tại bước 5 của Luồng chính, sau 60 giây nếu chưa nhận được mã, người dùng nhấn "Gửi lại mã OTP".
2. Hệ thống hủy mã OTP cũ, tạo mã mới và gửi lại đến kênh liên lạc ban đầu.
3. Người dùng tiếp tục từ bước 6 của Luồng chính.

### Exception Flow

#### EXC-01: Email hoặc Số điện thoại không tồn tại trong hệ thống

1. Tại bước 3 của Luồng chính, thông tin người dùng nhập không khớp với bất kỳ tài khoản nào.
2. Vì lý do an ninh (chống dò quét tài khoản người dùng), hệ thống vẫn hiển thị thông báo: "Nếu thông tin của bạn khớp với tài khoản trên hệ thống, mã xác thực sẽ được gửi trong vài giây tới".
3. Hệ thống không tạo OTP và không thực hiện lệnh gửi nào.

#### EXC-02: Nhập sai mã OTP quá 5 lần

1. Người dùng nhập sai mã OTP khôi phục 5 lần liên tiếp.
2. Hệ thống vô hiệu hóa phiên khôi phục hiện tại, hiển thị thông báo lỗi: "Mã xác thực không chính xác quá 5 lần. Phiên làm việc đã bị hủy, vui lòng thực hiện lại yêu cầu từ đầu".

#### EXC-03: Mật khẩu mới trùng với mật khẩu cũ

1. Tại bước 12 của Luồng chính, mật khẩu mới người dùng nhập trùng khớp với mật khẩu cũ đang sử dụng.
2. Hệ thống hiển thị lỗi: "Mật khẩu mới không được trùng với mật khẩu hiện tại. Vui lòng chọn mật khẩu khác".

## Acceptance Criteria

#### AC-001: Khôi phục mật khẩu thành công bằng mã OTP

- **Given**: Người dùng có tài khoản hợp lệ với email `jockey@stable.com`.
- **When**: Người dùng yêu cầu khôi phục, nhập đúng mã OTP được gửi về email và cung cấp mật khẩu mới đạt chuẩn bảo mật.
- **Then**: CSDL cập nhật mật khẩu băm mới, token đặt lại mật khẩu bị thu hồi và người dùng đăng nhập thành công với mật khẩu mới.

#### AC-002: Đăng xuất mọi phiên cũ khi đặt lại mật khẩu

- **Given**: Tài khoản người dùng đang đăng nhập trên 2 thiết bị khác nhau.
- **When**: Người dùng hoàn tất việc đổi mật khẩu thông qua luồng Quên mật khẩu.
- **Then**: Toàn bộ `RefreshToken` cũ bị thu hồi trong CSDL; các thiết bị đang hoạt động bị buộc đăng xuất ngay trong lần gửi request tiếp theo.

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Quy định định dạng mật khẩu và an toàn tài khoản
- BR-002: Quy định xác thực 2 bước (OTP) và chống lạm dụng gửi mã (Rate Limiting)

### Dependencies

- Dịch vụ gửi Email giao dịch (SMTP/SendGrid)
- Dịch vụ gửi SMS (Twilio/Mock SMS)

## Non-Functional

- `PasswordResetToken` phải là chuỗi ngẫu nhiên bảo mật cao (Cryptographically Secure Pseudo-Random Number Generator - CSPRNG) với độ dài $\ge 32$ bytes.
- Giới hạn tần suất: Tối đa 3 yêu cầu quên mật khẩu cho một tài khoản trong vòng 1 giờ.

## Out of Scope

- Khôi phục mật khẩu thông qua liên kết ma thuật (Magic Link) một chạm (sẽ cân nhắc ở phiên bản tiếp theo).
