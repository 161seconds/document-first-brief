# STORY-015: Xem lịch sử thay đổi trạng thái

## Metadata

- **Story**: Là một Người dùng (Khách hàng hoặc Quản lý điều hành Logistics), tôi muốn xem dòng thời gian (Timeline) ghi lại toàn bộ lịch sử thay đổi trạng thái của yêu cầu vận chuyển (thời điểm, thao tác, người thực hiện, lý do) để bảo đảm tính minh bạch và khả năng truy vết trách nhiệm trong suốt vòng đời xử lý đơn hàng.
- **Context**: Do giá trị kinh tế của ngựa đua rất lớn và quy trình vận chuyển trải qua nhiều mắt xích kiểm duyệt nghiêm ngặt, việc truy vết vết tích kiểm toán (`Audit Trail`) là yêu cầu pháp lý bắt buộc. Khách hàng cần biết đơn hàng của mình đã trải qua các khâu nào và ai đã xử lý; nhà quản lý cần căn cứ vào lịch sử này để kiểm tra hiệu suất xử lý đơn của đội ngũ và giải quyết các khiếu nại nếu có tranh chấp về thời gian xử lý.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập vào hệ thống.
- Yêu cầu vận chuyển tồn tại trên hệ thống.
- Người dùng có quyền xem yêu cầu (là Khách hàng sở hữu đơn hoặc có vai trò `LOGISTICS_MANAGER`).

### Trigger

- Người dùng chọn tab "Lịch sử trạng thái" (`Status History` / `Audit Trail`) trong màn hình Chi tiết yêu cầu vận chuyển.

## Flow

### Main Flow

1. Người dùng mở màn hình Chi tiết yêu cầu vận chuyển và chọn tab "Lịch sử trạng thái".
2. Hệ thống kiểm tra quyền xem dữ liệu của người dùng.
3. Hệ thống truy vấn toàn bộ các bản ghi trong bảng `RequestStatusHistory` liên kết với `requestId` hiện tại.
4. Hệ thống kết hợp thông tin người thực hiện từ bảng `User` (Họ tên, Vai trò).
5. Hệ thống sắp xếp các bản ghi theo thời gian giảm dần (`performedAt DESC`) hoặc tăng dần theo tùy chọn của người dùng.
6. Hệ thống hiển thị dòng thời gian trực quan (`Visual Timeline`):
   - Mỗi mốc lịch sử gồm:
     - Thời gian chính xác: Ngày, giờ, phút, giây (`DD/MM/YYYY HH:mm:ss`).
     - Tên hành động nghiệp vụ (`action`): Ví dụ `Khởi tạo yêu cầu`, `Cập nhật thông tin`, `Phê duyệt yêu cầu`, `Từ chối tiếp nhận`, `Hủy yêu cầu`.
     - Chuyển đổi trạng thái: Trạng thái cũ $\to$ Trạng thái mới (kèm huy hiệu màu sắc tương ứng).
     - Người thực hiện: Họ tên và vai trò (Customer hoặc Logistics Manager).
     - Ghi chú hoặc lý do đi kèm: Hiển thị lý do từ chối, lý do hủy hoặc ghi chú duyệt (nếu có).
7. Người dùng có thể nhấn vào từng mốc để xem chi tiết nội dung thay đổi hoặc thu gọn/mở rộng dòng thời gian.

### Alternative Flow

#### ALT-01: Xem lịch sử rút gọn ngay tại đầu trang chi tiết

1. Tại trang chi tiết yêu cầu, một thanh tiến trình rút gọn (`Mini Stepper`) luôn hiển thị ở đầu trang mô tả các giai đoạn chính: `Khởi tạo` $\to$ `Chờ duyệt` $\to$ `Đã duyệt` (hoặc `Từ chối` / `Đã hủy`).
2. Người dùng di chuột (`Hover`) vào một bước đã hoàn thành trên thanh tiến trình.
3. Hệ thống hiển thị bong bóng thông tin (`Tooltip`) chứa thời điểm và người thực hiện bước đó.

### Exception Flow

#### EXC-01: Không thể tải lịch sử do lỗi kết nối CSDL

1. Khi người dùng mở tab lịch sử, dịch vụ truy vấn CSDL gặp trục trặc tạm thời.
2. Hệ thống hiển thị thông báo lỗi thân thiện: "Không thể tải lịch sử thay đổi trạng thái vào lúc này. Vui lòng nhấn nút 'Thử lại'".
3. Người dùng nhấn nút "Thử lại" để hệ thống gửi lại truy vấn.

## Acceptance Criteria

#### AC-001: Hiển thị đầy đủ chuỗi lịch sử từ khởi tạo đến phê duyệt

- **Given**: Yêu cầu `REQ-001` được tạo bởi Khách hàng lúc 08:00 và được Manager duyệt lúc 10:30.
- **When**: Người dùng mở tab Lịch sử trạng thái của `REQ-001`.
- **Then**: Hệ thống hiển thị ít nhất 2 mốc sự kiện:
  - Mốc 1: 08:00 - `CREATE_REQUEST` - Trạng thái: Chờ duyệt - Người thực hiện: Khách hàng.
  - Mốc 2: 10:30 - `APPROVE_REQUEST` - Trạng thái: Chờ duyệt -> Đã duyệt - Người thực hiện: Manager.

#### AC-002: Hiển thị lý do hủy hoặc từ chối rõ ràng trong lịch sử

- **Given**: Yêu cầu `REQ-003` bị từ chối bởi Manager với lý do "Đường đèo đang sạt lở".
- **When**: Người dùng xem lịch sử trạng thái của `REQ-003`.
- **Then**: Mốc từ chối hiển thị rõ ràng nội dung "Đường đèo đang sạt lở" bên cạnh tên Manager thực hiện.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-020: Quy chuẩn ghi vết nhật ký kiểm toán và bất biến dữ liệu lịch sử trạng thái (Audit Trail Immutability)

### Dependencies

- Bảng `RequestStatusHistory`, `TransportRequest`, `User`

## Non-Functional

- Toàn bộ bản ghi trong `RequestStatusHistory` là bất biến (`Append-only`), nghiêm cấm mọi thao tác `UPDATE` hoặc `DELETE` trên bảng này.
- Thời gian tải toàn bộ dòng thời gian: $\le 200$ ms.

## Out of Scope

- So sánh chi tiết từng trường dữ liệu thay đổi giữa các phiên bản (Diff view chi tiết của payload JSON) trong Sprint 1.
