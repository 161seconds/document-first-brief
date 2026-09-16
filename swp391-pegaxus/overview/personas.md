# Báo cáo Xác định Chân dung Người dùng (Personas)
**Dự án:** Hệ thống Quản lý Vận chuyển Ngựa đua Xuyên Quốc gia (Cross-Border Racehorse Transport System)

---

## Nhóm Guest (Khách)

### 1. Guest (Khách Vãng Lai)

* **Tên đại diện (Persona Name):** Khách hàng tiềm năng
* **Chức danh:** Người dùng chưa đăng nhập
* **Mô tả vai trò (Role Summary):** Khách vãng lai truy cập các trang công khai (public pages) bao gồm trang chủ, trang đăng nhập và đăng ký, để xem các thông tin cơ bản về hệ thống và dịch vụ hiện có.
* **Mục tiêu chính (Goals):**
  * Tìm hiểu thông tin dịch vụ vận chuyển ngựa đua.
  * Truy cập hệ thống hoặc đăng ký tài khoản nhanh chóng.
* **Chức năng chính trên hệ thống:**
  * Xem thông tin dịch vụ, tra cứu thông tin chung trên trang chủ.
  * Đăng ký tài khoản Khách hàng (Customer).
  * Đăng nhập vào hệ thống.
  * Khôi phục mật khẩu (Quên mật khẩu).

---

## Nhóm Logged-in User (Người dùng đã đăng nhập)
Người dùng truy cập các tính năng và công cụ của hệ thống dựa trên vai trò và quyền hạn được phân bổ. Bao gồm 6 vai trò cụ thể:

### 2. Logistics Manager (Quản lý Điều hành Logistics)

* **Tên đại diện (Persona Name):** Trần Anh Tuấn (Mr. Tuấn)
* **Chức danh:** Quản lý Điều hành Logistics Vận tải Quốc tế
* **Mô tả vai trò (Role Summary):** Là người chịu trách nhiệm cao nhất về hoạt động điều hành các chuyến vận chuyển ngựa đua trong và ngoài nước. Đảm bảo toàn bộ quy trình diễn ra đúng tiến độ, tối ưu chi phí, tuân thủ pháp luật và an toàn tuyệt đối cho tài sản/chiến mã.
* **Mục tiêu chính (Goals):**
  * Duy trì chỉ số hoàn thành chuyến đi đúng giờ (*On-time delivery*) ở mức cao nhất.
  * Tối ưu hóa chi phí vận hành, chi phí phương tiện và lịch trình trung chuyển.
  * Xử lý linh hoạt và kịp thời các biến cố phát sinh trong quá trình vận chuyển.
* **Thách thức (Pain Points):**
  * Quản lý rủi ro giao thông, thời tiết và thủ tục hải quan tại các cửa khẩu quốc tế.
  * Phải liên tục phối hợp và giám sát nhiều bên (Chuyên viên thủ tục, Điều phối viên, Tài xế/Escort).
  * Thiếu công cụ tổng hợp báo cáo chi phí và hiệu suất thời gian thực.
* **Chức năng chính trên hệ thống:**
  * Tiếp nhận & Phê duyệt các đơn hàng vận chuyển ngựa đua.
  * Lập kế hoạch vận chuyển tổng thể (lựa chọn tuyến đường, phương tiện, hãng hàng không, điểm trung chuyển).
  * Phân công nhiệm vụ cho Chuyên viên thủ tục, Điều phối viên, Tài xế & Escort.
  * Phê duyệt các phương án thay đổi lộ trình hoặc chi phí phát sinh khẩn cấp.
  * Xem báo cáo doanh thu, chi phí vận hành và chỉ số hiệu suất chuyến đi.

---

### 3. Transport Specialist (Chuyên viên Thủ tục & Kiểm dịch)

