# STORY-017: Copy Combo Formula

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn sao chép công thức của một combo hoa sẵn có sang combo hoa mới, để rút ngắn thời gian khai báo cho các combo hoa có cấu thành gần giống nhau.
- **Context**: Các combo hoa cùng dòng theo mùa thường chỉ khác nhau vài vật liệu. Nhập lại từ đầu vừa tốn thời gian vừa dễ sai sót. Đây là story tối ưu trải nghiệm, không chặn luồng nghiệp vụ chính. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S4
- **Priority**: Won't
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Tồn tại ít nhất một combo hoa có công thức ở trạng thái `active`.
  - Combo hoa đích chưa có công thức hoặc công thức đang ở trạng thái `NHAP`.
- **Trigger**: Quản trị viên bấm "Sao chép từ combo hoa khác" trên màn hình công thức của combo hoa đích.

## Flow
### Sao chép công thức thành công
1. Hệ thống hiển thị danh sách combo hoa có công thức hiệu lực kèm ô tìm kiếm.
2. Quản trị viên chọn combo hoa nguồn.
3. Hệ thống hiển thị bản xem trước toàn bộ dòng vật liệu sẽ được sao chép.
4. Quản trị viên xác nhận.
5. Hệ thống tạo công thức ở trạng thái `NHAP` cho combo hoa đích với nội dung sao chép.
6. Hệ thống mở màn hình công thức để quản trị viên tinh chỉnh trước khi kích hoạt.

### Alternative Flow
- **ALT-01**: Combo hoa đích đã có công thức `NHAP`; hệ thống cảnh báo nội dung hiện tại sẽ bị thay thế và yêu cầu xác nhận.

### Exception Flow
- **EXC-01**: Hệ thống vẫn sao chép nhưng đánh dấu các dòng cần thay thế và không cho kích hoạt tới khi xử lý xong.
- **EXC-02**: Hệ thống chặn sao chép và hướng dẫn tạo phiên bản mới thay vì sao chép.

## Acceptance Criteria
### AC-001
- **Given**: Combo hoa nguồn có công thức gồm 4 dòng vật liệu
- **When**: Quản trị viên sao chép sang combo hoa đích và xác nhận
- **Then**: Combo hoa đích có công thức `NHAP` gồm đúng 4 dòng với cùng vật liệu, định lượng và vai trò

### AC-002
- **Given**: Việc sao chép vừa hoàn tất
- **When**: Quản trị viên xem trạng thái công thức của combo hoa đích
- **Then**: Công thức ở trạng thái `NHAP` và chưa ảnh hưởng tới khả năng bán

### AC-003
- **Given**: Công thức nguồn chứa một vật liệu đã ngừng kinh doanh
- **When**: Sao chép hoàn tất
- **Then**: Dòng đó được đánh dấu cần thay thế và công thức không kích hoạt được cho tới khi xử lý

## Non-Functional
- Thao tác sao chép hoàn tất trong dưới 2 giây với công thức tối đa 30 dòng.

## Out of Scope
- Sao chép hàng loạt cho nhiều combo hoa cùng lúc.
- Tạo mẫu công thức dùng chung.