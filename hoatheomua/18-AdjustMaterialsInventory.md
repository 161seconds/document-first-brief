# STORY-018: Adjust Material Inventory

## Overview
Story này cho phép quản trị viên điều chỉnh tồn kho của vật liệu khi xảy ra hao hụt, kiểm kê hoặc các trường hợp khác nhằm đảm bảo dữ liệu tồn kho trên hệ thống luôn phản ánh đúng thực tế. Sau mỗi lần điều chỉnh, hệ thống sẽ cập nhật lại số lượng có thể bán của các combo liên quan.

---

## Objective
Cho phép quản trị viên:
- Điều chỉnh tồn kho của vật liệu.
- Ghi nhận lý do điều chỉnh.
- Xem trước tồn kho sau điều chỉnh.
- Xem tác động đến số lượng có thể bán của các combo.
- Lưu lịch sử điều chỉnh tồn kho.

---

## Preconditions
- Vật liệu đã tồn tại trong hệ thống.
- Tồn thực của vật liệu lớn hơn hoặc bằng **0**.
- Quản trị viên có quyền điều chỉnh tồn kho.

---

## Trigger
Quản trị viên chọn **Adjust Inventory** trên một vật liệu.

---

## Main Features

### Adjust Inventory
Hệ thống hiển thị:
- Tồn thực hiện tại.
- Tồn khả dụng hiện tại.

Quản trị viên chọn loại điều chỉnh:
- Hao hụt.
- Kiểm kê.
- Điều chỉnh khác.

Sau đó nhập:
- Số lượng thay đổi hoặc số tồn thực tế.
- Lý do điều chỉnh (bắt buộc).

### Preview Result
Trước khi xác nhận:
- Hiển thị tồn kho dự kiến sau điều chỉnh.
- Hiển thị tác động đến số lượng có thể bán của các combo liên quan.

### Save Adjustment
Sau khi xác nhận:
- Sinh bút toán điều chỉnh.
- Cập nhật tồn thực.
- Tính lại số lượng có thể bán của các combo bị ảnh hưởng.

### Stock Count Adjustment
Nếu nhập số tồn thực tế:
- Hệ thống tự động tính chênh lệch.
- Sinh bút toán điều chỉnh đúng bằng phần chênh lệch.

---

## Validation

Hệ thống kiểm tra:
- Lý do điều chỉnh không được để trống.
- Không cho phép điều chỉnh làm giảm tồn thực vượt quá phần tồn không bị giữ chỗ.
- Ghi nhận đầy đủ người thực hiện điều chỉnh.

---

## Exception Handling

### Reserved Inventory
Nếu số lượng cần giảm vượt quá phần tồn chưa bị giữ chỗ:
- Không cho phép lưu.
- Hiển thị số lượng đang được giữ chỗ bởi các đơn hàng.

### Missing Reason
- Không cho phép lưu.
- Yêu cầu nhập lý do điều chỉnh.

### Affected Combos
Nếu điều chỉnh ảnh hưởng đến khả năng bán của các combo:
- Vẫn cho phép lưu.
- Hiển thị danh sách các combo bị ảnh hưởng.

---

## Acceptance Criteria

### AC-001
- Cho phép ghi nhận hao hụt.
- Cập nhật tồn thực chính xác.
- Sinh bút toán điều chỉnh kèm lý do và người thực hiện.

### AC-002
- Không cho phép lưu khi chưa nhập lý do điều chỉnh.

### AC-003
- Không cho phép giảm tồn vượt quá phần tồn chưa bị giữ chỗ.
- Hiển thị thông báo phù hợp.

### AC-004
- Khi nhập số tồn thực tế:
  - Hệ thống tự động tính chênh lệch.
  - Sinh bút toán kiểm kê.
  - Cập nhật tồn thực.

### AC-005
- Nếu điều chỉnh làm giảm số lượng có thể bán của một hoặc nhiều combo:
  - Hiển thị cảnh báo.
  - Liệt kê các combo bị ảnh hưởng.

---

## Non-functional Requirements
- Mọi bút toán điều chỉnh phải lưu đầy đủ thông tin người thực hiện.
- Không cho phép thực hiện điều chỉnh tồn kho ẩn danh.

---

## Out of Scope
- Quy trình kiểm kê toàn kho theo đợt.
- Cơ chế phê duyệt điều chỉnh tồn kho bởi cấp quản lý.