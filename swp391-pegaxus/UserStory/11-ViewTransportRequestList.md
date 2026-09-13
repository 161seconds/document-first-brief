# STORY-011: Xem danh sách yêu cầu vận chuyển

## Metadata

- **Story**: Là một Người dùng (Khách hàng hoặc Quản lý điều hành Logistics), tôi muốn xem danh sách các yêu cầu vận chuyển kèm các thông tin tóm tắt, bộ lọc và công cụ tìm kiếm để dễ dàng theo dõi tiến độ, phân loại và tra cứu các đơn hàng.
- **Context**: Màn hình danh sách đơn hàng là điểm truy cập trung tâm hàng ngày của cả khách hàng lẫn nhà quản lý. Tuy nhiên, phạm vi hiển thị dữ liệu phải được giới hạn chặt chẽ theo vai trò (Data Isolation): Khách hàng chỉ được xem các đơn do chính mình tạo (`customerId == currentUserId`), trong khi Logistics Manager có quyền xem toàn diện tất cả các đơn hàng trên toàn hệ thống để phân loại, thẩm định và điều hành.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập vào hệ thống với vai trò `CUSTOMER` hoặc `LOGISTICS_MANAGER`.

### Trigger

- Người dùng nhấp chọn mục "Yêu cầu vận chuyển" (`Transport Requests`) trên thanh điều hướng chính (`Sidebar` / `Navbar`).

## Flow

### Main Flow

1. Người dùng mở trang Danh sách yêu cầu vận chuyển (`/customer/requests` hoặc `/manager/requests`).
2. Hệ thống kiểm tra vai trò người dùng và áp dụng bộ lọc phân quyền dữ liệu (`Data Scope Filter`):
   - Nếu là `CUSTOMER`: Truy vấn các bản ghi `TransportRequest` có `customerId = currentUserId`.
   - Nếu là `LOGISTICS_MANAGER`: Truy vấn toàn bộ bản ghi `TransportRequest` trên hệ thống.
3. Hệ thống sắp xếp mặc định theo thời gian tạo giảm dần (`createdAt DESC`).
4. Hệ thống tải dữ liệu trang đầu tiên (mặc định 10 bản ghi/trang) và hiển thị bảng thông tin gồm:
   - Mã yêu cầu (`requestCode`): Dạng liên kết nhấp chuột để xem chi tiết.
   - Thông tin khách hàng: Tên khách hàng & Tên CLB (chỉ hiển thị cho Manager).
   - Địa điểm xuất phát $\to$ Địa điểm đến.
   - Thời gian mong muốn khởi hành.
   - Tổng số lượng ngựa (`totalHorses`).
   - Trạng thái hiện tại (`currentStatus`): Hiển thị dưới dạng huy hiệu màu sắc trực quan (Vàng: `Chờ duyệt`, Xanh lá: `Đã duyệt`, Đỏ: `Từ chối`, Xám: `Hủy`, Xanh dương: `Đang vận chuyển`).
   - Ngày tạo yêu cầu.
   - Cột hành động: Xem chi tiết, Sửa (nếu là Customer & đơn chờ duyệt), Duyệt/Từ chối nhanh (nếu là Manager).
5. Người dùng có thể sử dụng các công cụ:
   - Thanh tìm kiếm: Tìm kiếm tức thời theo mã yêu cầu hoặc tên địa điểm.
   - Bộ lọc trạng thái: Lọc theo Tất cả, Chờ duyệt, Đã duyệt, Từ chối, Đã hủy.
   - Bộ lọc khoảng thời gian khởi hành (Từ ngày - Đến ngày).
   - Phân trang: Chọn số lượng hiển thị (10, 25, 50 dòng/trang) và chuyển trang.
6. Hệ thống thực hiện truy vấn động và làm mới danh sách dữ liệu theo điều kiện lọc đã chọn.

### Alternative Flow

#### ALT-01: Manager lọc các yêu cầu "Chờ duyệt" cần xử lý gấp

1. Logistics Manager nhấp chọn tab nhanh "Cần xử lý" (`Pending Review`).
2. Hệ thống lọc nhanh các yêu cầu có trạng thái `PENDING_APPROVAL` và sắp xếp ưu tiên các đơn có ngày khởi hành gần nhất lên đầu.

### Exception Flow

#### EXC-01: Không tìm thấy yêu cầu nào phù hợp điều kiện tìm kiếm

1. Người dùng nhập từ khóa tìm kiếm hoặc chọn bộ lọc nhưng không có bản ghi nào khớp.
2. Hệ thống hiển thị giao diện trạng thái trống (`Empty State`):
   - Hình minh họa thân thiện.
   - Thông điệp: "Không tìm thấy yêu cầu vận chuyển nào phù hợp".
   - Nút hành động: "Đặt lại bộ lọc" (`Reset Filters`).

#### EXC-02: Khách hàng mới chưa có đơn hàng nào

1. Khách hàng mới đăng ký lần đầu truy cập danh sách yêu cầu.
2. Hệ thống hiển thị màn hình khởi tạo:
   - Thông điệp: "Bạn chưa tạo yêu cầu vận chuyển nào".
   - Nút hành động nổi bật: "Tạo yêu cầu vận chuyển đầu tiên" (Dẫn tới STORY-007).

## Acceptance Criteria

#### AC-001: Khách hàng chỉ xem được danh sách đơn hàng của chính mình

- **Given**: Khách hàng A có 2 đơn hàng, Khách hàng B có 3 đơn hàng.
- **When**: Khách hàng A đăng nhập và xem danh sách yêu cầu vận chuyển.
- **Then**: Hệ thống chỉ hiển thị đúng 2 đơn hàng của Khách hàng A, hoàn toàn không nhìn thấy bất kỳ đơn nào của Khách hàng B.

#### AC-002: Logistics Manager xem được toàn bộ danh sách đơn hàng

- **Given**: Toàn hệ thống có 50 đơn hàng từ nhiều khách hàng khác nhau.
- **When**: Logistics Manager đăng nhập và vào màn hình danh sách.
- **Then**: Hệ thống hiển thị đầy đủ 50 đơn hàng kèm tên khách hàng và CLB tương ứng.

#### AC-003: Lọc chính xác theo trạng thái

- **Given**: Danh sách có 5 đơn `PENDING_APPROVAL` và 10 đơn `APPROVED`.
- **When**: Người dùng chọn bộ lọc trạng thái "Chờ duyệt".
- **Then**: Bảng dữ liệu chỉ hiển thị đúng 5 đơn `PENDING_APPROVAL`.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-003: Quy định phân quyền truy cập theo vai trò (RBAC)
- BR-017: Quy chuẩn phân trang và lọc dữ liệu danh sách đơn hàng

### Dependencies

- Bảng `TransportRequest`, `User`, `CustomerProfile`

## Non-Functional

- Thời gian tải trang danh sách kèm phân trang: $\le 400$ ms đối với tập dữ liệu 100.000 bản ghi (có đánh chỉ mục index trên `customerId`, `currentStatus`, `createdAt`).
- Hỗ trợ giao diện phản hồi linh hoạt (Responsive) hiển thị tốt trên cả màn hình Laptop và Tablet.

## Out of Scope

- Xuất khẩu danh sách đơn hàng ra file Excel / CSV (chức năng báo cáo quản trị nâng cao ở Flow 6).
