# Danh Mục Use Cases — Hệ Thống Quản Lý Vận Chuyển Ngựa Đua Xuyên Quốc Gia
## Cross-Border Racehorse Transport System (`swp391-cross-border-racehorse-transport-system`)

> **Mã đề tài / Phụ trách**: `2 — HoangNT20`  
> **Dự án**: Cross-Border Racehorse Transport System  
> **Môn học**: SWP391 (Software Project)  
> **Phương pháp tiếp cận**: Document-First System Specification (BDD Acceptance Criteria)  
> **Cập nhật gần nhất**: 2026-09-12  

---

## 👥 Ma Trận 5 Nhóm Tác Nhân (Actors & Responsibilities)

| Tác nhân (Actor) | Tên tiếng Việt | Phạm vi trách nhiệm cốt lõi |
|---|---|---|
| **Customer** | Khách hàng (Chủ ngựa / CLB đua) | Đăng ký/đăng nhập, tạo yêu cầu vận chuyển, tải hồ sơ y tế/hộ chiếu ngựa, theo dõi hành trình realtime, nhận thông báo thông quan, ký biên bản bàn giao và gửi khiếu nại/claim. |
| **Logistics Manager** | Quản lý Điều hành Logistics | Tiếp nhận & phê duyệt đơn hàng; lập kế hoạch tổng thể; phân công Specialist, Coordinator, Driver; phê duyệt thay đổi lộ trình & chi phí khẩn cấp; xem báo cáo doanh thu, chi phí, OTD, sự cố. |
| **Transport Specialist** | Chuyên viên Thủ tục & Kiểm dịch | Quản lý quy định pháp lý/kiểm dịch từng quốc gia; khởi tạo & quản lý hồ sơ số hóa của ngựa (FEI Passport, Coggins Test, tiêm phòng); nộp và theo dõi duyệt từ cơ quan chức năng; xác nhận đủ điều kiện khởi hành. |
| **Fleet & Route Coordinator** | Điều phối viên Đội xe & Lộ trình | Quản lý đội xe chuyên dụng và thùng Jet Stalls; thiết kế lộ trình tối ưu và các điểm dừng nghỉ/kiểm dịch; theo dõi tiến độ chặng; tái lập lộ trình tránh sự cố; đề xuất chi phí phát sinh. |
| **Vehicle Driver / Escort** | Tài xế / Nhân viên Đi kèm & Chăm sóc | Xem lịch trình & danh sách ngựa phụ trách; cập nhật trạng thái mốc chặng thủ công; ghi nhật ký sức khỏe & ảnh chụp ngựa; báo cáo sự cố khẩn cấp (SOS); thực hiện bàn giao tại đích. |

---

# Flow 1 - Quản lý Tài khoản và Yêu cầu Vận chuyển

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Đăng ký tài khoản khách hàng | Cho phép người dùng bên ngoài (Chủ ngựa, Câu lạc bộ) cung cấp thông tin (Họ tên, Email/SĐT, Mật khẩu, Tên CLB) để tạo tài khoản mới. Hệ thống kiểm tra trùng lặp, gửi mã xác thực (OTP/Link) và khởi tạo hồ sơ Customer trên hệ thống. (Lưu ý: Nhân sự nội bộ không tự đăng ký mà sẽ được cấp tài khoản). | Customer |
| 02 | Đăng nhập và phân quyền | Cho phép người dùng nhập thông tin xác thực (Email/SĐT và Mật khẩu). Hệ thống kiểm tra dữ liệu, cấp phiên làm việc (token) và điều hướng giao diện (phân quyền) tương ứng với role của người dùng (Customer, Manager, Driver...). | Tất cả các Actor (Customer, Logistics Manager, Transport Specialist, Fleet & Route Coordinator, Vehicle Driver) |
| 03 | Quên / Khôi phục mật khẩu | Cho phép người dùng yêu cầu khôi phục quyền truy cập khi quên mật khẩu. Người dùng nhập Email/SĐT, hệ thống gửi mã xác thực (OTP) qua email/sdt. Sau khi xác thực thành công, cho phép người dùng đặt lại mật khẩu mới. | Tất cả các Actor |
| 04 | Thay đổi mật khẩu | Cho phép người dùng đang trong phiên đăng nhập (đã xác thực) đổi mật khẩu tài khoản để tăng tính bảo mật. Người dùng cần nhập đúng mật khẩu cũ và xác nhận mật khẩu mới. | Tất cả các Actor |
| 05 | Cập nhật hồ sơ cá nhân (Profile) | Cho phép người dùng xem và chỉnh sửa các thông tin cá nhân cơ bản (Ảnh đại diện, Số điện thoại liên hệ, Địa chỉ). Hệ thống lưu vết chỉnh sửa và cập nhật dữ liệu mới nhất. | Tất cả các Actor |
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

