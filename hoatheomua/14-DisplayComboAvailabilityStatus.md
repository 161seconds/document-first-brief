# STORY-014: Display Combo Availability Status

## Overview
Story này cho phép khách hàng biết tình trạng còn hàng của từng combo hoa trước khi đặt mua. Hệ thống chỉ hiển thị trạng thái bán hàng mà không tiết lộ thông tin tồn kho nội bộ, giúp khách hàng tránh lựa chọn những sản phẩm không còn khả năng cung cấp.

---

## Objective
Cho phép khách hàng:
- Xem trạng thái còn hàng của combo hoa.
- Biết khi nào sản phẩm sắp hết hoặc đã hết hàng.
- Xem thông tin này ngay cả khi chưa đăng nhập.
- Không thể đặt mua combo đã hết hàng.

---

## Preconditions
- Combo hoa có công thức đang hiệu lực.
- Hệ thống đã tính được số lượng có thể bán của combo.

---

## Trigger
Khách hàng mở **Combo List** hoặc **Combo Detail** trên website.

---

## Main Features

### Display Availability Status
Hệ thống lấy số lượng có thể bán của combo và hiển thị trạng thái tương ứng:

- **In Stock**: Combo còn khả năng bán.
- **Low Stock**: Combo còn ít hàng (dựa trên ngưỡng hiển thị).
- **Out of Stock**: Combo không còn khả năng bán.

Trạng thái được hiển thị:
- Trên thẻ sản phẩm.
- Trên trang chi tiết combo.

### Order Availability
Nếu combo ở trạng thái **Out of Stock**:
- Nút **Order** bị vô hiệu hóa.
- Khách hàng vẫn có thể xem thông tin sản phẩm.

### Product Sorting
Theo mặc định:
- Các combo hết hàng được đưa xuống cuối danh sách.

---

## Validation

Hệ thống:
- Chỉ hiển thị trạng thái bán hàng.
- Không hiển thị số lượng tồn kho hoặc dữ liệu nội bộ.
- Dựa trên kết quả tính số lượng có thể bán của hệ thống.

---

## Exception Handling

### Status Changed
Nếu trạng thái của combo thay đổi trong khi khách hàng đang thao tác:
- Hệ thống cập nhật trạng thái mới.
- Thông báo rằng combo vừa hết hàng.

### Status Unavailable
Nếu chưa lấy được trạng thái:
- Tạm thời cho phép khách hàng tiếp tục thao tác.
- Kiểm tra lại ở bước tạo đơn hàng.

---

## Acceptance Criteria

### AC-001
- Nếu số lượng có thể bán bằng **0**:
  - Hiển thị nhãn **Out of Stock**.
  - Vô hiệu hóa nút **Order**.

### AC-002
- Nếu số lượng có thể bán nhỏ hơn hoặc bằng ngưỡng quy định:
  - Hiển thị nhãn **Low Stock**.

### AC-003
- Khách hàng chưa đăng nhập vẫn xem được đầy đủ trạng thái của combo.

---

## Dependencies
- STORY-013: View Combo Sellable Quantity

---

## Non-functional Requirements
- Trạng thái còn hàng phải phản ánh dữ liệu không cũ hơn **60 giây**.
- Không để lộ số lượng tồn kho cụ thể thông qua giao diện hoặc phản hồi từ máy chủ.

---

## Out of Scope
- Thông báo khi sản phẩm có hàng trở lại.
- Chức năng chặn đặt hàng ở bước tạo đơn (STORY-030).