* **Tên đại diện (Persona Name):** Lê Minh Hương (Ms. Hương)
* **Chức danh:** Chuyên viên Thủ tục Pháp lý & Kiểm dịch Động vật
* **Mô tả vai trò (Role Summary):** Đảm bảo tính hợp pháp và đầy đủ của hồ sơ kiểm dịch, hộ chiếu, tiêm phòng và thủ tục hải quan cho từng chiến mã trước và trong quá trình xuất nhập cảnh.
* **Mục tiêu chính (Goals):**
  * Đảm bảo 100% hồ sơ vận chuyển hợp lệ, đúng quy định của Bộ Nông nghiệp, Hải quan và cơ quan kiểm dịch các nước.
  * Rút ngắn thời gian thông quan tại các cửa khẩu/sân bay.
* **Thách thức (Pain Points):**
  * Quy định kiểm dịch và thủ tục hải quan thay đổi thường xuyên giữa các quốc gia.
  * Khách hàng chậm trễ hoặc cung cấp thiếu/sai thông tin hồ sơ y tế của ngựa.
  * Quản lý khối lượng tài liệu số hóa khổng lồ cho nhiều chuyến đi cùng lúc.
* **Chức năng chính trên hệ thống:**
  * Quản lý danh mục quy định y tế, kiểm dịch và hải quan theo quốc gia/cửa khẩu.
  * Khởi tạo & Lưu trữ hồ sơ vận chuyển số hóa (Hộ chiếu ngựa, Giấy chứng nhận tiêm phòng, Giấy phép xuất nhập cảnh).
  * Theo dõi tiến độ phê duyệt hồ sơ từ các cơ quan chức năng.
  * Gửi thông báo và hướng dẫn khách hàng hoàn thiện giấy tờ pháp lý còn thiếu.

---

### 4. Fleet & Route Coordinator (Điều phối viên Đội xe & Lộ trình)

* **Tên đại diện (Persona Name):** Phạm Hoàng Nam (Mr. Nam)
* **Chức danh:** Điều phối viên Đội xe & Tuyến đường
* **Mô tả vai trò (Role Summary):** Quản lý phương tiện chuyên dụng và xây dựng lộ trình tối ưu nhất. Theo dõi sát sao tiến độ di chuyển của từng phương tiện để cảnh báo và điều chỉnh lịch trình khi có sự cố.
* **Mục tiêu chính (Goals):**
  * Lập lộ trình di chuyển an toàn, tiết kiệm thời gian và đảm bảo thể trạng ngựa đua.
  * Phân bổ hiệu quả phương tiện chở ngựa chuyên dụng (*thùng stalls, xe tải, khoang máy bay*).
* **Thách thức (Pain Points):**
  * Tình trạng kẹt xe, tắc nghẽn cửa khẩu hoặc thời tiết xấu làm gián đoạn lịch trình.
  * Cần cân đối hợp lý các trạm dừng nghỉ, trạm tiếp nhiên liệu và trạm kiểm dịch trên đường.
* **Chức năng chính trên hệ thống:**
  * Quản lý danh sách phương tiện chuyên dụng (*xe tải chở ngựa, thùng vận chuyển - stalls, khoang máy bay*).
  * Xây dựng lộ trình di chuyển tối ưu (xác định điểm dừng nghỉ, trạm tiếp nhiên liệu, trạm kiểm dịch).
  * Cập nhật và giám sát tiến độ chuyến đi theo từng chặng (*Khởi hành, Đến điểm dừng, Đã thông quan, Đã giao*).
  * Cảnh báo sự cố và điều chỉnh lịch trình di chuyển kịp thời.

---

### 5. Vehicle Driver (Tài xế Vận chuyển)

* **Tên đại diện (Persona Name):** Nguyễn Văn Hùng (Mr. Hùng)
* **Chức danh:** Tài xế Xe tải Chuyên dụng Vận chuyển Ngựa
* **Mô tả vai trò (Role Summary):** Chịu trách nhiệm vận hành phương tiện chở ngựa an toàn trên tuyến đường được phân công và báo cáo kịp thời các mốc lộ trình.
* **Mục tiêu chính (Goals):**
  * Lái xe an toàn, êm ái, tuân thủ lộ trình và thời gian quy định.
  * Báo cáo đầy đủ và chính xác các mốc dừng nghỉ, xuất phát, thông quan.
