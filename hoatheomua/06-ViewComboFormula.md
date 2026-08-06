# STORY-006: View Combo Formula

## Overview
Story này cho phép quản trị viên xem công thức định lượng của một combo hoa để biết combo được cấu thành từ những vật liệu nào, số lượng sử dụng của từng vật liệu và vật liệu nào đang là nút thắt ảnh hưởng đến khả năng bán của combo.

---

## Objective
Cho phép quản trị viên:
- Xem công thức định lượng của combo hoa.
- Xem danh sách vật liệu theo từng vai trò.
- Theo dõi tồn khả dụng của từng vật liệu.
- Xem số lượng combo có thể bán.
- Xác định vật liệu đang giới hạn khả năng bán.

---

## Preconditions
- Combo hoa đã tồn tại trong hệ thống.
- Quản trị viên có quyền xem công thức.

---

## Trigger
Quản trị viên mở tab **Formula** trong màn hình chi tiết combo hoa.

---

## Main Features

### View Formula
Hệ thống tải công thức đang có hiệu lực của combo hoa.

Danh sách vật liệu được hiển thị theo thứ tự:
1. CORE
2. SUPPORT

### Display Information
Mỗi dòng hiển thị:
- Tên vật liệu
- Vai trò
- Định lượng
- Đơn vị tính
- Tồn khả dụng
- Số lượng combo có thể tạo từ vật liệu đó

### Sellable Quantity
Hệ thống hiển thị số lượng combo có thể bán ở đầu màn hình.

Việc tính toán chỉ dựa trên các vật liệu có vai trò **CORE**.

### Bottleneck Material
- Hệ thống xác định vật liệu đang giới hạn khả năng bán.
- Hiển thị nhãn hoặc đánh dấu riêng cho vật liệu này.

### Empty Formula
Nếu combo chưa có công thức:
- Hiển thị trạng thái rỗng.
- Hiển thị nút **Add Material** để thêm vật liệu.

---

## Validation

Hệ thống kiểm tra:
- Công thức có tồn tại hay không.
- Trạng thái của các vật liệu trong công thức.
- Tồn khả dụng của từng vật liệu.

---

## Exception Handling

### Empty Formula
- Hiển thị trạng thái chưa có công thức.
- Số lượng có thể bán bằng **0**.

### Discontinued Material
- Nếu công thức chứa vật liệu đã ngừng kinh doanh:
  - Hiển thị cảnh báo trên dòng tương ứng.
  - Yêu cầu thay thế vật liệu.

### System Error
- Hiển thị thông báo lỗi.
- Cho phép người dùng **Retry**.

---

## Acceptance Criteria

### AC-001
- Hiển thị đầy đủ danh sách vật liệu trong công thức.
- Nhóm **CORE** hiển thị trước **SUPPORT**.
- Mỗi dòng hiển thị đúng định lượng và đơn vị tính.

### AC-002
- Nếu vật liệu **SUPPORT** hết hàng:
  - Số lượng có thể bán vẫn chỉ tính theo các vật liệu **CORE**.
  - Hiển thị cảnh báo hết hàng trên dòng SUPPORT.

### AC-003
- Nếu combo chưa có công thức:
  - Hiển thị trạng thái rỗng.
  - Số lượng có thể bán bằng **0**.

### AC-004
- Nếu công thức chứa vật liệu ở trạng thái **Ngừng kinh doanh**:
  - Hiển thị cảnh báo trên dòng vật liệu.
  - Yêu cầu thay thế vật liệu.

---

## Dependencies
- STORY-002: View and Search Material List

---

## Non-functional Requirements
- Số lượng có thể bán phải phản ánh dữ liệu không cũ hơn **60 giây**.

---

## Out of Scope
- Chỉnh sửa công thức (STORY-019 đến STORY-021).
- So sánh sự khác biệt giữa các phiên bản công thức.