# STORY-007: Add Materials to Combo Formula

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn thêm một vật liệu kèm số lượng và vai trò vào công thức của combo hoa, để hệ thống biết combo hoa cần tiêu hao những gì khi bán ra.
- **Context**: Đây là story cốt lõi hiện thực hóa yêu cầu "CRUD số lượng của một bó hoa". Vai trò CORE hay SUPPORT được quyết định tại đây và ảnh hưởng trực tiếp tới công thức tính khả năng bán, nên cần hiển thị rõ hệ quả cho người nhập liệu. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Combo hoa đã tồn tại và quản trị viên có quyền sửa công thức.
  - Có ít nhất một vật liệu ở trạng thái `đang bán` trong danh mục.
- **Trigger**: Quản trị viên bấm "Thêm vật liệu" trên màn hình công thức của combo hoa.

## Flow
### Thêm dòng vật liệu thành công
1. Hệ thống popup chọn vật liệu với ô tìm kiếm.
2. Quản trị viên chọn một vật liệu.
3. Hệ thống điền sẵn vai trò theo vai trò mặc định của vật liệu và điền sẵn đơn vị tính. (Chưa chốt)
4. Quản trị viên nhập định lượng cho từng hoa thành phần và điều chỉnh vai trò nếu cần.
5. Quản trị viên bấm "Thêm".
6. Hệ thống kiểm tra vật liệu chưa tồn tại trong công thức và định lượng hợp lệ.
7. Hệ thống thêm dòng vào công thức, tính lại số lượng có thể bán và hiển thị kết quả mới.

### Alternative Flow
- **ALT-01**: Quản trị viên chọn "Thêm và tiếp tục"; hệ thống lưu dòng hiện tại.
- **ALT-02**: Quản trị viên đổi vai trò từ SUPPORT sang CORE; hệ thống hiển thị chú thích rằng dòng này sẽ tham gia giới hạn số lượng có thể bán.

### Exception Flow
- **EXC-01**: Hệ thống chặn và gợi ý chỉnh sửa dòng hiện có.
- **EXC-02**: Hệ thống chặn và yêu cầu nhập số nguyên dương.
- **EXC-03**: Hệ thống chặn thêm và làm mới danh sách chọn.

## Acceptance Criteria
### AC-001
- **Given**: Công thức của combo hoa chưa chứa vật liệu "Hoa hồng đỏ"
- **When**: Quản trị viên chọn vật liệu này, nhập định lượng 2, vai trò CORE và bấm Thêm
- **Then**: Hệ thống thêm dòng vào công thức
- **And**: Số lượng có thể bán của combo hoa được tính lại ngay trên màn hình

### AC-002
- **Given**: Vật liệu "Hoa baby" có vai trò mặc định là SUPPORT
- **When**: Quản trị viên chọn vật liệu này trong hộp thoại
- **Then**: Trường vai trò được điền sẵn giá trị SUPPORT
- **And**: Quản trị viên vẫn có thể đổi sang CORE

### AC-003
- **Given**: Công thức đã có dòng vật liệu "Hoa hồng đỏ"
- **When**: Quản trị viên thêm lại vật liệu "Hoa hồng đỏ"
- **Then**: Hệ thống từ chối và hiển thị thông báo vật liệu đã tồn tại trong công thức

### AC-004
- **Given**: Quản trị viên nhập định lượng bằng 0 hoặc số âm
- **When**: Bấm Thêm
- **Then**: Hệ thống chặn và yêu cầu nhập số nguyên lớn hơn 0

### AC-005
- **Given**: Có vật liệu ở trạng thái `ngừng kinh doanh`
- **When**: Quản trị viên mở hộp thoại chọn vật liệu
- **Then**: Vật liệu đó không xuất hiện trong danh sách chọn

## References
- **Dependencies**: STORY-006

## Non-Functional
- Hộp thoại chọn vật liệu phải trả kết quả tìm kiếm trong dưới 500ms.
- Số lượng có thể bán phải được tính lại và hiển thị trong dưới 1 giây sau khi thêm dòng.

## Out of Scope
- Kích hoạt công thức để áp dụng cho việc bán hàng.
- Gợi ý vật liệu thay thế.