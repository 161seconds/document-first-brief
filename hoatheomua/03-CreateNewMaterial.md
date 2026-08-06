# STORY-003: Create New Material

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn khai báo một vật liệu mới với đầy đủ đơn vị tính và vai trò mặc định, để vật liệu đó có thể được đưa vào công thức cấu thành combo và được theo dõi tồn kho.
- **Context**: Danh mục vật liệu là dữ liệu nền của toàn bộ module. Việc khai báo sai đơn vị tính hoặc vai trò ngay từ đầu sẽ kéo theo sai lệch tồn kho và tính khả năng bán, nên các trường này cần được ràng buộc chặt ngay tại bước tạo.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập và có quyền tạo vật liệu.
  - Đang ở màn hình danh sách vật liệu.
- **Trigger**: Quản trị viên bấm nút "Thêm vật liệu".

## Flow
### Tạo vật liệu mới thành công
1. Hệ thống mở biểu mẫu tạo vật liệu.
2. Quản trị viên nhập mã vật liệu, tên, nhóm, vai trò mặc định, đơn vị tính, ngưỡng cảnh báo tồn thấp, nhãn mùa vụ và mô tả.
3. Quản trị viên bấm "Lưu".
4. Hệ thống kiểm tra tính hợp lệ và tính duy nhất của mã vật liệu.
5. Hệ thống tạo vật liệu ở trạng thái `đang bán` với tồn thực bằng 0.
6. Hệ thống hiển thị thông báo thành công và đưa vật liệu mới vào danh sách.

### Alternative Flow
- **ALT-01**: Quản trị viên chọn "Lưu và tạo tiếp"; hệ thống lưu vật liệu rồi mở biểu mẫu trống mới, giữ lại giá trị nhóm và đơn vị tính vừa dùng.
- **ALT-02**: Quản trị viên bỏ trống mã vật liệu; hệ thống sinh mã theo quy tắc tiền tố nhóm cộng số thứ tự.

### Exception Flow
- **EXC-01**: Hệ thống chặn lưu và hiển thị lỗi tại trường mã.
- **EXC-02**: Hệ thống chặn lưu và đánh dấu các trường còn trống.
- **EXC-03**: Hệ thống chặn lưu và yêu cầu nhập số nguyên không âm.
- **EXC-04**: Hệ thống giữ nguyên dữ liệu đã nhập trên biểu mẫu và hiển thị nút thử lại.

## Acceptance Criteria
### AC-001
- **Given**: Quản trị viên đang ở biểu mẫu tạo vật liệu
- **When**: Nhập đầy đủ trường bắt buộc với mã chưa tồn tại và bấm Lưu
- **Then**: Hệ thống tạo vật liệu ở trạng thái đang bán với tồn thực bằng 0
- **And**: Vật liệu xuất hiện trong danh sách và có thể được chọn khi thêm vào công thức

### AC-002
- **Given**: Trong hệ thống đã tồn tại vật liệu mã `flowerId`
- **When**: Quản trị viên tạo vật liệu mới cũng dùng mã `flowerId`
- **Then**: Hệ thống từ chối lưu và hiển thị thông báo mã vật liệu đã tồn tại

### AC-003
- **Given**: Quản trị viên bỏ trống vai trò mặc định hoặc đơn vị tính
- **When**: Bấm Lưu
- **Then**: Hệ thống chặn lưu và yêu cầu nhập hai trường này

### AC-004
- **Given**: Vật liệu vừa được tạo thành công
- **When**: Xem chi tiết vật liệu
- **Then**: Tồn thực bằng 0, đang giữ chỗ bằng 0 và tồn khả dụng bằng 0
- **And**: Chưa có bút toán kho nào được sinh ra

## References
- **Dependencies**: STORY-002

## Non-Functional
- Biểu mẫu phải chống gửi trùng khi bấm Lưu nhiều lần.
- Thông báo lỗi hiển thị bằng tiếng Việt, gắn trực tiếp vào trường lỗi.

## Out of Scope
- Nhập tồn kho ban đầu (thuộc STORY-024).
- Nhập vật liệu hàng loạt từ file.
- Quản lý giá vốn vật liệu (theo Q-09, tách phase sau).