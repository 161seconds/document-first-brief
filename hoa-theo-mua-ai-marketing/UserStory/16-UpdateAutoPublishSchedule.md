# STORY-016: Sửa lịch đăng bài tự động

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn sửa thông tin của lịch đăng bài tự động để kế hoạch đăng luôn phù hợp với nhu cầu thực tế
- **Context**: Trong quá trình quản lý, thông tin của lịch đăng bài có thể không còn phù hợp. Quản trị viên cần cập nhật lịch hiện có mà không phải tạo lại từ đầu. Trước khi lưu, hệ thống phải kiểm tra quyền, trạng thái lịch và tính hợp lệ của dữ liệu. Chức năng này chỉ bao gồm sửa lịch, không bao gồm tạo, xem, xóa hoặc chỉnh sửa bài đã đăng trên nền tảng.
- **Quy định các trường được phép sửa**:
  - **Content đã lưu**: Chọn một content còn tồn tại trong hệ thống.
  - **Nền tảng đăng**: Chọn một hoặc nhiều nền tảng đang kết nối hợp lệ với hệ thống.
  - **Ngày đăng**: Ngày mới phải được nhập đầy đủ và lớn hơn thời điểm hiện tại.
  - **Giờ đăng**: Giờ mới phải được nhập đầy đủ và lớn hơn thời điểm hiện tại.
  - **Hình ảnh gắn với lịch đăng bài**: Cho phép thay đổi/gắn ảnh mới (JPG, JPEG, PNG $\le$ 10MB, tối đa 10 ảnh).
- **Quy định trạng thái cho phép sửa**:
  - **Chỉ được phép sửa khi**: Lịch đang ở trạng thái **"Đã lên lịch"** (chưa đến hạn đăng và Job chưa chạy).
  - **Không được phép sửa khi**: Lịch đang được đăng (Job đang chạy) hoặc đã ở trạng thái **"Đăng thành công"**.
- **Quy định kiểm tra trùng lịch (Validation)**:
  - Không được cập nhật lịch dẫn đến trùng đồng thời cả Content, Nền tảng và Thời điểm đăng với một lịch khác đang "Đã lên lịch".
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
  > *"Context không cho sửa ảnh hả? Chưa liệt kê trạng thái cho phép sửa. US này không có NFR hả? Chưa có AC cho việc lịch trùng."* — Nguyễn Đức Bình · 09:47 03/09/2026

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Lịch đăng bài còn tồn tại và ở trạng thái được phép sửa ("Đã lên lịch").
- **Trigger**: 
  - Quản trị viên chọn nút "Sửa" tại một bản ghi lịch đăng bài.

---

## Flow

### Main Flow — Sửa lịch đăng bài tự động
1. Quản trị viên chọn lịch cần sửa trong danh sách.
2. Quản trị viên nhấn nút "Sửa".
3. Hệ thống kiểm tra trạng thái hiện tại của lịch: xác nhận lịch còn tồn tại và chưa được thực thi đăng (trạng thái "Đã lên lịch").
4. Hệ thống hiển thị màn hình chỉnh sửa và điền sẵn toàn bộ dữ liệu hiện tại của lịch.
5. Quản trị viên thay đổi một hoặc nhiều trường được phép sửa (Content, Nền tảng, Ngày đăng, Giờ đăng, Hình ảnh đính kèm).
6. Quản trị viên chọn "Lưu".
7. Hệ thống kiểm tra tính hợp lệ của dữ liệu:
   - Ngày và giờ đăng mới phải hợp lệ và lớn hơn thời điểm hiện tại.
   - Content phải còn tồn tại, đã được lưu và không để trống.
   - Nền tảng phải còn kết nối hợp lệ.
   - Không bị trùng với lịch đăng khác về content, nền tảng và thời điểm.
