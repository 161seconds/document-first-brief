# Danh Mục Use Cases — Hệ Thống Quản Lý Vận Chuyển Ngựa Đua Xuyên Quốc Gia
## Cross-Border Racehorse Transport System (`swp391-cross-border-racehorse-transport-system`)

> **Mã đề tài / Phụ trách**: `2 — HoangNT20`  
> **Dự án**: Cross-Border Racehorse Transport System  
> **Nguồn dữ liệu**: Excel `UC List Brainstorm (1).xlsx`  
> **Định dạng**: Đánh số Use Case duy nhất trên toàn bộ hệ thống (UC-01 đến UC-77)  

---

## 👥 Ma Trận 2 Nhóm Tác Nhân Chính (Actors & Responsibilities)

| Tác nhân (Actor) | Tên tiếng Việt | Phạm vi trách nhiệm cốt lõi |
|---|---|---|
| **Guest** | Khách | Truy cập các trang công khai (public pages) bao gồm trang chủ, trang đăng nhập và đăng ký, để xem các thông tin cơ bản về hệ thống và dịch vụ hiện có. |
| **Logged-in User** | Người dùng đã đăng nhập | Truy cập các tính năng và công cụ của hệ thống dựa trên vai trò và quyền hạn được phân bổ để thực hiện quy trình nghiệp vụ, quản lý dữ liệu và các hành động được ủy quyền. |

*(Ghi chú: **Logged-in User** là tác nhân cha của 6 vai trò cụ thể: Customer, Logistics Manager, Transport Specialist, Fleet & Route Coordinator, Vehicle Driver, và Escort).*

---

