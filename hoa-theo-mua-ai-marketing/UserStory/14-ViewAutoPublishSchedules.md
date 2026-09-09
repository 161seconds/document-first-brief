# STORY-014: Xem lịch đăng bài tự động

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem danh sách lịch đăng bài để theo dõi các lịch đăng bài đã lên của mình
- **Context**: Sau khi lịch đăng được tạo, Quản trị viên cần màn hình tổng hợp để theo dõi kế hoạch đăng bài đa nền tảng theo thời gian. Hệ thống hiển thị lịch đăng bài theo dạng bảng, sắp xếp theo thời gian tăng dần theo ngày. Story này chỉ bao gồm xem danh sách; không bao gồm tạo, tìm kiếm, lọc, xem chi tiết, sửa hoặc xóa lịch.
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
  > *"Kiểm tra lại AC - 005, có thực sự là phân loại theo timezone không? Chưa có AC cho EXC - 04"* — Nguyễn Đức Bình · 09:08 03/09/2026
  - **Phản hồi & Cập nhật**:
    1. **Về Timezone**: Đã xác nhận hệ thống **không phân loại hay lọc theo timezone**. Mọi mốc thời gian hiển thị trên danh sách đều được chuẩn hóa và hiển thị thống nhất theo múi giờ hệ thống `Asia/Ho_Chi_Minh` (GMT+7) theo đúng mục Non-Functional. AC-005 là trường hợp lỗi tải dữ liệu (EXC-02), không liên quan đến phân loại timezone.
    2. **Về AC cho ALT-04 / EXC-04**: 
       - Đã bổ sung **AC-007** bao phủ **ALT-04** (Xử lý trường hợp nhiều lịch có cùng thời điểm đăng — đảm bảo thứ tự hiển thị ổn định theo BR-012, không gộp hay làm mất lịch).
       - Đã bổ sung **EXC-04** và **AC-008** (Xử lý khi dữ liệu phản hồi bị gián đoạn hoặc không đồng bộ trong quá trình tải danh sách).

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang quản trị với quyền hạn hợp lệ.
- **Trigger**: 
  - Quản trị viên chọn mục "Lịch đăng bài" trên menu quản trị.

---

## Flow

### Main Flow
1. Quản trị viên mở màn hình "Lịch đăng bài" từ menu điều hướng.
2. Hệ thống tải dữ liệu và hiển thị danh sách lịch đăng bài theo dạng bảng, sắp xếp theo thời gian đăng tăng dần (từ gần nhất đến xa nhất trong tương lai) (BR-007).
3. Quản trị viên xem danh sách các lịch và tổng số lượng lịch đã lên (BR-013).

### Alternative Flow
- **ALT-01 — Không có lịch đăng bài**:
  - Tại bước 2, nếu hệ thống chưa có lịch đăng bài nào.
  - Hệ thống hiển thị giao diện danh sách rỗng kèm thông báo: *"Chưa có lịch đăng bài nào"*.
- **ALT-04 — Nhiều lịch có cùng thời điểm đăng**:
  - Khi có nhiều lịch đăng bài có cùng ngày giờ dự kiến đăng (ví dụ cùng lúc đăng lên Facebook và Instagram).
  - Hệ thống bảo lưu và hiển thị đầy đủ tất cả các bản ghi lịch, không tự ý gộp hoặc loại bỏ lịch trùng thời điểm.
  - Áp dụng tiêu chí sắp xếp phụ (theo thời gian tạo `created_at` hoặc ID) để đảm bảo thứ tự hiển thị luôn nhất quán và ổn định (BR-012).

### Exception Flow
- **EXC-01 — Phiên đăng nhập hết hạn**:
  - Tại bước 2, nếu phiên đăng nhập của Quản trị viên đã hết hạn.
  - Hệ thống không tải dữ liệu lịch, hiển thị thông báo phiên hết hạn và tự động chuyển hướng đến màn hình đăng nhập.
- **EXC-02 — Không thể tải dữ liệu lịch**:
  - Tại bước 2-3, nếu kết nối mạng lỗi hoặc máy chủ quá tải không phản hồi.
  - Hệ thống không hiển thị danh sách chắp vá hay thiếu sót (BR-014).
  - Hệ thống thông báo: *"Không thể tải lịch đăng bài. Vui lòng thử lại"* và cung cấp nút "Thử lại".
  - Nếu màn hình đã có dữ liệu từ lần tải thành công trước đó, giữ nguyên dữ liệu đó và cảnh báo dữ liệu chưa được làm mới.
- **EXC-04 — Dữ liệu phản hồi bị lỗi cấu trúc hoặc gián đoạn kết nối**:
  - Trong quá trình nhận dữ liệu từ server, kết nối bị ngắt quãng hoặc dữ liệu JSON trả về bị lỗi định dạng.
  - Hệ thống từ chối render dữ liệu hỏng, giữ an toàn cho giao diện, hiển thị thông báo lỗi và cho phép Quản trị viên bấm "Thử lại".

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách lịch theo dạng bảng**:
  - **Given**: Quản trị viên đã đăng nhập vào trang quản trị.
  - **When**: Quản trị viên truy cập màn hình Lịch đăng bài.
  - **Then**: Hệ thống hiển thị danh sách lịch đăng bài theo dạng bảng, sắp xếp mặc định theo ngày giờ đăng tăng dần (BR-007).

