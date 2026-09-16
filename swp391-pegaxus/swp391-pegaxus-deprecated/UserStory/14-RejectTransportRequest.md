# STORY-014: Từ chối yêu cầu vận chuyển

## Metadata

- **Story**: Là một Quản lý Điều hành Logistics (Logistics Manager), tôi muốn từ chối tiếp nhận một yêu cầu vận chuyển không đủ điều kiện khả thi (hết xe chuyên dụng, tuyến đường đang bị phong tỏa kiểm dịch, thời gian không kịp chuẩn bị giấy tờ) và bắt buộc nhập lý do từ chối rõ ràng để phản hồi minh bạch cho khách hàng.
- **Context**: Không phải mọi yêu cầu vận chuyển đều có thể được tiếp nhận. Đội xe có thể đã hết tải trọng, tuyến đường quốc tế có thể đang bùng phát dịch bệnh Equine Influenza khiến biên giới đóng cửa, hoặc yêu cầu quá gấp không kịp làm xét nghiệm máu Coggins Test. Logistics Manager có thẩm quyền từ chối yêu cầu nhưng bắt buộc phải nêu rõ lý do cụ thể để khách hàng hiểu và có thể điều chỉnh lại thời gian hoặc lộ trình.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập và có vai trò `LOGISTICS_MANAGER`.
- Yêu cầu vận chuyển đang ở trạng thái `PENDING_APPROVAL`.

### Trigger

- Logistics Manager nhấp chọn nút "Từ chối" (`Reject Request`) trên màn hình chi tiết hoặc danh sách yêu cầu.

## Flow

### Main Flow

1. Logistics Manager mở trang chi tiết yêu cầu vận chuyển và đánh giá không đủ điều kiện tiếp nhận.
2. Manager nhấn nút "Từ chối tiếp nhận" (`Reject Request`).
3. Hệ thống hiển thị hộp thoại xác nhận từ chối (`Rejection Modal`):
   - Danh sách lý do từ chối mẫu chọn nhanh:
     - "Không còn phương tiện chuyên dụng phù hợp vào thời gian này".
     - "Tuyến đường di chuyển đang bị hạn chế kiểm dịch động vật sống".
     - "Thời gian yêu cầu quá gấp, không đủ thời gian làm thủ tục kiểm dịch quốc tế".
     - "Địa điểm xuất phát / đích đến nằm ngoài khu vực phục vụ".
     - "Lý do khác".
   - Khung nhập văn bản giải thích chi tiết lý do (`rejectionReason`): Bắt buộc nhập tối thiểu 15 ký tự để khách hàng hiểu rõ nguyên nhân.
   - Gợi ý hướng xử lý cho khách hàng (tùy chọn: ví dụ lùi ngày khởi hành hoặc đổi điểm tập kết).
4. Manager chọn lý do hoặc nhập văn bản giải thích chi tiết.
5. Manager nhấn nút "Xác nhận Từ chối".
6. Hệ thống kiểm tra lý do từ chối không được để trống và đạt độ dài tối thiểu.
7. Hệ thống cập nhật bản ghi `TransportRequest`:
   - `currentStatus = 'REJECTED'`.
   - `rejectionReason = [Lý do từ chối Manager đã nhập]`.
   - `updatedAt = [Thời gian hiện tại]`.
8. Hệ thống tạo một bản ghi mới trong bảng `RequestStatusHistory`:
   - `requestId`: ID yêu cầu.
   - `fromStatus`: `PENDING_APPROVAL`.
   - `toStatus`: `REJECTED`.
   - `action`: `REJECT_REQUEST`.
   - `reason`: [Lý do từ chối].
   - `performedBy`: ID của Manager.
   - `performedAt`: Thời gian hiện tại.
9. Hệ thống gửi thông báo In-app và Email cho Khách hàng: "Yêu cầu vận chuyển [Mã yêu cầu] không được tiếp nhận. Lý do: [Lý do từ chối]".
10. Hệ thống hiển thị thông báo thành công: "Đã từ chối yêu cầu vận chuyển" và cập nhật trạng thái đơn thành `Từ chối` (màu đỏ).

### Alternative Flow

#### ALT-01: Manager đóng hộp thoại từ chối

1. Tại bước 3 của Luồng chính, Manager quyết định trao đổi thêm với khách hàng qua điện thoại trước và nhấn "Hủy bỏ".
2. Hệ thống đóng hộp thoại và giữ nguyên trạng thái `PENDING_APPROVAL` của yêu cầu.

### Exception Flow

#### EXC-01: Bỏ trống hoặc nhập lý do từ chối quá ngắn

1. Tại bước 5 của Luồng chính, ô lý do từ chối bị bỏ trống hoặc dưới 15 ký tự.
2. Hệ thống chặn việc gửi yêu cầu và hiển thị thông báo lỗi: "Vui lòng cung cấp lý do từ chối cụ thể (tối thiểu 15 ký tự) để gửi phản hồi chính thức cho khách hàng".

#### EXC-02: Đơn hàng đã được duyệt bởi một Manager khác trước đó

1. Hai Manager cùng mở xem đơn `REQ-001`.
2. Manager A nhấn Duyệt đơn thành công lúc 10:00.
3. Manager B nhấn Từ chối lúc 10:01.
4. Hệ thống phát hiện trạng thái đã đổi thành `APPROVED`.
5. Hệ thống từ chối cập nhật và cảnh báo: "Yêu cầu này đã được phê duyệt bởi người quản lý khác. Thao tác từ chối không còn hiệu lực".

## Acceptance Criteria

#### AC-001: Từ chối thành công yêu cầu và lưu đầy đủ lý do

- **Given**: Yêu cầu `REQ-005` đang ở trạng thái `PENDING_APPROVAL`.
- **When**: Logistics Manager nhập lý do "Tuyến đường biên giới Hà Khẩu đang tạm ngừng thông quan gia súc" và nhấn Xác nhận Từ chối.
- **Then**: Trạng thái yêu cầu chuyển thành `REJECTED`, trường `rejectionReason` lưu chính xác lý do đã nhập.
- **And**: Ghi nhận một dòng lịch sử trong `RequestStatusHistory` với hành động `REJECT_REQUEST` và người thực hiện là Manager.

#### AC-002: Bắt buộc cung cấp lý do từ chối

- **Given**: Manager mở hộp thoại từ chối yêu cầu.
- **When**: Manager để trống ô lý do và cố tình nhấn nút Xác nhận Từ chối.
- **Then**: Hệ thống không cho phép thực hiện và hiển thị cảnh báo lỗi màu đỏ.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-003: Quy định phân quyền truy cập theo vai trò (RBAC - Thẩm quyền từ chối của Manager)
- BR-019: Quy định về phản hồi minh bạch lý do từ chối đơn dịch vụ

### Dependencies

- Bảng `TransportRequest`, `RequestStatusHistory`
- Dịch vụ gửi Email thông báo khách hàng

## Non-Functional

- Thời gian xử lý từ chối và gửi mail: $\le 600$ ms.
- Nội dung lý do từ chối được lưu trữ bất biến phục vụ đối soát và báo cáo chất lượng dịch vụ.

## Out of Scope

- Cơ chế tự động hoàn lại tiền ứng trước của khách hàng (thuộc luồng kế toán xử lý riêng).
