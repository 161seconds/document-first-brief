# Ma Trận Giấy Tờ Qua Lại Từng Cặp Quốc Gia (Country-Pair Document Matrix — DB Ready)
## Pegaxus: Cross-Border Racehorse Transport System — Seed Specification v1.0

> **Mã tài liệu**: `DB-SEED-COUNTRY-PAIR-DOCUMENT-MATRIX`  
> **Mục tiêu**: Chuẩn hóa ma trận giấy tờ theo cặp điểm đi (`Src`) và điểm đến (`Dest`), phân tách rõ Lượt Đi (`OUTBOUND`) và Lượt Về (`RETURN`).  
> **Ứng dụng**: Nhúng trực tiếp vào bảng cơ sở dữ liệu `RegulatoryRequirement` và `DossierTemplateItem` (Flow 2) làm dữ liệu Seed Data / Fixtures.

---

## 📑 1. Bảng Tra Cứu Mã Quốc Gia Chuẩn ISO-3166 & Phân Loại Tuyến

| Mã Nước (`ISO-2`) | Tên Quốc Gia | Khối Pháp Lý | Vai Trò Trong Mạng Lưới Pegaxus |
|:---:|---|---|---|
| **GB** | United Kingdom (Great Britain) | Non-EU (Third Country List A) | **Quốc gia Trụ sở chính công ty** |
| **FR** | France (Cộng hòa Pháp) | EU Member State (Schengen) | Cửa ngõ BCP cảng biển / Hầm Eurotunnel sang EU |
| **IE** | Republic of Ireland (Cộng hòa Ireland) | EU Member State (Non-Schengen) | Thủ phủ đua ngựa, BCP Cảng Dublin |
| **DE** | Germany (CHLB Đức) | EU Member State (Schengen) | Trung tâm đua ngựa Trung Âu, BCP Sân bay Frankfurt |
| **IT** | Italy (Cộng hòa Ý) | EU Member State (Schengen) | Đua ngựa Nam Âu, quản lý thú y qua cổng Vetinfo |
| **NL** | Netherlands (Hà Lan) | EU Member State (Schengen) | Trạm trung chuyển Cảng Rotterdam & Sân bay Schiphol |
| **BE** | Belgium (Vương quốc Bỉ) | EU Member State (Schengen) | Trung tâm vận tải hàng không Sân bay Liège Horse Inn |
| **ES** | Spain (Tây Ban Nha) | EU Member State (Schengen) | Điểm đến tour thi đấu mùa đông Sunshine Tour |
| **XI** | Northern Ireland (Bắc Ireland) | UK Territory (Windsor Framework) | Quy chế đặc thù Luồng Xanh / Luồng Đỏ |

---

## 📑 2. Ma Trận Giấy Tờ Theo Cặp Quốc Gia (Country-Pair Document Matrix)

