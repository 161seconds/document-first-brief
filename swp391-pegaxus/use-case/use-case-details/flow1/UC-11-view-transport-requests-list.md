# UC-11: Xem danh sách yêu cầu vận chuyển (View Transport Requests List)

## Metadata

- **Story**: Là Khách hàng (David Michael) hoặc Quản lý Điều hành Logistics (Trần Anh Tuấn), tôi muốn xem danh sách các yêu cầu vận chuyển kèm các thông tin tóm tắt và bộ lọc tìm kiếm trên ứng dụng Web responsive để dễ dàng theo dõi, phân loại và quản lý tiến độ xử lý các đơn vận chuyển.
- **Context**: UC-11 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Chức năng áp dụng cơ chế Phân quyền truy cập theo vai trò (BR-003): Khách hàng (`CUSTOMER`) chỉ được xem các yêu cầu do chính mình khởi tạo (`created_by = current_user_id`); trong khi Quản lý Điều hành Logistics (`LOGISTICS_MANAGER`) xem được toàn bộ yêu cầu của tất cả khách hàng trên hệ thống. Danh sách hỗ trợ phân trang (Pagination), lọc theo trạng thái (`PENDING`, `APPROVED`, `REJECTED`, `CANCELLED`), tìm kiếm theo mã đơn hàng hoặc địa điểm đi/đến. Hệ thống chạy trên Web responsive (BR-004) với giao diện Tiếng Anh (BR-005).
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
- Phiên làm việc (JWT Session) còn hiệu lực.

### Trigger

- Khách hàng hoặc Logistics Manager chọn mục "Transport Requests" hoặc "Manage Requests" từ thanh menu chính.

## Flow

### Main Flow

1. Người dùng nhấp chọn mục "Transport Requests" trên menu điều hướng của Web app.
2. Hệ thống xác định vai trò (`Role`) và ID người dùng từ JWT Token:
   - **Nếu người dùng là CUSTOMER**: Hệ thống chỉ truy vấn các yêu cầu có `created_by = Customer ID`.
   - **Nếu người dùng là LOGISTICS_MANAGER**: Hệ thống truy vấn toàn bộ yêu cầu vận chuyển trên hệ thống.
3. Hệ thống trả về bảng danh sách các yêu cầu vận chuyển đáp ứng tiêu chí, hiển thị thông tin gồm:
   - Request Code (Mã đơn hàng: `REQ-YYYYMMDD-XXXX`)
   - Customer Name (Tên khách hàng/CLB - hiển thị cho Logistics Manager)
   - Pickup & Destination Locations (Điểm đi và Điểm đến tóm tắt)
   - Expected Departure Date (Ngày khởi hành mong muốn)
   - Total Racehorses (Tổng số lượng ngựa)
   - Status (Trạng thái đơn hàng: `PENDING`, `APPROVED`, `REJECTED`, `CANCELLED` với mã màu trực quan)
   - Created At (Thời điểm tạo)
   - Actions (Nút thao tác: View Details, Edit, Cancel, Approve/Reject tùy theo vai trò và trạng thái)
4. Hệ thống phân trang (mặc định 10 đơn hàng/trang).
5. Người dùng có thể thực hiện tìm kiếm hoặc lọc dữ liệu (xem Alternative Flows).
6. Nhấp chọn một dòng hoặc nút "View Details" để chuyển sang giao diện Chi tiết yêu cầu (UC-12).

### Alternative Flow

#### ALT-01: Lọc danh sách theo Trạng thái (Filter by Status)

1. Tại màn hình danh sách, người dùng chọn giá trị từ bộ lọc "Status Filter" (All, Pending, Approved, Rejected, Cancelled).
2. Hệ thống tự động lọc và hiển thị danh sách các đơn hàng có trạng thái tương ứng.

#### ALT-02: Tìm kiếm yêu cầu vận chuyển (Search Requests)

1. Tại ô tìm kiếm "Search Request", người dùng nhập Mã yêu cầu (ví dụ: `REQ-20260915`), Tên điểm đi/đến hoặc Tên khách hàng.
2. Người dùng nhấn Enter hoặc biểu tượng Tìm kiếm.
3. Hệ thống lọc và hiển thị danh sách các yêu cầu phù hợp với từ khóa tìm kiếm.

### Exception Flow

#### EXC-01: Không tìm thấy yêu cầu nào phù hợp

1. Người dùng áp dụng bộ lọc hoặc từ khóa tìm kiếm không khớp với bất kỳ bản ghi nào trong cơ sở dữ liệu.
2. Hệ thống hiển thị bảng trống kèm thông điệp: "No transport requests found matching your criteria."
3. Người dùng nhấp nút "Clear Filters" để xem lại toàn bộ danh sách.

## Acceptance Criteria

### AC-001: Phân quyền hiển thị dữ liệu chính xác theo vai trò (RBAC)

- **Given**: Khách hàng David Michael có 3 yêu cầu vận chuyển, còn toàn hệ thống có 20 yêu cầu.
- **When**: David Michael truy cập trang "Transport Requests".
- **Then**: Hệ thống chỉ hiển thị đúng 3 yêu cầu do David Michael khởi tạo.

### AC-002: Logistics Manager xem toàn bộ yêu cầu và lọc theo trạng thái PENDING

- **Given**: Logistics Manager Trần Anh Tuấn đăng nhập và truy cập "Manage Requests".
- **When**: Ông Tuấn chọn bộ lọc trạng thái `PENDING`.
- **Then**: Hệ thống hiển thị toàn bộ các yêu cầu ở trạng thái `PENDING` của tất cả khách hàng trên hệ thống để phục vụ công tác phê duyệt.

### AC-003: Phân trang và tìm kiếm theo mã đơn hàng

- **Given**: Người dùng nhập mã `REQ-20260915-0005` vào ô tìm kiếm.
- **When**: Nhấn Enter.
- **Then**: Hệ thống trả về đúng đơn hàng `REQ-20260915-0005` trong bảng dữ liệu $\le 1$ giây.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- API Phân trang & Tìm kiếm Danh sách (Paginated List Query Endpoint)

## Non-Functional

- Security: Chặn truy cập trái phép chéo dữ liệu giữa các Customer ở cấp API Backend.
- Compatibility: Hiển thị co giãn mượt mượt trên điện thoại di động, máy tính bảng và desktop (Responsive Web App).

## Out of Scope

- Xuất dữ liệu danh sách ra file Excel / PDF (nằm ở các yêu cầu báo cáo nâng cao).
