# STORY-006: View Combo Formula

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem công thức định lượng của một combo hoa, để biết combo hoa được cấu thành từ những vật liệu nào với số lượng bao nhiêu và vật liệu nào đang giới hạn khả năng bán.
- **Context**: Đây là màn hình trung tâm của module định lượng. Ngoài việc liệt kê thành phần, màn hình cần chỉ rõ vật liệu nút thắt để quản trị viên biết cần nhập thêm loại hoa nào khi muốn tăng sản lượng bán. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Combo hoa đã tồn tại trong hệ thống.
  - Quản trị viên có quyền xem công thức.
- **Trigger**: Quản trị viên mở tab "Định lượng" trong màn hình chi tiết combo hoa.

## Flow
### Xem công thức hiệu lực
1. Hệ thống load công thức đang hiệu lực của combo hoa.
2. Hệ thống hiển thị danh sách loại vật liệu, tách hai nhóm: vai trò CORE trước, vai trò SUPPORT sau.
3. Mỗi dòng hiển thị: tên vật liệu, vai trò, định lượng, đơn vị tính, tồn khả dụng của vật liệu, số combo hoa.
4. Hệ thống hiển thị số lượng có thể bán của combo hoa ở đầu màn hình.
5. Hệ thống đánh dấu dòng vật liệu nút thắt bằng nhãn riêng.

### Alternative Flow
- **ALT-01**: Hệ thống hiển thị thông báo Combo hoa chưa có công thức, vui lòng thêm vật liệu. Hệ thống hiển thị trạng thái rỗng kèm nút "Thêm vật liệu".

### Exception Flow
- **EXC-01**: Hệ thống hiển thị cảnh báo trên dòng tương ứng.
- **EXC-02**: Hệ thống hiển thị cảnh báo combo hoa không thể bán và số lượng có thể bán là 0.
- **EXC-03**: Hệ thống hiển thị thông báo lỗi kèm nút thử lại.

## Acceptance Criteria
### AC-001
- **Given**: Ví dụ combo hoa "Set hoa mùa xuân" có công thức gồm 2 hoa hồng đỏ vai trò CORE, 3 hoa hồng trắng vai trò CORE và 1 bó baby vai trò SUPPORT
- **When**: Quản trị viên mở tab Định lượng
- **Then**: Hệ thống hiển thị đủ 3 dòng, nhóm CORE hiển thị trước nhóm SUPPORT
- **And**: Mỗi dòng hiển thị đúng định lượng và đơn vị tính

### AC-002
- **Given**: Vật liệu vai trò SUPPORT có tồn khả dụng bằng 0
- **When**: Hệ thống tính số lượng có thể bán
- **Then**: Số lượng có thể bán vẫn được tính chỉ dựa trên các dòng CORE
- **And**: Dòng SUPPORT hiển thị cảnh báo hết hàng nhưng không làm số lượng có thể bán về 0

### AC-003
- **Given**: Combo hoa chưa có dòng vật liệu nào
- **When**: Quản trị viên mở tab Định lượng
- **Then**: Hệ thống hiển thị trạng thái rỗng và số lượng có thể bán bằng 0

### AC-004
- **Given**: Công thức có một dòng tham chiếu vật liệu ở trạng thái `ngừng kinh doanh`
- **When**: Màn hình công thức được hiển thị
- **Then**: Dòng đó hiển thị cảnh báo yêu cầu thay thế vật liệu

## References
- **Dependencies**: STORY-002

## Non-Functional
- Số lượng có thể bán hiển thị phải phản ánh dữ liệu không cũ hơn 60 giây.

## Out of Scope
- Chỉnh sửa công thức.
- So sánh khác biệt giữa hai phiên bản công thức.