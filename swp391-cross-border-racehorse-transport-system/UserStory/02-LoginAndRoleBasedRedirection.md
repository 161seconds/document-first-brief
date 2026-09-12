# STORY-002: Đăng nhập và điều hướng phân quyền

## Metadata

- **Story**: Là một Người dùng hệ thống (Khách hàng, Quản lý điều hành, Chuyên viên thủ tục, Điều phối viên, Tài xế), tôi muốn đăng nhập bằng Email hoặc Số điện thoại và Mật khẩu để được cấp quyền truy cập và tự động chuyển hướng đến giao diện làm việc tương ứng với vai trò của mình.
- **Context**: Hệ thống vận chuyển ngựa đua đa tác nhân quy định 5 nhóm vai trò người dùng độc lập với chức năng và phạm vi nghiệp vụ riêng biệt. Quá trình đăng nhập bắt buộc phải xác thực danh tính an toàn, cấp JWT Access Token và Refresh Token, đồng thời điều hướng chính xác về màn hình chuyên biệt (ví dụ: Customer về trang quản lý đơn cá nhân; Manager về tổng quan điều hành; Specialist về bàn làm việc hồ sơ pháp lý; Coordinator về bản đồ lộ trình xe; Driver về lịch trình chặng di động).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã có tài khoản hợp lệ trên hệ thống ở trạng thái hoạt động (`status = 'ACTIVE'`).
- Người dùng đang ở màn hình Đăng nhập của ứng dụng (`/login`).

### Trigger

- Người dùng nhập thông tin đăng nhập và nhấp chọn nút "Đăng nhập" (`Login`).

## Flow

### Main Flow

1. Người dùng nhập tên tài khoản (Email hoặc Số điện thoại) và Mật khẩu vào biểu mẫu.
2. Người dùng có thể tùy chọn tích vào "Ghi nhớ đăng nhập" (`Remember Me`).
3. Người dùng nhấn nút "Đăng nhập".
4. Hệ thống kiểm tra định dạng dữ liệu đầu vào không được để trống.
5. Hệ thống truy vấn thông tin tài khoản theo Email/SĐT và kiểm tra trạng thái hoạt động của tài khoản.
6. Hệ thống đối soát mật khẩu nhập vào với mã băm mật khẩu (`passwordHash`) lưu trong CSDL.
7. Mật khẩu chính xác: Hệ thống sinh cặp mã xác thực `AccessToken` (thời hạn 60 phút) và `RefreshToken` (thời hạn 7 ngày hoặc 30 ngày nếu chọn Remember Me).
8. Hệ thống ghi nhận lịch sử đăng nhập (thời điểm, địa chỉ IP, User-Agent thiết bị).
9. Hệ thống kiểm tra vai trò người dùng (`role`) và điều hướng tự động:
   - `CUSTOMER`: Điều hướng đến Cổng dịch vụ khách hàng (`/customer/dashboard`).
   - `LOGISTICS_MANAGER`: Điều hướng đến Bảng điều khiển quản lý điều hành (`/manager/overview`).
   - `TRANSPORT_SPECIALIST`: Điều hướng đến Không gian xử lý hồ sơ kiểm dịch (`/specialist/dossiers`).
   - `FLEET_ROUTE_COORDINATOR`: Điều hướng đến Trung tâm điều phối phương tiện & lộ trình (`/coordinator/routes`).
   - `VEHICLE_DRIVER`: Điều hướng đến Bảng lịch trình chuyến đi của tài xế (`/driver/trips`).
10. Giao diện hiển thị lời chào kèm tên và vai trò của người dùng trên thanh tiêu đề (`Header`).

### Alternative Flow

#### ALT-01: Tài khoản đăng nhập trên ứng dụng di động của Tài xế (Driver Mobile App)

1. Tài xế mở ứng dụng di động dành riêng cho tài xế và nhập thông tin đăng nhập.
2. Hệ thống kiểm tra xác thực. Nếu vai trò không phải `VEHICLE_DRIVER`, hệ thống hiển thị thông báo: "Ứng dụng này chỉ dành cho Tài xế và Người đi kèm. Vui lòng đăng nhập trên cổng thông tin Web".
3. Nếu vai trò hợp lệ, hệ thống lưu token vào Secure Storage của điện thoại và điều hướng thẳng vào màn hình "Chuyến đi hiện tại" (`Current Trip`).

