# Flow 1 - Quản lý Tài khoản và Yêu cầu Vận chuyển

## Danh sách Use Case

| Tên UC | Mô tả UC | Tác nhân |
|---|---|---|
| Đăng ký tài khoản khách hàng | Cho phép người dùng bên ngoài (Chủ ngựa, Câu lạc bộ) cung cấp thông tin (Họ tên, Email/SĐT, Mật khẩu, Tên CLB) để tạo tài khoản mới. Hệ thống kiểm tra trùng lặp, gửi mã xác thực (OTP/Link) và khởi tạo hồ sơ Customer trên hệ thống. (Lưu ý: Nhân sự nội bộ không tự đăng ký mà sẽ được cấp tài khoản). | Khách hàng |
| Đăng nhập và phân quyền | Cho phép người dùng nhập thông tin xác thực (Email/SĐT và Mật khẩu). Hệ thống kiểm tra dữ liệu, cấp phiên làm việc (token) và điều hướng giao diện (phân quyền) tương ứng với role của người dùng (Customer, Manager, Driver...). | Tất cả các Actor (Khách hàng, Quản lý Điều hành Logistics, Chuyên viên Kiểm dịch, Điều phối viên Đội xe và Lộ trình, Tài xế) |
| Quên / Khôi phục mật khẩu | Cho phép người dùng yêu cầu khôi phục quyền truy cập khi quên mật khẩu. Người dùng nhập Email/SĐT, hệ thống gửi mã xác thực (OTP) qua email/sdt. Sau khi xác thực thành công, cho phép người dùng đặt lại mật khẩu mới. | Tất cả các Actor |
| Thay đổi mật khẩu | Cho phép người dùng đang trong phiên đăng nhập (đã xác thực) đổi mật khẩu tài khoản để tăng tính bảo mật. Người dùng cần nhập đúng mật khẩu cũ và xác nhận mật khẩu mới. | Tất cả các Actor |
| Cập nhật hồ sơ cá nhân (Profile) | Cho phép người dùng xem và chỉnh sửa các thông tin cá nhân cơ bản (Ảnh đại diện, Số điện thoại liên hệ, Địa chỉ). Hệ thống lưu vết chỉnh sửa và cập nhật dữ liệu mới nhất. | Tất cả các Actor |
| Đăng xuất hệ thống | Cho phép người dùng chủ động chấm dứt phiên làm việc hiện tại. Hệ thống tiến hành xóa token, hủy quyền truy cập và điều hướng người dùng về lại màn hình Đăng nhập. | Tất cả các Actor |
| Tạo yêu cầu vận chuyển mới | Cho phép Customer khởi tạo một yêu cầu vận chuyển ngựa. Customer cần nhập đầy đủ thông tin: điểm đi, điểm đến, và thời gian mong muốn khởi hành. Hệ thống tạo mã yêu cầu, gắn trạng thái mặc định là "Chờ duyệt" và tự động ghi nhận thời điểm tạo yêu cầu. | Khách hàng |
| Quản lý danh sách ngựa và yêu cầu đặc biệt của riêng khách hàng | Cho phép Customer thêm, xóa hoặc chỉnh sửa danh sách những con ngựa sẽ tham gia vào chuyến vận chuyển. Đồng thời, Customer có thể thiết lập các yêu cầu đặc biệt cho từng con ngựa hoặc toàn chuyến (ví dụ: loại xe chuyên dụng, chế độ ăn, hoặc yêu cầu người đi kèm). Hệ thống tự động tính toán tổng số lượng ngựa trong yêu cầu. | Khách hàng |
| Cập nhật thông tin yêu cầu | Cho phép Customer thay đổi thông tin của yêu cầu (điểm đi, điểm đến, thời gian, danh sách ngựa, yêu cầu đặc biệt) với điều kiện yêu cầu đó vẫn đang ở trạng thái "Chờ duyệt". Hệ thống sẽ lưu lại thời điểm chỉnh sửa và cập nhật phiên bản dữ liệu mới nhất. | Khách hàng |
| Hủy yêu cầu vận chuyển | Cho phép Customer chủ động hủy bỏ một yêu cầu vận chuyển khi hệ thống chưa xử lý. Customer bắt buộc phải nhập lý do hủy. Hệ thống sẽ chuyển trạng thái sang "Hủy", ghi nhận lý do và thời điểm thực hiện thao tác vào lịch sử yêu cầu. | Khách hàng |
| Xem danh sách yêu cầu vận chuyển | Cho phép người dùng xem danh sách các yêu cầu vận chuyển với các thông tin cơ bản. Customer chỉ xem được các yêu cầu do chính mình tạo. Logistics Manager được xem toàn bộ danh sách trên hệ thống để phục vụ việc phân loại, theo dõi và xử lý. | Khách hàng, Quản lý Điều hành Logistics |
| Xem chi tiết yêu cầu vận chuyển | Cho phép người dùng truy xuất toàn bộ thông tin chi tiết của một yêu cầu cụ thể. Dữ liệu hiển thị có thể bao gồm: thông tin khách hàng đặt, lịch trình mong muốn, tổng số lượng ngựa, chi tiết từng con ngựa, các yêu cầu đặc biệt, trạng thái hiện tại và người phê duyệt (nếu có). | Khách hàng, Quản lý Điều hành Logistics |
| Phê duyệt yêu cầu vận chuyển | Cho phép Logistics Manager kiểm tra tính khả thi của yêu cầu và tiến hành phê duyệt. Khi thao tác, hệ thống chuyển trạng thái yêu cầu từ "Chờ duyệt" sang "Đã duyệt", đồng thời lưu trữ thông tin định danh của người phê duyệt và thời điểm thực hiện phê duyệt. | Quản lý Điều hành Logistics |
| Từ chối yêu cầu vận chuyển | Cho phép Logistics Manager từ chối tiếp nhận một yêu cầu vận chuyển nếu không đủ điều kiện. Logistics Manager bắt buộc phải nhập lý do từ chối để phản hồi. Trạng thái yêu cầu chuyển thành "Từ chối", hệ thống lưu lại thông tin người thao tác, lý do cụ thể và thời điểm thực hiện. | Quản lý Điều hành Logistics |
| Xem lịch sử thay đổi trạng thái | Cho phép xem và theo dõi toàn bộ tiến trình trạng thái của một yêu cầu (Khởi tạo -> Chờ duyệt -> Đã duyệt). Hệ thống ghi nhận và hiển thị chi tiết mỗi mốc chuyển trạng thái bao gồm: thời điểm thay đổi, thao tác được thực hiện và người thực hiện, đảm bảo khả năng truy vết dữ liệu rành mạch. | Khách hàng, Quản lý Điều hành Logistics |

