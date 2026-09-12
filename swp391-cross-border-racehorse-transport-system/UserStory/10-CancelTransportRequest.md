# STORY-010: Hủy yêu cầu vận chuyển

## Metadata

- **Story**: Là một Khách hàng (Customer), tôi muốn chủ động hủy bỏ một yêu cầu vận chuyển khi đơn chưa được xử lý và bắt buộc cung cấp lý do hủy để hệ thống giải phóng lịch đặt trước và lưu trữ lịch sử minh bạch.
- **Context**: Khách hàng có thể thay đổi kế hoạch thi đấu, ngựa gặp vấn đề chấn thương trước ngày vận chuyển hoặc không còn nhu cầu sử dụng dịch vụ. Để tránh việc đội điều hành tiếp tục thẩm định và phân bổ tài nguyên thừa thãi, khách hàng được quyền chủ động hủy đơn khi đơn đang ở trạng thái `Chờ duyệt` (`PENDING_APPROVAL`) hoặc `Bản nháp` (`DRAFT`). Lý do hủy bắt buộc phải được ghi nhận để phục vụ thống kê phân tích lý do mất đơn (Churn Analysis).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập và là chủ sở hữu của yêu cầu vận chuyển.
- Trạng thái của yêu cầu hiện tại là `PENDING_APPROVAL` hoặc `DRAFT`.
- Yêu cầu chưa chuyển sang trạng thái `APPROVED`, `IN_TRANSIT` hoặc đã hoàn tất.

### Trigger

- Khách hàng nhấp chọn nút "Hủy yêu cầu" (`Cancel Request`) trên trang Chi tiết yêu cầu vận chuyển hoặc danh sách đơn hàng.

## Flow

### Main Flow

1. Khách hàng nhấn nút "Hủy yêu cầu" tại đơn hàng mong muốn.
2. Hệ thống kiểm tra điều kiện trạng thái yêu cầu có cho phép hủy hay không.
3. Hệ thống hiển thị hộp thoại xác nhận hủy yêu cầu (`Cancel Confirmation Modal`):
   - Cảnh báo: "Thao tác hủy yêu cầu không thể hoàn tác sau khi xác nhận".
   - Danh sách lý do mẫu chọn nhanh: "Thay đổi kế hoạch thi đấu", "Ngựa gặp vấn đề sức khỏe", "Tìm được phương án vận chuyển khác", "Chi phí không phù hợp", "Khác".
   - Trường văn bản nhập chi tiết lý do hủy (`cancellationReason`): Bắt buộc nhập tối thiểu 10 ký tự.
4. Khách hàng chọn lý do mẫu hoặc nhập lý do chi tiết vào ô văn bản.
5. Khách hàng nhấn nút xác nhận "Xác nhận Hủy yêu cầu".
6. Hệ thống kiểm tra lý do hủy không được để trống và đạt độ dài quy định.
7. Hệ thống cập nhật bản ghi trong bảng `TransportRequest`:
   - `currentStatus = 'CANCELLED'`.
   - `cancellationReason = [Nội dung lý do khách nhập]`.
   - `updatedAt = [Thời gian hiện tại]`.
8. Hệ thống tạo một bản ghi mới trong bảng `RequestStatusHistory`:
   - `requestId`: ID yêu cầu.
   - `fromStatus`: `PENDING_APPROVAL`.
   - `toStatus`: `CANCELLED`.
   - `action`: `CANCEL_REQUEST`.
   - `reason`: [Nội dung lý do khách nhập].
   - `performedBy`: ID của khách hàng.
   - `performedAt`: Thời gian hiện tại.
9. Hệ thống gửi thông báo In-app và Email cho Ban quản lý Logistics: "Khách hàng đã hủy yêu cầu [Mã yêu cầu] với lý do: [Lý do]".
10. Hệ thống hiển thị thông báo thành công: "Yêu cầu vận chuyển đã được hủy bỏ thành công" và cập nhật huy hiệu trạng thái sang màu xám (`Đã hủy`).

### Alternative Flow

#### ALT-01: Khách hàng đổi ý và đóng hộp thoại xác nhận

1. Tại bước 3 của Luồng chính, khách hàng đổi ý không muốn hủy đơn nữa và nhấn nút "Đóng" hoặc "Giữ lại yêu cầu".
2. Hệ thống đóng hộp thoại và giữ nguyên trạng thái `PENDING_APPROVAL` của đơn hàng.

### Exception Flow

#### EXC-01: Khách hàng không nhập hoặc nhập lý do quá ngắn

1. Tại bước 5 của Luồng chính, ô lý do hủy bị bỏ trống hoặc dưới 10 ký tự.
2. Hệ thống hiển thị cảnh báo đỏ dưới ô nhập liệu: "Vui lòng cung cấp lý do hủy chi tiết (tối thiểu 10 ký tự) để chúng tôi cải thiện chất lượng dịch vụ".
3. Vô hiệu hóa nút Xác nhận hủy cho tới khi nhập đủ.

#### EXC-02: Đơn hàng đã được duyệt trong thời gian chờ xác nhận

1. Khách hàng mở hộp thoại hủy lúc 10:00 (khi đơn đang Chờ duyệt).
2. Lúc 10:01, Manager đã duyệt đơn thành công (`status = 'APPROVED'`).
3. Lúc 10:02, khách hàng nhấn Xác nhận hủy.
4. Hệ thống phát hiện trạng thái đã đổi sang `APPROVED`.
5. Hệ thống từ chối hủy tự động và thông báo: "Yêu cầu này vừa được Ban quản lý phê duyệt. Quý khách không thể tự hủy trực tiếp. Vui lòng liên hệ Hotline Logistics để được hỗ trợ hủy đơn theo quy trình".

## Acceptance Criteria

#### AC-001: Hủy thành công yêu cầu ở trạng thái Chờ duyệt

- **Given**: Yêu cầu `REQ-003` đang ở trạng thái `PENDING_APPROVAL`.
- **When**: Khách hàng nhập lý do hủy "Giải đua bị hoãn do thời tiết" và nhấn xác nhận hủy.
- **Then**: Trạng thái yêu cầu chuyển thành `CANCELLED`, trường `cancellationReason` được lưu đầy đủ.
- **And**: Ghi nhận một dòng lịch sử trong `RequestStatusHistory` với hành động `CANCEL_REQUEST`.

#### AC-002: Bắt buộc nhập lý do hủy

- **Given**: Khách hàng mở hộp thoại hủy yêu cầu.
- **When**: Khách hàng để trống trường lý do và cố tình gửi yêu cầu hủy.
- **Then**: Hệ thống chặn thao tác và hiển thị thông báo lỗi yêu cầu nhập lý do.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-016: Quy định về điều kiện hủy yêu cầu vận chuyển và lý do hủy bắt buộc

### Dependencies

- Bảng `TransportRequest`, `RequestStatusHistory`
- Dịch vụ thông báo nội bộ (Notification Service)

## Non-Functional

- Thao tác chuyển đổi trạng thái hủy và ghi audit log hoàn tất trong thời gian $\le 400$ ms.
- Trạng thái `CANCELLED` là trạng thái kết thúc (Terminal State), không thể chuyển ngược lại trạng thái đang hoạt động.

## Out of Scope

- Chính sách hoàn tiền đặt cọc hoặc bồi thường phí hủy đơn (thuộc phân hệ tài chính nâng cao).