> **Quy ước mã chứng từ (Document Type Code)**:
> 1. `EQUINE_PASSPORT`: Hộ chiếu ngựa (Weatherbys / FEI Card / SIRE).
> 2. `EHC_GB_TO_EU_8438`: Chứng thư kiểm dịch xuất khẩu UK sang EU (Defra Form 8438).
> 3. `EHC_EU_TO_GB`: Chứng thư kiểm dịch xuất khẩu EU sang UK (cấp trên TRACES-NT).
> 4. `INTRA_EU_HEALTH_CERT`: Chứng thư thú y nội khối EU (TRACES-NT Model `EQUI-INTRA`).
> 5. `CHED_A_DOCUMENT`: Tờ khai nhập cảnh y tế động vật BCP trên hệ thống TRACES-NT.
> 6. `IPAFFS_PRE_NOTIF`: Khai báo thông báo trước nhập khẩu vào UK trên cổng IPAFFS.
> 7. `LAB_TEST_EIA_COGGINS`: Phiếu xét nghiệm âm tính Coggins Test (EIA <= 90 ngày).
> 8. `LAB_TEST_EVA`: Phiếu xét nghiệm âm tính Viêm động mạch EVA (ngựa đực giống).
> 9. `ATA_CARNET_EXPORT`: Sổ ATA Carnet đóng dấu xuất cảnh (Yellow Voucher - Xuất khẩu tạm).
> 10. `ATA_CARNET_IMPORT`: Sổ ATA Carnet đóng dấu nhập cảnh (White Voucher - Tạm nhập).
> 11. `ATA_CARNET_REEXPORT`: Sổ ATA Carnet đóng dấu tái xuất khẩu rời khỏi nước đến (White Voucher).
> 12. `ATA_CARNET_REIMPORT`: Sổ ATA Carnet đóng dấu tái nhập khẩu về nước xuất phát (Yellow Voucher).
> 13. `CUSTOMS_DECLARATION_CDS`: Tờ khai hải quan xuất nhập khẩu UK qua hệ thống CDS.
> 14. `SMART_BORDER_PASS`: Mã vạch thông quan thông minh (Hải quan Pháp SI Brexit / Pass En Douane).
> 15. `CUSTOMS_DECLARATION_AIS`: Tờ khai hải quan nhập khẩu Ireland qua hệ thống AIS.
> 16. `ITALIAN_MODELLO_4`: Tờ khai di chuyển động vật điện tử Ý có mã QR trên hệ thống Vetinfo.
> 17. `WINDSOR_HORSE_DECLARATION`: Bản khai báo di chuyển ngựa sang Bắc Ireland (Luồng Xanh NIRMS).
> 18. `JOURNEY_LOG_EC1_2005`: Nhật ký hành trình vận chuyển động vật đường dài (> 8h).
> 19. `TRANSPORTER_AUTH_DUAL`: Cặp giấy phép vận tải Loại 2 (Cả UK APHA và EU Member State).
> 20. `DRIVER_COMPETENCE_CERT`: Chứng chỉ năng lực tài xế/người áp tải vận chuyển ngựa.
> 21. `VEHICLE_APPROVAL_CERT`: Giấy kiểm định xe tải giảm xóc khí nén và thông gió tự động.
> 22. `ANIMAL_TRANSPORT_CERT_ATC`: Giấy chứng nhận vận chuyển động vật nội địa Anh (ATC).

---

### Bảng Ma Trận Tổng Hợp (Dùng Nạp Trực Tiếp Vào Database)