## Flow 1 - Quản lý Tài khoản và Yêu cầu Vận chuyển

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-01** | Đăng ký tài khoản khách hàng | Cho phép người dùng bên ngoài (Chủ ngựa, Câu lạc bộ) cung cấp thông tin (Họ tên, Email, Mật khẩu, Tên CLB) để tạo tài khoản mới. Hệ thống kiểm tra trùng lặp, gửi mã/link xác thực qua Email và khởi tạo hồ sơ Customer trên hệ thống. (Lưu ý: Nhân sự nội bộ không tự đăng ký mà sẽ được cấp tài khoản). | Guest |
| **UC-02** | Đăng nhập và phân quyền | Cho phép người dùng nhập thông tin xác thực (Email và Mật khẩu). Hệ thống kiểm tra dữ liệu, cấp phiên làm việc (token) và điều hướng giao diện (phân quyền) tương ứng với role của người dùng (Customer, Manager, Specialist, Coordinator, Driver, Escort...). | Guest, Logged-in User |
| **UC-03** | Quên / Khôi phục mật khẩu | Cho phép người dùng yêu cầu khôi phục quyền truy cập khi quên mật khẩu. Người dùng nhập Email, hệ thống gửi mã xác thực hoặc link khôi phục qua Email. Sau khi xác thực thành công, cho phép người dùng đặt lại mật khẩu mới. | Guest |
| **UC-04** | Thay đổi mật khẩu | Cho phép người dùng đang trong phiên đăng nhập (đã xác thực) đổi mật khẩu tài khoản để tăng tính bảo mật. Người dùng cần nhập đúng mật khẩu cũ và xác nhận mật khẩu mới. | Logged-in User |
| **UC-05** | Cập nhật hồ sơ cá nhân (Profile) | Cho phép người dùng xem và chỉnh sửa các thông tin cá nhân cơ bản (Ảnh đại diện, Địa chỉ liên hệ, Thông tin liên lạc). Hệ thống lưu vết chỉnh sửa và cập nhật dữ liệu mới nhất. | Logged-in User |
| **UC-06** | Đăng xuất hệ thống | Cho phép người dùng chủ động chấm dứt phiên làm việc hiện tại. Hệ thống tiến hành xóa token, hủy quyền truy cập và điều hướng người dùng về lại màn hình Đăng nhập. | Logged-in User |
| **UC-07** | Tạo yêu cầu vận chuyển mới | Cho phép Customer khởi tạo một yêu cầu vận chuyển ngựa. Customer cần nhập đầy đủ thông tin: điểm đi, điểm đến, và thời gian mong muốn khởi hành. Hệ thống tạo mã yêu cầu, gắn trạng thái mặc định là "Chờ duyệt" và tự động ghi nhận thời điểm tạo yêu cầu. | Customer |
| **UC-08** | Quản lý danh sách ngựa và yêu cầu đặc biệt của riêng khách hàng | Cho phép Customer thêm, xóa hoặc chỉnh sửa danh sách những con ngựa sẽ tham gia vào chuyến vận chuyển. Đồng thời, Customer có thể thiết lập các yêu cầu đặc biệt cho từng con ngựa hoặc toàn chuyến (ví dụ: loại xe chuyên dụng, chế độ ăn, hoặc yêu cầu người đi kèm). Hệ thống tự động tính toán tổng số lượng ngựa trong yêu cầu. | Customer |
| **UC-09** | Cập nhật thông tin yêu cầu | Cho phép Customer thay đổi thông tin của yêu cầu (điểm đi, điểm đến, thời gian, danh sách ngựa, yêu cầu đặc biệt) với điều kiện yêu cầu đó vẫn đang ở trạng thái "Chờ duyệt". Hệ thống sẽ lưu lại thời điểm chỉnh sửa và cập nhật phiên bản dữ liệu mới nhất. | Customer |
| **UC-10** | Hủy yêu cầu vận chuyển | Cho phép Customer chủ động hủy bỏ một yêu cầu vận chuyển khi hệ thống chưa xử lý. Customer bắt buộc phải nhập lý do hủy. Hệ thống sẽ chuyển trạng thái sang "Hủy", ghi nhận lý do và thời điểm thực hiện thao tác vào lịch sử yêu cầu. | Customer |
| **UC-11** | Xem danh sách yêu cầu vận chuyển | Cho phép người dùng xem danh sách các yêu cầu vận chuyển với các thông tin cơ bản. Customer chỉ xem được các yêu cầu do chính mình tạo. Logistics Manager được xem toàn bộ danh sách trên hệ thống để phục vụ việc phân loại, theo dõi và xử lý. | Customer, Logistics Manager |
| **UC-12** | Xem chi tiết yêu cầu vận chuyển | Cho phép người dùng truy xuất toàn bộ thông tin chi tiết của một yêu cầu cụ thể. Dữ liệu hiển thị có thể bao gồm: thông tin khách hàng đặt, lịch trình mong muốn, tổng số lượng ngựa, chi tiết từng con ngựa, các yêu cầu đặc biệt, trạng thái hiện tại và người phê duyệt (nếu có). | Customer, Logistics Manager |
| **UC-13** | Phê duyệt yêu cầu vận chuyển | Cho phép Logistics Manager kiểm tra tính khả thi của yêu cầu và tiến hành phê duyệt. Khi thao tác, hệ thống chuyển trạng thái yêu cầu từ "Chờ duyệt" sang "Đã duyệt", đồng thời lưu trữ thông tin định danh của người phê duyệt và thời điểm thực hiện phê duyệt. | Logistics Manager |
| **UC-14** | Từ chối yêu cầu vận chuyển | Cho phép Logistics Manager từ chối tiếp nhận một yêu cầu vận chuyển nếu không đủ điều kiện. Logistics Manager bắt buộc phải nhập lý do từ chối để phản hồi. Trạng thái yêu cầu chuyển thành "Từ chối", hệ thống lưu lại thông tin người thao tác, lý do cụ thể và thời điểm thực hiện. | Logistics Manager |
| **UC-15** | Xem lịch sử thay đổi trạng thái | Cho phép xem và theo dõi toàn bộ tiến trình trạng thái của một yêu cầu (Khởi tạo -> Chờ duyệt -> Đã duyệt). Hệ thống ghi nhận và hiển thị chi tiết mỗi mốc chuyển trạng thái bao gồm: thời điểm thay đổi, thao tác được thực hiện và người thực hiện, đảm bảo khả năng truy vết dữ liệu rành mạch. | Customer, Logistics Manager |

