# STORY-007: Thêm vật liệu vào công thức combo

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn thêm một vật liệu kèm số lượng và vai trò vào công thức của combo hoa, để hệ thống biết combo hoa cần tiêu hao những gì khi bán ra.
- **Context**: Đây là story cốt lõi hiện thực hóa yêu cầu "CRUD số lượng của một bó hoa". Vai trò CORE hay SUPPORT được quyết định tại đây và ảnh hưởng trực tiếp tới công thức tính khả năng bán, nên cần hiển thị rõ hệ quả cho người nhập liệu. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Combo hoa đã tồn tại và quản trị viên có quyền sửa công thức.
  - Combo hoa đã tồn tại và có các combo theo size S / M / L.
- **Trigger**: Quản trị viên bấm "Thêm vật liệu" trên màn hình công thức của combo hoa.

## Flow
### Thêm vật liệu cho đầy đủ size
1. Hệ thống hiển thị các con của combo được tạo.
2. Quản trị viên chọn một vật liệu nếu muốn thêm.
3. Quản trị viên nhập định lượng cho từng hoa thành phần của size S và vai trò.
4. Hệ thống kiểm tra vật liệu chưa tồn tại trong công thức và định lượng hợp lệ.
5. Giao diện hiển thị 3 size S/ M/ L cùng với nhập hệ số cho size M L.
6. Quản trị viên nhập số hệ số cho size M và L.
7. Quản trị viên bấm nút lưu. Hệ thống cập nhật 3 sản phẩm size S/ M/ L và lưu vào hệ thống và tính lại khả năng bán cho các size.
8. Hệ thống tính định lượng cho các size dựa trên hệ số và định lượng base (size S) theo công thức: công thức combo cha × hệ số con, làm tròn xuống.

### Alternative Flow
- **ALT-01**: 
  - Quản trị viên chọn một vật liệu để thêm nếu muốn.
  - Quản trị viên nhập định lượng và vai trò cho từng thành phần.
  - Quản trị viên bấm lưu. Hệ thống cập nhật Combo và tính lại khả năng bán cho Combo.
- **ALT-02**: Quản trị viên đổi vai trò từ SUPPORT sang CORE; hệ thống hiển thị chú thích rằng dòng này sẽ tham gia giới hạn số lượng có thể bán.

### Exception Flow
- **EXC-01**: Hệ thống chặn và gợi ý chỉnh sửa dòng hiện có (Vật liệu đã tồn tại).
- **EXC-02**: Hệ thống chặn và yêu cầu nhập số nguyên dương (Số lượng = 0 hoặc âm).
- **EXC-03**: Hệ thống chặn thêm và làm mới danh sách chọn (Vật liệu ngừng kinh doanh).

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Công thức của combo hoa chưa chứa vật liệu "Hoa hồng đỏ".
  - **When**: Quản trị viên chọn vật liệu này, nhập định lượng 2, vai trò CORE và bấm Thêm.
  - **Then**: Hệ thống thêm dòng vào công thức.
  - **And**: Số lượng có thể bán của combo hoa được tính ngay trên màn hình.
- **AC-003**: 
  - **Given**: Công thức đã có dòng vật liệu "Hoa hồng đỏ".
  - **When**: Quản trị viên thêm lại vật liệu "Hoa hồng đỏ".
  - **Then**: Hệ thống từ chối và hiển thị thông báo vật liệu đã tồn tại trong công thức.
- **AC-004**: 
  - **Given**: Quản trị viên nhập định lượng bằng 0 hoặc số âm.
  - **When**: Bấm Thêm.
  - **Then**: Hệ thống chặn và yêu cầu nhập số nguyên lớn hơn 0.
- **AC-005**: 
  - **Given**: Có vật liệu ở trạng thái `ngừng kinh doanh`.
  - **When**: Quản trị viên mở hộp thoại chọn vật liệu.
  - **Then**: Vật liệu đó không xuất hiện trong danh sách chọn.
- **AC-006**: 
  - **Given**: Combo hoa tồn tại và các size của combo tồn tại.
  - **When**: Quản trị viên nhập định lượng cho từng hoa thành phần size S và hệ số cho từng size.
  - **Then**: Hệ thống tính số lượng được bán của combo hoa đó, hiện bảng preview cho các size sau khi nhân hệ số.

## References
- **Dependencies**: STORY-006

## Non-Functional
- Hộp thoại chọn vật liệu phải trả kết quả tìm kiếm trong dưới 500ms.
- Số lượng có thể bán phải được tính lại và hiển thị trong dưới 1 giây sau khi thêm dòng.

## Out of Scope
- Kích hoạt công thức để áp dụng cho việc bán hàng.
- Gợi ý vật liệu thay thế.