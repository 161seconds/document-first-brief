# Hệ thống Quản lý Vận chuyển Ngựa đua Xuyên Quốc gia (Cross-Border Racehorse Transport System)

## 1. Người tác động & Phân công (Actors & Assignments)

### 1. Guest (Khách)
* **Truy cập công khai:** Truy cập các trang công khai (public pages) bao gồm trang chủ, trang đăng nhập và đăng ký, để xem các thông tin cơ bản về hệ thống và dịch vụ hiện có.

### 2. Logged-in User (Người dùng đã đăng nhập)
* **Truy cập hệ thống:** Truy cập các tính năng và công cụ của hệ thống dựa trên vai trò và quyền hạn được phân bổ để thực hiện quy trình nghiệp vụ, quản lý dữ liệu và các hành động được ủy quyền. Người dùng đã đăng nhập bao gồm 6 vai trò cụ thể dưới đây:

#### 2.1 Logistics Manager (Quản lý Điều hành Logistics)
* **Phê duyệt đơn hàng:** Tiếp nhận và phê duyệt các đơn hàng vận chuyển ngựa đua trong nước và quốc tế.
* **Lập kế hoạch tổng thể:** Lập kế hoạch tổng thể cho chuyến vận chuyển (chọn phương tiện, hãng hàng không, tuyến đường, điểm trung chuyển).
* **Phân công nhiệm vụ:** Phân công nhiệm vụ cho Chuyên viên thủ tục, Điều phối viên và Tài xế/Nhân viên đi kèm.
* **Xử lý sự cố khẩn cấp:** Xử lý và phê duyệt các phương án thay đổi lộ trình hoặc chi phí phát sinh trong trường hợp khẩn cấp.
* **Báo cáo & Hiệu suất:** Xem báo cáo doanh thu, chi phí vận hành và chỉ số hiệu suất hoàn thành chuyến đi (*On-time delivery*).

#### 2.2 Transport Specialist (Chuyên viên Thủ tục & Kiểm dịch)
* **Quản lý quy định:** Quản lý danh mục quy định y tế, kiểm dịch và hải quan của từng quốc gia/cửa khẩu.
* **Hồ sơ số hóa:** Khởi tạo và lưu trữ bộ hồ sơ vận chuyển số hóa cho từng con ngựa (*Giấy chứng nhận tiêm phòng, Hộ chiếu ngựa, Giấy phép xuất nhập cảnh*).
* **Theo dõi phê duyệt:** Theo dõi tiến độ phê duyệt hồ sơ từ các cơ quan chức năng (*Bộ Nông nghiệp, Hải quan, Đơn vị kiểm dịch*).
* **Hướng dẫn khách hàng:** Gửi thông báo và hướng dẫn khách hàng bổ sung các giấy tờ pháp lý còn thiếu trước ngày khởi hành.

#### 2.3 Fleet & Route Coordinator (Điều phối viên Đội xe & Lộ trình)
* **Quản lý phương tiện:** Quản lý danh sách phương tiện chuyên dụng (*Xe tải chở ngựa, Khoang máy bay, Thùng vận chuyển - Stalls*).
* **Xây dựng lộ trình:** Xây dựng lộ trình di chuyển tối ưu, xác định các điểm dừng nghỉ, trạm kiểm dịch và trạm tiếp nhiên liệu.
* **Cập nhật tiến độ:** Cập nhật thủ công hoặc theo dõi trạng thái tiến độ chuyến đi theo từng chặng (*Khởi hành, Đến điểm dừng, Đã thông quan, Đã giao*).
* **Điều chỉnh & Cảnh báo:** Điều chỉnh lịch trình và gửi cảnh báo khi phát sinh sự cố giao thông, thời tiết xấu hoặc tắc nghẽn cửa khẩu.

#### 2.4 Vehicle Driver (Tài xế)
* **Lịch trình & Nhiệm vụ:** Xem lịch trình chuyến đi, danh sách ngựa phụ trách và danh sách điểm dừng nghỉ được phân công.
* **Báo cáo mốc di chuyển:** Báo cáo trạng thái chuyến đi thủ công tại mỗi điểm mốc (*Đã xuất phát, Đã tới điểm dừng, Đã hoàn thành thông quan*).
* **Báo cáo sự cố:** Gửi báo cáo sự cố khẩn cấp (*Hỏng xe, Kẹt xe kéo dài*) về trung tâm điều hành.

