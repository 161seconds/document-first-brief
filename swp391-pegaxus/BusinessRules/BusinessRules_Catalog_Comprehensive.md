# Danh Mục Quy Tắc Nghiệp Vụ Toàn Diện (Master Business Rules Catalog)
## Cross-Border Racehorse Transport System — Specification v2.0

> **Dự án**: Hệ thống Quản lý Vận chuyển Ngựa đua Xuyên Biên giới (`swp391-cross-border-racehorse-transport-system`)  
> **Áp dụng cho**: Toàn bộ 6 Flow nghiệp vụ và 5 Tác nhân (Customer, Logistics Manager, Transport Specialist, Fleet & Route Coordinator, Vehicle Driver).  
> **Mục tiêu**: Bộ khung quy tắc nghiệp vụ bất khả xâm phạm phục vụ bảo vệ đồ án, thẩm định BDD và triển khai mã nguồn.

---

## 📑 Mục Lục Quy Tắc Nghiệp Vụ

- [Nhóm 1: BR-BOOKING (Quản lý Yêu Cầu & Đặt Chuyến)](#nhóm-1-br-booking-quản-lý-yêu-cầu--đặt-chuyến)
- [Nhóm 2: BR-LEGAL-VET (Hồ Sơ Pháp Lý, Tiêm Phòng & Kiểm Dịch)](#nhóm-2-br-legal-vet-hồ-sơ-pháp-lý-tiêm-phòng--kiểm-dịch)
- [Nhóm 3: BR-ROUTING-WELFARE (Lập Tuyến, Phúc Lợi & Nghỉ Ngơi EC 1/2005)](#nhóm-3-br-routing-welfare-lập-tuyến-phúc-lợi--nghỉ-ngơi-ec-12005)
- [Nhóm 4: BR-BORDER-CUSTOMS (Thông Quan Cửa Khẩu & BCP Hậu Brexit)](#nhóm-4-br-border-customs-thông-quan-cửa-khẩu--bcp-hậu-brexit)
- [Nhóm 5: BR-TELEMETRY-HEALTH (Giám Sát Thể Trạng & Chỉ Số Sinh Tồn)](#nhóm-5-br-telemetry-health-giám-sát-thể-trạng--chỉ-số-sinh-tồn)
- [Nhóm 6: BR-INCIDENT-SOS (Xử Lý Sự Cố Hiện Trường & Tái Lập Lộ Trình)](#nhóm-6-br-incident-sos-xử-lý-sự-cố-hiện-trường--tái-lập-lộ-trình)
- [Nhóm 7: BR-HANDOVER-CLAIMS (Bàn Giao Đích, e-POD & Khiếu Nại Bồi Thường)](#nhóm-7-br-handover-claims-bàn-giao-đích-e-pod--khiếu-nại-bồi-thường)

---

## Nhóm 1: BR-BOOKING (Quản Lý Yêu Cầu & Đặt Chuyến)

### BR-BOOKING-01: Giới hạn Thời gian Đặt chuyến Tối thiểu (Lead Time)
- **Nội dung**: Yêu cầu vận chuyển phải được tạo trước thời điểm khởi hành mong muốn tối thiểu:
  - Vận chuyển nội địa (GB to GB): Tối thiểu **72 giờ (3 ngày)**.
  - Vận chuyển xuyên biên giới (UK sang EU hoặc EU sang UK): Tối thiểu **14 ngày** (để đủ thời gian thực hiện xét nghiệm Coggins, đăng ký sổ ATA Carnet và nộp thông báo BCP).
- **Ngoại lệ**: Các chuyến đi khẩn cấp thay thế ngựa đua bị chấn thương trước giải đấu chỉ được chấp nhận nếu cá thể ngựa thay thế đã có sẵn kết quả xét nghiệm Coggins âm tính còn hạn và hộ chiếu hợp lệ.

### BR-BOOKING-02: Giới hạn Sức chứa Phương tiện (Capacity Constraint)
- Mỗi xe tải chuyên dụng (Horse Truck) chỉ được chở tối đa từ **2 đến 9 cá thể ngựa** tùy theo phân hạng xe được phê duyệt trong `VehicleApprovalCertificate`.
- Tuyệt đối không xếp chung ngựa đực giống (*Stallions*) cạnh ngựa cái (*Mares*) mà không có vách ngăn kiên cố cách ly tối thiểu 1 ô chuồng trống hoặc chuồng ngăn cách âm.

### BR-BOOKING-03: Điều kiện Chỉnh sửa & Hủy Yêu cầu
- Khách hàng chỉ được chỉnh sửa hoặc hủy yêu cầu khi trạng thái đơn ở mức `PENDING_APPROVAL` (Chờ duyệt) hoặc `APPROVED` nhưng chưa khởi tạo kế hoạch hành trình (`TripPlan` chưa ở trạng thái `ACTIVE`).
- Nếu khách hàng hủy chuyến trong vòng dưới 7 ngày trước khởi hành, áp dụng phí phạt hủy chuyến 30% giá trị hợp đồng để bù đắp chi phí đặt cọc trạm dừng chân và phí cấp chứng thư thú y đã xuất bản.

---

## Nhóm 2: BR-LEGAL-VET (Hồ Sơ Pháp Lý, Tiêm Phòng & Kiểm Dịch)

### BR-LEGAL-VET-01: Hiệu lực Vaccine Cúm Ngựa (Equine Influenza)
- Toàn bộ ngựa tham gia vận chuyển bắt buộc phải có chứng thực tiêm phòng vaccine Cúm ngựa trong Hộ chiếu:
  - Phải hoàn thành phác đồ cơ bản (Primary course: Mũi 1 và Mũi 2 cách nhau 21 - 60 ngày).
  - Mũi tiêm nhắc lại (Booster) phải được tiêm trong vòng **tối đa 180 ngày (6 tháng) đến 365 ngày (12 tháng)** trước ngày khởi hành tùy theo quy định giải đua.
  - **Quy tắc 7 ngày**: Tuyệt đối không vận chuyển ngựa vừa được tiêm vaccine trong vòng **7 ngày** trước ngày khởi hành (tránh tác dụng phụ sốt nhẹ và ức chế miễn dịch).

### BR-LEGAL-VET-02: Thời hạn Hiệu lực của Xét nghiệm Coggins Test (EIA)
- Phiếu xét nghiệm âm tính với Bệnh Thiếu máu truyền nhiễm ngựa (Equine Infectious Anaemia - EIA) bắt buộc phải được cấp bởi phòng thí nghiệm đạt chuẩn ISO 17025:
  - Có giá trị tối đa **90 ngày** đối với ngựa đua đã đăng ký (Registered Equidae) di chuyển giữa UK và Pháp/Ireland.
  - Có giá trị tối đa **30 ngày** đối với một số quốc gia áp dụng kiểm dịch nghiêm ngặt hoặc vùng có nguy cơ dịch tễ.

### BR-LEGAL-VET-03: Chứng thư Kiểm dịch Xuất khẩu (EHC Form 8438)
- Bác sĩ Thú y Chính thức (OV) chỉ được phép ký phát hành EHC trong vòng tối đa **48 giờ** trước thời điểm ngựa bốc lên phương tiện.
- Chứng thư EHC có giá trị hiệu lực tối đa **10 ngày** kể từ ngày ký cho đến khi nhập cảnh qua trạm BCP đích.

### BR-LEGAL-VET-04: Điều kiện Khai báo CHED-A trên TRACES-NT
- Tờ khai CHED-A bắt buộc phải được gửi lên hệ thống TRACES-NT tối thiểu **24 giờ** trước khi phương tiện cập cảng hoặc tới cổng trạm kiểm soát BCP biên giới.
- Nếu không có mã số CHED-A hợp lệ, hệ thống tự động khóa trạng thái không cho phép xuất bản lệnh khởi hành (*Departure Lock*).

---

## Nhóm 3: BR-ROUTING-WELFARE (Lập Tuyến, Phúc Lợi & Nghỉ Ngơi EC 1/2005)

### BR-ROUTING-WELFARE-01: Giới hạn Thời gian Lái xe & Dừng xe Uống nước
- Thời gian xe chạy liên tục không được vượt quá **8 giờ**.
- Sau mỗi 4 đến 4.5 giờ xe chạy, bắt buộc phải có mốc dừng nghỉ tối thiểu **45 phút** tại trạm dừng để kiểm tra thông gió, cho ngựa uống nước và kiểm tra dây buộc an toàn (không hạ tải ngựa).

### BR-ROUTING-WELFARE-02: Quy tắc Dừng nghỉ Trọn vẹn 24 Giờ (24-Hour Control Post Rest)
- Nếu tổng hành trình vượt quá **24 giờ** (tính cả thời gian phà biển và làm thủ tục BCP), phương tiện bắt buộc phải dừng tại **Trạm Kiểm soát Thú y Phê duyệt (Approved Control Post)**.
- Toàn bộ ngựa phải được hạ tải, đưa vào chuồng đơn có đệm rơm, được cho ăn cỏ khô và nghỉ ngơi trọn vẹn tối thiểu **24 giờ liên tục** trước khi được phép bốc lên xe tiếp tục chặng 2.

### BR-ROUTING-WELFARE-03: Dải Nhiệt độ Thùng xe An toàn
- Cảm biến nhiệt độ thùng xe phải luôn duy trì trong khoảng **10°C đến 25°C**.
- Nếu nhiệt độ thùng xe duy trì trên **30°C** hoặc dưới **5°C** liên tục quá 15 phút, hệ thống tự động phát cảnh báo âm thanh cấp độ vàng trong cabin tài xế và gửi thông báo khẩn về trung tâm điều hành.

---

## Nhóm 4: BR-BORDER-CUSTOMS (Thông Quan Cửa Khẩu & BCP Hậu Brexit)

### BR-BORDER-CUSTOMS-01: Quy định Giấy phép Vận chuyển Kép (Dual Authorisation Rule)
- Mọi chuyến xe xuất phát từ Anh sang EU hoặc ngược lại bắt buộc phải có:
  1. Giấy phép Vận tải Loại 2 do Vương quốc Anh cấp (`UK Transporter Authorisation Type 2`).
  2. Giấy phép Vận tải Loại 2 do một Nước Thành viên EU cấp (`EU Transporter Authorisation Type 2`).
- Nếu xe hoặc tài xế chỉ có giấy phép đơn quốc gia, hệ thống từ chối gán chuyến xuyên biên giới.

### BR-BORDER-CUSTOMS-02: Khai báo Ghép cặp Hải quan Thông minh (SI Brexit Pairing)
- Trước khi xe đến Cảng Dover hoặc Nhà ga Eurotunnel Folkestone, tài xế/chuyên viên thủ tục bắt buộc phải hoàn thành ghép cặp mã vạch Sổ ATA Carnet (hoặc mã tờ khai hải quan CDS/Delta-G) với biển số xe trên hệ thống `Pass En Douane / SI Brexit`.
- Xe tải không có mã vạch Logistics Envelope hợp lệ sẽ bị cảnh sát cảng từ chối cho vào làn xe ưu tiên.

### BR-BORDER-CUSTOMS-03: Thời hạn Tạm nhập Tái xuất của Sổ ATA Carnet
- Thời hạn hiệu lực của Sổ ATA Carnet tối đa là **12 tháng** kể từ ngày cấp.
- Ngựa tạm nhập vào EU để thi đấu bắt buộc phải tái xuất khẩu về Anh (hoặc ngược lại) trước ngày hết hạn in trên bìa sổ; tuyệt đối không để quá hạn dẫn đến việc hải quan nước sở tại phạt tiền và truy thu thuế nhập khẩu 20% giá trị ngựa.

---

## Nhóm 5: BR-TELEMETRY-HEALTH (Giám Sát Thể Trạng & Chỉ Số Sinh Tồn)

### BR-TELEMETRY-HEALTH-01: Định kỳ Báo cáo Thể trạng Hiện trường
- Tài xế / Người đi kèm bắt buộc phải đo thân nhiệt và cập nhật nhật ký sức khỏe trên ứng dụng di động:
  - Tối thiểu **1 lần sau mỗi 4 đến 6 giờ** (tại các điểm dừng nghỉ).
  - Bắt buộc chụp ảnh hiện trường có gắn thẻ thời gian và tọa độ GPS (Geotagged Photo).

### BR-TELEMETRY-HEALTH-02: Ngưỡng Báo động Đỏ Sốt & Mất nước
- Hệ thống tự động kích hoạt trạng thái `HEALTH_CRITICAL` và cảnh báo SOS khi ghi nhận:
  - Thân nhiệt ngựa vượt quá **38.9°C** (nguy cơ sốt vận chuyển *Shipping Fever*).
  - Hoặc thời gian hồi phục mao mạch (CRT) vượt quá **3 giây** kèm dấu hiệu nếp gấp da không đàn hồi (mất nước cấp tính > 8%).
  - Hoặc ngựa từ chối uống nước liên tục qua **2 chặng dừng nghỉ (trên 8 giờ)**.

---

## Nhóm 6: BR-INCIDENT-SOS (Xử Lý Sự Cố Hiện Trường & Tái Lập Lộ Trình)

### BR-INCIDENT-SOS-01: Thẩm quyền Quyết định Chuyển viện & Cấp cứu Thú y
- Khi phát sinh sự cố y tế khẩn cấp dọc đường:
  - Bác sĩ Thú y lưu động thuộc mạng lưới khẩn cấp có quyền quyết định đưa ngựa vào bệnh viện thú y gần nhất.
  - Tài xế không được tự ý tiếp tục hành trình nếu chưa có sự chấp thuận bằng văn bản điện tử của Bác sĩ Thú y qua ứng dụng.

### BR-INCIDENT-SOS-02: Quy định Sang xe Dự phòng (Cross-Docking Rule)
- Khi xe tải chuyên dụng gặp sự cố cơ học không thể khắc phục trong vòng **2 giờ** giữa điều kiện thời tiết khắc nghiệt:
  - Điều phối viên bắt buộc phải điều động xe tải chuyên dụng dự phòng đạt chuẩn tương đương đến hiện trường.
  - Việc chuyển ngựa sang xe mới phải diễn ra tại khu vực an toàn (bãi đỗ trạm xăng hoặc làn dừng khẩn cấp có cảnh sát bảo vệ), có thảm chống trượt và nhân viên đi kèm dẫn dắt từng con.

### BR-INCIDENT-SOS-03: Thẩm quyền Phê duyệt Thay đổi Lộ trình (Detour Approval)
- Tài xế được quyền chủ động chọn đường tránh tạm thời nếu quãng đường lệch dưới **50 km** và thời gian trễ dưới **1 giờ**.
- Mọi thay đổi lộ trình làm phát sinh thêm trạm dừng nghỉ 24h hoặc đổi cửa khẩu BCP (ví dụ từ Calais sang Dunkirk) bắt buộc phải được `Logistics Manager` phê duyệt trên hệ thống để điều chỉnh lại chứng từ kiểm dịch.

---

## Nhóm 7: BR-HANDOVER-CLAIMS (Bàn Giao Đích, e-POD & Khiếu Nại Bồi Thường)

### BR-HANDOVER-CLAIMS-01: Điều kiện Ký Biên bản Giao nhận Điện tử (e-POD)
- Biên bản bàn giao điện tử e-POD chỉ có giá trị pháp lý khi hội đủ 3 điều kiện:
  1. Chữ ký tay điện tử trực tiếp của Đại diện Khách hàng / Quản lý trường đua nhận ngựa.
  2. Tọa độ định vị GPS của thiết bị ký trùng khớp với địa chỉ chuồng đích trong bán kính **500 mét**.
  3. Đính kèm ảnh chụp tình trạng thể trạng ngựa khi hạ tải và ảnh chụp Section 3 Journey Log đã ký.

### BR-HANDOVER-CLAIMS-02: Thời hạn Khiếu nại Chấn thương (24-Hour Claim Window)
- Thời hạn khách hàng được quyền mở phiếu khiếu nại bồi thường chấn thương là tối đa **24 giờ** kể từ thời điểm ký e-POD.
- Sau 24 giờ, hệ thống tự động khóa tính năng khiếu nại vận chuyển và chuyển trạng thái chuyến đi thành `COMPLETED_CLOSED`.

### BR-HANDOVER-CLAIMS-03: Nghĩa vụ Chứng minh Khiếu nại (Burden of Proof)
- Mọi khiếu nại yêu cầu bồi thường bảo hiểm bắt buộc phải có:
  - Biên bản giám định độc lập của Bác sĩ Thú y có chứng chỉ hành nghề trong vòng 12h sau bàn giao.
  - Trích xuất dữ liệu cảm biến va đập (G-Force sensor) hoặc cảm biến nhiệt độ từ thùng xe để chứng minh lỗi thuộc về quy trình vận tải của nhà xe.