---

## Flow 2 - Quản lý Hồ sơ Pháp lý & Kiểm dịch Thông quan

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-16** | Quản lý danh mục quy định ** | Cho phép quản lý danh mục các quy định pháp lý, kiểm dịch, hải quan và các loại giấy tờ bắt buộc theo từng quốc gia, cửa khẩu hoặc tuyến vận chuyển. Transport Specialist có thể thêm, chỉnh sửa, xem và xóa các danh mục. | Transport Specialist |
| **UC-17** | Quản lý các bộ hồ sơ mẫu | Cho phép Transport Specialist quản lý các bộ hồ sơ mẫu tương ứng với từng trường hợp vận chuyển, trong đó xác định sẵn các loại giấy tờ bắt buộc. Ví dụ, bộ hồ sơ trong nước gồm giấy kiểm dịch và giấy tờ nguồn gốc; bộ hồ sơ xuyên biên giới gồm hộ chiếu ngựa, giấy kiểm dịch, giấy tờ hải quan và giấy tờ nguồn gốc. Transport Specialist có thể tạo, xem, cập nhật, kích hoạt/ngừng sử dụng và xóa các bộ hồ sơ mẫu. | Transport Specialist |
| **UC-18** | Áp dụng bộ hồ sơ mẫu cho chuyến vận chuyển | Cho phép Transport Specialist lựa chọn và áp dụng một bộ hồ sơ mẫu phù hợp cho chuyến vận chuyển. Hệ thống tự động tạo danh sách các giấy tờ cần có của chuyến dựa trên bộ hồ sơ được chọn và đánh dấu các giấy tờ trong bộ là bắt buộc. | Transport Specialist |
| **UC-19** | Quản lý bộ hồ sơ pháp lý của tuyến vận chuyển | Cho phép Transport Specialist quản lý các yêu cầu hồ sơ pháp lý áp dụng cho từng tuyến vận chuyển. Hệ thống lưu các yêu cầu giấy tờ đặc thù của tuyến để có thể tự động bổ sung vào hồ sơ khi chuyến vận chuyển sử dụng tuyến đó. TS có thể thêm vào các giấy tờ phát sinh, yêu cầu đặc thù của chuyến, chỉnh sửa hoặc xóa các giấy tờ không nằm trong bộ mẫu bắt buộc | Transport Specialist / System |
| **UC-20** | Xem bộ hồ sơ pháp lý của chuyến vận chuyển | Cho phép người dùng xem tình trạng tổng thể của bộ hồ sơ pháp lý, bao gồm các giấy tờ đã có, còn thiếu, đang chờ kiểm tra, cần bổ sung, đã được xác nhận và tình trạng xét duyệt từ cơ quan chức năng. Quyền xem chi tiết được giới hạn theo từng actor. | Transport Specialist, Logistics Manager, Customer |
| **UC-21** | Tải lên giấy tờ pháp lý / y tế | Cho phép Customer tải lên các giấy tờ được yêu cầu cho từng con ngựa hoặc cho chuyến vận chuyển, ví dụ hộ chiếu ngựa, giấy chứng nhận sức khỏe, chứng nhận tiêm phòng hoặc giấy tờ sở hữu. Customer có thể cung cấp thêm thông tin như số giấy tờ, ngày cấp, ngày hết hạn và cơ quan cấp. | Customer |
| **UC-22** | Cập nhật hoặc thay thế giấy tờ đã tải lên | Cho phép Customer cung cấp phiên bản mới của một giấy tờ khi giấy tờ cũ bị sai, hết hạn, không hợp lệ hoặc được yêu cầu bổ sung lại. Hệ thống giữ lại các phiên bản cũ để đảm bảo khả năng truy vết lịch sử hồ sơ. | Customer |
| **UC-23** | Xác nhận giấy tờ hợp lệ | Cho phép Transport Specialist xác nhận một giấy tờ là hợp lệ sau khi đã kiểm tra đầy đủ và đáp ứng yêu cầu. Sau khi được xác nhận, giấy tờ được tính là hoàn thành trong checklist hồ sơ của chuyến vận chuyển. | Transport Specialist |
| **UC-24** | Yêu cầu chỉnh sửa / cung cấp lại giấy tờ * | Cho phép Transport Specialist yêu cầu Customer cung cấp lại một giấy tờ đã tải lên nhưng không hợp lệ, chẳng hạn như bị hết hạn, thiếu chữ ký, sai thông tin, file không rõ hoặc không đúng đối tượng. Transport Specialist phải ghi rõ lý do để Customer biết cần chỉnh sửa gì. | Transport Specialist |
| **UC-25** | Yêu cầu bổ sung giấy tờ còn thiếu * | Cho phép Transport Specialist xác định các giấy tờ bắt buộc mà Customer chưa cung cấp và gửi yêu cầu bổ sung. Yêu cầu có thể bao gồm danh sách giấy tờ còn thiếu, thời hạn cần bổ sung và nội dung hướng dẫn cho Customer. | Transport Specialist |
| **UC-26** | Ghi nhận nộp bộ hồ sơ tới cơ quan chức năng ** | Cho phép Transport Specialist ghi nhận việc bộ hồ sơ đã được gửi tới cơ quan kiểm dịch, hải quan hoặc cơ quan quản lý có thẩm quyền bên ngoài hệ thống. Có thể lưu ngày nộp, mã tham chiếu hồ sơ, cơ quan tiếp nhận và file xác nhận nộp hồ sơ. | Transport Specialist |
| **UC-27** | Cập nhật kết quả xét duyệt hồ sơ *** | Cho phép Transport Specialist cập nhật trạng thái xét duyệt của hồ sơ từ cơ quan chức năng, ví dụ đã nộp, đang xét duyệt, được phê duyệt hoặc bị từ chối. Nếu bị từ chối, hệ thống lưu lý do và yêu cầu chỉnh sửa để tiếp tục xử lý. | Transport Specialist |
| **UC-28** | Ghi nhận kết quả kiểm dịch | Cho phép Transport Specialist ghi nhận kết quả kiểm tra kiểm dịch tại trạm hoặc cửa khẩu trong quá trình vận chuyển, ví dụ đạt yêu cầu, không đạt hoặc cần kiểm tra lại. Nếu phát sinh vấn đề, có thể lưu lý do, con ngựa bị ảnh hưởng và hướng xử lý tiếp theo. | Transport Specialist |
| **UC-29** | Ghi nhận kết quả thông quan | Cho phép Transport Specialist cập nhật tình trạng xử lý hải quan của chuyến vận chuyển tại từng cửa khẩu, bao gồm đang chờ, đang xử lý, đã thông quan, bị giữ hoặc bị từ chối. Khi thông quan thành công, chuyến có thể tiếp tục sang chặng tiếp theo. | Transport Specialist |
| **UC-30** | Theo dõi tiến độ hoàn thiện hồ sơ | Cho phép Transport Specialist và Logistics Manager theo dõi tổng quan mức độ hoàn thiện hồ sơ của nhiều chuyến vận chuyển, phát hiện các chuyến đang thiếu giấy tờ, có giấy tờ bị từ chối, hồ sơ sắp tới ngày khởi hành hoặc đang chờ cơ quan chức năng xét duyệt. | Transport Specialist, Logistics Manager |
| **UC-31** | Xem lịch sử xử lý hồ sơ | Cho phép xem toàn bộ lịch sử thao tác và thay đổi trạng thái của bộ hồ sơ, bao gồm ai đã tải giấy tờ, ai kiểm tra, thời điểm xác nhận, yêu cầu bổ sung, nộp hồ sơ và cập nhật kết quả xét duyệt. Mục đích là đảm bảo khả năng truy vết và kiểm soát quá trình xử lý hồ sơ. | Transport Specialist, Logistics Manager |

