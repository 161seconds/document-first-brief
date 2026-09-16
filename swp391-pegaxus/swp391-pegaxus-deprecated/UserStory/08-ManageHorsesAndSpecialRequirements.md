# STORY-008: Quản lý danh sách ngựa và yêu cầu đặc biệt

## Metadata

- **Story**: Là một Khách hàng (Customer), tôi muốn thêm, chỉnh sửa hoặc xóa danh sách các cá thể ngựa tham gia chuyến đi và thiết lập các yêu cầu chăm sóc đặc biệt cho từng con ngựa (chế độ ăn, chuồng đơn/đôi, người hộ tống) để hệ thống tự động tính toán số lượng ngựa và chuẩn bị trang thiết bị vận chuyển phù hợp.
- **Context**: Ngựa đua là tài sản sinh học nhạy cảm, mỗi cá thể có tính nết, độ tuổi, tiền sử bệnh tật và nhu cầu dinh dưỡng khác nhau. Khách hàng cần khai báo chính xác danh tính của từng cá thể ngựa (Tên, Giống, Giới tính, Độ tuổi, Số microchip định danh) cùng các yêu cầu đặc thù để phía Logistics sắp xếp loại thùng chứa (Air Stalls), phân khoang xe tải và chuẩn bị thức ăn/nước uống theo quy chuẩn thú y.
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: HoangNT20
- **Assignee**:
  - Backend: Nguyễn Tiến Hoàng
  - QA: Đội ngũ QA SWP391

## Conditions

### Preconditions

- Khách hàng đã khởi tạo yêu cầu vận chuyển (ở trạng thái `DRAFT` hoặc `PENDING_APPROVAL`).
- Yêu cầu thuộc quyền sở hữu của khách hàng đang đăng nhập.

### Trigger

- Khách hàng chuyển sang Bước 2 của quy trình tạo đơn hoặc nhấn "Chỉnh sửa danh sách ngựa" tại trang chi tiết đơn hàng đang chờ duyệt.

## Flow

### Main Flow

1. Hệ thống hiển thị giao diện "Danh sách cá thể ngựa & Yêu cầu đặc biệt" liên kết với mã yêu cầu.
2. Khách hàng nhấn nút "Thêm cá thể ngựa" (`Add Horse`).
3. Hệ thống mở cửa sổ biểu mẫu nhập thông tin chi tiết cá thể ngựa:
   - Tên ngựa (`horseName`): Tên đăng ký thi đấu chính thức (bắt buộc).
   - Giống ngựa (`breed`): Ví dụ Thoroughbred, Arabian, Warmblood,... (bắt buộc).
   - Giới tính (`gender`): Ngựa đực giống (`Stallion`), Ngựa thiến (`Gelding`), hoặc Ngựa cái (`Mare`).
   - Độ tuổi (`age`): Số tuổi tính theo năm (số nguyên $\ge 1$).
   - Mã số chip định danh vi mạch (`microchipNumber`): 15 chữ số chuẩn ISO 11784/11785 (nếu có).
   - Tùy chọn chuồng khoang (`stallPreference`): Chuồng đơn độc lập (`Single Stall`), Chuồng đôi tiêu chuẩn (`Shared 1.5 Stall`), Chuồng hạng nhất (`Open Stall`).
   - Chế độ dinh dưỡng đặc biệt (`dietaryRequirements`): Loại cỏ khô (Timothy, Alfalfa), khẩu phần cám tinh, lượng nước tối thiểu, chất điện giải.
   - Hướng dẫn chăm sóc đặc biệt (`specialInstructions`): Ngựa nhát xe, dễ kích động, cần bịt tai chống ồn, có người đi kèm riêng (`Groom escort`).
4. Khách hàng nhập đầy đủ thông tin và nhấn "Lưu cá thể ngựa".
5. Hệ thống kiểm tra tính hợp lệ dữ liệu và lưu bản ghi vào bảng `RequestHorseItem`.
6. Khách hàng có thể lặp lại bước 2-5 để thêm nhiều cá thể ngựa khác vào cùng một chuyến đi.
7. Mỗi khi thêm, sửa hoặc xóa một cá thể ngựa:
   - Hệ thống tự động đếm tổng số lượng cá thể trong `RequestHorseItem` thuộc về `requestId`.
   - Hệ thống cập nhật trường `totalHorses` trong bảng `TransportRequest`.
8. Hệ thống hiển thị danh sách các ngựa đã thêm dưới dạng bảng tóm tắt kèm các thẻ phân loại nhu cầu chuồng/ăn uống.
9. Khách hàng kiểm tra lại danh sách và nhấn "Hoàn tất & Nộp đơn" (`Submit Request`).
10. Hệ thống lưu toàn bộ dữ liệu, cập nhật `updatedAt` của `TransportRequest` và thông báo nộp đơn thành công.

