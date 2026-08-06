# STORY-005: Discontinue or Delete Materials

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn ngừng kinh doanh hoặc xóa một vật liệu không còn sử dụng, để danh mục vật liệu gọn gàng mà không làm hỏng dữ liệu lịch sử và các công thức đang chạy.
- **Context**: Vật liệu là dữ liệu được nhiều đối tượng tham chiếu: công thức cấu thành nên combo. Xóa cứng sẽ làm hỏng công thức và phá vỡ cam kết danh sách nguyên liệu trên các đơn cũ nên xóa mềm. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...). Combo được hiểu là một sản phẩm hoa lớn chứa nhiều vật liệu thành phần.
- **Sprint**: S2
- **Priority**: Must
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**:
  - Vật liệu tồn tại trong hệ thống.
  - Quản trị viên có quyền xóa vật liệu.
- **Trigger**: Quản trị viên chọn hành động "Ngừng kinh doanh" hoặc "Xóa" trên một vật liệu.

## Flow
### Ngừng kinh doanh/xóa vật liệu đang được sử dụng
1. Hệ thống rà soát các tham chiếu tới vật liệu: dòng công thức đang hiệu lực, giữ chỗ đang mở.
2. Hệ thống hiển thị hộp thoại xác nhận, liệt kê các Combo đang dùng vật liệu này.
3. Quản trị viên xác nhận ngừng kinh doanh/xóa.
4. Hệ thống chuyển trạng thái vật liệu sang `ngừng kinh doanh` / `xóa`.
5. Hệ thống loại vật liệu khỏi danh sách chọn khi thêm dòng công thức mới.
6. Hệ thống gắn cảnh báo lên các công thức đang tham chiếu vật liệu này.

### Alternative Flow
- **ALT-01**: Quản trị viên chọn "Mở bán lại" trên vật liệu đang ngừng kinh doanh; hệ thống chuyển trạng thái về `đang bán`.

### Exception Flow
- **EXC-01**: Hệ thống cảnh báo rằng combo sẽ không còn bán được và yêu cầu xác nhận lần hai.
- **EXC-02**: Hệ thống vẫn cho ngừng kinh doanh nhưng giữ nguyên các giữ chỗ hiện có để không phá vỡ đơn đang xử lý.

## Acceptance Criteria
### AC-001
- **Given**: Vật liệu đang được ít nhất một công thức tham chiếu
- **When**: Quản trị viên yêu cầu xóa vật liệu
- **Then**: Hệ thống chuyển vật liệu sang `ngừng kinh doanh` thay vì xóa dữ liệu
- **And**: Toàn bộ kho và snapshot đơn hàng liên quan được giữ nguyên

### AC-002
- **Given**: Vật liệu đang được 3 combo sử dụng
- **When**: Popup xác nhận xóa hiển thị
- **Then**: Hệ thống liệt kê tên của cả 3 combo bị ảnh hưởng

### AC-003
- **Given**: Vật liệu ở trạng thái `ngừng kinh doanh`
- **When**: Quản trị viên thêm dòng vật liệu vào một công thức bất kỳ
- **Then**: Vật liệu này không xuất hiện trong danh sách chọn

### AC-004
- **Given**: Vật liệu là dòng CORE duy nhất của một sản phẩm đang mở bán
- **When**: Quản trị viên yêu cầu ngừng kinh doanh vật liệu
- **Then**: Hệ thống cảnh báo sản phẩm sẽ có số lượng có thể bán bằng 0 và yêu cầu xác nhận lần hai

## References
- **Dependencies**: STORY-004

## Non-Functional
- Việc rà soát tham chiếu phải hoàn tất trong dưới 3 giây.

## Out of Scope
- Thay thế hàng loạt vật liệu này bằng vật liệu khác trong các công thức.