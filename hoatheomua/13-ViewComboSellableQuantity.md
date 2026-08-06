# STORY-013: View Combo Sellable Quantity

## Overview
Story này cho phép quản trị viên xem số lượng có thể bán của từng combo hoa dựa trên công thức định lượng và tồn kho hiện tại. Hệ thống đồng thời xác định vật liệu nút thắt để giúp quản trị viên biết cần bổ sung vật liệu nào nhằm tăng khả năng bán.

---

## Objective
Cho phép quản trị viên:
- Xem số lượng có thể bán của từng combo hoa.
- Xác định vật liệu nút thắt của mỗi combo.
- Lọc và sắp xếp combo theo khả năng bán.
- Theo dõi tình trạng còn hàng của các combo.

---

## Preconditions
- Combo hoa có công thức ở trạng thái **Active**.
- Các vật liệu trong công thức có dữ liệu tồn kho.

---

## Trigger
Quản trị viên mở **Combo List** hoặc **Combo Detail**.

---

## Main Features

### Calculate Sellable Quantity
Đối với mỗi combo:
- Hệ thống lấy công thức đang có hiệu lực.
- Với mỗi vật liệu có vai trò **CORE**:
  - Tính số lượng combo có thể tạo bằng:
    - `Tồn khả dụng / Định lượng` (lấy phần nguyên).
- Chọn giá trị nhỏ nhất làm **số lượng có thể bán**.

### Identify Bottleneck Material
- Hệ thống xác định vật liệu **CORE** có giá trị nhỏ nhất.
- Hiển thị tên vật liệu nút thắt cùng số lượng có thể bán.

Nếu có nhiều vật liệu cùng là nút thắt:
- Hiển thị tất cả các vật liệu có cùng giá trị nhỏ nhất.

### Display Information
Mỗi combo hiển thị:
- Tên combo.
- Số lượng có thể bán.
- Vật liệu nút thắt.

### Filter
Cho phép lọc theo:
- Còn hàng.
- Sắp hết.
- Hết hàng.

### Sorting
Cho phép sắp xếp theo:
- Số lượng có thể bán tăng dần.
- Số lượng có thể bán giảm dần.

---

## Validation

Hệ thống:
- Chỉ tính các vật liệu có vai trò **CORE**.
- Sử dụng **tồn khả dụng** (`Tồn thực - Đang giữ chỗ`) để tính toán.
- Bỏ qua các vật liệu có vai trò **SUPPORT** khi tính số lượng có thể bán.

---

## Exception Handling

### No Formula
- Nếu combo chưa có công thức:
  - Số lượng có thể bán bằng **0**.
  - Hiển thị nhãn **Chưa cấu hình định lượng**.

### No CORE Material
- Nếu công thức chỉ có vật liệu **SUPPORT**:
  - Số lượng có thể bán bằng **0**.
  - Hiển thị nhãn **Thiếu vật liệu chính**.

---

## Acceptance Criteria

### AC-001
- Hệ thống tính đúng số lượng có thể bán dựa trên các vật liệu **CORE**.
- Hiển thị đúng vật liệu nút thắt.

### AC-002
- Vật liệu **SUPPORT** không ảnh hưởng đến số lượng có thể bán dù tồn khả dụng bằng **0**.

### AC-003
- Nếu nhiều combo cùng sử dụng một vật liệu:
  - Kết quả tính toán phản ánh đúng tồn khả dụng hiện tại.
  - Khi tồn kho thay đổi, số lượng có thể bán của các combo liên quan được cập nhật tương ứng.

### AC-004
- Nếu công thức chỉ có vật liệu **SUPPORT**:
  - Hiển thị số lượng có thể bán bằng **0**.
  - Hiển thị nhãn **Thiếu vật liệu chính**.

### AC-005
- Số lượng có thể bán được tính trên **tồn khả dụng**, không phải tồn thực.

---

## Non-functional Requirements
- Danh sách **200 combo hoa** hiển thị đầy đủ số lượng có thể bán trong dưới **2 giây**.
- Kết quả tính toán phải nhất quán giữa màn hình danh sách và màn hình chi tiết.

---

## Out of Scope
- Hiển thị số lượng có thể bán cho khách hàng.
- Dự báo khả năng bán theo kế hoạch nhập hàng.