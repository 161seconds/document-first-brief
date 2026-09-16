# UC-03: Quên / Khôi phục mật khẩu (Forgot / Reset Password)

## Metadata

- **Story**: Là Khách (Guest), khi bị quên mật khẩu, tôi muốn yêu cầu gửi liên kết khôi phục mật khẩu qua Email trên ứng dụng Web responsive để có thể tự thiết lập lại mật khẩu mới và lấy lại quyền truy cập hệ thống.
- **Context**: UC-03 hỗ trợ Guest tự khôi phục mật khẩu một cách an toàn thông qua Email giao dịch trên ứng dụng Web responsive (BR-004, BR-009). Để ngăn chặn hành vi dò quét danh sách Email người dùng, hệ thống luôn hiển thị một thông báo thành công chung bất kể Email có tồn tại trong cơ sở dữ liệu hay không (BR-007). Hệ thống không sử dụng SMS hay cuộc gọi thoại cho tính năng này.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Review
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng truy cập màn hình Đăng nhập của ứng dụng Web responsive trên trình duyệt máy tính hoặc thiết bị di động (BR-004).
- Người dùng có địa chỉ Email đã từng đăng ký/cấp phát trên hệ thống.

### Trigger

- Người dùng nhấp vào liên kết "Forgot Password?" (Quên mật khẩu?) tại màn hình Đăng nhập của Web app.

## Flow

### Main Flow

1. Người dùng nhấp chọn liên kết "Forgot Password?".
2. Hệ thống hiển thị giao diện Web "Reset Password Request" gồm ô nhập Email và nút "Send Reset Link".
3. Người dùng nhập Email tài khoản của mình và nhấn "Send Reset Link".
4. Hệ thống kiểm tra định dạng Email phía client.
5. Hệ thống gửi yêu cầu khôi phục mật khẩu lên hệ thống.
6. Hệ thống truy vấn cơ sở dữ liệu kiểm tra sự tồn tại của Email:
   - Nếu Email tồn tại và tài khoản đang ở trạng thái hoạt động: Hệ thống sinh mã xác thực đặt lại mật khẩu ngẫu nhiên an toàn, lưu mã xác thực kèm thời hạn hiệu lực 15 phút (900 giây) vào cơ sở dữ liệu, và gửi một Email chứa liên kết khôi phục mật khẩu đến địa chỉ Email của người dùng (BR-009).
   - Nếu Email không tồn tại: Hệ thống bỏ qua bước gửi Email nhưng vẫn tiếp tục bước 7 (không báo lỗi Email không tồn tại - BR-007).
7. Hệ thống hiển thị thông báo bảo mật chung trên giao diện Web (BR-007): "If your email address is registered in our system, a password reset link has been sent. Please check your inbox and spam folder."
8. Người dùng kiểm tra hộp thư Email và nhấp vào liên kết "Reset Password".
9. Trình duyệt mở màn hình Web "Set New Password". Hệ thống tự động gửi yêu cầu xác thực tính hợp lệ của mã khôi phục.
10. Hệ thống xác nhận mã khôi phục tồn tại, còn hạn (dưới 15 phút) và chưa từng sử dụng (BR-009), hiển thị biểu mẫu nhập Mật khẩu mới và Xác nhận mật khẩu mới.
11. Người dùng nhập Mật khẩu mới và Xác nhận mật khẩu mới, sau đó nhấn "Update Password".
12. Hệ thống kiểm tra mật khẩu mới đạt tiêu chuẩn an toàn, tiến hành bảo mật mật khẩu mới, lưu mật khẩu vào cơ sở dữ liệu, đánh dấu mã khôi phục đã sử dụng, và hủy toàn bộ các phiên làm việc hiện tại của tài khoản (BR-009).
13. Hệ thống hiển thị thông báo "Password reset successfully. Please log in with your new password." và điều hướng người dùng quay lại màn hình Đăng nhập của Web app.

### Alternative Flow

#### ALT-01: Yêu cầu gửi lại Email khôi phục (Resend Reset Link)

