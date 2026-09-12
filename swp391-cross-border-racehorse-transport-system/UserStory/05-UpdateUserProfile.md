# STORY-005: Cập nhật hồ sơ cá nhân (Profile)

## Metadata

- **Story**: Là một Người dùng hệ thống, tôi muốn xem và cập nhật các thông tin cá nhân cơ bản (Ảnh đại diện, Số điện thoại liên hệ, Địa chỉ liên lạc, Tên CLB/Trang trại) để đảm bảo dữ liệu luôn chính xác phục vụ việc liên lạc và lập chứng từ vận chuyển.
- **Context**: Thông tin hồ sơ của khách hàng và nhân sự đóng vai trò then chốt trong quá trình ký kết chứng từ vận chuyển, xuất hóa đơn và liên lạc điều phối hiện trường. Người dùng cần có quyền tự quản lý và cập nhật thông tin cá nhân, đồng thời hệ thống phải lưu vết các thay đổi để phục vụ kiểm toán và truy cứu trách nhiệm khi có tranh chấp.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập vào hệ thống với phiên làm việc hợp lệ.
- Người dùng truy cập vào trang Thông tin cá nhân (`/profile` hoặc `/settings/profile`).

### Trigger

- Người dùng nhấn vào avatar hoặc biểu tượng tài khoản trên thanh điều hướng và chọn "Hồ sơ cá nhân".

## Flow

### Main Flow

1. Hệ thống tải và hiển thị thông tin hồ sơ hiện tại của người dùng:
   - Thông tin cơ bản từ bảng `User`: Họ và tên, Email (chỉ đọc), Vai trò người dùng (chỉ đọc).
   - Thông tin chi tiết từ bảng `CustomerProfile` (nếu là Customer) hoặc hồ sơ nhân sự: Ảnh đại diện (`avatarUrl`), Số điện thoại liên hệ (`phone`), Địa chỉ liên lạc (`contactAddress`), Tên Câu lạc bộ/Trang trại (`clubName`), Mã số thuế (`taxCode`).
2. Người dùng thực hiện chỉnh sửa các trường được phép:
   - Tải lên ảnh đại diện mới từ máy tính/điện thoại (định dạng JPG, PNG, WEBP; dung lượng $\le 5$ MB).
   - Chỉnh sửa Họ tên, Số điện thoại, Địa chỉ liên lạc, Tên CLB hoặc Mã số thuế.
3. Người dùng nhấn nút "Lưu thay đổi" (`Save Profile`).
4. Hệ thống kiểm tra định dạng dữ liệu đầu vào:
   - Định dạng ảnh hợp lệ và kích thước không vượt quá giới hạn cho phép.
   - Số điện thoại đúng định dạng chuẩn quốc tế hoặc nội địa (10-11 chữ số).
5. Nếu người dùng tải ảnh mới, hệ thống tải file lên dịch vụ lưu trữ đám mây (Cloud Storage / S3), nhận URL ảnh mới và cập nhật `avatarUrl`.
6. Hệ thống cập nhật các trường dữ liệu tương ứng trong bảng `User` và `CustomerProfile`, cập nhật trường `updatedAt`.
7. Hệ thống ghi lại lịch sử chỉnh sửa vào bảng nhật ký kiểm toán.
8. Hệ thống hiển thị thông báo: "Cập nhật hồ sơ cá nhân thành công!" và làm mới giao diện hiển thị dữ liệu mới nhất.

### Alternative Flow

#### ALT-01: Người dùng chỉ cập nhật ảnh đại diện

1. Người dùng chỉ nhấp vào biểu tượng camera trên khung ảnh đại diện và chọn tệp ảnh mới.
2. Hệ thống hiển thị bản xem trước ảnh (`Image Preview`) kèm công cụ cắt ảnh vuông.
3. Người dùng xác nhận cắt ảnh và nhấn "Lưu ảnh".
4. Hệ thống tải ảnh lên và cập nhật ngay lập tức mà không cần chỉnh sửa các trường văn bản khác.

### Exception Flow

#### EXC-01: Định dạng hoặc dung lượng file ảnh không hợp lệ

1. Tại bước 4 của Luồng chính, người dùng chọn file ảnh có dung lượng lớn hơn 5 MB hoặc định dạng không được hỗ trợ (ví dụ: file PDF, GIF, BMP).
2. Hệ thống từ chối tải file và hiển thị cảnh báo: "Ảnh đại diện phải ở định dạng JPG, PNG hoặc WEBP và dung lượng không vượt quá 5 MB".
3. Giữ nguyên ảnh đại diện hiện tại.

#### EXC-02: Số điện thoại không đúng định dạng hoặc đã trùng với tài khoản khác

1. Tại bước 4 của Luồng chính, người dùng nhập số điện thoại không hợp lệ hoặc đã được đăng ký bởi một tài khoản khác trong hệ thống.
2. Hệ thống hiển thị lỗi: "Số điện thoại không hợp lệ hoặc đã được sử dụng bởi người dùng khác".
3. Ngăn chặn việc lưu thông tin.

## Acceptance Criteria

#### AC-001: Cập nhật thành công thông tin hồ sơ khách hàng

- **Given**: Khách hàng đang ở màn hình hồ sơ cá nhân với địa chỉ hiện tại là "Hà Nội".
- **When**: Khách hàng sửa địa chỉ thành "Khu đô thị Sala, TP. Thủ Đức, TP. Hồ Chí Minh" và nhấn "Lưu thay đổi".
- **Then**: Hệ thống ghi nhận địa chỉ mới vào bảng `CustomerProfile` và hiển thị thông báo thành công.

#### AC-002: Chặn sửa đổi các trường thông tin bất biến

- **Given**: Người dùng đang ở màn hình thông tin cá nhân.
- **When**: Người dùng quan sát trường Email và Vai trò (`Role`).
- **Then**: Các trường này ở trạng thái vô hiệu hóa chỉnh sửa (`Read-only` / `Disabled`), không cho phép người dùng tự ý thay đổi trực tiếp trên biểu mẫu.

#### AC-003: Tải lên ảnh đại diện hợp lệ

- **Given**: Người dùng chọn file ảnh `avatar.png` dung lượng 2 MB.
- **When**: Người dùng tải ảnh lên và nhấn Lưu.
- **Then**: File được lưu trữ trên hệ thống, trường `avatarUrl` được cập nhật và avatar mới hiển thị ngay lập tức trên thanh Header.

## References

### TDDs

- TDD-001: Quản trị Tài khoản & Phân quyền Người dùng (User Management & Authentication Architecture)

### Rules

- BR-005: Quy định định dạng và xác thực hồ sơ người dùng
- BR-006: Quy định an toàn tệp tải lên (File Upload Validation & Antivirus scanning)

### Dependencies

- Dịch vụ lưu trữ file đám mây (Cloud Object Storage - AWS S3 / Firebase Storage)

## Non-Functional

- Xử lý nén và tải ảnh đại diện hoàn tất trong thời gian $\le 2$ giây.
- Đảm bảo an toàn thông tin cá nhân theo quy định bảo vệ dữ liệu người dùng (GDPR / Nghị định 13/2023/NĐ-CP).

## Out of Scope

- Quy trình thay đổi Email đăng nhập (cần luồng xác thực bảo mật 2 chiều riêng biệt trong bản cập nhật sau).
