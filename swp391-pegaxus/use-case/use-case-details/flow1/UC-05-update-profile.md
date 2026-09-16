# UC-05: Cập nhật hồ sơ cá nhân (Update Profile)

## Metadata

- **Story**: Là Người dùng đã đăng nhập (Logged-in User), tôi muốn xem và chỉnh sửa thông tin cá nhân (Họ và tên, Số điện thoại liên hệ, Địa chỉ, Ảnh đại diện Avatar) trên ứng dụng Web responsive để hồ sơ trên hệ thống luôn chính xác và mới nhất.
- **Context**: UC-05 phục vụ Logged-in User quản lý thông tin cá nhân trên ứng dụng Web responsive (BR-004). Để bảo đảm an toàn dữ liệu và phân quyền, các thông tin quan trọng như Email định danh và Vai trò (Role) hiển thị ở dạng chỉ đọc (Read-only - BR-012). Tệp tin ảnh đại diện phải tuân thủ định dạng và dung lượng tối đa 5MB (BR-013). Mọi hình ảnh và văn bản giao diện hiển thị bằng Tiếng Anh (BR-005).
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
- Người dùng chuyển tới trang "My Profile" từ menu cá nhân trên Web app.

### Trigger

- Người dùng thực hiện thay đổi thông tin biểu mẫu hoặc tải ảnh đại diện mới và nhấp chọn nút "Save Changes".

## Flow

### Main Flow

1. Người dùng nhấp vào biểu tượng Avatar trên thanh điều hướng (header bar) của Web app và chọn "My Profile".
2. Hệ thống gửi yêu cầu truy vấn thông tin hồ sơ cá nhân lên hệ thống.
3. Hệ thống truy vấn cơ sở dữ liệu và trả về thông tin hồ sơ hiện tại, hiển thị lên biểu mẫu Profile bao gồm:
   - Avatar Image (Ảnh đại diện hiện tại)
   - Full Name (Họ và tên - chỉnh sửa được)
   - Email (Địa chỉ email - Read-only - BR-012)
   - Role (Vai trò hệ thống - Read-only - BR-012)
   - Phone Number (Số điện thoại liên hệ - chỉnh sửa được)
   - Address (Địa chỉ tại Châu Âu - chỉnh sửa được)
   - Club/Organization Name (Tên CLB/Doanh nghiệp - chỉnh sửa được nếu là Khách hàng)
4. Người dùng chỉnh sửa các trường thông tin cần cập nhật.
5. (Tùy chọn) Người dùng nhấp chọn nút "Change Avatar" và chọn file ảnh từ máy tính/thiết bị (`.jpg`, `.png` - BR-013).
6. Hệ thống hiển thị xem trước (Image Preview) bức ảnh vừa chọn ngay trên màn hình Web.
7. Người dùng nhấn nút "Save Changes".
8. Hệ thống kiểm tra dữ liệu đầu vào phía client (format số điện thoại, kiểm tra định dạng file ảnh `.png`/`.jpg` và dung lượng file $\le 5\text{MB}$ - BR-013).
9. Hệ thống gửi yêu cầu cập nhật hồ sơ cá nhân và file ảnh đính kèm lên hệ thống.
10. Hệ thống xử lý lưu tải ảnh lên máy chủ lưu trữ tệp tin, cập nhật đường dẫn ảnh đại diện, lưu thông tin hồ sơ mới vào cơ sở dữ liệu và ghi nhận mốc thời gian cập nhật.
11. Hệ thống trả về thông tin hồ sơ đã cập nhật.
12. Hệ thống cập nhật hiển thị Avatar mới trên thanh điều hướng header bar của Web app và hiển thị thông báo thành công: "Profile updated successfully."

### Alternative Flow

#### ALT-01: Xóa ảnh đại diện hiện tại (Remove Avatar)

