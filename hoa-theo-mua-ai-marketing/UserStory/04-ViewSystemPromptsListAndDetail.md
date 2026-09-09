# STORY-004: Xem danh sách và chi tiết System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem danh sách và thông tin chi tiết của System Prompt để có thể kiểm tra nội dung, ngày cập nhật của từng Prompt trước khi thực hiện các thao tác quản lý khác
- **Context**: Hệ thống sử dụng các System Prompt để định hướng cách các chức năng AI xử lý từng nghiệp vụ như tạo nội dung bài viết, paraphrase nội dung hoặc các tác vụ liên quan đến Marketing. Chức năng này cung cấp màn hình danh sách System Prompt và cho phép Quản trị viên chọn một System Prompt để xem thông tin chi tiết. 
  - **Quy định về trạng thái (Status)**: System Prompt **không có trạng thái (Active/Inactive)** do hệ thống duy trì cấu hình mẫu mặc định duy nhất cho từng phân loại nghiệp vụ (`type`: 0 => Flower, 1 => Card, 2 => Post).
  - **Quy định phạm vi**: Story này chỉ phục vụ mục đích xem dữ liệu (Read-only), không thực hiện tìm kiếm, lọc, chỉnh sửa, khôi phục hoặc xóa System Prompt.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.2
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 09/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Chưa có AC cho EXC-01, 02, ALT-02 System Prompt có status không? Mainflow 3, 4 sao như nay thế?"* — Nguyễn Đức Bình · 08:58 03/09/2026
  - **Phản hồi & Cập nhật**:
    1. **Về Status của System Prompt**: Đã làm rõ trong Context và Out of Scope rằng System Prompt **không có status** (Active/Inactive) vì mỗi loại nghiệp vụ (`type`) được gán cứng duy nhất một prompt hoạt động cho hệ thống AI (phù hợp với TDD-003 mục 2.3).
    2. **Về Mainflow 3, 4**: Đã sửa lại đúng trình tự kỹ thuật. Hệ thống truy vấn CSDL và sắp xếp theo `updated_at` giảm dần (BR-040) ở bước 2 TRƯỚC, sau đó bước 3 mới hiển thị danh sách đã sắp xếp lên giao diện (không còn lỗi hiển thị rồi mới sắp xếp).
    3. **Về Acceptance Criteria**: Đã liên kết và hoàn thiện đầy đủ:
       - **ALT-02** (Quay lại danh sách) $\rightarrow$ **AC-005**.
       - **EXC-02** (Lỗi tải danh sách) $\rightarrow$ **AC-006**.
       - **EXC-01** (Lỗi tải chi tiết Prompt) $\rightarrow$ **AC-007** và **AC-008** (Thử lại).
       - **EXC-03** (Prompt không còn tồn tại - 404) $\rightarrow$ **AC-009**.

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
2. Hệ thống truy vấn danh sách System Prompt hiện có từ cơ sở dữ liệu và tự động sắp xếp theo thời gian cập nhật giảm dần (`updated_at` DESC) (BR-040).
3. Hệ thống hiển thị danh sách System Prompt đã sắp xếp lên giao diện với các thông tin nhận diện: ID và Phân loại nghiệp vụ (`type`: hoa, thiệp, bài đăng).
4. Quản trị viên chọn một System Prompt trong danh sách để xem chi tiết.
5. Hệ thống gửi yêu cầu lấy chi tiết theo ID, truy vấn và tải nội dung đầy đủ (`content`) của System Prompt được chọn.
6. Hệ thống chuyển hướng và hiển thị màn hình chi tiết của System Prompt ở chế độ chỉ xem (Read-only).
7. Quản trị viên kiểm tra nội dung và thông tin của System Prompt.

### Alternative Flow
- **ALT-01 — Danh sách chưa có System Prompt**:
  - Tại bước 2 của Main Flow, nếu cơ sở dữ liệu chưa có bản ghi System Prompt nào (danh sách rỗng).
  - Hệ thống hiển thị trạng thái rỗng (Empty state) kèm thông báo: *"Chưa có System Prompt"*.
  - Luồng kết thúc.
