# STORY-019: View Material Inventory History

## Overview
Story này cho phép quản trị viên xem toàn bộ lịch sử biến động tồn kho của một vật liệu nhằm hỗ trợ kiểm tra, đối soát và truy vết nguyên nhân khi số liệu tồn kho không khớp với thực tế.

---

## Objective
Cho phép quản trị viên:
- Xem lịch sử biến động tồn kho của vật liệu.
- Lọc lịch sử theo thời gian và loại bút toán.
- Xem thông tin chi tiết của từng bút toán.
- Điều hướng đến đơn hàng liên quan nếu có.
- Theo dõi tổng hợp nhập, xuất và điều chỉnh trong khoảng thời gian lựa chọn.

---

## Preconditions
- Vật liệu đã phát sinh ít nhất một bút toán kho.
- Quản trị viên có quyền xem lịch sử tồn kho.

---

## Trigger
Quản trị viên mở tab **Inventory History** trong màn hình chi tiết vật liệu.

---

## Main Features

### View Inventory History
Hệ thống hiển thị danh sách bút toán theo thứ tự thời gian giảm dần.

Mỗi dòng bao gồm:
- Thời điểm.
- Loại bút toán.
- Số lượng thay đổi.
- Tồn sau biến động.
- Lý do.
- Người thực hiện.
- Mã đơn hàng liên quan (nếu có).

### Filter History
Cho phép lọc theo:
- Khoảng thời gian.
- Loại bút toán.

### View Order Detail
Nếu bút toán liên quan đến đơn hàng:
- Cho phép nhấn vào mã đơn hàng.
- Mở màn hình chi tiết đơn hàng tương ứng.

### Summary View
Cho phép chuyển sang chế độ tổng hợp để xem:
- Tổng số lượng nhập.
- Tổng số lượng xuất bán.
- Tổng hao hụt.
- Tổng điều chỉnh.

---

## Validation

Hệ thống:
- Chỉ hiển thị các bút toán thuộc vật liệu được chọn.
- Không cho phép chỉnh sửa hoặc xóa các bút toán đã phát sinh.

---

## Exception Handling

### No History
- Hiển thị trạng thái chưa có lịch sử tồn kho.

### Invalid Filter
- Nếu điều kiện lọc không hợp lệ:
  - Không thực hiện tìm kiếm.
  - Yêu cầu người dùng chọn lại.

---

## Acceptance Criteria

### AC-001
- Hiển thị đầy đủ thông tin của từng bút toán:
  - Thời điểm.
  - Loại.
  - Số lượng thay đổi.
  - Tồn sau biến động.
  - Lý do.
  - Người thực hiện.

### AC-002
- Cho phép lọc lịch sử theo khoảng thời gian.
- Chỉ hiển thị các bút toán thuộc khoảng thời gian được chọn.

### AC-003
- Tổng hợp các biến động theo thời gian phải khớp với tồn thực hiện tại của vật liệu.

### AC-004
- Không cung cấp chức năng sửa hoặc xóa bút toán.
- Chỉ cho phép tạo bút toán điều chỉnh mới.

---

## Dependencies
- STORY-018: Adjust Material Inventory

---

## Non-functional Requirements
- Danh sách phải hỗ trợ phân trang.
- Thời gian tải dưới **2 giây** với tối đa **10.000 bút toán** cho một vật liệu.

---

## Out of Scope
- Báo cáo tồn kho toàn hệ thống.
- Xuất dữ liệu lịch sử ra file.