1. Tại bước 5 của Luồng chính, người dùng nhấp chọn nút "Remove Avatar".
2. Hệ thống đặt lại xem trước về hình ảnh đại diện mặc định của hệ thống.
3. Người dùng nhấn "Save Changes".
4. Hệ thống cập nhật ảnh đại diện về hình ảnh mặc định trong cơ sở dữ liệu và hiển thị thông báo thành công.

### Exception Flow

#### EXC-01: File ảnh đại diện quá dung lượng hoặc sai định dạng

1. Tại bước 8 của Luồng chính, file ảnh được chọn có dung lượng lớn hơn 5MB (ví dụ: 8MB) hoặc không phải định dạng JPG/PNG (ví dụ: `.gif`, `.pdf`, `.svg` - BR-013).
2. Hệ thống hiển thị thông báo lỗi ngay dưới nút tải ảnh: "Avatar image must be in JPG or PNG format and less than 5MB in size."
3. Hệ thống hủy xem trước file ảnh lỗi và chặn thao tác nhấn "Save Changes".

#### EXC-02: Định dạng Số điện thoại không hợp lệ

1. Tại bước 8 của Luồng chính, ô Phone Number nhập chứa các ký tự chữ cái hoặc chuỗi số không đúng định dạng quốc tế.
2. Hệ thống hiển thị lỗi dưới ô nhập: "Please enter a valid contact phone number."
3. Người dùng chỉnh sửa lại số điện thoại hợp lệ và nhấn "Save Changes".

## Acceptance Criteria

#### AC-001: Cập nhật hồ sơ và ảnh đại diện thành công trên Web app

- **Given**: Người dùng đã đăng nhập trên ứng dụng Web responsive, thay đổi số điện thoại hợp lệ và chọn file ảnh `.jpg` dung lượng 2MB (BR-013).
- **When**: Người dùng nhấn "Save Changes".
- **Then**: Hệ thống tải ảnh mới lên máy chủ lưu trữ, lưu thông tin mới vào cơ sở dữ liệu, cập nhật Avatar trên header bar và hiển thị thông báo "Profile updated successfully."

#### AC-002: Chặn tải lên file ảnh sai quy định

- **Given**: Người dùng chọn file ảnh có dung lượng 7MB (BR-013).
- **When**: Người dùng chọn file.
- **Then**: Hệ thống từ chối nhận file, hiển thị lỗi "Avatar image must be in JPG or PNG format and less than 5MB in size." và không cho phép lưu.

#### AC-003: Đảm bảo các trường định danh là Read-only

- **Given**: Khách hàng ở giao diện "My Profile".
- **When**: Kiểm tra các trường Email và Role (BR-012).
- **Then**: Các trường Email và Role hiển thị ở trạng thái vô hiệu hóa (disabled / read-only), không cho phép con trỏ chuột chỉnh sửa văn bản.

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Kiến trúc Xác thực Người dùng (User Management & Authentication Architecture)

### Rules

- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-012: Quy định Bảo vệ Trường Định danh Hồ sơ (Read-only Profile Fields)
- BR-013: Quy định Định dạng & Dung lượng Ảnh đại diện (Avatar Image Policy)

### Dependencies

- Dịch vụ lưu trữ tệp tin (File Storage Service)

## Non-Functional

- Performance: Thời gian xử lý cập nhật hồ sơ và lưu trữ tệp tin $\le 2$ giây.
- Security: Kiểm tra định dạng và tính hợp lệ của tệp tin phía server (BR-013); Mọi giao diện và thông báo bằng Tiếng Anh (`English` - BR-005).
- Compatibility: Vận hành trên Responsive Web App (BR-004).

## Out of Scope

- Phát triển ứng dụng di động riêng (Native/Hybrid Mobile App) (BR-004).
- Thay đổi địa chỉ Email đăng nhập (yêu cầu quy trình xác thực riêng).
- Thay đổi vai trò (Role) người dùng (chỉ do Admin/Logistics Manager cấp phát).
