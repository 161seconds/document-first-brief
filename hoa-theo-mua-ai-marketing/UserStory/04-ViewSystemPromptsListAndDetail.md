# STORY-004: Xem danh sách và chi tiết System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem danh sách và thông tin chi tiết của System Prompt để có thể kiểm tra nội dung, ngày cập nhật của từng Prompt trước khi thực hiện các thao tác quản lý khác
- **Context**: Hệ thống sử dụng các System Prompt để định hướng cách các chức năng AI xử lý từng nghiệp vụ như tạo nội dung bài viết, paraphrase nội dung hoặc các tác vụ liên quan đến Marketing. Chức năng này cung cấp màn hình danh sách System Prompt và cho phép Quản trị viên chọn một System Prompt để xem thông tin chi tiết. Story này chỉ phục vụ mục đích xem dữ liệu, không thực hiện tìm kiếm, lọc, chỉnh sửa, khôi phục hoặc xóa System Prompt.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Nháp
- **Cập nhật**: 04/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Chưa có AC cho EXC-01, 02, ALT-02. System Prompt có status không? Mainflow 3, 4 sao như nay thế?"* — Nguyễn Đức Bình · 08:58 03/09/2026

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin với quyền hạn hợp lệ.
- **Trigger**: 
  - Quản trị viên chọn mục "System Prompt" trên menu điều hướng của trang Admin.

---

## Flow

### Main Flow
1. Quản trị viên chọn mục "System Prompt" trên menu quản trị.
2. Hệ thống tải danh sách System Prompt hiện có trong cơ sở dữ liệu.
3. Hệ thống hiển thị danh sách với các trường: tên/mã hoặc ID, và thời gian cập nhật gần nhất của từng System Prompt.
4. Hệ thống mặc định sắp xếp danh sách theo thời gian cập nhật giảm dần (mới nhất lên đầu).
5. Quản trị viên chọn một System Prompt trong danh sách.
6. Hệ thống tải nội dung chi tiết và thời gian cập nhật của System Prompt được chọn.
7. Hệ thống chuyển đến màn hình xem chi tiết.
8. Quản trị viên xem đầy đủ nội dung Prompt ở chế độ chỉ xem (Read-only).

### Alternative Flow
- **ALT-01 — Danh sách chưa có System Prompt**:
  - Tại bước 3 của Main Flow, nếu hệ thống chưa có bản ghi System Prompt nào.
  - Hệ thống hiển thị trạng thái rỗng (Empty state) kèm thông báo: *"Chưa có System Prompt"*.
  - Luồng kết thúc.
- **ALT-02 — Quay lại danh sách từ màn hình chi tiết**:
  - Tại màn hình chi tiết, Quản trị viên chọn nút "Quay lại".
  - Hệ thống điều hướng trở về màn hình danh sách System Prompt.
  - Hệ thống hiển thị lại danh sách theo thứ tự thời gian cập nhật giảm dần.

### Exception Flow
- **EXC-01 — Không tải được chi tiết System Prompt**:
  - Tại bước 6-7 của Main Flow, hệ thống gặp sự cố mạng hoặc lỗi máy chủ không thể tải thông tin chi tiết.
  - Hệ thống không hiển thị dữ liệu chi tiết dở dang/thiếu sót.
  - Hệ thống hiển thị thông báo: *"Không thể tải chi tiết System Prompt. Vui lòng thử lại"* kèm nút "Thử lại".
  - Khi Quản trị viên chọn "Thử lại", hệ thống thực hiện lại yêu cầu tải chi tiết.
- **EXC-02 — Không tải được danh sách System Prompt**:
  - Tại bước 2-3 của Main Flow, hệ thống gặp sự cố không thể truy vấn danh sách.
  - Hệ thống không hiển thị danh sách lỗi/thiếu.
  - Hệ thống thông báo: *"Không thể tải danh sách System Prompt. Vui lòng thử lại"* kèm nút "Thử lại".
  - Khi Quản trị viên chọn "Thử lại", hệ thống thực hiện tải lại danh sách.
- **EXC-03 — System Prompt không còn tồn tại**:
  - Tại bước 6 của Main Flow, System Prompt vừa chọn đã bị xóa hoặc không còn tồn tại trong hệ thống.
  - Hệ thống không hiển thị màn hình chi tiết, đưa ra thông báo: *"System Prompt không còn tồn tại"*.
  - Quản trị viên chọn "Quay lại danh sách", hệ thống tự động tải lại danh sách System Prompt mới nhất.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách System Prompt**:
  - **Given**: Quản trị viên đã đăng nhập vào hệ thống.
  - **When**: Quản trị viên truy cập màn hình danh sách System Prompt.
  - **Then**: Hệ thống hiển thị danh sách các System Prompt hiện có.
  - **And**: Mỗi dòng System Prompt hiển thị rõ tên/mã hoặc ID và thời gian cập nhật gần nhất.

