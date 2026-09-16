# UC-02: Đăng nhập và phân quyền (Login & Role-Based Authorization)

## Metadata

- **Story**: Là Khách (Guest) hoặc Người dùng đã đăng nhập (Logged-in User), tôi muốn đăng nhập vào ứng dụng Web responsive bằng Email/Password hoặc Google Account để hệ thống xác thực danh tính, cấp phiên làm việc và điều hướng trực tiếp đến giao diện chức năng phù hợp với vai trò của tôi.
- **Context**: UC-02 là cổng xác thực chính cho các tác nhân hệ thống trên nền tảng Responsive Web App (BR-004). Khách (Guest) có thể chọn đăng nhập qua Google Account (BR-001) hoặc Email/Password. Nhân sự nội bộ được Admin cấp tài khoản để đăng nhập bằng Email/Password (BR-002). Sau khi xác thực thành công, hệ thống phân quyền tự động dựa trên vai trò của người dùng thành Logged-in User với các role tương ứng (BR-003). Hệ thống kiểm tra trạng thái tài khoản hoạt động (BR-008), áp dụng thông báo lỗi bảo mật chung (BR-007), tự động khóa tài khoản khi nhập sai 5 lần (BR-006) và hiển thị duy nhất ngôn ngữ Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Review
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã có tài khoản tồn tại trên cơ sở dữ liệu ở trạng thái hoạt động (BR-008).
- Người dùng truy cập trang Đăng nhập hệ thống trên trình duyệt Web của máy tính, điện thoại hoặc máy tính bảng (BR-004).

### Trigger

- Người dùng nhập thông tin xác thực (Email, Mật khẩu) hoặc nhấp chọn "Continue with Google" trên màn hình Đăng nhập.

## Flow

### Main Flow

1. Người dùng truy cập màn hình Đăng nhập của ứng dụng Web responsive.
2. Với Khách hàng (Customer): Có thể nhấp chọn "Continue with Google" (chuyển sang xử lý theo BR-001) hoặc nhập Email & Password. Với Nhân sự nội bộ: Nhập Email và Password được Admin cấp phát (BR-002).
3. Người dùng nhập Email, Password và nhấn nút "Sign In".
4. Hệ thống kiểm tra dữ liệu đầu vào phía client (Email không rỗng, Mật khẩu không rỗng).
5. Hệ thống gửi thông tin xác thực đăng nhập lên hệ thống.
6. Hệ thống truy vấn cơ sở dữ liệu tìm kiếm tài khoản theo Email và kiểm tra tính chính xác của mật khẩu.
7. Hệ thống kiểm tra trạng thái tài khoản phải đang ở trạng thái hoạt động (BR-008).
8. Hệ thống khởi tạo và cấp phiên làm việc (Session Token) chứa thông tin định danh và vai trò của người dùng (BR-003).
9. Hệ thống cập nhật mốc thời gian đăng nhập gần nhất và ghi nhật ký truy cập.
10. Hệ thống lưu mã phiên làm việc an toàn trên trình duyệt người dùng.
11. Hệ thống giải mã vai trò và tự động điều hướng người dùng đến giao diện trang chủ phù hợp với vai trò và thiết bị đang sử dụng (BR-003, BR-004):
    - **Customer** (David Michael) $\rightarrow$ Trang chủ Khách hàng (Quản lý yêu cầu & theo dõi hành trình)
    - **Logistics Manager** (Trần Anh Tuấn) $\rightarrow$ Trang quản lý Logistics (Bảng điều phối tổng thể & phê duyệt)
    - **Transport Specialist** (Lê Minh Hương) $\rightarrow$ Trang quản lý Thủ tục & Kiểm dịch (Quản lý hồ sơ & kiểm dịch)
    - **Fleet & Route Coordinator** (Phạm Hoàng Nam) $\rightarrow$ Trang điều phối Đội xe & Lộ trình (Lập lộ trình & điều phối xe)
    - **Vehicle Driver** (Nguyễn Văn Hùng) $\rightarrow$ Trang lịch trình Tài xế (Xem lịch trình vận chuyển & báo cáo mốc di chuyển trên trình duyệt di động)
    - **Escort** (Đặng Thái Bình) $\rightarrow$ Trang nhật ký Nhân viên đi kèm (Ghi nhật ký sức khỏe ngựa & báo cáo sự cố y tế)
12. Hệ thống hiển thị thông báo "Login successful. Welcome back!" và tải dữ liệu bảng điều khiển tương ứng (BR-005).

### Alternative Flow

#### ALT-01: Đăng nhập tự động duy trì phiên làm việc (Session Restoration)

