# STORY-013: Phê duyệt yêu cầu vận chuyển

## Metadata

- **Story**: Là một Quản lý Điều hành Logistics (Logistics Manager), tôi muốn kiểm tra tính khả thi của một yêu cầu vận chuyển đang chờ duyệt và thực hiện phê duyệt chính thức để chuyển đơn sang trạng thái "Đã duyệt", ghi nhận định danh người duyệt và thời điểm phê duyệt, đồng thời khởi động các quy trình tiếp theo cho đội ngũ Chuyên viên thủ tục và Điều phối viên.
- **Context**: Sau khi khách hàng nộp yêu cầu, Logistics Manager phải đánh giá tính khả thi: khả năng đáp ứng của đội xe tải/thùng bay, tuyến đường di chuyển, tính hợp lệ của thời gian khởi hành và năng lực chuyên chở. Khi đơn được duyệt, hệ thống chuyển trạng thái sang `APPROVED`, kích hoạt luồng khởi tạo hồ sơ kiểm dịch số (Flow 2) và thiết kế lộ trình (Flow 3).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập và sở hữu vai trò `LOGISTICS_MANAGER`.
- Yêu cầu vận chuyển đang ở trạng thái `PENDING_APPROVAL`.

### Trigger

- Logistics Manager nhấp chọn nút "Phê duyệt" (`Approve Request`) trên màn hình chi tiết hoặc danh sách yêu cầu.

## Flow

### Main Flow

1. Logistics Manager mở trang chi tiết yêu cầu vận chuyển cần thẩm định.
2. Manager xem xét toàn bộ thông tin: Lộ trình, ngày giờ khởi hành, danh sách cá thể ngựa, yêu cầu chuồng trại và chế độ ăn uống.
3. Manager nhấn nút "Phê duyệt yêu cầu" (`Approve Request`).
4. Hệ thống hiển thị hộp thoại xác nhận phê duyệt (`Approval Modal`):
   - Tóm tắt thông tin đơn: Mã yêu cầu, Khách hàng, Tuyến đường, Tổng số ngựa (`totalHorses`).
   - Tùy chọn chỉ định nhân sự phụ trách:
     - Chuyên viên Kiểm dịch & Thủ tục (`Transport Specialist`).
     - Điều phối viên Đội xe & Lộ trình (`Fleet & Route Coordinator`).
   - Ghi chú phê duyệt nội bộ (`approvalNotes` - tùy chọn).
5. Manager chọn Specialist, Coordinator phụ trách và nhấn "Xác nhận Phê duyệt".
6. Hệ thống kiểm tra trạng thái hiện tại của đơn trong CSDL vẫn là `PENDING_APPROVAL`.
7. Hệ thống cập nhật bản ghi `TransportRequest`:
   - `currentStatus = 'APPROVED'`.
   - `approvedBy = [ID của Manager đang đăng nhập]`.
   - `approvedAt = [Thời gian hiện tại]`.
   - `updatedAt = [Thời gian hiện tại]`.
8. Hệ thống tạo một bản ghi mới trong bảng `RequestStatusHistory`:
   - `requestId`: ID yêu cầu.
   - `fromStatus`: `PENDING_APPROVAL`.
   - `toStatus`: `APPROVED`.
   - `action`: `APPROVE_REQUEST`.
   - `reason`: [Ghi chú phê duyệt của Manager nếu có].
   - `performedBy`: ID của Manager.
   - `performedAt`: Thời gian hiện tại.
9. Hệ thống gửi thông báo tự động (Push Notification & Email):
   - Tới Khách hàng: "Yêu cầu vận chuyển [Mã yêu cầu] của quý khách đã được phê duyệt thành công. Vui lòng chuẩn bị hồ sơ y tế/kiểm dịch cho ngựa".
   - Tới Transport Specialist được chỉ định: "Bạn được giao phụ trách hồ sơ pháp lý cho đơn [Mã yêu cầu]".
   - Tới Fleet & Route Coordinator được chỉ định: "Bạn được giao lập kế hoạch lộ trình cho đơn [Mã yêu cầu]".
10. Hệ thống hiển thị thông báo thành công: "Phê duyệt yêu cầu vận chuyển thành công!" và làm mới giao diện với trạng thái `Đã duyệt` (màu xanh lá).

### Alternative Flow

#### ALT-01: Phê duyệt nhanh từ danh sách yêu cầu (`Quick Approval`)

1. Tại màn hình danh sách, Manager nhấn nút "Duyệt nhanh" tại dòng của đơn hàng chờ duyệt.
2. Hệ thống hiển thị hộp thoại xác nhận rút gọn.
3. Manager xác nhận duyệt. Hệ thống thực hiện các bước cập nhật tương tự và giữ nguyên màn hình danh sách.

### Exception Flow

#### EXC-01: Đơn hàng đã bị hủy bởi khách hàng trước thời điểm duyệt

1. Khách hàng đã nhấn Hủy đơn lúc 11:00 (`status = 'CANCELLED'`).
2. Manager vẫn giữ màn hình cũ và nhấn Duyệt đơn lúc 11:01.
3. Hệ thống phát hiện trạng thái CSDL không còn là `PENDING_APPROVAL`.
4. Hệ thống từ chối cập nhật, hiển thị thông báo lỗi: "Không thể phê duyệt. Yêu cầu này đã bị khách hàng hủy bỏ trước đó".
5. Giao diện tải lại trạng thái mới nhất của đơn hàng.

#### EXC-02: Không có quyền phê duyệt (403 Forbidden)

1. Tài xế hoặc Khách hàng cố tình gửi API request `POST /api/v1/requests/{id}/approve`.
2. Hệ thống kiểm tra vai trò người dùng không phải `LOGISTICS_MANAGER`.
3. Hệ thống từ chối thực hiện và trả về mã lỗi 403 Forbidden.

## Acceptance Criteria

#### AC-001: Phê duyệt thành công yêu cầu chờ duyệt

- **Given**: Yêu cầu `REQ-001` đang ở trạng thái `PENDING_APPROVAL`.
- **When**: Logistics Manager nhấn phê duyệt và xác nhận.
- **Then**: Trạng thái yêu cầu chuyển thành `APPROVED`, trường `approvedBy` lưu ID của Manager và `approvedAt` lưu thời điểm hiện tại.
- **And**: Ghi nhận một dòng lịch sử trong `RequestStatusHistory` với hành động `APPROVE_REQUEST`.

#### AC-002: Kích hoạt gửi thông báo đa kênh sau khi duyệt

- **Given**: Đơn hàng vừa được Manager duyệt thành công.
- **When**: Quá trình cập nhật CSDL hoàn tất.
- **Then**: Hệ thống tự động tạo các thông báo gửi tới Khách hàng, Transport Specialist và Fleet & Route Coordinator.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-003: Quy định phân quyền truy cập theo vai trò (RBAC - Thẩm quyền phê duyệt của Manager)
- BR-018: Quy chuẩn trạng thái vòng đời yêu cầu vận chuyển (Request Lifecycle State Machine)

### Dependencies

- Bảng `TransportRequest`, `RequestStatusHistory`, `User`
- Hệ thống thông báo In-app & Email Notification Engine

## Non-Functional

- Thao tác phê duyệt và lưu vết hoàn tất trong thời gian $\le 500$ ms.
- Đảm bảo tính toàn vẹn giao dịch (Database Transaction) giữa việc đổi trạng thái đơn và ghi bản ghi lịch sử trạng thái.

## Out of Scope

- Bố trí xếp xe cụ thể và chỉ định tài xế chi tiết (chức năng của Fleet & Route Coordinator ở Flow 3).
