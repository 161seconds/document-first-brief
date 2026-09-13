# Ma Trận Phân Quyền & Ma Trận Trách Nhiệm RACI Toàn Hệ Thống (RBAC & RACI Matrix)
## Cross-Border Racehorse Transport System — Specification v2.0

> **Mã tài liệu**: `SPEC-RBAC-RACI`  
> **Áp dụng cho**: 5 Nhóm tác nhân (`Customer`, `Logistics Manager`, `Transport Specialist`, `Fleet & Route Coordinator`, `Vehicle Driver / Escort`).  
> **Mục tiêu**: Phân định ranh giới thẩm quyền, quyền hạn trên giao diện (UI) và API endpoint, ngăn chặn xung đột vai trò hoặc rò rỉ dữ liệu tài sản giá trị cao.

---

## 📑 Mục Lục

1. [Phần 1: Định Nghĩa 5 Vai Trò Người Dùng (Role Definitions)](#phần-1-định-nghĩa-5-vai-trò-người-dùng-role-definitions)
2. [Phần 2: Ma Trận Phân Quyền Thao Tác (CRUD Permission Matrix)](#phần-2-ma-trận-phân-quyền-thao-tác-crud-permission-matrix)
3. [Phần 3: Ma Trận Trách Nhiệm RACI Theo 6 Luồng Nghiệp Vụ](#phần-3-ma-trận-trách-nhiệm-raci-theo-6-luồng-nghiệp-vụ)
4. [Phần 4: Chính Sách Bảo Mật Dữ Liệu & Khả Năng Hiển Thị (Data Visibility Policy)](#phần-4-chính-sách-bảo-mật-dữ-liệu--khả-năng-hiển-thị-data-visibility-policy)

---

## Phần 1: Định Nghĩa 5 Vai Trò Người Dùng (Role Definitions)

| Vai trò (Role) | Mã hệ thống | Phân loại | Mục tiêu cốt lõi |
|---|---|---|---|
| **Khách Hàng (Chủ ngựa / CLB)** | `ROLE_CUSTOMER` | Tác nhân Bên ngoài (External) | Đặt chuyến, tải lên hồ sơ ngựa, theo dõi hành trình thời gian thực, nghiệm thu và đánh giá dịch vụ. |
| **Quản Lý Logistics** | `ROLE_LOGISTICS_MANAGER` | Nội bộ Quản lý (Internal Operations) | Tiếp nhận & thẩm định đơn hàng, phân công nhân sự, quản trị chi phí, phê duyệt ngoại lệ và báo cáo KPI. |
| **Chuyên Viên Kiểm Dịch** | `ROLE_TRANSPORT_SPECIALIST` | Nội bộ Chuyên môn (Internal Compliance) | Quản lý quy định thú y các nước, kiểm tra hồ sơ ngựa, xin cấp EHC, khai báo TRACES-NT/CHED-A và làm việc với hải quan. |
| **Điều Phối Viên Đội Xe** | `ROLE_FLEET_COORDINATOR` | Nội bộ Kỹ thuật (Internal Fleet) | Quản lý xe tải/thùng bay, thiết kế tuyến đường tối ưu, đặt trạm dừng nghỉ 24h, điều phối tài xế và tái lập lộ trình khi có biến động. |
| **Tài Xế / Nhân Viên Đi Kèm** | `ROLE_VEHICLE_DRIVER` | Nội bộ Hiện trường (Field Operations) | Nhận nhiệm vụ, bốc ngựa, cập nhật mốc hành trình, đo chỉ số sinh tồn, báo cáo sự cố SOS và thực hiện bàn giao e-POD. |

---

## Phần 2: Ma Trận Phân Quyền Thao Tác (CRUD Permission Matrix)

> **Ký hiệu**:
> - **C (Create)**: Tạo mới
> - **R (Read)**: Xem / Đọc dữ liệu (Own = Chỉ xem của mình; All = Xem toàn hệ thống)
> - **U (Update)**: Chỉnh sửa / Cập nhật
> - **D (Delete)**: Xóa dữ liệu
> - **A (Approve)**: Phê duyệt / Ký xác nhận
> - **—**: Không có quyền truy cập (Bị chặn ở tầng API và ẩn trên Menu)

| Module / Nhóm Dữ Liệu | Customer | Logistics Manager | Transport Specialist | Fleet Coordinator | Vehicle Driver |
|---|:---:|:---:|:---:|:---:|:---:|
| **Hồ sơ Cá nhân & Tài khoản** | C, R(Own), U | R(All), U, D | R(All) | R(All) | R(Own), U |
| **Đơn Yêu Cầu Vận Chuyển (`TransportRequest`)** | C, R(Own), U(Draft), D(Draft) | R(All), U, D, **A** | R(All) | R(All) | R(Assigned) |
| **Danh Sách Ngựa Trong Đơn (`RequestHorseItem`)** | C, R(Own), U(Draft), D(Draft) | R(All), U | R(All), U | R(All) | R(Assigned) |
| **Danh Mục Quy Định Pháp Lý (`RegulatoryRequirement`)** | R(Public) | R(All) | **C, R(All), U, D** | R(All) | — |
| **Bộ Hồ Sơ Mẫu (`DossierTemplate`)** | — | R(All) | **C, R(All), U, D, A** | R(All) | — |
| **Giấy Tờ Pháp Lý Tải Lên (`DossierDocumentItem`)** | C, R(Own), U(NewVersion) | R(All) | R(All), U, **A(Validate)** | R(All) | R(Assigned) |
| **Nộp Hồ Sơ Cơ Quan Nhà Nước (`AuthoritySubmission`)** | — | R(All) | **C, R(All), U, A** | — | — |
| **Kế Hoạch Tuyến Đường (`TripPlan` & `RouteStop`)** | R(Assigned, No Cost) | R(All), U, **A** | R(All) | **C, R(All), U, D** | R(Assigned) |
| **Tài Nguyên Phương Tiện (`Vehicle` & `Equipment`)** | — | R(All), U | — | **C, R(All), U, D** | R(Assigned) |
| **Phân Công Đội Xe & Tài Xế (`TripAssignment`)** | — | **A(Phê duyệt)** | — | **C, R(All), U** | R(Assigned) |
| **Cập Nhật Mốc Hành Trình (`TripCheckpointLog`)** | R(Assigned Realtime) | R(All) | R(All) | R(All), U | **C, R(Assigned), U** |
| **Nhật Ký Sức Khỏe Ngựa (`HorseHealthTelemetry`)** | R(Assigned Realtime) | R(All) | R(All), U(Ghi chú y tế) | R(All) | **C, R(Assigned), U** |
| **Báo Cáo Sự Cố Khẩn Cấp (`EmergencyIncidentSOS`)** | R(Notification) | R(All), U, **A(Quyết định)** | R(All), U(Thú y) | R(All), U(Lộ trình) | **C, R(Assigned)** |
| **Biên Bản Bàn Giao Điện Tử (`e-POD` & Signature)** | **A (Ký nhận)**, R(Own) | R(All), A | R(All) | R(All) | **C, R(Assigned), A** |
| **Khiếu Nại Bồi Thường (`TransportClaim`)** | **C, R(Own), U(Draft)** | R(All), U, **A(Quyết toán)** | R(All), U(Giám định) | — | R(Assigned) |
| **Báo Cáo Hiệu Suất & Doanh Thu (`AnalyticsKPI`)** | — | **R(All), C, U** | — | R(Fleet KPI) | — |

---

## Phần 3: Ma Trận Trách Nhiệm RACI Theo 6 Luồng Nghiệp Vụ

> **Ký hiệu chuẩn RACI**:
> - **R (Responsible - Người thực hiện)**: Người trực tiếp thao tác thực thi nhiệm vụ.
> - **A (Accountable - Người chịu trách nhiệm tối hậu)**: Người có quyền duyệt, chịu trách nhiệm cuối cùng về kết quả (Chỉ có duy nhất 1 "A" cho mỗi đầu việc).
> - **C (Consulted - Người tham vấn)**: Chuyên gia được hỏi ý kiến hai chiều trước khi thực hiện hoặc ra quyết định.
> - **I (Informed - Người được thông báo)**: Người nhận thông tin một chiều sau khi nhiệm vụ hoàn thành.

### Bảng RACI 20 Nhiệm Vụ Trọng Điểm

| STT | Đầu Việc / Hoạt Động Nghiệp Vụ | Customer | Logistics Manager | Transport Specialist | Fleet Coordinator | Vehicle Driver |
|:---:|---|:---:|:---:|:---:|:---:|:---:|
| **Flow 1** | **Quản lý Đặt Chuyến** | | | | | |
| 1 | Khởi tạo đơn yêu cầu vận chuyển mới | **R** | I | — | — | — |
| 2 | Thẩm định tính khả thi & Phê duyệt đơn hàng | I | **A, R** | C | C | — |
| 3 | Phân công Specialist, Coordinator, Driver cho chuyến | I | **A, R** | I | I | I |
| **Flow 2** | **Quản lý Hồ Sơ Pháp Lý & Kiểm Dịch** | | | | | |
| 4 | Áp dụng bộ hồ sơ mẫu phù hợp theo tuyến đường | — | I | **A, R** | C | — |
| 5 | Tải lên Hộ chiếu ngựa và lịch sử tiêm phòng | **R** | — | **A (Kiểm tra)** | — | — |
| 6 | Thẩm định tính hợp lệ của từng loại giấy tờ | I | I | **A, R** | — | — |
| 7 | Làm việc với Bác sĩ Thú y OV để lấy EHC Form 8438 | — | I | **A, R** | — | — |
| 8 | Khai báo tờ khai CHED-A trên cổng EU TRACES-NT | — | I | **A, R** | I | — |
| 9 | Đăng ký sổ tạm nhập tái xuất ATA Carnet tại LCCI | C | I | **A, R** | — | — |
| 10 | Xác nhận đủ điều kiện pháp lý để xuất phát (`Cleared`) | I | I | **A, R** | I | I |
| **Flow 3** | **Lập Tuyến Đường & Điều Phối Phương Tiện** | | | | | |
| 11 | Thiết kế tuyến đường cao tốc và đặt trạm dừng 24h | — | I | C | **A, R** | — |
| 12 | Kiểm định kỹ thuật xe giảm xóc khí nén & thông gió | — | I | — | **A, R** | C |
| 13 | Nộp phê duyệt Kế hoạch Lộ trình (Section 1 Journey Log) | — | I | **A, R** | C | — |
| **Flow 4** | **Giám Sát Hành Trình & Thể Trạng Ngựa** | | | | | |
| 14 | Cập nhật mốc thời gian thực tại các trạm dừng nghỉ | I | I | — | C | **A, R** |
| 15 | Đo thân nhiệt, nhịp tim & chụp ảnh hiện trường | I | I | C | — | **A, R** |
| 16 | Giám sát cảnh báo nhiệt độ thùng xe từ xa | — | I | — | **A, R** | C |
| **Flow 5** | **Quản Lý Sự Cố Khẩn Cấp (SOS)** | | | | | |
| 17 | Nhấn nút báo động khẩn cấp SOS tại hiện trường | — | I | I | I | **A, R** |
| 18 | Phê duyệt phương án thay đổi tuyến đường / Sang xe | I | **A** | C | **R** | C |
| 19 | Điều phối Bác sĩ Thú y lưu động cấp cứu ngựa | I | C | **A, R** | C | C |
| **Flow 6** | **Bàn Giao, e-POD & Quyết Toán** | | | | | |
| 20 | Kiểm tra chạy nước kiệu trot-up & Ký e-POD tại đích | **R, A** | I | I | I | **R** |
| 21 | Làm thủ tục đóng sổ ATA Carnet với LCCI sau chuyến đi | — | I | **A, R** | — | — |
| 22 | Thẩm định và giải quyết khiếu nại bồi thường bảo hiểm | C | **A, R** | C | C | I |

---

## Phần 4: Chính Sách Bảo Mật Dữ Liệu & Khả Năng Hiển Thị (Data Visibility Policy)

Ngựa đua là tài sản sinh học có giá trị từ hàng trăm nghìn đến hàng chục triệu USD/cá thể. Hệ thống áp dụng chính sách phân vùng dữ liệu nghiêm ngặt:

1. **Thông Tin Giá Trị Bảo Hiểm & Chi Phí Hợp Đồng**:
   - Chỉ `Logistics Manager` và `Customer` sở hữu đơn hàng mới xem được giá trị bảo hiểm ước tính và báo giá dịch vụ.
   - `Vehicle Driver` và `Fleet Coordinator` **bị ẩn hoàn toàn giá tiền** (chỉ nhìn thấy yêu cầu chăm sóc đặc biệt, tính nết của ngựa và địa chỉ giao nhận để tránh rủi ro an ninh/bắt cóc tống tiền).
2. **Định Vị Vệ Tinh GPS Thời Gian Thực (Real-time GPS Tracking)**:
   - `Fleet Coordinator`, `Logistics Manager` và `Vehicle Driver` xem được định vị chính xác theo thời gian thực (tần suất cập nhật 30 giây/lần).
   - `Customer` được xem định vị trực quan trên bản đồ nhưng hệ thống áp dụng cơ chế làm trễ 3-5 phút hoặc hiển thị theo từng chặng mốc để đảm bảo an ninh vận chuyển tài sản đặc biệt.
3. **Dữ Liệu Hồ Sơ Thú Y (Biosecurity Privacy)**:
   - Hồ sơ bệnh án và lịch sử tiêm vaccine của ngựa chỉ được hiển thị cho `Transport Specialist`, `Logistics Manager`, `Customer` và cơ quan kiểm dịch nhà nước (BCP/APHA). Không công khai ra bên ngoài để bảo vệ danh tiếng và giá trị thương mại của ngựa giống.
