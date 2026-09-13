# Đặc Tả Vòng Đời & Máy Trạng Thái Toàn Hệ Thống (Master Lifecycle State Machines)
## Cross-Border Racehorse Transport System — Specification v2.0

> **Mã tài liệu**: `SPEC-LIFECYCLE-STATE-MACHINE`  
> **Áp dụng cho**: Toàn bộ kiến trúc dữ liệu và logic chuyển trạng thái của hệ thống.  
> **Mục tiêu**: Định nghĩa rõ ràng điều kiện kích hoạt, tác nhân thực hiện, trạng thái hợp lệ và cơ chế hoàn tác (Rollback) cho 6 thực thể cốt lõi.

---

## 📑 Mục Lục Các Máy Trạng Thái (State Machines)

1. [Máy Trạng Thái 1: Yêu Cầu Vận Chuyển (`TransportRequestStatus`)](#1-máy-trạng-thái-1-yêu-cầu-vận-chuyển-transportrequeststatus)
2. [Máy Trạng Thái 2: Bộ Hồ Sơ Pháp Lý (`TripLegalDossierStatus`)](#2-máy-trạng-thái-2-bộ-hồ-sơ-pháp-lý-triplegaldossierstatus)
3. [Máy Trạng Thái 3: Giấy Tờ Pháp Lý Đơn Lẻ (`DossierDocumentItemStatus`)](#3-máy-trạng-thái-3-giấy-tờ-pháp-lý-đơn-lẻ-dossierdocumentitemstatus)
4. [Máy Trạng Thái 4: Kế Hoạch Chuyến Đi (`TripPlanStatus`)](#4-máy-trạng-thái-4-kế-hoạch-chuyến-đi-tripplanstatus)
5. [Máy Trạng Thái 5: Cá Thể Ngựa Trong Hành Trình (`HorseTransitStatus`)](#5-máy-trạng-thái-5-cá-thể-ngựa-trong-hành-trình-horsetransitstatus)
6. [Máy Trạng Thái 6: Sự Cố Khẩn Cấp Hiện Trường (`IncidentReportStatus`)](#6-máy-trạng-thái-6-sự-cố-khẩn-cấp-hiện-trường-incidentreportstatus)

---

## 1. Máy Trạng Thái 1: Yêu Cầu Vận Chuyển (`TransportRequestStatus`)

Vòng đời của đơn đặt hàng từ lúc khách hàng khởi tạo đến khi đóng đơn:

```mermaid
stateDiagram-v2
    [*] --> DRAFT: Khách hàng tạo nháp
    DRAFT --> PENDING_APPROVAL: Khách hàng nộp yêu cầu
    DRAFT --> CANCELLED: Khách hàng hủy nháp
    
    PENDING_APPROVAL --> UNDER_REVIEW: Logistics Manager mở xem xét
    PENDING_APPROVAL --> CANCELLED: Khách hàng hủy (trước khi duyệt)
    
    UNDER_REVIEW --> APPROVED: LM thẩm định tính khả thi & Duyệt
    UNDER_REVIEW --> REJECTED: LM từ chối (Ghi rõ lý do)
    UNDER_REVIEW --> REVISION_REQUESTED: LM yêu cầu bổ sung thông tin
    
    REVISION_REQUESTED --> PENDING_APPROVAL: Khách hàng cập nhật lại đơn
    REVISION_REQUESTED --> CANCELLED: Khách hàng không cập nhật / Hủy
    
    APPROVED --> ASSIGNED: LM phân công TS, FRC và VDE
    ASSIGNED --> IN_PROGRESS: Chuyến đi bắt đầu khởi hành
    IN_PROGRESS --> COMPLETED: Bàn giao thành công tại đích
    IN_PROGRESS --> FAILED_TERMINATED: Hủy chuyến giữa đường do sự cố bất khả kháng
    
    COMPLETED --> CLOSED: Hết thời hạn khiếu nại (24h)
    REJECTED --> [*]
    CANCELLED --> [*]
    CLOSED --> [*]
    FAILED_TERMINATED --> [*]
```

### Bảng Ma Trận Chuyển Trạng Thái (`TransportRequest`)

| Trạng thái hiện tại | Sự kiện kích hoạt (Trigger) | Tác nhân thực hiện | Trạng thái tiếp theo | Điều kiện kiểm tra (Guards) |
|---|---|---|---|---|
| `DRAFT` | `SUBMIT_REQUEST` | Customer | `PENDING_APPROVAL` | Điểm đi, điểm đến, thời gian, danh sách ngựa >= 1 con. |
| `PENDING_APPROVAL` | `START_REVIEW` | Logistics Manager | `UNDER_REVIEW` | LM mở chi tiết đơn hàng thẩm định năng lực xe/tài xế. |
| `UNDER_REVIEW` | `APPROVE_REQUEST` | Logistics Manager | `APPROVED` | Lộ trình khả thi, đội xe còn chỗ, thời gian thỏa mãn Lead Time. |
| `UNDER_REVIEW` | `REJECT_REQUEST` | Logistics Manager | `REJECTED` | Bắt buộc nhập lý do từ chối (`rejectionReason != null`). |
| `UNDER_REVIEW` | `REQUEST_CHANGE` | Logistics Manager | `REVISION_REQUESTED` | Bắt buộc nhập nội dung cần sửa đổi (`notes != null`). |
| `APPROVED` | `DISPATCH_TEAM` | Logistics Manager | `ASSIGNED` | Đã gán đủ 3 vai trò: Transport Specialist, Coordinator, Driver. |
| `ASSIGNED` | `DEPART_TRIP` | Vehicle Driver | `IN_PROGRESS` | Bộ hồ sơ pháp lý đã `CLEARED_FOR_DEPARTURE`. |
| `IN_PROGRESS` | `DELIVERY_CONFIRMED` | Driver & Customer | `COMPLETED` | Ký e-POD thành công, Section 3 Journey Log hoàn tất. |
| `COMPLETED` | `CLOSE_ORDER` | Hệ thống tự động | `CLOSED` | Sau 24h kể từ thời điểm ký e-POD và không có khiếu nại. |

---

## 2. Máy Trạng Thái 2: Bộ Hồ Sơ Pháp Lý (`TripLegalDossierStatus`)

Quản lý tiến độ hoàn thiện toàn bộ giấy tờ của một chuyến vận chuyển:

```mermaid
stateDiagram-v2
    [*] --> INITIATED: Áp dụng bộ hồ sơ mẫu theo tuyến
    INITIATED --> DOCUMENTS_PENDING: Chờ khách hàng nộp đủ giấy tờ
    DOCUMENTS_PENDING --> VERIFICATION_IN_PROGRESS: TS tiến hành kiểm tra
    
    VERIFICATION_IN_PROGRESS --> CORRECTION_REQUIRED: Phát hiện giấy tờ sai/hết hạn
    CORRECTION_REQUIRED --> VERIFICATION_IN_PROGRESS: Khách hàng tải lại giấy tờ mới
    
    VERIFICATION_IN_PROGRESS --> AWAITING_AUTHORITY_APPROVAL: Đủ giấy tờ, nộp EHC/TRACES
    AWAITING_AUTHORITY_APPROVAL --> CLEARED_FOR_DEPARTURE: Cơ quan nhà nước phê duyệt
    AWAITING_AUTHORITY_APPROVAL --> REJECTED_BY_AUTHORITY: Bị cơ quan chức năng từ chối
    
    CLEARED_FOR_DEPARTURE --> AT_BORDER_INSPECTION: Xe đến trạm kiểm soát BCP
    AT_BORDER_INSPECTION --> CUSTOMS_CLEARED: BCP & Hải quan thông quan thành công
    AT_BORDER_INSPECTION --> BORDER_DETAINED: Bị giữ lại tại BCP do nghi ngờ dịch tễ
    
    BORDER_DETAINED --> CUSTOMS_CLEARED: Khắc phục xong kiểm dịch / nộp bảo lãnh
    BORDER_DETAINED --> REJECTED_TURNED_BACK: Buộc quay đầu hoặc đưa vào cách ly
    
    CUSTOMS_CLEARED --> RECONCILED_CLOSED: Sổ ATA Carnet đã thanh khoản xong
    RECONCILED_CLOSED --> [*]
```

---

## 3. Máy Trạng Thái 3: Giấy Tờ Pháp Lý Đơn Lẻ (`DossierDocumentItemStatus`)

Vòng đời của từng tệp chứng từ (Hộ chiếu, EHC, ATA Carnet, Test Coggins):

```mermaid
stateDiagram-v2
    [*] --> MISSING: Giấy tờ bắt buộc chưa có file
    MISSING --> UPLOADED: Khách hàng hoặc TS tải file lên
    UPLOADED --> VALIDATED: TS kiểm tra tính hợp lệ & Duyệt
    UPLOADED --> INVALID_REJECTED: TS từ chối (file mờ, hết hạn)
    
    INVALID_REJECTED --> UPLOADED: Khách hàng tải phiên bản mới (v2)
    
    VALIDATED --> SUBMITTED_TO_AUTHORITY: Nộp cơ quan nhà nước (APHA/DGAL)
    SUBMITTED_TO_AUTHORITY --> APPROVED_BY_GOV: Cơ quan cấp chứng thư chính thức
    SUBMITTED_TO_AUTHORITY --> REJECTED_BY_GOV: Cơ quan từ chối cấp phép
    
    APPROVED_BY_GOV --> EXPIRED: Hết hạn sử dụng (qua 10 ngày EHC)
    APPROVED_BY_GOV --> ARCHIVED: Hoàn thành chuyến đi & Lưu trữ
    EXPIRED --> [*]
    ARCHIVED --> [*]
```

---

## 4. Máy Trạng Thái 4: Kế Hoạch Chuyến Đi (`TripPlanStatus`)

```mermaid
stateDiagram-v2
    [*] --> DRAFT_PLANNING: FRC thiết kế tuyến đường & điểm dừng
    DRAFT_PLANNING --> READY_FOR_DISPATCH: Lộ trình hoàn tất & Hồ sơ pháp lý Đạt
    READY_FOR_DISPATCH --> IN_TRANSIT: Xe lăn bánh khởi hành
    
    IN_TRANSIT --> REST_STOP_ACTIVE: Dừng nghỉ ngơi / Kiểm tra sức khỏe
    REST_STOP_ACTIVE --> IN_TRANSIT: Tiếp tục chạy chặng tiếp theo
    
    IN_TRANSIT --> BCP_PROCESSING: Đang làm thủ tục tại Trạm Kiểm soát Biên giới
    BCP_PROCESSING --> IN_TRANSIT: Thông quan thành công, xe rời BCP
    
    IN_TRANSIT --> EMERGENCY_HOLD: Sự cố khẩn cấp (Hỏng xe / Sốt cấp tính)
    EMERGENCY_HOLD --> REROUTED_RESUMED: Tái lập tuyến đường & chạy tiếp
    EMERGENCY_HOLD --> ABORTED_RETURN: Buộc hủy chuyến & đưa ngựa vào bệnh viện
    
    IN_TRANSIT --> ARRIVED_AT_DESTINATION: Xe đến cổng trường đua đích
    ARRIVED_AT_DESTINATION --> UNLOADED_COMPLETED: Hạ tải, khám nghiệm & Ký e-POD
    UNLOADED_COMPLETED --> [*]
```

---

## 5. Máy Trạng Thái 5: Cá Thể Ngựa Trong Hành Trình (`HorseTransitStatus`)

> **Quy tắc quan trọng**: Một chuyến xe chở nhiều con ngựa; nếu 1 con ngựa bị bệnh, con ngựa đó chuyển trạng thái riêng biệt mà không làm ảnh hưởng đến định danh của các cá thể khỏe mạnh khác.

```mermaid
stateDiagram-v2
    [*] --> STABLE_ORIGIN: Ngựa tại chuồng nuôi xuất phát
    STABLE_ORIGIN --> LOADED_ON_TRUCK: Bốc lên xe chuyên dụng
    
    LOADED_ON_TRUCK --> IN_TRANSIT_STABLE: Đang di chuyển, thể trạng ổn định
    
    IN_TRANSIT_STABLE --> RESTING_CONTROL_POST: Hạ tải nghỉ 24h tại trạm nghỉ
    RESTING_CONTROL_POST --> IN_TRANSIT_STABLE: Bốc lên xe tiếp tục chặng 2
    
    IN_TRANSIT_STABLE --> HEALTH_WARNING: Thân nhiệt tăng (38.4°C - 38.9°C)
    HEALTH_WARNING --> IN_TRANSIT_STABLE: Hạ nhiệt sau khi uống nước điện giải
    HEALTH_WARNING --> CRITICAL_ISOLATED: Sốt trên 39.0°C / Đau bụng Colic
    
    CRITICAL_ISOLATED --> ADMITTED_TO_CLINIC: Chuyển viện thú y khẩn cấp
    CRITICAL_ISOLATED --> EUTHANASIA_DECEASED: Tử vong do chấn thương bất khả kháng
    
    IN_TRANSIT_STABLE --> DELIVERED_HEALTHY: Bàn giao an toàn tại chuồng đích
    DELIVERED_HEALTHY --> [*]
    ADMITTED_TO_CLINIC --> [*]
    EUTHANASIA_DECEASED --> [*]
```

---

## 6. Máy Trạng Thái 6: Sự Cố Khẩn Cấp Hiện Trường (`IncidentReportStatus`)

```mermaid
stateDiagram-v2
    [*] --> REPORTED: Tài xế nhấn nút SOS trên ứng dụng
    REPORTED --> TRIAGED: Trung tâm điều hành tiếp nhận & phân loại cấp độ
    
    TRIAGED --> DISPATCHING_RESPONSE: Điều động Thú y / Xe cứu hộ / Xe dự phòng
    DISPATCHING_RESPONSE --> ON_SCENE_HANDLING: Lực lượng hỗ trợ có mặt xử lý
    
    ON_SCENE_HANDLING --> RESOLVED_RESUMED: Xử lý xong, tiếp tục lộ trình
    ON_SCENE_HANDLING --> ESCALATED_CRITICAL: Vượt quá thẩm quyền, báo cáo Ban Giám Đốc
    
    RESOLVED_RESUMED --> POST_INCIDENT_AUDIT: Lập biên bản sự cố phục vụ bảo hiểm
    ESCALATED_CRITICAL --> POST_INCIDENT_AUDIT
    
    POST_INCIDENT_AUDIT --> CLOSED: Đóng hồ sơ sự cố
    CLOSED --> [*]
```

---

## 7. Bảng Tổng Hợp Kiểm Soát Tính Toàn Vẹn Chuyển Đổi Trạng Thái (Guard Conditions)

Bảng này cung cấp các điều kiện logic (IF-THEN) để lập trình viên cài đặt vào backend (Spring Boot / NestJS / C#):

| Entity | Từ trạng thái | Sang trạng thái | Điều kiện tiên quyết (Guard Condition) | Ngoại lệ xử lý nếu vi phạm |
|---|---|---|---|---|
| `TransportRequest` | `APPROVED` | `ASSIGNED` | Đã gán `specialistId != null`, `coordinatorId != null`, `driverId != null`. | Báo lỗi `400: UnassignedTeamException`. |
| `TripPlan` | `READY_FOR_DISPATCH` | `IN_TRANSIT` | `TripLegalDossier.overallStatus == 'CLEARED_FOR_DEPARTURE'`. | Khóa nút xuất phát `403: DossierNotClearedException`. |
| `DossierDocumentItem` | `MISSING` | `VALIDATED` | Không được nhảy cóc qua trạng thái `UPLOADED`. | Báo lỗi `400: InvalidStateTransitionException`. |
| `TripPlan` | `BCP_PROCESSING` | `IN_TRANSIT` | Phải có mã phê duyệt Phần II CHED-A và dấu hải quan. | Không cho phép rời BCP `403: BorderClearanceRequired`. |
| `TripPlan` | `ARRIVED` | `UNLOADED_COMPLETED` | Bắt buộc phải có `ePodSignature != null` và tọa độ GPS hợp lệ. | Báo lỗi `400: SignatureMissingException`. |
