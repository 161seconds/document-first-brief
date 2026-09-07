# STORY-002: Xem, tìm kiếm và filter theo sản phẩm có nhãn là vật liệu

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem và lọc vật liệu cấu thành sản phẩm, để nắm được hệ thống đang quản lý những loại hoa và phụ liệu nào cùng tình trạng tồn kho của chúng
- **Context**: Trước đây hệ thống chỉ quản lý combo ở mức thành phẩm. Khi bổ sung quản lý vật liệu, quản trị viên cần một màn hình danh sách làm điểm vào cho toàn bộ nghiệp vụ vật liệu, định lượng và tồn kho. Đây là story nền của module. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Nháp
- **Cập nhật**: 20/08/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin.
  - Tài khoản có quyền xem dữ liệu vật liệu theo cấu hình RBAC.
- **Trigger**: Quản trị viên chọn mục "Vật liệu" trên menu điều hướng của trang admin.

## Flow
### Xem danh sách vật liệu
1. Hệ thống hiển thị danh sách vật liệu, mặc định sắp xếp theo tên tăng dần, phân trang 20 dòng mỗi trang (FE đề xuất).
2. Mỗi dòng hiển thị: mã vật liệu, tên, nhóm, đơn vị tính, tồn thực, đang giữ chỗ, tồn khả dụng, trạng thái (FE đề xuất).
3. Hệ thống hiển thị bộ lọc theo nhóm, trạng thái và ô tìm kiếm theo mã hoặc tên.
4. Quản trị viên chọn điều kiện lọc hoặc nhập tìm kiếm.
5. Hệ thống trả về danh sách khớp điều kiện và hiển thị tổng số kết quả.

### Alternative Flow
- **ALT-01 — Sắp xếp theo cột**: Quản trị viên bấm tiêu đề cột tồn khả dụng; hệ thống sắp xếp lại danh sách theo cột được chọn và giữ nguyên điều kiện lọc hiện tại.
- **ALT-02 — Xóa toàn bộ bộ lọc**: Quản trị viên bấm "Xóa bộ lọc"; hệ thống bỏ toàn bộ điều kiện lọc và từ khóa tìm kiếm, trả về danh sách mặc định.
- **ALT-03 — Chuyển trang khi đang lọc**: Quản trị viên chuyển sang trang khác; hệ thống giữ nguyên điều kiện lọc, từ khóa và thứ tự sắp xếp đang áp dụng.

### Exception Flow
- **EXC-01 — Không có kết quả**: Hệ thống hiển thị trạng thái rỗng.
- **EXC-02 — Không đủ quyền**: Hệ thống chặn truy cập và hiển thị thông báo không có quyền truy cập chức năng.
- **EXC-03 — Lỗi tải dữ liệu**: Hệ thống hiển thị thông báo lỗi kèm nút thử lại, không hiển thị dữ liệu cũ gây hiểu nhầm.
- **EXC-04 — Từ khóa không có nội dung tìm được**: Quản trị viên nhập từ khóa chỉ gồm khoảng trắng hoặc ký tự đặc biệt; hệ thống bỏ qua từ khóa và giữ nguyên danh sách theo điều kiện lọc hiện tại, không hiển thị trạng thái rỗng.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Quản trị viên đã đăng nhập và có quyền xem vật liệu.
  - **When**: Truy cập màn hình danh sách vật liệu.
  - **Then**: Hệ thống hiển thị danh sách vật liệu phân trang theo dòng (số dòng cụ thể FE đề xuất), sắp xếp theo tên tăng dần.
- **AC-002**: 
  - **Given**: Danh sách vật liệu đang hiển thị.
  - **When**: Quản trị viên nhập từ khóa vào ô tìm kiếm.
  - **Then**: Hệ thống trả về các vật liệu có mã hoặc tên chứa từ khóa, có debounce theo từng chữ.
  - **And**: Hiển thị tất cả kết quả tìm được.
- **AC-003**: 
  - **Given**: Quản trị viên đang ở màn hình danh sách.
  - **When**: Chọn đồng thời bộ lọc nhóm và bộ lọc trạng thái.
  - **Then**: Hệ thống trả về danh sách thỏa mãn đồng thời tất cả điều kiện lọc.