## Ghi chú Entity có thể phát sinh cho ERD (Flow 1)

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `User` | `id`, `fullName`, `email`, `phone`, `passwordHash`, `role`, `status`, `createdAt`, `updatedAt` |
| `CustomerProfile` | `id`, `userId`, `clubName`, `avatarUrl`, `contactAddress`, `taxCode`, `createdAt`, `updatedAt` |
| `TransportRequest` | `id`, `requestCode`, `customerId`, `departureLocation`, `destinationLocation`, `requestedDepartureTime`, `totalHorses`, `currentStatus`, `approvedBy`, `approvedAt`, `rejectionReason`, `cancellationReason`, `createdAt`, `updatedAt` |
| `RequestHorseItem` | `id`, `requestId`, `horseName`, `breed`, `gender`, `age`, `microchipNumber`, `dietaryRequirements`, `stallPreference`, `specialInstructions` |
| `RequestStatusHistory` | `id`, `requestId`, `fromStatus`, `toStatus`, `action`, `reason`, `performedBy`, `performedAt` |
| `OtpVerification` | `id`, `userId`, `contactValue`, `otpCode`, `otpType`, `expiresAt`, `isUsed`, `createdAt` |

---

# Flow 2 - Quản lý Hồ sơ Pháp lý & Kiểm dịch Thông quan

