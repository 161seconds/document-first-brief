# STORY-006: Xem công thức định lượng của combo hoa

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem công thức định lượng của một combo hoa, để biết combo hoa được cấu thành từ những vật liệu nào với số lượng bao nhiêu và vật liệu nào đang giới hạn khả năng bán
- **Context**: Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần
- **Sprint**: S2
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
  - Combo hoa đã tồn tại trong hệ thống.
  - Quản trị viên có quyền xem công thức.
- **Trigger**: Quản trị viên mở tab "Định lượng" trong màn hình chi tiết combo hoa.

## Flow
### Xem công thức hiệu lực
1. Hệ thống load công thức của combo hoa.
2. Hệ thống hiển thị danh sách loại vật liệu, tách hai nhóm: vai trò CORE trước, vai trò SUPPORT sau.
3. Mỗi dòng hiển thị: tên vật liệu, vai trò, định lượng, đơn vị tính, tồn khả dụng của vật liệu, số combo hoa.
4. Hệ thống hiển thị số lượng có thể bán của combo hoa ở đầu màn hình.

### Alternative Flow
- **ALT-01 — Combo hoa chưa có công thức**:
  - Hệ thống hiển thị thông báo "Combo hoa chưa có công thức, vui lòng thêm vật liệu".
  - Hệ thống hiển thị trạng thái rỗng kèm nút "Thêm vật liệu".
- **ALT-02 — Xem công thức của một combo con**:
  - Hệ thống hiển thị các thành phần của combo con với định lượng đã tính theo hệ số, ở chế độ chỉ đọc.
  - Hệ thống hiển thị hệ số đang áp dụng và định lượng chuẩn tương ứng ở combo cha, kèm đường dẫn tới công thức của combo cha.
- **ALT-03 — Công thức đã đạt hoặc vượt 15 thành phần**:
  - Hệ thống hiển thị cảnh báo công thức đã có từ 15 thành phần trở lên, không chặn thao tác nào.

### Exception Flow
- **EXC-01 — Công thức tham chiếu vật liệu đã ngừng kinh doanh**: Hệ thống hiển thị cảnh báo trên dòng tương ứng.
- **EXC-02 — Có công thức nhưng không có dòng CORE nào**: Hệ thống hiển thị cảnh báo combo hoa không thể bán và số lượng có thể bán là 0.
- **EXC-03 — Lỗi tải công thức**: Hệ thống hiển thị thông báo lỗi kèm nút thử lại.
- **EXC-05 — Công thức tham chiếu vật liệu không còn trong danh mục**: Hệ thống vẫn giữ dòng, hiển thị tên vật liệu đã lưu và cảnh báo vật liệu không còn tồn tại, coi tồn khả dụng của dòng là 0 khi tính số lượng có thể bán.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Ví dụ combo hoa "Set hoa mùa xuân" có công thức gồm 2 hoa hồng đỏ vai trò CORE, 3 hoa hồng trắng vai trò CORE và 1 bó baby vai trò SUPPORT.
  - **When**: Quản trị viên mở tab Định lượng.
  - **Then**: Hệ thống hiển thị đủ 3 dòng, nhóm CORE hiển thị trước nhóm SUPPORT.
  - **And**: Mỗi dòng hiển thị đúng định lượng và đơn vị tính.
- **AC-002**: 
  - **Given**: Vật liệu vai trò SUPPORT có tồn khả dụng bằng 0.
  - **When**: Hệ thống tính số lượng có thể bán.
  - **Then**: Số lượng có thể bán vẫn được tính chỉ dựa trên các dòng CORE.
  - **And**: Dòng SUPPORT hiển thị cảnh báo hết hàng nhưng không làm số lượng có thể bán về 0.
- **AC-003**: 
  - **Given**: Combo hoa chưa có dòng vật liệu nào.
  - **When**: Quản trị viên mở tab Định lượng.
  - **Then**: Hệ thống hiển thị trạng thái rỗng và số lượng có thể bán bằng 0.
- **AC-004**: 
  - **Given**: Công thức có một dòng tham chiếu vật liệu ở trạng thái `ngừng kinh doanh`.
  - **When**: Màn hình công thức được hiển thị.
  - **Then**: Dòng đó hiển thị cảnh báo yêu cầu thay thế vật liệu.
