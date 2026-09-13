# Danh Mục Use Cases — Hệ Thống Quản Lý Vận Chuyển Ngựa Đua Xuyên Quốc Gia
## Cross-Border Racehorse Transport System (`PasoFino` / `swp391-pegaxus`)

> **Mã đề tài / Phụ trách**: `2 — HoangNT20`  
> **Dự án**: Cross-Border Racehorse Transport System (PasoFino)  
> **Môn học**: SWP391 (Software Project)  
> **Phương pháp tiếp cận**: Document-First System Specification (BDD Acceptance Criteria)  
> **Cập nhật gần nhất**: 2026-09-14 (Thống nhất Scope & 6 Flow Nghiệp Vụ Chính Thức)  

---

## 👥 Ma Trận 6 Nhóm Tác Nhân (Actors & Responsibilities)

| Tác nhân (Actor) | Tên tiếng Việt | Mã hệ thống | Phạm vi trách nhiệm cốt lõi |
|---|---|---|---|
| **Customer** | Khách hàng (Chủ ngựa / CLB đua) | `ROLE_CUSTOMER` | Đăng ký/đăng nhập, tạo yêu cầu vận chuyển, tải hồ sơ y tế/hộ chiếu ngựa, theo dõi hành trình realtime, xem thông tin người duyệt & audit log, xem/thanh toán hóa đơn, ký e-POD và gửi claim. |
| **Logistics Manager** | Quản lý Điều hành Logistics | `ROLE_LOGISTICS_MANAGER` | Tiếp nhận & phê duyệt đơn hàng; lập kế hoạch tổng thể; phân công nhân sự; quản lý đối tác vận tải ngoài; duyệt lộ trình; duyệt chi phí khẩn cấp; lập/phát hành hóa đơn; quản lý thu chi và xem báo cáo KPI. |
| **Transport Specialist** | Chuyên viên Thủ tục & Kiểm dịch | `ROLE_TRANSPORT_SPECIALIST` | Quản lý quy định pháp lý/kiểm dịch; quản lý & áp dụng bộ hồ sơ mẫu; thẩm tra giấy tờ; nộp hồ sơ tới cơ quan chức năng bên ngoài thủ công; tiếp nhận và đánh dấu kết quả kiểm dịch/thông quan. |
| **Fleet & Route Coordinator** | Điều phối viên Đội xe & Lộ trình | `ROLE_FLEET_COORDINATOR` | Quản lý phương tiện chuyên dụng; lập lộ trình thủ công (đường bộ kết hợp phà biển); quản lý lộ trình dự phòng; theo dõi tiến độ chặng; tái lập tuyến tránh sự cố; đề xuất chi phí phát sinh. |
| **Vehicle Driver** | Tài xế Phương tiện Chuyên dụng | `ROLE_VEHICLE_DRIVER` | Tiếp nhận phương tiện & lộ trình; cập nhật mốc tiến độ chặng thủ công song song Realtime GPS; báo cáo sự cố xe/giao thông khẩn cấp; ghi nhận chi phí; thực hiện bàn giao dắt ngựa tại đích. Là nhân viên công ty trên xe nhà. |
| **Escort** | Người Đi Cùng / Chăm Sóc & Áp Tải (Groom) | `ROLE_ESCORT` | Chăm sóc ngựa trên đường; kiểm tra thể trạng, đo thân nhiệt, ghi nhật ký sức khỏe & phúc lợi; chụp ảnh hiện trường; gửi báo cáo khẩn cấp y tế thú y; phối hợp bàn giao tại đích. Là nhân viên công ty trên xe nhà. |
| **System** | Hệ thống Tự Động | `SYSTEM` | Tính toán cước phí tự động, gửi thông báo OTP/Email/SMS, đồng bộ cảnh báo GPS, tổng hợp dòng chi phí booking, tự động chuyển trạng thái hóa đơn khi thanh toán. |

---

## 📌 Danh Sách 12 Giả Định Hệ Thống Đã Thống Nhất (Official Project Assumptions)

1. **Vận chuyển đồng thời nhiều con ngựa**: Một chuyến vận chuyển có thể vận chuyển đồng thời nhiều con ngựa của một hoặc nhiều chủ sở hữu.
2. **Đa phương thức vận chuyển**: Ngựa có thể vận chuyển bằng đường bộ (xe tải chuyên dụng giảm xóc khí nén) và đường thủy (phà biển chuyên dụng).
3. **Phân hóa hồ sơ hoàn thành**: Mỗi con ngựa và mỗi quốc gia sẽ áp dụng loại hồ sơ hoàn thành và danh mục kiểm dịch khác nhau.
4. **Minh bạch phê duyệt & Audit Log**: Customer có thể xem thông tin người đã phê duyệt Order và xem toàn bộ Log thao tác của đơn hàng.
5. **Tái sử dụng bộ giấy tờ mẫu**: Tồn tại các bộ giấy tờ mẫu (`DossierTemplate`) được chuẩn hóa theo tuyến đường và tái sử dụng nhiều lần.
6. **Xử lý thủ công với cơ quan chức năng (Zero-Access for Customs/Gov)**: TS phụ trách nộp hồ sơ tới cơ quan chức năng, tiếp nhận kết quả và đánh dấu lên hệ thống (giả định cơ quan chức năng không cung cấp dữ liệu hay nền tảng số để đồng bộ tự động). **Cơ quan Hải quan và Chính quyền nhà nước tuyệt đối KHÔNG có tài khoản và KHÔNG được truy cập vào hệ thống**.
7. **Lập kế hoạch lộ trình thủ công**: Kế hoạch chuyến đi được lập thủ công bởi Điều phối viên (`Fleet & Route Coordinator`).
8. **Quản lý 2 loại hình phương tiện**: Phương tiện thuộc sở hữu của công ty (dùng vận chuyển trong nước và nơi có chi nhánh) và phương tiện thuê hoặc mua vé ngoài (phà biển/xe thuê ngoài).
9. **Cập nhật tiến độ song song Realtime**: Tài xế cập nhật tình hình chuyến đi và báo cáo khẩn cấp; thông tin này tồn tại song song với dữ liệu Realtime GPS.
10. **Escort giám sát sức khỏe & cấp cứu y tế**: Người đi cùng cập nhật tình hình sức khỏe của ngựa và gửi thông báo khẩn cấp khi phát sinh vấn đề thú y.
11. **Phân định nhân sự theo phương tiện**: Tài xế và Người đi cùng là nhân viên công ty (áp dụng với phương tiện của công ty); đối với phương tiện thuê ngoài chỉ theo dõi qua Realtime.
12. **Tách biệt Driver và Escort**: Chốt tách Driver và Escort thành 2 Actor riêng biệt.

