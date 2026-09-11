# API CONTRACT - MEETLY (Chính Thức)

## 0. Quy ước chung

### Base URL

```text
/api/v1
```

### Event Type

```text
1 = Dates (Khảo sát theo các ngày cụ thể)
2 = Weekdays (Khảo sát theo các thứ trong tuần: "Monday", "Tuesday"...)
```

### Response Format chuẩn

Response thành công (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Thông điệp thành công",
  "value": {}
}
```

Response thất bại (`400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`, `409 Conflict`):

```json
{
  "isSuccess": false,
  "code": 400,
  "message": "Nội dung lỗi chi tiết",
  "value": null
}
```

---

## 1. Khởi tạo sự kiện mới

**`POST /api/v1/events`**  
**Mô tả:** Admin tạo sự kiện. Mặc định múi giờ Việt Nam (`Asia/Ho_Chi_Minh`).

### Request:

```json
{
  "title": "Họp !!!",
  "eventType": 1,
  "availableDates": ["2026-09-10", "2026-09-11", "2026-09-12"],
  "dailyStartTime": "08:00",
  "dailyEndTime": "17:00"
}
```

*Lưu ý:* Nếu `eventType = 2`, mảng `availableDates` sẽ chứa các thứ: `["Monday", "Tuesday", "Thursday"]`.

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Tạo sự kiện thành công",
  "value": {
    "shortCode": "A1B2C3",
    "url": "https://meetly.com/A1B2C3"
  }
}
```

---

## 2. Load Dữ liệu Sự kiện & Heatmap Tổng

**`GET /api/v1/events/{shortCode}`**  
**Mô tả:** Gọi ngay khi vào trang để vẽ Heatmap. Cung cấp mảng lịch rảnh của tất cả thành viên.

### Request:
Trống (chỉ truyền `shortCode` trên URL path).

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Tải sự kiện thành công",
  "value": {
    "title": "Họp !!!",
    "shortCode": "A1B2C3",
    "url": "https://meetly.com/A1B2C3",
    "eventType": 1,
    "availableDates": ["2026-09-10", "2026-09-11", "2026-09-12"],
    "dailyStartTime": "08:00",
    "dailyEndTime": "17:00",
    "isFinalized": false,
    "finalStartTime": null,
    "finalEndTime": null,
    "participants": [
      {
        "username": "John",
        "timeSlots": [
          {
            "startTime": "2026-09-10T08:00:00+07:00",
            "endTime": "2026-09-10T10:00:00+07:00"
          }
        ]
      }
    ],
    "heatmapGrid": {
      "2026-09-10T08:00:00+07:00": ["Uyên", "Bảo", "Huy"],
      "2026-09-10T08:30:00+07:00": ["Uyên", "Bảo"],
      "2026-09-10T09:00:00+07:00": ["Bảo"],
      "2026-09-10T09:30:00+07:00": []
    }
  }
}
```

*Lưu ý:*
- Nếu `isFinalized: true`, khóa bảng trên UI, không cho chọn/sửa lịch nữa.
- Mảng `participants` là Data source để Zustand filter ra các ô khoanh đỏ khi Admin hover tìm khung giờ.

---

## 3. Đăng nhập & Lấy dữ liệu cá nhân (Nhập Username)

**`POST /api/v1/events/{shortCode}/participants/access`**  
**Mô tả:** Kiểm tra user. Nếu đã từng chọn lịch, lấy lịch cũ về tô màu sẵn cho họ sửa. Đồng thời trả về cờ xác nhận có phải Admin hay không.

### Request:

```json
{
  "username": "John",
  "password": "123"
}
```

*Lưu ý:* `password` là tùy chọn (Optional), bỏ trống `""` hoặc `null` nếu user không cài mật khẩu.

### Response (`200 OK` - Nếu user đã tồn tại):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Lấy dữ liệu thành công",
  "value": {
    "isAdmin": true,
    "timeSlots": [
      {
        "date": "2026-09-10",
        "weekday": null,
        "startTime": "08:00",
        "endTime": "10:00"
      },
      {
        "date": "2026-09-11",
        "weekday": null,
        "startTime": "13:00",
        "endTime": "15:00"
      }
    ]
  }
}
```

*Lưu ý:*
- Nếu là user mới chưa từng lưu lịch: trả về `timeSlots: []` để FE render lưới trắng.
- Nếu `isAdmin: true`: FE render thêm 2 nút chức năng **Chỉnh sửa sự kiện** và **Chốt lịch họp**.

---

## 4. Ghi nhận & Cập nhật thời gian rảnh

