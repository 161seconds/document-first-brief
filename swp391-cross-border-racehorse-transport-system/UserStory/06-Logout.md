# STORY-006: Đăng xuất hệ thống

## Metadata

- **Story**: Là một Người dùng đang đăng nhập, tôi muốn chủ động đăng xuất khỏi hệ thống để chấm dứt phiên làm việc hiện tại, vô hiệu hóa mã xác thực truy cập và đảm bảo an toàn cho tài khoản khi không còn sử dụng thiết bị.
- **Context**: Để tránh nguy cơ bị truy cập trái phép khi người dùng sử dụng chung máy tính tại văn phòng điều hành hoặc thiết bị di động hiện trường, tính năng đăng xuất cần đảm bảo thu hồi token ở phía máy chủ (`Server-side Token Revocation`) và xóa sạch các thông tin nhận thực nhạy cảm được lưu ở phía trình duyệt (`Client-side Storage`).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đang có một phiên đăng nhập hợp lệ và token còn thời hạn hiệu lực.

### Trigger

- Người dùng nhấp chọn nút "Đăng xuất" (`Logout`) trên menu người dùng ở thanh tiêu đề (`Header`) hoặc màn hình cài đặt.

## Flow

### Main Flow

1. Người dùng mở menu tài khoản cá nhân và chọn mục "Đăng xuất".
2. Hệ thống hiển thị hộp thoại xác nhận: "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?".
3. Người dùng nhấn nút xác nhận "Đăng xuất".
4. Phía trình duyệt gửi yêu cầu `POST /api/v1/auth/logout` kèm theo `AccessToken` và `RefreshToken`.
5. Phía máy chủ tiếp nhận yêu cầu:
   - Đưa `AccessToken` hiện tại vào danh sách đen thu hồi (`Token Blacklist / Invalidation Cache`).
   - Xóa bỏ hoặc đánh dấu vô hiệu hóa `RefreshToken` tương ứng trong cơ sở dữ liệu.
   - Ghi nhật ký kiểm toán: "Người dùng đăng xuất thành công".
6. Phía máy chủ trả về mã HTTP 200 kèm lệnh xóa cookie (nếu dùng `HttpOnly Cookie`).
7. Phía trình duyệt thực hiện dọn dẹp dữ liệu phiên:
   - Xóa `AccessToken` và thông tin người dùng khỏi `localStorage` / `sessionStorage`.
   - Đặt lại toàn bộ trạng thái bộ nhớ đệm (State Management / Cache).
8. Hệ thống hiển thị thông báo ngắn: "Bạn đã đăng xuất thành công".
9. Hệ thống tự động điều hướng người dùng về màn hình "Đăng nhập" (`/login`).

### Alternative Flow

#### ALT-01: Người dùng hủy bỏ thao tác đăng xuất

1. Tại bước 2 của Luồng chính, người dùng nhận thấy bấm nhầm và chọn "Hủy bỏ" (`Cancel`) trên hộp thoại xác nhận.
2. Hệ thống đóng hộp thoại và giữ nguyên phiên làm việc bình thường của người dùng tại màn hình hiện tại.

#### ALT-02: Đăng xuất khỏi tất cả các thiết bị (`Logout All Devices`)

1. Tại phần Cài đặt bảo mật, người dùng chọn tính năng "Đăng xuất khỏi tất cả thiết bị".
2. Hệ thống yêu cầu người dùng xác nhận mật khẩu hiện tại.
3. Khi xác thực thành công, máy chủ vô hiệu hóa toàn bộ `RefreshToken` thuộc về người dùng đó trên tất cả các thiết bị đã đăng nhập trước đây.
4. Phiên hiện tại cũng bị đăng xuất và điều hướng về trang Đăng nhập.

### Exception Flow

#### EXC-01: Lỗi kết nối mạng khi thực hiện đăng xuất

1. Khi người dùng nhấn Đăng xuất, kết nối mạng Internet bị mất khiến yêu cầu gửi lên máy chủ bị thất bại (`Network Error`).
2. Trình duyệt chủ động thực hiện xóa sạch token và thông tin phiên lưu trữ ở Local Storage để bảo vệ an toàn tức thì cho người dùng tại máy trạm.
3. Hệ thống hiển thị thông báo: "Đã xóa phiên làm việc cục bộ. Vui lòng kiểm tra lại kết nối mạng" và điều hướng về màn hình Đăng nhập.

## Acceptance Criteria

#### AC-001: Đăng xuất thành công và vô hiệu hóa token

- **Given**: Người dùng đang đăng nhập với JWT AccessToken hợp lệ.
- **When**: Người dùng nhấn xác nhận Đăng xuất.
- **Then**: Hệ thống thu hồi token ở server, xóa token ở client và điều hướng về màn hình `/login`.
- **And**: Nếu người dùng sử dụng nút "Back" của trình duyệt, hệ thống chặn truy cập và giữ nguyên ở trang Đăng nhập.

#### AC-002: Token cũ không thể tái sử dụng sau khi đăng xuất

- **Given**: Người dùng đã thực hiện thao tác đăng xuất thành công.
- **When**: Kẻ tấn công cố tình gửi lại request chứa `AccessToken` cũ đã bị thu hồi lên hệ thống.
- **Then**: Hệ thống trả về mã HTTP 401 Unauthorized kèm thông báo lỗi "Token đã bị thu hồi hoặc không hợp lệ".

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-003: Quy định phân quyền truy cập và quản trị phiên làm việc

### Dependencies

- Cơ chế lưu trữ Token Blacklist (Redis Cache hoặc Database Revocation Table)

## Non-Functional

- Thời gian xử lý đăng xuất hoàn tất trong thời gian $\le 300$ ms.
- Đảm bảo xóa sạch 100% dữ liệu nhạy cảm của người dùng khỏi bộ nhớ tạm của trình duyệt.

## Out of Scope

- Tự động đăng xuất theo cơ chế Idle Timeout (tính năng cấu hình riêng theo từng vai trò nghiệp vụ ở Sprint sau).
