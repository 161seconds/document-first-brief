# BẢN ĐÁNH GIÁ & GÓP Ý TOÀN DIỆN CƠ SỞ DỮ LIỆU (SWP391 - PEGAXUS)
**Hệ thống**: Vận chuyển ngựa đua qua biên giới quốc tế (*Cross-border Racehorse Transport System*)  
**Đối chiếu**: Giữa schema gốc (`db.md`) và bản tinh chỉnh kiến trúc (`QB-db.dbml` / `QB-db.md`)

---

## I. TỔNG QUAN ĐÁNH GIÁ HIỆN TRẠNG (`db.md`)

### 1. Điểm mạnh đã có
* **Phân tách luồng rõ ràng**: Chia tách tốt 8 phân hệ cốt lõi: Tài khoản, Đặt chỗ & Chuyến đi, Lập kế hoạch, Vận hành & Sức khỏe ngựa, Sự cố khẩn cấp, Tài chính, Khiếu nại, Thủ tục kiểm dịch & Thông báo.
* **Quy chuẩn mã hóa tốt**: Có tiền tố và hậu tố rõ ràng (`code`, `status`, `created_at`, `updated_at`), đặt tên nhất quán theo chuẩn PascalCase cho bảng và snake_case cho cột.
* **Tính toán trạng thái toàn diện**: Sử dụng triệt để Enum để kiểm soát chặt chẽ máy trạng thái (State Machine) từ đơn hàng đến chuyến đi.

### 2. Những điểm yếu & Lỗ hổng kỹ thuật cốt lõi
1. **Ràng buộc tài sản sai bản chất (Vehicle lock)**: Phương tiện (`Vehicle`) lại gắn cứng khóa ngoại `transport_plan_id`, khiến 1 xe chỉ chạy được 1 chuyến duy nhất trong đời.
2. **Dư thừa dữ liệu (Data Denormalization)**: Bảng `Location` vừa chứa `route_id` vừa chứa `transport_plan_id`.
3. **Lỗ hổng nghiệp vụ kiểm dịch thú y quốc tế**: Hồ sơ kiểm dịch (`ComplianceDossier`) chỉ quản lý chung theo chuyến mà thiếu scope từng con ngựa (`horse_id`). Trong thực tế hải quan quốc tế (OIE/FEI), mỗi con ngựa có 1 hộ chiếu (FEI Passport) và phiếu xét nghiệm dịch tễ riêng biệt.
4. **Cứng nhắc khi phát sinh sự cố trên đường**: `IncidentReport` bắt buộc phải có `location_id` (không cho `NULL`), trong khi tai nạn giao thông hoặc chết máy đa phần xảy ra giữa cao tốc/quốc lộ.
5. **Thiếu tính nghiệm thu cá thể ngựa khi bàn giao**: `HandoverRecord` chỉ ghi nhận chung chung cho cả đàn, không có biên bản chi tiết từng con khi giao nhận.
6. **Thừa thãi / Trùng lặp trong Notification**: Trạng thái `READ` trong Enum bị dư thừa khi bảng đã có sẵn cột `read_at`.

---

## II. CHI TIẾT NHỮNG ĐIỂM NÊN BỎ / TINH GIẢN (WHAT TO REMOVE)

| STT | Vị trí / Đối tượng | Đề xuất | Lý do kỹ thuật & Thực tế nghiệp vụ |
| :--- | :--- | :--- | :--- |
| **1** | `Enum notification_recipient_status` | **Bỏ trạng thái `READ`** | Trong bảng `AccountNotification` đã có sẵn cột `read_at timestamp`. Muốn biết đã đọc hay chưa chỉ cần check `read_at IS NOT NULL`. Enum chỉ nên giữ trạng thái vận chuyển tin: `PENDING`, `DELIVERED`, `FAILED`. |
| **2** | `Table Location` | **Bỏ cột `transport_plan_id`** | Bị trùng lặp dữ liệu (vi phạm chuẩn hóa 3NF). Từ `Location` $\rightarrow$ đã có `route_id` $\rightarrow$ `RoutePlan` $\rightarrow$ `TransportPlan`. Giữ lại cả hai dễ gây hiện tượng bất nhất dữ liệu (Inconsistency). |
| **3** | `Table Vehicle` | **Bỏ cột `transport_plan_id` khỏi bảng `Vehicle`** | Xe là tài sản cố định (Master Data) dùng nhiều lần. Nếu để `transport_plan_id` trong `Vehicle`, khi xe chạy chuyến mới sẽ đè mất dữ liệu chuyến cũ. |
| **4** | `Table IncidentReport` | **Bỏ ràng buộc `[not null]` ở `location_id`** | Cho phép `location_id` nhận giá trị `NULL`. Xe gặp nạn giữa cao tốc hoặc trên phà biển sẽ không thể map vào một `Location` (trạm dừng) cố định có sẵn trong database. |
| **5** | `Table Claim` | **Bỏ ràng buộc `[not null]` ở `handover_record_id`** | Cho phép `handover_record_id` nhận giá trị `NULL`. Khách hàng có thể khiếu nại vì chậm trễ chuyến bay, thất lạc yên cương dọc đường, hoặc thái độ tài xế mà không liên quan trực tiếp đến tờ biên bản bàn giao tại đích. |