**`POST /api/v1/events/{shortCode}/participants`**  
**Mô tả:** Gửi mảng thời gian rảnh xuống DB khi người dùng bấm Lưu.

### Request:

```json
{
  "username": "John",
  "password": "123",
  "email": "user@example.com",
  "inputMode": "BUSY",
  "timeSlots": [
    {
      "date": "2026-09-10",
      "weekday": null,
      "startTime": "13:00",
      "endTime": "15:00"
    }
  ]
}
```

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Cập nhật lịch rảnh thành công",
  "value": null
}
```

*Lưu ý:*
- Dù người dùng bôi màu ở Mode Rảnh hay Mode Bận trên giao diện, FE tính toán bù logic và **chỉ gửi một mảng Thời Gian Rảnh (Free Time)** duy nhất qua API này.
- Đọc `email`: Nếu có giá trị (`!= null`), BE chèn vào bảng `EventEmails` (không trùng lặp) để gửi thông báo sau này.

---

## 5. Chốt lịch & Gửi thông báo

**`POST /api/v1/events/{shortCode}/finalize`**  
**Mô tả:** Nút chốt lịch của Admin. Cập nhật `isFinalized = true`, lưu `finalStartTime`, `finalEndTime` và kích hoạt luồng bắn Email đồng loạt (BCC) qua backend .NET.

### Request:

```json
{
  "finalStartTime": "2026-09-10T08:00:00+07:00",
  "finalEndTime": "2026-09-10T09:00:00+07:00"
}
```

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Chốt lịch và gửi thông báo thành công",
  "value": null
}
```

---

## 6. Lọc Heatmap theo Người chủ chốt

**`GET /api/v1/events/{shortCode}/suggestions?keyParticipant={username}&minDuration={minutes}`**  
**Mô tả:** Admin chọn người chủ chốt và số phút tối thiểu muốn người chủ chốt xuất hiện. BE tính toán chỗ đông người nhất + có người chủ chốt tham gia đủ số phút đã chọn và trả ra các khoảng thời gian để vẽ viền đỏ.

### Query Parameters:
- `keyParticipant`: Username của người chủ chốt (ví dụ: `John`).
- `minDuration`: Thời lượng tối thiểu tính bằng phút (ví dụ: `120`).

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Lọc heatmap thành công",
  "value": {
    "suggestedSlots": [
      {
        "date": "2026-09-10",
        "weekday": null,
        "startTime": "08:00",
        "endTime": "10:00"
      },
      {
        "date": "2026-09-11",
        "weekday": null,
        "startTime": "14:00",
        "endTime": "16:30"
      }
    ]
  }
}
```

---

## 7. Chỉnh sửa sự kiện (Admin)

**`PUT /api/v1/events/{shortCode}`**  
**Mô tả:** Admin gửi thông tin mới để cập nhật sự kiện. Hệ thống xác thực quyền bằng username và password của Admin.

### Request:

```json
{
  "adminUsername": "John",
  "adminPassword": "123",
  "title": "Họp !!!",
  "eventType": 1,
  "availableDates": ["2026-09-15", "2026-09-16"],
  "dailyStartTime": "08:00",
  "dailyEndTime": "17:00"
}
```

### Response (`200 OK`):

```json
{
  "isSuccess": true,
  "code": 200,
  "message": "Cập nhật sự kiện thành công",
  "value": null
}
```

---

## 8. Bảng Tổng Hợp 7 Endpoints

| STT | Phương thức | Endpoint | Actor | Chức năng chính |
| :---: | :---: | :--- | :---: | :--- |
| **1** | `POST` | `/api/v1/events` | Guest / Admin | Khởi tạo sự kiện mới, sinh ShortCode |
| **2** | `GET` | `/api/v1/events/{shortCode}` | Public | Load thông tin sự kiện & ma trận Heatmap tổng |
| **3** | `POST` | `/api/v1/events/{shortCode}/participants/access` | Participant / Admin | Đăng nhập bằng Username + lấy lịch cá nhân |
| **4** | `POST` | `/api/v1/events/{shortCode}/participants` | Participant / Admin | Lưu & cập nhật thời gian rảnh (Free Time) |
| **5** | `POST` | `/api/v1/events/{shortCode}/finalize` | Admin | Chốt lịch chính thức & gửi email hàng loạt |
| **6** | `GET` | `/api/v1/events/{shortCode}/suggestions` | Admin | Gợi ý khung giờ tối ưu (Key participant & Min duration) |
| **7** | `PUT` | `/api/v1/events/{shortCode}` | Admin | Chỉnh sửa ngày giờ, cấu hình sự kiện |
