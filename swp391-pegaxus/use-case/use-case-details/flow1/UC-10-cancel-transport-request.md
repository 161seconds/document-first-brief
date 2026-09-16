# UC-10: Hủy yêu cầu vận chuyển (Cancel Transport Request)

## Metadata

- **Story**: Là Khách hàng (David Michael - Chủ sở hữu hoặc đại diện CLB Đua ngựa), tôi muốn chủ động hủy bỏ một yêu cầu vận chuyển khi đơn hàng chưa được xử lý và phải cung cấp lý do hủy để hệ thống lưu lịch sử và ngừng tiến trình xử lý đơn hàng.
- **Context**: UC-10 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Khách hàng có quyền hủy yêu cầu vận chuyển khi lịch thi đấu bị hủy hoặc kế hoạch cá nhân thay đổi. Việc hủy yêu cầu chỉ hợp lệ khi trạng thái đơn hàng hiện tại là "Chờ duyệt" (`PENDING`). Bắt buộc người dùng phải nhập lý do hủy (Cancellation Reason). Hệ thống cập nhật trạng thái đơn hàng sang `CANCELLED`, ghi lại người hủy, thời điểm hủy và đưa vào lịch sử truy vết (UC-15).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập thành công với vai trò `CUSTOMER`.
- Yêu cầu vận chuyển ở trạng thái `PENDING`.
- Khách hàng là người tạo yêu cầu vận chuyển này.

### Trigger

- Khách hàng nhấp chọn nút "Cancel Request" trên giao diện Danh sách yêu cầu hoặc Chi tiết yêu cầu.

## Flow

### Main Flow

1. Khách hàng truy cập trang Chi tiết hoặc Danh sách yêu cầu vận chuyển.
2. Khách hàng nhấp chọn nút "Cancel Request" tại yêu cầu đang ở trạng thái `PENDING`.
3. Hệ thống hiển thị hộp thoại xác nhận hủy yêu cầu (Cancel Request Confirmation Modal) bao gồm:
   - Thông tin tóm tắt mã yêu cầu (`REQ-YYYYMMDD-XXXX`)
   - Ô nhập lý do hủy: Cancellation Reason (Trường bắt buộc - textarea)
   - Nút "Confirm Cancel" và nút "Keep Request"
4. Khách hàng nhập chi tiết lý do hủy (ví dụ: "Event postponed by race organizer").
5. Khách hàng nhấp nút "Confirm Cancel".
6. Hệ thống kiểm tra dữ liệu:
   - Ô lý do hủy không được để trống (tối thiểu 10 ký tự).
   - Kiểm tra trạng thái yêu cầu phía server vẫn ở trạng thái `PENDING`.
7. Hệ thống cập nhật trạng thái yêu cầu sang `CANCELLED` (Đã hủy).
8. Hệ thống lưu lý do hủy, mốc thời gian hủy (`cancelled_at`) và định danh người thực hiện (`cancelled_by = Customer ID`) vào cơ sở dữ liệu và nhật ký lịch sử trạng thái (UC-15).
9. Hệ thống gửi thông báo xác nhận hủy thành công cho khách hàng: "Transport request REQ-YYYYMMDD-XXXX has been canceled."
10. Hệ thống cập nhật giao diện, chuyển nút thao tác về chế độ xem và đánh nhãn trạng thái màu đỏ `CANCELLED`.

### Alternative Flow

#### ALT-01: Khách hàng đổi ý không hủy

1. Tại bước 4 của Luồng chính, khách hàng nhấp chọn nút "Keep Request" hoặc đóng hộp thoại xác nhận.
2. Hệ thống đóng hộp thoại, giữ nguyên trạng thái `PENDING` của yêu cầu và không lưu bất kỳ thay đổi nào.

### Exception Flow

#### EXC-01: Để trống hoặc nhập quá ngắn lý do hủy

1. Tại bước 5 của Luồng chính, khách hàng để trống ô Cancellation Reason hoặc nhập ít hơn 10 ký tự.
2. Hệ thống hiển thị cảnh báo ngay dưới ô nhập: "Please provide a detailed reason for cancellation (minimum 10 characters)."
3. Hộp thoại xác nhận vẫn mở và nút "Confirm Cancel" bị vô hiệu hóa cho đến khi nhập đủ độ dài quy định.

#### EXC-02: Yêu cầu đã được phê duyệt trong lúc khách hàng đang mở hộp thoại hủy

1. Khách hàng mở hộp thoại hủy khi đơn hàng ở trạng thái `PENDING`. Đồng thời, Logistics Manager thực hiện phê duyệt đơn hàng trên hệ thống sang `APPROVED`.
2. Khách hàng nhấn "Confirm Cancel".
3. Hệ thống kiểm tra phía server và phát hiện trạng thái đã chuyển thành `APPROVED`.
4. Hệ thống từ chối thao tác hủy và hiển thị thông báo lỗi: "This request has already been Approved by Logistics Manager. Direct cancellation is no longer permitted. Please contact support."

## Acceptance Criteria

### AC-001: Hủy yêu cầu thành công với lý do hợp lệ

- **Given**: Khách hàng đang ở hộp thoại hủy cho yêu cầu `REQ-20260915-0003` ở trạng thái `PENDING`.
- **When**: Khách hàng nhập lý do "Race event in Berlin canceled due to bad weather" (trên 10 ký tự) và nhấn "Confirm Cancel".
- **Then**: Hệ thống chuyển trạng thái yêu cầu thành `CANCELLED`, lưu mốc thời gian và lý do hủy, hiển thị thông báo "Transport request has been canceled."

### AC-002: Chặn hủy khi không nhập lý do

- **Given**: Khách hàng mở hộp thoại hủy yêu cầu.
- **When**: Khách hàng để trống ô Cancellation Reason và nhấn "Confirm Cancel".
- **Then**: Hệ thống báo lỗi "Please provide a detailed reason for cancellation (minimum 10 characters)" và không chuyển trạng thái đơn hàng.

### AC-003: Chặn hủy đơn hàng đã duyệt

- **Given**: Đơn hàng `REQ-20260915-0003` đã chuyển sang trạng thái `APPROVED`.
- **When**: Khách hàng gửi lệnh hủy đơn.
- **Then**: Hệ thống từ chối hủy và báo lỗi "This request has already been Approved by Logistics Manager. Direct cancellation is no longer permitted."

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- Hệ thống Lịch sử Trạng thái Yêu cầu (Request Status Log Entity)

## Non-Functional

- Security: Đảm bảo khách hàng chỉ hủy được yêu cầu do chính mình sở hữu; Không cho phép khôi phục yêu cầu đã hủy (`CANCELLED` là trạng thái kết thúc luồng).
- Compatibility: Hoạt động mượt mà trên ứng dụng Web responsive.

## Out of Scope

- Hủy các đơn hàng đã được phê duyệt (`APPROVED`) hoặc đang di chuyển — việc này thuộc quy trình khiếu nại/xử lý ngoại lệ với Quản lý Logistics.
- Hoàn tiền dịch vụ (xử lý ở Flow 6).
