# UC-09: Cập nhật thông tin yêu cầu (Update Transport Request Information)

## Metadata

- **Story**: Là Khách hàng (David Michael - Chủ sở hữu hoặc đại diện CLB Đua ngựa), tôi muốn thay đổi các thông tin tổng quan của yêu cầu vận chuyển (Điểm đi, Điểm đến, Thời gian khởi hành mong muốn, Loại hình vận chuyển, Ghi chú) khi yêu cầu đang ở trạng thái Chờ duyệt để đảm bảo thông tin luôn chính xác trước khi Logistics Manager phê duyệt.
- **Context**: UC-09 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Khách hàng có thể cần điều chỉnh địa điểm hoặc thời gian di chuyển do thay đổi lịch thi đấu hoặc lịch kiểm dịch. Điều kiện tiên quyết là yêu cầu vận chuyển phải ở trạng thái "Chờ duyệt" (`PENDING`). Khi cập nhật, hệ thống ghi nhận mốc thời gian cập nhật (`updated_at`) và thông tin phiên bản dữ liệu mới nhất. Hệ thống chạy trên nền tảng Web responsive (BR-004) với giao diện Tiếng Anh (BR-005).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã đăng nhập thành công với vai trò `CUSTOMER`.
- Yêu cầu vận chuyển tồn tại trên hệ thống và đang ở trạng thái `PENDING`.
- Khách hàng là người sở hữu (người tạo) của yêu cầu vận chuyển này.

### Trigger

- Khách hàng nhấp chọn nút "Edit Request" trên màn hình Danh sách yêu cầu hoặc Chi tiết yêu cầu.

## Flow

### Main Flow

1. Khách hàng xem thông tin chi tiết một yêu cầu vận chuyển ở trạng thái `PENDING`.
2. Khách hàng nhấp chọn nút "Edit Request".
3. Hệ thống hiển thị biểu mẫu chỉnh sửa thông tin yêu cầu vận chuyển chứa dữ liệu hiện tại:
   - Pickup Address (Địa chỉ/Trang trại điểm đi)
   - Destination Address (Địa chỉ/Trường đua điểm đến)
   - Expected Departure Date & Time (Thời gian khởi hành mong muốn)
   - Transport Type (Loại hình vận chuyển)
   - General Notes (Ghi chú chung)
4. Khách hàng thực hiện chỉnh sửa các thông tin cần thay đổi (ví dụ: đổi địa chỉ điểm đến hoặc lùi ngày khởi hành).
5. Khách hàng nhấp chọn "Save Changes".
6. Hệ thống thực hiện kiểm tra tính hợp lệ dữ liệu đầu vào:
   - Trạng thái yêu cầu vẫn đang là `PENDING` tại thời điểm lưu.
   - Địa chỉ điểm đi và điểm đến không được để trống và phải thuộc Châu Âu (BR-005).
   - Thời gian khởi hành mới phải sau thời điểm hiện tại tối thiểu 48 giờ.
7. Hệ thống cập nhật các trường thông tin mới vào cơ sở dữ liệu, ghi nhận thời gian chỉnh sửa (`updated_at = Current Timestamp`).
8. Hệ thống hiển thị thông báo thành công: "Transport request REQ-YYYYMMDD-XXXX has been updated successfully."
9. Hệ thống điều hướng khách hàng về lại màn hình Chi tiết yêu cầu với thông tin mới vừa cập nhật.

### Alternative Flow

#### ALT-01: Hủy bỏ thao tác chỉnh sửa (Cancel Editing)

1. Tại bước 4 của Luồng chính, khách hàng thay đổi ý định và nhấp nút "Cancel".
2. Hệ thống bỏ qua các thay đổi chưa lưu và giữ nguyên dữ liệu ban đầu.
3. Hệ thống đưa khách hàng quay lại màn hình Chi tiết yêu cầu.

### Exception Flow

#### EXC-01: Yêu cầu đã chuyển sang trạng thái Đã duyệt (APPROVED) trong lúc khách hàng đang chỉnh sửa

1. Khách hàng mở biểu mẫu chỉnh sửa khi trạng thái đang là `PENDING`. Trong lúc khách hàng nhập liệu, Logistics Manager thực hiện phê duyệt yêu cầu trên hệ thống (`PENDING` $\rightarrow$ `APPROVED`).
2. Khách hàng nhấp nút "Save Changes".
3. Hệ thống kiểm tra phía server và phát hiện trạng thái yêu cầu đã chuyển thành `APPROVED`.
4. Hệ thống chặn thao tác lưu, giữ nguyên dữ liệu đã phê duyệt và hiển thị thông báo lỗi: "This transport request has just been Approved by Logistics Manager and can no longer be updated."
5. Hệ thống làm mới trang và hiển thị giao diện Chi tiết yêu cầu ở chế độ chỉ đọc.

#### EXC-02: Thời gian khởi hành mới không đáp ứng quy định tối thiểu 48 giờ

1. Tại bước 4 của Luồng chính, khách hàng thay đổi thời gian khởi hành mới thành 12 giờ tới.
2. Tại bước 6, hệ thống kiểm tra và phát hiện khoảng thời gian không đủ 48 giờ.
3. Hệ thống hiển thị thông báo lỗi: "New departure date must be at least 48 hours in advance."
4. Khách hàng chọn lại thời gian phù hợp và tiếp tục.

## Acceptance Criteria

### AC-001: Cập nhật thông tin yêu cầu thành công khi trạng thái PENDING

- **Given**: Khách hàng đang ở biểu mẫu chỉnh sửa yêu cầu `REQ-20260915-0002` ở trạng thái `PENDING`.
- **When**: Khách hàng đổi Destination Address thành "Chantilly Racecourse, France", chọn thời gian khởi hành mới sau 7 ngày và nhấn "Save Changes".
- **Then**: Hệ thống lưu dữ liệu mới vào CSDL, cập nhật mốc thời gian `updated_at`, hiển thị thông báo "Transport request has been updated successfully" và hiển thị thông tin mới.

### AC-002: Chặn cập nhật khi yêu cầu đã được phê duyệt hoặc từ chối

- **Given**: Yêu cầu `REQ-20260915-0002` đã được đổi trạng thái sang `APPROVED` hoặc `REJECTED`.
- **When**: Khách hàng cố gắng gửi yêu cầu lưu thay đổi.
- **Then**: Hệ thống từ chối lưu, hiển thị lỗi "This transport request has just been Approved by Logistics Manager and can no longer be updated." và cập nhật lại giao diện chỉ đọc.

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-017: Quy định Thời gian Khởi hành Mong muốn Tối thiểu (>= 48h)
- BR-018: Quy định Địa điểm Khởi hành và Điểm đến
- BR-020: Quy định Chỉnh sửa Thông tin Đơn hàng & Danh sách Ngựa

### Dependencies

- Cơ sở dữ liệu Yêu cầu Vận chuyển (Transport Request Entity)

## Non-Functional

- Security: Kiểm tra nghiêm ngặt quyền truy cập phía server (chỉ chính khách hàng tạo đơn mới được phép cập nhật).
- Compatibility: Hoạt động mượt mà trên Responsive Web App.

## Out of Scope

- Thay đổi mã yêu cầu (`Request Code`) hoặc thông tin người tạo (`created_by`).
- Cập nhật thông tin của các đơn hàng đã được khởi hành hoặc hoàn tất.