- **AC-005**: 
  - **Given**: Công thức có dòng CORE "Hoa hồng đỏ" định lượng 2, đơn vị tính `cành`, tồn khả dụng của vật liệu là 12.
  - **When**: Quản trị viên mở tab Định lượng.
  - **Then**: Dòng hiển thị đủ tên vật liệu, vai trò, định lượng, đơn vị tính, tồn khả dụng của vật liệu và số combo mà dòng này cho phép làm được.
  - **And**: Số combo mà dòng này cho phép là 6.
- **AC-007**: 
  - **Given**: Hệ thống không tải được công thức của combo.
  - **When**: Quản trị viên mở tab Định lượng.
  - **Then**: Hệ thống hiển thị thông báo lỗi kèm hành động thử lại.
  - **And**: Không hiển thị số lượng có thể bán bằng 0 như thể combo hết hàng.
  - **And**: Không hiển thị trạng thái rỗng như thể combo chưa có công thức.
- **AC-008**: 
  - **Given**: Combo con "Combo hoa mùa xuân M" có hệ số 1.5; combo cha có thành phần "Hoa hồng đỏ" định lượng chuẩn 4, đơn vị `cành`.
  - **When**: Quản trị viên mở tab Định lượng của combo con M.
  - **Then**: Dòng "Hoa hồng đỏ" hiển thị định lượng 6 ở chế độ chỉ đọc.
  - **And**: Màn hình cho biết hệ số đang áp dụng là 1.5 và định lượng chuẩn ở combo cha là 4.
- **AC-009**: 
  - **Given**: Công thức có 16 thành phần.
  - **When**: Quản trị viên mở tab Định lượng.
  - **Then**: Hệ thống hiển thị đủ 16 dòng kèm cảnh báo công thức đã vượt 15 thành phần.
  - **And**: Cảnh báo không chặn việc xem hay sửa công thức.

## References
- **TDDs (1)**: 
  - [TDD-011](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-011.md) (Xem công thức định lượng của combo hoa)
- **Rules (4)**:
  - [`BR-013`](file:///d:/VNZ/document-first-brief/hoatheomua/BusinessRules/MaterialManagement/BR-013.md): Giữ dòng tham chiếu vật liệu ngừng kinh doanh
  - [`BR-014`](file:///d:/VNZ/document-first-brief/hoatheomua/BusinessRules/MaterialManagement/BR-014.md): Thứ tự hiển thị theo vai trò
  - [`BR-018`](file:///d:/VNZ/document-first-brief/hoatheomua/BusinessRules/MaterialManagement/BR-018.md): Hoa phụ không giới hạn khả năng bán
  - [`BR-019`](file:///d:/VNZ/document-first-brief/hoatheomua/BusinessRules/MaterialManagement/BR-019.md): Combo không đủ điều kiện thì bằng 0
- **Dependencies**: 
  - [STORY-002](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/02-ViewSearchMaterialsList.md)
- **Unit Tests (6)**:
  - `UT-006-01`: kh tìm thấy combo (`GetComboSpecificationAsync`)
  - `UT-006-02`: Combo rỗng, trả về danh sách rỗng (`GetComboSpecificationAsync`)
  - `UT-006-03`: Tính toán số lượng MaxCombo và Sắp xếp đúng Role (`GetComboSpecificationAsync`)
  - `UT-006-04`: Combo rỗng hoặc vật liệu bị vô hiệu hóa (`GetComboSpecificationAsync`)
  - `UT-006-05`: Lọc chính xác theo ProductLabel (`GetMaterialsAsync`)
  - `UT-006-06`: Phân trang hoạt động (`GetMaterialsAsync`)
- **System Tests (4)**

## Non-Functional
- Số lượng có thể bán hiển thị phải phản ánh dữ liệu không cũ hơn 60 giây, tính từ thời điểm tồn khả dụng của vật liệu thay đổi tới thời điểm màn hình công thức hiển thị con số mới.

## Out of Scope
- Chỉnh sửa công thức.
- So sánh khác biệt giữa hai phiên bản công thức.