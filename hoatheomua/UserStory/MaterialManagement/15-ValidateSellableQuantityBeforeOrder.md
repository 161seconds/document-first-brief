# STORY-015: Kiểm tra số lượng có thể bán trước khi tạo đơn

## Metadata
- **Story**: Là một Khách hàng, tôi muốn được thông báo ngay khi số lượng tôi muốn đặt vượt quá lượng hàng còn lại, để không đặt phải đơn mà cửa hàng không thể giao.
- **Context**: Vì vật liệu dùng chung giữa nhiều sản phẩm, tình trạng còn hàng có thể thay đổi trong lúc khách đang thao tác. Cần kiểm tra ở hai điểm để chống bán vượt khi nhiều khách mua đồng thời. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một bó hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S4
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Khách hàng đã chọn sản phẩm hoặc hoàn tất thiết kế.
  - Sản phẩm có công thức hiệu lực.
- **Trigger**: Khách hàng nhập số lượng và bấm thêm vào giỏ hoặc bấm đặt hàng.

## Flow
### Kiểm tra khả năng bán trước khi tạo đơn
1. Hệ thống nhận số lượng khách yêu cầu.
2. Hệ thống tính lại số lượng có thể bán tại thời điểm hiện tại.
3. Hệ thống so sánh số lượng yêu cầu với số lượng có thể bán.
4. Nếu hợp lệ, hệ thống cho phép thao tác tiếp tục.
5. Tại bước tạo đơn, hệ thống kiểm tra lại lần nữa và giữ chỗ vật liệu.

### Exception Flow
- **EXC-01**: Hệ thống chặn tạo đơn và thông báo combo vừa hết hàng.
- **EXC-02**: Hệ thống chỉ chấp nhận đơn giữ chỗ thành công trước, đơn còn lại nhận thông báo hết hàng.
- **EXC-03**: Trong điều kiện cả hai đều là core, hệ thống kiểm tra tổng nhu cầu vật liệu của toàn giỏ, không kiểm tra riêng lẻ từng dòng.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Combo có số lượng có thể bán là 2.
  - **When**: Khách hàng yêu cầu đặt 5 combo hoa.
  - **Then**: Hệ thống chặn thao tác và thông báo chỉ còn 2 combo.
- **AC-002**: 
  - **Given**: Giỏ hàng có combo mùa hè và sản phẩm mùa đông cùng dùng hoa hồng đỏ, tồn khả dụng hoa hồng đỏ chỉ đủ cho một trong hai.
  - **When**: Khách hàng bấm đặt hàng.
  - **Then**: Hệ thống chặn và chỉ rõ vật liệu chung không đủ cho toàn bộ giỏ hàng.
- **AC-003**: 
  - **Given**: Combo chỉ còn số lượng có thể bán là 1.
  - **When**: Hai khách hàng cùng bấm đặt hàng tại cùng thời điểm.
  - **Then**: Chỉ một đơn được tạo thành công.
  - **And**: Khách còn lại nhận thông báo combo hoa vừa hết hàng.
- **AC-004**: 
  - **Given**: Hoa baby vai trò SUPPORT trong công thức đã hết tồn.
  - **When**: Khách hàng đặt combo.
  - **Then**: Hệ thống vẫn cho tạo đơn thành công.

## References
- **Dependencies**: STORY-013, STORY-014

## Non-Functional
- Việc kiểm tra và giữ chỗ phải thực hiện trong một giao dịch nguyên tử (atomic transaction), không cho phép hai đơn cùng chiếm một đơn vị vật liệu.
- Thông báo lỗi phải nêu rõ combo và số lượng còn lại, không hiển thị số tồn vật liệu.

## Out of Scope
- Cơ chế giữ chỗ và trừ tồn theo trạng thái đơn.
- Danh sách chờ khi hết hàng.