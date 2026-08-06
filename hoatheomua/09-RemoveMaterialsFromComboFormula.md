# STORY-009: Remove Material from Combo Formula

## Overview
Story này cho phép quản trị viên xóa một hoặc nhiều dòng vật liệu khỏi công thức của combo hoa khi thành phần của bó hoa thay đổi. Hệ thống phải đảm bảo công thức luôn hợp lệ, cập nhật lại số lượng combo có thể bán và xác định lại vật liệu nút thắt sau khi xóa.

---

## Objective
Cho phép quản trị viên:
- Xóa một hoặc nhiều dòng vật liệu khỏi công thức.
- Xem trước ảnh hưởng đến số lượng combo có thể bán.
- Hoàn tác thao tác xóa trong thời gian cho phép.
- Đảm bảo công thức luôn còn ít nhất một vật liệu **CORE**.

---

## Preconditions
- Công thức có ít nhất một dòng vật liệu.
- Quản trị viên có quyền chỉnh sửa công thức.

---

## Trigger
Quản trị viên nhấn **Delete** trên một hoặc nhiều dòng vật liệu.

---

## Main Features

### Delete Material
Khi người dùng chọn xóa một dòng:
- Hệ thống kiểm tra dòng bị xóa có phải là dòng **CORE** cuối cùng hay không.
- Hiển thị hộp thoại xác nhận.
- Hiển thị số lượng combo có thể bán hiện tại và dự kiến sau khi xóa.
- Sau khi xác nhận:
  - Xóa dòng khỏi công thức.
  - Tính lại số lượng combo có thể bán.
  - Cập nhật vật liệu nút thắt.

### Delete Multiple Materials
Khi người dùng chọn nhiều dòng:
- Hiển thị hộp thoại xác nhận.
- Xóa tất cả các dòng được chọn.
- Tính lại số lượng combo có thể bán của combo hiện tại.
- Cập nhật vật liệu nút thắt và các dữ liệu liên quan.

### Undo Delete
Sau khi xóa thành công:
- Hiển thị thông báo kèm nút **Undo** trong **10 giây**.
- Nếu người dùng chọn **Undo**:
  - Khôi phục đầy đủ dòng vật liệu.
  - Giữ nguyên định lượng và vai trò trước khi xóa.

---

## Validation

Hệ thống kiểm tra:
- Công thức phải còn ít nhất một dòng **CORE** sau khi xóa.
- Dòng vật liệu vẫn còn tồn tại trước khi thực hiện thao tác.

---

## Exception Handling

### Last CORE Material
- Không cho phép xóa nếu đây là dòng **CORE** cuối cùng.
- Hiển thị lý do cho người dùng.

### Material Not Found
- Nếu dòng vật liệu không còn tồn tại:
  - Hiển thị thông báo lỗi.
  - Làm mới dữ liệu trên màn hình.

---

## Acceptance Criteria

### AC-001
- Cho phép xóa dòng **SUPPORT**.
- Công thức được cập nhật chính xác.
- Số lượng combo có thể bán không thay đổi nếu chỉ xóa dòng SUPPORT.

### AC-002
- Không cho phép xóa dòng **CORE** cuối cùng.
- Hiển thị thông báo giải thích lý do.

### AC-003
- Nếu dòng bị xóa là vật liệu nút thắt:
  - Hiển thị số lượng combo có thể bán hiện tại.
  - Hiển thị số lượng dự kiến sau khi xóa trước khi xác nhận.

### AC-004
- Sau khi xóa:
  - Người dùng có thể **Undo** trong vòng **10 giây**.
  - Dòng vật liệu được khôi phục đúng định lượng và vai trò.

### AC-005
- Cho phép xóa nhiều dòng vật liệu cùng lúc.
- Tính lại số lượng combo có thể bán của combo hiện tại.
- Cập nhật số lượng dự kiến của các combo liên quan nếu bị ảnh hưởng.

---

## Dependencies
- STORY-007: Add Material to Combo Formula

---

## Non-functional Requirements
- Thao tác xóa phải hoàn thành trong dưới **1 giây**.

---

## Out of Scope
- Xóa toàn bộ công thức của một combo hoa.