> **Câu hỏi trọng tâm**: *“Đối với chuyến vận chuyển này, từng con ngựa đã có đầy đủ giấy tờ cần thiết chưa, giấy tờ có hợp lệ không, hồ sơ đã được gửi đi chưa, cơ quan chức năng đã phê duyệt chưa, và chuyến hàng đã đủ điều kiện thông quan hay chưa?”*

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Quản lý danh mục quy định | Cho phép quản lý danh mục các quy định pháp lý, kiểm dịch, hải quan và các loại giấy tờ bắt buộc theo từng quốc gia, cửa khẩu hoặc tuyến vận chuyển. Transport Specialist có thể thêm, chỉnh sửa, xem và xóa các danh mục. | Transport Specialist |
| 02 | Quản lý các bộ hồ sơ mẫu | Cho phép Transport Specialist quản lý các bộ hồ sơ mẫu tương ứng với từng trường hợp vận chuyển, trong đó xác định sẵn các loại giấy tờ bắt buộc (ví dụ: nội địa gồm giấy kiểm dịch + nguồn gốc; xuyên biên giới gồm hộ chiếu FEI + giấy kiểm dịch + hải quan + ATA Carnet). TS có thể tạo, xem, cập nhật, kích hoạt/ngừng sử dụng và xóa các bộ hồ sơ mẫu. | Transport Specialist |
| 03 | Áp dụng bộ hồ sơ mẫu cho chuyến vận chuyển | Cho phép Transport Specialist lựa chọn và áp dụng một bộ hồ sơ mẫu phù hợp cho chuyến vận chuyển. Hệ thống tự động tạo danh sách các giấy tờ cần có của chuyến dựa trên bộ hồ sơ được chọn và đánh dấu các giấy tờ trong bộ là bắt buộc. | Transport Specialist |
| 04 | Quản lý bộ hồ sơ pháp lý của tuyến vận chuyển | Cho phép Transport Specialist quản lý các yêu cầu hồ sơ pháp lý áp dụng cho từng tuyến vận chuyển. Hệ thống lưu các yêu cầu giấy tờ đặc thù của tuyến để có thể tự động bổ sung vào hồ sơ khi chuyến vận chuyển sử dụng tuyến đó. TS có thể thêm vào các giấy tờ phát sinh, yêu cầu đặc thù của chuyến, chỉnh sửa hoặc xóa các giấy tờ không nằm trong bộ mẫu bắt buộc. | Transport Specialist / System |
| 05 | Xem bộ hồ sơ pháp lý của chuyến vận chuyển | Cho phép người dùng xem tình trạng tổng thể của bộ hồ sơ pháp lý, bao gồm các giấy tờ đã có, còn thiếu, đang chờ kiểm tra, cần bổ sung, đã được xác nhận và tình trạng xét duyệt từ cơ quan chức năng. Quyền xem chi tiết được giới hạn theo từng actor. | Transport Specialist, Logistics Manager, Customer |
| 06 | Tải lên giấy tờ pháp lý / y tế | Cho phép Customer tải lên các giấy tờ được yêu cầu cho từng con ngựa hoặc cho chuyến vận chuyển (hộ chiếu ngựa, giấy chứng nhận sức khỏe, chứng nhận tiêm phòng, giấy tờ sở hữu). Customer có thể cung cấp thêm thông tin như số giấy tờ, ngày cấp, ngày hết hạn và cơ quan cấp. | Customer |
| 07 | Cập nhật hoặc thay thế giấy tờ đã tải lên | Cho phép Customer cung cấp phiên bản mới của một giấy tờ khi giấy tờ cũ bị sai, hết hạn, không hợp lệ hoặc được yêu cầu bổ sung lại. Hệ thống giữ lại các phiên bản cũ để đảm bảo khả năng truy vết lịch sử hồ sơ. | Customer |
| 08 | Xác nhận giấy tờ hợp lệ | Cho phép Transport Specialist xác nhận một giấy tờ là hợp lệ sau khi đã kiểm tra đầy đủ và đáp ứng yêu cầu. Sau khi được xác nhận, giấy tờ được tính là hoàn thành trong checklist hồ sơ của chuyến vận chuyển. | Transport Specialist |
| 09 | Yêu cầu chỉnh sửa / cung cấp lại giấy tờ | Cho phép Transport Specialist yêu cầu Customer cung cấp lại một giấy tờ đã tải lên nhưng không hợp lệ (hết hạn, thiếu chữ ký, sai thông tin, file mờ hoặc không đúng đối tượng). Transport Specialist phải ghi rõ lý do để Customer biết cần chỉnh sửa gì. | Transport Specialist |
| 10 | Yêu cầu bổ sung giấy tờ còn thiếu | Cho phép Transport Specialist xác định các giấy tờ bắt buộc mà Customer chưa cung cấp và gửi yêu cầu bổ sung. Yêu cầu bao gồm danh sách giấy tờ còn thiếu, thời hạn cần bổ sung và nội dung hướng dẫn cho Customer. | Transport Specialist |
| 11 | Ghi nhận nộp bộ hồ sơ tới cơ quan chức năng | Cho phép Transport Specialist ghi nhận việc bộ hồ sơ đã được gửi tới cơ quan kiểm dịch, hải quan hoặc cơ quan quản lý có thẩm quyền bên ngoài hệ thống (lưu ngày nộp, mã tham chiếu hồ sơ, cơ quan tiếp nhận và file xác nhận nộp hồ sơ). | Transport Specialist |
| 12 | Cập nhật kết quả xét duyệt hồ sơ | Cho phép Transport Specialist cập nhật trạng thái xét duyệt của hồ sơ từ cơ quan chức năng (đã nộp, đang xét duyệt, được phê duyệt hoặc bị từ chối). Nếu bị từ chối, hệ thống lưu lý do và yêu cầu chỉnh sửa để tiếp tục xử lý. | Transport Specialist |
| 13 | Ghi nhận kết quả kiểm dịch | Cho phép Transport Specialist ghi nhận kết quả kiểm tra kiểm dịch tại trạm hoặc cửa khẩu trong quá trình vận chuyển (đạt yêu cầu, không đạt hoặc cần kiểm tra lại). Nếu phát sinh vấn đề, lưu lý do, con ngựa bị ảnh hưởng và hướng xử lý tiếp theo. | Transport Specialist |
| 14 | Ghi nhận kết quả thông quan | Cho phép Transport Specialist cập nhật tình trạng xử lý hải quan của chuyến vận chuyển tại từng cửa khẩu (đang chờ, đang xử lý, đã thông quan, bị giữ hoặc bị từ chối). Khi thông quan thành công, chuyến có thể tiếp tục sang chặng tiếp theo. | Transport Specialist |
| 15 | Theo dõi tiến độ hoàn thiện hồ sơ | Cho phép Transport Specialist và Logistics Manager theo dõi tổng quan mức độ hoàn thiện hồ sơ của nhiều chuyến vận chuyển, phát hiện các chuyến đang thiếu giấy tờ, có giấy tờ bị từ chối, hồ sơ sắp tới ngày khởi hành hoặc đang chờ cơ quan chức năng xét duyệt. | Transport Specialist, Logistics Manager |
| 16 | Xem lịch sử xử lý hồ sơ | Cho phép xem toàn bộ lịch sử thao tác và thay đổi trạng thái của bộ hồ sơ (người tải giấy tờ, người kiểm tra, thời điểm xác nhận, yêu cầu bổ sung, nộp hồ sơ và cập nhật kết quả xét duyệt). Đảm bảo truy vết và kiểm soát quá trình xử lý. | Transport Specialist, Logistics Manager |

