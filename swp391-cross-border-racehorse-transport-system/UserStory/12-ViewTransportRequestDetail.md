# STORY-012: Xem chi tiết yêu cầu vận chuyển

## Metadata

- **Story**: Là một Người dùng (Khách hàng hoặc Quản lý điều hành Logistics), tôi muốn xem toàn bộ thông tin chi tiết của một yêu cầu vận chuyển cụ thể (lộ trình, danh sách cá thể ngựa, yêu cầu chăm sóc, trạng thái phê duyệt và thông tin người duyệt) để nắm bắt đầy đủ bối cảnh phục vụ việc theo dõi hoặc thẩm định đơn hàng.
- **Context**: Trang chi tiết đơn hàng là nơi tổng hợp đầy đủ bức tranh nghiệp vụ của một chuyến vận chuyển ngựa: thông tin người gửi, điểm đi/đến, thời gian dự kiến, cấu hình chuồng, chế độ ăn của từng con ngựa, thông tin người phê duyệt và lịch sử thay đổi. Khách hàng sử dụng màn hình này để theo dõi tiến độ đơn, trong khi Logistics Manager dùng màn hình này làm căn cứ chính để ra quyết định phê duyệt hoặc từ chối.
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
- Yêu cầu vận chuyển tồn tại trong hệ thống.
- Người dùng có quyền truy cập: là chủ sở hữu của đơn (nếu là Customer) hoặc có vai trò `LOGISTICS_MANAGER`.

### Trigger

- Người dùng nhấp vào mã yêu cầu vận chuyển hoặc nút "Xem chi tiết" (`View Details`) từ màn hình danh sách đơn hàng.

## Flow

### Main Flow

1. Người dùng chọn một yêu cầu vận chuyển cụ thể.
2. Hệ thống kiểm tra quyền truy cập của người dùng đối với yêu cầu:
   - Nếu là Customer: Kiểm tra `customerId == currentUserId`. Nếu không khớp, chặn truy cập.
   - Nếu là Manager: Cho phép truy cập.
3. Hệ thống tải toàn bộ dữ liệu liên quan từ các bảng CSDL:
   - Thông tin chung (`TransportRequest`): Mã yêu cầu, ngày tạo, trạng thái hiện tại, thời gian khởi hành mong muốn, địa điểm đi và đến, ghi chú của khách hàng.
   - Thông tin khách hàng (`User`, `CustomerProfile`): Họ tên khách hàng, Số điện thoại, Email, Tên CLB/Trại ngựa, Địa chỉ liên hệ.
   - Danh sách cá thể ngựa (`RequestHorseItem`): Tên ngựa, giống, tuổi, giới tính, số microchip, loại chuồng mong muốn, chế độ ăn, ghi chú đặc biệt.
   - Thông tin phê duyệt (nếu đã xử lý): Người phê duyệt (`approvedBy`), thời điểm phê duyệt (`approvedAt`), hoặc lý do từ chối (`rejectionReason`).
   - Tóm tắt lịch sử trạng thái (`RequestStatusHistory`): Các mốc chuyển trạng thái gần nhất.
4. Hệ thống hiển thị giao diện chi tiết phân bố khoa học theo các khối nội dung:
   - Khối đầu trang (`Header Summary`): Mã yêu cầu, Huy hiệu trạng thái lớn, Nút hành động theo ngữ cảnh (Sửa/Hủy đối với Customer khi đơn chờ duyệt; Duyệt/Từ chối đối với Manager khi đơn chờ duyệt).
   - Khối 1: Thông tin khách hàng & Liên hệ khẩn cấp.
   - Khối 2: Lộ trình & Thời gian dự kiến.
   - Khối 3: Bảng danh sách chi tiết các cá thể ngựa (Hiển thị `totalHorses` và chi tiết từng con).
   - Khối 4: Yêu cầu chăm sóc, dinh dưỡng & Người đi kèm.
   - Khối 5: Lịch sử phê duyệt & Trạng thái xử lý.
5. Người dùng xem xét các thông tin và có thể nhấp chọn các hành động khả dụng tương ứng với vai trò của mình.