8. Hệ thống cập nhật bản ghi lịch trong cơ sở dữ liệu và tái cấu hình Cron Job thực thi theo mốc thời gian/dữ liệu mới.
9. Hệ thống hiển thị thông báo: *"Cập nhật lịch đăng bài thành công"* và hiển thị dữ liệu mới nhất.

### Alternative Flow
- **ALT-01 — Hủy chỉnh sửa**:
  - Trong khi màn hình chỉnh sửa đang hiển thị, Quản trị viên chọn "Hủy".
  - Hệ thống đóng màn hình chỉnh sửa, không gửi yêu cầu cập nhật, giữ nguyên lịch đăng bài và Cron Job cũ.
- **ALT-02 — Chỉ sửa một phần thông tin**:
  - Tại bước 5, Quản trị viên chỉ cập nhật một phần thông tin (ví dụ: chỉ đổi giờ đăng, giữ nguyên content và nền tảng).
  - Hệ thống chỉ cập nhật các trường có thay đổi, giữ nguyên các thông tin còn lại và tiếp tục từ bước 6.
- **ALT-03 — Không có thay đổi để lưu**:
  - Quản trị viên mở màn hình chỉnh sửa nhưng không thay đổi bất kỳ dữ liệu nào và nhấn "Lưu".
  - Hệ thống xác định không có thay đổi, không cập nhật CSDL hay Cron Job, hiển thị thông báo: *"Không có thay đổi để lưu"*.
- **ALT-04 — Thoát khi có thay đổi chưa lưu**:
  - Quản trị viên đã thay đổi dữ liệu nhưng chưa lưu mà nhấn đóng hoặc quay lại.
  - Hệ thống hiển thị cảnh báo có thay đổi chưa lưu.
  - Nếu chọn "Tiếp tục chỉnh sửa", giữ nguyên màn hình và dữ liệu đang nhập.
  - Nếu chọn "Thoát không lưu", loại bỏ nội dung chưa lưu và giữ nguyên lịch cũ.

### Exception Flow
- **EXC-01 — Lịch không còn hợp lệ để sửa**:
  - Tại bước 3, nếu lịch không tồn tại hoặc đã chuyển sang trạng thái đang chạy / đã đăng.
  - Hệ thống dừng thao tác, thông báo lý do lịch không thể sửa và tải lại danh sách.
- **EXC-02 — Dữ liệu không hợp lệ hoặc trùng lịch**:
  - Tại bước 7, nếu content/media không còn tồn tại, ngày/giờ nhỏ hơn thời điểm hiện tại, hoặc bị trùng lặp với lịch khác.
  - Hệ thống không cập nhật lịch, đánh dấu các trường lỗi, hiển thị lý do chi tiết và giữ lại dữ liệu đang nhập để Quản trị viên sửa lại.
- **EXC-03 — Lỗi hệ thống khi cập nhật**:
  - Tại bước 8, xảy ra lỗi mạng hoặc máy chủ không thể hoàn tất lưu CSDL hoặc cập nhật Cron Job.
  - Hệ thống rollback, giữ nguyên dữ liệu cũ và thông báo: *"Không thể cập nhật lịch đăng bài. Vui lòng thử lại"*.
- **EXC-04 — Xung đột phiên bản (Lịch bị thay đổi trong lúc chỉnh sửa)**:
  - Admin A mở form sửa; trong lúc đó Admin B đã cập nhật hoặc lịch đã chuyển trạng thái.
  - Admin A bấm lưu $\rightarrow$ hệ thống chặn ghi đè, thông báo lịch đã bị thay đổi và yêu cầu tải lại dữ liệu mới nhất.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi nhấn Lưu $\rightarrow$ hệ thống từ chối cập nhật, yêu cầu đăng nhập lại.
- **EXC-06 — Không thể tải dữ liệu hiện tại của lịch**:
  - Tại bước 4, hệ thống không thể tải đầy đủ thông tin ban đầu của lịch $\rightarrow$ không hiển thị form thiếu dữ liệu, thông báo lỗi và cho phép tải lại.

---

## Acceptance Criteria