---

## 🚫 Giới Hạn Phạm Vi Dự Án (Official Out of Scope)

1. **KHÔNG Native Mobile App**: Hệ thống là 100% Web Application (Responsive Web cho Desktop & Mobile Browser). Không phát triển app native iOS/Android.
2. **KHÔNG ứng dụng AI**: Không dùng AI tự động tối ưu hóa lộ trình, không dùng AI nhận diện bệnh lý qua ảnh, không dùng OCR tự động bóc tách giấy tờ. Lộ trình và kiểm duyệt giấy tờ do con người (Coordinator, Specialist) thao tác thủ công.
3. **KHÔNG phần cứng IoT**: Không tích hợp cảm biến đeo cổ ngựa, vòng sinh trắc học, hay thiết bị phần cứng vi điều khiển (Arduino/Raspberry Pi) trên thùng xe. Nhật ký sức khỏe ngựa (thân nhiệt, ăn uống) do Escort đo thủ công và nhập lên web form.
4. **KHÔNG tài khoản / API Hải quan & Gov**: Cơ quan nhà nước không vào hệ thống; mọi thủ tục do TS nộp ngoài và đánh dấu thủ công.
5. **KHÔNG Số điện thoại & SMS**: Không dùng SĐT để đăng ký/đăng nhập/khôi phục mật khẩu. Không gửi SMS. 100% qua Email và thông báo Web nội bộ.

---

# Flow 1 - Quản lý Tài khoản và Yêu cầu Vận chuyển

## Danh sách Use Case (15 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Đăng ký tài khoản khách hàng | Cho phép người dùng bên ngoài (Chủ ngựa, Câu lạc bộ) cung cấp thông tin (Họ tên, Email, Mật khẩu, Tên CLB) để tạo tài khoản mới. Hệ thống kiểm tra trùng lặp, gửi mã xác thực (OTP/Link) và khởi tạo hồ sơ Customer trên hệ thống. (Lưu ý: Nhân sự nội bộ không tự đăng ký mà sẽ được cấp tài khoản). | Customer |
| 02 | Đăng nhập và phân quyền | Cho phép người dùng nhập thông tin xác thực (Email và Mật khẩu). Hệ thống kiểm tra dữ liệu, cấp phiên làm việc (token) và điều hướng giao diện (phân quyền) tương ứng với role của người dùng (Customer, Manager, Specialist, Coordinator, Driver, Escort). | Tất cả các Actor (Customer, Logistics Manager, Transport Specialist, Fleet & Route Coordinator, Vehicle Driver, Escort) |
| 03 | Quên / Khôi phục mật khẩu | Cho phép người dùng yêu cầu khôi phục quyền truy cập khi quên mật khẩu. Người dùng nhập Email, hệ thống gửi mã xác thực (OTP) qua email. Sau khi xác thực thành công, cho phép người dùng đặt lại mật khẩu mới. | Tất cả các Actor |
| 04 | Thay đổi mật khẩu | Cho phép người dùng đang trong phiên đăng nhập (đã xác thực) đổi mật khẩu tài khoản để tăng tính bảo mật. Người dùng cần nhập đúng mật khẩu cũ và xác nhận mật khẩu mới. | Tất cả các Actor |
| 05 | Cập nhật hồ sơ cá nhân (Profile) | Cho phép người dùng xem và chỉnh sửa các thông tin cá nhân cơ bản (Ảnh đại diện, Địa chỉ liên hệ). Hệ thống lưu vết chỉnh sửa và cập nhật dữ liệu mới nhất. | Tất cả các Actor |
| 06 | Đăng xuất hệ thống | Cho phép người dùng chủ động chấm dứt phiên làm việc hiện tại. Hệ thống tiến hành xóa token, hủy quyền truy cập và điều hướng người dùng về lại màn hình Đăng nhập. | Tất cả các Actor |
| 07 | Tạo yêu cầu vận chuyển mới | Cho phép Customer khởi tạo một yêu cầu vận chuyển ngựa. Customer cần nhập đầy đủ thông tin: điểm đi, điểm đến, và thời gian mong muốn khởi hành. Hệ thống tạo mã yêu cầu, gắn trạng thái mặc định là "Chờ duyệt" và tự động ghi nhận thời điểm tạo yêu cầu. | Customer |
| 08 | Quản lý danh sách ngựa và yêu cầu đặc biệt của riêng khách hàng | Cho phép Customer thêm, xóa hoặc chỉnh sửa danh sách những con ngựa sẽ tham gia vào chuyến vận chuyển. Đồng thời, Customer có thể thiết lập các yêu cầu đặc biệt cho từng con ngựa hoặc toàn chuyến (ví dụ: loại xe chuyên dụng, chế độ ăn, hoặc yêu cầu người đi kèm). Hệ thống tự động tính toán tổng số lượng ngựa trong yêu cầu. | Customer |
| 09 | Cập nhật thông tin yêu cầu | Cho phép Customer thay đổi thông tin của yêu cầu (điểm đi, điểm đến, thời gian, danh sách ngựa, yêu cầu đặc biệt) với điều kiện yêu cầu đó vẫn đang ở trạng thái "Chờ duyệt". Hệ thống sẽ lưu lại thời điểm chỉnh sửa và cập nhật phiên bản dữ liệu mới nhất. | Customer |
| 10 | Hủy yêu cầu vận chuyển | Cho phép Customer chủ động hủy bỏ một yêu cầu vận chuyển khi hệ thống chưa xử lý. Customer bắt buộc phải nhập lý do hủy. Hệ thống sẽ chuyển trạng thái sang "Hủy", ghi nhận lý do và thời điểm thực hiện thao tác vào lịch sử yêu cầu. | Customer |
| 11 | Xem danh sách yêu cầu vận chuyển | Cho phép người dùng xem danh sách các yêu cầu vận chuyển với các thông tin cơ bản. Customer chỉ xem được các yêu cầu do chính mình tạo. Logistics Manager được xem toàn bộ danh sách trên hệ thống để phục vụ việc phân loại, theo dõi và xử lý. | Customer, Logistics Manager |
| 12 | Xem chi tiết yêu cầu vận chuyển | Cho phép người dùng truy xuất toàn bộ thông tin chi tiết của một yêu cầu cụ thể. Dữ liệu hiển thị có thể bao gồm: thông tin khách hàng đặt, lịch trình mong muốn, tổng số lượng ngựa, chi tiết từng con ngựa, các yêu cầu đặc biệt, trạng thái hiện tại và người phê duyệt (nếu có). | Customer, Logistics Manager |
| 13 | Phê duyệt yêu cầu vận chuyển | Cho phép Logistics Manager kiểm tra tính khả thi của yêu cầu và tiến hành phê duyệt. Khi thao tác, hệ thống chuyển trạng thái yêu cầu từ "Chờ duyệt" sang "Đã duyệt", đồng thời lưu trữ thông tin định danh của người phê duyệt và thời điểm thực hiện phê duyệt. | Logistics Manager |
| 14 | Từ chối yêu cầu vận chuyển | Cho phép Logistics Manager từ chối tiếp nhận một yêu cầu vận chuyển nếu không đủ điều kiện. Logistics Manager bắt buộc phải nhập lý do từ chối để phản hồi. Trạng thái yêu cầu chuyển thành "Từ chối", hệ thống lưu lại thông tin người thao tác, lý do cụ thể và thời điểm thực hiện. | Logistics Manager |
| 15 | Xem lịch sử thay đổi trạng thái | Cho phép xem và theo dõi toàn bộ tiến trình trạng thái của một yêu cầu (Khởi tạo -> Chờ duyệt -> Đã duyệt). Hệ thống ghi nhận và hiển thị chi tiết mỗi mốc chuyển trạng thái bao gồm: thời điểm thay đổi, thao tác được thực hiện và người thực hiện, đảm bảo khả năng truy vết dữ liệu rành mạch. | Customer, Logistics Manager |

