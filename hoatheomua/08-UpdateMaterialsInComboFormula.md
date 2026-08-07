# STORY-008: Sửa định lượng/vai trò vật liệu trong công thức

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn sửa định lượng hoặc vai trò của một dòng vật liệu trong công thức, để điều chỉnh cấu thành combo hoa khi thiết kế bó hoa thay đổi.
- **Context**: Thay đổi định lượng làm thay đổi trực tiếp số lượng có thể bán. Người nhập liệu cần thấy trước tác động này trước khi lưu. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Công thức đã có ít nhất một dòng vật liệu.
  - Quản trị viên có quyền sửa công thức.
- **Trigger**: Quản trị viên bấm biểu tượng chỉnh sửa trên một dòng vật liệu.

## Flow
### Sửa dòng vật liệu thành công
1. Hệ thống chuyển dòng sang chế độ chỉnh sửa tại chỗ với hai trường: định lượng và vai trò.
2. Quản trị viên thay đổi giá trị.
3. Hệ thống hiển thị trước số lượng có thể bán mới tương ứng với giá trị đang nhập.
4. Quản trị viên bấm "Lưu".
5. Hệ thống kiểm tra hợp lệ và lưu thay đổi.

### Alternative Flow
- **ALT-01**: Quản trị viên bấm "Hủy" khi đang sửa; hệ thống khôi phục giá trị ban đầu của dòng.
- **ALT-02**: Hệ thống hiển thị cảnh báo rằng số lượng có thể bán có thể giảm và yêu cầu xác nhận.

### Exception Flow
- **EXC-01**: Hệ thống chặn và thông báo công thức phải còn ít nhất một dòng CORE.
- **EXC-02**: Hệ thống chặn lưu và hiển thị lỗi tại trường định lượng (nếu nhập không hợp lệ).

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Dòng "Hoa hồng đỏ" đang có định lượng 2 và tồn khả dụng là 12.
  - **When**: Quản trị viên đổi định lượng thành 3 và lưu.
  - **Then**: Hệ thống lưu giá trị mới.
  - **And**: Số lượng có thể bán được tính lại từ 6 xuống 4.
- **AC-002**: 
  - **Given**: Quản trị viên đang chỉnh sửa định lượng của một dòng CORE.
  - **When**: Nhập giá trị mới nhưng chưa bấm Lưu.
  - **Then**: Hệ thống hiển thị số lượng có thể bán dự kiến tương ứng với giá trị đang nhập.

## References
- **Dependencies**: STORY-007

## Non-Functional
- Việc xem trước tác động phải tính phía máy chủ để đảm bảo dùng đúng dữ liệu tồn khả dụng hiện hành.

## Out of Scope
- Sửa hàng loạt nhiều dòng cùng lúc.
- Đổi vật liệu của một dòng sang vật liệu khác (thực hiện bằng cách xóa dòng và thêm dòng mới).