---

## Flow 3 - Lập Kế hoạch Lộ trình & Điều phối Phương tiện

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-32** | Xem danh sách yêu cầu vận chuyển đã được phê duyệt | Xem các yêu cầu vận chuyển đã được Quản lý Điều hành Logistics phê duyệt và đủ điều kiện để bắt đầu lập kế hoạch lộ trình. Thông tin có thể bao gồm khách hàng, danh sách ngựa, nơi đi, nơi đến, ngày mong muốn khởi hành, yêu cầu đặc biệt và loại vận chuyển trong nước hoặc quốc tế | Fleet & Route Coordinator |
| **UC-33** | Tạo lộ trình | Xây dựng lộ trình di chuyển tối ưu, xác định các điểm dừng nghỉ, trạm kiểm dịch và trạm tiếp nhiên liệu cho một yêu cầu vận chuyển đã được duyệt | Fleet & Route Coordinator |
| **UC-34** | Tạo kế hoạch tổng quát | Tạo kế hoạch tổng quát dựa trên một yêu cầu vận chuyển đã được phê duyệt. Kế hoạch là nơi tổng hợp tuyến đường, phương tiện, lộ trình, lịch trình và ngày đến dự kiến; đồng thời xác định phương án vận chuyển phù hợp dựa trên số lượng ngựa, sức chứa phương tiện và trạng thái sẵn sàng tại thời điểm dự kiến vận chuyển. | Logistics Manager |
| **UC-35** | Chỉnh sửa kế hoạch tổng quát | Thay đổi bản kế hoạch tổng quát do Manager lập | Logistics Manager |
| **UC-36** | Phân công nhân sự | Phân công nhân sự phụ trách thực hiện chuyến đi và từng chặng vận chuyển theo vai trò, nhiệm vụ và thời gian đã được xác định trong kế hoạch; theo dõi tình trạng phân công và khả năng đáp ứng của nhân sự. | Logistics Manager |
| **UC-37** | Quản lý lộ trình dự phòng | Điều phối viên có thể tạo, xem, cập nhật hoặc xóa một hoặc nhiều lộ trình dự phòng cho tuyến chính trong quá trình lập kế hoạch chuyến đi. Lộ trình dự phòng là tùy chọn, có thể áp dụng cho toàn tuyến hoặc một chặng cụ thể có rủi ro, và được sử dụng khi tuyến chính không thể tiếp tục. Nếu không có lộ trình dự phòng phù hợp hoặc tất cả lộ trình dự phòng đều không khả thi trong quá trình vận chuyển, việc lập lộ trình thay thế thủ công sẽ được xử lý | Fleet & Route Coordinator |
| **UC-38** | Xem lộ trình | Xem toàn bộ thông tin của kế hoạch lộ trình, bao gồm tuyến đường, phương tiện, các điểm dừng, lịch trình dự kiến, hãng hàng không nếu có và nhân sự được phân công | Fleet & Route Coordinator, Logistics Manager |
| **UC-39** | Cập nhật lộ trình | Chỉnh sửa thông tin kế hoạch lộ trình khi kế hoạch chưa được phê duyệt, hoặc khi bị Logistics Manager từ chối/yêu cầu điều chỉnh hoặc khi Coordinator chủ động phát hiện thay đổi cần cập nhật trước khi khởi hành, hoặc khi phát hiện có sự cố dọc đường. Sau khi cập nhật, hệ thống tự động gửi yêu cầu Manager phê duyệt lại | Fleet & Route Coordinator |
| **UC-40** | Hủy  lộ trình đang soạn thảo | Hủy một kế hoạch chưa được phê duyệt khi kế hoạch được tạo nhầm hoặc không còn được sử dụng. Việc hủy không đồng nghĩa với hủy yêu cầu vận chuyển hoặc hủy chuyến đã được phê duyệt | Fleet & Route Coordinator |
| **UC-41** | Quản lý đối tác vận chuyển | Quản lý các đơn vị vận chuyển bên ngoài mà công ty có thể hợp tác, bao gồm hãng hàng không và đơn vị vận tải đường bộ thuê ngoài, cùng thông tin loại dịch vụ mà đối tác cung cấp | Logistics Manager |
| **UC-42** | Lựa chọn dịch vụ vận chuyển thuê ngoài | Lựa chọn đối tác và dịch vụ vận chuyển phù hợp cho chặng không sử dụng phương tiện nội bộ, dựa trên tuyến, lịch trình, sức chứa và khả năng phục vụ vận chuyển ngựa | Logistics Manager |
| **UC-43** | Xem phương tiện chuyên dụng | Quản lý các phương tiện sử dụng để vận chuyển ngựa như xe tải chuyên dụng và khoang máy bay. Thông tin quản lý bao gồm loại phương tiện, sức chứa và trạng thái sẵn sàng | Fleet & Route Coordinator |
| **UC-44** | Thêm phương tiện chuyên dụng | Thêm một phương tiện mới vào hệ thống để có thể sử dụng trong các chuyến vận chuyển sau này | Fleet & Route Coordinator |
| **UC-45** | Cập nhật thông tin phương tiện | Cập nhật thông tin như sức chứa, biển số, loại phương tiện, ghi chú vận hành hoặc các thông tin kỹ thuật liên quan | Fleet & Route Coordinator |
| **UC-46** | Cập nhật trạng thái phương tiện | Thay đổi thủ công trạng thái của phương tiện giữa: sẵn sàng, bảo trì, tạm ngưng sử dụng. Các trạng thái "đã phân công" và "đang vận chuyển" không thao tác thủ công ở đây mà do hệ thống tự động cập nhật | Fleet & Route Coordinator |
| **UC-47** | Phê duyệt lộ trình cho một yêu cầu vận chuyển trước khi khởi hành | Phê duyệt lộ trình do điều phối viên lập riêng cho một yêu cầu vận chuyển, hoặc từ chối và yêu cầu chỉnh sửa thêm trước khi khởi hành | Logistics Manager |
| **UC-48** | Phê duyệt thay đổi lộ trình cho một yêu cầu vận chuyển trong lúc vận chuyển | Phê duyệt yêu cầu thay đổi lộ trình do điều phối viên gửi cho một yêu cầu vận chuyển, khi điều phối viên phát hiện có vấn đề phát sinh dọc đường | Logistics Manager |