### Entity ERD Đề Xuất (Flow 1)
- `User`: `id`, `fullName`, `email`, `passwordHash`, `role`, `status`, `createdAt`, `updatedAt`
- `CustomerProfile`: `id`, `userId`, `clubName`, `avatarUrl`, `contactAddress`, `taxCode`, `createdAt`, `updatedAt`
- `TransportRequest`: `id`, `requestCode`, `customerId`, `departureLocation`, `destinationLocation`, `requestedDepartureTime`, `totalHorses`, `currentStatus`, `approvedBy`, `approvedAt`, `rejectionReason`, `cancellationReason`, `createdAt`, `updatedAt`
- `RequestHorseItem`: `id`, `requestId`, `horseName`, `breed`, `gender`, `age`, `microchipNumber`, `dietaryRequirements`, `stallPreference`, `specialInstructions`
- `RequestStatusHistory`: `id`, `requestId`, `fromStatus`, `toStatus`, `action`, `reason`, `performedBy`, `performedAt`
- `OtpVerification`: `id`, `userId`, `email`, `otpCode`, `otpType`, `expiresAt`, `isUsed`, `createdAt`

---

# Flow 2 - Quản lý Hồ sơ Pháp lý & Kiểm dịch Thông quan

## Danh sách Use Case (16 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Quản lý danh mục quy định | Cho phép quản lý danh mục các quy định pháp lý, kiểm dịch, hải quan và các loại giấy tờ bắt buộc theo từng quốc gia, cửa khẩu hoặc tuyến vận chuyển. Transport Specialist có thể thêm, chỉnh sửa, xem và xóa các danh mục. | Transport Specialist |
| 02 | Quản lý các bộ hồ sơ mẫu | Cho phép Transport Specialist quản lý các bộ hồ sơ mẫu tương ứng với từng trường hợp vận chuyển, trong đó xác định sẵn các loại giấy tờ bắt buộc. Ví dụ, bộ hồ sơ trong nước gồm giấy kiểm dịch và giấy tờ nguồn gốc; bộ hồ sơ xuyên biên giới gồm hộ chiếu ngựa, giấy kiểm dịch, giấy tờ hải quan và giấy tờ nguồn gốc. Transport Specialist có thể tạo, xem, cập nhật, kích hoạt/ngừng sử dụng và xóa các bộ hồ sơ mẫu. | Transport Specialist |
| 03 | Áp dụng bộ hồ sơ mẫu cho chuyến vận chuyển | Cho phép Transport Specialist lựa chọn và áp dụng một bộ hồ sơ mẫu phù hợp cho chuyến vận chuyển. Hệ thống tự động tạo danh sách các giấy tờ cần có của chuyến dựa trên bộ hồ sơ được chọn và đánh dấu các giấy tờ trong bộ là bắt buộc. | Transport Specialist |
| 04 | Quản lý bộ hồ sơ pháp lý của tuyến vận chuyển | Cho phép Transport Specialist quản lý các yêu cầu hồ sơ pháp lý áp dụng cho từng tuyến vận chuyển. Hệ thống lưu các yêu cầu giấy tờ đặc thù của tuyến để có thể tự động bổ sung vào hồ sơ khi chuyến vận chuyển sử dụng tuyến đó. TS có thể thêm vào các giấy tờ phát sinh, yêu cầu đặc thù của chuyến, chỉnh sửa hoặc xóa các giấy tờ không nằm trong bộ mẫu bắt buộc. | Transport Specialist / System |
| 05 | Xem bộ hồ sơ pháp lý của chuyến vận chuyển | Cho phép người dùng xem tình trạng tổng thể của bộ hồ sơ pháp lý, bao gồm các giấy tờ đã có, còn thiếu, đang chờ kiểm tra, cần bổ sung, đã được xác nhận và tình trạng xét duyệt từ cơ quan chức năng. Quyền xem chi tiết được giới hạn theo từng actor. | Transport Specialist, Logistics Manager, Customer |
| 06 | Tải lên giấy tờ pháp lý / y tế | Cho phép Customer tải lên các giấy tờ được yêu cầu cho từng con ngựa hoặc cho chuyến vận chuyển, ví dụ hộ chiếu ngựa, giấy chứng nhận sức khỏe, chứng nhận tiêm phòng hoặc giấy tờ sở hữu. Customer có thể cung cấp thêm thông tin như số giấy tờ, ngày cấp, ngày hết hạn và cơ quan cấp. | Customer |
| 07 | Cập nhật hoặc thay thế giấy tờ đã tải lên | Cho phép Customer cung cấp phiên bản mới của một giấy tờ khi giấy tờ cũ bị sai, hết hạn, không hợp lệ hoặc được yêu cầu bổ sung lại. Hệ thống giữ lại các phiên bản cũ để đảm bảo khả năng truy vết lịch sử hồ sơ. | Customer |
| 08 | Xác nhận giấy tờ hợp lệ | Cho phép Transport Specialist xác nhận một giấy tờ là hợp lệ sau khi đã kiểm tra đầy đủ và đáp ứng yêu cầu. Sau khi được xác nhận, giấy tờ được tính là hoàn thành trong checklist hồ sơ của chuyến vận chuyển. | Transport Specialist |
| 09 | Yêu cầu chỉnh sửa / cung cấp lại giấy tờ | Cho phép Transport Specialist yêu cầu Customer cung cấp lại một giấy tờ đã tải lên nhưng không hợp lệ, chẳng hạn như bị hết hạn, thiếu chữ ký, sai thông tin, file không rõ hoặc không đúng đối tượng. Transport Specialist phải ghi rõ lý do để Customer biết cần chỉnh sửa gì. | Transport Specialist |
| 10 | Yêu cầu bổ sung giấy tờ còn thiếu | Cho phép Transport Specialist xác định các giấy tờ bắt buộc mà Customer chưa cung cấp và gửi yêu cầu bổ sung. Yêu cầu có thể bao gồm danh sách giấy tờ còn thiếu, thời hạn cần bổ sung và nội dung hướng dẫn cho Customer. | Transport Specialist |
| 11 | Ghi nhận nộp bộ hồ sơ tới cơ quan chức năng | Cho phép Transport Specialist ghi nhận việc bộ hồ sơ đã được gửi tới cơ quan kiểm dịch, hải quan hoặc cơ quan quản lý có thẩm quyền bên ngoài hệ thống. Có thể lưu ngày nộp, mã tham chiếu hồ sơ, cơ quan tiếp nhận và file xác nhận nộp hồ sơ. | Transport Specialist |
| 12 | Cập nhật kết quả xét duyệt hồ sơ | Cho phép Transport Specialist cập nhật trạng thái xét duyệt của hồ sơ từ cơ quan chức năng, ví dụ đã nộp, đang xét duyệt, được phê duyệt hoặc bị từ chối. Nếu bị từ chối, hệ thống lưu lý do và yêu cầu chỉnh sửa để tiếp tục xử lý. | Transport Specialist |
| 13 | Ghi nhận kết quả kiểm dịch | Cho phép Transport Specialist ghi nhận kết quả kiểm tra kiểm dịch tại trạm hoặc cửa khẩu trong quá trình vận chuyển, ví dụ đạt yêu cầu, không đạt hoặc cần kiểm tra lại. Nếu phát sinh vấn đề, có thể lưu lý do, con ngựa bị ảnh hưởng và hướng xử lý tiếp theo. | Transport Specialist |
| 14 | Ghi nhận kết quả thông quan | Cho phép Transport Specialist cập nhật tình trạng xử lý hải quan của chuyến vận chuyển tại từng cửa khẩu, bao gồm đang chờ, đang xử lý, đã thông quan, bị giữ hoặc bị từ chối. Khi thông quan thành công, chuyến có thể tiếp tục sang chặng tiếp theo. | Transport Specialist |
| 15 | Theo dõi tiến độ hoàn thiện hồ sơ | Cho phép Transport Specialist và Logistics Manager theo dõi tổng quan mức độ hoàn thiện hồ sơ của nhiều chuyến vận chuyển, phát hiện các chuyến đang thiếu giấy tờ, có giấy tờ bị từ chối, hồ sơ sắp tới ngày khởi hành hoặc đang chờ cơ quan chức năng xét duyệt. | Transport Specialist, Logistics Manager |
| 16 | Xem lịch sử xử lý hồ sơ | Cho phép xem toàn bộ lịch sử thao tác và thay đổi trạng thái của bộ hồ sơ, bao gồm ai đã tải giấy tờ, ai kiểm tra, thời điểm xác nhận, yêu cầu bổ sung, nộp hồ sơ và cập nhật kết quả xét duyệt. Mục đích là đảm bảo khả năng truy vết và kiểm soát quá trình xử lý hồ sơ. | Transport Specialist, Logistics Manager |