## Ghi chú Entity có thể phát sinh cho ERD

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `User` | `id`, `fullName`, `email`, `phone`, `passwordHash`, `role`, `status`, `createdAt`, `updatedAt` |
| `CustomerProfile` | `id`, `userId`, `clubName`, `avatarUrl`, `contactAddress`, `taxCode`, `createdAt`, `updatedAt` |
| `TransportRequest` | `id`, `requestCode`, `customerId`, `departureLocation`, `destinationLocation`, `requestedDepartureTime`, `totalHorses`, `currentStatus`, `approvedBy`, `approvedAt`, `rejectionReason`, `cancellationReason`, `createdAt`, `updatedAt` |
| `RequestHorseItem` | `id`, `requestId`, `horseName`, `breed`, `gender`, `age`, `microchipNumber`, `dietaryRequirements`, `stallPreference`, `specialInstructions` |
| `RequestStatusHistory` | `id`, `requestId`, `fromStatus`, `toStatus`, `action`, `reason`, `performedBy`, `performedAt` |
| `OtpVerification` | `id`, `userId`, `contactValue`, `otpCode`, `otpType`, `expiresAt`, `isUsed`, `createdAt` |

---

# Flow 1 - Account and Transport Request Management

## Use Cases

| UC Name | UC Description | Actor |
|---|---|---|
| Register Customer Account | Allows external users (Horse Owners, Racing Clubs) to provide details (Full Name, Email/Phone, Password, Club Name) to create a new account. The system validates uniqueness, sends a verification code (OTP/Link), and initializes the Customer profile. (Note: Internal staff accounts are provisioned, not self-registered). | Customer |
| Login and Role-based Redirection | Allows users to authenticate with Email/Phone and Password. The system verifies credentials, issues a session token, and redirects to the appropriate role-based interface (Customer, Manager, Driver, etc.). | All Actors (Customer, Logistics Manager, Transport Specialist, Fleet & Route Coordinator, Vehicle Driver) |
| Forgot and Reset Password | Allows users to request password recovery. Users submit their Email/Phone, receive an OTP, and set a new password upon successful verification. | All Actors |
| Change Password | Allows authenticated users to change their password by verifying their current password and submitting a new one. | All Actors |
| Update Profile | Allows users to view and update basic personal information (Avatar, Contact Phone, Address). The system audits updates and maintains the latest data. | All Actors |
| Logout | Allows users to end their current session. The system revokes tokens, clears access rights, and redirects to the Login screen. | All Actors |
| Create Transport Request | Allows Customers to initiate a horse transport request with departure, destination, and desired departure time. The system creates a request code, assigns "Pending Approval" status, and records creation time. | Customer |
| Manage Horses and Special Requirements | Allows Customers to add, remove, or edit horses for the trip and configure special requirements (vehicle type, feeding regimen, accompanying grooms). The system calculates total horse count automatically. | Customer |
| Update Transport Request | Allows Customers to modify request details while it remains in "Pending Approval" status. The system records the modification time and updates data version. | Customer |
| Cancel Transport Request | Allows Customers to cancel an unprocessed request with a mandatory reason. The system updates status to "Cancelled" and logs the reason and timestamp. | Customer |
| View Transport Request List | Allows users to browse transport requests with summary info. Customers view only their own requests; Logistics Managers view all requests across the system. | Customer, Logistics Manager |
| View Transport Request Detail | Allows users to view complete details of a specific request (customer info, schedule, horse count, horse details, special requirements, status, approver info). | Customer, Logistics Manager |
| Approve Transport Request | Allows Logistics Managers to assess feasibility and approve a request. Status transitions from "Pending Approval" to "Approved", capturing approver identity and timestamp. | Logistics Manager |
| Reject Transport Request | Allows Logistics Managers to reject an unfeasible request with a mandatory rejection reason. Status transitions to "Rejected" with reason and timestamp recorded. | Logistics Manager |
| View Request Status History | Allows users to track the end-to-end lifecycle and status transitions of a request (Created -> Pending Approval -> Approved). Details include timestamp, action, and operator for auditability. | Customer, Logistics Manager |

