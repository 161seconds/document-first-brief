# STORY-015: Validate Sellable Quantity Before Order

## Overview
Story này đảm bảo khách hàng chỉ có thể đặt số lượng combo hoa mà cửa hàng còn khả năng cung cấp. Hệ thống kiểm tra khả năng bán trước khi thêm vào giỏ hoặc tạo đơn hàng, đồng thời kiểm tra lại khi xác nhận đơn để tránh bán vượt tồn kho trong trường hợp nhiều khách đặt hàng cùng lúc.

---

## Objective
Cho phép hệ thống:
- Kiểm tra số lượng khách yêu cầu trước khi tạo đơn.
- Ngăn khách đặt vượt số lượng có thể bán.
- Kiểm tra toàn bộ giỏ hàng thay vì từng sản phẩm riêng lẻ.
- Đảm bảo không xảy ra bán vượt khi nhiều khách đặt cùng lúc.

---

## Preconditions
- Khách hàng đã chọn sản phẩm hoặc hoàn tất thiết kế.
- Combo hoa có công thức đang hiệu lực.

---

## Trigger
Khách hàng nhập số lượng và chọn **Add to Cart** hoặc **Order**.

---

## Main Features

### Validate Sellable Quantity
Khi khách hàng thực hiện đặt hàng:
- Hệ thống nhận số lượng yêu cầu.
- Tính lại số lượng có thể bán tại thời điểm hiện tại.
- So sánh số lượng yêu cầu với số lượng có thể bán.
- Nếu hợp lệ:
  - Cho phép tiếp tục thao tác.

### Final Validation
Khi tạo đơn hàng:
- Hệ thống kiểm tra lại khả năng bán.
- Thực hiện giữ chỗ vật liệu.
- Chỉ tạo đơn khi việc giữ chỗ thành công.

### Suggest Maximum Quantity
Nếu khách yêu cầu nhiều hơn khả năng bán nhưng vẫn còn hàng:
- Hiển thị số lượng tối đa có thể đặt.
- Cho phép khách điều chỉnh sang số lượng đó.

### Cart Validation
Đối với giỏ hàng:
- Kiểm tra tổng nhu cầu vật liệu của toàn bộ giỏ.
- Không kiểm tra từng sản phẩm riêng lẻ.
- Phát hiện các combo sử dụng chung vật liệu.

---

## Validation

Hệ thống:
- Kiểm tra số lượng có thể bán tại thời điểm hiện tại.
- Kiểm tra tổng nhu cầu vật liệu của toàn bộ giỏ hàng.
- Chỉ sử dụng vật liệu **CORE** khi tính khả năng bán.
- Không chặn đơn chỉ vì vật liệu **SUPPORT** hết hàng.

---

## Exception Handling

### Requested Quantity Exceeded
- Không cho phép tiếp tục.
- Hiển thị số lượng tối đa còn có thể đặt.

### Product Out of Stock
- Nếu combo vừa hết hàng:
  - Không tạo đơn.
  - Thông báo cho khách hàng.

### Concurrent Orders
Nếu nhiều khách đặt cùng lúc:
- Chỉ đơn giữ chỗ vật liệu thành công được tạo.
- Các đơn còn lại nhận thông báo hết hàng.

---

## Acceptance Criteria

### AC-001
- Nếu khách đặt nhiều hơn số lượng có thể bán:
  - Hệ thống chặn thao tác.
  - Hiển thị số lượng tối đa còn có thể đặt.

### AC-002
- Nếu nhiều combo trong giỏ sử dụng chung vật liệu:
  - Hệ thống kiểm tra tổng nhu cầu của toàn bộ giỏ.
  - Thông báo khi vật liệu không đủ.

### AC-003
- Khi nhiều khách đặt cùng một combo tại cùng thời điểm:
  - Chỉ một đơn được tạo thành công.
  - Các đơn còn lại nhận thông báo combo đã hết hàng.

### AC-004
- Nếu vật liệu **SUPPORT** hết hàng:
  - Hệ thống vẫn cho phép tạo đơn.

---

## Dependencies
- STORY-013: View Combo Sellable Quantity
- STORY-014: Display Combo Availability Status

---

## Non-functional Requirements
- Việc kiểm tra khả năng bán và giữ chỗ vật liệu phải được thực hiện trong **một giao dịch nguyên tử (atomic transaction)** để tránh bán vượt tồn kho.
- Thông báo lỗi phải hiển thị rõ tên combo và số lượng còn có thể đặt, nhưng không được tiết lộ số lượng tồn kho của vật liệu.

---

## Out of Scope
- Cơ chế giữ chỗ và trừ tồn theo trạng thái đơn hàng.
- Danh sách chờ khi sản phẩm hết hàng.