## Ghi chú Entity có thể phát sinh cho ERD (Flow 2)

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `RegulatoryRequirement` | `id`, `countryCode`, `checkpointId`, `routeId`, `category`, `documentType`, `title`, `description`, `isMandatory`, `validityPeriodDays`, `createdAt`, `updatedAt` |
| `DossierTemplate` | `id`, `templateCode`, `templateName`, `scope`, `description`, `isActive`, `createdAt`, `updatedAt` |
| `DossierTemplateItem` | `id`, `templateId`, `documentType`, `documentName`, `isMandatory`, `targetScope`, `instructions` |
| `TripLegalDossier` | `id`, `tripPlanId`, `templateId`, `overallStatus`, `clearedForDeparture`, `clearedAt`, `clearedBy`, `createdAt`, `updatedAt` |
| `DossierDocumentItem` | `id`, `dossierId`, `horseId`, `documentType`, `documentName`, `documentNumber`, `issuingAuthority`, `issueDate`, `expiryDate`, `currentFileUrl`, `currentVersion`, `verificationStatus`, `rejectionReason`, `verifiedBy`, `verifiedAt` |
| `DocumentVersion` | `id`, `documentItemId`, `fileUrl`, `versionNumber`, `uploadedBy`, `uploadedAt`, `uploadNotes` |
| `AuthoritySubmission` | `id`, `dossierId`, `authorityName`, `authorityType`, `submissionReferenceCode`, `submittedAt`, `submittedBy`, `receiptFileUrl`, `reviewStatus`, `decisionDate`, `decisionNotes` |
| `QuarantineRecord` | `id`, `dossierId`, `checkpointId`, `horseId`, `inspectionDate`, `result`, `healthFindings`, `actionPlan`, `inspectorName`, `recordedBy` |
| `CustomsClearanceRecord` | `id`, `dossierId`, `borderGateId`, `declarationNumber`, `status`, `clearedAt`, `remarks`, `recordedBy` |
| `DossierAuditTrail` | `id`, `dossierId`, `documentItemId`, `action`, `performedBy`, `performedAt`, `details` |

