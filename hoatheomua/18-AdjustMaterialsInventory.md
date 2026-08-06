# STORY-018: Adjust Materials Inventory

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn điều chỉnh tồn kho kèm lý do khi kiểm kê hoặc khi hoa bị hỏng, để số liệu trên hệ thống khớp với thực tế và không bán ra những combo hoa không thể giao.
- **Context**: Hoa tươi là mặt hàng mau hỏng, tỷ lệ hao hụt tự nhiên cao. Nếu chỉ có bút toán nhập và bán, tồn sổ sách sẽ luôn cao hơn tồn thực tế và hệ thống vẫn cho bán trong khi kho đã hết hàng dùng được. Đây là biện pháp bù cho việc chưa quản lý lô và hạn sử dụng ở phase 1. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S3
- **Priority**: Won't
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Vật liệu đã tồn tại và có tồn thực lớn hơn hoặc bằng 0.
  - Quản trị viên có quyền điều chỉnh tồn kho.
- **Trigger**: Quản trị viên chọn "Điều chỉnh tồn" trên một vật liệu.

## Flow
### Ghi nhận điều chỉnh giảm do hao hụt
1. Hệ thống hiển thị biểu mẫu điều chỉnh với tồn thực và tồn khả dụng hiện tại.
2. Quản trị viên chọn loại điều chỉnh: hao hụt, kiểm kê hoặc điều chỉnh khác.
3. Quản trị viên nhập số lượng thay đổi hoặc số tồn thực tế sau kiểm kê.
4. Quản trị viên nhập lý do bắt buộc.
5. Hệ thống hiển thị số tồn dự kiến sau điều chỉnh và tác động tới số lượng có thể bán.
6. Quản trị viên xác nhận.
7. Hệ thống sinh bút toán tương ứng và cập nhật tồn thực.
8. Hệ thống tính lại số lượng có thể bán của các combo hoa liên quan.

### Alternative Flow
- **ALT-01**: Quản trị viên nhập số tồn thực tế đếm được; hệ thống tự tính chênh lệch và sinh bút toán bằng đúng phần chênh lệch.

### Exception Flow
- **EXC-01**: Hệ thống chặn và cho biết số lượng đang bị giữ chỗ bởi các đơn hàng đang mở.
- **EXC-02**: Hệ thống chặn lưu và yêu cầu nhập lý do.
- **EXC-03**: Hệ thống vẫn cho lưu nhưng cảnh báo danh sách combo hoa bị ảnh hưởng.

## Acceptance Criteria
### AC-001
- **Given**: Vật liệu có tồn thực 20
- **When**: Quản trị viên ghi nhận hao hụt 5 với lý do hoa héo và xác nhận
- **Then**: Tồn thực trở thành 15
- **And**: Hệ thống sinh bút toán loại hao hụt kèm lý do và người thực hiện

### AC-002
- **Given**: Quản trị viên bỏ trống trường lý do
- **When**: Bấm xác nhận điều chỉnh
- **Then**: Hệ thống chặn lưu và yêu cầu nhập lý do

### AC-003
- **Given**: Vật liệu có tồn thực 10 và đang giữ chỗ 8
- **When**: Quản trị viên ghi nhận hao hụt 5
- **Then**: Hệ thống chặn thao tác và thông báo chỉ có thể giảm tối đa 2 do phần còn lại đang bị giữ chỗ

### AC-004
- **Given**: Tồn thực trên hệ thống là 20 và số đếm thực tế là 17
- **When**: Quản trị viên nhập số kiểm kê 17 và xác nhận
- **Then**: Hệ thống sinh bút toán kiểm kê với chênh lệch âm 3 và cập nhật tồn thực thành 17

### AC-005
- **Given**: Điều chỉnh làm combo hoa "Set hoa mùa đông" có số lượng có thể bán về 0
- **When**: Quản trị viên xác nhận điều chỉnh
- **Then**: Hệ thống hiển thị cảnh báo kèm tên combo hoa bị ảnh hưởng

## Non-Functional
- Mọi bút toán điều chỉnh phải ghi lại danh tính người thực hiện, không cho phép ẩn danh.

## Out of Scope
- Quy trình kiểm kê toàn kho theo đợt.
- Phê duyệt điều chỉnh bởi cấp quản lý.