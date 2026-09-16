# UC-14: Từ chối yêu cầu vận chuyển (Reject Transport Request)

## Metadata

- **Story**: Là Quản lý Điều hành Logistics (Trần Anh Tuấn), tôi muốn từ chối một yêu cầu vận chuyển không đủ điều kiện và cung cấp lý do từ chối chi tiết để phản hồi lại cho khách hàng và lưu trữ lịch sử xử lý đơn hàng.
- **Context**: UC-14 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Khi yêu cầu vận chuyển của khách hàng không thể đáp ứng (ví dụ: vượt quá năng lực vận chuyển, địa điểm nằm ngoài vùng phục vụ, lịch trình xung đột không thể sắp xếp xe chuyên dụng), Quản lý Điều hành Logistics (`LOGISTICS_MANAGER`) sẽ thực hiện từ chối yêu cầu. Bắt buộc phải nhập lý do từ chối (Rejection Reason). Hệ thống chuyển trạng thái đơn hàng sang `REJECTED`, ghi lại ID người thực hiện (`rejected_by`), mốc thời gian (`rejected_at`), lý do chi tiết và gửi thông báo phản hồi cho Khách hàng.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Người dùng đã đăng nhập thành công với vai trò `LOGISTICS_MANAGER`.
- Yêu cầu vận chuyển tồn tại trên hệ thống và đang ở trạng thái "Chờ duyệt" (`PENDING`).

### Trigger

- Logistics Manager nhấp chọn nút "Reject Request" trên giao diện Chi tiết yêu cầu (UC-12) hoặc Danh sách yêu cầu (UC-11).

## Flow

### Main Flow

1. Logistics Manager xem thông tin chi tiết một yêu cầu vận chuyển ở trạng thái `PENDING`.
2. Manager xác định yêu cầu không đủ điều kiện phục vụ và nhấp chọn nút "Reject Request".
3. Hệ thống hiển thị hộp thoại từ chối yêu cầu (Rejection Modal) bao gồm:
   - Thông tin tóm tắt mã yêu cầu (`REQ-YYYYMMDD-XXXX`) và tên Khách hàng
   - Ô nhập lý do từ chối: Rejection Reason (Trường bắt buộc - textarea)
   - Các lý do mẫu có sẵn (Pre-defined templates: "Route out of coverage area", "No specialized vehicle available on requested date", "Insufficient preparation time for border quarantine", "Other...")
   - Nút "Confirm Rejection" và nút "Cancel"
4. Manager chọn lý do mẫu hoặc nhập lý do chi tiết vào ô Rejection Reason.
5. Manager nhấp nút "Confirm Rejection".
6. Hệ thống kiểm tra dữ liệu phía server:
   - Vai trò người thực hiện là `LOGISTICS_MANAGER` (BR-003).
   - Ô lý do từ chối không được để trống (tối thiểu 10 ký tự).
   - Trạng thái đơn hàng tại thời điểm xử lý là `PENDING`.
7. Hệ thống cập nhật trạng thái yêu cầu sang `REJECTED` (Từ chối).
8. Hệ thống lưu mốc thời gian từ chối (`rejected_at`), ID người từ chối (`rejected_by = Manager ID`), nội dung lý do từ chối và ghi nhận vào nhật ký lịch sử trạng thái (UC-15).
9. Hệ thống gửi email và thông báo trên Web app cho Khách hàng: "Your transport request REQ-YYYYMMDD-XXXX has been Rejected. Reason: [Rejection Reason]".
10. Hệ thống đóng hộp thoại và cập nhật trạng thái đơn hàng trên giao diện thành `REJECTED` với nhãn màu đỏ.

### Alternative Flow

#### ALT-01: Sử dụng mẫu lý do từ chối nhanh

1. Tại bước 4 của Luồng chính, Manager nhấp chọn một lý do có sẵn (ví dụ: "No specialized vehicle available on requested date").
2. Hệ thống tự động điền nội dung mẫu vào ô Rejection Reason. Manager có thể bổ sung thêm chi tiết.
3. Manager nhấn "Confirm Rejection" để hoàn tất.

### Exception Flow

#### EXC-01: Để trống lý do từ chối

1. Tại bước 5 của Luồng chính, Manager để trống ô Rejection Reason hoặc nhập ít hơn 10 ký tự.
2. Hệ thống hiển thị cảnh báo ngay dưới ô nhập: "Please provide a valid rejection reason (minimum 10 characters)."
3. Nút "Confirm Rejection" bị vô hiệu hóa cho đến khi nhập đầy đủ.

#### EXC-02: Đơn hàng đã được phê duyệt hoặc hủy trước đó

1. Manager mở hộp thoại từ chối khi đơn hàng ở trạng thái `PENDING`. Tuy nhiên, đơn hàng đã thay đổi trạng thái trong khoảng thời gian này.
2. Manager nhấn "Confirm Rejection".
3. Hệ thống kiểm tra CSDL và phát hiện trạng thái không còn là `PENDING`.
4. Hệ thống từ chối cập nhật và hiển thị thông báo lỗi: "Request status has changed. Current status is no longer Pending."

## Acceptance Criteria

### AC-001: Từ chối yêu cầu vận chuyển thành công với lý do hợp lệ

- **Given**: Logistics Manager Trần Anh Tuấn ở hộp thoại từ chối yêu cầu `REQ-20260915-0004` trạng thái `PENDING`.
- **When**: Ông Tuấn nhập lý do "No specialized air-conditioned vehicle available on 2026-10-01" và nhấn "Confirm Rejection".
- **Then**: Hệ thống chuyển trạng thái đơn hàng sang `REJECTED`, lưu mốc thời gian và ID người từ chối, phát thông báo cho khách hàng kèm lý do từ chối.

### AC-002: Bắt buộc nhập lý do từ chối

- **Given**: Manager mở hộp thoại từ chối.
- **When**: Manager để trống ô Rejection Reason và nhấn "Confirm Rejection".
- **Then**: Hệ thống báo lỗi "Please provide a valid rejection reason (minimum 10 characters)" và không chuyển trạng thái đơn hàng.

### AC-003: Phân quyền RBAC cho vai trò Logistics Manager

- **Given**: Người dùng có vai trò `CUSTOMER` cố gắng gọi API từ chối đơn hàng.
- **When**: Gửi yêu cầu POST Reject.
- **Then**: Hệ thống từ chối và trả về lỗi HTTP 403 Forbidden.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- Dịch vụ Thông báo & Email (Notification Service)

## Non-Functional

- Security: Chặn truy cập trái phép bằng kiểm tra vai trò `LOGISTICS_MANAGER` phía Backend.
- Compatibility: Hiển thị mượt mà trên ứng dụng Web responsive.

## Out of Scope

- Tự động gợi ý thời gian/lộ trình thay thế cho khách hàng khi từ chối (khách hàng cần tạo yêu cầu mới với mốc thời gian khác).
