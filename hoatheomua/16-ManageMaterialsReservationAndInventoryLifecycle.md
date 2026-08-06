# STORY-016: Manage Material Reservation and Inventory Lifecycle

## Overview
Story này quản lý toàn bộ vòng đời tồn kho của vật liệu theo trạng thái đơn hàng. Hệ thống thực hiện giữ chỗ vật liệu, trừ tồn và hoàn trả tồn kho theo từng giai đoạn của đơn nhằm đảm bảo tồn kho luôn chính xác, tránh bán vượt và không bị khóa bởi các đơn hàng không hoàn tất.

---

## Objective
Cho phép hệ thống:
- Tự động giữ chỗ vật liệu khi đơn được tạo.
- Chuyển giữ chỗ thành xác nhận khi thanh toán thành công.
- Trừ tồn kho khi đơn được xác nhận.
- Tự động hoàn trả giữ chỗ khi đơn hết hạn hoặc bị hủy.
- Ghi nhận các trường hợp thiếu vật liệu để quản trị viên xử lý.

---

## Preconditions
- Đơn hàng đã lưu **snapshot** công thức của từng combo.
- Hệ thống quản lý trạng thái đơn hàng đang hoạt động.

---

## Trigger
Đơn hàng được tạo mới hoặc chuyển sang một trạng thái khác.

---

## Main Features

### Soft Reservation
Khi đơn chuyển sang **Pending**:
- Hệ thống tạo giữ chỗ mềm cho toàn bộ vật liệu trong snapshot.
- Bao gồm cả vật liệu **CORE** và **SUPPORT**.
- Thiết lập thời gian hết hạn giữ chỗ.

### Hard Reservation
Khi đơn chuyển sang **Confirm**:
- Giữ chỗ mềm được chuyển thành giữ chỗ cứng.
- Xóa thời gian hết hạn giữ chỗ.

### Inventory Deduction
Khi đơn chuyển sang **Confirmed**:
- Hệ thống giảm tồn thực của các vật liệu.
- Giải phóng bản ghi giữ chỗ.
- Không phát sinh thêm thay đổi tồn kho khi đơn chuyển sang **Completed**.

### Reservation Expiration
Nếu đơn ở trạng thái **Pending** quá thời gian quy định:
- Hệ thống tự động hủy giữ chỗ.
- Hoàn trả tồn khả dụng.
- Chuyển đơn sang **Cancelled**.

### Order Cancellation
Nếu khách hủy đơn trước khi thanh toán:
- Giải phóng toàn bộ giữ chỗ.
- Hoàn trả tồn khả dụng.

---

## Validation

Hệ thống:
- Sử dụng snapshot công thức đã lưu trong đơn hàng.
- Thực hiện toàn bộ thao tác dựa trên trạng thái đơn.
- Mỗi lần chuyển trạng thái chỉ tạo một bộ bút toán tồn kho duy nhất.

---

## Exception Handling

### Insufficient Inventory
Nếu một số vật liệu không đủ:
- Hệ thống vẫn xử lý các vật liệu đủ điều kiện.
- Ghi nhận phần thiếu.
- Tạo cảnh báo bổ sung hàng cho quản trị viên.

### Transaction Failure
Nếu xảy ra lỗi trong quá trình xử lý:
- Hoàn tác toàn bộ giao dịch.
- Giữ nguyên trạng thái đơn trước đó.
- Ghi nhật ký lỗi.

---

## Acceptance Criteria

### AC-001
- Khi đơn ở trạng thái **Pending**:
  - Tăng số lượng giữ chỗ của vật liệu.
  - Giảm tồn khả dụng.
  - Không thay đổi tồn thực.

### AC-002
- Khi giữ chỗ hết hạn:
  - Giải phóng giữ chỗ.
  - Khôi phục tồn khả dụng.
  - Chuyển đơn sang **Cancelled**.

### AC-006
- Khi đơn chuyển sang **Confirm**:
  - Tồn thực của vật liệu **SUPPORT** được cập nhật theo công thức.

### AC-007
- Nếu vật liệu **SUPPORT** không còn tồn kho:
  - Đơn vẫn được xác nhận thành công.
  - Hệ thống ghi nhận thiếu hụt.
  - Tạo cảnh báo bổ sung hàng cho quản trị viên.

---

## Dependencies
- STORY-015: Validate Sellable Quantity Before Order

---

## Non-functional Requirements
- Tất cả thao tác giữ chỗ, trừ tồn và hoàn tồn phải đảm bảo **idempotency**, mỗi sự kiện chuyển trạng thái chỉ tạo một bộ bút toán duy nhất.
- Tác vụ giải phóng giữ chỗ hết hạn phải chạy định kỳ với chu kỳ không quá **1 phút**.

---

## Out of Scope
- Thay đổi combo hoa sau khi đơn đã được xác nhận.
- Giao hàng một phần.