### Entity ERD Đề Xuất (Flow 2)
- `RegulatoryRequirement`: `id`, `countryCode`, `checkpointId`, `routeId`, `category`, `documentType`, `title`, `description`, `isMandatory`, `validityPeriodDays`, `createdAt`, `updatedAt`
- `DossierTemplate`: `id`, `templateCode`, `templateName`, `scope`, `description`, `isActive`, `createdAt`, `updatedAt`
- `DossierTemplateItem`: `id`, `templateId`, `documentType`, `documentName`, `isMandatory`, `targetScope`, `instructions`
- `TripLegalDossier`: `id`, `tripPlanId`, `templateId`, `overallStatus`, `clearedForDeparture`, `clearedAt`, `clearedBy`, `createdAt`, `updatedAt`
- `DossierDocumentItem`: `id`, `dossierId`, `horseId`, `documentType`, `documentName`, `documentNumber`, `issuingAuthority`, `issueDate`, `expiryDate`, `currentFileUrl`, `currentVersion`, `verificationStatus`, `rejectionReason`, `verifiedBy`, `verifiedAt`
- `DocumentVersion`: `id`, `documentItemId`, `fileUrl`, `versionNumber`, `uploadedBy`, `uploadedAt`, `uploadNotes`
- `AuthoritySubmission`: `id`, `dossierId`, `authorityName`, `authorityType`, `submissionReferenceCode`, `submittedAt`, `submittedBy`, `receiptFileUrl`, `reviewStatus`, `decisionDate`, `decisionNotes`
- `QuarantineRecord`: `id`, `dossierId`, `checkpointId`, `horseId`, `inspectionDate`, `result`, `healthFindings`, `actionPlan`, `inspectorName`, `recordedBy`
- `CustomsClearanceRecord`: `id`, `dossierId`, `borderGateId`, `declarationNumber`, `status`, `clearedAt`, `remarks`, `recordedBy`
- `DossierAuditTrail`: `id`, `dossierId`, `documentItemId`, `action`, `performedBy`, `performedAt`, `details`

