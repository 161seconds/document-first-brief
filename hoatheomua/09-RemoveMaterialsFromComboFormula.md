# STORY-009: Remove Materials from Combo Formula

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa một dòng vật liệu khỏi công thức combo hoa, để loại bỏ thành phần không còn nằm trong thiết kế của bó hoa.
- **Context**: Xóa dòng là thao tác có tác động lan tỏa: nếu xóa dòng CORE cuối cùng thì combo hoa mất khả năng bán, còn nếu xóa dòng nút thắt thì số lượng có thể bán sẽ tăng đột ngột. Cần chặn trường hợp thứ nhất và thông báo rõ trường hợp thứ hai. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần. Vật liệu nút thắt của một combo là dòng CORE có sản lượng (tức là số lượng sản phẩm đó trong kho so với định lượng mà combo cần) khả thi thấp nhất — tức vật liệu quyết định trần số lượng combo bán được.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Công thức có ít nhất một dòng vật liệu.
  - Quản trị viên có quyền sửa công thức.
- **Trigger**: Quản trị viên bấm biểu tượng xóa trên một dòng vật liệu.

## Flow
### Xóa dòng vật liệu thành công
1. Hệ thống kiểm tra dòng bị xóa có phải dòng CORE cuối cùng hay không.
2. Hệ thống hiển thị hộp thoại xác nhận kèm số lượng có thể bán dự kiến sau khi xóa.
3. Quản trị viên xác nhận.
4. Hệ thống xóa dòng khỏi công thức.
5. Hệ thống tính lại số lượng có thể bán và cập nhật nhãn vật liệu nút thắt.

### Alternative Flow
- **ALT-01**: Sau khi xóa, hệ thống hiển thị thông báo kèm hành động hoàn tác trong 10 giây; nếu quản trị viên chọn hoàn tác thì dòng được khôi phục nguyên trạng.
- **ALT-02**: Quản trị viên chọn nhiều dòng vật liệu thành phần trong combo -> Quản trị viên bấm "Xóa" -> Quản trị viên xác nhận xóa -> Hệ thống xóa dòng khỏi công thức -> Hệ thống tính lại số lượng có thể bán và cập nhật nhãn vật liệu nút thắt.

### Exception Flow
- **EXC-01**: Hệ thống chặn và thông báo công thức phải còn ít nhất một dòng CORE.
- **EXC-02**: Hệ thống thông báo dòng không còn tồn tại và làm mới màn hình.

## Acceptance Criteria
### AC-001
- **Given**: Công thức có 3 dòng gồm 2 dòng CORE và 1 dòng SUPPORT
- **When**: Quản trị viên xóa dòng SUPPORT và xác nhận
- **Then**: Hệ thống xóa dòng và công thức còn lại 2 dòng
- **And**: Số lượng có thể bán không thay đổi

### AC-002
- **Given**: Công thức chỉ còn 1 dòng vai trò CORE
- **When**: Quản trị viên yêu cầu xóa dòng đó
- **Then**: Hệ thống chặn thao tác và hiển thị lý do

### AC-003
- **Given**: Dòng bị xóa đang là vật liệu nút thắt
- **When**: Hộp thoại xác nhận hiển thị
- **Then**: Hệ thống hiển thị số lượng có thể bán hiện tại và số lượng dự kiến sau khi xóa

### AC-004
- **Given**: Quản trị viên vừa xóa một dòng vật liệu
- **When**: Chọn hành động hoàn tác trong vòng 10 giây
- **Then**: Dòng được khôi phục với đúng định lượng và vai trò trước đó

### AC-005
- **Given**: Quản trị viên chọn nhiều dòng vật liệu
- **When**: Quản trị viên bấm nút "Xóa" và xác nhận xóa
- **Then**: Hệ thống xóa các dòng vật liệu được chọn khỏi combo
- **And**: Hệ thống tính toán lại số lượng có thể bán hiện tại và số lượng dự kiến của combo hiện tại và các combo khác

## References
- **Dependencies**: STORY-007

## Non-Functional
- Thao tác xóa phải phản hồi trong dưới 1 giây.

## Out of Scope
- Xóa toàn bộ công thức của combo hoa.