- **AC-004**: 
  - **Given**: Bộ lọc hiện tại không khớp vật liệu nào.
  - **When**: Hệ thống trả kết quả.
  - **Then**: Hiển thị thông báo không tìm thấy vật liệu kèm hành động xóa bộ lọc.
- **AC-005**: 
  - **Given**: Danh sách vật liệu đang hiển thị.
  - **When**: Quản trị viên xem một dòng bất kỳ.
  - **Then**: Dòng hiển thị đủ mã vật liệu, tên, nhóm, đơn vị tính, tồn thực, đang giữ chỗ, tồn khả dụng và trạng thái.
- **AC-006**: 
  - **Given**: Có 137 vật liệu khớp điều kiện lọc hiện tại và mỗi trang hiển thị 20 dòng (tùy chỉnh trong cms).
  - **When**: Hệ thống trả kết quả.
  - **Then**: Màn hình hiển thị tổng số kết quả là 137.
  - **And**: Con số này là tổng trên toàn bộ kết quả, không phải số dòng của trang đang xem.
- **AC-008**: 
  - **Given**: Tài khoản đã đăng nhập nhưng không có quyền xem dữ liệu vật liệu.
  - **When**: Truy cập màn hình danh sách vật liệu.
  - **Then**: Hệ thống chặn truy cập và thông báo không có quyền truy cập chức năng.
  - **And**: Không hiển thị bất kỳ dòng vật liệu nào, kể cả dòng rỗng hay số tổng.
- **AC-009**: 
  - **Given**: Hệ thống không tải được danh sách vật liệu.
  - **When**: Quản trị viên mở màn hình danh sách.
  - **Then**: Hệ thống hiển thị thông báo lỗi kèm hành động thử lại.
  - **And**: Không hiển thị dữ liệu của lần tải trước.
  - **And**: Thông báo lỗi phân biệt được với thông báo không tìm thấy vật liệu của AC-004.
- **AC-010**: 
  - **Given**: Quản trị viên đang áp dụng bộ lọc nhóm, bộ lọc trạng thái và một từ khóa tìm kiếm.
  - **When**: Bấm "Xóa bộ lọc".
  - **Then**: Cả ba điều kiện được bỏ.
  - **And**: Danh sách trở về mặc định sắp xếp theo tên tăng dần.
- **AC-011**: 
  - **Given**: Kết quả lọc có nhiều hơn một trang và đang sắp xếp theo tồn khả dụng.
  - **When**: Quản trị viên chuyển sang trang 2.
  - **Then**: Điều kiện lọc và từ khóa được giữ nguyên.
  - **And**: Thứ tự sắp xếp theo tồn khả dụng được giữ nguyên.
- **AC-013**: 
  - **Given**: Danh sách đang lọc theo nhóm "Hoa tươi" với 20 kết quả.
  - **When**: Quản trị viên nhập từ khóa chỉ gồm khoảng trắng.
  - **Then**: Danh sách vẫn hiển thị 20 kết quả của nhóm "Hoa tươi".
  - **And**: Hệ thống không hiển thị thông báo không tìm thấy vật liệu.

## References
- **TDDs (1)**: 
  - [TDD-010](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-010.md) (Xem, tìm kiếm và filter theo sản phẩm có nhãn là vật liệu)
- **Unit Tests (5)**:
  - `UT-002-01`: Xem và search list material
  - `UT-002-02`: Lọc Product - sản phẩm thường
  - `UT-002-02-01`: Lọc Material - vật liệu
  - `UT-002-03`: Không truyền typeProduct
  - `UT-002-04`: Không có dữ liệu thỏa mãn
- **System Tests (2)**

## Non-Functional
- Thời gian phản hồi danh sách dưới 2 giây với tối đa 2.000 vật liệu (Đề xuất).
- Tìm kiếm hỗ trợ Tiếng Việt và debounce trên từng kí tự.

## Out of Scope
- Xuất danh sách vật liệu ra file.
- Xem chi tiết lịch sử biến động tồn của từng vật liệu.