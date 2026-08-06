# STORY-004: Update Material Information

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn cập nhật thông tin của một vật liệu đã khai báo, để thông tin danh mục luôn phản ánh đúng thực tế kinh doanh.
- **Context**: Một số thuộc tính như tên, ngưỡng cảnh báo, có thể thay đổi theo thời gian. Tuy nhiên đơn vị tính là thuộc tính neo toàn bộ số liệu kho lịch sử, nên phải bị khóa sau khi có giao dịch để tránh làm sai lệch dữ liệu quá khứ. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một bó hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Vật liệu cần sửa đã tồn tại trong hệ thống.
  - Quản trị viên có quyền sửa vật liệu.
- **Trigger**: Quản trị viên chọn một vật liệu trong danh sách và bấm "Chỉnh sửa".

## Flow
### Cập nhật thông tin vật liệu thành công
1. Hệ thống mở biểu mẫu chỉnh sửa với dữ liệu hiện tại của vật liệu.
2. Hệ thống khóa trường đơn vị tính nếu vật liệu đã phát sinh trong lịch sử đơn hàng.
3. Quản trị viên thay đổi các trường được phép sửa.
4. Quản trị viên bấm "Lưu".
5. Hệ thống kiểm tra hợp lệ và lưu thay đổi.
6. Hệ thống hiển thị thông báo thành công và ghi nhận thời điểm cập nhật.

### Alternative Flow
- **ALT-01**: Quản trị viên đổi vai trò mặc định của vật liệu; hệ thống lưu và hiển thị cảnh báo rằng thay đổi chỉ áp dụng cho các dòng công thức tạo mới về sau, không ảnh hưởng công thức hiện có.
- **ALT-02**: Quản trị viên bấm "Hủy" khi đã có thay đổi chưa lưu; hệ thống yêu cầu xác nhận trước khi rời khỏi biểu mẫu.

### Exception Flow
- **EXC-01**: Hệ thống chặn và hiển thị hướng dẫn tạo vật liệu mới.
- **EXC-02**: Hệ thống phát hiện xung đột phiên bản, chặn ghi đè và yêu cầu tải lại dữ liệu.
- **EXC-03**: Hệ thống thông báo và đưa người dùng về danh sách.

## Acceptance Criteria
### AC-001
- **Given**: Vật liệu đang ở trạng thái `đang bán`
- **When**: Quản trị viên sửa tên rồi bấm Lưu
- **Then**: Hệ thống lưu giá trị mới và hiển thị trên danh sách
- **And**: Thời điểm cập nhật gần nhất được ghi lại

### AC-002
- **Given**: Vật liệu đã phát sinh ít nhất một bút toán kho
- **When**: Quản trị viên mở biểu mẫu chỉnh sửa
- **Then**: Trường đơn vị tính ở trạng thái chỉ đọc kèm giải thích lý do khi hover

### AC-004
- **Given**: Hai quản trị viên cùng mở một vật liệu để sửa
- **When**: Người thứ hai bấm Lưu sau khi người thứ nhất đã lưu thành công
- **Then**: Hệ thống chặn ghi đè và yêu cầu tải lại dữ liệu mới nhất

## References
- **Dependencies**: STORY-003

## Non-Functional
- Cơ chế kiểm soát xung đột đồng thời phải dựa trên phiên bản bản ghi, không dựa trên khóa bi quan kéo dài.

## Out of Scope
- Chuyển trạng thái ngừng kinh doanh.
- Lịch sử thay đổi thông tin vật liệu.