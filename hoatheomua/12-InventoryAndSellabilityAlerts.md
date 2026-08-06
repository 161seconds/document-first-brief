# STORY-012: Inventory and Sellability Alerts

## Overview
Story này bổ sung các cảnh báo trên Dashboard giúp quản trị viên chủ động theo dõi tình trạng tồn kho và khả năng bán của các combo hoa. Hệ thống sẽ cảnh báo các vật liệu sắp hết hàng và các combo không còn khả năng bán để hỗ trợ nhập hàng kịp thời.

---

## Objective
Cho phép quản trị viên:
- Xem danh sách vật liệu sắp hết hàng.
- Xem danh sách combo không còn khả năng bán.
- Xác định vật liệu nút thắt của từng combo.
- Điều hướng nhanh đến màn hình nhập kho.

---

## Preconditions
- Vật liệu đã được cấu hình ngưỡng cảnh báo tồn thấp.
- Quản trị viên đã đăng nhập.
- Tài khoản có quyền xem Dashboard.

---

## Trigger
Quản trị viên truy cập **Dashboard** của hệ thống.

---

## Main Features

### Low Stock Alerts
Hệ thống:
- Tính các vật liệu có **tồn khả dụng ≤ ngưỡng cảnh báo**.
- Hiển thị danh sách cảnh báo theo mức độ nghiêm trọng giảm dần.
- Mỗi cảnh báo hiển thị:
  - Tên vật liệu.
  - Tồn khả dụng hiện tại.
  - Ngưỡng cảnh báo.

### Out-of-Sellable Combo Alerts
Hệ thống:
- Tính các combo đang mở bán nhưng có **số lượng có thể bán bằng 0**.
- Hiển thị:
  - Tên combo.
  - Vật liệu nút thắt đang gây thiếu.
  - Thông tin liên quan đến khả năng bán.

### Quick Navigation
Mỗi cảnh báo vật liệu:
- Có liên kết mở nhanh màn hình **Stock In**.
- Tự động chọn sẵn vật liệu tương ứng.

### Stable Inventory
Nếu không có cảnh báo:
- Hiển thị trạng thái tồn kho ổn định.

---

## Alert Priority

### High Priority
- Vật liệu CORE dưới ngưỡng cảnh báo.
- Combo không còn khả năng bán.

### Low Priority
- Vật liệu có vai trò mặc định **SUPPORT** dưới ngưỡng.
- Hiển thị ghi chú rằng tình trạng này không ảnh hưởng đến số lượng combo có thể bán nhưng có thể ảnh hưởng đến chất lượng của bó hoa.

---

## Exception Handling

### Alert Loading Failed
- Hiển thị thông báo không tải được dữ liệu cảnh báo.
- Không ảnh hưởng đến việc hiển thị các khối khác trên Dashboard.

---

## Acceptance Criteria

### AC-001
- Vật liệu có tồn khả dụng nhỏ hơn hoặc bằng ngưỡng cảnh báo xuất hiện trong danh sách cảnh báo.
- Hiển thị đúng số lượng tồn hiện tại.

### AC-002
- Combo có số lượng có thể bán bằng **0** xuất hiện trong danh sách cảnh báo.
- Hiển thị vật liệu nút thắt gây ra tình trạng này.

### AC-003
- Vật liệu **SUPPORT** hết hàng được hiển thị ở mức cảnh báo thấp.
- Ghi rõ không ảnh hưởng đến số lượng combo có thể bán.

### AC-004
- Khi nhấn vào một cảnh báo vật liệu:
  - Hệ thống mở màn hình **Stock In**.
  - Vật liệu tương ứng được chọn sẵn.

---

## Non-functional Requirements
- Khối cảnh báo tải trong dưới **2 giây**.
- Việc tải cảnh báo không được làm chậm hoặc chặn các thành phần khác của Dashboard.

---

## Out of Scope
- Gửi cảnh báo qua email hoặc tin nhắn.
- Dự báo nhu cầu nhập hàng.