---

# Flow 3 - Lập Kế hoạch Lộ trình & Điều phối Phương tiện

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Quản lý danh mục phương tiện chuyên dụng | Quản lý danh sách xe tải chuyên dụng (Horse Trucks/Vans trang bị giảm xóc hơi), thùng máy bay (Air Stalls) và tình trạng kiểm định kỹ thuật. | Fleet & Route Coordinator |
| 02 | Thiết kế lộ trình di chuyển & điểm dừng nghỉ | Xây dựng tuyến đường tối ưu, giới hạn chặng di chuyển liên tục tối đa 4-6 tiếng, chỉ định các trạm nghỉ chân (Rest Stops), trạm kiểm dịch và tiếp nhiên liệu. | Fleet & Route Coordinator |
| 03 | Gán phương tiện và phân công tài xế | Gán xe chuyên dụng, thùng chứa hàng không và phân công đội tài xế / nhân viên đi kèm (`Vehicle Driver / Escort`) cho từng chuyến vận chuyển. | Fleet & Route Coordinator |
| 04 | Xuất bản kế hoạch hành trình | Xuất bản lịch trình chi tiết và gửi thông báo bàn giao nhiệm vụ hành trình tới ứng dụng của Tài xế và Quản lý. | Fleet & Route Coordinator |

---

# Flow 4 - Cập nhật Trạng thái & Nhật ký Hành trình

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Xem tiến độ hành trình | Xem danh sách các chặng trong hành trình, trạng thái hiện tại, thời gian cập nhật gần nhất và người cập nhật. | Fleet & Route Coordinator, Logistics Manager, Customer |
| 02 | Cập nhật trạng thái chặng | Cập nhật trạng thái của từng chặng thành Khởi hành, Đến điểm dừng, Đã thông quan hoặc Đã giao. Hệ thống ghi nhận thời gian và người cập nhật. | Vehicle Driver / Escort |
| 03 | Ghi nhật ký sức khỏe ngựa | Ghi nhận tình trạng từng con ngựa trong hành trình, gồm Ổn định, Bỏ ăn, Căng thẳng hoặc Bị ảnh hưởng bởi thay đổi khí hậu. Hệ thống lưu thời điểm ghi nhận. | Vehicle Driver / Escort |
| 04 | Thêm ghi chú và hình ảnh tình trạng ngựa | Thêm mô tả và hình ảnh thực tế vào nhật ký sức khỏe để làm bằng chứng về tình trạng ngựa tại thời điểm kiểm tra. | Vehicle Driver / Escort |
| 05 | Theo dõi vị trí thực tế | Ghi nhận và hiển thị vị trí hiện tại của chuyến đi khi chức năng theo dõi realtime được bật. | Vehicle Driver / Escort, Fleet & Route Coordinator, Logistics Manager, Customer |
| 06 | Xem trạng thái ngựa | Xem trạng thái sức khỏe từng con ngựa trên hành trình, người ghi nhận, thời điểm ghi nhận. | Vehicle Driver / Escort, Fleet & Route Coordinator, Logistics Manager, Customer |

