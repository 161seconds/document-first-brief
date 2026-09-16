# STORY-007: Tạo yêu cầu vận chuyển mới

## Metadata

- **Story**: Là một Khách hàng (Customer), tôi muốn khởi tạo một yêu cầu vận chuyển ngựa đua mới bằng cách cung cấp địa điểm xuất phát, địa điểm đến và thời gian khởi hành mong muốn để đơn vị vận chuyển tiếp nhận, khảo sát tính khả thi và thẩm định báo giá.
- **Context**: Vận chuyển ngựa đua đòi hỏi kế hoạch chi tiết từ điểm xuất phát (chuồng trại/CLB) tới điểm đích (trường đua quốc tế, triển lãm hoặc cơ sở cách ly). Khách hàng khởi tạo yêu cầu bằng cách nhập các thông tin căn bản của hành trình. Hệ thống tự động sinh mã định danh yêu cầu duy nhất (`requestCode`), gắn trạng thái khởi điểm `Chờ duyệt` (`PENDING_APPROVAL`) và ghi nhận dấu thời gian tạo để làm căn cứ thẩm định theo cam kết dịch vụ (SLA).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập với tài khoản hợp lệ có vai trò `CUSTOMER`.
- Hồ sơ khách hàng (`CustomerProfile`) đã có đầy đủ thông tin liên hệ cơ bản (Họ tên, SĐT).

### Trigger

- Khách hàng nhấp chọn nút "Tạo yêu cầu mới" (`Create Request`) tại màn hình Quản lý vận chuyển của Khách hàng.

## Flow

### Main Flow

1. Khách hàng mở biểu mẫu tạo yêu cầu vận chuyển mới (`/customer/requests/create`).
2. Hệ thống hiển thị biểu mẫu nhập liệu Bước 1 - Thông tin hành trình:
   - Địa điểm xuất phát (`departureLocation`): Tên địa chỉ, Quốc gia, Tỉnh/Thành phố, Tọa độ gợi ý.
   - Địa điểm đến (`destinationLocation`): Tên địa chỉ, Quốc gia, Tỉnh/Thành phố, Tọa độ gợi ý.
   - Thời gian mong muốn khởi hành (`requestedDepartureTime`): Ngày và giờ dự kiến (bắt buộc cách thời điểm hiện tại tối thiểu 48 giờ đối với nội địa hoặc 7 ngày đối với xuyên biên giới).
   - Phương thức vận chuyển mong muốn: Đường bộ chuyên dụng (`Road`), Hàng không kết hợp (`Air`), hoặc Để công ty tư vấn tối ưu (`Recommended`).
   - Ghi chú chung cho chuyến đi (`notes`).
3. Khách hàng nhập đầy đủ các trường bắt buộc và nhấn nút "Tiếp tục" (`Next`).
4. Hệ thống kiểm tra tính hợp lệ của dữ liệu:
   - Điểm đi và điểm đến không được trùng nhau.
   - Thời gian khởi hành phải nằm trong tương lai và thỏa mãn quy tắc thời gian chuẩn bị tối thiểu.
5. Hệ thống sinh mã yêu cầu theo định dạng chuẩn: `REQ-YYYYMMDD-XXXX` (ví dụ: `REQ-20260912-0042`).
6. Hệ thống lưu bản ghi mới vào bảng `TransportRequest` với:
   - `customerId`: ID của khách hàng đang đăng nhập.
   - `currentStatus`: `PENDING_APPROVAL` (Chờ duyệt).
   - `totalHorses`: Khởi tạo mặc định bằng 0 (sẽ cập nhật ở bước thêm ngựa).
   - `createdAt`, `updatedAt`: Thời gian hiện tại.
7. Hệ thống tự động ghi một bản ghi vào bảng `RequestStatusHistory`:
   - `requestId`: ID yêu cầu vừa tạo.
   - `fromStatus`: `NULL`.
   - `toStatus`: `PENDING_APPROVAL`.
   - `action`: `CREATE_REQUEST`.
   - `performedBy`: ID của khách hàng.
   - `performedAt`: Thời gian hiện tại.