- **AC-001 — Mở biểu mẫu chỉnh sửa thành công**:
  - **Given**: Lịch đăng bài đang ở trạng thái được phép sửa ("Đã lên lịch").
  - **When**: Quản trị viên chọn "Sửa".
  - **Then**: Hệ thống hiển thị màn hình chỉnh sửa, điền sẵn đầy đủ dữ liệu hiện tại và cho phép sửa các trường theo quy định.
  - **And**: Dữ liệu trên hệ thống chưa bị thay đổi trước khi Quản trị viên nhấn "Lưu".

- **AC-002 — Cập nhật thành công dữ liệu mới**:
  - **Given**: Màn hình chỉnh sửa đang hiển thị và dữ liệu nhập mới hoàn toàn hợp lệ.
  - **When**: Quản trị viên bấm "Lưu".
  - **Then**: Hệ thống cập nhật bản ghi lịch và cấu hình Cron Job thực thi theo dữ liệu mới.
  - **And**: Hiển thị thông báo: *"Cập nhật lịch đăng bài thành công"*.

- **AC-003 — Hủy thao tác chỉnh sửa (ALT-01)**:
  - **Given**: Quản trị viên đã thay đổi thông tin trên màn hình chỉnh sửa.
  - **When**: Quản trị viên chọn "Hủy".
  - **Then**: Hệ thống đóng màn hình chỉnh sửa và giữ nguyên lịch đăng bài ban đầu.

- **AC-004 — Chặn lưu khi dữ liệu không hợp lệ (EXC-02)**:
  - **Given**: Màn hình chỉnh sửa đang hiển thị.
  - **When**: Quản trị viên lưu dữ liệu không hợp lệ (ví dụ: ngày giờ trong quá khứ, để trống content).
  - **Then**: Hệ thống từ chối cập nhật, đánh dấu lỗi tại từng trường tương ứng và giữ nguyên dữ liệu trên form.

- **AC-005 — Cập nhật một phần thông tin (ALT-02)**:
  - **Given**: Màn hình chỉnh sửa đang hiển thị dữ liệu hiện tại.
  - **When**: Quản trị viên chỉ thay đổi một phần thông tin hợp lệ (ví dụ: đổi nền tảng) và nhấn "Lưu".
  - **Then**: Hệ thống chỉ cập nhật các trường đã thay đổi, giữ nguyên các trường còn lại và đồng bộ cấu hình Cron Job.

- **AC-006 — Xử lý khi không có thay đổi (ALT-03)**:
  - **Given**: Quản trị viên mở form nhưng không thay đổi bất kỳ trường nào.
  - **When**: Quản trị viên chọn "Lưu".
  - **Then**: Hệ thống không gửi request cập nhật, giữ nguyên lịch cũ và hiển thị: *"Không có thay đổi để lưu"*.

- **AC-007 — Chặn sửa khi lịch đã đổi trạng thái (EXC-01)**:
  - **Given**: Lịch đã bị xóa hoặc chuyển sang trạng thái đang chạy / đã đăng thành công.
  - **When**: Quản trị viên cố gắng mở hoặc lưu màn hình chỉnh sửa.
  - **Then**: Hệ thống từ chối cập nhật và tải lại trạng thái thực tế của lịch.

- **AC-008 — Rollback khi xảy ra lỗi hệ thống (EXC-03)**:
  - **Given**: Quản trị viên đang lưu dữ liệu hợp lệ.
  - **When**: Quá trình cập nhật CSDL hoặc cấu hình Cron Job xảy ra sự cố kỹ thuật.
  - **Then**: Hệ thống không ghi nhận cập nhật thành công, không lưu dở dang và giữ lại dữ liệu đang nhập để Quản trị viên thử lại.

