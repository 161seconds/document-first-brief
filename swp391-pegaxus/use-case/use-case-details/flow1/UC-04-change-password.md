# UC-04: Thay đổi mật khẩu (Change Password)

## Metadata

- **Story**: Là Người dùng đã đăng nhập (Logged-in User), tôi muốn thay đổi mật khẩu cá nhân từ mục Cài đặt tài khoản trên ứng dụng Web responsive để tăng cường tính an toàn và bảo mật cho tài khoản của mình.
- **Context**: UC-04 cho phép Logged-in User chủ động cập nhật mật khẩu định kỳ hoặc khi nghi ngờ mật khẩu bị lộ trên giao diện Web responsive (BR-004). Hệ thống yêu cầu xác thực mật khẩu hiện tại trước khi áp dụng mật khẩu mới (BR-011). Tất cả các giao diện và thông báo phản hồi được hiển thị bằng Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Should
- **Status**: Review
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập thành công và có phiên làm việc còn hiệu lực.
- Người dùng truy cập giao diện Web Cài đặt tài khoản (Account Settings) $\rightarrow$ Bảo mật (Security) $\rightarrow$ Thay đổi mật khẩu (Change Password).

### Trigger

- Người dùng điền đầy đủ các thông tin mật khẩu và nhấp chọn nút "Update Password".

## Flow

### Main Flow

1. Người dùng chuyển đến mục Account Settings $\rightarrow$ Security $\rightarrow$ Change Password trên ứng dụng Web responsive.
2. Hệ thống hiển thị biểu mẫu Đổi mật khẩu gồm 3 trường nhập liệu: Current Password (Mật khẩu hiện tại), New Password (Mật khẩu mới), Confirm New Password (Xác nhận mật khẩu mới).
3. Người dùng nhập Mật khẩu hiện tại, Mật khẩu mới và Nhập lại mật khẩu mới vào các trường tương ứng.
4. Người dùng nhấn nút "Update Password".
5. Hệ thống kiểm tra dữ liệu đầu vào phía client: các trường không được để rỗng, Mật khẩu mới và Mật khẩu xác nhận phải trùng khớp, Mật khẩu mới đạt tiêu chuẩn độ dài/ký tự (BR-011).
6. Hệ thống gửi yêu cầu đổi mật khẩu lên hệ thống kèm thông tin xác thực phiên làm việc.
7. Hệ thống xác thực danh tính người dùng, kiểm tra đối chiếu Mật khẩu hiện tại nhập vào với mật khẩu lưu trong cơ sở dữ liệu (BR-011).
8. Hệ thống kiểm tra Mật khẩu mới không được trùng khớp với Mật khẩu hiện tại (BR-011).
9. Hệ thống tiến hành mã hóa và bảo mật Mật khẩu mới, lưu mật khẩu mới vào cơ sở dữ liệu, cập nhật mốc thời gian thay đổi mật khẩu.
10. Hệ thống gia hạn mã phiên làm việc mới để duy trì đăng nhập an toàn.
11. Hệ thống hiển thị thông báo thành công: "Password updated successfully." và xóa trắng dữ liệu trong biểu mẫu đổi mật khẩu.

### Alternative Flow

- N/A (Quy trình thực hiện trực tiếp).

### Exception Flow

#### EXC-01: Mật khẩu hiện tại nhập không chính xác

1. Tại bước 7 của Luồng chính, Mật khẩu hiện tại người dùng nhập không khớp với mật khẩu lưu trong cơ sở dữ liệu (BR-011).
2. Hệ thống hiển thị thông báo lỗi nổi bật ngay dưới ô Current Password: "Incorrect current password. Please try again."
3. Hệ thống không cập nhật cơ sở dữ liệu và giữ nguyên thông tin ô Mật khẩu mới để người dùng sửa lại ô Mật khẩu hiện tại.

#### EXC-02: Mật khẩu mới không đạt độ an toàn hoặc trùng với mật khẩu hiện tại

1. Tại bước 5 hoặc 8 của Luồng chính, Mật khẩu mới không đáp ứng chính sách an toàn (dưới 8 ký tự, thiếu chữ hoa/chữ số/ký tự đặc biệt) hoặc giống hệt Mật khẩu hiện tại (BR-011).
2. Hệ thống hiển thị lỗi: "New password must be at least 8 characters long, contain uppercase, lowercase, numbers, special characters, and be different from your current password."
3. Người dùng chỉnh sửa lại Mật khẩu mới hợp lệ và nhấn "Update Password".

#### EXC-03: Mật khẩu mới và Mật khẩu xác nhận không trùng khớp

1. Tại bước 5 của Luồng chính, ô New Password và Confirm New Password có giá trị khác nhau.
2. Hệ thống hiển thị lỗi dưới ô Confirm New Password: "Passwords do not match." và chặn gửi yêu cầu lên server.

## Acceptance Criteria

#### AC-001: Thay đổi mật khẩu thành công trên Web app

- **Given**: Người dùng đã đăng nhập trên ứng dụng Web responsive, nhập đúng Mật khẩu hiện tại và Mật khẩu mới hợp lệ khác mật khẩu cũ (BR-011).
- **When**: Người dùng nhấn "Update Password".
- **Then**: Hệ thống cập nhật thành công mật khẩu mới vào cơ sở dữ liệu và hiển thị thông báo "Password updated successfully."

#### AC-002: Chặn đổi mật khẩu khi sai Mật khẩu hiện tại

- **Given**: Người dùng nhập sai Mật khẩu hiện tại nhưng Mật khẩu mới hợp lệ.
- **When**: Người dùng nhấn "Update Password".
- **Then**: Hệ thống từ chối cập nhật cơ sở dữ liệu và hiển thị thông báo "Incorrect current password." (BR-011).

#### AC-003: Chặn đổi mật khẩu khi Mật khẩu mới giống Mật khẩu cũ

- **Given**: Người dùng nhập đúng Mật khẩu hiện tại nhưng ô Mật khẩu mới nhập giống hệt Mật khẩu hiện tại.
- **When**: Người dùng nhấn "Update Password".
- **Then**: Hệ thống từ chối cập nhật và hiển thị thông báo yêu cầu Mật khẩu mới phải khác Mật khẩu hiện tại (BR-011).

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-011: Quy định Tiêu chuẩn Mật khẩu & Xác thực Mật khẩu Hiện tại

### Dependencies

- Dịch vụ xác thực phiên làm việc

## Non-Functional

- Performance: Thời gian phản hồi xử lý đổi mật khẩu $\le 1.5$ giây.
- Security: Bảo mật mật khẩu lưu trữ theo chuẩn an toàn; Mọi giao diện và thông báo bằng Tiếng Anh (`English` - BR-005).
- Compatibility: Vận hành trên nền tảng Web đáp ứng (Responsive Web App - BR-004).

## Out of Scope

- Phát triển ứng dụng di động riêng (Native/Hybrid Mobile App) (BR-004).
- Thay đổi mật khẩu cho tài khoản người dùng khác (chức năng Quản trị viên).