#### 2.5 Escort (Nhân viên đi kèm)
* **Nhật ký trạng thái ngựa:** Ghi nhận nhật ký trạng thái của ngựa trong suốt chuyến đi (*Sức khỏe ổn định, Bỏ ăn, Căng thẳng, Khí hậu thay đổi*) bằng văn bản và hình ảnh.
* **Báo cáo sự cố y tế:** Gửi báo cáo sự cố khẩn cấp liên quan đến sức khỏe của ngựa về trung tâm điều hành.

#### 2.6 Customer (Khách hàng - CLB / Chủ ngựa)
* **Yêu cầu dịch vụ:** Tạo yêu cầu đặt dịch vụ vận chuyển ngựa đua (*Địa điểm đi/đến, Thời gian, Số lượng ngựa, Yêu cầu đặc biệt*).
* **Cung cấp hồ sơ:** Tải lên các giấy tờ lý lịch và hồ sơ y tế của ngựa theo yêu cầu của đơn vị vận chuyển.
* **Theo dõi chuyến đi:** Theo dõi tiến độ vị trí chuyến đi và trạng thái cập nhật theo từng chặng thời gian thực.
* **Nhận thông báo bàn giao:** Nhận thông báo khi ngựa hoàn tất thông quan hoặc đã tới địa điểm bàn giao an toàn.

---

## 2. Các Quy trình / Quy luồng Nghiệp vụ (Business Workflows)

* **Flow 1:** Luồng Tạo & Phê duyệt Yêu cầu Vận chuyển *(REQUIRED)*
* **Flow 2:** Luồng Quản lý Hồ sơ Pháp lý & Kiểm dịch Thông quan *(REQUIRED)*
* **Flow 3:** Luồng Lập Kế hoạch Lộ trình & Điều phối Phương tiện *(REQUIRED)*
* **Flow 4:** Luồng Cập nhật Trạng thái & Nhật ký Lộ trình *(REQUIRED)*
* **Flow 5:** Luồng Xử lý Sự cố & Điều chỉnh Lộ trình Khẩn cấp *(OPTIONAL)*
* **Flow 6:** Luồng Bàn giao & Nghiệm thu Chuyến đi *(OPTIONAL)*

---

## 3. Các Yêu cầu Loại trừ (Out of Scope / Exclusive)

* **Không tích hợp thiết bị IoT:** Hệ thống không kết nối với các cảm biến IoT gắn trên xe hay trên ngựa. Việc định vị vị trí chuyến đi chỉ được thực hiện thông qua GPS trên điện thoại của Tài xế.
* **Giới hạn phạm vi địa lý:** Hoạt động vận chuyển chỉ được giới hạn trong phạm vi các nước khu vực Châu Âu.
* **Ngôn ngữ hệ thống:** Hệ thống chỉ hỗ trợ một ngôn ngữ duy nhất là Tiếng Anh.
* **Không áp dụng Trí tuệ Nhân tạo (AI):** Hệ thống không bao gồm bất kỳ tính năng AI nào.
* **Phương thức liên lạc hạn chế:** Hệ thống không kết nối với Dịch vụ SMS (SMS Service) hay hỗ trợ gọi điện thoại. Tất cả các thông báo của hệ thống chỉ được gửi thông qua Email.
* **Không phát triển Mobile App:** Hệ thống chỉ phát triển duy nhất dưới dạng Ứng dụng Web đáp ứng (Responsive Web Application), hoạt động trên trình duyệt của máy tính, điện thoại và máy tính bảng. Hệ thống không phát triển ứng dụng di động riêng (Native / Hybrid Mobile App).
* **Không cho phép các tác nhân bên ngoài truy cập trực tiếp:** Bác sĩ thú y bên ngoài (External Veterinarians), Cơ quan Hải quan (Customs Authorities), Đơn vị Kiểm dịch và Cơ quan Pháp lý nhà nước không có tài khoản và không truy cập trực tiếp vào hệ thống. Mọi thao tác cập nhật giấy tờ, nộp hồ sơ số hóa và ghi nhận kết quả kiểm dịch/thông quan đều do nhân sự nội bộ của hệ thống (như Transport Specialist, Logistics Manager, Escort) thực hiện.