1. Người dùng mở lại ứng dụng Web khi phiên làm việc ngắn hạn hết hạn nhưng phiên làm việc dài hạn vẫn còn hiệu lực.
2. Hệ thống tự động gửi yêu cầu gia hạn phiên làm việc lên hệ thống.
3. Hệ thống xác nhận phiên hợp lệ, cấp mã phiên mới và duy trì đăng nhập mà không yêu cầu người dùng nhập lại mật khẩu.

### Exception Flow

#### EXC-01: Sai Email hoặc Mật khẩu

1. Tại bước 6 của Luồng chính, thông tin Email không tồn tại hoặc mật khẩu không khớp.
2. Hệ thống tăng số lần đăng nhập sai của tài khoản.
3. Hệ thống hiển thị cảnh báo lỗi bảo mật chung (BR-007): "Invalid email or password. Please try again."
4. Người dùng có thể thử lại hoặc chọn "Forgot Password?".

#### EXC-02: Tài khoản bị khóa hoặc không ở trạng thái hoạt động

1. Tại bước 7 của Luồng chính, trạng thái tài khoản không phải ở trạng thái hoạt động (BR-008).
2. Hệ thống hiển thị thông báo lỗi: "Your account is not active. Please contact customer support."
3. Hệ thống từ chối cấp phiên làm việc và chặn truy cập.

#### EXC-03: Khóa tài khoản tạm thời do nhập sai quá số lần quy định

1. Số lần đăng nhập sai liên tiếp đạt 5 lần (BR-006).
2. Hệ thống tạm thời khóa khả năng đăng nhập của tài khoản trong 15 phút.
3. Hệ thống hiển thị thông báo: "Account temporarily locked due to 5 consecutive failed login attempts. Please try again after 15 minutes or reset your password."

## Acceptance Criteria

#### AC-001: Đăng nhập thành công và điều hướng đúng vai trò trên Web app

- **Given**: Người dùng có tài khoản hợp lệ ở trạng thái hoạt động với vai trò Tài xế (Vehicle Driver) (BR-003, BR-008).
- **When**: Người dùng truy cập ứng dụng Web responsive trên trình duyệt điện thoại, nhập đúng Email, Mật khẩu và nhấn "Sign In".
- **Then**: Hệ thống cấp phiên làm việc hợp lệ và điều hướng thành công đến Trang lịch trình Tài xế.

#### AC-002: Từ chối đăng nhập khi sai thông tin xác thực

- **Given**: Người dùng nhập Email đúng nhưng Mật khẩu sai.
- **When**: Người dùng nhấn "Sign In".
- **Then**: Hệ thống không cấp phiên làm việc, tăng biến đếm đăng nhập sai và hiển thị thông báo lỗi bảo mật chung "Invalid email or password." (BR-007).

#### AC-003: Khóa tài khoản khi vượt quá 5 lần nhập sai

- **Given**: Tài khoản đã có 4 lần đăng nhập sai liên tiếp.
- **When**: Người dùng tiếp tục nhập sai mật khẩu lần thứ 5 (BR-006).
- **Then**: Hệ thống khóa tài khoản trong 15 phút và hiển thị thông báo khóa tài khoản tạm thời.

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Đăng ký & Đăng nhập Khách hàng qua Google Account
- BR-002: Cấp phát Tài khoản Nhân sự Nội bộ
- BR-003: Phân quyền Truy cập theo Vai trò (RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-006: Quy định Khóa Tài khoản Tạm thời khi Đăng nhập Sai
- BR-007: Quy định Thông báo Lỗi Bảo mật khi Đăng nhập Không Thành công
- BR-008: Quy định Kiểm tra Trạng thái Tài khoản khi Đăng nhập

### Dependencies

- Dịch vụ xác thực và phân quyền tài khoản

## Non-Functional

- Performance: Thời gian phản hồi xử lý đăng nhập và cấp phiên làm việc $\le 2$ giây.
- Security: Chống tấn công Brute-force bằng cơ chế Lockout (5 lần sai/15 phút lock - BR-006); Bảo mật mã phiên làm việc; Mọi giao diện và thông báo bằng Tiếng Anh (`English` - BR-005).
- Compatibility: Hệ thống chạy duy nhất trên nền tảng Web đáp ứng (Responsive Web App), hoạt động mượt mà trên trình duyệt máy tính và thiết bị di động (BR-004).

## Out of Scope

- Phát triển ứng dụng di động riêng (Native/Hybrid Mobile App) (BR-004).
- Đăng nhập bằng SMS OTP / Mã cuộc gọi điện thoại.
- Xác thực sinh trắc học (Vân tay / FaceID).