1. Tại bước 7 của Luồng chính, nếu sau 60 giây người dùng chưa nhận được Email, người dùng nhấn nút "Resend Link".
2. Hệ thống kiểm tra giới hạn thời gian (Rate limit 60s - BR-010), vô hiệu hóa mã khôi phục cũ, tạo mã mới và gửi lại Email khôi phục.
3. Người dùng tiếp tục từ bước 8 của Luồng chính.

### Exception Flow

#### EXC-01: Format Email không hợp lệ

1. Tại bước 4 của Luồng chính, Email nhập vào sai định dạng syntax.
2. Hệ thống hiển thị thông báo lỗi ngay dưới ô nhập: "Please enter a valid email address." và chặn gửi yêu cầu.

#### EXC-02: Mã khôi phục không hợp lệ, đã hết hạn hoặc đã sử dụng

1. Tại bước 10 của Luồng chính, người dùng nhấp vào liên kết chứa mã khôi phục đã quá 15 phút hoặc đã được sử dụng trước đó (BR-009).
2. Hệ thống hiển thị màn hình Web thông báo lỗi: "This password reset link is invalid or has expired. Please request a new link." kèm nút "Request New Link".
3. Người dùng nhấp chọn "Request New Link" để quay lại bước 2 của Luồng chính.

#### EXC-03: Mật khẩu mới không đáp ứng tiêu chuẩn an toàn hoặc giống mật khẩu cũ

1. Tại bước 12 của Luồng chính, mật khẩu mới không đạt quy định về độ dài/ký tự hoặc trùng khớp với mật khẩu cũ.
2. Hệ thống hiển thị cảnh báo lỗi: "New password must meet security requirements and be different from your current password."
3. Người dùng nhập lại mật khẩu mới hợp lệ và nhấn "Update Password".

## Acceptance Criteria

#### AC-001: Khôi phục mật khẩu thành công qua Email link trên Web app

- **Given**: Người dùng có tài khoản đang hoạt động với email `tuan.tran@logistics-racehorse.eu`.
- **When**: Người dùng nhập email trên Web app, nhận Email link trong vòng 15 phút, nhấp chọn link và nhập mật khẩu mới hợp lệ (BR-009).
- **Then**: Hệ thống lưu mật khẩu mới, vô hiệu hóa mã khôi phục, hủy các phiên làm việc cũ và chuyển hướng về trang Login với thông báo thành công.

#### AC-002: Bảo mật thông tin Email khi nhập Email không tồn tại

- **Given**: Người dùng nhập email `unknown@domain.com` không có trong cơ sở dữ liệu hệ thống.
- **When**: Người dùng nhấn "Send Reset Link".
- **Then**: Hệ thống vẫn hiển thị thông báo chung "If your email address is registered...", không gửi Email và không tiết lộ sự không tồn tại của tài khoản (BR-007).

#### AC-003: Chặn sử dụng mã khôi phục hết hạn

- **Given**: Mã khôi phục được sinh lúc 09:00:00 và hết hạn lúc 09:15:00 (BR-009).
- **When**: Người dùng nhấp vào link khôi phục lúc 09:15:01.
- **Then**: Hệ thống từ chối hiển thị biểu mẫu đổi mật khẩu và thông báo "This password reset link is invalid or has expired."

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-007: Quy định Thông báo Lỗi Bảo mật khi Đăng nhập Không Thành công
- BR-009: Quy định Khôi phục Mật khẩu qua Email Link
- BR-010: Quy định Giới hạn Tần suất Yêu cầu Khôi phục Mật khẩu (Rate Limiting)

### Dependencies

- Dịch vụ gửi Email giao dịch

## Non-Functional

- Performance: Thời gian phản hồi gửi Email khôi phục $\le 3$ giây kể từ khi nhấn yêu cầu.
- Security: Mã khôi phục an toàn, thời hạn tối đa 15 phút, chỉ dùng 1 lần (BR-009); Toàn bộ giao diện Web và Email gửi đi bằng Tiếng Anh (`English` - BR-005).
- Compatibility: Vận hành trên Responsive Web App (BR-004).

## Out of Scope

- Phát triển ứng dụng di động riêng (Native/Hybrid Mobile App) (BR-004).
- Gửi mã OTP khôi phục qua SMS hoặc cuộc gọi thoại.
- Khôi phục mật khẩu bằng câu hỏi bảo mật.