## Ghi chú Entity có thể phát sinh cho ERD (Flow 4)

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `RouteCheckpoint` | `id`, `tripPlanId`, `sequenceOrder`, `checkpointName`, `checkpointType`, `scheduledArrival`, `actualArrival`, `actualDeparture`, `currentStatus` |
| `CheckpointStatusHistory` | `id`, `checkpointId`, `status`, `note`, `updatedBy`, `updatedAt` |
| `HorseHealthLog` | `id`, `tripPlanId`, `horseId`, `checkpointId`, `bodyTemperature`, `appetiteStatus`, `hydrationStatus`, `behaviorStatus`, `stallCondition`, `observedBy`, `observedAt` |
| `HealthLogAttachment` | `id`, `healthLogId`, `fileUrl`, `fileType`, `caption`, `uploadedAt` |
| `LocationPing` | `id`, `tripPlanId`, `latitude`, `longitude`, `accuracy`, `source`, `recordedAt` |

---

# Flow 5 - Xử lý Sự cố & Điều chỉnh Lộ trình Khẩn cấp

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Báo cáo sự cố phương tiện / giao thông | Tài xế báo cáo sự cố liên quan đến xe hoặc giao thông (hỏng xe, tai nạn, kẹt xe) kèm mức độ nghiêm trọng, thời gian và tóm tắt từ hiện trường. | Vehicle Driver / Escort |
| 02 | Báo cáo sự cố sức khỏe ngựa khẩn cấp | Tài xế / nhân viên đi kèm báo cáo tình trạng sức khỏe ngựa thay đổi đột ngột hoặc nguy cấp cần xử lý ngay (sốt cao, sốc nhiệt, chấn thương). | Vehicle Driver / Escort |
| 03 | Nhận thông báo cảnh báo khẩn cấp | Quản lý nhận cảnh báo tức thời khi có sự cố khẩn cấp được báo cáo, để kịp thời theo dõi và ra quyết định phê duyệt. | Logistics Manager |
| 04 | Quyết định lộ trình thủ công khi không thể xử lý tự động | Tính toán (tự động hoặc thủ công) tuyến đường tránh tối ưu và thiết lập lại lộ trình khi có sự cố khẩn cấp. | Fleet & Route Coordinator |
| 05 | Đề xuất chi phí phát sinh khẩn cấp | Tạo yêu cầu chi phí phát sinh do thay đổi lộ trình khẩn cấp, đổi xe hoặc điều động bác sĩ thú y. Nếu chi phí chưa vượt quá budget ứng đối trong quy định công ty thì chỉ cần ghi nhận để hậu kiểm, không cần đề xuất duyệt. | Fleet & Route Coordinator, Vehicle Driver / Escort |
| 06 | Ghi nhận sử dụng chi phí phát sinh | Ghi nhận lại số tiền, lý do sử dụng, chi cho người nào, đơn hàng nào để kiểm tra về sau. Nếu số tiền vượt quá budget cho chuyến thì bắt buộc tạo yêu cầu duyệt khẩn. | Vehicle Driver / Escort, Fleet & Route Coordinator |
| 07 | Phê duyệt chi phí phát sinh khẩn cấp | Phê duyệt yêu cầu chi phí phát sinh khẩn cấp do Điều phối viên hoặc Tài xế đề xuất. | Logistics Manager |

## Ghi chú Entity có thể phát sinh cho ERD (Flow 5)

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `TripIncident` | `id`, `tripPlanId`, `incidentType` (VEHICLE, TRAFFIC, HORSE_HEALTH, WEATHER), `severity` (LOW, MEDIUM, HIGH, CRITICAL), `description`, `reportedBy`, `reportedAt`, `resolvedAt`, `resolutionNotes` |
| `IncidentAttachment` | `id`, `incidentId`, `fileUrl`, `fileType`, `uploadedAt` |
| `ReroutePlan` | `id`, `incidentId`, `oldRouteDetails`, `newRouteDetails`, `detourReason`, `proposedBy`, `approvedBy`, `approvedAt`, `status` |
| `EmergencyExpense` | `id`, `tripPlanId`, `incidentId`, `amount`, `currency`, `expenseCategory`, `reason`, `paidTo`, `receiptUrl`, `requiresApproval`, `status` (LOGGED_FOR_AUDIT, PENDING, APPROVED, REJECTED), `approvedBy`, `approvedAt` |

