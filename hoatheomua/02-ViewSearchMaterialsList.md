# STORY-002: View and Search Material List

## Overview
Story này xây dựng màn hình chính của **Material Management**, cho phép quản trị viên xem, tìm kiếm, lọc và sắp xếp danh sách vật liệu dùng để cấu thành các combo hoa, đồng thời theo dõi tình trạng tồn kho của từng vật liệu.


## Objective
Cho phép quản trị viên:
- Xem danh sách vật liệu.
- Tìm kiếm vật liệu theo mã hoặc tên.
- Lọc vật liệu theo nhóm, vai trò mặc định và trạng thái.
- Sắp xếp danh sách theo các cột hỗ trợ.
- Theo dõi thông tin tồn kho của từng vật liệu.



## Preconditions
- Quản trị viên đã đăng nhập vào hệ thống.
- Tài khoản có quyền xem dữ liệu vật liệu (RBAC).


## Trigger
Quản trị viên chọn menu **Materials** trên trang Admin.


## Main Features

### Material List
- Hiển thị danh sách vật liệu theo dạng bảng.
- Phân trang 20 dòng mỗi trang.
- Mặc định sắp xếp theo tên vật liệu (A → Z).

### Display Information
Mỗi vật liệu hiển thị các thông tin:
- Mã vật liệu
- Tên vật liệu
- Nhóm
- Vai trò mặc định
- Đơn vị tính
- Tồn thực
- Đang giữ chỗ
- Tồn khả dụng
- Trạng thái

### Search
- Tìm kiếm theo mã hoặc tên vật liệu.
- Hỗ trợ tìm kiếm tiếng Việt.
- Debounce khi nhập từ khóa.

### Filter
Hỗ trợ lọc theo:
- Nhóm
- Vai trò mặc định
- Trạng thái

Có thể kết hợp nhiều bộ lọc cùng lúc.

### Sorting
- Cho phép sắp xếp theo các cột (ví dụ: Tồn khả dụng).
- Giữ nguyên điều kiện tìm kiếm và bộ lọc hiện tại sau khi sắp xếp.


## Exception Handling

### No Results
- Hiển thị trạng thái không có dữ liệu.
- Gợi ý xóa bộ lọc.
- Gợi ý tạo vật liệu mới.

### Unauthorized Access
- Chặn truy cập màn hình.
- Hiển thị thông báo không có quyền sử dụng chức năng.

### System Error
- Hiển thị thông báo lỗi.
- Cho phép người dùng thử lại.
- Không hiển thị dữ liệu cũ để tránh gây nhầm lẫn.


## Acceptance Criteria

### AC-001
- Hiển thị danh sách vật liệu có phân trang.
- Mặc định sắp xếp theo tên tăng dần.
- Hiển thị đầy đủ tồn thực, đang giữ chỗ và tồn khả dụng.

### AC-002
- Tìm kiếm theo mã hoặc tên vật liệu.
- Debounce theo từng ký tự.
- Hiển thị tất cả kết quả phù hợp.

### AC-003
- Hỗ trợ áp dụng đồng thời nhiều bộ lọc.
- Chỉ hiển thị các vật liệu thỏa mãn tất cả điều kiện.

### AC-004
- Khi không có kết quả, hiển thị trạng thái rỗng.
- Cho phép người dùng xóa bộ lọc để tìm kiếm lại.


## Non-functional Requirements
- Thời gian phản hồi dưới **2 giây** với tối đa **2.000 vật liệu**.
- Hỗ trợ tìm kiếm tiếng Việt.
- Debounce khi tìm kiếm.


## Out of Scope
- Xuất danh sách vật liệu ra file.
- Xem lịch sử biến động tồn kho của vật liệu.