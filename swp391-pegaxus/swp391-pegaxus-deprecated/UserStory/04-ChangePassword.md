# STORY-004: Thay đổi mật khẩu

## Metadata

- **Story**: Là một Người dùng đang đăng nhập, tôi muốn chủ động thay đổi mật khẩu hiện tại bằng cách nhập đúng mật khẩu cũ và cung cấp mật khẩu mới để nâng cao tính bảo mật cho tài khoản cá nhân.
- **Context**: Để tuân thủ chính sách an ninh thông tin khi vận hành quản lý các hợp đồng vận chuyển ngựa trị giá cao, người dùng cần có khả năng thay đổi mật khẩu định kỳ hoặc ngay khi nghi ngờ mật khẩu cũ bị lộ. Thao tác này đòi hỏi người dùng phải chứng thực mật khẩu đang hoạt động trước khi cập nhật.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập thành công vào hệ thống và sở hữu `AccessToken` còn hiệu lực.
- Người dùng đang ở màn hình Cài đặt tài khoản / Bảo mật (`/settings/security`).

### Trigger

- Người dùng nhấp chọn mục "Đổi mật khẩu" (`Change Password`).

## Flow

### Main Flow

1. Hệ thống hiển thị biểu mẫu Đổi mật khẩu gồm 3 trường:
   - Mật khẩu hiện tại (`Current Password`)
   - Mật khẩu mới (`New Password`)
   - Xác nhận mật khẩu mới (`Confirm New Password`)
2. Người dùng nhập đầy đủ cả 3 trường dữ liệu.
3. Người dùng nhấn nút "Lưu thay đổi" (`Save Changes`).
4. Hệ thống kiểm tra các trường không được bỏ trống.
5. Hệ thống xác minh Mật khẩu hiện tại có khớp với `passwordHash` trong cơ sở dữ liệu hay không.
6. Hệ thống kiểm tra Mật khẩu mới:
   - Đạt độ dài tối thiểu 8 ký tự, có đủ chữ hoa, chữ thường, số và ký tự đặc biệt.
   - Khác với Mật khẩu hiện tại.
   - Khớp hoàn toàn với trường Xác nhận mật khẩu mới.
7. Hệ thống băm mật khẩu mới bằng thuật toán bảo mật cao và cập nhật vào trường `passwordHash` của bản ghi `User`, cập nhật `updatedAt`.
8. Hệ thống ghi nhận sự kiện bảo mật vào nhật ký kiểm toán (`Audit Log`): "Người dùng đổi mật khẩu thành công".
9. Hệ thống vô hiệu hóa các phiên đăng nhập khác của tài khoản, cấp lại token mới cho phiên hiện tại.
10. Hệ thống hiển thị thông báo thành công: "Mật khẩu của bạn đã được cập nhật thành công!".

### Alternative Flow

#### ALT-01: Người dùng hủy thao tác đổi mật khẩu

1. Tại bước 2 của Luồng chính, người dùng quyết định không đổi mật khẩu và nhấn nút "Hủy bỏ" (`Cancel`).
2. Hệ thống đặt lại biểu mẫu về trạng thái trống ban đầu và giữ nguyên mật khẩu cũ.

### Exception Flow

#### EXC-01: Nhập sai mật khẩu hiện tại

1. Tại bước 5 của Luồng chính, mật khẩu hiện tại người dùng nhập không khớp với dữ liệu trong hệ thống.
2. Hệ thống giữ nguyên ô Mật khẩu mới, làm trống ô Mật khẩu hiện tại và hiển thị cảnh báo đỏ: "Mật khẩu hiện tại không chính xác. Vui lòng thử lại".
3. Không thực hiện cập nhật CSDL.

#### EXC-02: Xác nhận mật khẩu mới không khớp

1. Tại bước 6 của Luồng chính, giá trị tại ô "Xác nhận mật khẩu mới" không trùng khớp từng ký tự với "Mật khẩu mới".
2. Hệ thống hiển thị thông báo lỗi dưới ô xác nhận: "Mật khẩu xác nhận không khớp với mật khẩu mới".

#### EXC-03: Mật khẩu mới trùng mật khẩu hiện tại

1. Tại bước 6 của Luồng chính, người dùng nhập mật khẩu mới giống hệt mật khẩu hiện tại.
2. Hệ thống cảnh báo: "Mật khẩu mới không được trùng với mật khẩu hiện tại nhằm đảm bảo an toàn".

## Acceptance Criteria

#### AC-001: Đổi mật khẩu thành công khi nhập đúng thông tin

- **Given**: Người dùng đã đăng nhập với mật khẩu hiện tại là `OldPass@2026`.
- **When**: Người dùng nhập mật khẩu hiện tại là `OldPass@2026`, mật khẩu mới là `NewSecurePass#99` và xác nhận mật khẩu khớp.
- **Then**: Hệ thống cập nhật thành công mật khẩu mới vào cơ sở dữ liệu.
- **And**: Hiển thị thông báo thành công và người dùng có thể đăng nhập bằng `NewSecurePass#99` ở lần tiếp theo.

#### AC-002: Báo lỗi khi sai mật khẩu hiện tại

- **Given**: Mật khẩu hiện tại của người dùng là `OldPass@2026`.
- **When**: Người dùng nhập sai thành `WrongPass@2026` và nhấn "Lưu thay đổi".
- **Then**: Hệ thống từ chối cập nhật và hiển thị thông báo "Mật khẩu hiện tại không chính xác".

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-001: Quy định định dạng mật khẩu và an toàn tài khoản

### Dependencies

- Dịch vụ xác thực JWT và băm mật khẩu

## Non-Functional

- Xử lý xác thực và băm mật khẩu hoàn tất trong thời gian $\le 400$ ms.
- Mọi dữ liệu truyền qua mạng phải được mã hóa qua kênh an toàn HTTPS / TLS 1.3.

## Out of Scope

- Bắt buộc đổi mật khẩu định kỳ 90 ngày (tính năng thuộc cấu hình bảo mật Enterprise trong tương lai).
