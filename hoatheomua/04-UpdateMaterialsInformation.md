# STORY-004: Update Material Information

## Overview
Story này cho phép quản trị viên cập nhật thông tin của một vật liệu đã được tạo trong hệ thống nhằm đảm bảo dữ liệu luôn phản ánh đúng thực tế kinh doanh. Một số trường như đơn vị tính sẽ bị khóa nếu vật liệu đã phát sinh giao dịch để đảm bảo tính toàn vẹn của dữ liệu lịch sử.

---

## Objective
Cho phép quản trị viên:
- Cập nhật thông tin của vật liệu.
- Chỉnh sửa các thuộc tính được phép thay đổi.
- Bảo vệ các trường ảnh hưởng đến dữ liệu lịch sử.
- Ghi nhận thời điểm cập nhật gần nhất.

---

## Preconditions
- Vật liệu đã tồn tại trong hệ thống.
- Quản trị viên có quyền chỉnh sửa vật liệu.

---

## Trigger
Quản trị viên chọn một vật liệu trong danh sách và nhấn **Edit**.

---

## Main Features

### Edit Material
Hệ thống mở biểu mẫu chỉnh sửa với dữ liệu hiện tại của vật liệu.

Các trường được phép cập nhật gồm:
- Tên vật liệu
- Nhóm
- Vai trò mặc định
- Ngưỡng cảnh báo tồn thấp
- Nhãn mùa vụ
- Mô tả
- Trạng thái (nếu được hỗ trợ)

Sau khi nhấn **Save**, hệ thống:
- Kiểm tra dữ liệu hợp lệ.
- Lưu các thay đổi.
- Ghi nhận thời điểm cập nhật.
- Hiển thị thông báo thành công.

### Lock Unit of Measure
Nếu vật liệu đã phát sinh giao dịch hoặc xuất hiện trong lịch sử đơn hàng:
- Trường **Unit of Measure** ở trạng thái chỉ đọc.
- Hiển thị giải thích lý do khi người dùng di chuột (hover).

### Change Default Role
Nếu thay đổi **Default Role**:
- Hệ thống lưu thay đổi.
- Hiển thị cảnh báo rằng thay đổi chỉ áp dụng cho các công thức được tạo sau này.
- Không ảnh hưởng đến các công thức đã tồn tại.

### Cancel Editing
Nếu người dùng đã thay đổi dữ liệu nhưng nhấn **Cancel**:
- Hệ thống yêu cầu xác nhận trước khi thoát khỏi biểu mẫu.

---

## Validation

Hệ thống kiểm tra:
- Dữ liệu hợp lệ trước khi lưu.
- Phiên bản bản ghi để tránh ghi đè dữ liệu của người khác.

---

## Exception Handling

### Locked Field
- Không cho phép thay đổi đơn vị tính khi vật liệu đã có giao dịch.
- Hướng dẫn người dùng tạo vật liệu mới nếu cần sử dụng đơn vị tính khác.

### Version Conflict
- Phát hiện xung đột khi nhiều người cùng chỉnh sửa.
- Chặn ghi đè dữ liệu.
- Yêu cầu tải lại dữ liệu mới nhất.

### Material Not Found
- Hiển thị thông báo lỗi.
- Đưa người dùng quay về danh sách vật liệu.

---

## Acceptance Criteria

### AC-001
- Cho phép cập nhật tên vật liệu.
- Thông tin mới được hiển thị trên danh sách.
- Ghi nhận thời điểm cập nhật.

### AC-002
- Nếu vật liệu đã phát sinh giao dịch:
  - Trường **Unit of Measure** ở chế độ chỉ đọc.
  - Hiển thị lý do khi hover.

### AC-003
- Nếu hai quản trị viên cùng chỉnh sửa:
  - Người lưu sau không được ghi đè dữ liệu.
  - Hệ thống yêu cầu tải lại dữ liệu mới nhất.

---

## Dependencies
- STORY-003: Create New Material

---

## Non-functional Requirements
- Kiểm soát xung đột đồng thời bằng cơ chế **Optimistic Concurrency (Record Versioning)**.
- Không sử dụng khóa bi quan (Pessimistic Locking).

---

## Out of Scope
- Chuyển trạng thái ngừng kinh doanh.
- Xem lịch sử thay đổi thông tin vật liệu.