- **AC-002 — Thứ tự sắp xếp mặc định**:
  - **Given**: Hệ thống đang có nhiều System Prompt.
  - **When**: Quản trị viên truy cập màn hình danh sách System Prompt.
  - **Then**: Danh sách được sắp xếp theo thời gian cập nhật giảm dần, System Prompt có thời gian cập nhật gần nhất hiển thị đầu tiên.

- **AC-003 — Trạng thái danh sách rỗng (ALT-01)**:
  - **Given**: Hệ thống chưa có System Prompt nào.
  - **When**: Quản trị viên truy cập màn hình System Prompt.
  - **Then**: Hệ thống hiển thị giao diện trạng thái danh sách rỗng.
  - **And**: Hiển thị thông báo: *"Chưa có System Prompt"*.

- **AC-004 — Xem chi tiết System Prompt**:
  - **Given**: Danh sách System Prompt đang hiển thị và có ít nhất một bản ghi.
  - **When**: Quản trị viên chọn một System Prompt bất kỳ trong danh sách.
  - **Then**: Hệ thống điều hướng đến màn hình chi tiết của System Prompt được chọn.
  - **And**: Hệ thống hiển thị đầy đủ nội dung, tên/mã và thời gian cập nhật gần nhất.
  - **And**: Toàn bộ nội dung hiển thị ở chế độ chỉ xem (Read-only), không cho phép chỉnh sửa.

- **AC-005 — Quay lại danh sách từ màn hình chi tiết (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình xem chi tiết một System Prompt.
  - **When**: Quản trị viên nhấn nút "Quay lại".
  - **Then**: Hệ thống trở về màn hình danh sách System Prompt.
  - **And**: Danh sách vẫn duy trì thứ tự sắp xếp theo thời gian cập nhật giảm dần.

- **AC-006 — Xử lý lỗi không tải được danh sách (EXC-02)**:
  - **Given**: Quản trị viên truy cập màn hình danh sách System Prompt.
  - **When**: Hệ thống gặp lỗi kết nối hoặc truy vấn cơ sở dữ liệu thất bại.
  - **Then**: Hệ thống không hiển thị bảng dữ liệu dở dang.
  - **And**: Hiển thị thông báo lỗi không thể tải danh sách kèm nút "Thử lại".

- **AC-007 — Xử lý lỗi không tải được chi tiết (EXC-01)**:
  - **Given**: Quản trị viên chọn một System Prompt trong danh sách.
  - **When**: Hệ thống gặp lỗi khi truy vấn chi tiết bản ghi.
  - **Then**: Hệ thống không hiển thị màn hình chi tiết trắng/dở dang.
  - **And**: Hiển thị thông báo: *"Không thể tải chi tiết System Prompt. Vui lòng thử lại"* kèm nút "Thử lại".

- **AC-008 — Cơ chế thử lại khi gặp lỗi**:
  - **Given**: Giao diện đang hiển thị thông báo lỗi tải chi tiết hoặc tải danh sách.
  - **When**: Quản trị viên nhấn nút "Thử lại".
  - **Then**: Hệ thống gửi lại request truy vấn dữ liệu.
  - **And**: Nếu thành công, hiển thị đầy đủ thông tin tương ứng; nếu tiếp tục lỗi, giữ nguyên giao diện lỗi và nút "Thử lại".

- **AC-009 — Bản ghi không còn tồn tại (EXC-03)**:
  - **Given**: Quản trị viên chọn một System Prompt từ danh sách.
  - **When**: Bản ghi này đã bị xóa hoặc không còn tồn tại tại thời điểm tải chi tiết.
  - **Then**: Hệ thống không hiển thị màn hình chi tiết rỗng.
  - **And**: Hiển thị thông báo lỗi: *"System Prompt không còn tồn tại"*.
  - **And**: Cho phép Quản trị viên nhấn "Quay lại danh sách" để tự động refresh danh sách hiện hành.

---

## References

### Business Rules
- [BR-040: Sắp xếp danh sách System Prompt](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-040.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1610ca66-ea96-494a-a1fe-b80ee018ce9a))

---

## Non-Functional
- **Tốc độ phản hồi**: Thời gian tải danh sách và tải chi tiết System Prompt phải đạt dưới 2 giây trong điều kiện hoạt động bình thường.
- **Toàn vẹn hiển thị**: Nội dung Prompt phải hiển thị nguyên vẹn đầy đủ cấu trúc định dạng văn bản gốc và hoàn toàn ở chế độ chỉ xem (Read-only).

---

## Out of Scope
- Không bao gồm các chức năng tạo mới, chỉnh sửa, khôi phục, tìm kiếm, lọc hoặc xóa System Prompt (các tính năng này thuộc về các User Story quản trị Prompt riêng biệt).