---

# Flow 3 - Lập Kế hoạch Lộ trình & Điều phối Phương tiện

## Danh sách Use Case (17 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Xem danh sách yêu cầu vận chuyển đã được phê duyệt | Xem các yêu cầu vận chuyển đã được Quản lý Điều hành Logistics phê duyệt và đủ điều kiện để bắt đầu lập kế hoạch lộ trình. Thông tin có thể bao gồm khách hàng, danh sách ngựa, nơi đi, nơi đến, ngày mong muốn khởi hành, yêu cầu đặc biệt và loại vận chuyển trong nước hoặc quốc tế. | Fleet & Route Coordinator |
| 02 | Tạo lộ trình | Xây dựng lộ trình di chuyển tối ưu, xác định các điểm dừng nghỉ, trạm kiểm dịch và trạm tiếp nhiên liệu cho một yêu cầu vận chuyển đã được duyệt. | Fleet & Route Coordinator |
| 03 | Tạo kế hoạch tổng quát | Tạo kế hoạch tổng quát dựa trên một yêu cầu vận chuyển đã được phê duyệt. Kế hoạch là nơi tổng hợp tuyến đường, phương tiện, lộ trình, lịch trình và ngày đến dự kiến; đồng thời xác định phương án vận chuyển phù hợp dựa trên số lượng ngựa, sức chứa phương tiện và trạng thái sẵn sàng tại thời điểm dự kiến vận chuyển. | Logistics Manager |
| 04 | Chỉnh sửa kế hoạch tổng quát | Thay đổi bản kế hoạch tổng quát do Manager lập. | Logistics Manager |
| 05 | Phân công nhân sự | Phân công nhân sự phụ trách thực hiện chuyến đi và từng chặng vận chuyển theo vai trò, nhiệm vụ và thời gian đã được xác định trong kế hoạch; theo dõi tình trạng phân công và khả năng đáp ứng của nhân sự. | Logistics Manager |
| 06 | Quản lý lộ trình dự phòng | Điều phối viên có thể tạo, xem, cập nhật hoặc xóa một hoặc nhiều lộ trình dự phòng cho tuyến chính trong quá trình lập kế hoạch chuyến đi. Lộ trình dự phòng là tùy chọn, có thể áp dụng cho toàn tuyến hoặc một chặng cụ thể có rủi ro, và được sử dụng khi tuyến chính không thể tiếp tục. Nếu không có lộ trình dự phòng phù hợp hoặc tất cả lộ trình dự phòng đều không khả thi trong quá trình vận chuyển, việc lập lộ trình thay thế thủ công sẽ được xử lý. | Fleet & Route Coordinator |
| 07 | Xem lộ trình | Xem toàn bộ thông tin của kế hoạch lộ trình, bao gồm tuyến đường, phương tiện, các điểm dừng, lịch trình dự kiến, hãng hàng không nếu có và nhân sự được phân công. | Fleet & Route Coordinator, Logistics Manager |
| 08 | Cập nhật lộ trình | Chỉnh sửa thông tin kế hoạch lộ trình khi kế hoạch chưa được phê duyệt, hoặc khi bị Logistics Manager từ chối/yêu cầu điều chỉnh hoặc khi Coordinator chủ động phát hiện thay đổi cần cập nhật trước khi khởi hành, hoặc khi phát hiện có sự cố dọc đường. Sau khi cập nhật, hệ thống tự động gửi yêu cầu Manager phê duyệt lại. | Fleet & Route Coordinator |
| 09 | Hủy lộ trình đang soạn thảo | Hủy một kế hoạch chưa được phê duyệt khi kế hoạch được tạo nhầm hoặc không còn được sử dụng. Việc hủy không đồng nghĩa với hủy yêu cầu vận chuyển hoặc hủy chuyến đã được phê duyệt. | Fleet & Route Coordinator |
| 10 | Quản lý đối tác vận chuyển | Quản lý các đơn vị vận chuyển bên ngoài mà công ty có thể hợp tác, bao gồm hãng hàng không và đơn vị vận tải đường bộ thuê ngoài, cùng thông tin loại dịch vụ mà đối tác cung cấp. | Logistics Manager |
| 11 | Lựa chọn dịch vụ vận chuyển thuê ngoài | Lựa chọn đối tác và dịch vụ vận chuyển phù hợp cho chặng không sử dụng phương tiện nội bộ, dựa trên tuyến, lịch trình, sức chứa và khả năng phục vụ vận chuyển ngựa. | Logistics Manager |
| 12 | Xem phương tiện chuyên dụng | Quản lý các phương tiện sử dụng để vận chuyển ngựa như xe tải chuyên dụng và khoang máy bay. Thông tin quản lý bao gồm loại phương tiện, sức chứa và trạng thái sẵn sàng. | Fleet & Route Coordinator |
| 13 | Thêm phương tiện chuyên dụng | Thêm một phương tiện mới vào hệ thống để có thể sử dụng trong các chuyến vận chuyển sau này. | Fleet & Route Coordinator |
| 14 | Cập nhật thông tin phương tiện | Cập nhật thông tin như sức chứa, biển số, loại phương tiện, ghi chú vận hành hoặc các thông tin kỹ thuật liên quan. | Fleet & Route Coordinator |
| 15 | Cập nhật trạng thái phương tiện | Thay đổi thủ công trạng thái của phương tiện giữa: sẵn sàng, bảo trì, tạm ngưng sử dụng. Các trạng thái "đã phân công" và "đang vận chuyển" không thao tác thủ công ở đây mà do hệ thống tự động cập nhật. | Fleet & Route Coordinator |
| 16 | Phê duyệt lộ trình cho một yêu cầu vận chuyển trước khi khởi hành | Phê duyệt lộ trình do điều phối viên lập riêng cho một yêu cầu vận chuyển, hoặc từ chối và yêu cầu chỉnh sửa thêm trước khi khởi hành. | Logistics Manager |
| 17 | Phê duyệt thay đổi lộ trình cho một yêu cầu vận chuyển trong lúc vận chuyển | Phê duyệt yêu cầu thay đổi lộ trình do điều phối viên gửi cho một yêu cầu vận chuyển, khi điều phối viên phát hiện có vấn đề phát sinh dọc đường. | Logistics Manager |