* **Thách thức (Pain Points):**
  * Thao tác trên ứng dụng di động khi đang di chuyển trên đường đường dài.
  * Ứng phó với các sự cố kỹ thuật xe hoặc ùn tắc giao thông kéo dài.
* **Chức năng chính trên hệ thống:**
  * Xem lịch trình chuyến đi và danh sách điểm dừng nghỉ được phân công.
  * Báo cáo trạng thái mốc chuyến đi thủ công (*Đã xuất phát, Đã tới điểm dừng, Đã hoàn thành thông quan*).
  * Gửi báo cáo sự cố khẩn cấp về xe hoặc giao thông (*Hỏng xe, Kẹt xe kéo dài*) về trung tâm điều hành.

---

### 6. Escort (Nhân viên Đi kèm & Chăm sóc Ngựa)

* **Tên đại diện (Persona Name):** Đặng Thái Bình (Mr. Bình)
* **Chức danh:** Nhân viên Đi kèm & Chăm sóc Thú y đường dài
* **Mô tả vai trò (Role Summary):** Trực tiếp đi cùng ngựa trong suốt chuyến đi (trên xe hoặc máy bay) để theo dõi sức khỏe, tâm lý, khẩu phần ăn và xử lý các vấn đề y tế phát sinh.
* **Mục tiêu chính (Goals):**
  * Đảm bảo sức khỏe, tâm lý và an toàn sinh học cho chiến mã suốt hành trình.
  * Ghi nhận đầy đủ nhật ký theo dõi trạng thái ngựa.
* **Thách thức (Pain Points):**
  * Ngựa đua dễ bị căng thẳng (*stress*), bỏ ăn, sốt vận chuyển hoặc chấn thương do rung lắc.
  * Cần báo cáo nhanh sự cố y tế cấp bách để nhận hỗ trợ từ bác sĩ thú y / ban điều hành.
* **Chức năng chính trên hệ thống:**
  * Xem danh sách ngựa phụ trách và yêu cầu chăm sóc đặc biệt trên chuyến đi.
  * Ghi nhận nhật ký trạng thái của ngựa (*Sức khỏe ổn định, Bỏ ăn, Căng thẳng, Khí hậu thay đổi*) bằng văn bản và hình ảnh.
  * Gửi báo cáo sự cố y tế khẩn cấp của ngựa về trung tâm điều hành.

---

### 7. Customer (Khách hàng - CLB / Chủ sở hữu Ngựa)

* **Tên đại diện (Persona Name):** David Michael (Mr. David)
* **Chức danh:** Quản lý Câu lạc bộ Đua ngựa / Chủ sở hữu Chiến mã
* **Mô tả vai trò (Role Summary):** Khách hàng sử dụng dịch vụ vận chuyển để đưa ngựa đua tham gia các giải đấu quốc tế hoặc di chuyển giữa các trang trại/câu lạc bộ.
* **Mục tiêu chính (Goals):**
  * Ngựa đua được vận chuyển an toàn, đúng giờ và giữ nguyên phong độ thi đấu.
  * Theo dõi minh bạch vị trí và tình trạng sức khỏe của ngựa mọi lúc mọi nơi.
* **Thách thức (Pain Points):**
  * Lo lắng về độ an toàn và sức khỏe của ngựa giá trị cao trong hành trình dài.
  * Quy trình chuẩn bị tài liệu pháp lý phức tạp nếu không có hướng dẫn rõ ràng.
* **Chức năng chính trên hệ thống:**
  * Tạo yêu cầu đặt dịch vụ vận chuyển ngựa đua (*Địa điểm đi/đến, Thời gian, Số lượng ngựa, Yêu cầu đặc biệt*).
  * Tải lên các giấy tờ lý lịch và hồ sơ y tế của ngựa.
  * Theo dõi tiến độ vị trí chuyến đi và trạng thái cập nhật theo từng chặng thời gian thực.
  * Nhận thông báo khi ngựa hoàn tất thông quan hoặc bàn giao an toàn.