## Possible ERD Entities

| Entity | Suggested Fields |
|---|---|
| `User` | `id`, `fullName`, `email`, `phone`, `passwordHash`, `role`, `status`, `createdAt`, `updatedAt` |
| `CustomerProfile` | `id`, `userId`, `clubName`, `avatarUrl`, `contactAddress`, `taxCode`, `createdAt`, `updatedAt` |
| `TransportRequest` | `id`, `requestCode`, `customerId`, `departureLocation`, `destinationLocation`, `requestedDepartureTime`, `totalHorses`, `currentStatus`, `approvedBy`, `approvedAt`, `rejectionReason`, `cancellationReason`, `createdAt`, `updatedAt` |
| `RequestHorseItem` | `id`, `requestId`, `horseName`, `breed`, `gender`, `age`, `microchipNumber`, `dietaryRequirements`, `stallPreference`, `specialInstructions` |
| `RequestStatusHistory` | `id`, `requestId`, `fromStatus`, `toStatus`, `action`, `reason`, `performedBy`, `performedAt` |
| `OtpVerification` | `id`, `userId`, `contactValue`, `otpCode`, `otpType`, `expiresAt`, `isUsed`, `createdAt` |

---

# Flow 2 - Quản lý Hồ sơ Pháp lý và Kiểm dịch

## Danh sách Use Case

