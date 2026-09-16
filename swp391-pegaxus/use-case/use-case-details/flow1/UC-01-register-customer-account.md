# UC-01: Đăng ký tài khoản khách hàng qua Google (Customer Registration via Google Account)

## Metadata

- **Story**: Là Khách (Guest), tôi muốn đăng ký và đăng nhập tài khoản trực tuyến nhanh chóng thông qua Tài khoản Google (Google Account) trên ứng dụng Web responsive để có thể truy cập hệ thống và khởi tạo các yêu cầu vận chuyển ngựa đua xuyên biên giới Châu Âu.
- **Context**: Hệ thống Logistics vận chuyển ngựa đua tối giản hóa quy trình đăng ký cho khách hàng (Guest -> Customer) bằng cách áp dụng xác thực qua Google OAuth 2.0 (BR-001). Toàn bộ quá trình xác minh thông tin cá nhân và email được trừu tượng hóa thông qua Google Identity Service. Nhân sự nội bộ (Logistics Manager, Transport Specialist, Coordinator, Driver, Escort) không tự đăng ký mà do Quản trị viên cấp phát (BR-002). Hệ thống vận hành duy nhất trên nền tảng Responsive Web App (BR-004) tại thị trường Châu Âu (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng truy cập cổng thông tin hệ thống trên ứng dụng Web đáp ứng (Responsive Web App) thông qua trình duyệt web trên máy tính, điện thoại hoặc máy tính bảng.
- Khách hàng sở hữu một Tài khoản Google (`@gmail.com` hoặc Google Workspace) đang hoạt động.

### Trigger

- Khách hàng nhấp chọn nút "Continue with Google" hoặc "Sign Up with Google" trên màn hình đăng ký/đăng nhập của Web app.

## Flow

### Main Flow

1. Khách hàng nhấp chọn nút "Continue with Google" tại màn hình Đăng ký / Đăng nhập của ứng dụng Web responsive.
2. Hệ thống chuyển hướng trình duyệt của khách hàng sang cửa sổ xác thực ủy quyền của Google (Google OAuth 2.0 Authorization Endpoint).
3. Khách hàng chọn tài khoản Google của mình và đồng ý cấp quyền chia sẻ thông tin hồ sơ cơ bản (Full Name, Email, Profile Avatar).
4. Google thực hiện xác thực người dùng và trả về mã xác thực `ID Token` hợp lệ kèm thông tin hồ sơ cho hệ thống.
5. Hệ thống tiếp nhận `ID Token`, kiểm tra tính hợp lệ và truy vấn CSDL theo địa chỉ Email nhận được:
   - **Nếu Email chưa tồn tại trong CSDL**: Hệ thống tự động tạo bản ghi người dùng mới với vai trò `CUSTOMER`, trạng thái `ACTIVE`, đồng thời khởi tạo bản ghi Customer Profile tương ứng với thông tin lấy từ Google.
   - **Nếu Email đã tồn tại**: Hệ thống cập nhật các thông tin hồ sơ mới nhất từ Google (nếu có).
6. Hệ thống khởi tạo và cấp cặp JWT Session Token (Access Token và Refresh Token) cho phiên làm việc.
7. Hệ thống hiển thị thông báo thành công "Account authenticated successfully via Google" và tự động điều hướng khách hàng vào Trang chủ Khách hàng.

### Alternative Flow

#### ALT-01: Đăng nhập tự động một chạm (Google One-Tap Login)

1. Tại màn hình Đăng nhập của Web app, nếu khách hàng đã đăng nhập sẵn tài khoản Google trên trình duyệt, hệ thống hiển thị hộp thoại Google One-Tap.
2. Khách hàng nhấp chọn "Continue as [Google Name]".
3. Hệ thống tiếp nhận Token và thực hiện từ bước 4 đến bước 7 của Luồng chính.

### Exception Flow

#### EXC-01: Khách hàng hủy bỏ thao tác ủy quyền trên Google

1. Tại bước 3 của Luồng chính, khách hàng nhấp chọn "Cancel" hoặc đóng cửa sổ xác thực của Google.
2. Google trả về mã trạng thái hủy thao tác (`access_denied`).
3. Hệ thống giữ khách hàng ở lại màn hình Đăng ký / Đăng nhập của Web app và hiển thị thông báo nhẹ: "Google authentication was canceled. Please try again."

#### EXC-02: Lỗi dịch vụ Google OAuth hoặc Token không hợp lệ

1. Tại bước 4 hoặc 5 của Luồng chính, dịch vụ Google gặp sự cố gián đoạn hoặc `ID Token` trả về bị lỗi/hết hạn.
2. Hệ thống từ chối đăng nhập và hiển thị thông báo lỗi: "Unable to authenticate with Google at this time. Please try again later."

#### EXC-03: Email Google thuộc tài khoản bị khóa trên hệ thống

1. Tại bước 5 của Luồng chính, địa chỉ Email trả về từ Google khớp với một tài khoản đã bị khóa (`status == SUSPENDED`) trên hệ thống.
2. Hệ thống từ chối cấp JWT Token và hiển thị thông báo: "Your account has been suspended. Please contact support."

## Acceptance Criteria

#### AC-001: Đăng ký và đăng nhập thành công qua Google Account

- **Given**: Khách hàng truy cập ứng dụng Web responsive và nhấp chọn "Continue with Google".
- **When**: Khách hàng hoàn tất chọn tài khoản Google hợp lệ và đồng ý ủy quyền.
- **Then**: Hệ thống tự động khởi tạo tài khoản `CUSTOMER` ở trạng thái `ACTIVE`, cấp JWT Token và điều hướng vào `Customer Dashboard`.

#### AC-002: Xử lý khi khách hàng hủy xác thực Google

- **Given**: Khách hàng đang ở cửa sổ xác thực của Google.
- **When**: Khách hàng nhấp nút "Cancel" hoặc đóng cửa sổ xác thực.
- **Then**: Hệ thống không tạo tài khoản, giữ người dùng ở lại trang Đăng nhập Web app và hiển thị thông báo "Google authentication was canceled."

#### AC-003: Chặn đăng nhập khi tài khoản bị khóa

- **Given**: Tài khoản khách hàng có email Google `david.michael@equine-club.eu` đang bị khóa (`SUSPENDED`).
- **When**: Khách hàng đăng nhập qua Google với email này.
- **Then**: Hệ thống từ chối đăng nhập và hiển thị thông báo "Your account has been suspended."

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Đăng ký & Đăng nhập Khách hàng qua Google Account (`Google OAuth 2.0`)
- BR-002: Cấp phát Tài khoản Nhân sự Nội bộ
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- Dịch vụ xác thực Google Identity Services (Google OAuth 2.0 API)

## Non-Functional

- Performance: Thời gian phản hồi xử lý xác thực Google và khởi tạo phiên $\le 2$ giây.
- Security: Xác thực mã token Google bằng chuẩn RS256/JWKS; Hệ thống vận hành duy nhất trên Web responsive; Ngôn ngữ giao diện hoàn toàn bằng Tiếng Anh (`English`).
- Compatibility: Hoạt động tương thích mượt mà trên trình duyệt Web của thiết bị máy tính, máy tính bảng và điện thoại di động (Responsive Web App).

## Out of Scope

- Phát triển ứng dụng di động độc lập (Native/Hybrid Mobile App) — hệ thống chỉ chạy trên Responsive Web App (BR-004).
- Tự đăng ký cho nhân sự nội bộ (Logistics Manager, Specialist, Coordinator, Driver, Escort) (BR-002).
- Đăng ký qua biểu mẫu Email/Password thủ công hoặc OTP SMS (chỉ sử dụng Google OAuth cho Khách hàng).
