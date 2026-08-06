# STORY-008: Update Material in Combo Formula

## Overview
Story này cho phép quản trị viên chỉnh sửa định lượng hoặc vai trò của một vật liệu trong công thức của combo hoa. Trong quá trình chỉnh sửa, hệ thống hiển thị trước tác động của thay đổi lên số lượng combo có thể bán để hỗ trợ người dùng đưa ra quyết định trước khi lưu.

---

## Objective
Cho phép quản trị viên:
- Chỉnh sửa định lượng của vật liệu.
- Thay đổi vai trò của vật liệu.
- Xem trước số lượng combo có thể bán sau khi thay đổi.
- Cập nhật công thức và tính lại khả năng bán.

---

## Preconditions
- Công thức đã có ít nhất một dòng vật liệu.
- Quản trị viên có quyền chỉnh sửa công thức.

---

## Trigger
Quản trị viên nhấn **Edit** trên một dòng vật liệu trong công thức.

---

## Main Features

### Edit Material Line
Hệ thống chuyển dòng vật liệu sang chế độ chỉnh sửa tại chỗ.

Cho phép chỉnh sửa:
- Định lượng.
- Vai trò (CORE hoặc SUPPORT).

### Preview Sellable Quantity
Trong khi người dùng thay đổi dữ liệu:
- Hệ thống tính toán số lượng combo có thể bán dự kiến.
- Hiển thị kết quả ngay trước khi lưu.
- Việc tính toán sử dụng dữ liệu tồn kho hiện tại.

### Save Changes
Sau khi nhấn **Save**, hệ thống:
- Kiểm tra dữ liệu hợp lệ.
- Lưu thay đổi.
- Tính lại số lượng combo có thể bán.
- Cập nhật vật liệu đang là nút thắt nếu có.

### Cancel Editing
Nếu người dùng chọn **Cancel**:
- Khôi phục giá trị ban đầu của dòng vật liệu.
- Không lưu bất kỳ thay đổi nào.

---

## Validation

Hệ thống kiểm tra:
- Định lượng phải là số nguyên lớn hơn **0**.
- Công thức luôn phải có ít nhất một vật liệu có vai trò **CORE**.
- Kiểm tra xung đột dữ liệu trước khi lưu.

---

## Exception Handling

### No CORE Material
- Không cho phép lưu nếu công thức không còn dòng **CORE**.
- Hiển thị thông báo lỗi.

### Invalid Quantity
- Không cho phép lưu.
- Hiển thị lỗi tại trường định lượng.

### Version Conflict
- Không cho phép ghi đè dữ liệu.
- Yêu cầu tải lại dữ liệu mới nhất.

---

## Acceptance Criteria

### AC-001
- Cho phép cập nhật định lượng của vật liệu.
- Tính lại số lượng combo có thể bán sau khi lưu.
- Cập nhật kết quả ngay trên màn hình.

### AC-002
- Khi chỉnh sửa định lượng nhưng chưa lưu:
  - Hệ thống hiển thị trước số lượng combo có thể bán tương ứng với giá trị đang nhập.

---

## Dependencies
- STORY-007: Add Material to Combo Formula

---

## Non-functional Requirements
- Việc tính toán số lượng combo có thể bán để hiển thị xem trước phải được thực hiện ở **máy chủ (server-side)** nhằm đảm bảo sử dụng dữ liệu tồn kho mới nhất.

---

## Out of Scope
- Chỉnh sửa nhiều dòng vật liệu cùng lúc.
- Thay đổi vật liệu của một dòng sang vật liệu khác (thực hiện bằng cách xóa dòng và thêm dòng mới).