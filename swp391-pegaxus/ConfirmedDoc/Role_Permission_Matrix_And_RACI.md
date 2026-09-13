# Ma Trận Phân Quyền & Ma Trận Trách Nhiệm RACI Toàn Hệ Thống (RBAC & RACI Matrix)
## Cross-Border Racehorse Transport System — Specification v2.0
### Dự Án: PasoFino (`swp391-pegaxus`)

> **Mã tài liệu**: `SPEC-RBAC-RACI`  
> **Áp dụng cho**: 6 Nhóm tác nhân (`Customer`, `Logistics Manager`, `Transport Specialist`, `Fleet & Route Coordinator`, `Vehicle Driver`, `Escort`).  
> **Mục tiêu**: Phân định ranh giới thẩm quyền, quyền hạn trên giao diện (UI) và API endpoint, ngăn chặn xung đột vai trò hoặc rò rỉ dữ liệu tài sản giá trị cao.

---

## 📑 Mục Lục

1. [Phần 1: Định Nghĩa 6 Vai Trò Người Dùng (Role Definitions)](#phần-1-định-nghĩa-6-vai-trò-người-dùng-role-definitions)
2. [Phần 2: Ma Trận Phân Quyền Thao Tác (CRUD Permission Matrix)](#phần-2-ma-trận-phân-quyền-thao-tác-crud-permission-matrix)
3. [Phần 3: Ma Trận Trách Nhiệm RACI Theo 6 Luồng Nghiệp Vụ](#phần-3-ma-trận-trách-nhiệm-raci-theo-6-luồng-nghiệp-vụ)
4. [Phần 4: Chính Sách Bảo Mật Dữ Liệu & Khả Năng Hiển Thị (Data Visibility Policy)](#phần-4-chính-sách-bảo-mật-dữ-liệu--khả-năng-hiển-thị-data-visibility-policy)

---

## Phần 1: Định Nghĩa 6 Vai Trò Người Dùng (Role Definitions)

| Vai trò (Role) | Mã hệ thống | Phân loại | Mục tiêu cốt lõi |
|---|---|---|---|
| **Khách Hàng (Chủ ngựa / CLB)** | `ROLE_CUSTOMER` | Tác nhân Bên ngoài (External) | Đặt chuyến, tải lên hồ sơ ngựa, theo dõi hành trình thời gian thực, xem thông tin người duyệt & audit log, xem/thanh toán hóa đơn, nghiệm thu và đánh giá dịch vụ. |
| **Quản Lý Logistics** | `ROLE_LOGISTICS_MANAGER` | Nội bộ Quản lý (Internal Operations) | Tiếp nhận & thẩm định đơn hàng, phân công nhân sự, quản trị đối tác vận tải ngoài, phê duyệt lộ trình & ngoại lệ, lập/phát hành hóa đơn và báo cáo KPI. |
| **Chuyên Viên Kiểm Dịch** | `ROLE_TRANSPORT_SPECIALIST` | Nội bộ Chuyên môn (Internal Compliance) | Quản lý quy định thú y các nước, quản lý & áp dụng bộ hồ sơ mẫu, kiểm tra hồ sơ ngựa, nộp hồ sơ thủ công tới cơ quan chức năng, cập nhật kết quả kiểm dịch/thông quan. |
| **Điều Phối Viên Đội Xe** | `ROLE_FLEET_COORDINATOR` | Nội bộ Kỹ thuật (Internal Fleet) | Quản lý phương tiện chuyên dụng, thiết kế lộ trình thủ công (bộ & thủy), quản lý lộ trình dự phòng, đặt trạm dừng 24h, theo dõi tiến độ chặng và tái lập lộ trình khi có sự cố. |
| **Tài Xế Phương Tiện** | `ROLE_VEHICLE_DRIVER` | Nội bộ Hiện trường (Field Operations) | Nhận xe & lộ trình, cập nhật mốc hành trình thủ công song song Realtime GPS, báo cáo sự cố xe/giao thông khẩn cấp, ghi nhận chi phí phát sinh và phối hợp dắt ngựa bàn giao. (Áp dụng cho xe công ty). |
| **Người Đi Cùng Chăm Sóc (Groom)** | `ROLE_ESCORT` | Nội bộ Hiện trường (Field Equine Welfare) | Nhận danh sách ngựa, kiểm tra thể trạng & đo thân nhiệt tại trạm nghỉ, ghi nhật ký phúc lợi kèm ảnh, báo cáo sự cố y tế khẩn cấp và phối hợp bàn giao tại đích. (Áp dụng cho xe công ty). |

---

## Phần 2: Ma Trận Phân Quyền Thao Tác (CRUD Permission Matrix)

> **Ký hiệu**:
> - **C (Create)**: Tạo mới
> - **R (Read)**: Xem / Đọc dữ liệu (Own = Chỉ xem của mình; All = Xem toàn hệ thống; Assigned = Xem chuyến được phân công)
> - **U (Update)**: Chỉnh sửa / Cập nhật
> - **D (Delete)**: Xóa dữ liệu
> - **A (Approve)**: Phê duyệt / Ký xác nhận
> - **—**: Không có quyền truy cập (Bị chặn ở tầng API và ẩn trên Menu)

| Module / Nhóm Dữ Liệu | Customer | Logistics Manager | Transport Specialist | Fleet Coordinator | Vehicle Driver | Escort |
|---|:---:|:---:|:---:|:---:|:---:|:---:|
| **Hồ sơ Cá nhân & Tài khoản** | C, R(Own), U | R(All), U, D | R(All) | R(All) | R(Own), U | R(Own), U |
| **Đơn Yêu Cầu Vận Chuyển (`TransportRequest`)** | C, R(Own), U(Draft), D(Draft) | R(All), U, D, **A** | R(All) | R(All) | R(Assigned) | R(Assigned) |
| **Danh Sách Ngựa Trong Đơn (`RequestHorseItem`)** | C, R(Own), U(Draft), D(Draft) | R(All), U | R(All), U | R(All) | R(Assigned) | R(Assigned) |
| **Danh Mục Quy Định Pháp Lý (`RegulatoryRequirement`)** | R(Public) | R(All) | **C, R(All), U, D** | R(All) | — | — |
| **Bộ Hồ Sơ Mẫu (`DossierTemplate`)** | — | R(All) | **C, R(All), U, D, A** | R(All) | — | — |
| **Giấy Tờ Pháp Lý Tải Lên (`DossierDocumentItem`)** | C, R(Own), U(NewVersion) | R(All) | R(All), U, **A(Validate)** | R(All) | R(Assigned) | R(Assigned) |
| **Nộp Hồ Sơ Cơ Quan Nhà Nước (`AuthoritySubmission`)** | — | R(All) | **C, R(All), U, A** | — | — | — |
| **Kế Hoạch Tuyến Đường Chính & Dự Phòng (`TripPlan` & `RoutePlan`)** | R(Assigned, No Cost) | R(All), U, **A** | R(All) | **C, R(All), U, D** | R(Assigned) | R(Assigned) |
| **Tài Nguyên Phương Tiện (`Vehicle` & `Equipment`)** | — | R(All), U | — | **C, R(All), U, D** | R(Assigned) | — |
| **Quản Lý Đối Tác Vận Tải Ngoài (`TransportSubcontractor`)** | — | **C, R(All), U, D** | — | R(All) | — | — |
| **Phân Công Nhân Sự Đội Xe & Đi Cùng (`TripStaffAssignment`)** | — | **C, R(All), U, A** | — | R(All), U(Gợi ý) | R(Assigned) | R(Assigned) |
| **Cập Nhật Mốc Hành Trình (`TripCheckpointLog`)** | R(Assigned Realtime) | R(All) | R(All) | R(All), U | **C, R(Assigned), U** | R(Assigned) |
| **Nhật Ký Sức Khỏe & Phúc Lợi Ngựa (`HorseHealthLog`)** | R(Assigned Realtime) | R(All) | R(All), U(Ghi chú) | R(All) | R(Assigned) | **C, R(Assigned), U** |
| **Báo Cáo Sự Cố Xe / Giao Thông (`VehicleIncidentSOS`)** | R(Notification) | R(All), U, **A** | R(All) | R(All), U(Lộ trình) | **C, R(Assigned)** | R(Assigned) |
| **Báo Cáo Sự Cố Y Tế Thú Y (`MedicalIncidentSOS`)** | R(Notification) | R(All), U, **A** | R(All), U(Thú y) | R(All) | R(Assigned) | **C, R(Assigned)** |
| **Đề Xuất & Ghi Nhận Chi Phí Phát Sinh (`EmergencyExpense`)** | — | R(All), U, **A** | — | **C, R(All), U** | **C, R(Assigned)** | **C, R(Assigned)** |
| **Biên Bản Bàn Giao Điện Tử (`e-POD` & Signature)** | **A (Ký nhận)**, R(Own) | R(All), A | R(All) | R(All) | **C, R(Assigned), A** | **C, R(Assigned), A** |
| **Khiếu Nại Bồi Thường (`TripClaim`)** | **C, R(Own), U(Draft)** | R(All), U, **A(Quyết toán)** | R(All), U(Giám định) | — | R(Assigned) | R(Assigned) |
| **Hóa Đơn & Thanh Toán (`Invoice` & `PaymentReceipt`)** | R(Own), U(Thanh toán) | **C, R(All), U, D, A** | — | — | — | — |
| **Báo Cáo Hiệu Suất & Doanh Thu (`AnalyticsKPI`)** | — | **R(All), C, U** | — | R(Fleet KPI) | — | — |

---

## Phần 3: Ma Trận Trách Nhiệm RACI Theo 6 Luồng Nghiệp Vụ

> **Ký hiệu chuẩn RACI**:
> - **R (Responsible - Người thực hiện)**: Người trực tiếp thao tác thực thi nhiệm vụ.
> - **A (Accountable - Người chịu trách nhiệm tối hậu)**: Người có quyền duyệt, chịu trách nhiệm cuối cùng về kết quả (Chỉ có duy nhất 1 "A" cho mỗi đầu việc).
> - **C (Consulted - Người tham vấn)**: Chuyên gia được hỏi ý kiến hai chiều trước khi thực hiện hoặc ra quyết định.
> - **I (Informed - Người được thông báo)**: Người nhận thông tin một chiều sau khi nhiệm vụ hoàn thành.

### Bảng RACI 24 Nhiệm Vụ Trọng Điểm

| STT | Đầu Việc / Hoạt Động Nghiệp Vụ | Customer | Logistics Manager | Transport Specialist | Fleet Coordinator | Vehicle Driver | Escort |
|:---:|---|:---:|:---:|:---:|:---:|:---:|:---:|
| **Flow 1** | **Quản lý Đặt Chuyến** | | | | | | |
| 1 | Khởi tạo đơn yêu cầu vận chuyển mới đa cá thể | **R** | I | — | — | — | — |
| 2 | Thẩm định tính khả thi & Phê duyệt đơn hàng | I | **A, R** | C | C | — | — |
| 3 | Xem thông tin người phê duyệt đơn hàng & Audit Log | **R** | R | R | R | — | — |
| **Flow 2** | **Quản lý Hồ Sơ Pháp Lý & Kiểm Dịch** | | | | | | |
| 4 | Áp dụng bộ hồ sơ mẫu phù hợp theo tuyến đường | — | I | **A, R** | C | — | — |
| 5 | Tải lên Hộ chiếu ngựa và chứng nhận tiêm phòng | **R** | — | **A (Kiểm tra)** | — | — | — |
| 6 | Thẩm định tính hợp lệ của từng loại giấy tờ | I | I | **A, R** | — | — | — |
| 7 | Nộp hồ sơ thủ công tới cơ quan chức năng bên ngoài | — | I | **A, R** | — | — | — |
| 8 | Đánh dấu thủ công kết quả kiểm dịch & thông quan | I | I | **A, R** | I | I | I |
| 9 | Xác nhận đủ điều kiện pháp lý để xuất phát (`Cleared`) | I | I | **A, R** | I | I | I |
| **Flow 3** | **Lập Tuyến Đường & Điều Phối Phương Tiện** | | | | | | |
| 10 | Tạo kế hoạch tổng quát & phân công nhân sự (DRV, ESC) | I | **A, R** | I | C | I | I |
| 11 | Thiết kế tuyến đường tối ưu (bộ & thủy) & trạm nghỉ 24h | — | I | C | **A, R** | — | — |
| 12 | Quản lý lộ trình dự phòng rủi ro | — | I | — | **A, R** | — | — |
| 13 | Quản lý phương tiện công ty & dịch vụ thuê ngoài | — | **A** | — | **R** | — | — |
| 14 | Phê duyệt lộ trình chính thức trước khi khởi hành | I | **A** | I | **R** | I | I |
| **Flow 4** | **Giám Sát Hành Trình & Thể Trạng Ngựa** | | | | | | |
| 15 | Cập nhật mốc hành trình thủ công song song Realtime | I | I | — | C | **A, R** | I |
| 16 | Kiểm tra thể trạng, đo thân nhiệt & ghi nhật ký ngựa | I | I | C | — | I | **A, R** |
| 17 | Chụp ảnh thực tế phúc lợi ngựa tại trạm nghỉ | I | I | — | — | I | **A, R** |
| **Flow 5** | **Quản Lý Sự Cố Khẩn Cấp (SOS)** | | | | | | |
| 18 | Báo cáo khẩn cấp sự cố xe cơ học / giao thông | — | I | I | I | **A, R** | I |
| 19 | Báo cáo khẩn cấp sự cố y tế thú y của ngựa | — | I | I | I | I | **A, R** |
| 20 | Phê duyệt đổi lộ trình nhánh / trạm thú y cấp cứu | I | **A** | C | **R** | C | C |
| 21 | Đề xuất & Phê duyệt chi phí phát sinh khẩn cấp | — | **A** | — | **R** | **R** | **R** |
| **Flow 6** | **Bàn Giao, Hóa Đơn & Báo Cáo Quản Trị** | | | | | | |
| 22 | Kiểm tra ngoại quan chạy nước kiệu & Ký e-POD tại đích | **R, A** | I | I | I | **R** | **R** |
| 23 | Lập, phát hành & ghi nhận thanh toán hóa đơn | **R(Trả)** | **A, R** | — | — | — | — |
| 24 | Thẩm định khiếu nại bồi thường & Lưu trữ lịch sử vận chuyển | C | **A, R** | C | C | I | I |

---

## Phần 4: Chính Sách Bảo Mật Dữ Liệu & Khả Năng Hiển Thị (Data Visibility Policy)

Ngựa đua là tài sản sinh học có giá trị từ hàng trăm nghìn đến hàng chục triệu USD/cá thể. Hệ thống áp dụng chính sách phân vùng dữ liệu nghiêm ngặt:

1. **Thông Tin Giá Trị Bảo Hiểm & Chi Phí Hợp Đồng**:
   - Chỉ `Logistics Manager` và `Customer` sở hữu đơn hàng mới xem được giá trị bảo hiểm ước tính, báo giá dịch vụ và thông tin hóa đơn thanh toán.
   - `Vehicle Driver`, `Escort` và `Fleet Coordinator` **bị ẩn hoàn toàn giá tiền** (chỉ nhìn thấy yêu cầu chăm sóc đặc biệt, tính nết của ngựa và địa chỉ giao nhận để tránh rủi ro an ninh/bắt cóc tống tiền).
2. **Định Vị Vệ Tinh GPS Thời Gian Thực (Real-time GPS Tracking)**:
   - `Fleet Coordinator`, `Logistics Manager` và `Vehicle Driver` xem được định vị chính xác theo thời gian thực (tần suất cập nhật 30 giây/lần).
   - `Customer` được xem định vị trực quan trên bản đồ nhưng hệ thống áp dụng cơ chế làm trễ 3-5 phút hoặc hiển thị theo từng chặng mốc để đảm bảo an ninh vận chuyển tài sản đặc biệt.
3. **Dữ Liệu Hồ Sơ Thú Y (Biosecurity Privacy)**:
   - Hồ sơ bệnh án và lịch sử tiêm vaccine của ngựa chỉ được hiển thị nội bộ cho `Transport Specialist`, `Logistics Manager`, `Customer` và `Escort` (chỉ thông tin chăm sóc cần thiết). Tuyệt đối không công khai ra bên ngoài để bảo vệ danh tiếng và giá trị thương mại của ngựa giống.
4. **Chính Sách Không Cấp Quyền Cho Hải Quan & Cơ Quan Nhà Nước (Zero-Access for Customs & Government)**:
   - Cơ quan Hải quan, Cục Thú y, BCP/APHA và cơ quan nhà nước các nước **HOÀN TOÀN KHÔNG CÓ TÀI KHOẢN** và **KHÔNG ĐƯỢC TRUY CẬP VÀO HỆ THỐNG**.
   - Mọi hoạt động nộp hồ sơ, tiếp nhận quyết định cấp phép, kết quả kiểm dịch và thông quan đều được thực hiện ngoại tuyến / bên ngoài bởi `Transport Specialist`. Sau đó, TS tự thao tác đánh dấu kết quả thủ công lên hệ thống PasoFino.