| Tên UC | Mô tả UC | Tác nhân |
|---|---|---|
| Quản lý danh mục quy định | Cho phép quản lý danh mục các quy định pháp lý, kiểm dịch, hải quan và các loại giấy tờ bắt buộc theo từng quốc gia, cửa khẩu hoặc tuyến vận chuyển. Transport Specialist có thể thêm, chỉnh sửa, xem và xóa các danh mục. | Chuyên viên Kiểm dịch |
| Quản lý các bộ hồ sơ mẫu | Cho phép Transport Specialist quản lý các bộ hồ sơ mẫu tương ứng với từng trường hợp vận chuyển, trong đó xác định sẵn các loại giấy tờ bắt buộc. Ví dụ, bộ hồ sơ trong nước gồm giấy kiểm dịch và giấy tờ nguồn gốc; bộ hồ sơ xuyên biên giới gồm hộ chiếu ngựa, giấy kiểm dịch, giấy tờ hải quan và giấy tờ nguồn gốc. Transport Specialist có thể tạo, xem, cập nhật, kích hoạt/ngừng sử dụng và xóa các bộ hồ sơ mẫu. | Chuyên viên Kiểm dịch |
| Áp dụng bộ hồ sơ mẫu cho chuyến vận chuyển | Cho phép Transport Specialist lựa chọn và áp dụng một bộ hồ sơ mẫu phù hợp cho chuyến vận chuyển. Hệ thống tự động tạo danh sách các giấy tờ cần có của chuyến dựa trên bộ hồ sơ được chọn và đánh dấu các giấy tờ trong bộ là bắt buộc. | Chuyên viên Kiểm dịch |
| Quản lý bộ hồ sơ pháp lý của tuyến vận chuyển | Cho phép Transport Specialist quản lý các yêu cầu hồ sơ pháp lý áp dụng cho từng tuyến vận chuyển. Hệ thống lưu các yêu cầu giấy tờ đặc thù của tuyến để có thể tự động bổ sung vào hồ sơ khi chuyến vận chuyển sử dụng tuyến đó. TS có thể thêm vào các giấy tờ phát sinh, yêu cầu đặc thù của chuyến, chỉnh sửa hoặc xóa các giấy tờ không nằm trong bộ mẫu bắt buộc. | Chuyên viên Kiểm dịch, Hệ thống |
| Xem bộ hồ sơ pháp lý của chuyến vận chuyển | Cho phép người dùng xem tình trạng tổng thể của bộ hồ sơ pháp lý, bao gồm các giấy tờ đã có, còn thiếu, đang chờ kiểm tra, cần bổ sung, đã được xác nhận và tình trạng xét duyệt từ cơ quan chức năng. Quyền xem chi tiết được giới hạn theo từng actor. | Chuyên viên Kiểm dịch, Quản lý Điều hành Logistics, Khách hàng |
| Tải lên giấy tờ pháp lý / y tế | Cho phép Customer tải lên các giấy tờ được yêu cầu cho từng con ngựa hoặc cho chuyến vận chuyển, ví dụ hộ chiếu ngựa, giấy chứng nhận sức khỏe, chứng nhận tiêm phòng hoặc giấy tờ sở hữu. Customer có thể cung cấp thêm thông tin như số giấy tờ, ngày cấp, ngày hết hạn và cơ quan cấp. | Khách hàng |
| Cập nhật hoặc thay thế giấy tờ đã tải lên | Cho phép Customer cung cấp phiên bản mới của một giấy tờ khi giấy tờ cũ bị sai, hết hạn, không hợp lệ hoặc được yêu cầu bổ sung lại. Hệ thống giữ lại các phiên bản cũ để đảm bảo khả năng truy vết lịch sử hồ sơ. | Khách hàng |
| Xác nhận giấy tờ hợp lệ | Cho phép Transport Specialist xác nhận một giấy tờ là hợp lệ sau khi đã kiểm tra đầy đủ và đáp ứng yêu cầu. Sau khi được xác nhận, giấy tờ được tính là hoàn thành trong checklist hồ sơ của chuyến vận chuyển. | Chuyên viên Kiểm dịch |
| Yêu cầu chỉnh sửa / cung cấp lại giấy tờ | Cho phép Transport Specialist yêu cầu Customer cung cấp lại một giấy tờ đã tải lên nhưng không hợp lệ, chẳng hạn như bị hết hạn, thiếu chữ ký, sai thông tin, file không rõ hoặc không đúng đối tượng. Transport Specialist phải ghi rõ lý do để Customer biết cần chỉnh sửa gì. | Chuyên viên Kiểm dịch |
| Yêu cầu bổ sung giấy tờ còn thiếu | Cho phép Transport Specialist xác định các giấy tờ bắt buộc mà Customer chưa cung cấp và gửi yêu cầu bổ sung. Yêu cầu có thể bao gồm danh sách giấy tờ còn thiếu, thời hạn cần bổ sung và nội dung hướng dẫn cho Customer. | Chuyên viên Kiểm dịch |
| Ghi nhận nộp bộ hồ sơ tới cơ quan chức năng | Cho phép Transport Specialist ghi nhận việc bộ hồ sơ đã được gửi tới cơ quan kiểm dịch, hải quan hoặc cơ quan quản lý có thẩm quyền bên ngoài hệ thống. Có thể lưu ngày nộp, mã tham chiếu hồ sơ, cơ quan tiếp nhận và file xác nhận nộp hồ sơ. | Chuyên viên Kiểm dịch |
| Cập nhật kết quả xét duyệt hồ sơ | Cho phép Transport Specialist cập nhật trạng thái xét duyệt của hồ sơ từ cơ quan chức năng, ví dụ đã nộp, đang xét duyệt, được phê duyệt hoặc bị từ chối. Nếu bị từ chối, hệ thống lưu lý do và yêu cầu chỉnh sửa để tiếp tục xử lý. | Chuyên viên Kiểm dịch |
| Ghi nhận kết quả kiểm dịch | Cho phép Transport Specialist ghi nhận kết quả kiểm tra kiểm dịch tại trạm hoặc cửa khẩu trong quá trình vận chuyển, ví dụ đạt yêu cầu, không đạt hoặc cần kiểm tra lại. Nếu phát sinh vấn đề, có thể lưu lý do, con ngựa bị ảnh hưởng và hướng xử lý tiếp theo. | Chuyên viên Kiểm dịch |
| Ghi nhận kết quả thông quan | Cho phép Transport Specialist cập nhật tình trạng xử lý hải quan của chuyến vận chuyển tại từng cửa khẩu, bao gồm đang chờ, đang xử lý, đã thông quan, bị giữ hoặc bị từ chối. Khi thông quan thành công, chuyến có thể tiếp tục sang chặng tiếp theo. | Chuyên viên Kiểm dịch |
| Theo dõi tiến độ hoàn thiện hồ sơ | Cho phép Transport Specialist và Logistics Manager theo dõi tổng quan mức độ hoàn thiện hồ sơ của nhiều chuyến vận chuyển, phát hiện các chuyến đang thiếu giấy tờ, có giấy tờ bị từ chối, hồ sơ sắp tới ngày khởi hành hoặc đang chờ cơ quan chức năng xét duyệt. | Chuyên viên Kiểm dịch, Quản lý Điều hành Logistics |
| Xem lịch sử xử lý hồ sơ | Cho phép xem toàn bộ lịch sử thao tác và thay đổi trạng thái của bộ hồ sơ, bao gồm ai đã tải giấy tờ, ai kiểm tra, thời điểm xác nhận, yêu cầu bổ sung, nộp hồ sơ và cập nhật kết quả xét duyệt. Mục đích là đảm bảo khả năng truy vết và kiểm soát quá trình xử lý hồ sơ. | Chuyên viên Kiểm dịch, Quản lý Điều hành Logistics |

