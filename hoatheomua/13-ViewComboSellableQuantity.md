# STORY-013: Xem khả năng bán của combo hoa

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem số lượng có thể bán của từng combo hoa cùng vật liệu đang giới hạn, để biết cần nhập loại hoa nào nếu muốn tăng sản lượng bán ra.
- **Context**: Đây là story hiện thực hóa trực tiếp yêu cầu cốt lõi của khách hàng: dựa vào hoa thành phần để biết combo hoa có bán được hay không. Story chỉ ra không chỉ con số mà cả nguyên nhân, vì đó mới là thông tin ra quyết định được. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S3
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Combo hoa có công thức ở trạng thái `active`.
  - Vật liệu trong công thức có dữ liệu tồn kho.
- **Trigger**: Quản trị viên mở màn hình danh sách combo hoa hoặc chi tiết một combo hoa.

## Flow
### Xem khả năng bán theo combo hoa
1. Hệ thống lấy công thức hiệu lực của từng combo hoa.
2. Với mỗi dòng vai trò CORE, hệ thống tính số combo hoa mà dòng đó cho phép bằng phép chia lấy phần nguyên giữa tồn khả dụng và định lượng.
3. Hệ thống lấy giá trị nhỏ nhất trong các kết quả trên làm số lượng có thể bán.
4. Hệ thống hiển thị số lượng có thể bán trên danh sách combo hoa.
5. Hệ thống cho phép lọc combo hoa theo trạng thái còn hàng, sắp hết và hết hàng.

### Alternative Flow
- **ALT-02**: Quản trị viên sắp xếp danh sách theo số lượng có thể bán tăng dần để ưu tiên xử lý combo hoa sắp hết.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Công thức có hoa hồng đỏ định lượng 2 với tồn khả dụng 7 và hoa hồng trắng định lượng 3 với tồn khả dụng 30.
  - **When**: Hệ thống tính số lượng có thể bán.
  - **Then**: Kết quả là 3.
- **AC-002**: 
  - **Given**: Công thức có thêm dòng hoa baby vai trò SUPPORT với tồn khả dụng bằng 0.
  - **When**: Hệ thống tính số lượng có thể bán.
  - **Then**: Kết quả vẫn là 3, không bị hoa baby kéo về 0.
- **AC-003**: 
  - **Given**: Hoa hồng đỏ có tồn khả dụng 3 và được dùng bởi cả combo hoa mùa hè và combo hoa mùa đông với định lượng 3.
  - **When**: Hệ thống tính khả năng bán.
  - **Then**: Cả hai combo hoa đều hiển thị số lượng có thể bán là 1.
  - **And**: Sau khi bán 1 combo hoa mùa hè, cả hai combo hoa cùng chuyển về 0.
- **AC-004**: 
  - **Given**: Công thức chỉ gồm các dòng vai trò SUPPORT.
  - **When**: Hệ thống tính số lượng có thể bán.
  - **Then**: Kết quả là 0 kèm nhãn thiếu vật liệu chính.

## References
- **TDDs**: TDD-004

## Non-Functional
- Danh sách 200 combo hoa phải hiển thị đầy đủ số lượng có thể bán trong dưới 2 giây.
- Kết quả tính toán phải nhất quán giữa màn hình danh sách và màn hình chi tiết.

## Out of Scope
- Hiển thị phía khách hàng.
- Dự báo khả năng bán theo kế hoạch nhập hàng.