8. Hệ thống điều hướng khách hàng sang Bước 2: "Quản lý danh sách ngựa và yêu cầu đặc biệt" (STORY-008).

### Alternative Flow

#### ALT-01: Lưu bản nháp yêu cầu (`Save as Draft`)

1. Tại bước 3 của Luồng chính, khách hàng chưa muốn gửi thẩm định ngay mà chỉ muốn lưu tạm thông tin hành trình. Khách hàng chọn nút "Lưu bản nháp".
2. Hệ thống lưu yêu cầu với trạng thái `DRAFT`.
3. Yêu cầu hiển thị trong danh sách của khách hàng với nhãn "Bản nháp", cho phép tiếp tục chỉnh sửa bất kỳ lúc nào mà chưa chuyển tới Logistics Manager.

### Exception Flow

#### EXC-01: Điểm đi và điểm đến trùng nhau

1. Tại bước 4 của Luồng chính, khách hàng chọn địa chỉ xuất phát và địa chỉ đến giống hệt nhau.
2. Hệ thống hiển thị cảnh báo đỏ tại ô địa điểm đến: "Địa điểm đến không được trùng với địa điểm xuất phát".
3. Chặn hành động tiếp tục.

#### EXC-02: Thời gian khởi hành không hợp lệ (quá gần thời điểm hiện tại)

1. Tại bước 4 của Luồng chính, khách hàng chọn thời gian khởi hành trong quá khứ hoặc cách thời điểm hiện tại dưới 48 giờ.
2. Hệ thống hiển thị cảnh báo nghiệp vụ: "Thời gian khởi hành phải cách thời điểm hiện tại tối thiểu 48 giờ để chuẩn bị phương tiện và xét duyệt kiểm dịch".
3. Yêu cầu khách hàng chọn lại ngày giờ.

## Acceptance Criteria

#### AC-001: Khởi tạo thành công yêu cầu vận chuyển hợp lệ

- **Given**: Khách hàng nhập điểm đi "CLB Đua Ngựa Phú Thọ, TP.HCM", điểm đến "Sân bay Quốc tế Tân Sơn Nhất", thời gian khởi hành cách 5 ngày tới.
- **When**: Khách hàng nhấn nút "Tiếp tục".
- **Then**: Hệ thống tạo bản ghi `TransportRequest` với trạng thái `PENDING_APPROVAL` và mã định dạng `REQ-YYYYMMDD-XXXX`.
- **And**: Ghi nhận một bản ghi tương ứng trong `RequestStatusHistory` với hành động `CREATE_REQUEST`.

#### AC-002: Chặn tạo yêu cầu khi thời gian khởi hành không hợp lệ

- **Given**: Thời điểm hiện tại là 10:00 ngày 12/09/2026.
- **When**: Khách hàng chọn thời gian khởi hành là 14:00 ngày 12/09/2026 (cách 4 tiếng).
- **Then**: Hệ thống báo lỗi vi phạm quy tắc thời gian tối thiểu và không tạo bản ghi yêu cầu mới.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-010: Quy định về thời gian đặt chỗ tối thiểu trước chuyến vận chuyển
- BR-011: Quy tắc sinh mã yêu cầu vận chuyển duy nhất

### Dependencies

- Bảng dữ liệu địa lý / Bản đồ địa chỉ (Mapbox / Google Maps Geocoding API)

## Non-Functional

- Thời gian phản hồi tạo bản ghi: $\le 500$ ms.
- Mã `requestCode` phải đảm bảo duy nhất tuyệt đối trong toàn hệ thống (sử dụng ràng buộc `UNIQUE` trên CSDL).

## Out of Scope

- Báo giá cước vận chuyển chính thức (Báo giá được tự động tính toán sơ bộ hoặc do Logistics Manager thẩm định duyệt ở bước sau).