## Ghi chú Entity có thể phát sinh cho ERD

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `RegulatoryRequirement` | `id`, `countryCode`, `checkpointId`, `routeId`, `category` (QUARANTINE, CUSTOMS, WELFARE), `documentType`, `title`, `description`, `isMandatory`, `validityPeriodDays`, `createdAt`, `updatedAt` |
| `DossierTemplate` | `id`, `templateCode`, `templateName`, `scope` (DOMESTIC, CROSS_BORDER_AIR, CROSS_BORDER_LAND), `description`, `isActive`, `createdAt`, `updatedAt` |
| `DossierTemplateItem` | `id`, `templateId`, `documentType`, `documentName`, `isMandatory`, `targetScope` (PER_HORSE, PER_TRIP), `instructions` |
| `TripLegalDossier` | `id`, `tripPlanId`, `templateId`, `overallStatus` (DRAFT, INCOMPLETE, SUBMITTED, CLEARED, REJECTED), `clearedForDeparture`, `clearedAt`, `clearedBy`, `createdAt`, `updatedAt` |
| `DossierDocumentItem` | `id`, `dossierId`, `horseId`, `documentType`, `documentName`, `documentNumber`, `issuingAuthority`, `issueDate`, `expiryDate`, `currentFileUrl`, `currentVersion`, `verificationStatus` (PENDING, VALID, REJECTED, EXPIRED), `rejectionReason`, `verifiedBy`, `verifiedAt` |
| `DocumentVersion` | `id`, `documentItemId`, `fileUrl`, `versionNumber`, `uploadedBy`, `uploadedAt`, `uploadNotes` |
| `AuthoritySubmission` | `id`, `dossierId`, `authorityName`, `authorityType` (QUARANTINE, CUSTOMS), `submissionReferenceCode`, `submittedAt`, `submittedBy`, `receiptFileUrl`, `reviewStatus` (SUBMITTED, UNDER_REVIEW, APPROVED, REJECTED), `decisionDate`, `decisionNotes` |
| `QuarantineRecord` | `id`, `dossierId`, `checkpointId`, `horseId`, `inspectionDate`, `result` (PASSED, FAILED, RE_TEST), `healthFindings`, `actionPlan`, `inspectorName`, `recordedBy` |
| `CustomsClearanceRecord` | `id`, `dossierId`, `borderGateId`, `declarationNumber`, `status` (PENDING, PROCESSING, CLEARED, HELD, REJECTED), `clearedAt`, `remarks`, `recordedBy` |
| `DossierAuditTrail` | `id`, `dossierId`, `documentItemId`, `action`, `performedBy`, `performedAt`, `details` |