---

## III. CHI TIẾT NHỮNG ĐIỂM NÊN THÊM / BỔ SUNG (WHAT TO ADD)

### 1. Phân hệ Thủ tục Kiểm dịch & Pháp lý (Compliance)
* **Thêm `horse_id (nullable)` vào `DossierRequirement` và `ComplianceDocument`**:
  * *Lý do*: Một chuyến vận chuyển 5 con ngựa sẽ có 2 loại giấy tờ:
    * Giấy chung cả chuyến: Giấy phép xuất nhập cảnh phương tiện, chứng nhận khử trùng xe.
    * Giấy riêng từng con: Hộ chiếu ngựa (FEI Passport), xét nghiệm sốt thiếu máu truyền nhiễm (Coggins Test), xét nghiệm viêm động mạch ngựa (EVA), chứng thư tiêm phòng cúm.
* **Thêm `OTHER` vào `Enum document_request_type`**:
  * *Lý do*: Cơ quan kiểm dịch/hải quan cửa khẩu các nước phát sinh nhiều yêu cầu dị biệt (công chứng dịch thuật lãnh sự, giấy ủy quyền chủ nuôi, chứng nhận miễn trừ...). Bảng đã có cột `message text` để giải thích chi tiết.
* **Thêm `submitted_by_employee_id` vào `AuthoritySubmission`**:
  * *Lý do*: Xác định rõ Chuyên viên thủ tục nào là người trực tiếp đại diện nộp bộ hồ sơ lên cơ quan nhà nước (phục vụ Audit log).

### 2. Phân hệ Lập kế hoạch Vận chuyển (Transport Planning)
* **Thêm bảng trung gian `TransportPlanVehicle` (N-N)**:
  * *Cấu trúc*: `(transport_plan_id, vehicle_id, assigned_at, note)`
  * *Lý do*: Cho phép 1 kế hoạch điều phối nhiều xe (ví dụ: 1 xe chở 6 con, 1 xe hậu cần kéo rơ-moóc đồ đạc), đồng thời giải phóng bảng `Vehicle` thành tài sản dùng trọn đời.

### 3. Phân hệ Bàn giao & Sức khỏe Ngựa (Execution & Handover)
* **Thêm bảng `HandoverHorseDetail` (Chi tiết nghiệm thu thể trạng từng con)**:
  * *Cấu trúc*: `(handover_record_id, horse_id, health_status, condition_note, created_at)`
  * *Lý do*: Tránh tranh chấp pháp lý. Khi bàn giao tại đích hoặc cửa khẩu, tài xế và người nhận kiểm tra từng con một (con nào trầy chân, con nào sốt nhẹ ghi nhận rõ ràng tại chỗ).
* **Thêm `location_id (nullable)` và tọa độ GPS vào `HorseHealthLog`**:
  * *Lý do*: Hiện tại chỉ có `location_description` dạng text. Khi có `location_id`, hệ thống có thể phân tích báo cáo: *Trạm dừng nào hoặc chặng nào khiến ngựa hay bị mất nước/stress nhất*.

### 4. Phân hệ Xử lý Sự cố khẩn cấp (Incident Management)
* **Thêm `latitude`, `longitude` và `transport_id` trực tiếp vào `IncidentReport`**:
  * *Lý do*: Khi xe gặp tai nạn giữa đường, tài xế bấm nút SOS trên Mobile App. Hệ thống lấy ngay tọa độ GPS vệ tinh để gửi đội cứu hộ/thú y lưu động đến ứng cứu khẩn cấp.