| STT | Src (Nước đi) | Dest (Nước đến) | Chiều (Direction) | Phân loại tuyến (Route Type) | Cửa khẩu / Hệ thống kiểm soát | Danh Sách Giấy Tờ Bắt Buộc (List of Required Documents) |
|:---:|:---:|:---:|:---:|---|---|---|
| **01** | **GB** | **FR** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT` | BCP Cảng Calais / Coquelles Eurotunnel | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `CUSTOMS_DECLARATION_CDS`, `SMART_BORDER_PASS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **02** | **FR** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT` | Cửa khẩu Sevington IBF / Cảng Dover | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `SMART_BORDER_PASS`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **03** | **GB** | **IE** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT` | BCP Cảng Dublin (Ferry Holyhead-Dublin) | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `CUSTOMS_DECLARATION_AIS`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **04** | **IE** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT` | BCP Cảng Holyhead / Cairnryan | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **05** | **GB** | **DE** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT_TRANSIT` | BCP Calais (Vào EU) -> Transit Pháp/Bỉ -> Đức | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `SMART_BORDER_PASS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **06** | **DE** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT_TRANSIT` | Transit Bỉ/Pháp -> Cửa khẩu Calais -> Dover | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `SMART_BORDER_PASS`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **07** | **GB** | **IT** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT_TRANSIT` | BCP Calais -> Transit Pháp -> Hầm Fréjus (Ý) | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `ITALIAN_MODELLO_4`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **08** | **IT** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT_TRANSIT` | Hầm Mont Blanc -> Transit Pháp -> Dover (UK) | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `ITALIAN_MODELLO_4`, `SMART_BORDER_PASS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **09** | **GB** | **XI** | `OUTBOUND` (Lượt đi) | `DOMESTIC_UK_WINDSOR` | Cảng Belfast / Larne (Green Lane) | `EQUINE_PASSPORT`, `WINDSOR_HORSE_DECLARATION`, `ANIMAL_TRANSPORT_CERT_ATC`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **10** | **XI** | **GB** | `RETURN` (Lượt về) | `DOMESTIC_UK_WINDSOR` | Cảng Cairnryan / Liverpool | `EQUINE_PASSPORT`, `ANIMAL_TRANSPORT_CERT_ATC`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **11** | **FR** | **DE** | `ROUNDTRIP` (Hai chiều) | `INTRA_EU_MOVEMENT` | Biên giới mở Strasbourg (Không BCP, Không Thuế) | `EQUINE_PASSPORT`, `INTRA_EU_HEALTH_CERT`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **12** | **FR** | **IT** | `OUTBOUND` (Lượt đi) | `INTRA_EU_MOVEMENT` | Cửa hầm Mont Blanc / Fréjus | `EQUINE_PASSPORT`, `INTRA_EU_HEALTH_CERT`, `ITALIAN_MODELLO_4`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **13** | **IT** | **FR** | `RETURN` (Lượt về) | `INTRA_EU_MOVEMENT` | Cửa hầm Fréjus / Mont Blanc | `EQUINE_PASSPORT`, `INTRA_EU_HEALTH_CERT`, `ITALIAN_MODELLO_4`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **14** | **FR** | **IE** | `ROUNDTRIP` (Hai chiều) | `INTRA_EU_FERRY` | Tuyến phà biển trực tiếp Cherbourg - Dublin/Rosslare | `EQUINE_PASSPORT`, `INTRA_EU_HEALTH_CERT`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **15** | **GB** | **NL** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT` | BCP Cảng Hoek van Holland / Rotterdam | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **16** | **NL** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT` | Cửa khẩu Cảng Harwich / Killingholme | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **17** | **GB** | **BE** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_AIR_OR_ROAD` | BCP Sân bay Liège / BCP Cảng Zeebrugge | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **18** | **GB** | **ES** | `OUTBOUND` (Lượt đi) | `EXTRA_EU_EXPORT_TRANSIT` | BCP Calais -> Transit Pháp -> Biên giới Irun (Tây Ban Nha) | `EQUINE_PASSPORT`, `EHC_GB_TO_EU_8438`, `LAB_TEST_EIA_COGGINS`, `CHED_A_DOCUMENT`, `ATA_CARNET_EXPORT`, `ATA_CARNET_IMPORT`, `SMART_BORDER_PASS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **19** | **ES** | **GB** | `RETURN` (Lượt về) | `EXTRA_EU_IMPORT_TRANSIT` | Biên giới La Jonquera -> Transit Pháp -> Dover (UK) | `EQUINE_PASSPORT`, `EHC_EU_TO_GB`, `IPAFFS_PRE_NOTIF`, `ATA_CARNET_REEXPORT`, `ATA_CARNET_REIMPORT`, `SMART_BORDER_PASS`, `CUSTOMS_DECLARATION_CDS`, `JOURNEY_LOG_EC1_2005`, `TRANSPORTER_AUTH_DUAL`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |
| **20** | **GB** | **GB** | `DOMESTIC` (Nội địa) | `DOMESTIC_UK` | Không cửa khẩu (Lưu hành trong nước Anh/Scotland/Wales) | `EQUINE_PASSPORT`, `ANIMAL_TRANSPORT_CERT_ATC`, `DRIVER_COMPETENCE_CERT`, `VEHICLE_APPROVAL_CERT` |


