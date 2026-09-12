# STORY-009: Cập nhật thông tin yêu cầu vận chuyển

## Metadata

- **Story**: Là một Khách hàng (Customer), tôi muốn chỉnh sửa các thông tin của yêu cầu vận chuyển (địa điểm, thời gian, danh sách cá thể ngựa, yêu cầu đặc biệt) khi đơn vẫn đang ở trạng thái "Chờ duyệt" để cập nhật những thay đổi phát sinh trước khi ban quản lý điều hành tiến hành phê duyệt chính thức.
- **Context**: Trước khi yêu cầu được Logistics Manager phê duyệt và bắt đầu điều động tài nguyên (xe tải, chuyên viên thủ tục), khách hàng có thể có nhu cầu thay đổi ngày giờ khởi hành, bổ sung/bớt ngựa hoặc thay đổi địa điểm đón. Để đảm bảo tính toàn vẹn quy trình, việc chỉnh sửa chỉ được phép diễn ra khi đơn còn ở trạng thái `Chờ duyệt` (`PENDING_APPROVAL`) hoặc `Bản nháp` (`DRAFT`). Khi đã duyệt, mọi chỉnh sửa đều bị khóa để tránh xung đột lịch trình.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập và là chủ sở hữu hợp pháp của yêu cầu vận chuyển (`customerId == currentUserId`).
- Trạng thái hiện tại của yêu cầu là `PENDING_APPROVAL` hoặc `DRAFT`.
- Yêu cầu chưa bị khóa hoặc chuyển sang trạng thái `APPROVED`, `IN_TRANSIT`, `CANCELLED`, `REJECTED`.

### Trigger

- Khách hàng nhấn vào nút "Chỉnh sửa" (`Edit Request`) trên trang chi tiết đơn hàng hoặc danh sách đơn hàng.

## Flow

### Main Flow

1. Khách hàng nhấn nút "Chỉnh sửa" tại yêu cầu vận chuyển cần thay đổi.
2. Hệ thống kiểm tra trạng thái yêu cầu trong cơ sở dữ liệu:
   - Trạng thái là `PENDING_APPROVAL` hoặc `DRAFT`: Cho phép tiếp tục.
3. Hệ thống hiển thị biểu mẫu chỉnh sửa toàn diện chứa đầy đủ dữ liệu hiện tại:
   - Nhóm 1: Địa điểm đi, địa điểm đến, thời gian mong muốn khởi hành, phương thức mong muốn.
   - Nhóm 2: Danh sách cá thể ngựa hiện có (cho phép sửa chi tiết từng con, thêm con mới hoặc xóa bớt).
   - Nhóm 3: Ghi chú và yêu cầu đặc biệt.
4. Khách hàng thực hiện các thay đổi cần thiết.
5. Khách hàng nhấn nút "Lưu thay đổi" (`Save Updates`).
6. Hệ thống thực hiện kiểm tra tính hợp lệ của toàn bộ dữ liệu mới (địa điểm hợp lệ, thời gian khởi hành hợp lệ, danh sách ngựa tối thiểu 1 con).
7. Hệ thống cập nhật các trường thông tin trong bảng `TransportRequest`, tính toán lại `totalHorses` nếu có biến động.
8. Hệ thống cập nhật trường `updatedAt` thành thời gian hiện tại.
9. Hệ thống tự động ghi một bản ghi vào bảng `RequestStatusHistory`:
   - `requestId`: ID yêu cầu.
   - `fromStatus`: Trạng thái cũ.
   - `toStatus`: `PENDING_APPROVAL`.
   - `action`: `UPDATE_REQUEST`.
   - `performedBy`: ID của khách hàng.
   - `performedAt`: Thời gian hiện tại.
10. Hệ thống hiển thị thông báo thành công: "Yêu cầu vận chuyển đã được cập nhật thành công!" và điều hướng về trang Chi tiết yêu cầu với phiên bản dữ liệu mới nhất.