---

# Flow 2 - Legal Documentation and Quarantine Clearance

## Use Cases

| UC Name | UC Description | Actor |
|---|---|---|
| Manage Regulatory Catalog | Manage legal, quarantine, and customs regulations and mandatory documents categorized by country, border checkpoint, or route. Transport Specialists can add, edit, view, and delete regulatory entries. | Transport Specialist |
| Manage Document Templates | Manage standardized document package templates for different transport scenarios (domestic, cross-border, etc.) specifying mandatory documents. Transport Specialists can create, view, update, activate/deactivate, and delete templates. | Transport Specialist |
| Apply Document Package to Transport Trip | Select and apply an appropriate document template to a transport trip. The system automatically initializes the trip document checklist and flags mandatory items. | Transport Specialist |
| Manage Route-Specific Legal Requirements | Manage unique legal documentation requirements for specific routes. The system automatically incorporates route-specific documents when a trip uses that route. Specialists can add ad-hoc documents, customize requirements, and adjust non-mandatory items. | Transport Specialist, System |
| View Trip Legal Documentation | View overall dossier readiness, including uploaded, missing, pending inspection, revision-requested, verified, and authority-review status. Detail visibility is governed by actor role. | Transport Specialist, Logistics Manager, Customer |
| Upload Legal and Health Documents | Allows Customers to upload required documents per horse or per trip (FEI passport, health certificate, vaccination record, ownership proof) along with document number, issuance/expiry dates, and issuing authority. | Customer |
| Update or Replace Uploaded Document | Allows Customers to submit new document versions when previous files are invalid, expired, or rejected. The system preserves historic versions for auditability. | Customer |
| Verify Document Validity | Allows Transport Specialists to review and verify uploaded documents against standards. Verified documents are marked as complete in the trip checklist. | Transport Specialist |
| Request Document Revision | Allows Transport Specialists to reject an invalid upload (expired, missing signatures, illegible scan, incorrect horse) with mandatory feedback explaining required corrections. | Transport Specialist |
| Request Missing Documents | Allows Transport Specialists to identify unfulfilled mandatory requirements and issue an addendum request with missing items list, submission deadline, and guidance. | Transport Specialist |
| Record Submission to Authorities | Record the submission of dossier packages to external quarantine bureaus, customs offices, or veterinary authorities (submission date, reference number, receiving agency, acknowledgment receipt). | Transport Specialist |
| Update Authority Review Result | Update official review status from government authorities (Submitted, Under Review, Approved, Rejected). Rejections capture reasons and rectification notes. | Transport Specialist |
| Record Quarantine Inspection Result | Record in-transit quarantine inspections at waypoints or border gates (Passed, Failed, Re-test). Incidents record affected horses, reasons, and corrective actions. | Transport Specialist |
| Record Customs Clearance Result | Update customs clearance status at border checkpoints (Pending, In Progress, Cleared, Held, Rejected). Clearance approval unlocks progression to the next leg. | Transport Specialist |
| Track Documentation Readiness | Allows Specialists and Managers to monitor multi-trip document completion, flagging missing files, rejections, approaching departure deadlines, and pending approvals. | Transport Specialist, Logistics Manager |
| View Document Audit Trail | Access the full audit history of dossier lifecycle events: uploads, reviews, verifications, revision requests, external submissions, and approval status transitions. | Transport Specialist, Logistics Manager |

