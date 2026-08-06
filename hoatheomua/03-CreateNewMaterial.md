# STORY-003: Create New Material

## Overview
Story này cho phép quản trị viên tạo mới một vật liệu để sử dụng trong công thức cấu thành các combo hoa và phục vụ việc quản lý tồn kho. Các thông tin quan trọng như đơn vị tính và vai trò mặc định được ràng buộc ngay từ bước tạo nhằm đảm bảo tính chính xác của dữ liệu.

---

## Objective
Cho phép quản trị viên:
- Tạo mới một vật liệu.
- Khai báo đầy đủ thông tin của vật liệu.
- Thiết lập đơn vị tính và vai trò mặc định.
- Đưa vật liệu vào danh sách để có thể sử dụng trong công thức và theo dõi tồn kho.

---

## Preconditions
- Quản trị viên đã đăng nhập vào hệ thống.
- Tài khoản có quyền tạo vật liệu.
- Đang ở màn hình **Material List**.

---

## Trigger
Quản trị viên nhấn nút **Add Material**.

---

## Main Features

### Create Material
Hệ thống hiển thị biểu mẫu tạo vật liệu.

Các thông tin cần nhập gồm:
- Mã vật liệu
- Tên vật liệu
- Nhóm
- Vai trò mặc định
- Đơn vị tính
- Ngưỡng cảnh báo tồn thấp
- Nhãn mùa vụ
- Mô tả

Sau khi nhấn **Save**, hệ thống:
- Kiểm tra dữ liệu hợp lệ.
- Kiểm tra mã vật liệu không bị trùng.
- Tạo vật liệu với:
  - Trạng thái: **Đang bán**
  - Tồn thực: **0**
- Hiển thị thông báo thành công.
- Thêm vật liệu vào danh sách.

### Save and Create Another
- Lưu vật liệu hiện tại.
- Mở biểu mẫu tạo mới.
- Giữ lại giá trị **Nhóm** và **Đơn vị tính** vừa sử dụng.

### Auto Generate Code
Nếu không nhập mã vật liệu:
- Hệ thống tự sinh mã theo quy tắc:
  - Tiền tố của nhóm.
  - Số thứ tự tăng dần.

---

## Validation

Hệ thống kiểm tra:
- Mã vật liệu không được trùng.
- Các trường bắt buộc không được để trống.
- Ngưỡng cảnh báo tồn thấp phải là số nguyên không âm.

---

## Exception Handling

### Duplicate Code
- Không cho phép lưu.
- Hiển thị lỗi tại trường mã vật liệu.

### Missing Required Fields
- Không cho phép lưu.
- Đánh dấu các trường bắt buộc còn thiếu.

### Invalid Number
- Không cho phép lưu.
- Yêu cầu nhập số nguyên không âm.

### System Error
- Giữ nguyên dữ liệu đã nhập.
- Hiển thị nút **Retry** để thử lại.

---

## Acceptance Criteria

### AC-001
- Tạo vật liệu thành công khi nhập đầy đủ thông tin hợp lệ.
- Vật liệu có trạng thái **Đang bán**.
- Tồn thực bằng **0**.
- Có thể được chọn khi tạo công thức.

### AC-002
- Không cho phép tạo vật liệu có mã đã tồn tại.
- Hiển thị thông báo lỗi phù hợp.

### AC-003
- Không cho phép lưu nếu thiếu:
  - Vai trò mặc định.
  - Đơn vị tính.

### AC-004
Sau khi tạo thành công:
- Tồn thực = 0.
- Đang giữ chỗ = 0.
- Tồn khả dụng = 0.
- Chưa phát sinh giao dịch kho.

---

## Dependencies
- STORY-002: View and Search Material List

---

## Non-functional Requirements
- Chống gửi biểu mẫu nhiều lần khi người dùng nhấn **Save** liên tục.
- Thông báo lỗi hiển thị bằng tiếng Việt và gắn trực tiếp vào trường bị lỗi.

---

## Out of Scope
- Nhập tồn kho ban đầu (STORY-024).
- Nhập vật liệu hàng loạt từ file.
- Quản lý giá vốn vật liệu.