---

## Flow 4 - Cập nhật Trạng thái & Nhật ký Hành trình

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-49** | Xem tiến độ hành trình | Xem danh sách các chặng trong hành trình, trạng thái hiện tại, thời gian cập nhật gần nhất và người cập nhật. | Fleet & Route Coordinator, Logistics Manager, Customer, Vehicle Driver, Escort |
| **UC-50** | Cập nhật trạng thái chặng | Cập nhật trạng thái của từng chặng thành Khởi hành, Đến điểm dừng, Đã thông quan hoặc Đã giao. Hệ thống ghi nhận thời gian và người cập nhật. | Vehicle Driver |
| **UC-51** | Ghi nhật ký sức khỏe ngựa | Ghi nhận tình trạng từng con ngựa trong hành trình, gồm Ổn định, Bỏ ăn, Căng thẳng hoặc Bị ảnh hưởng bởi thay đổi khí hậu. Hệ thống lưu thời điểm ghi nhận. | Escort |
| **UC-52** | Thêm ghi chú và hình ảnh tình trạng ngựa | Thêm mô tả và hình ảnh thực tế vào nhật ký sức khỏe để làm bằng chứng về tình trạng ngựa tại thời điểm kiểm tra. | Escort |
| **UC-53** | Theo dõi vị trí thực tế | Ghi nhận và hiển thị vị trí hiện tại của chuyến đi dựa trên tọa độ GPS chia sẻ từ thiết bị di động (qua giao diện Web Responsive) của Tài xế khi bật chức năng theo dõi realtime (không sử dụng cảm biến IoT phần cứng). | Vehicle Driver, Fleet & Route Coordinator, Logistics Manager, Customer |
| **UC-54** | Xem trạng thái ngựa | Xem trạng thái sức khỏe từng con ngựa trên hành trình, người ghi nhận, thời điểm ghi nhận | Escort, Fleet & Route Coordinator, Logistics Manager, Customer |

