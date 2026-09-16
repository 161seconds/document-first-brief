# UC-13: Phê duyệt yêu cầu vận chuyển (Approve Transport Request)

## Metadata

- **Story**: Là Quản lý Điều hành Logistics (Trần Anh Tuấn), tôi muốn kiểm tra tính khả thi và thực hiện phê duyệt một yêu cầu vận chuyển ở trạng thái Chờ duyệt để đơn hàng chính thức được chuyển sang các luồng tiếp theo (Lập hồ sơ pháp lý & Lập kế hoạch điều phối lộ trình).
- **Context**: UC-13 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Chỉ có vai trò Quản lý Điều hành Logistics (`LOGISTICS_MANAGER`) mới có quyền phê duyệt yêu cầu (BR-003). Khi phê duyệt, hệ thống kiểm tra trạng thái đơn hàng hiện tại phải là "Chờ duyệt" (`PENDING`), chuyển trạng thái đơn hàng sang "Đã duyệt" (`APPROVED`), ghi nhận mốc thời gian phê duyệt (`approved_at`), ID người phê duyệt (`approved_by = Manager ID`). Đồng thời, hệ thống gửi thông báo cho Khách hàng và tạo tác vụ chuẩn bị cho Chuyên viên thủ tục (Transport Specialist) và Điều phối viên (Fleet & Route Coordinator).
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
- Đơn hàng đã có đầy đủ thông tin điểm đi, điểm đến, ngày khởi hành và có ít nhất 1 con ngựa đua trong danh sách.

### Trigger

- Logistics Manager nhấp chọn nút "Approve Request" trên màn hình Chi tiết yêu cầu (UC-12) hoặc Danh sách yêu cầu (UC-11).

## Flow

### Main Flow

1. Logistics Manager xem thông tin chi tiết của yêu cầu vận chuyển `REQ-YYYYMMDD-XXXX` ở trạng thái `PENDING`.
2. Manager kiểm tra các điều kiện vận tải (tuyến đường Châu Âu, số lượng ngựa, các yêu cầu chăm sóc đặc biệt).
3. Manager nhấp chọn nút "Approve Request".
4. Hệ thống hiển thị hộp thoại xác nhận phê duyệt (Approval Confirmation Modal) bao gồm:
   - Mã yêu cầu & Tên khách hàng
   - Tổng số lượng ngựa & Tuyến đường
   - Ô nhập ghi chú phê duyệt (Approval Notes - tùy chọn)
   - Nút "Confirm Approval" và "Cancel"
5. Manager nhập ghi chú (nếu có) và nhấp chọn "Confirm Approval".
6. Hệ thống kiểm tra điều kiện phía server:
   - Vai trò người thao tác là `LOGISTICS_MANAGER`.
   - Trạng thái đơn hàng tại thời điểm lưu vẫn là `PENDING`.
   - Đơn hàng chứa ít nhất 1 con ngựa (`total_racehorses >= 1`).
7. Hệ thống cập nhật trạng thái yêu cầu sang `APPROVED` (Đã duyệt).
8. Hệ thống lưu mốc thời gian phê duyệt (`approved_at`), ghi nhận ID người phê duyệt (`approved_by`), thông tin ghi chú phê duyệt và tạo bản ghi lịch sử chuyển trạng thái (UC-15).
9. Hệ thống phát thông báo (Notification/Email) tới Khách hàng: "Your transport request REQ-YYYYMMDD-XXXX has been Approved."
10. Hệ thống tự động khởi tạo luồng chuẩn bị hồ sơ pháp lý (Flow 2 - Transport Specialist) và luồng điều phối đội xe (Flow 3 - Fleet & Route Coordinator).
11. Hệ thống đóng hộp thoại, cập nhật giao diện chi tiết đơn hàng sang trạng thái `APPROVED` với nhãn màu xanh lá.

### Alternative Flow

#### ALT-01: Phê duyệt nhanh từ danh sách (Quick Approve)

1. Tại màn hình Danh sách yêu cầu (UC-11), Manager nhấp chọn biểu tượng "Approve" tại dòng tương ứng với đơn hàng `PENDING`.
2. Hệ thống hiển thị hộp thoại xác nhận phê duyệt nhanh.
3. Manager nhấn "Confirm Approval" và hệ thống thực hiện từ bước 6 đến bước 11 của Luồng chính.

### Exception Flow

#### EXC-01: Yêu cầu chưa có ngựa đua nào trong danh sách

1. Tại bước 6 của Luồng chính, hệ thống kiểm tra và phát hiện đơn hàng có `total_racehorses == 0`.
2. Hệ thống từ chối phê duyệt và hiển thị thông báo lỗi: "Cannot approve a transport request with no racehorses listed. Please request customer to add racehorses first."
3. Trạng thái đơn hàng vẫn giữ nguyên `PENDING`.

#### EXC-02: Đơn hàng đã bị Khách hàng hủy trong lúc Manager đang xem

1. Manager mở hộp thoại phê duyệt khi đơn hàng ở trạng thái `PENDING`. Đồng thời, Khách hàng thực hiện hủy đơn hàng (`PENDING` $\rightarrow$ `CANCELLED`).
2. Manager nhấn "Confirm Approval".
3. Hệ thống kiểm tra phía server và phát hiện trạng thái đã chuyển thành `CANCELLED`.
4. Hệ thống chặn phê duyệt và hiển thị thông báo: "This request has been Canceled by the customer and cannot be approved."

## Acceptance Criteria

### AC-001: Phê duyệt thành công yêu cầu vận chuyển hợp lệ

- **Given**: Logistics Manager Trần Anh Tuấn ở màn hình phê duyệt đơn hàng `REQ-20260915-0001` ở trạng thái `PENDING` có 2 con ngựa đua.
- **When**: Ông Tuấn nhấn "Confirm Approval".
- **Then**: Hệ thống chuyển trạng thái đơn hàng thành `APPROVED`, lưu mốc thời gian `approved_at`, lưu ID người phê duyệt, gửi thông báo cho khách hàng và hiển thị nhãn màu xanh `APPROVED`.

### AC-002: Chặn người dùng không có vai trò Logistics Manager phê duyệt

- **Given**: Khách hàng David Michael truy cập API phê duyệt đơn hàng.
- **When**: Khách hàng gửi yêu cầu POST Approve.
- **Then**: Hệ thống trả về lỗi 403 Forbidden và từ chối cập nhật trạng thái.

### AC-003: Chặn phê duyệt đơn hàng không có ngựa

- **Given**: Đơn hàng `REQ-20260915-0009` ở trạng thái `PENDING` nhưng chưa có con ngựa nào trong danh sách.
- **When**: Manager nhấn "Confirm Approval".
- **Then**: Hệ thống báo lỗi "Cannot approve a transport request with no racehorses listed." và không đổi trạng thái.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)

### Dependencies

- Dịch vụ Thông báo Hệ thống (Notification & Email Service)

## Non-Functional

- Security: Phân quyền RBAC nghiêm ngặt (chỉ `LOGISTICS_MANAGER` được gọi API phê duyệt).
- Compatibility: Tương thích mượt mà trên giao diện Web responsive.

## Out of Scope

- Phân công cụ thể tài xế, xe chở hoặc lịch trình chi tiết (do Coordinator và Manager thực hiện ở Flow 3).