### Alternative Flow

#### ALT-01: Chỉnh sửa thông tin một cá thể ngựa đã thêm

1. Tại danh sách ngựa, khách hàng nhấn biểu tượng "Sửa" (`Edit`) bên cạnh tên con ngựa.
2. Hệ thống hiển thị lại biểu mẫu với đầy đủ thông tin đã lưu trước đó.
3. Khách hàng chỉnh sửa thông tin (ví dụ: đổi chế độ chuồng từ Single sang Shared) và nhấn "Cập nhật".
4. Hệ thống cập nhật bản ghi `RequestHorseItem` tương ứng.

#### ALT-02: Xóa một cá thể ngựa khỏi yêu cầu

1. Khách hàng nhấn biểu tượng "Xóa" (`Delete`) tại hàng của con ngựa tương ứng.
2. Hệ thống hiển thị hộp thoại xác nhận: "Bạn có chắc chắn muốn xóa ngựa [Tên ngựa] khỏi yêu cầu?".
3. Khách hàng xác nhận xóa.
4. Hệ thống xóa bản ghi khỏi `RequestHorseItem`, trừ số lượng tương ứng và cập nhật lại `totalHorses` trong `TransportRequest`.

### Exception Flow

#### EXC-01: Không có cá thể ngựa nào khi nhấn Nộp đơn

1. Tại bước 9 của Luồng chính, danh sách ngựa đang trống (`totalHorses = 0`).
2. Khách hàng cố tình nhấn nút "Hoàn tất & Nộp đơn".
3. Hệ thống chặn thao tác và hiển thị thông báo lỗi: "Yêu cầu vận chuyển phải có ít nhất 1 cá thể ngựa. Vui lòng thêm ngựa vào danh sách".

#### EXC-02: Trùng lặp mã số chip định danh vi mạch

1. Tại bước 4 của Luồng chính, khách hàng nhập mã số vi mạch trùng lặp với một con ngựa khác đã có trong danh sách chuyến đi này.
2. Hệ thống cảnh báo: "Mã số chip định danh này đã tồn tại trong danh sách chuyến đi".

## Acceptance Criteria

#### AC-001: Thêm thành công cá thể ngựa và cập nhật tổng số lượng

- **Given**: Yêu cầu vận chuyển hiện có 0 con ngựa (`totalHorses = 0`).
- **When**: Khách hàng thêm thành công ngựa tên "Pegasus Red", giống Thoroughbred, tuổi 4, chuồng Single.
- **Then**: Bản ghi mới được tạo trong `RequestHorseItem` và trường `totalHorses` của `TransportRequest` tự động cập nhật thành 1.

#### AC-002: Xóa cá thể ngựa giảm số lượng chính xác

- **Given**: Yêu cầu vận chuyển hiện có 3 con ngựa (`totalHorses = 3`).
- **When**: Khách hàng xóa 1 con ngựa khỏi danh sách.
- **Then**: Bản ghi bị xóa khỏi `RequestHorseItem` và `totalHorses` tự động giảm xuống còn 2.

#### AC-003: Chặn gửi đơn khi chưa có ngựa nào

- **Given**: Khách hàng ở màn hình cấu hình danh sách ngựa nhưng chưa thêm cá thể nào.
- **When**: Khách hàng nhấn "Hoàn tất & Nộp đơn".
- **Then**: Hệ thống không cho phép gửi và hiển thị cảnh báo yêu cầu tối thiểu 1 con ngựa.

## References

### TDDs

- TDD-002: Thiết kế Hệ thống Quản lý Đơn hàng & Vận chuyển (Order & Request Management Engine)

### Rules

- BR-012: Quy chuẩn khai báo thông tin nhận dạng sinh học cá thể ngựa (FEI Standard)
- BR-013: Quy tắc số lượng ngựa tối đa theo từng chủng loại phương tiện vận chuyển

### Dependencies

- Bảng dữ liệu giống ngựa và tiêu chuẩn kích thước thùng vận chuyển động vật sống

## Non-Functional

- Bảng danh sách ngựa tải mượt mà với số lượng lên tới 50 cá thể/chuyến đi.
- Giao diện có tính năng tự động lưu nháp (`Auto-save draft`) sau mỗi thao tác thêm/sửa ngựa.

## Out of Scope

- Tải lên chứng từ y tế / hộ chiếu ngựa chi tiết (chức năng này thực hiện ở Flow 2 sau khi đơn được phê duyệt).