## Possible ERD Entities

| Entity | Suggested Fields |
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

# Flow 4 - Cập nhật Trạng thái và Nhật ký Hành trình

## Danh sách Use Case

| Tên UC | Mô tả UC | Tác nhân |
|---|---|---|
| Xem tiến độ hành trình |  | Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |
| Cập nhật trạng thái mốc hành trình | Ghi nhận các mốc Đã khởi hành, Đến trạm nghỉ, Vào khu kiểm dịch, Đã thông quan, Đã hạ cánh hoặc Đến điểm đích. Hệ thống lưu thời gian và người cập nhật. | Tài xế / Nhân viên Đi kèm |
| Cập nhật vị trí thực tế | Ghi nhận vĩ độ, kinh độ, thời điểm và nguồn vị trí của chuyến đi. | Tài xế / Nhân viên Đi kèm |
| Xem vị trí thực tế |  | Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |
| Ghi nhật ký sức khỏe ngựa | Ghi nhận thể trạng từng con ngựa, gồm thân nhiệt, mức ăn, mức uống nước, tình trạng mất nước, hành vi hoặc mức độ căng thẳng và tình trạng chuồng. | Tài xế / Nhân viên Đi kèm |
| Ghi nhật ký chăm sóc ngựa trong hành trình | Ghi nhận hoạt động cho ăn, cho uống nước, kiểm tra chuồng hoặc đệm lót và xác nhận ngựa được giám sát tại điểm dừng hoặc trong chuyến bay. | Tài xế / Nhân viên Đi kèm |
| Thêm ghi chú và hình ảnh tình trạng ngựa |  | Tài xế / Nhân viên Đi kèm |
| Xem lịch sử cập nhật hành trình | Xem dòng thời gian gồm trạng thái mốc, vị trí, nhật ký sức khỏe, nhật ký chăm sóc, thời điểm và người cập nhật. | Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |
| Nhận thông báo cập nhật hành trình | Nhận thông báo khi chuyến đi đạt mốc mới hoặc có nhật ký sức khỏe mới. | Điều phối viên Đội xe và Lộ trình, Quản lý Điều hành Logistics, Khách hàng |

## Ghi chú Entity có thể phát sinh cho ERD

| Entity | Trường dữ liệu gợi ý |
|---|---|
| `RouteCheckpoint` | `id`, `tripPlanId`, `sequenceOrder`, `checkpointName`, `checkpointType`, `scheduledArrival`, `actualArrival`, `actualDeparture`, `currentStatus` |
| `CheckpointStatusHistory` | `id`, `checkpointId`, `status`, `note`, `updatedBy`, `updatedAt` |
| `HorseHealthLog` | `id`, `tripPlanId`, `horseId`, `checkpointId`, `bodyTemperature`, `appetiteStatus`, `hydrationStatus`, `behaviorStatus`, `stallCondition`, `observedBy`, `observedAt` |
| `HorseCareLog` | `id`, `tripPlanId`, `horseId`, `checkpointId`, `careType`, `quantity`, `result`, `performedBy`, `performedAt` |
| `HealthLogAttachment` | `id`, `healthLogId`, `fileUrl`, `fileType`, `caption`, `uploadedAt` |
| `LocationPing` | `id`, `tripPlanId`, `latitude`, `longitude`, `accuracy`, `source`, `recordedAt` |
| `JourneyNotification` | `id`, `tripPlanId`, `eventType`, `recipientId`, `channel`, `deliveryStatus`, `sentAt`, `readAt` |

## Nguồn tham khảo

