# UC-08: Quản lý danh sách ngựa và yêu cầu đặc biệt của riêng khách hàng (Manage Racehorses and Special Requirements for Request)

## Metadata

- **Story**: Là Khách hàng (David Michael - Chủ sở hữu hoặc đại diện CLB Đua ngựa), tôi muốn thêm, chỉnh sửa hoặc xóa thông tin các con ngựa đua tham gia chuyến vận chuyển và thiết lập các yêu cầu chăm sóc/vận tải đặc biệt cho từng con ngựa hoặc toàn chuyến để đơn vị logistics chuẩn bị phương tiện và nhân sự phù hợp.
- **Context**: UC-08 thuộc Flow 1 (Quản lý Tài khoản và Yêu cầu Vận chuyển). Sau khi khởi tạo yêu cầu vận chuyển (UC-07), Khách hàng cần khai báo danh sách chi tiết các con ngựa đua (Tên ngựa, Mã số Hộ chiếu ngựa - Passport ID, Giống ngựa, Tuổi, Chiều cao, Cân nặng, Giá trị bảo hiểm) và các yêu cầu đặc biệt (ví dụ: khoang xe chuyên dụng riêng, chế độ ăn uống đặc biệt, nhiệt độ điều hòa, hoặc yêu cầu Escort đi kèm). Hệ thống tự động tính toán và cập nhật tổng số lượng ngựa (`total_racehorses`) trong yêu cầu.
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
- Yêu cầu vận chuyển đã được khởi tạo và đang ở trạng thái "Chờ duyệt" (`PENDING`).

### Trigger

- Khách hàng chọn một yêu cầu vận chuyển ở trạng thái `PENDING` và nhấp nút "Manage Racehorses" hoặc "+ Add Racehorse".

## Flow

### Main Flow

1. Khách hàng truy cập trang chi tiết hoặc chỉnh sửa của yêu cầu vận chuyển `REQ-YYYYMMDD-XXXX`.
2. Hệ thống hiển thị danh sách các con ngựa đua hiện có trong yêu cầu (nếu đã tạo) cùng tổng số lượng ngựa `total_racehorses`.
3. Khách hàng nhấp nút "+ Add Racehorse" để thêm con ngựa mới vào danh sách.
4. Hệ thống hiển thị hộp thoại / biểu mẫu nhập thông tin ngựa đua:
   - Horse Name (Tên chiến mã)
   - Passport ID (Mã số Hộ chiếu ngựa)
   - Breed & Gender (Giống ngựa & Giới tính)
   - Age & Weight (Tuổi & Cân nặng ước tính kg)
   - Estimated Value (Giá trị bảo hiểm ước tính EUR - €)
   - Special Requirements (Yêu cầu đặc biệt cá thể: Chế độ ăn, thuốc, cách ly khoang, nhạy cảm tiếng ồn...)
5. Khách hàng nhập đầy đủ các thông tin bắt buộc của con ngựa và nhấp "Save Racehorse".
6. Hệ thống kiểm tra dữ liệu đầu vào (tên ngựa không để trống, Passport ID không trùng lặp trong cùng đơn hàng, giá trị bảo hiểm $\ge 0$).
7. Hệ thống bổ sung con ngựa vào danh sách yêu cầu và tự động tính toán lại tổng số lượng ngựa (`total_racehorses = total_racehorses + 1`).
8. Khách hàng thiết lập Yêu cầu đặc biệt cấp Chuyến vận chuyển (Trip-level Special Requirements):
   - Special Stall Type (Loại khoang/thùng xe chuyên dụng padded/air-conditioned)
   - Dedicated Escort Required (Yêu cầu nhân viên Escort chăm sóc riêng: Có/Không)
   - Climate Control Preference (Yêu cầu duy trì dải nhiệt độ cabin)
9. Khách hàng nhấn "Save & Update Request".
10. Hệ thống lưu toàn bộ danh sách ngựa và các yêu cầu đặc biệt vào CSDL, cập nhật thông tin yêu cầu và hiển thị thông báo thành công: "Racehorse list and special requirements updated successfully."

### Alternative Flow

#### ALT-01: Chỉnh sửa thông tin con ngựa trong danh sách

1. Tại bước 2 của Luồng chính, khách hàng nhấp biểu tượng "Edit" bên cạnh tên con ngựa trong danh sách.
2. Hệ thống hiển thị biểu mẫu với dữ liệu hiện tại của con ngựa đó.
3. Khách hàng điều chỉnh các thông tin cần thiết và nhấn "Update Racehorse".
4. Hệ thống kiểm tra dữ liệu, lưu thông tin mới và hiển thị thông báo "Racehorse details updated."

