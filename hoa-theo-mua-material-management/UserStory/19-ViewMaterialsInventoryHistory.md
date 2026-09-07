# STORY-019: View Materials Inventory History

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem toàn bộ lịch sử biến động tồn của một vật liệu, để truy được nguyên nhân khi số liệu kho lệch so với thực tế.
- **Context**: Khi tồn kho sai, câu hỏi đầu tiên luôn là ai đã thay đổi, thay đổi bao nhiêu và vì lý do gì. Sổ cái bút toán đã được thiết kế từ STORY-024, story này chỉ trình bày dữ liệu đó ra giao diện phục vụ đối soát. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S4
- **Priority**: Won't
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Vật liệu đã phát sinh ít nhất một bút toán kho.
  - Quản trị viên có quyền xem lịch sử kho.
- **Trigger**: Quản trị viên mở tab "Lịch sử tồn kho" trong màn hình chi tiết vật liệu.

## Flow
### Xem lịch sử biến động
1. Hệ thống hiển thị danh sách bút toán theo thứ tự thời gian giảm dần.
2. Mỗi dòng hiển thị: thời điểm, loại bút toán, số lượng thay đổi, tồn sau biến động, lý do, người thực hiện, mã đơn hàng liên quan nếu có.
3. Hệ thống hiển thị bộ lọc theo khoảng thời gian và loại bút toán.
4. Quản trị viên chọn điều kiện lọc và xem kết quả.

### Alternative Flow
- **ALT-01**: Quản trị viên bấm vào mã đơn hàng trên một bút toán loại xuất bán; hệ thống mở chi tiết đơn hàng tương ứng.
- **ALT-02**: Quản trị viên chọn chế độ tổng hợp; hệ thống hiển thị tổng số lượng nhập, xuất bán, hao hụt và điều chỉnh trong khoảng thời gian đã chọn.

### Exception Flow
- **EXC-01**: Hệ thống hiển thị trạng thái rỗng.
- **EXC-02**: Hệ thống chặn và yêu cầu chọn lại.

## Acceptance Criteria
### AC-001
- **Given**: Vật liệu đã có các bút toán nhập, xuất bán và hao hụt
- **When**: Quản trị viên mở tab lịch sử tồn kho
- **Then**: Mỗi dòng hiển thị đủ thời điểm, loại, số lượng thay đổi, tồn sau biến động, lý do và người thực hiện

### AC-002
- **Given**: Danh sách bút toán đang hiển thị
- **When**: Quản trị viên chọn khoảng thời gian 7 ngày gần nhất
- **Then**: Hệ thống chỉ hiển thị bút toán trong khoảng đó

### AC-003
- **Given**: Toàn bộ bút toán của vật liệu được liệt kê
- **When**: Cộng dồn số lượng thay đổi theo thứ tự thời gian
- **Then**: Kết quả bằng đúng tồn thực hiện tại của vật liệu

### AC-004
- **Given**: Quản trị viên đang xem một bút toán đã ghi nhận
- **When**: Tìm hành động sửa hoặc xóa bút toán
- **Then**: Hệ thống không cung cấp hành động này, chỉ có hành động tạo bút toán điều chỉnh

## References
- **Dependencies**: STORY-018

## Non-Functional
- Danh sách phân trang, tải trong dưới 2 giây với tối đa 10.000 bút toán trên một vật liệu.

## Out of Scope
- Báo cáo tồn kho toàn hệ thống và xuất file.