### Exception Flow

#### EXC-01: Sai thông tin đăng nhập (Tài khoản hoặc Mật khẩu không đúng)

1. Tại bước 6 của Luồng chính, thông tin tài khoản không tồn tại hoặc mật khẩu không trùng khớp.
2. Hệ thống tăng bộ đếm số lần đăng nhập thất bại liên tiếp (`failedLoginAttempts += 1`).
3. Hệ thống hiển thị thông báo lỗi chung: "Email/Số điện thoại hoặc mật khẩu không chính xác. Vui lòng kiểm tra lại". (Không tiết lộ tài khoản có tồn tại hay không vì lý do an ninh).
4. Giữ nguyên giá trị ô tài khoản, xóa trống ô mật khẩu để người dùng nhập lại.

#### EXC-02: Tài khoản bị khóa hoặc vô hiệu hóa (`status = 'BLOCKED' / 'INACTIVE'`)

1. Tại bước 5 của Luồng chính, tài khoản được tìm thấy nhưng trạng thái không phải `ACTIVE`.
2. Hệ thống từ chối xác thực và hiển thị thông báo: "Tài khoản của bạn đang bị tạm khóa hoặc chưa được kích hoạt. Vui lòng liên hệ bộ phận hỗ trợ khách hàng".

#### EXC-03: Nhập sai mật khẩu liên tiếp quá số lần cho phép (Brute-force protection)

1. Người dùng nhập sai mật khẩu 5 lần liên tiếp trong vòng 15 phút.
2. Hệ thống tự động khóa tạm thời tính năng đăng nhập của tài khoản trong 15 phút.
3. Hệ thống hiển thị thông báo: "Tài khoản tạm thời bị khóa do đăng nhập sai nhiều lần. Vui lòng thử lại sau 15 phút hoặc sử dụng tính năng Quên mật khẩu".

## Acceptance Criteria

#### AC-001: Đăng nhập thành công với vai trò Customer

- **Given**: Khách hàng có tài khoản `customer@equine.com` trạng thái `ACTIVE` và mật khẩu `Secret@123`.
- **When**: Khách hàng nhập đúng thông tin và nhấn nút "Đăng nhập".
- **Then**: Hệ thống trả về mã HTTP 200 kèm JWT token chứa claim `role: 'CUSTOMER'`.
- **And**: Trình duyệt điều hướng thành công tới `/customer/dashboard`.

#### AC-002: Đăng nhập thành công và phân quyền chính xác cho Logistics Manager

- **Given**: Quản lý có tài khoản `manager@logistics.com` vai trò `LOGISTICS_MANAGER`.
- **When**: Đăng nhập thành công với mật khẩu chính xác.
- **Then**: Hệ thống điều hướng trực tiếp về trang tổng quan quản trị `/manager/overview` với đầy đủ quyền hạn duyệt đơn.

#### AC-003: Chặn đăng nhập khi tài khoản bị khóa

- **Given**: Tài khoản `driver01@logistics.com` có `status = 'BLOCKED'`.
- **When**: Tài xế nhập đúng mật khẩu và nhấn Đăng nhập.
- **Then**: Hệ thống từ chối cấp token và hiển thị cảnh báo tài khoản bị tạm khóa.

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Quy định định dạng mật khẩu và an toàn tài khoản
- BR-003: Quy định phân quyền truy cập theo vai trò (Role-Based Access Control - RBAC)
- BR-004: Cơ chế chống tấn công dò quét mật khẩu (Brute-Force Protection)

### Dependencies

- CSDL Người dùng (`User`, `CustomerProfile`)
- Dịch vụ quản lý khóa ký số JWT (Token Issuer)

## Non-Functional

- Thời gian phản hồi API đăng nhập: $\le 500$ ms.
- Token được mã hóa an toàn theo chuẩn `HMAC-SHA256` hoặc `RSA-256`.
- Cookie chứa `RefreshToken` phải được gắn cờ `HttpOnly`, `Secure` và `SameSite=Strict`.

## Out of Scope

- Xác thực sinh trắc học (Vân tay, FaceID) trên ứng dụng Web trong Sprint 1 (chỉ hỗ trợ trên Mobile Driver ở pha nâng cao).
- Tích hợp đăng nhập một lần (Single Sign-On - SSO) qua hệ thống bên thứ ba.