#### ALT-02: Xóa con ngựa khỏi danh sách yêu cầu

1. Tại bước 2 của Luồng chính, khách hàng nhấp biểu tượng "Delete" bên cạnh tên con ngựa cần xóa.
2. Hệ thống hiển thị hộp thoại xác nhận: "Are you sure you want to remove [Horse Name] from this request?"
3. Khách hàng nhấn "Confirm Delete".
4. Hệ thống xóa con ngựa khỏi bản ghi yêu cầu và cập nhật lại tổng số lượng ngựa (`total_racehorses = total_racehorses - 1`).

### Exception Flow

#### EXC-01: Trùng mã Passport ID trong cùng một yêu cầu

1. Tại bước 6 của Luồng chính, khách hàng nhập mã Passport ID trùng với một con ngựa đã có trong danh sách của đơn hàng này.
2. Hệ thống hiển thị thông báo lỗi: "Passport ID '[ID]' is already added in this transport request."
3. Khách hàng kiểm tra và nhập lại mã định danh chính xác.

#### EXC-02: Thao tác khi yêu cầu không còn ở trạng thái Chờ duyệt (PENDING)

1. Khách hàng cố gắng thêm, sửa hoặc xóa ngựa khi yêu cầu đã được Logistics Manager phê duyệt (`APPROVED`) hoặc từ chối (`REJECTED`).
2. Hệ thống từ chối thao tác, vô hiệu hóa các nút chỉnh sửa và hiển thị thông báo: "Cannot modify racehorse list for requests that are already Approved or Rejected."

## Acceptance Criteria

### AC-001: Thêm mới ngựa đua và tự động tính tổng số lượng

- **Given**: Khách hàng đang chỉnh sửa yêu cầu `REQ-20260915-0001` ở trạng thái `PENDING` có 1 con ngựa.
- **When**: Khách hàng thêm thành công con ngựa thứ 2 với thông tin hợp lệ (Name: "Thunderbolt", Passport ID: "FRA-2022-9876").
- **Then**: Hệ thống lưu thông tin con ngựa mới, tự động cập nhật `total_racehorses = 2` và hiển thị thông báo thành công.

### AC-002: Lưu yêu cầu đặc biệt cho chiến mã và toàn chuyến

- **Given**: Khách hàng nhập yêu cầu đặc biệt cho ngựa "Thunderbolt" là "Requires gluten-free hay" và yêu cầu chuyến "Padded stall with temperature 18-20°C".
- **When**: Khách hàng nhấn "Save & Update Request".
- **Then**: Hệ thống lưu trữ chính xác các ghi chú yêu cầu đặc biệt này để chuyển giao cho Chuyên viên thủ tục và Điều phối viên lộ trình.

### AC-003: Chặn chỉnh sửa danh sách ngựa khi đơn hàng đã duyệt

- **Given**: Yêu cầu vận chuyển `REQ-20260915-0001` đang ở trạng thái `APPROVED`.
- **When**: Khách hàng truy cập trang quản lý ngựa của yêu cầu này.
- **Then**: Hệ thống vô hiệu hóa tính năng thêm/sửa/xóa ngựa và hiển thị thông báo "Cannot modify racehorse list for requests that are already Approved or Rejected."

## References

### TDDs

- TDD-002: Quản lý Yêu cầu Vận chuyển & Quy trình Phê duyệt (Transport Request Management Architecture)

### Rules

- BR-003: Phân quyền Truy cập theo Vai trò (Role-Based Access Control - RBAC)
- BR-004: Quy định Nền tảng Truy cập (Responsive Web App Only)
- BR-005: Quy định Giới hạn Địa lý & Ngôn ngữ Hệ thống (European Scope & English Only)
- BR-019: Quy định Định danh & Thông tin Ngựa đua (Passport ID)
- BR-020: Quy định Chỉnh sửa Thông tin Đơn hàng & Danh sách Ngựa

### Dependencies

- Cơ sở dữ liệu thông tin Ngựa đua (Racehorse Entity Model)

## Non-Functional

- Security: Xác thực quyền sở hữu yêu cầu (chỉ Khách hàng tạo yêu cầu mới có quyền cập nhật danh sách ngựa của mình).
- Compatibility: Tương thích hoàn hảo trên giao diện Web responsive.

## Out of Scope

- Tải lên hộ chiếu y tế/giấy kiểm dịch cho từng con ngựa (được xử lý ở Flow 2 - UC-21).
- Phê duyệt thể trạng thi đấu của ngựa (do Escort/Bác sĩ thú y kiểm tra thực tế trong hành trình).
