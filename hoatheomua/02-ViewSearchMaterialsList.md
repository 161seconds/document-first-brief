# STORY-002: View and Search Material List

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem và tìm kiếm danh sách vật liệu cấu thành sản phẩm, để nắm được hệ thống đang quản lý những loại hoa và phụ kiện nào cùng tình trạng tồn kho của chúng.
- **Context**: Trước đây hệ thống chỉ quản lý combo ở mức thành phẩm. Khi bổ sung quản lý vật liệu, quản trị viên cần một màn hình danh sách làm điểm vào cho toàn bộ nghiệp vụ vật liệu, định lượng và tồn kho. Đây là story nền của module. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào trang admin.
  - Tài khoản có quyền xem dữ liệu vật liệu theo cấu hình RBAC.
- **Trigger**: Quản trị viên chọn mục "Vật liệu" trên menu điều hướng của trang admin.

## Flow
### Xem danh sách vật liệu
1. Hệ thống hiển thị danh sách vật liệu, mặc định sắp xếp theo tên tăng dần, phân trang 20 dòng mỗi trang. (FE đề xuất)
2. Mỗi dòng hiển thị: mã vật liệu, tên, nhóm, vai trò mặc định, đơn vị tính, tồn thực, đang giữ chỗ, tồn khả dụng, trạng thái.
3. Hệ thống hiển thị bộ lọc theo nhóm, vai trò mặc định, trạng thái và ô tìm kiếm theo mã hoặc tên.
4. Quản trị viên nhập từ khóa hoặc chọn điều kiện lọc.
5. Hệ thống trả về danh sách khớp điều kiện và hiển thị tổng số kết quả.

### Alternative Flow
- **ALT-01**: Quản trị viên bấm tiêu đề cột tồn khả dụng; hệ thống sắp xếp lại danh sách theo cột được chọn và giữ nguyên điều kiện lọc hiện tại.

### Exception Flow
- **EXC-01**: Hệ thống hiển thị trạng thái rỗng kèm gợi ý xóa bộ lọc hoặc tạo vật liệu mới.
- **EXC-02**: Hệ thống chặn truy cập và hiển thị thông báo không có quyền truy cập chức năng.
- **EXC-03**: Hệ thống hiển thị thông báo lỗi kèm nút thử lại, không hiển thị dữ liệu cũ gây hiểu nhầm.

## Acceptance Criteria
### AC-001
- **Given**: Quản trị viên đã đăng nhập và có quyền xem vật liệu
- **When**: Truy cập màn hình danh sách vật liệu
- **Then**: Hệ thống hiển thị danh sách vật liệu phân trang theo dòng (số dòng cụ thể FE đề xuất), sắp xếp theo tên tăng dần
- **And**: Mỗi dòng hiển thị đủ tồn thực, đang giữ chỗ và tồn khả dụng

### AC-002
- **Given**: Danh sách vật liệu đang hiển thị
- **When**: Quản trị viên nhập từ khóa vào ô tìm kiếm
- **Then**: Hệ thống trả về các vật liệu có mã hoặc tên chứa từ khóa, có debounce theo từng chữ
- **And**: Hiển thị tất cả kết quả tìm được

### AC-003
- **Given**: Quản trị viên đang ở màn hình danh sách
- **When**: Chọn đồng thời bộ lọc nhóm và bộ lọc trạng thái
- **Then**: Hệ thống trả về danh sách thỏa mãn đồng thời tất cả điều kiện lọc

### AC-004
- **Given**: Bộ lọc hiện tại không khớp vật liệu nào
- **When**: Hệ thống trả kết quả
- **Then**: Hiển thị thông báo không tìm thấy vật liệu kèm hành động xóa bộ lọc

## Non-Functional
- Thời gian phản hồi danh sách dưới 2 giây với tối đa 2.000 vật liệu. (Đề xuất)
- Tìm kiếm hỗ trợ Tiếng Việt và debounce trên từng kí tự.

## Out of Scope
- Xuất danh sách vật liệu ra file.
- Xem chi tiết lịch sử biến động tồn của từng vật liệu. (check 02)