- **ALT-02 — Quay lại danh sách từ màn hình chi tiết**:
  - Tại màn hình chi tiết, Quản trị viên chọn nút "Quay lại".
  - Hệ thống điều hướng trở về màn hình danh sách System Prompt.
  - Hệ thống hiển thị lại danh sách theo thứ tự thời gian cập nhật giảm dần.

### Exception Flow
- **EXC-01 — Không tải được chi tiết System Prompt**:
  - Tại bước 5 của Main Flow, hệ thống gặp lỗi mạng hoặc lỗi máy chủ (500 Internal Server Error) không thể tải thông tin chi tiết.
  - Hệ thống không hiển thị màn hình chi tiết dở dang.
  - Hệ thống hiển thị thông báo lỗi: *"Không thể tải chi tiết System Prompt. Vui lòng thử lại"* kèm nút "Thử lại".
  - Khi Quản trị viên chọn "Thử lại", hệ thống gửi lại yêu cầu tải chi tiết.
- **EXC-02 — Không tải được danh sách System Prompt**:
  - Tại bước 2 của Main Flow, hệ thống gặp sự cố kết nối cơ sở dữ liệu không thể lấy danh sách.
  - Hệ thống không hiển thị bảng dữ liệu dở dang hoặc bảng rỗng gây hiểu nhầm.
  - Hệ thống hiển thị thông báo lỗi: *"Không thể tải danh sách System Prompt. Vui lòng thử lại"* kèm nút "Thử lại".
  - Khi Quản trị viên chọn "Thử lại", hệ thống thực hiện lại yêu cầu lấy danh sách.
- **EXC-03 — System Prompt không còn tồn tại (404 Not Found)**:
  - Tại bước 5 của Main Flow, System Prompt vừa được chọn đã bị xóa hoặc không còn tồn tại trong cơ sở dữ liệu.
  - Hệ thống không hiển thị màn hình chi tiết, hiển thị thông báo lỗi: *"System Prompt không còn tồn tại"*.
  - Quản trị viên chọn "Quay lại danh sách", hệ thống tự động tải lại danh sách System Prompt mới nhất.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách System Prompt**:
  - **Given**: Quản trị viên đã đăng nhập vào trang admin.
  - **When**: Quản trị viên truy cập màn hình danh sách System Prompt.
  - **Then**: Hệ thống hiển thị danh sách các System Prompt hiện có.
  - **And**: Mỗi dòng System Prompt hiển thị rõ ID và phân loại nghiệp vụ (`type`), không trả nội dung `content` tại màn hình danh sách.

- **AC-002 — Thứ tự sắp xếp mặc định theo thời gian cập nhật**:
  - **Given**: Hệ thống có nhiều bản ghi System Prompt.
  - **When**: Quản trị viên truy cập màn hình danh sách System Prompt.
  - **Then**: Danh sách được sắp xếp theo thời gian cập nhật giảm dần, System Prompt có thời gian cập nhật gần nhất được hiển thị trước (BR-040).

- **AC-003 — Trạng thái danh sách rỗng (ALT-01)**:
  - **Given**: Hệ thống chưa có bất kỳ bản ghi System Prompt nào.
  - **When**: Quản trị viên truy cập màn hình System Prompt.
  - **Then**: Hệ thống hiển thị trạng thái danh sách rỗng (Empty state).
  - **And**: Hệ thống hiển thị thông báo: *"Chưa có System Prompt"*.

- **AC-004 — Xem chi tiết System Prompt**:
  - **Given**: Danh sách System Prompt đã được hiển thị và có ít nhất một bản ghi.
  - **When**: Quản trị viên nhấn chọn một System Prompt trong danh sách.
  - **Then**: Hệ thống chuyển đến màn hình chi tiết của System Prompt được chọn.
  - **And**: Hệ thống hiển thị đầy đủ ID, loại (`type`) và nội dung chi tiết (`content`).
  - **And**: Toàn bộ nội dung hiển thị ở chế độ chỉ xem (Read-only), không cho phép thao tác chỉnh sửa.