---

## Flow 5 - Xử lý Sự cố & Điều chỉnh Lộ trình Khẩn cấp

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-55** | Báo cáo sự cố phương tiện/giao thông | Tài xế báo cáo sự cố liên quan đến xe hoặc giao thông (hỏng xe, tai nạn, kẹt xe) kèm mức độ nghiêm trọng, thời gian và tóm tắt từ hiện trường | Vehicle Driver |
| **UC-56** | Báo cáo sự cố sức khỏe ngựa khẩn cấp | Escort báo cáo tình trạng sức khỏe ngựa thay đổi đột ngột hoặc nguy cấp cần xử lý ngay | Escort |
| **UC-57** | Nhận thông báo cảnh báo khẩn cấp | Quản lý nhận cảnh báo tức thời khi có sự cố khẩn cấp được báo cáo, để kịp thời theo dõi và ra quyết định phê duyệt | Logistics Manager |
| **UC-58** | Quyết định lộ trình thủ công khi không thể xử lí tự động | Tính toán (tự động hoặc thủ công) tuyến đường tránh tối ưu và thiết lập lại lộ trình khi có sự cố khẩn cấp | Fleet & Route Coordinator |
| **UC-59** | Đề xuất chi phí phát sinh khẩn cấp | Tạo yêu cầu chi phí phát sinh do thay đổi lộ trình khẩn cấp, đổi xe hoặc điều động bác sĩ thú y. Nếu chi phí chưa vượt quá budget ứng đối trong quy định công ty thì chỉ cần ghi nhận để hậu kiểm, không cần đề xuất duyệt | Fleet & Route Coordinator, Vehicle Driver, Escort |
| **UC-60** | Ghi nhận sử dụng chi phí phát sinh | Ghi nhận lại số tiền, lí do sử dụng, chi cho người nào, đơn hàng nào để kiểm tra về sau. Nếu số tiền vượt quá budget cho chuyến thì bắt buộc tạo yêu cầu duyệt khẩn | Vehicle Driver, Escort |
| **UC-61** | Phê duyệt chi phí phát sinh khẩn cấp | Phê duyệt yêu cầu chi phí phát sinh khẩn cấp do Điều phối viên hoặc Tài xế/Escort đề xuất | Logistics Manager |