### Entity ERD Đề Xuất (Flow 3)
- `TripPlan`: `id`, `bookingId`, `planCode`, `overallStrategy`, `primaryRouteId`, `departureTime`, `estimatedArrivalTime`, `status` (DRAFT, SUBMITTED, APPROVED, REJECTED, IN_TRANSIT, COMPLETED), `createdBy`, `approvedBy`, `approvedAt`
- `RoutePlan`: `id`, `tripPlanId`, `routeName`, `isContingency`, `totalDistanceKm`, `totalDurationHours`, `riskLevel`, `status`
- `RouteCheckpoint`: `id`, `routePlanId`, `sequenceOrder`, `name`, `type` (REST_STOP, VET_CHECK, BORDER_GATE, FERRY_PORT, AIRPORT, FINAL_DEST), `plannedArrival`, `plannedDeparture`, `mandatoryRestMinutes`
- `Vehicle`: `id`, `plateNumber`, `vehicleType`, `ownershipType` (COMPANY_OWNED, LEASED_EXTERNAL), `capacity`, `suspensionType`, `ventilationSystem`, `status` (AVAILABLE, MAINTENANCE, ASSIGNED, IN_TRANSIT)
- `TransportSubcontractor`: `id`, `partnerName`, `serviceType` (FERRY_OPERATOR, AIR_CARGO, EXTERNAL_HAULIER), `contactEmail`, `contactPhone`, `contractStatus`
- `TripStaffAssignment`: `id`, `tripPlanId`, `userId`, `role` (DRIVER, ESCORT), `assignedAt`, `status`

---

# Flow 4 - Cập nhật Trạng thái & Nhật ký Hành trình

## Danh sách Use Case (6 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Xem tiến độ hành trình | Xem danh sách các chặng trong hành trình, trạng thái hiện tại, thời gian cập nhật gần nhất và người cập nhật. | Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |
| 02 | Cập nhật trạng thái chặng | Cập nhật trạng thái của từng chặng thành Khởi hành, Đến điểm dừng, Đã thông quan hoặc Đã giao. Hệ thống ghi nhận thời gian và người cập nhật. | Tài xế / Nhân viên Đi kèm (Vehicle Driver) |
| 03 | Ghi nhật ký sức khỏe ngựa | Ghi nhận tình trạng từng con ngựa trong hành trình, gồm Ổn định, Bỏ ăn, Căng thẳng hoặc Bị ảnh hưởng bởi thay đổi khí hậu. Hệ thống lưu thời điểm ghi nhận. | Nhân viên Đi kèm (Escort) |
| 04 | Thêm ghi chú và hình ảnh tình trạng ngựa | Thêm mô tả và hình ảnh thực tế vào nhật ký sức khỏe để làm bằng chứng về tình trạng ngựa tại thời điểm kiểm tra. | Nhân viên Đi kèm (Escort) |
| 05 | Theo dõi vị trí thực tế | Ghi nhận và hiển thị vị trí hiện tại của chuyến đi khi chức năng theo dõi realtime được bật. | Tài xế, Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |
| 06 | Xem trạng thái ngựa | Xem trạng thái ngựa trên hành trình, người ghi nhận, thời điểm ghi nhận. | Tài xế, Nhân viên Đi kèm, Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |

### Entity ERD Đề Xuất (Flow 4)
- `CheckpointProgressLog`: `id`, `checkpointId`, `tripPlanId`, `actualArrival`, `actualDeparture`, `stageStatus` (PENDING, ARRIVED, CLEARED, DEPARTED), `loggedBy`, `loggedAt`, `notes`
- `HorseHealthLog`: `id`, `tripPlanId`, `horseId`, `checkpointId`, `bodyTemperature`, `appetiteStatus` (NORMAL, REDUCED, NONE), `hydrationStatus`, `stressLevel`, `respiratoryRate`, `loggedBy`, `loggedAt`
- `HealthLogPhoto`: `id`, `healthLogId`, `photoUrl`, `caption`, `uploadedAt`
- `TelemetryLocation`: `id`, `tripPlanId`, `latitude`, `longitude`, `speedKmh`, `recordedAt`, `source` (GPS_DEVICE, DRIVER_MOBILE)

---

# Flow 5 - Xử lý Sự cố & Điều chỉnh Lộ trình Khẩn cấp

## Danh sách Use Case (7 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Báo cáo sự cố phương tiện/giao thông | Tài xế báo cáo sự cố liên quan đến xe hoặc giao thông (hỏng xe, tai nạn, kẹt xe) kèm mức độ nghiêm trọng, thời gian và tóm tắt từ hiện trường. | Vehicle Driver |
| 02 | Báo cáo sự cố sức khỏe ngựa khẩn cấp | Nhân viên đi kèm báo cáo tình trạng sức khỏe ngựa thay đổi đột ngột hoặc nguy cấp cần xử lý ngay (sốt cao, sốc nhiệt, đau bụng colic, chấn thương). | Escort |
| 03 | Nhận thông báo cảnh báo khẩn cấp | Quản lý nhận cảnh báo tức thời khi có sự cố khẩn cấp được báo cáo, để kịp thời theo dõi và ra quyết định phê duyệt. | Logistics Manager |
| 04 | Quyết định lộ trình thủ công khi không thể xử lý tự động | Tính toán (tự động hoặc thủ công) tuyến đường tránh tối ưu và thiết lập lại lộ trình khi có sự cố khẩn cấp. | Fleet & Route Coordinator |
| 05 | Đề xuất chi phí phát sinh khẩn cấp | Tạo yêu cầu chi phí phát sinh do thay đổi lộ trình khẩn cấp, đổi xe hoặc điều động bác sĩ thú y. Nếu chi phí chưa vượt quá budget ứng đối trong quy định công ty thì chỉ cần ghi nhận để hậu kiểm, không cần đề xuất duyệt. | Fleet & Route Coordinator, Vehicle Driver |
| 06 | Ghi nhận sử dụng chi phí phát sinh | Ghi nhận lại số tiền, lý do sử dụng, chi cho người nào, đơn hàng nào để kiểm tra về sau. Nếu số tiền vượt quá budget cho chuyến thì bắt buộc tạo yêu cầu duyệt khẩn. | Vehicle Driver, Escort |
| 07 | Phê duyệt chi phí phát sinh khẩn cấp | Phê duyệt yêu cầu chi phí phát sinh khẩn cấp do Điều phối viên đề xuất. | Logistics Manager |

### Entity ERD Đề Xuất (Flow 5)
- `IncidentReport`: `id`, `tripPlanId`, `incidentType` (VEHICLE_BREAKDOWN, TRAFFIC_JAM, HORSE_HEALTH_CRITICAL, WEATHER_STORM), `severity` (LOW, MEDIUM, HIGH, CRITICAL), `description`, `reportedBy`, `reportedAt`, `resolvedAt`, `resolutionPlan`
- `RerouteApproval`: `id`, `incidentId`, `oldRouteId`, `newRouteId`, `proposedBy`, `approvedBy`, `approvedAt`, `status`
- `EmergencyExpense`: `id`, `tripPlanId`, `incidentId`, `expenseType` (VET_EMERGENCY, TOWING_TRUCK, DETOUR_TOLL, REPLACEMENT_VEHICLE), `amount`, `currency`, `receiptUrl`, `requiresApproval`, `status` (LOGGED_FOR_AUDIT, PENDING, APPROVED, REJECTED), `approvedBy`, `approvedAt`

---

# Flow 6 - Bàn giao, Khiếu nại, Tài chính / Hóa đơn & Báo cáo Quản trị