- **AC-002 — Trạng thái danh sách rỗng (ALT-01)**:
  - **Given**: Hệ thống chưa có lịch đăng bài nào được tạo.
  - **When**: Quản trị viên truy cập màn hình Lịch đăng bài.
  - **Then**: Hệ thống hiển thị trạng thái danh sách rỗng.
  - **And**: Hiển thị thông báo: *"Chưa có lịch đăng bài nào"*.

- **AC-003 — Sắp xếp chính xác theo thời gian**:
  - **Given**: Hệ thống có nhiều lịch đăng bài với các mốc thời gian khác nhau.
  - **When**: Hệ thống tải và hiển thị danh sách.
  - **Then**: Toàn bộ các lịch hiển thị theo thứ tự thời gian tăng dần (sự kiện diễn ra sớm hơn được xếp trước).

- **AC-004 — Khớp tổng số lịch chính xác**:
  - **Given**: Hệ thống tải thành công danh sách lịch hợp lệ.
  - **When**: Danh sách được hiển thị trên giao diện.
  - **Then**: Tổng số lượng bản ghi hiển thị trên nhãn tổng số phải bằng đúng số lượng bản ghi trong bảng (BR-013).
  - **And**: Không có lịch nào bị lặp bản ghi hoặc bị bỏ sót.

- **AC-005 — Xử lý lỗi khi không tải được dữ liệu lịch (EXC-02)**:
  - **Given**: Quản trị viên truy cập màn hình Lịch đăng bài.
  - **When**: Yêu cầu tải dữ liệu bị lỗi kết nối hoặc quá thời gian chờ phản hồi (timeout).
  - **Then**: Hệ thống không hiển thị danh sách chưa đầy đủ (BR-014).
  - **And**: Không cập nhật tổng số lịch bằng dữ liệu chưa hoàn chỉnh.
  - **And**: Hệ thống hiển thị thông báo lỗi và nút cho phép Quản trị viên thử lại.

- **AC-006 — Phiên đăng nhập hết hạn (EXC-01)**:
  - **Given**: Phiên làm việc của Quản trị viên đã hết hạn.
  - **When**: Quản trị viên truy cập hoặc làm mới màn hình Lịch đăng bài.
  - **Then**: Hệ thống chặn tải dữ liệu lịch.
  - **And**: Hiển thị thông báo phiên đăng nhập đã hết hạn và chuyển hướng Quản trị viên về màn hình đăng nhập.

- **AC-007 — Nhiều lịch có cùng thời điểm đăng (ALT-04)**:
  - **Given**: Tồn tại 2 hoặc nhiều lịch đăng bài có cùng ngày và giờ đăng dự kiến.
  - **When**: Hệ thống hiển thị danh sách lịch.
  - **Then**: Hệ thống hiển thị đầy đủ tất cả các lịch riêng biệt, không gộp và không bỏ sót lịch nào.
  - **And**: Áp dụng tiêu chí sắp xếp phụ ổn định (theo ID/thời gian tạo) để thứ tự hiển thị không bị nhảy vị trí khi tải lại trang (BR-012).

- **AC-008 — Gián đoạn đường truyền khi tải danh sách (EXC-04)**:
  - **Given**: Quản trị viên đang tải danh sách lịch đăng bài.
  - **When**: Kết nối bị ngắt giữa chừng hoặc dữ liệu trả về bị hỏng.
  - **Then**: Hệ thống không hiển thị dữ liệu lỗi một phần.
  - **And**: Hiển thị thông báo: *"Không thể tải lịch đăng bài. Vui lòng thử lại"* kèm nút "Thử lại".

---

## References

### Business Rules
- [BR-007: Sắp xếp danh sách lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-007.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/64397395-3bee-4810-b0fb-36a88065b493))
- [BR-012: Thứ tự ổn định khi nhiều lịch có cùng thời điểm](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-012.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/82bb5dcd-988b-449a-9f8d-0676025e2e25))
- [BR-013: Tổng số lịch phải khớp danh sách hiển thị](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-013.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/ea2b2e70-3b1b-4843-af44-02c91f3c6050))
- [BR-014: Không hiển thị danh sách lịch chưa đầy đủ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-014.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/4792eb80-b0dd-4e70-9238-cee45ac86922))

---

## Non-Functional
- **Thống nhất múi giờ (Timezone)**: Tất cả ngày và giờ hiển thị trên danh sách lịch phải dùng thống nhất múi giờ chuẩn của hệ thống (GMT+7 - Asia/Ho_Chi_Minh).
- **Tính toàn vẹn**: Thao tác xem danh sách hoàn toàn là thao tác đọc (Read-only), không làm thay đổi trạng thái hoặc dữ liệu của bất kỳ lịch đăng nào.

---

## Out of Scope
- Không bao gồm tạo mới hoặc tự động đăng bài.
- Không bao gồm sửa, xóa hoặc hủy lịch đăng bài.
- Không bao gồm tìm kiếm, lọc, xem chi tiết hoặc sắp xếp thủ công (các tính năng này thuộc về các Story quản trị riêng).