| Nguồn | Nội dung áp dụng cho Flow 4 |
|---|---|
| [IRT](https://www.irt.com/) | Điểm dừng trung chuyển, giám sát ngựa tại mọi điểm dừng, chăm sóc bởi nhân viên đi kèm trong chuyến bay. |
| [NOSAWA](https://www.nosawa.co.jp/en/horse/) | Các mốc đến sân bay, dỡ thùng ngựa, hoàn tất hải quan, chuyển tới khu kiểm dịch và tiếp tục tới điểm đích. |
| [John Parker International](https://johnparkerinternational.com/) | Theo dõi liên tục bằng CCTV trên xe; nước và thức ăn có sẵn trong suốt hành trình. |
| [BBA Shipping](https://www.bbashipping.com/) | Chuồng trung chuyển, nhân viên chăm sóc chuyên nghiệp trên chuyến bay, thông quan và kiểm dịch sau nhập khẩu. |
| [Equitrans Logistics](https://www.equitranslogistics.com/horses/) | Nhân viên chăm sóc đi cùng ngựa xuyên suốt; các giai đoạn trước chuyến bay, trong chuyến bay, sau chuyến bay, kiểm dịch và hành trình tiếp nối. |

> Các form báo giá và liên hệ không được đưa vào vì thuộc Flow 1, không thuộc Flow 4.

---

# Flow 4 - Trip Status and Journey Logs

## Use Cases

| UC Name | UC Description | Actor |
|---|---|---|
| View Journey Progress |  | Fleet & Route Coordinator, Logistics Manager, Customer |
| Update Journey Milestone Status | Record Departed, Arrived at Rest Stop, Entered Quarantine, Customs Cleared, Landed, or Arrived at Destination. The system stores the update time and updating user. | Vehicle Driver / Escort |
| Update Actual Location | Record the trip's latitude, longitude, timestamp, and location source. | Vehicle Driver / Escort |
| View Actual Location |  | Fleet & Route Coordinator, Logistics Manager, Customer |
| Record Horse Health Log | Record each horse's body temperature, appetite, water intake, hydration condition, behavior or stress level, and stall condition. | Vehicle Driver / Escort |
| Record In-Transit Horse Care | Record feeding, watering, stall or bedding checks, and confirmation that the horse was supervised at a transit stop or during a flight. | Vehicle Driver / Escort |
| Add Horse Condition Notes and Images |  | Vehicle Driver / Escort |
| View Journey Update History | View a timeline of milestone statuses, locations, health logs, care logs, update times, and updating users. | Fleet & Route Coordinator, Logistics Manager, Customer |
| Receive Journey Update Notifications | Receive a notification when the trip reaches a new milestone or a new horse health log is recorded. | Fleet & Route Coordinator, Logistics Manager, Customer |

## Possible ERD Entities

| Entity | Suggested Fields |
|---|---|
| `RouteCheckpoint` | `id`, `tripPlanId`, `sequenceOrder`, `checkpointName`, `checkpointType`, `scheduledArrival`, `actualArrival`, `actualDeparture`, `currentStatus` |
| `CheckpointStatusHistory` | `id`, `checkpointId`, `status`, `note`, `updatedBy`, `updatedAt` |
| `HorseHealthLog` | `id`, `tripPlanId`, `horseId`, `checkpointId`, `bodyTemperature`, `appetiteStatus`, `hydrationStatus`, `behaviorStatus`, `stallCondition`, `observedBy`, `observedAt` |
| `HorseCareLog` | `id`, `tripPlanId`, `horseId`, `checkpointId`, `careType`, `quantity`, `result`, `performedBy`, `performedAt` |
| `HealthLogAttachment` | `id`, `healthLogId`, `fileUrl`, `fileType`, `caption`, `uploadedAt` |
| `LocationPing` | `id`, `tripPlanId`, `latitude`, `longitude`, `accuracy`, `source`, `recordedAt` |
| `JourneyNotification` | `id`, `tripPlanId`, `eventType`, `recipientId`, `channel`, `deliveryStatus`, `sentAt`, `readAt` |

## References

| Source | Flow 4 Findings Used |
|---|---|
| [IRT](https://www.irt.com/) | Transit stops, horse supervision at every stop, and in-flight care by travelling grooms. |
| [NOSAWA](https://www.nosawa.co.jp/en/horse/) | Airport arrival, horse-stall unloading, customs clearance, quarantine transfer, and onward travel to the destination. |
| [John Parker International](https://johnparkerinternational.com/) | Continuous onboard CCTV monitoring; water and feed available throughout the journey. |
| [BBA Shipping](https://www.bbashipping.com/) | Transit stabling, professional flying grooms, customs clearance, and post-import quarantine. |
| [Equitrans Logistics](https://www.equitranslogistics.com/horses/) | Travelling grooms accompany horses throughout pre-flight, in-flight, post-flight, quarantine, and onward journey stages. |

> Quote and contact forms are excluded because they belong to Flow 1, not Flow 4.
