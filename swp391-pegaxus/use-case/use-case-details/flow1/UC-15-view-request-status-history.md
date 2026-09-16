# UC-15: Xem lịch sử thay đổi trạng thái (View Request Status Change History)

## Metadata

- **Story**: Là Khách hàng (David Michael) hoặc Quản lý Điều hành Logistics (Trần Anh Tuấn), tôi muốn xem danh sách lịch sử tất cả các mốc thay đổi trạng thái của một yêu cầu vận chuyển để theo dõi diễn biến xử lý đơn hàng và đảm bảo tính minh bạch, khả năng truy vết dữ liệu.
- **Context**: UC-15 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Mọi thao tác tác động làm chuyển dịch trạng thái của một yêu cầu vận chuyển (từ `PENDING` $\rightarrow$ `APPROVED` / `REJECTED` / `CANCELLED`) đều được hệ thống ghi nhận thành một bản ghi nhật ký không thể sửa đổi (Audit Log / Audit Trail). Nhật ký ghi nhận các trường: Trạng thái cũ (`from_status`), Trạng thái mới (`to_status`), Thời điểm thực hiện (`timestamp`), Định danh & Vai trò người thực hiện (`performed_by`, `user_role`), Ghi chú / Lý do đi kèm (`notes` / `reason`). Khách hàng chỉ xem được lịch sử đơn hàng của mình; Logistics Manager xem được toàn bộ. Hệ thống vận hành trên Web responsive (BR-004) bằng Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Should
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập thành công với vai trò `CUSTOMER` hoặc `LOGISTICS_MANAGER`.
- Yêu cầu vận chuyển tồn tại trên hệ thống và người dùng có quyền truy cập xem thông tin đơn hàng đó.

### Trigger

- Người dùng nhấp chọn nút hoặc tab "View Status History" hoặc "Audit Trail" trên giao diện Chi tiết yêu cầu vận chuyển (UC-12).

## Flow

### Main Flow

1. Người dùng đang ở màn hình Chi tiết yêu cầu vận chuyển `REQ-YYYYMMDD-XXXX` (UC-12).
2. Người dùng nhấp chọn nút "View Status History" hoặc chọn tab "Status History".
3. Hệ thống kiểm tra quyền truy cập lịch sử của người dùng phía server (RBAC - BR-003):
   - Khách hàng: Chỉ truy xuất lịch sử đơn hàng do chính mình tạo.
   - Logistics Manager: Được phép truy xuất lịch sử mọi đơn hàng.
4. Hệ thống truy vấn CSDL bảng Nhật ký thay đổi trạng thái (`request_status_logs`) tương ứng với mã yêu cầu.
5. Hệ thống hiển thị giao diện Dòng thời gian tiến trình (Status Timeline UI) sắp xếp theo thứ tự thời gian tăng dần hoặc giảm dần bao gồm các mốc:
   - Status Transition (Ví dụ: `PENDING` $\rightarrow$ `APPROVED`, `PENDING` $\rightarrow$ `REJECTED`)
   - Performed By (Họ tên & Vai trò người thao tác, ví dụ: "David Michael (Customer)", "Trần Anh Tuấn (Logistics Manager)")
   - Timestamp (Mốc ngày giờ chính xác: `YYYY-MM-DD HH:mm:ss UTC`)
   - Notes / Reason (Ghi chú phê duyệt hoặc Lý do từ chối/hủy đơn hàng nếu có)
6. Người dùng xem chi tiết từng mốc lịch sử và có thể nhấp chọn "Close" để quay về màn hình Chi tiết yêu cầu.

### Alternative Flow

#### ALT-01: Hiển thị trong Hộp thoại Modal (Modal View)

1. Tại bước 2 của Luồng chính, người dùng nhấn "View Status History".
2. Hệ thống hiển thị một hộp thoại Modal đè lên trang chi tiết, liệt kê danh sách các mốc sự kiện lịch sử.
3. Người dùng xem xong nhấp nút "X" hoặc "Close" để đóng modal.

### Exception Flow

#### EXC-01: Chưa có bản ghi lịch sử chuyển trạng thái

1. Yêu cầu mới được khởi tạo và chưa trải qua bước chuyển đổi trạng thái nào khác ngoài trạng thái ban đầu.
2. Hệ thống hiển thị dòng thời gian chỉ chứa 1 mốc duy nhất là mốc khởi tạo đơn hàng (`Created as PENDING`).

#### EXC-02: Truy cập trái phép lịch sử của yêu cầu thuộc người dùng khác

1. Khách hàng cố gắng truy vấn API lấy lịch sử chuyển trạng thái của một yêu cầu không thuộc sở hữu của mình.
2. Hệ thống kiểm tra phía server, từ chối truy xuất và trả về lỗi HTTP 403 Forbidden.

## Acceptance Criteria

### AC-001: Hiển thị đầy đủ dòng thời gian thay đổi trạng thái đơn hàng

- **Given**: Đơn hàng `REQ-20260915-0001` đã trải qua các bước: Khởi tạo bởi Customer $\rightarrow$ Phê duyệt bởi Manager.
- **When**: Người dùng nhấp nút "View Status History".
- **Then**: Hệ thống hiển thị chính xác 2 mốc thời gian: (1) Created as PENDING by David Michael (Customer) lúc 10:00, (2) Approved by Trần Anh Tuấn (Logistics Manager) lúc 10:30 với nhãn trạng thái và thời gian chuẩn xác.

### AC-002: Minh bạch lý do hủy hoặc từ chối đơn hàng trong nhật ký

- **Given**: Đơn hàng `REQ-20260915-0004` bị từ chối bởi Manager với lý do "Route out of coverage area".
- **When**: Khách hàng mở xem Status History.
- **Then**: Mốc chuyển trạng thái `PENDING` $\rightarrow$ `REJECTED` hiển thị rõ ràng nội dung lý do "Route out of coverage area" và thời điểm từ chối.

### AC-003: Nhật ký ở chế độ chỉ đọc, không thể chỉnh sửa hoặc xóa (Immutable Audit Trail)

- **Given**: Người dùng ở giao diện Status History.
- **When**: Kiểm tra các tính năng trên màn hình.
- **Then**: Mọi dữ liệu lịch sử hiển thị ở dạng chỉ đọc (Read-only), không có bất kỳ nút hay tính năng nào cho phép chỉnh sửa hoặc xóa bản ghi lịch sử.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- Entity Nhật ký Lịch sử Trạng thái (Request Status Log Entity / Audit Trail DB)

## Non-Functional

- Security: Bản ghi lịch sử là bất biến (Immutable), không cho phép thao tác UPDATE/DELETE từ giao diện hoặc API thường.
- Compatibility: Hiển thị giao diện Timeline đẹp mắt và co giãn mượt mà trên ứng dụng Web responsive.

## Out of Scope

- Chỉnh sửa hoặc xóa các mốc bản ghi lịch sử đã ghi nhận trong quá khứ.
