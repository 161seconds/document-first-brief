# STORY-017: Copy Combo Formula

## Overview
Story này cho phép quản trị viên sao chép công thức từ một combo hoa đã có sang một combo hoa khác nhằm giảm thời gian nhập liệu và hạn chế sai sót khi tạo các combo có cấu thành tương tự.

---

## Objective
Cho phép quản trị viên:
- Sao chép toàn bộ công thức của một combo hoa.
- Xem trước công thức trước khi sao chép.
- Chỉnh sửa công thức sau khi sao chép.
- Giữ công thức mới ở trạng thái **Draft (NHAP)** trước khi kích hoạt.

---

## Preconditions
- Có ít nhất một combo hoa có công thức ở trạng thái **Active**.
- Combo hoa đích chưa có công thức hoặc công thức hiện tại đang ở trạng thái **NHAP**.

---

## Trigger
Quản trị viên chọn **Copy from Another Combo** trên màn hình công thức của combo hoa đích.

---

## Main Features

### Select Source Formula
Hệ thống hiển thị:
- Danh sách các combo hoa có công thức đang hoạt động.
- Ô tìm kiếm để tìm nhanh combo nguồn.

### Preview Formula
Sau khi chọn combo nguồn:
- Hiển thị toàn bộ danh sách vật liệu sẽ được sao chép.
- Bao gồm:
  - Tên vật liệu.
  - Định lượng.
  - Vai trò.

### Copy Formula
Sau khi xác nhận:
- Tạo công thức mới cho combo đích.
- Sao chép toàn bộ:
  - Vật liệu.
  - Định lượng.
  - Vai trò.
- Đặt công thức ở trạng thái **NHAP**.

### Edit Formula
Sau khi sao chép:
- Mở màn hình chỉnh sửa công thức.
- Cho phép quản trị viên tiếp tục điều chỉnh trước khi kích hoạt.

---

## Validation

Hệ thống kiểm tra:
- Combo nguồn có công thức ở trạng thái **Active**.
- Combo đích chưa có công thức hoặc công thức đang ở trạng thái **NHAP**.

Nếu combo đích đã có công thức **NHAP**:
- Hiển thị cảnh báo.
- Yêu cầu xác nhận trước khi ghi đè.

---

## Exception Handling

### Discontinued Material
Nếu công thức nguồn chứa vật liệu đã **Ngừng kinh doanh**:
- Vẫn sao chép công thức.
- Đánh dấu các dòng cần thay thế.
- Không cho phép kích hoạt công thức cho đến khi xử lý.

### Invalid Target Formula
Nếu công thức hiện tại không được phép ghi đè:
- Không cho phép sao chép.
- Hướng dẫn tạo phiên bản công thức mới.

---

## Acceptance Criteria

### AC-001
- Sao chép toàn bộ công thức sang combo đích.
- Giữ nguyên vật liệu, định lượng và vai trò.

### AC-002
- Công thức mới ở trạng thái **NHAP**.
- Chưa ảnh hưởng đến số lượng có thể bán của combo.

### AC-003
- Nếu công thức nguồn chứa vật liệu **Ngừng kinh doanh**:
  - Đánh dấu các dòng cần thay thế.
  - Không cho phép kích hoạt công thức cho đến khi được xử lý.

---

## Non-functional Requirements
- Thao tác sao chép hoàn thành trong dưới **2 giây** với công thức tối đa **30 dòng vật liệu**.

---

## Out of Scope
- Sao chép công thức cho nhiều combo cùng lúc.
- Tạo mẫu công thức dùng chung.