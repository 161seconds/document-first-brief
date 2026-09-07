# STORY-005: Sửa System Prompt

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chỉnh sửa System Prompt và quản lý phiên bản để có thể điều chỉnh cách AI xử lý các chức năng của hệ thống khi yêu cầu nghiệp vụ thay đổi
- **Context**: System Prompt là thành phần cốt lõi được sử dụng để định hướng cách AI xử lý và trả về kết quả (tạo caption, viết bài, paraphrase...). Khi cần thay đổi nội dung System Prompt, Quản trị viên thực hiện chỉnh sửa trên phiên bản hiện tại. Hệ thống cập nhật phiên bản mới từ nội dung đã chỉnh sửa và lưu trữ phiên bản cũ vào lịch sử.
- **Quy định dữ liệu của System Prompt**:
  - **Độ dài**: Tối thiểu 1 ký tự sau khi loại bỏ khoảng trắng ở đầu và cuối; tối đa 20.000 ký tự.
  - **Định dạng**: Cho phép tiếng Việt, ký tự Unicode, ký tự xuống dòng và cú pháp Markdown.
  - **Ràng buộc an toàn**: Không cho phép nhập các ký tự điều khiển không hiển thị (invisible control characters).
  - **Tính toàn vẹn**: Hệ thống không tự động thay đổi khoảng trắng, thụt dòng, xuống dòng hoặc nội dung bên trong Prompt.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 06/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Làm rõ lại chỗ preview khi thay đổi system prompt, input để AI run ra output cho phần này là gì chưa thấy đề cập. Chưa có AC cho EXC-04, 05, 01."* — Nguyễn Đức Bình · 09:01 03/09/2026

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin với quyền quản trị phù hợp.
- **Trigger**: 
  - Quản trị viên chọn một System Prompt cụ thể và nhấn "Chỉnh sửa".

---

## Flow

### Main Flow
1. Quản trị viên mở màn hình chi tiết một System Prompt.
2. Quản trị viên chọn chức năng "Chỉnh sửa".
3. Hệ thống hiển thị nội dung phiên bản hiện tại trong biểu mẫu chỉnh sửa (editor).
4. Quản trị viên chỉnh sửa nội dung System Prompt.
5. Hệ thống kiểm tra tính hợp lệ của nội dung theo quy định (1 - 20.000 ký tự, không chứa ký tự điều khiển).
6. Quản trị viên chọn "Lưu".
7. Hệ thống xác thực nội dung không để trống và đáp ứng đầy đủ quy tắc định dạng.
8. Hệ thống tạo một phiên bản mới, đồng thời lưu giữ nguyên vẹn phiên bản trước đó trong lịch sử phiên bản (Version History).
9. Phiên bản vừa chỉnh sửa chính thức trở thành phiên bản hiện tại (Active version) được các dịch vụ AI sử dụng.
10. Hệ thống hiển thị thông báo: *"Cập nhật System Prompt thành công"*.

### Alternative Flow
- **ALT-01 — Hủy chỉnh sửa**:
  - Quản trị viên đã thay đổi nội dung nhưng chọn "Hủy" hoặc "Quay lại".
  - Hệ thống hiển thị modal cảnh báo xác nhận thoát.
  - Nếu chọn "Thoát không lưu", hệ thống không lưu nội dung vừa sửa, giữ nguyên phiên bản hiện tại và quay lại màn hình chi tiết.
- **ALT-02 — Không có thay đổi để lưu**:
  - Quản trị viên bấm "Lưu" khi chưa thay đổi bất kỳ ký tự nào trong nội dung Prompt.
  - Hệ thống hiển thị thông báo: *"Không có thay đổi để lưu"* và giữ nguyên màn hình chỉnh sửa.
