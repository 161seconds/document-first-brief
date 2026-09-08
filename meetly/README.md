# Meetly — Kho Tài Liệu Đặc Tả (Document-First)

> **Mã dự án (Key)**: `meetly`  
> **Tên dự án**: `Meetly` — Nền tảng khảo sát và tìm thời gian họp tối ưu qua Heatmap  
> **Phương pháp tiếp cận**: Document-First System Specification  

---

## 📖 1. Tổng quan Dự án

**Meetly** là ứng dụng hỗ trợ người tổ chức và người tham gia tìm kiếm thời gian gặp mặt, họp nhóm thuận tiện nhất thông qua khảo sát trực quan bằng **Heatmap**:
- **Khảo sát thời gian linh hoạt**: Hỗ trợ 2 dạng sự kiện: `Dates and Times` (ngày cụ thể) và `Weekdays` (các thứ định kỳ trong tuần).
- **Trực quan hóa qua Heatmap**: Ma trận thời gian hiển thị mật độ người rảnh (màu xanh từ nhạt đến đậm), tự động xác định các khung giờ tối ưu nhất.
- **Tương tác bình chọn hai chế độ**: Hỗ trợ đánh dấu thời gian RẢNH hoặc loại trừ thời gian BẬN, hỗ trợ kéo thả (drag-and-drop) và bình chọn khung giờ xuyên ngày.
- **Định danh nhanh gọn (Event-Scoped)**: Người tham gia định danh trực tiếp trong phạm vi sự kiện mà không cần đăng ký tài khoản phức tạp, bảo vệ quyền riêng tư và tối đa hóa tỷ lệ phản hồi.

---

## 📂 2. Cấu trúc Thư mục Phân hệ

```text
meetly/
├── README.md                              # Cổng tra cứu tổng quan dự án Meetly (File hiện tại)
├── BusinessRules/                         # 16 Quy tắc nghiệp vụ (BR-01 -> BR-16)
├── ConfirmedDoc/                          # Tài liệu & Hợp đồng API (API Contracts) đã chốt
├── Context/                               # Sơ đồ CSDL (ERD), Từ điển dữ liệu & Kiến trúc
├── UserStory/                             # 6 User Stories đặc tả yêu cầu (US-01 -> US-06)
├── SystemTest/                            # 65 Kịch bản kiểm thử hệ thống chuẩn hóa (ST-US01-01 -> ST-US06-16)
├── TDD/                                   # Thiết kế kỹ thuật chi tiết (Technical Design Documents)
└── UnitTest/                              # Kịch bản kiểm thử đơn vị (Unit Tests)
```

---

## 📑 3. Danh Mục User Stories

