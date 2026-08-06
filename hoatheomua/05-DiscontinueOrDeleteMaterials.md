# STORY-005: Discontinue or Delete Material

## Overview
Story này cho phép quản trị viên ngừng kinh doanh hoặc xóa mềm một vật liệu không còn sử dụng. Hệ thống phải đảm bảo không làm ảnh hưởng đến dữ liệu lịch sử, công thức đang sử dụng và các đơn hàng đã được tạo.

---

## Objective
Cho phép quản trị viên:
- Ngừng kinh doanh hoặc xóa mềm một vật liệu.
- Kiểm tra các tham chiếu trước khi thực hiện.
- Bảo toàn dữ liệu lịch sử và công thức hiện có.
- Ngăn vật liệu không còn sử dụng xuất hiện trong các thao tác tạo mới.

---

## Preconditions
- Vật liệu đã tồn tại trong hệ thống.
- Quản trị viên có quyền xóa hoặc ngừng kinh doanh vật liệu.

---

## Trigger
Quản trị viên chọn **Discontinue** hoặc **Delete** trên một vật liệu.

---

## Main Features

### Discontinue/Delete Material
Khi thực hiện thao tác:
- Hệ thống kiểm tra các tham chiếu đến vật liệu:
  - Công thức đang sử dụng.
  - Giữ chỗ tồn kho đang mở.
- Hiển thị hộp thoại xác nhận.
- Liệt kê các combo đang sử dụng vật liệu.
- Sau khi xác nhận:
  - Chuyển trạng thái vật liệu sang **Ngừng kinh doanh** hoặc **Đã xóa** (xóa mềm).
  - Loại vật liệu khỏi danh sách chọn khi tạo công thức mới.
  - Hiển thị cảnh báo trên các công thức đang tham chiếu vật liệu.

### Reopen Material
Nếu vật liệu đang ở trạng thái **Ngừng kinh doanh**:
- Quản trị viên có thể chọn **Reopen**.
- Hệ thống chuyển trạng thái về **Đang bán**.

---

## Validation

Hệ thống kiểm tra:
- Vật liệu đang được sử dụng bởi bao nhiêu công thức.
- Vật liệu có phải là vật liệu **CORE** duy nhất của sản phẩm hay không.
- Các giữ chỗ tồn kho hiện tại.

---

## Exception Handling

### Material Still In Use
- Nếu việc ngừng kinh doanh khiến combo không còn bán được:
  - Hiển thị cảnh báo.
  - Yêu cầu người dùng xác nhận lần thứ hai.

### Existing Reservations
- Vẫn cho phép ngừng kinh doanh.
- Giữ nguyên các giữ chỗ hiện có.
- Không ảnh hưởng đến các đơn hàng đang xử lý.

---

## Acceptance Criteria

### AC-001
- Khi vật liệu đang được công thức tham chiếu:
  - Không xóa dữ liệu.
  - Chuyển sang trạng thái **Ngừng kinh doanh**.
  - Giữ nguyên dữ liệu kho và lịch sử đơn hàng.

### AC-002
- Popup xác nhận hiển thị danh sách tất cả các combo đang sử dụng vật liệu.

### AC-003
- Vật liệu ở trạng thái **Ngừng kinh doanh** không xuất hiện khi thêm vật liệu vào công thức mới.

### AC-004
- Nếu vật liệu là **CORE** duy nhất của một sản phẩm đang bán:
  - Hiển thị cảnh báo sản phẩm sẽ không còn khả năng bán.
  - Yêu cầu xác nhận lần hai trước khi thực hiện.

---

## Dependencies
- STORY-004: Update Material Information

---

## Non-functional Requirements
- Thời gian kiểm tra các tham chiếu phải hoàn thành trong dưới **3 giây**.

---

## Out of Scope
- Thay thế hàng loạt vật liệu này bằng vật liệu khác trong các công thức.