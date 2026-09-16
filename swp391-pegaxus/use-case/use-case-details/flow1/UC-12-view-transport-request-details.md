# UC-12: Xem chi tiết yêu cầu vận chuyển (View Transport Request Details)

## Metadata

- **Story**: Là Khách hàng (David Michael) hoặc Quản lý Điều hành Logistics (Trần Anh Tuấn), tôi muốn truy xuất xem toàn bộ thông tin chi tiết của một yêu cầu vận chuyển cụ thể bao gồm lịch trình, danh sách ngựa, yêu cầu đặc biệt và trạng thái phê duyệt để nắm rõ bức tranh toàn cảnh của đơn hàng.
- **Context**: UC-12 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Khách hàng sử dụng giao diện này để theo dõi tiến độ và kiểm tra dữ liệu đơn hàng đã tạo; Logistics Manager xem chi tiết thông tin đơn hàng để đánh giá tính khả thi trước khi thực hiện Phê duyệt (UC-13) hoặc Từ chối (UC-14). Dữ liệu hiển thị tuân thủ phân quyền RBAC (BR-003) và giao diện responsive (BR-004) bằng Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập thành công với vai trò `CUSTOMER` hoặc `LOGISTICS_MANAGER`.
- Yêu cầu vận chuyển hợp lệ tồn tại trên hệ thống và người dùng có quyền truy cập (Customer sở hữu đơn hàng hoặc Manager).

### Trigger

- Người dùng nhấp chọn mã đơn hàng hoặc nút "View Details" trên bảng danh sách yêu cầu (UC-11).

## Flow

### Main Flow

1. Người dùng nhấp chọn mã yêu cầu `REQ-YYYYMMDD-XXXX` hoặc nút "View Details" tại trang danh sách.
2. Hệ thống kiểm tra quyền truy cập chi tiết đơn hàng của người dùng phía server:
   - **Nếu là CUSTOMER**: Kiểm tra đơn hàng thuộc về chính khách hàng đó (`created_by == Current Customer ID`).
   - **Nếu là LOGISTICS_MANAGER**: Cho phép xem toàn bộ.
3. Hệ thống truy vấn CSDL và trả về đầy đủ các khối thông tin của yêu cầu vận chuyển:
   - **Header Summary**: Mã yêu cầu, Trạng thái hiện tại (`PENDING`, `APPROVED`, `REJECTED`, `CANCELLED`), Ngày tạo, Người tạo.
   - **Customer Information**: Họ tên khách hàng, Tên CLB/Đơn vị, Email liên hệ, Số điện thoại.
   - **Route & Schedule**: Pickup Address, Destination Address, Expected Departure Date/Time, Transport Mode.
   - **Racehorses List**: Bảng chi tiết từng con ngựa (Horse Name, Passport ID, Breed, Age, Weight, Estimated Value, Individual Notes).
   - **Special Requirements**: Loại khoang xe yêu cầu, Yêu cầu Escort chăm sóc riêng, Chế độ ăn uống/khí hậu.
   - **Approval & Decision Log**: Định danh người phê duyệt/từ chối, Thời điểm thực hiện, Lý do từ chối/hủy (nếu có).
4. Hệ thống hiển thị các nút thao tác ngữ cảnh dựa trên vai trò và trạng thái đơn hàng:
   - Với `CUSTOMER` khi đơn ở trạng thái `PENDING`: Hiển thị "Edit Request", "Manage Racehorses", "Cancel Request".
   - Với `LOGISTICS_MANAGER` khi đơn ở trạng thái `PENDING`: Hiển thị nút "Approve Request" (UC-13) và "Reject Request" (UC-14).
   - Mọi trạng thái khác: Hiển thị giao diện xem chỉ đọc (Read-only) và nút "View Status History" (UC-15).

### Alternative Flow

#### ALT-01: Chuyển hướng sang xem lịch sử trạng thái

1. Tại màn hình chi tiết, người dùng nhấp nút "View Status History".
2. Hệ thống mở màn hình / hộp thoại hiển thị toàn bộ tiến trình thay đổi trạng thái của yêu cầu theo mốc thời gian (UC-15).

### Exception Flow

#### EXC-01: Yêu cầu không tồn tại hoặc đã bị xóa

1. Khách hàng truy cập vào URL chi tiết yêu cầu với ID không hợp lệ hoặc không tồn tại trong hệ thống.
2. Hệ thống trả về lỗi HTTP 404 và hiển thị màn hình thông báo: "Transport request not found."
3. Người dùng nhấp nút "Back to List" để quay về danh sách.

#### EXC-02: Truy cập trái phép đơn hàng của khách hàng khác (Unauthorized Access)

1. Khách hàng A nhập thủ công URL xem chi tiết đơn hàng thuộc sở hữu của Khách hàng B.
2. Hệ thống kiểm tra và phát hiện `created_by != Customer A ID`.
3. Hệ thống chặn truy cập, trả về lỗi HTTP 403 Forbidden và hiển thị thông báo: "You do not have permission to view this transport request."

## Acceptance Criteria

### AC-001: Hiển thị đầy đủ thông tin chi tiết yêu cầu cho Customer hợp lệ

- **Given**: Khách hàng David Michael mở xem chi tiết đơn hàng `REQ-20260915-0001` do chính mình khởi tạo.
- **When**: Trang chi tiết tải xong.
- **Then**: Hệ thống hiển thị chính xác thông tin điểm đi/đến, danh sách 2 con ngựa đua, các yêu cầu đặc biệt và trạng thái `PENDING`.

### AC-002: Chặn khách hàng xem đơn hàng của người khác (Security & Isolation)

- **Given**: Khách hàng A cố gắng truy cập URL chi tiết đơn hàng của Khách hàng B.
- **When**: Gửi yêu cầu GET API.
- **Then**: Hệ thống từ chối với mã lỗi 403 Forbidden và hiển thị "You do not have permission to view this transport request."

### AC-003: Hiển thị các nút thao tác phê duyệt/từ chối cho Logistics Manager

- **Given**: Logistics Manager Trần Anh Tuấn mở xem đơn hàng `REQ-20260915-0001` đang ở trạng thái `PENDING`.
- **When**: Giao diện chi tiết hiển thị.
- **Then**: Hệ thống hiển thị rõ ràng nút "Approve Request" và "Reject Request" trên thanh công cụ thao tác.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- API Truy vấn Chi tiết Đơn hàng (Request Detail Query Endpoint)

## Non-Functional

- Security: Kiểm tra quyền sở hữu và phân quyền RBAC nghiêm ngặt phía Backend.
- Compatibility: Hiển thị tối ưu trên giao diện Web responsive.

## Out of Scope

- Thực hiện trực tiếp việc lập kế hoạch lộ trình chi tiết (được xử lý ở Flow 3 bởi Coordinator).
