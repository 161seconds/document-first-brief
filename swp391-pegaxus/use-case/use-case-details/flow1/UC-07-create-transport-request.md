# UC-07: Tạo yêu cầu vận chuyển mới (Create New Transport Request)

## Metadata

- **Story**: Là Khách hàng (David Michael - Chủ sở hữu hoặc đại diện CLB Đua ngựa), tôi muốn khởi tạo một yêu cầu vận chuyển ngựa đua mới trên ứng dụng Web responsive bao gồm thông tin điểm đi, điểm đến và thời gian khởi hành mong muốn để yêu cầu công ty logistics kiểm tra và xử lý chuyến vận chuyển.
- **Context**: UC-07 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Khách hàng sau khi đăng nhập thành công có thể khởi tạo yêu cầu đặt dịch vụ vận chuyển ngựa đua xuyên biên giới Châu Âu (BR-005). Yêu cầu được hệ thống sinh mã định danh duy nhất (Request Code format `REQ-YYYYMMDD-XXXX`), mặc định chuyển sang trạng thái "Chờ duyệt" (`PENDING`) và lưu trữ mốc thời gian tạo. Hệ thống vận hành trên nền tảng Web responsive (BR-004) với giao diện Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập thành công vào hệ thống với vai trò `CUSTOMER` và có phiên làm việc hợp lệ.
- Khách hàng đang ở giao diện Bảng điều khiển Khách hàng (Customer Dashboard) hoặc Trang danh sách yêu cầu.

### Trigger

- Khách hàng nhấp chọn nút "Create Transport Request" hoặc "+ New Request" trên giao diện Web app.

## Flow

### Main Flow

1. Khách hàng nhấp nút "+ New Request" trên thanh menu hoặc giao diện quản lý yêu cầu.
2. Hệ thống hiển thị biểu mẫu "Create Transport Request" với các thông tin cần nhập:
   - Pickup Address (Địa chỉ/Trang trại điểm đi tại Châu Âu)
   - Destination Address (Địa chỉ/Trường đua/CLB điểm đến tại Châu Âu)
   - Expected Departure Date & Time (Ngày và giờ mong muốn khởi hành)
   - Transport Type (Loại hình vận chuyển: Đường bộ / Đường hàng không / Kết hợp)
   - General Notes (Ghi chú chung cho đơn hàng - không bắt buộc)
3. Khách hàng nhập đầy đủ các thông tin điểm đi, điểm đến, chọn thời gian khởi hành và loại hình vận chuyển.
4. Khách hàng nhấp chọn "Save & Continue to Add Racehorses" hoặc "Submit Request".
5. Hệ thống kiểm tra dữ liệu đầu vào phía client:
   - Điểm đi và điểm đến không được để trống và phải thuộc phạm vi địa lý Châu Âu.
   - Thời gian khởi hành mong muốn phải ở tương lai (tối thiểu sau 48 giờ so với thời điểm hiện tại).
6. Hệ thống tạo mã yêu cầu vận chuyển duy nhất (`REQ-YYYYMMDD-XXXX`), lưu thông tin vào cơ sở dữ liệu với trạng thái mặc định `PENDING` (Chờ duyệt) và gắn thông tin người tạo (`created_by = Customer ID`).
7. Hệ thống hiển thị thông báo thành công: "Transport request REQ-YYYYMMDD-XXXX created successfully as PENDING."
8. Hệ thống điều hướng khách hàng sang màn hình Thêm danh sách ngựa và yêu cầu đặc biệt (UC-08) hoặc màn hình Chi tiết yêu cầu (UC-12).

### Alternative Flow

*Không có luồng rẽ nhánh cho chức năng này.*

### Exception Flow

#### EXC-01: Ngày khởi hành mong muốn không hợp lệ (trong quá khứ hoặc quá gấp)

1. Tại bước 5 của Luồng chính, khách hàng chọn ngày khởi hành nhỏ hơn thời điểm hiện tại hoặc trong vòng 48 giờ tiếp theo.
2. Hệ thống hiển thị thông báo lỗi dưới ô chọn thời gian: "Departure date must be at least 48 hours in advance for cross-border logistics clearance."
3. Khách hàng chọn lại thời gian hợp lệ và tiếp tục.

#### EXC-02: Địa chỉ điểm đi và điểm đến trùng nhau

1. Tại bước 5 của Luồng chính, địa chỉ Pickup Address và Destination Address giống hệt nhau.
2. Hệ thống hiển thị thông báo lỗi: "Destination address must be different from pickup address."
3. Khách hàng điều chỉnh lại địa chỉ và thực hiện lại thao tác.

## Acceptance Criteria

### AC-001: Khởi tạo yêu cầu vận chuyển mới thành công

- **Given**: Khách hàng đã đăng nhập và đang ở màn hình tạo yêu cầu.
- **When**: Khách hàng nhập hợp lệ điểm đi "Paris Stables, France", điểm đến "Aachen Equestrian Center, Germany", chọn ngày khởi hành sau 5 ngày và nhấn "Submit Request".
- **Then**: Hệ thống sinh mã yêu cầu `REQ-YYYYMMDD-XXXX`, đặt trạng thái `PENDING`, lưu thời gian tạo và điều hướng đến bước tiếp theo kèm thông báo "Transport request created successfully."

### AC-002: Chặn chọn thời gian khởi hành không đủ thời gian chuẩn bị

- **Given**: Khách hàng nhập điểm đi, điểm đến và chọn ngày khởi hành là ngày mai (trong vòng 24 giờ).
- **When**: Khách hàng nhấn "Submit Request".
- **Then**: Hệ thống báo lỗi "Departure date must be at least 48 hours in advance for cross-border logistics clearance." và không lưu yêu cầu.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-016: Quy định Mã Yêu cầu & Trạng thái Ban đầu của Đơn hàng
- BR-017: Quy định Thời gian Khởi hành Mong muốn Tối thiểu (>= 48h)
- BR-018: Quy định Địa điểm Khởi hành và Điểm đến

### Dependencies

- Dịch vụ định vị địa lý / Mã hóa địa chỉ khu vực Châu Âu (Geocoding / Address Validation API)

## Non-Functional

- Security: Xác thực vai trò `CUSTOMER` qua JWT Token, đảm bảo khách hàng chỉ tạo được yêu cầu cho chính mình.
- Compatibility: Vận hành mượt mà trên giao diện Web responsive (Desktop, Tablet, Mobile).

## Out of Scope

- Tự động lập lịch di chuyển chi tiết (do Coordinator thực hiện ở Flow 3).
- Phê duyệt tự động yêu cầu vận chuyển (phải qua xem xét của Logistics Manager ở UC-13).