### Alternative Flow

#### ALT-01: Khách hàng hủy bỏ các chỉnh sửa chưa lưu

1. Trong quá trình chỉnh sửa biểu mẫu, khách hàng nhấn nút "Hủy bỏ" (`Cancel`).
2. Hệ thống hiển thị cảnh báo: "Các thay đổi chưa lưu sẽ bị mất. Bạn có muốn tiếp tục?".
3. Khách hàng xác nhận "Hủy".
4. Hệ thống giữ nguyên dữ liệu ban đầu và quay trở lại màn hình chi tiết yêu cầu.

### Exception Flow

#### EXC-01: Đơn hàng đã được duyệt trong lúc khách hàng đang mở màn hình sửa (Race condition)

1. Khách hàng mở biểu mẫu lúc 09:00 (lúc này đơn đang `PENDING_APPROVAL`).
2. Lúc 09:05, Logistics Manager tiến hành duyệt đơn (`status` chuyển thành `APPROVED`).
3. Lúc 09:06, khách hàng nhấn "Lưu thay đổi".
4. Hệ thống phát hiện trạng thái CSDL đã là `APPROVED` (không còn là `PENDING_APPROVAL`).
5. Hệ thống chặn việc ghi đè dữ liệu, hiển thị thông báo lỗi: "Yêu cầu vận chuyển này đã được Ban quản lý phê duyệt nên không thể tự ý chỉnh sửa trực tiếp. Vui lòng liên hệ Hotline điều hành nếu cần thay đổi khẩn cấp".
6. Hệ thống tải lại trang chi tiết đơn hàng ở chế độ chỉ đọc.

#### EXC-02: Dữ liệu chỉnh sửa không hợp lệ

1. Khách hàng xóa toàn bộ ngựa trong danh sách rồi nhấn Lưu.
2. Hệ thống cảnh báo: "Yêu cầu phải có ít nhất 1 cá thể ngựa" và giữ nguyên giao diện để khách hàng điều chỉnh lại.

## Acceptance Criteria

#### AC-001: Chỉnh sửa thành công khi đơn ở trạng thái Chờ duyệt

- **Given**: Yêu cầu `REQ-001` đang có trạng thái `PENDING_APPROVAL`, thời gian khởi hành là 20/09/2026.
- **When**: Khách hàng chỉnh sửa thời gian khởi hành thành 22/09/2026 và nhấn "Lưu thay đổi".
- **Then**: Hệ thống cập nhật thời gian mới, cập nhật `updatedAt` và ghi log `UPDATE_REQUEST` trong `RequestStatusHistory`.

#### AC-002: Chặn chỉnh sửa khi đơn đã được duyệt

- **Given**: Yêu cầu `REQ-002` có trạng thái `APPROVED`.
- **When**: Người dùng cố tình gọi API `PUT /api/v1/requests/REQ-002` để sửa thông tin.
- **Then**: Hệ thống trả về mã HTTP 400 Bad Request kèm thông báo "Không thể chỉnh sửa yêu cầu đã được phê duyệt".

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-014: Ràng buộc trạng thái cho phép chỉnh sửa yêu cầu vận chuyển
- BR-015: Cơ chế kiểm soát xung đột dữ liệu đồng thời (Optimistic Concurrency Control)

### Dependencies

- Bảng `TransportRequest`, `RequestHorseItem`, `RequestStatusHistory`

## Non-Functional

- Áp dụng kỹ thuật kiểm tra phiên bản dữ liệu (`rowVersion` / `updatedAt`) để phát hiện và ngăn chặn xung đột cập nhật đồng thời.
- Thời gian lưu và cập nhật: $\le 600$ ms.

## Out of Scope

- Quy trình yêu cầu sửa đổi sau khi đã duyệt (`Request Re-approval Flow`) — tính năng nâng cao yêu cầu nộp diff cho Manager duyệt lại.