- **ALT-03 — Xử lý lựa chọn trong modal xác nhận thoát**:
  - Nếu Quản trị viên chọn "Tiếp tục chỉnh sửa": hệ thống giữ nguyên màn hình và toàn bộ nội dung đang nhập dở dang.
  - Nếu Quản trị viên chọn "Thoát không lưu": hệ thống hủy bỏ toàn bộ nội dung chưa lưu và quay về phiên bản hiện tại.

### Exception Flow
- **EXC-01 — Nội dung Prompt không hợp lệ**:
  - Quản trị viên để trống nội dung, chỉ nhập khoảng trắng, nhập vượt quá 20.000 ký tự hoặc chứa ký tự điều khiển ẩn.
  - Hệ thống chặn thao tác lưu, hiển thị thông báo lỗi tại trường tương ứng và yêu cầu chỉnh sửa lại.
- **EXC-02 — Lỗi hệ thống khi lưu**:
  - Máy chủ hoặc cơ sở dữ liệu xảy ra lỗi trong quá trình tạo phiên bản mới.
  - Hệ thống không tạo phiên bản mới chưa hoàn chỉnh, không làm mất phiên bản hiện tại.
  - Hệ thống thông báo: *"Không thể lưu, vui lòng thử lại"* và giữ lại nội dung đang nhập để Quản trị viên thử lại.
- **EXC-04 — Xung đột phiên bản (Optimistic Concurrency Conflict)**:
  - Quản trị viên A mở phiên bản v1 để chỉnh sửa.
  - Trong lúc đó, Quản trị viên B đã cập nhật thành công lên phiên bản v2.
  - Quản trị viên A nhấn "Lưu" nội dung dựa trên nền tảng v1 cũ.
  - Hệ thống phát hiện xung đột, không ghi đè lên phiên bản v2 của Quản trị viên B.
  - Hệ thống thông báo: *"System Prompt đã được người khác cập nhật. Vui lòng tải lại phiên bản mới nhất."*

---

## Acceptance Criteria

- **AC-001 — Kiểm tra tính hợp lệ của nội dung (EXC-01)**:
  - **Given**: Quản trị viên đang ở màn hình biểu mẫu chỉnh sửa System Prompt.
  - **When**: Quản trị viên nhập nội dung rỗng, chỉ chứa khoảng trắng, vượt quá 20.000 ký tự hoặc chứa ký tự điều khiển không hiển thị và bấm "Lưu".
  - **Then**: Hệ thống chặn thao tác lưu.
  - **And**: Hiển thị thông báo lỗi chi tiết tại trường nội dung.

- **AC-002 — Lưu vết kiểm toán (Audit Trail)**:
  - **Given**: Quản trị viên cập nhật System Prompt thành công.
  - **When**: Phiên bản mới được tạo trong hệ thống.
  - **Then**: Hệ thống ghi nhận đầy đủ định danh người chỉnh sửa (Admin ID/Tên) và mốc thời gian cập nhật chính xác.

- **AC-003 — Chặn lưu khi nội dung trống**:
  - **Given**: Quản trị viên đang chỉnh sửa System Prompt.
  - **When**: Nội dung bị xóa trống hoàn toàn và nhấn "Lưu".
  - **Then**: Hệ thống không lưu và hiển thị thông báo yêu cầu nhập nội dung Prompt.

- **AC-005 — Cảnh báo khi thoát chưa lưu (ALT-01)**:
  - **Given**: Quản trị viên đã chỉnh sửa nội dung System Prompt nhưng chưa bấm "Lưu".
  - **When**: Quản trị viên thao tác thoát khỏi màn hình (bấm Hủy hoặc điều hướng rời trang).
  - **Then**: Hệ thống hiển thị modal xác nhận thoát và cảnh báo các thay đổi chưa lưu sẽ bị mất.

- **AC-006 — Bấm lưu khi không có thay đổi (ALT-02)**:
  - **Given**: Quản trị viên không thay đổi bất kỳ ký tự nào của nội dung System Prompt.
  - **When**: Quản trị viên bấm "Lưu".
  - **Then**: Hệ thống không tạo phiên bản mới.
  - **And**: Hiển thị thông báo: *"Không có thay đổi để lưu"*.