---

## Flow 6 - Bàn giao, Khiếu nại, Tài chính & Báo cáo Quản trị

### Danh sách Use Case

| Mã UC | Tên Use Case | Mô tả Use Case | Tác nhân (Actor) |
|:---:|---|---|---|
| **UC-62** | Xử lý bàn giao cuối cùng | Ghi nhận chữ ký người nhận, thời gian bàn giao và tình trạng sức khỏe ngựa tại điểm đến cuối cùng | Vehicle Driver, Escort |
| **UC-63** | Gửi khiếu nại/claim khi nhận hàng | Khách hàng gửi khiếu nại hoặc yêu cầu bồi thường nếu phát hiện ngựa bị tổn thương, stress nặng hoặc sai lệch so với cam kết khi nhận bàn giao | Customer |
| **UC-64** | Xử lý khiếu nại/claim | Tiếp nhận, xác minh và phản hồi/giải quyết khiếu nại hoặc yêu cầu bồi thường từ khách hàng | Logistics Manager |
| **UC-65** | Gửi thông báo hoàn tất tự động | Tự động gửi Email và thông báo trên hệ thống Web (Web Notification) thông báo hành trình hoàn tất và ngựa đến nơi an toàn cho khách hàng và nội bộ | System |
| **UC-66** | Quản lý tổng hợp thu chi thực tế | Hệ thống tổng hợp các khoản chi phí của một Booking đã hoàn tất: cước vận chuyển theo tuyến, phí theo số lượng ngựa, phí hồ sơ pháp lý/kiểm dịch, và các chi phí phát sinh khẩn cấp đã được phê duyệt. Kết quả là danh sách các dòng phí đề xuất để lập hóa đơn. | System, Logistics Manager |
| **UC-67** | Lập hóa đơn thanh toán | Logistics Manager lập hóa đơn và có thể thêm, sửa, xóa dòng phí, chọn loại hóa đơn và đặt hạn thanh toán. Hệ thống sinh mã hóa đơn, gắn trạng thái "Nháp". | Logistics Manager |
| **UC-68** | Phát hành hóa đơn cho khách hàng | Chuyển hóa đơn từ "Nháp" sang "Đã phát hành", khóa nội dung hóa đơn và gửi thông báo tới Customer. Hệ thống ghi nhận người phát hành và thời điểm phát hành. | Logistics Manager |
| **UC-69** | Xem danh sách / chi tiết hóa đơn | Cho phép xem hóa đơn kèm từng dòng phí, tổng tiền cần thanh toán, và hạn thanh toán. Customer chỉ xem được hóa đơn thuộc Booking do mình tạo; Logistics Manager xem toàn bộ. | Customer, Logistics Manager |
| **UC-70** | Ghi nhận thanh toán | Cho phép Logistics Manager ghi nhận thanh toán của khách hàng (số tiền, phương thức, thời điểm, mã giao dịch đối chiếu). Hệ thống cập nhật và tự động chuyển trạng thái hóa đơn sang "Đã thanh toán", đồng thời sinh biên nhận tương ứng gửi cho Customer. | Logistics Manager |
| **UC-71** | Điều chỉnh / hủy hóa đơn | Cho phép hủy hóa đơn lập sai khi chưa phát sinh thanh toán, hoặc điều chỉnh khi có sai lệch. Bắt buộc nhập lý do. Hệ thống lưu vết người thao tác và thời điểm. | Logistics Manager |
| **UC-72** | Xem lịch sử trạng thái hóa đơn | Xem toàn bộ tiến trình trạng thái của hóa đơn kèm người thực hiện và thời điểm, đảm bảo khả năng truy vết tài chính. | Customer, Logistics Manager |
| **UC-73** | Xem báo cáo tỷ lệ đúng giờ | Xem báo cáo thống kê tỷ lệ chuyến đi hoàn thành đúng thời hạn cam kết (On-Time Delivery) | Logistics Manager |
| **UC-74** | Xem báo cáo tần suất sự cố | Xem thống kê số lượng và loại sự cố phát sinh trong các chuyến vận chuyển theo thời gian | Logistics Manager |
| **UC-75** | Xem báo cáo thời gian vận chuyển trung bình | Xem thống kê số ngày vận chuyển trung bình theo tuyến đường hoặc loại đơn hàng | Logistics Manager |
| **UC-76** | Xem báo cáo tỷ lệ sử dụng nguồn lực | Xem thống kê tỷ lệ sử dụng xe, container, thiết bị so với tổng nguồn lực sẵn có | Logistics Manager |
| **UC-77** | Xem lịch sử vận chuyển | Để đảm bảo an toàn cho các con ngựa tham gia các cuộc đua, cũng như tuân thủ các quy định pháp luật, chức năng này cho phép lưu trữ và xuất khẩu toàn bộ thông tin liên quan: thời điểm, người vận chuyển, tuyến đường vận chuyển, tình trạng sức khỏe của ngựa vào thời điểm đó, cũng như các giấy tờ liên quan. (Lý do: Để đảm bảo trách nhiệm bồi thường và tính minh bạch về mặt pháp lý trong trường hợp xảy ra sự cố khi vận chuyển những con ngựa có giá trị cao ra nước ngoài.) | Logistics Manager |

---
