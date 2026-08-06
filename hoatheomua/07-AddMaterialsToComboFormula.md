# STORY-007: Add Material to Combo Formula

## Overview
Story này cho phép quản trị viên thêm một vật liệu vào công thức của combo hoa bằng cách khai báo định lượng và vai trò của vật liệu. Vai trò **CORE** hoặc **SUPPORT** sẽ ảnh hưởng trực tiếp đến cách hệ thống tính số lượng combo có thể bán.

---

## Objective
Cho phép quản trị viên:
- Thêm vật liệu vào công thức của combo hoa.
- Khai báo định lượng cho từng vật liệu.
- Thiết lập hoặc thay đổi vai trò của vật liệu.
- Tự động cập nhật số lượng combo có thể bán sau khi thêm vật liệu.

---

## Preconditions
- Combo hoa đã tồn tại.
- Quản trị viên có quyền chỉnh sửa công thức.
- Có ít nhất một vật liệu ở trạng thái **Đang bán**.

---

## Trigger
Quản trị viên nhấn **Add Material** trên màn hình công thức của combo hoa.

---

## Main Features

### Add Material
Hệ thống hiển thị hộp thoại chọn vật liệu kèm ô tìm kiếm.

Sau khi chọn vật liệu:
- Tự động điền vai trò mặc định của vật liệu.
- Tự động điền đơn vị tính.
- Quản trị viên nhập định lượng.
- Có thể thay đổi vai trò nếu cần.

Sau khi nhấn **Add**, hệ thống:
- Kiểm tra dữ liệu hợp lệ.
- Kiểm tra vật liệu chưa tồn tại trong công thức.
- Thêm vật liệu vào công thức.
- Tính lại số lượng combo có thể bán.
- Cập nhật kết quả ngay trên màn hình.

### Add and Continue
- Lưu dòng vật liệu hiện tại.
- Giữ hộp thoại mở để tiếp tục thêm vật liệu khác.

### Change Role
Nếu thay đổi vai trò từ **SUPPORT** sang **CORE**:
- Hiển thị ghi chú rằng vật liệu này sẽ tham gia vào việc tính số lượng combo có thể bán.

---

## Validation

Hệ thống kiểm tra:
- Vật liệu chưa tồn tại trong công thức.
- Định lượng phải là số nguyên lớn hơn **0**.
- Chỉ cho phép chọn các vật liệu đang ở trạng thái **Đang bán**.

---

## Exception Handling

### Duplicate Material
- Không cho phép thêm.
- Gợi ý chỉnh sửa dòng vật liệu đã tồn tại.

### Invalid Quantity
- Không cho phép thêm.
- Yêu cầu nhập số nguyên dương.

### Material No Longer Available
- Không cho phép thêm.
- Làm mới danh sách vật liệu để cập nhật dữ liệu mới nhất.

---

## Acceptance Criteria

### AC-001
- Cho phép thêm vật liệu mới vào công thức.
- Tính lại số lượng combo có thể bán ngay sau khi thêm.

### AC-002
- Tự động điền vai trò mặc định của vật liệu.
- Cho phép quản trị viên thay đổi vai trò sang **CORE** hoặc **SUPPORT**.

### AC-003
- Không cho phép thêm trùng vật liệu đã có trong công thức.
- Hiển thị thông báo lỗi phù hợp.

### AC-004
- Không cho phép nhập định lượng bằng **0** hoặc số âm.
- Yêu cầu nhập số nguyên lớn hơn **0**.

### AC-005
- Vật liệu ở trạng thái **Ngừng kinh doanh** không xuất hiện trong danh sách lựa chọn.

---

## Dependencies
- STORY-006: View Combo Formula

---

## Non-functional Requirements
- Hộp thoại tìm kiếm vật liệu phản hồi trong dưới **500ms**.
- Số lượng combo có thể bán được tính lại và hiển thị trong dưới **1 giây** sau khi thêm vật liệu.

---

## Out of Scope
- Kích hoạt công thức để áp dụng cho bán hàng (STORY-022).
- Gợi ý vật liệu thay thế.