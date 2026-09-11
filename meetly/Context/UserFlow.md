# MEETLY - USER FLOW

## 1. Admin Flow

### 1.1. Tạo sự kiện

1. Truy cập website Meetly.
2. Chọn **Create Event**.
3. Nhập thông tin người tạo sự kiện:
   - **Username**: duy nhất trong phạm vi sự kiện.
   - **Password**: không bắt buộc.
4. Thiết lập thông tin sự kiện:
   - Chọn loại lịch:
     - **Dates**: chọn các ngày cụ thể.
     - **Weekdays**: chọn các thứ trong tuần.
   - Chọn các ngày/thứ muốn tổ chức.
   - Thiết lập khung giờ bắt đầu và kết thúc trong ngày.
5. Nhấn **Create Event** để hoàn tất việc tạo sự kiện.
6. Hệ thống chuyển Admin đến trang quản lý lịch của sự kiện.

---

### 1.2. Xem Heatmap tổng

Tại màn hình chính của sự kiện, hệ thống hiển thị **Heatmap tổng**.

Heatmap thể hiện mức độ khả dụng của tất cả người tham gia theo từng khung thời gian.

Admin có thể sử dụng Heatmap tổng để:

- Xem các khoảng thời gian có nhiều người rảnh.
- So sánh mức độ phù hợp giữa các khung giờ.
- Tìm thời điểm thích hợp để tổ chức cuộc họp.

---

### 1.3. Cập nhật lịch cá nhân

1. Chọn **Lịch của tôi**.
2. Hệ thống hiển thị lịch cá nhân của Admin:
   - Nếu chưa từng chọn lịch: hiển thị lưới thời gian trống.
   - Nếu đã có dữ liệu trước đó: hiển thị các khoảng thời gian đã chọn.
3. Admin có thể:
   - Kéo thả trên lưới thời gian để chọn khoảng thời gian rảnh.
   - Chọn thời gian thủ công.
   - Chỉnh sửa hoặc bỏ chọn các khoảng thời gian đã chọn trước đó.
4. Lưu lịch cá nhân.
5. Chọn **Tổng quan** để quay lại Heatmap tổng.
6. Heatmap tổng được cập nhật theo dữ liệu mới và đồng bộ realtime đến các người dùng đang tham gia sự kiện.

---

### 1.4. Tìm khung giờ phù hợp

Admin có thể sử dụng tính năng **Gợi ý khung giờ** để tìm thời gian phù hợp.

Có thể lọc theo một hoặc kết hợp nhiều điều kiện:

- **Thời lượng cuộc họp tối thiểu**  
  Ví dụ: `120 phút`.
- **Người tham gia chủ chốt**  
  Ví dụ: `Huy`.

Ví dụ:

```text
Thời lượng tối thiểu: 120 phút
Người chủ chốt: Huy
```

Hệ thống phân tích Heatmap và đánh dấu các khoảng thời gian đáp ứng điều kiện.

Các khoảng thời gian phù hợp được làm nổi bật bằng **khung viền** trên Heatmap để Admin dễ nhận biết và lựa chọn.

---

### 1.5. Chỉnh sửa sự kiện

1. Chọn **Chỉnh sửa sự kiện**.
2. Admin có thể thay đổi:
   - Tên sự kiện.
   - Loại lịch.
   - Các ngày/thứ được chọn.
   - Thêm ngày.
   - Xóa ngày.
   - Khung giờ bắt đầu.
   - Khung giờ kết thúc.
3. Nhấn **Lưu thay đổi**.
4. Hệ thống hiển thị cảnh báo về việc thay đổi cấu hình sự kiện có thể ảnh hưởng đến dữ liệu lịch hiện tại.
5. Admin xác nhận thay đổi.
6. Hệ thống cập nhật sự kiện.
7. Heatmap tổng được cập nhật theo cấu hình mới và đồng bộ realtime đến tất cả người đang mở sự kiện.

---

### 1.6. Chốt lịch họp