---

# Flow 6 - Bàn giao, Khiếu nại & Báo cáo Quản trị

## Danh sách Use Case

| STT | Tên UC | Mô tả UC | Tác nhân |
|:---:|---|---|---|
| 01 | Xử lý bàn giao cuối cùng | Ghi nhận chữ ký người nhận, thời gian bàn giao và tình trạng sức khỏe ngựa tại điểm đến cuối cùng. | Vehicle Driver / Escort |
| 02 | Gửi khiếu nại / claim khi nhận hàng | Khách hàng gửi khiếu nại hoặc yêu cầu bồi thường nếu phát hiện ngựa bị tổn thương, stress nặng hoặc sai lệch so với cam kết khi nhận bàn giao. | Customer |
| 03 | Xử lý khiếu nại / claim | Tiếp nhận, xác minh và phản hồi/giải quyết khiếu nại hoặc yêu cầu bồi thường từ khách hàng. | Logistics Manager |
| 04 | Gửi thông báo hoàn tất tự động | Tự động gửi Email/SMS thông báo hành trình hoàn tất và ngựa đến nơi an toàn cho khách hàng và nội bộ. | System |
| 05 | Quản lý thu chi thực tế | Tổng hợp doanh thu dự kiến và chi phí thực tế (nhiên liệu, phí đường, kiểm dịch, chi phí khẩn cấp) theo từng dự án vận chuyển. | Logistics Manager |
| 06 | Xem báo cáo tỷ lệ đúng giờ | Xem báo cáo thống kê tỷ lệ chuyến đi hoàn thành đúng thời hạn cam kết (On-Time Delivery - OTD). | Logistics Manager |
| 07 | Xem báo cáo tần suất sự cố | Xem thống kê số lượng và loại sự cố phát sinh trong các chuyến vận chuyển theo thời gian. | Logistics Manager |
| 08 | Xem báo cáo thời gian vận chuyển trung bình | Xem thống kê số ngày vận chuyển trung bình theo tuyến đường hoặc loại đơn hàng. | Logistics Manager |
| 09 | Xem báo cáo tỷ lệ sử dụng nguồn lực | Xem thống kê tỷ lệ sử dụng xe, container, thiết bị so với tổng nguồn lực sẵn có. | Logistics Manager |
| 10 | Xem lịch sử vận chuyển | Lưu trữ và xuất khẩu toàn bộ thông tin liên quan: thời điểm, người vận chuyển, tuyến đường vận chuyển, tình trạng sức khỏe của ngựa vào thời điểm đó, cũng như các giấy tờ liên quan để đảm bảo trách nhiệm bồi thường và tính minh bạch pháp lý quốc tế. | Logistics Manager, Customer |

## Ghi chú Entity có thể phát sinh cho ERD (Flow 6)

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `HandoverReceipt` | `id`, `tripPlanId`, `recipientName`, `recipientSignatureUrl`, `handoverTime`, `horseHealthConditionAtDelivery`, `notes`, `signedBy` |
| `TripClaim` | `id`, `tripPlanId`, `customerId`, `claimType`, `damageDescription`, `claimedAmount`, `evidenceFileUrls`, `status` (SUBMITTED, UNDER_REVIEW, APPROVED, REJECTED, COMPENSATED), `resolutionNotes`, `resolvedBy`, `resolvedAt` |
| `TripFinancialSummary` | `id`, `tripPlanId`, `quotedRevenue`, `actualFuelCost`, `actualTollCost`, `quarantineFees`, `emergencyExpenses`, `netProfit`, `settledAt` |
| `TransportAuditExport` | `id`, `tripPlanId`, `exportType` (PDF, CSV), `fileUrl`, `generatedBy`, `generatedAt` |