- **AC-009 — Xử lý xung đột phiên bản (EXC-04)**:
  - **Given**: Quản trị viên A đang chỉnh sửa dựa trên phiên bản lịch cũ; Quản trị viên B đã cập nhật trước.
  - **When**: Quản trị viên A chọn "Lưu".
  - **Then**: Hệ thống không ghi đè dữ liệu mới nhất của Quản trị viên B.
  - **And**: Thông báo cho Quản trị viên A rằng lịch đã thay đổi và cho phép tải lại dữ liệu mới.

- **AC-010 — Xử lý phiên đăng nhập hết hạn (EXC-05)**:
  - **Given**: Phiên làm việc của Quản trị viên hết hạn trong lúc đang thao tác trên form.
  - **When**: Quản trị viên bấm "Lưu".
  - **Then**: Hệ thống từ chối cập nhật và yêu cầu Quản trị viên đăng nhập lại.

- **AC-011 — Cảnh báo thoát khi có thay đổi chưa lưu (ALT-04)**:
  - **Given**: Quản trị viên đã thay đổi dữ liệu nhưng chưa lưu.
  - **When**: Quản trị viên yêu cầu đóng màn hình hoặc chuyển hướng.
  - **Then**: Hệ thống hiển thị modal cảnh báo xác nhận. Chọn tiếp tục sẽ giữ dữ liệu; chọn thoát sẽ hủy bỏ thay đổi.

- **AC-012 — Xử lý lỗi không tải được dữ liệu ban đầu (EXC-06)**:
  - **Given**: Quản trị viên nhấn "Sửa" tại một lịch đăng bài.
  - **When**: Hệ thống không thể tải đầy đủ thông tin hiện tại của lịch.
  - **Then**: Hệ thống không hiển thị biểu mẫu chứa dữ liệu thiếu sót và hiển thị thông báo lỗi kèm nút thử lại.

- **AC-013 — Chặn cập nhật thành lịch bị trùng**:
  - **Given**: Đã tồn tại một lịch đăng bài khác có cùng Content, Nền tảng và Thời điểm đăng.
  - **When**: Quản trị viên thay đổi dữ liệu khiến lịch đang sửa bị trùng hoàn toàn với lịch đã tồn tại và nhấn "Lưu".
  - **Then**: Hệ thống chặn cập nhật, thông báo rằng đã tồn tại lịch có cùng Content, Media, Nền tảng và Thời điểm đăng.
  - **And**: Dữ liệu hiện tại của lịch được giữ nguyên.

---

## References

### Business Rules
- [BR-008: Trạng thái hợp lệ để chỉnh sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-008.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/c90b51f9-dde9-4ad8-8374-d7a0f1ba3c2e))
- [BR-009: Cập nhật dữ liệu khi sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-009.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/13ba9f66-51a7-4cc9-9596-bb5725b8aac1))
- [BR-053: Dữ liệu hợp lệ khi sửa lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-053.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9c59b3ec-029f-420a-8eae-643b64d2319d))
- [BR-054: Không ghi đè lịch đã thay đổi trong lúc sửa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-054.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/fb457dd2-6461-4c3d-8732-fdbd18217696))

---

## Non-Functional
- **Tốc độ phản hồi**: Thao tác mở form và cập nhật lịch đăng bài phải phản hồi trong vòng 2 giây trong điều kiện hoạt động bình thường.
- **Đồng bộ Cron Job**: Sau khi cập nhật thành công, Cron Job bắt buộc phải thực thi chính xác theo thời gian, Content, Media và Nền tảng mới nhất của lịch.
- **Kiểm tra an toàn Backend**: Backend phải kiểm tra lại quyền hạn, trạng thái lịch, tính hợp lệ của ảnh/thời gian và kiểm tra lịch trùng trước khi thực thi lệnh cập nhật vào cơ sở dữ liệu.

---

## Out of Scope
- Không bao gồm tạo lịch mới hoặc xem danh sách/chi tiết lịch.
- Không bao gồm xóa lịch đăng bài.
- Không bao gồm chỉnh sửa bài viết đã đăng thành công trên các mạng xã hội bên thứ ba.