1. Chọn **Chốt lịch họp**.
2. Hệ thống chuyển Heatmap sang chế độ chọn lịch cuối cùng.
3. Admin kéo chọn khoảng thời gian muốn sử dụng làm lịch họp chính thức.
4. Chọn **Xác nhận lịch họp**.
5. Hệ thống hiển thị thông tin lịch đã chọn để Admin xác nhận lần cuối.
6. Sau khi xác nhận:
   - Sự kiện được chuyển sang trạng thái **Đã chốt lịch**.
   - Hệ thống lưu thời gian họp cuối cùng.
   - Email thông báo được gửi đến những người đã đăng ký email.
   - Thông tin lịch họp được cập nhật realtime trên màn hình của tất cả người tham gia.
   - Heatmap được khóa và không cho phép tiếp tục thay đổi lịch.

---

## 2. Participant Flow

### 2.1. Tham gia sự kiện

1. Truy cập website Meetly.
2. Chọn **Join Event**.
3. Nhập mã sự kiện.

Ví dụ:

```text
SJD452
```

Ngoài ra, người dùng có thể truy cập trực tiếp thông qua link chia sẻ của sự kiện.

4. Nhập thông tin tham gia:
   - **Username**: duy nhất trong phạm vi sự kiện.
   - **Password**: không bắt buộc.
5. Hệ thống xác thực thông tin và chuyển người dùng đến trang chính của sự kiện.

---

### 2.2. Xem Heatmap tổng

Sau khi tham gia, người dùng được chuyển đến màn hình **Tổng quan**.

Tại đây hệ thống hiển thị Heatmap tổng của sự kiện, thể hiện mức độ khả dụng của tất cả người tham gia theo từng khoảng thời gian.

Heatmap được cập nhật realtime khi có người tham gia thay đổi lịch của mình.

---

### 2.3. Cập nhật lịch cá nhân

1. Chọn **Lịch của tôi**.
2. Hệ thống hiển thị lịch cá nhân:
   - Nếu chưa từng chọn lịch: hiển thị lưới thời gian trống.
   - Nếu đã từng lưu lịch: hiển thị lại các khoảng thời gian đã chọn trước đó.
3. Người dùng có thể:
   - Kéo thả trên lưới để chọn khoảng thời gian rảnh.
   - Chọn thời gian thủ công.
   - Chỉnh sửa hoặc bỏ chọn lịch đã lưu.
4. Lưu thay đổi.
5. Chọn **Tổng quan** để quay lại Heatmap tổng.
6. Heatmap tổng được cập nhật và đồng bộ realtime đến những người khác trong sự kiện.

---

### 2.4. Nhận lịch họp cuối cùng

Khi Admin chốt lịch họp:

1. Người dùng đang mở sự kiện nhận thông báo realtime.
2. Thời gian họp chính thức được hiển thị nổi bật trên màn hình chính.
3. Heatmap chuyển sang trạng thái chỉ xem và không thể tiếp tục chỉnh sửa lịch.
4. Nếu người dùng đã đăng ký email, hệ thống gửi email thông báo về lịch họp đã được chốt.

---

## 3. Tổng quan luồng chính

```text
ADMIN
  │
  ├── Tạo Event
  │
  ▼
Chia sẻ Link / Short Code
  │
  ▼
Participants tham gia
  │
  ▼
Mỗi người cập nhật lịch cá nhân
  │
  ▼
Heatmap tổng cập nhật Realtime
  │
  ▼
Admin xem Heatmap
  │
  ├── Lọc theo thời lượng
  │
  ├── Lọc theo người chủ chốt
  │
  └── Xem các khung giờ gợi ý
  │
  ▼
Admin chọn lịch họp
  │
  ▼
Xác nhận lịch cuối cùng
  │
  ├── Lưu vào hệ thống
  ├── Gửi Email
  ├── Broadcast Realtime
  └── Khóa Heatmap
       │
       ▼
Tất cả Participants nhận lịch cuối cùng
```