- **AC-008 — Lưu phiên bản mới thành công và toàn vẹn**:
  - **Given**: Nội dung chỉnh sửa hợp lệ và có sự thay đổi so với phiên bản hiện tại.
  - **When**: Quản trị viên xác nhận "Lưu".
  - **Then**: Hệ thống tạo một phiên bản mới với nội dung vừa chỉnh sửa.
  - **And**: Phiên bản trước đó được lưu giữ nguyên vẹn trong lịch sử phiên bản.
  - **And**: Phiên bản mới được đánh dấu là phiên bản kích hoạt (Active) phục vụ các tác vụ AI.
  - **And**: Hệ thống ghi nhận người chỉnh sửa và thời gian chỉnh sửa.

- **AC-009 — Xử lý lỗi khi lưu thất bại (EXC-02)**:
  - **Given**: Quản trị viên đang lưu nội dung hợp lệ.
  - **When**: Quá trình tạo phiên bản mới xảy ra lỗi hệ thống hoặc mất kết nối mạng.
  - **Then**: Hệ thống rollback toàn bộ giao dịch, không tạo phiên bản dở dang.
  - **And**: Phiên bản hiện tại và lịch sử phiên bản được giữ nguyên không thay đổi.
  - **And**: Nội dung đang chỉnh sửa được giữ lại trên form để Quản trị viên có thể thử lại ngay.

- **AC-010 — Xử lý lựa chọn trong modal xác nhận thoát (ALT-03)**:
  - **Given**: Quản trị viên đang có nội dung chỉnh sửa chưa lưu và modal xác nhận thoát đang mở.
  - **When**: Quản trị viên chọn "Tiếp tục chỉnh sửa".
  - **Then**: Modal đóng lại và giữ nguyên màn hình cùng nội dung đang nhập.
  - **When**: Quản trị viên chọn "Thoát không lưu".
  - **Then**: Hệ thống hủy bỏ các thay đổi và quay về màn hình chi tiết của phiên bản hiện tại.

- **AC-011 — Xử lý xung đột phiên bản (EXC-04)**:
  - **Given**: Quản trị viên A đang chỉnh sửa System Prompt dựa trên phiên bản hiện tại; trong lúc đó, Quản trị viên B đã lưu thành công một phiên bản mới hơn.
  - **When**: Quản trị viên A chọn "Lưu" nội dung đang chỉnh sửa.
  - **Then**: Hệ thống phát hiện xung đột và từ chối lưu nội dung của Quản trị viên A.
  - **And**: Không ghi đè lên phiên bản mới nhất do Quản trị viên B vừa tạo.
  - **And**: Hiển thị thông báo lỗi: *"System Prompt đã được người khác cập nhật. Vui lòng tải lại phiên bản mới nhất."*

---

## References

### Business Rules
- [BR-041](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/7fcca618-4169-41e1-9191-35ad76d245d6)
- [BR-042](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8e47c852-d79c-4345-946a-e3fd36e9ab53)
- `BR-043`
- `BR-044`
- [BR-045](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/aa2f2bab-03a7-4455-a2e5-c638749cb6f4)
- [BR-052](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0bc478ae-bb1f-4f44-ba13-46f161d807de)

---

## Non-Functional
- **Tốc độ phản hồi**: Thao tác mở form và lưu System Prompt phải phản hồi dưới 2 giây trong điều kiện bình thường.
- **Quản lý phiên bản**: Hệ thống bắt buộc lưu lịch sử tất cả các phiên bản trước đó kèm người thao tác và thời gian thao tác; tuyệt đối không được ghi đè hoặc làm mất dữ liệu của bất kỳ phiên bản nào.

---

## Out of Scope
- Không bao gồm tạo mới hoặc xóa vĩnh viễn System Prompt.
- Không bao gồm tính năng tự động gợi ý/tối ưu Prompt bằng AI.