### 5. Phân hệ Tài khoản, Đơn hàng & Khiếu nại (Audit & Tracking)
* **Thêm `viewed_at timestamp` vào `Table Booking` và `Table Claim`**:
  * *Lý do*: Phân biệt giữa hai trạng thái:
    * Khách vừa gửi (`PENDING` / `SUBMITTED`).
    * Nhân viên đã mở ra xem màn hình chi tiết (`viewed_at` được gán giờ) nhưng chưa kịp chuyển trạng thái sang `UNDER_REVIEW`.
* **Thêm `booking_id` trực tiếp vào `Table Claim`**:
  * *Lý do*: Khách hàng khiếu nại thường tra cứu và khiếu nại trực tiếp từ Mã đơn hàng (`booking_code`), không cần biết `handover_record_id` nội bộ của hệ thống là gì.

---

## IV. BẢNG TỔNG HỢP SO SÁNH TRƯỚC VÀ SAU KHI TỐI ƯU

| Hạng mục | Bản gốc (`db.md`) | Bản đề xuất tối ưu (`QB-db.dbml`) | Giá trị mang lại |
| :--- | :--- | :--- | :--- |
| **Quản lý Xe** | Xe bị khóa chết vào 1 kế hoạch | Tách bảng trung gian `TransportPlanVehicle` | Quản lý đội xe linh hoạt, tái sử dụng tài sản |
| **Kiểm dịch Ngựa** | Quản lý chung cả chuyến | Bổ sung `horse_id` vào từng đầu mục giấy | Đúng chuẩn thủ tục thú y quốc tế OIE/FEI |
| **Bàn giao Ngựa** | 1 biên bản chung chung | Bổ sung bảng `HandoverHorseDetail` | Rõ ràng trách nhiệm, hạn chế kiện tụng |
| **Báo sự cố SOS** | Bắt buộc chọn trạm dừng có sẵn | Cho phép `NULL` trạm dừng + Tọa độ GPS | Cứu hộ realtime chính xác giữa đường |
| **Khiếu nại Claim** | Bắt buộc có biên bản giao nhận | Nối trực tiếp `booking_id`, nullable handover | Khách dễ khiếu nại các vấn đề khác |
| **Chuẩn hóa 3NF** | `Location` bị dư thừa FK | Bỏ `transport_plan_id` khỏi `Location` | Cơ sở dữ liệu sạch, không lo lệch dữ liệu |
| **Delivery Status** | Enum lẫn lộn giữa Gửi và Đọc | Tách biệt `status` gửi tin và cột `read_at` | Đúng bản chất kỹ thuật Web Push / Mailer |

---

## V. ĐỀ XUẤT LẬP LUẬN BẢO VỆ TRƯỚC GIẢNG VIÊN / HỘI ĐỒNG (DEFENSE STRATEGY)

Khi thuyết trình hoặc phản biện với giảng viên, nhóm có thể tự tin khẳng định:

1. **"Database của nhóm không chỉ là đồ án lý thuyết, mà bám sát nghiệp vụ vận tải sống động ngoài thực tế"**:
   * Ngựa đua có giá trị hàng triệu USD, vận chuyển qua biên giới đòi hỏi kiểm dịch từng cá thể nghiêm ngặt (FEI/OIE) $\rightarrow$ Cần bảng chi tiết từng con và giấy tờ theo từng con ngựa.
2. **"Hệ thống thiết kế theo kiến trúc điều hành thời gian thực (Real-time Operations)"**:
   * Tài xế dùng Mobile App ghi nhận sinh hiệu, ký nhận điện tử (`signature_url`), báo tọa độ GPS khẩn cấp khi xe gặp nạn $\rightarrow$ Quản lý ở văn phòng trung tâm giám sát Dashboard trực tiếp mà không cần có mặt cơ học tại chỗ.
3. **"Mô hình dữ liệu đạt chuẩn hóa 3NF và khả năng mở rộng cao (Scalability)"**:
   * Tách rời xe và đối tác vận tải thành các bảng Master Data độc lập, sẵn sàng đáp ứng khi quy mô công ty tăng từ 5 xe lên 500 xe.
