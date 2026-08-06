# STORY-016: Manage Materials Reservation and Inventory Lifecycle

## Metadata
- **Story**: Là một Quản trị viên vận hành, tôi muốn hệ thống tự động giữ chỗ, trừ tồn và hoàn tồn vật liệu theo từng trạng thái của đơn hàng, để tồn kho luôn phản ánh đúng lượng hàng đã cam kết mà không bị khóa bởi các đơn không bao giờ hoàn tất.
- **Context**: Đây là mắt xích quan trọng nhất và cũng là phần brief chưa đề cập. Nếu trừ tồn quá sớm thì đơn rác sẽ khóa kho oan; nếu trừ quá muộn thì bán vượt. Thiết kế ba giai đoạn giữ chỗ mềm, trừ tồn và hoàn tồn. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần. Snapshot (bản chụp) là bản sao đông cứng của công thức combo, được lưu vào chính đơn hàng tại thời điểm tạo đơn, và không bao giờ thay đổi sau đó — kể cả khi công thức gốc bị sửa.
- **Sprint**: S4
- **Priority**: Should
- **Assignee**: BE: Hồ Hoàng Nam | QA: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Đơn hàng đã lưu snapshot công thức của từng combo hoa trong đơn.
  - Máy trạng thái đơn hàng hiện có đã hoạt động.
- **Trigger**: Đơn hàng được tạo hoặc chuyển trạng thái.

## Flow
### Vòng đời giữ chỗ và trừ tồn của đơn hoàn tất
1. Đơn hàng chuyển sang `pending`, hệ thống tạo giữ chỗ mềm cho toàn bộ vật liệu trong snapshot, bao gồm cả vai trò SUPPORT, kèm thời hạn (a) phút (cần chốt lại).
2. Đơn hàng chuyển sang `confirm` sau khi thanh toán thành công, hệ thống chuyển giữ chỗ mềm thành giữ chỗ cứng và gỡ thời hạn.
3. Đơn hàng chuyển sang `confirmed`, hệ thống giảm tồn thực và giải phóng bản ghi giữ chỗ tương ứng.
4. Đơn hàng đi tiếp qua `complete` mà không phát sinh thêm biến động tồn.

### Alternative Flow
- **ALT-01**: Đơn ở `pending` quá (a) phút chưa thanh toán; hệ thống nhả giữ chỗ, trả lại tồn khả dụng và chuyển đơn sang `cancelled`.
- **ALT-02**: Khách hàng hủy đặt hàng lúc chưa thanh toán -> đơn chuyển sang `cancelled` khi chưa vào `confirm`.

### Exception Flow
- **EXC-01**: Hệ thống vẫn thực hiện trừ tồn cho các vật liệu đủ hàng, ghi nhận phần thiếu và tạo cảnh báo bù hàng cho quản trị viên.
- **EXC-02**: Hệ thống hoàn tác toàn bộ bút toán của đơn, giữ đơn ở trạng thái trước đó và ghi nhật ký lỗi.

## Acceptance Criteria
### AC-001
- **Given**: Hoa hồng đỏ có tồn thực 10, đang giữ chỗ 0 và định lượng combo hoa là 2
- **When**: Khách hàng tạo đơn 1 combo hoa và đơn ở trạng thái `pending`
- **Then**: Đang giữ chỗ của hoa hồng đỏ tăng lên 2 và tồn khả dụng còn 8
- **And**: Tồn thực vẫn là 10

### AC-002
- **Given**: Đơn ở `pending` với giữ chỗ mềm 2 cành hoa hồng đỏ
- **When**: Quá (a) phút mà đơn chưa được thanh toán
- **Then**: Hệ thống nhả giữ chỗ và tồn khả dụng trở lại 10
- **And**: Đơn chuyển sang `cancel`

### AC-006
- **Given**: Công thức có hoa baby vai trò SUPPORT định lượng 1 và tồn thực 5
- **When**: Đơn chuyển sang `confirm`
- **Then**: Tồn thực hoa baby giảm còn 4

### AC-007
- **Given**: Hoa baby vai trò SUPPORT có tồn thực 0
- **When**: Đơn chuyển sang `confirm`
- **Then**: Đơn vẫn được xác nhận thành công
- **And**: Hệ thống ghi nhận thiếu hụt và tạo cảnh báo bù hàng cho quản trị viên

## References
- **Dependencies**: STORY-015

## Non-Functional
- Mọi thao tác trừ tồn và hoàn tồn phải bảo đảm tính bất biến khi gọi lặp, một sự kiện chuyển trạng thái chỉ sinh một bộ bút toán duy nhất.
- Tác vụ nhả giữ chỗ hết hạn phải chạy định kỳ với chu kỳ không quá 1 phút.

## Out of Scope
- Đổi combo hoa trong đơn sau khi đã xác nhận.
- Giao hàng một phần.