# STORY-014: Hiển thị trạng thái còn hàng của combo hoa

## Metadata
- **Story**: Là một Khách hàng, tôi muốn biết combo hoa còn hàng hay đã hết trước khi bỏ công thiết kế và đặt mua, để không mất thời gian với combo hoa không thể giao.
- **Context**: Cơ chế tính khả năng bán phải hiển thị ra phía khách hàng thì mới giảm được đơn hỏng. Cần cân bằng giữa việc minh bạch tình trạng hàng và việc không tiết lộ số liệu tồn kho nội bộ cho đối thủ. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S4
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Combo hoa có công thức hiệu lực và số lượng có thể bán đã được tính.
  - Không yêu cầu khách hàng đăng nhập.
- **Trigger**: Khách hàng xem danh sách combo hoa hoặc chi tiết một combo hoa trên website.

## Flow
### Hiển thị trạng thái còn hàng
1. Hệ thống lấy số lượng có thể bán của combo hoa.
2. Hệ thống quy đổi thành nhãn trạng thái: còn hàng (kèm theo số lượng có thể bán) hoặc hết hàng dựa trên công thức tính số lượng có thể bán của combo hoa đó.
3. Hệ thống hiển thị nhãn trạng thái trên thẻ combo hoa và trên trang chi tiết.
4. Với combo hoa hết hàng, hệ thống vô hiệu hóa nút đặt hàng nhưng vẫn cho phép xem.

### Alternative Flow
- **ALT-02**: Hệ thống đưa combo hoa hết hàng xuống cuối danh sách mặc định.

### Exception Flow
- **EXC-01**: Hệ thống cập nhật trạng thái khi khách thao tác tiếp và thông báo combo hoa vừa hết hàng.
- **EXC-02**: Hệ thống hiển thị trạng thái an toàn là cho phép đặt và kiểm tra lại ở bước tạo đơn.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Combo hoa có số lượng có thể bán bằng 0.
  - **When**: Khách hàng xem combo hoa.
  - **Then**: Hệ thống hiển thị nhãn hết hàng và vô hiệu hóa nút đặt hàng.
- **AC-002**: 
  - **Given**: Khách hàng chưa đăng nhập.
  - **When**: Duyệt danh sách combo hoa.
  - **Then**: Nhãn trạng thái vẫn hiển thị đầy đủ.

## References
- **Dependencies**: STORY-013

## Non-Functional
- Nhãn trạng thái phản ánh dữ liệu không cũ hơn 60 giây.
- Không để lộ số lượng tồn kho cụ thể qua giao diện hoặc qua phản hồi của máy chủ.

## Out of Scope
- Chức năng thông báo khi có hàng trở lại.
- Chặn thao tác đặt hàng (sẽ có ở bước thanh toán / giỏ hàng).