### Alternative Flow

#### ALT-01: Xem đơn hàng ở trạng thái "Từ chối" hoặc "Đã hủy"

1. Người dùng mở xem chi tiết một đơn hàng có trạng thái `REJECTED` hoặc `CANCELLED`.
2. Hệ thống hiển thị thêm một banner cảnh báo nổi bật ở đầu trang:
   - Với đơn bị từ chối: Hiển thị lý do từ chối do Logistics Manager cung cấp và thời điểm từ chối.
   - Với đơn đã hủy: Hiển thị lý do hủy do khách hàng nhập và thời điểm hủy.
3. Toàn bộ các nút chức năng Chỉnh sửa, Phê duyệt đều bị ẩn hoặc vô hiệu hóa.

### Exception Flow

#### EXC-01: Yêu cầu không tồn tại (Mã yêu cầu sai hoặc đã bị xóa)

1. Người dùng truy cập đường dẫn URL chứa mã yêu cầu không có trong CSDL (ví dụ: `/requests/REQ-999999`).
2. Hệ thống trả về lỗi HTTP 404 Not Found.
3. Hệ thống hiển thị trang thông báo thân thiện: "Không tìm thấy yêu cầu vận chuyển này. Yêu cầu có thể không tồn tại hoặc đã bị xóa" kèm nút "Quay lại danh sách".

#### EXC-02: Truy cập trái phép đơn hàng của khách hàng khác (403 Forbidden)

1. Khách hàng A đang đăng nhập nhưng cố tình nhập URL chi tiết đơn hàng của Khách hàng B.
2. Hệ thống phát hiện `customerId != currentUserId`.
3. Hệ thống chặn hiển thị dữ liệu và trả về lỗi HTTP 403 Forbidden kèm thông báo: "Bạn không có quyền truy cập vào thông tin yêu cầu vận chuyển này".

## Acceptance Criteria

#### AC-001: Hiển thị đầy đủ thông tin chi tiết cho khách hàng hợp lệ

- **Given**: Khách hàng A là chủ sở hữu của đơn `REQ-20260912-0001` gồm 2 con ngựa.
- **When**: Khách hàng A mở xem chi tiết đơn hàng.
- **Then**: Hệ thống hiển thị đầy đủ thông tin điểm đi, điểm đến, ngày giờ, bảng chi tiết 2 con ngựa kèm yêu cầu ăn uống và chuồng đơn.

#### AC-002: Chặn khách hàng khác xem trộm đơn hàng

- **Given**: Khách hàng B đăng nhập vào hệ thống.
- **When**: Khách hàng B truy cập đường dẫn chi tiết đơn hàng của Khách hàng A.
- **Then**: Hệ thống chặn truy cập, trả về lỗi 403 Forbidden và không làm lộ bất kỳ dữ liệu nào của Khách hàng A.

#### AC-003: Manager xem được thông tin phê duyệt của đơn đã xử lý

- **Given**: Đơn `REQ-002` đã được Logistics Manager Nguyễn Văn A phê duyệt vào lúc 14:00 ngày 11/09/2026.
- **When**: Người dùng mở xem chi tiết đơn hàng.
- **Then**: Giao diện hiển thị rõ thông tin "Người phê duyệt: Nguyễn Văn A" và "Thời điểm duyệt: 11/09/2026 14:00".

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-003: Quy định phân quyền truy cập theo vai trò (RBAC & Data Ownership)

### Dependencies

- Bảng `TransportRequest`, `RequestHorseItem`, `User`, `CustomerProfile`, `RequestStatusHistory`

## Non-Functional

- Thời gian tải trang chi tiết đầy đủ dữ liệu: $\le 350$ ms.
- Bảo đảm nguyên tắc bảo mật thông tin cá nhân (PII), chỉ những người có thẩm quyền mới thấy số điện thoại và địa chỉ liên lạc.

## Out of Scope

- Chức năng in phiếu đơn hàng ra bản cứng PDF (sẽ được tích hợp trong tính năng xuất chứng từ ở Flow 6).