## Danh sách Use Case (16 UCs)

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Xử lý bàn giao cuối cùng | Ghi nhận chữ ký người nhận, thời gian bàn giao và tình trạng sức khỏe ngựa tại điểm đến cuối cùng. | Vehicle Driver / Escort |
| 02 | Gửi khiếu nại/claim khi nhận hàng | Khách hàng gửi khiếu nại hoặc yêu cầu bồi thường nếu phát hiện ngựa bị tổn thương, stress nặng hoặc sai lệch so với cam kết khi nhận bàn giao. | Customer |
| 03 | Xử lý khiếu nại/claim | Tiếp nhận, xác minh và phản hồi/giải quyết khiếu nại hoặc yêu cầu bồi thường từ khách hàng. | Logistics Manager |
| 04 | Gửi thông báo hoàn tất tự động | Tự động gửi Email thông báo hành trình hoàn tất và ngựa đến nơi an toàn cho khách hàng và nội bộ. | System |
| 05 | Quản lý tổng hợp thu chi thực tế | Hệ thống tổng hợp các khoản chi phí của một Booking đã hoàn tất: cước vận chuyển theo tuyến, phí theo số lượng ngựa, phí hồ sơ pháp lý/kiểm dịch, và các chi phí phát sinh khẩn cấp đã được phê duyệt. Kết quả là danh sách các dòng phí đề xuất để lập hóa đơn. | System, Logistics Manager |
| 06 | Lập hóa đơn thanh toán | Logistics Manager lập hóa đơn và có thể thêm, sửa, xóa dòng phí, chọn loại hóa đơn và đặt hạn thanh toán. Hệ thống sinh mã hóa đơn, gắn trạng thái "Nháp". | Logistics Manager |
| 07 | Phát hành hóa đơn cho khách hàng | Chuyển hóa đơn từ "Nháp" sang "Đã phát hành", khóa nội dung hóa đơn và gửi thông báo tới Customer. Hệ thống ghi nhận người phát hành và thời điểm phát hành. | Logistics Manager |
| 08 | Xem danh sách / chi tiết hóa đơn | Cho phép xem hóa đơn kèm từng dòng phí, tổng tiền cần thanh toán, và hạn thanh toán. Customer chỉ xem được hóa đơn thuộc Booking do mình tạo; Logistics Manager xem toàn bộ. | Customer, Logistics Manager |
| 09 | Ghi nhận thanh toán | Cho phép Logistics Manager ghi nhận thanh toán của khách hàng (số tiền, phương thức, thời điểm, mã giao dịch đối chiếu). Hệ thống cập nhật và tự động chuyển trạng thái hóa đơn sang "Đã thanh toán", đồng thời sinh biên nhận tương ứng gửi cho Customer. | Logistics Manager |
| 10 | Điều chỉnh / hủy hóa đơn | Cho phép hủy hóa đơn lập sai khi chưa phát sinh thanh toán, hoặc điều chỉnh khi có sai lệch. Bắt buộc nhập lý do. Hệ thống lưu vết người thao tác và thời điểm. | Logistics Manager |
| 11 | Xem lịch sử trạng thái hóa đơn | Xem toàn bộ tiến trình trạng thái của hóa đơn kèm người thực hiện và thời điểm, đảm bảo khả năng truy vết tài chính. | Customer, Logistics Manager |
| 12 | Xem báo cáo tỷ lệ đúng giờ | Xem báo cáo thống kê tỷ lệ chuyến đi hoàn thành đúng thời hạn cam kết (On-Time Delivery). | Logistics Manager |
| 13 | Xem báo cáo tần suất sự cố | Xem thống kê số lượng và loại sự cố phát sinh trong các chuyến vận chuyển theo thời gian. | Logistics Manager |
| 14 | Xem báo cáo thời gian vận chuyển trung bình | Xem thống kê số ngày vận chuyển trung bình theo tuyến đường hoặc loại đơn hàng. | Logistics Manager |
| 15 | Xem báo cáo tỷ lệ sử dụng nguồn lực | Xem thống kê tỷ lệ sử dụng xe, container, thiết bị so với tổng nguồn lực sẵn có. | Logistics Manager |
| 16 | Xem lịch sử vận chuyển | Để đảm bảo an toàn cho các con ngựa tham gia các cuộc đua, cũng như tuân thủ các quy định pháp luật, chức năng này cho phép lưu trữ và xuất khẩu toàn bộ thông tin liên quan: thời điểm, người vận chuyển, tuyến đường vận chuyển, tình trạng sức khỏe của ngựa vào thời điểm đó, cũng như các giấy tờ liên quan. (Lý do: Để đảm bảo trách nhiệm bồi thường và tính minh bạch về mặt pháp lý trong trường hợp xảy ra sự cố khi vận chuyển những con ngựa có giá trị cao ra nước ngoài.) | Logistics Manager |

### Entity ERD Đề Xuất (Flow 6)
- `HandoverReceipt`: `id`, `tripPlanId`, `recipientName`, `recipientSignatureUrl`, `handoverTime`, `trotUpCondition`, `deliveryNotes`, `signedBy`
- `TripClaim`: `id`, `tripPlanId`, `customerId`, `claimType`, `damageDescription`, `claimedAmount`, `evidenceFileUrls`, `status` (SUBMITTED, UNDER_REVIEW, APPROVED, REJECTED, SETTLED), `settlementAmount`, `resolvedBy`, `resolvedAt`
- `Invoice`: `id`, `bookingId`, `invoiceCode`, `customerId`, `issueDate`, `dueDate`, `subtotalAmount`, `quarantineFee`, `emergencyFee`, `vatAmount`, `totalAmount`, `status` (DRAFT, ISSUED, PAID, CANCELLED, ADJUSTED), `issuedBy`, `issuedAt`
- `InvoiceItem`: `id`, `invoiceId`, `feeType`, `description`, `unitPrice`, `quantity`, `amount`
- `PaymentReceipt`: `id`, `invoiceId`, `paymentMethod` (BANK_TRANSFER, CREDIT_CARD), `transactionReference`, `paidAmount`, `paidAt`, `recordedBy`, `receiptUrl`
- `InvoiceStatusHistory`: `id`, `invoiceId`, `fromStatus`, `toStatus`, `reason`, `performedBy`, `performedAt`
- `TransportAuditExport`: `id`, `tripPlanId`, `exportType` (PDF_DOSSIER, AUDIT_CSV), `fileUrl`, `generatedBy`, `generatedAt`
