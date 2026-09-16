# UC-06: Đăng xuất hệ thống (Logout)

## Metadata

- **Story**: Là Người dùng đã đăng nhập (Logged-in User), tôi muốn chủ động đăng xuất khỏi ứng dụng Web responsive để chấm dứt phiên làm việc, xóa các token xác thực và bảo vệ an toàn cho thông tin tài khoản của mình.
- **Context**: UC-06 đảm bảo tính an toàn và bảo mật cho tài khoản Logged-in User khi không còn sử dụng ứng dụng Web responsive (BR-004). Quá trình đăng xuất thực hiện thu hồi phiên làm việc phía Server và xóa hoàn toàn thông tin phiên làm việc phía Client (BR-014), đồng thời ngăn chặn truy cập lại thông qua lịch sử trình duyệt (BR-015). Mọi giao diện và thông báo phản hồi hiển thị bằng Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Review
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đang có phiên làm việc hợp lệ trên ứng dụng Web responsive.

### Trigger

- Người dùng nhấp chọn mục "Log Out" (Đăng xuất) từ menu tài khoản cá nhân hoặc thanh điều hướng của Web app.

## Flow

### Main Flow

1. Người dùng nhấp chọn biểu tượng đại diện tài khoản trên thanh điều hướng Web app và chọn "Log Out".
2. Hệ thống hiển thị hộp thoại xác nhận: "Are you sure you want to log out?" kèm hai tùy chọn "Cancel" và "Log Out".
3. Người dùng nhấp chọn "Log Out".
4. Hệ thống gửi yêu cầu đăng xuất lên hệ thống.
5. Hệ thống phía Server thực hiện thu hồi phiên làm việc trong cơ sở dữ liệu và ghi nhận mốc thời gian đăng xuất (BR-014).
6. Hệ thống phía Client xóa toàn bộ mã phiên làm việc và thông tin người dùng lưu trữ trên trình duyệt (BR-014).
7. Hệ thống chuyển hướng giao diện trình duyệt về trang Đăng nhập.
8. Hệ thống hiển thị thông báo thành công: "You have been logged out successfully."
9. Hệ thống vô hiệu hóa bộ nhớ đệm cache các trang nội bộ đối với phiên hiện tại (BR-015).

### Alternative Flow

- N/A (Quy trình thực hiện trực tiếp).

### Exception Flow

#### EXC-01: Mất kết nối mạng khi thực hiện Đăng xuất (Offline Logout)

1. Tại bước 4 của Luồng chính, kết nối mạng internet bị gián đoạn, yêu cầu gửi đăng xuất bị ngắt kết nối.
2. Hệ thống phía Client chủ động hiển thị thông báo: "Network unavailable. Session cleared locally."
3. Hệ thống phía Client vẫn thực hiện xóa toàn bộ mã phiên làm việc và thông tin lưu trữ trên trình duyệt để bảo vệ an toàn tuyệt đối cho thiết bị của người dùng (BR-014).
4. Trình duyệt chuyển hướng người dùng về trang Đăng nhập.

## Acceptance Criteria

#### AC-001: Đăng xuất thành công và thu hồi phiên làm việc

- **Given**: Người dùng đang trong phiên làm việc hợp lệ trên ứng dụng Web responsive.
- **When**: Người dùng nhấn "Log Out" và xác nhận.
- **Then**: Server thu hồi phiên làm việc, Client xóa toàn bộ mã phiên lưu trên trình duyệt và chuyển hướng thành công về trang Đăng nhập kèm thông báo "You have been logged out successfully." (BR-014).

#### AC-002: Chặn quay lại trang nội bộ bằng nút Back của trình duyệt

- **Given**: Người dùng vừa thực hiện Đăng xuất thành công và đang ở trang Đăng nhập.
- **When**: Người dùng nhấn nút "Back" ($\leftarrow$) trên trình duyệt.
- **Then**: Hệ thống kiểm tra phiên làm việc không tồn tại, từ chối tải lại dữ liệu trang nội bộ và giữ người dùng ở lại trang Đăng nhập (BR-015).

#### AC-003: Đảm bảo an toàn tài khoản ngay cả khi mất mạng

- **Given**: Người dùng nhấn Đăng xuất trong điều kiện thiết bị bị ngắt kết nối mạng.
- **When**: Yêu cầu đăng xuất trả về lỗi kết nối mạng.
- **Then**: Hệ thống client vẫn xóa sạch thông tin phiên làm việc lưu trên trình duyệt và đưa người dùng về trang Đăng nhập (BR-014).

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-014: Quy định An toàn Đăng xuất & Thu hồi Phiên Làm việc (Session Invalidation & Client Wipe)
- BR-015: Quy định Chống Truy cập lại sau Đăng xuất (Prevent Back-Button Access)

### Dependencies

- Dịch vụ quản lý phiên làm việc người dùng

## Non-Functional

- Performance: Thời gian xử lý đăng xuất phía client và server $\le 1$ giây.
- Security: Xóa toàn bộ thông tin phiên làm việc phía client; Thu hồi phiên làm việc phía server (BR-014); Tiếng Anh duy nhất (`English` - BR-005).
- Compatibility: Vận hành trên Responsive Web App (BR-004).

## Out of Scope

- Phát triển ứng dụng di động riêng (Native/Hybrid Mobile App) (BR-004).
- Đăng xuất tất cả các thiết bị từ xa (Remote Session Management).
- Tự động đăng xuất khi hết thời gian chờ phiên làm việc không hoạt động.