| Mã Story | Tiêu đề Story | Độ ưu tiên | Quy tắc liên quan | Mô tả tóm tắt |
| :--- | :--- | :-: | :--- | :--- |
| [`US-01`](file:///d:/VNZ/document-first-brief/meetly/UserStory/01-CreateSurveyEvent.md) | **Tạo sự kiện cần khảo sát** | `Must` | BR-01, BR-02, BR-03, BR-04, BR-05, BR-06 | Khởi tạo sự kiện mới, chọn loại sự kiện, danh sách ngày, đăng ký tài khoản admin và nhận link chia sẻ. |
| [`US-02`](file:///d:/VNZ/document-first-brief/meetly/UserStory/02-EditSurveyEvent.md) | **Chỉnh sửa thông tin của sự kiện đã tạo** | `Must` | BR-01, BR-02, BR-03, BR-04, BR-07 | Admin chỉnh sửa tên, loại và danh sách ngày khảo sát từ Dashboard. Cascade delete bình chọn của ngày bị xóa. |
| [`US-03`](file:///d:/VNZ/document-first-brief/meetly/UserStory/03-JoinEvent.md) | **Tham gia sự kiện** | `Must` | BR-01, BR-04, BR-11, BR-12 | Người tham gia vào sự kiện qua liên kết URL hoặc ShortCode, xem Dashboard và Heatmap tổng quan. |
| [`US-04`](file:///d:/VNZ/document-first-brief/meetly/UserStory/04-RegisterParticipantIdentity.md) | **Đăng ký tài khoản định danh trong sự kiện** | `Must` | BR-06, BR-08, BR-10 | Định danh cá nhân theo sự kiện (Username bắt buộc, Password tùy chọn) để ghi nhận kết quả bình chọn. |
| [`US-05`](file:///d:/VNZ/document-first-brief/meetly/UserStory/05-ViewResultsViaHeatmap.md) | **Xem kết quả qua Heatmap** | `Must` | BR-14, BR-15 | Xem ma trận Heatmap chia 15 phút (trục dọc) và 1 ngày (trục ngang). Hover xem số lượng và danh sách người rảnh. |
| [`US-06`](file:///d:/VNZ/document-first-brief/meetly/UserStory/06-VoteMeetingTime.md) | **Bình chọn thời gian họp** | `Must` | BR-09, BR-11, BR-12, BR-13, BR-14, BR-15, BR-16 | Chọn thời gian rảnh qua click/kéo thả, hỗ trợ mode RẢNH/BẬN và bình chọn khung giờ xuyên đêm. |

---

## ⚖️ 4. Danh Mục Quy Tắc Nghiệp Vụ (Business Rules)

| Mã BR | Tên quy tắc nghiệp vụ | Nhóm nghiệp vụ | Tóm tắt phát biểu |
| :--- | :--- | :--- | :--- |
| [`BR-01`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-01.md) | Phân loại loại sự kiện khảo sát | Event Management | Sự kiện có 2 loại: `Dates and Times` (ngày cụ thể) và `Weekdays` (các thứ trong tuần). |
| [`BR-02`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-02.md) | Ràng buộc ngày bình chọn trong tương lai | Event Validation | Danh sách ngày đối với sự kiện Dates and Times bắt buộc phải là ngày trong tương lai. |
| [`BR-03`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-03.md) | Số lượng ngày bình chọn tối thiểu | Event Validation | Mỗi sự kiện bắt buộc có $\ge 1$ ngày/thứ được đưa vào bình chọn. |
| [`BR-04`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-04.md) | Tính duy nhất và bất biến của ID sự kiện | Event Identity | ID sự kiện là duy nhất và bất biến sau khi sự kiện được tạo. |
| [`BR-05`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-05.md) | Quyền quản trị duy nhất của người tạo sự kiện | Authorization | Người tạo sự kiện là admin duy nhất của sự kiện đó. |
| [`BR-06`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-06.md) | Tính độc nhất của Username trong sự kiện | Identity | Username phải là độc nhất trong phạm vi sự kiện. |
| [`BR-07`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-07.md) | Xóa bình chọn khi ngày bình chọn bị xóa | Data Integrity | Nếu một ngày bình chọn bị xóa, toàn bộ bình chọn tương ứng của ngày đó cũng bị xóa. |
| [`BR-08`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-08.md) | Phạm vi hiệu lực của tài khoản định danh | Identity | Username và mật khẩu chỉ tồn tại trong sự kiện đó, không dùng chung cho toàn hệ thống. |
| [`BR-09`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-09.md) | Một kết quả bình chọn duy nhất cho mỗi username | Voting Logic | Mỗi username trong sự kiện chỉ có duy nhất một kết quả bình chọn (ghi đè khi sửa). |
| [`BR-10`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-10.md) | Bắt buộc Username và tùy chọn Password | Identity | Khi đăng ký định danh, username là bắt buộc, mật khẩu là không bắt buộc. |
| [`BR-11`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-11.md) | Bắt buộc đăng nhập định danh khi bình chọn | Voting Auth | Khi bình chọn một sự kiện, cần phải đăng nhập vào tài khoản định danh. |
| [`BR-12`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-12.md) | Chặn bình chọn khi vượt quá ngày cuối cùng | Constraints | Không được phép bình chọn khi thời gian hiện tại đã vượt quá ngày cuối cùng trong danh sách. |
| [`BR-13`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-13.md) | Lưu trữ dữ liệu dưới dạng thời gian RẢNH | Data Standard | Dữ liệu bình chọn được lưu dưới dạng thời gian RẢNH. Mode BẬN chỉ là cách nhập liệu. |
| [`BR-14`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-14.md) | Quy ước màu sắc hiển thị trên Heatmap | UI / Visual | Ô trắng thể hiện bận (0 người), màu xanh từ nhạt đến đậm thể hiện số người rảnh tăng dần. |
| [`BR-15`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-15.md) | Chặn bình chọn cho các ngày trong quá khứ | Constraints | Không thể bình chọn cho các ngày trong quá khứ (hiển thị mờ, disabled). |
| [`BR-16`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-16.md) | Hỗ trợ bình chọn khung giờ xuyên ngày | Voting Logic | Người tham gia có thể chọn các mốc sự kiện xuyên ngày (ví dụ từ 21h - 3h hôm sau). |

---

## 🧪 5. Danh Mục Kịch Bản Kiểm Thử Hệ Thống (System Tests — 65 Tests)

Mỗi kịch bản kiểm thử được chuẩn hóa thành 1 file độc lập theo biểu mẫu chuẩn [`template-SystemTest.md`](file:///d:/VNZ/document-first-brief/template-SystemTest.md):

### 5.1. Phân hệ `US-01`: Tạo sự kiện cần khảo sát (10 Tests)
- [`ST-US01-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-01.md): Mở chức năng tạo sự kiện
- [`ST-US01-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-02.md): Tạo sự kiện Dates and Times hợp lệ
- [`ST-US01-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-03.md): Tạo sự kiện Weekdays hợp lệ
- [`ST-US01-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-04.md): Bỏ trống tên sự kiện
- [`ST-US01-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-05.md): Không chọn loại sự kiện
- [`ST-US01-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-06.md): Không thêm ngày hoặc thứ bình chọn
- [`ST-US01-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-07.md): Nhập ngày quá khứ
- [`ST-US01-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-08.md): Đăng ký thông tin người tổ chức sau khi tạo
- [`ST-US01-09`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-09.md): Kiểm tra chuyển hướng sau khi tạo
- [`ST-US01-10`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US01-10.md): Kiểm tra link và ID sự kiện

### 5.2. Phân hệ `US-02`: Chỉnh sửa thông tin sự kiện (12 Tests)
- [`ST-US02-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-01.md): Admin mở chức năng chỉnh sửa
- [`ST-US02-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-02.md): Participant truy cập chức năng chỉnh sửa
- [`ST-US02-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-03.md): Người chưa đăng nhập truy cập chỉnh sửa
- [`ST-US02-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-04.md): Hiển thị dữ liệu hiện tại
- [`ST-US02-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-05.md): Cập nhật tên sự kiện
- [`ST-US02-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-06.md): Cập nhật danh sách ngày bình chọn
- [`ST-US02-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-07.md): Đổi loại sự kiện
- [`ST-US02-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-08.md): Xóa toàn bộ lựa chọn bình chọn
- [`ST-US02-09`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-09.md): Xóa ngày đã có bình chọn
- [`ST-US02-10`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-10.md): Hủy thay đổi
- [`ST-US02-11`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-11.md): Lỗi khi cập nhật
- [`ST-US02-12`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US02-12.md): Kiểm tra ID sau chỉnh sửa

### 5.3. Phân hệ `US-03`: Tham gia sự kiện (8 Tests)
- [`ST-US03-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-01.md): Tham gia bằng URL
- [`ST-US03-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-02.md): Tham gia bằng ShortCode
- [`ST-US03-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-03.md): Nhập ShortCode không tồn tại
- [`ST-US03-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-04.md): Để trống ShortCode
- [`ST-US03-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-05.md): Xem nội dung sau khi tham gia
- [`ST-US03-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-06.md): Bình chọn khi chưa định danh
- [`ST-US03-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-07.md): Truy cập Dashboard khi chưa tham gia
- [`ST-US03-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US03-08.md): Tham gia sự kiện đã kết thúc

### 5.4. Phân hệ `US-04`: Đăng ký tài khoản định danh trong sự kiện (8 Tests)
- [`ST-US04-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-01.md): Đăng ký định danh sau khi tham gia
- [`ST-US04-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-02.md): Đăng ký khi chưa tham gia sự kiện
- [`ST-US04-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-03.md): Đăng ký username sai format
- [`ST-US04-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-04.md): Đăng ký username đã tồn tại
- [`ST-US04-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-05.md): Đăng ký không nhập mật khẩu
- [`ST-US04-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-06.md): Kiểm tra phạm vi tài khoản
- [`ST-US04-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-07.md): Đăng nhập username đã đăng ký
- [`ST-US04-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US04-08.md): Truy cập nội dung khi chưa định danh

### 5.5. Phân hệ `US-05`: Xem kết quả qua Heatmap (11 Tests)
- [`ST-US05-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-01.md): Xem Heatmap sau khi tham gia
- [`ST-US05-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-02.md): Kiểm tra phạm vi ngày và giờ
- [`ST-US05-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-03.md): Kiểm tra trục thời gian
- [`ST-US05-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-04.md): Kiểm tra trục ngày
- [`ST-US05-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-05.md): Heatmap khi chưa có bình chọn
- [`ST-US05-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-06.md): Hiển thị số người rảnh
- [`ST-US05-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-07.md): Kiểm tra màu sắc Heatmap
- [`ST-US05-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-08.md): Cập nhật Heatmap
- [`ST-US05-09`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-09.md): Người chưa tham gia xem Heatmap
- [`ST-US05-10`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-10.md): Hiển thị ngày quá khứ
- [`ST-US05-11`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US05-11.md): Kiểm tra chú thích màu sắc

### 5.6. Phân hệ `US-06`: Bình chọn thời gian họp (16 Tests)
- [`ST-US06-01`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-01.md): Bình chọn khi chưa định danh
- [`ST-US06-02`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-02.md): Mở Heatmap bình chọn
- [`ST-US06-03`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-03.md): Bình chọn bằng thao tác click
- [`ST-US06-04`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-04.md): Bình chọn bằng thao tác kéo thả
- [`ST-US06-05`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-05.md): Chọn nhiều khoảng thời gian
- [`ST-US06-06`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-06.md): Chọn ngày đã qua
- [`ST-US06-07`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-07.md): Bình chọn sau thời hạn
- [`ST-US06-08`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-08.md): Sử dụng mode RẢNH
- [`ST-US06-09`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-09.md): Sử dụng mode BẬN
- [`ST-US06-10`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-10.md): Lưu kết quả bình chọn
- [`ST-US06-11`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-11.md): Chỉnh sửa bình chọn
- [`ST-US06-12`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-12.md): Không tạo bản ghi bình chọn mới
- [`ST-US06-13`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-13.md): Hủy bình chọn
- [`ST-US06-14`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-14.md): Lỗi khi lưu bình chọn
- [`ST-US06-15`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-15.md): Bình chọn xuyên ngày
- [`ST-US06-16`](file:///d:/VNZ/document-first-brief/meetly/SystemTest/ST-US06-16.md): Hiển thị kết quả sau khi lưu