- **AC-005 — Quay lại danh sách từ màn hình chi tiết (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình xem chi tiết một System Prompt.
  - **When**: Quản trị viên chọn "Quay lại".
  - **Then**: Hệ thống trở về màn hình danh sách System Prompt.
  - **And**: Danh sách được hiển thị theo đúng thứ tự sắp xếp thời gian cập nhật giảm dần.

- **AC-006 — Xử lý lỗi không tải được danh sách System Prompt (EXC-02)**:
  - **Given**: Quản trị viên đang truy cập màn hình danh sách System Prompt.
  - **When**: Hệ thống gặp sự cố không thể tải danh sách (500 Internal Server Error).
  - **Then**: Hệ thống không hiển thị bảng dữ liệu không đầy đủ.
  - **And**: Hệ thống hiển thị thông báo lỗi: *"Không thể tải danh sách System Prompt. Vui lòng thử lại"*.
  - **And**: Hệ thống cung cấp nút "Thử lại" để Quản trị viên có thể tải lại dữ liệu mà không cần tải lại toàn bộ trang web.

- **AC-007 — Xử lý lỗi không tải được thông tin chi tiết (EXC-01)**:
  - **Given**: Quản trị viên chọn một System Prompt đang tồn tại trong danh sách.
  - **When**: Hệ thống gặp sự cố máy chủ không thể tải nội dung chi tiết.
  - **Then**: Hệ thống không hiển thị dữ liệu chi tiết dở dang/trắng trang.
  - **And**: Hệ thống hiển thị thông báo: *"Không thể tải chi tiết System Prompt. Vui lòng thử lại"*.
  - **And**: Hệ thống hiển thị nút "Thử lại".

- **AC-008 — Cơ chế thử lại khi tải chi tiết thất bại (EXC-01)**:
  - **Given**: Hệ thống đang hiển thị thông báo lỗi không tải được chi tiết System Prompt kèm nút "Thử lại".
  - **When**: Quản trị viên nhấn chọn nút "Thử lại".
  - **Then**: Hệ thống gửi lại yêu cầu tải thông tin chi tiết đến máy chủ.
  - **And**: Nếu tải thành công, hệ thống hiển thị đầy đủ thông tin chi tiết của System Prompt.
  - **And**: Nếu tiếp tục thất bại, hệ thống duy trì trạng thái lỗi và tiếp tục cho phép Quản trị viên bấm thử lại.

- **AC-009 — System Prompt không còn tồn tại tại thời điểm xem (EXC-03)**:
  - **Given**: Quản trị viên đã chọn một System Prompt trong danh sách.
  - **When**: Bản ghi System Prompt đó không còn tồn tại trong CSDL tại thời điểm tải chi tiết (lỗi 404 Not Found).
  - **Then**: Hệ thống không hiển thị màn hình chi tiết.
  - **And**: Hệ thống hiển thị thông báo lỗi: *"System Prompt không còn tồn tại"*.
  - **And**: Hệ thống cung cấp tùy chọn cho phép Quản trị viên quay lại màn hình danh sách và tự động làm mới danh sách hiện tại.

---

## References

### TDDs
- [TDD-003: Xem danh sách và chi tiết System Prompt](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-003-ViewSystemPromptsListAndDetail.md)

### Rules
- [BR-040: Sắp xếp danh sách System Prompt](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-040.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1610ca66-ea96-494a-a1fe-b80ee018ce9a))

---

## Non-Functional
- **Hiệu năng**: Thời gian tải danh sách và chi tiết System Prompt phải dưới 2 giây trong điều kiện hoạt động bình thường.
- **Tính toàn vẹn**: Nội dung System Prompt phải hiển thị đầy đủ, giữ nguyên định dạng và không bị biến đổi khi ở chế độ xem (Read-only).

---

## Out of Scope
- Không hỗ trợ tìm kiếm, lọc hoặc phân trang danh sách System Prompt.
- Không bao gồm chỉnh sửa, khôi phục, tạo mới hoặc xóa System Prompt.
- Không hỗ trợ trạng thái Active/Inactive, quản lý phiên bản hoặc xem lịch sử thay đổi của Prompt.
- Không tự động retry truy vấn database ở tầng backend.
