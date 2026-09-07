# STORY-024: Tự động viết lại nội dung theo từng nền tảng

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chọn một nội dung gốc đã lưu và một hoặc nhiều nền tảng được chọn để hệ thống sử dụng trí tuệ nhân tạo viết lại nội dung và đề xuất hashtag phù hợp cho từng nền tảng, giúp tôi tạo nhanh các nội dung mới mà vẫn giữ được thông điệp chính của nội dung gốc.
- **Context**: Một nội dung marketing có thể được sử dụng trên nhiều nền tảng như Facebook, Instagram hoặc Zalo Official Account (Zalo OA). Tuy nhiên, cách diễn đạt, độ dài, cấu trúc và hashtag phù hợp có thể khác nhau giữa các nền tảng. Chức năng này cho phép Quản trị viên chọn một nội dung (content) gốc đã lưu (hoặc đưa vào nội dung ngoài), chọn một hoặc nhiều nền tảng mục tiêu và yêu cầu AI viết lại nội dung cùng đề xuất hashtag tối ưu riêng cho từng nền tảng. Quản trị viên có thể xem, chỉnh sửa và lưu độc lập từng kết quả thành một content mới có ID riêng. Content mới kế thừa tiêu đề từ content gốc, lưu nền tảng được chọn, nội dung, danh sách hashtag và lưu ID của content gốc (`parent_content_id`) để duy trì mối liên kết nguồn. Content gốc tuyệt đối không bị ghi đè hoặc thay đổi.
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
  > *"Chưa có AC cho EXC - 06, 02"* — Nguyễn Đức Bình · 10:16 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Content gốc đã được lưu và vẫn còn tồn tại trong hệ thống (hoặc văn bản gốc được nhập hợp lệ).
  - Hệ thống đã cấu hình ít nhất một nền tảng mạng xã hội được hỗ trợ.
- **Trigger**:
  - Quản trị viên mở một content đã lưu và chọn chức năng “Viết lại theo nền tảng”.

---

## Flow

### Main Flow — Tự động viết lại nội dung theo từng nền tảng
1. Quản trị viên chọn content gốc đã lưu (hoặc đưa content gốc từ bên ngoài vào hệ thống) và nhấn chọn “Viết lại theo nền tảng”.
2. Hệ thống tải và hiển thị: Tiêu đề, nội dung đầy đủ, danh sách hashtag của content gốc cùng danh sách các nền tảng được hỗ trợ (Facebook, Instagram, Zalo OA,...).
3. Quản trị viên kiểm tra nội dung gốc và tích chọn một hoặc nhiều nền tảng muốn viết lại.
4. Quản trị viên chọn “Tạo nội dung”.
5. Hệ thống gửi các yêu cầu xử lý độc lập đến dịch vụ AI cho từng nền tảng đã chọn, kết hợp nội dung gốc với bộ quy tắc/văn phong đặc thù của từng nền tảng.
6. Dịch vụ AI trả về nội dung đã viết lại cùng danh sách hashtag đề xuất riêng cho từng nền tảng.
7. Hệ thống kiểm tra kết quả và hiển thị trực quan từng kết quả trong các thẻ/khu vực riêng biệt tương ứng với từng nền tảng, kèm trạng thái xử lý.
8. Quản trị viên xem xét và có thể chỉnh sửa trực tiếp nội dung văn bản hoặc danh sách hashtag của từng kết quả.
9. Quản trị viên chọn nút “Lưu” tại kết quả của nền tảng muốn lưu.
10. Hệ thống kiểm tra tính hợp lệ của kết quả và trạng thái của content gốc tại thời điểm lưu.
11. Hệ thống tạo một bản ghi content mới có ID riêng, sao chép tiêu đề của content gốc, lưu nền tảng được chọn, nội dung, danh sách hashtag và lưu ID của content gốc (`parent_content_id`).
12. Hệ thống ghi nhận tài khoản người tạo và thời gian tạo content mới.
13. Hệ thống hiển thị thông báo lưu thành công; content gốc và các kết quả của các nền tảng khác được giữ nguyên vẹn.

### Alternative Flow
- **ALT-01 — Viết lại nội dung cho một nền tảng duy nhất**:
  - Tại bước 3, Quản trị viên chỉ chọn đúng 1 nền tảng.
  - Hệ thống chỉ gửi một yêu cầu tới AI và hiển thị một kết quả duy nhất để Quản trị viên xem, chỉnh sửa và lưu.
- **ALT-02 — Viết lại nội dung cho nhiều nền tảng đồng thời**:
  - Tại bước 3, Quản trị viên chọn từ hai nền tảng trở lên.
  - Hệ thống gửi và xử lý các yêu cầu độc lập song song cho từng nền tảng.
  - Hiển thị từng kết quả trong các tab hoặc cột riêng biệt. Quản trị viên có thể chỉnh sửa và lưu từng kết quả theo bất kỳ thứ tự nào mà không ảnh hưởng lẫn nhau.
- **ALT-03 — Tạo lại nội dung cho một nền tảng cụ thể (Regenerate per Platform)**:
  - Sau khi xem kết quả, Quản trị viên nhấn nút “Tạo lại” tại riêng một nền tảng chưa ưng ý.
  - Hệ thống chỉ gửi lại yêu cầu sinh nội dung cho riêng nền tảng đó.
  - Kết quả mới thay thế cho kết quả cũ của nền tảng đó; kết quả của các nền tảng khác và content gốc được giữ nguyên vẹn.
- **ALT-04 — Thoát khi còn kết quả chưa lưu**:
  - Quản trị viên thao tác đóng hoặc rời màn hình khi vẫn còn kết quả AI chưa được nhấn "Lưu".
  - Hệ thống hiển thị cảnh báo: *"Có kết quả chưa được lưu. Bạn có chắc chắn muốn rời đi?"*.
  - Nếu chọn "Tiếp tục chỉnh sửa", giữ nguyên màn hình và toàn bộ kết quả đang hiển thị.
  - Nếu chọn "Thoát không lưu", loại bỏ các kết quả tạm thời chưa lưu, giữ nguyên content gốc và các content đã lưu trước đó.

### Exception Flow
- **EXC-01 — Content gốc không còn tồn tại**:
  - Content gốc đã bị xóa bởi thao tác khác trước khi AI xử lý hoặc trước khi bấm lưu.
  - Hệ thống dừng thao tác, không gửi yêu cầu đến AI hoặc không lưu content mới, thông báo: *"Content gốc không còn tồn tại trong hệ thống"* và cho phép quay lại danh sách.
- **EXC-02 — Dữ liệu đầu vào không hợp lệ (Chưa chọn nền tảng)**:
  - Quản trị viên chưa tích chọn bất kỳ nền tảng nào nhưng bấm “Tạo nội dung”.
  - Hệ thống chặn gửi yêu cầu đến AI, hiển thị thông báo lỗi: *"Vui lòng chọn ít nhất một nền tảng để viết lại nội dung"* và giữ nguyên giao diện.
- **EXC-03 — Không thể tạo bất kỳ kết quả nào (AI toàn bộ thất bại)**:
  - Dịch vụ AI timeout hoặc gặp sự cố với toàn bộ các nền tảng đã chọn.
  - Hệ thống hiển thị thông báo: *"Không thể tạo nội dung, vui lòng thử lại"*, không lưu dữ liệu rác và cho phép Quản trị viên nhấn thử lại.
- **EXC-04 — Một phần nền tảng tạo nội dung thất bại (Partial Failure)**:
  - Trong số các nền tảng đã chọn, có nền tảng thành công và có nền tảng bị lỗi kết nối AI.
  - Hệ thống hiển thị kết quả thành công cho các nền tảng đạt yêu cầu, và hiển thị trạng thái lỗi kèm nút "Thử lại" tại riêng các nền tảng thất bại.
  - Quản trị viên vẫn được phép lưu các kết quả thành công mà không bị gián đoạn.
- **EXC-05 — Kết quả do AI sinh không hợp lệ**:
  - Kết quả AI trả về bị rỗng nội dung hoặc vi phạm quy tắc độ dài/hashtag của nền tảng đó.
  - Hệ thống đánh dấu kết quả nền tảng đó không hợp lệ, vô hiệu hóa nút "Lưu" của nền tảng đó và cho phép Quản trị viên bấm "Tạo lại".
- **EXC-06 — Lỗi hệ thống khi lưu content mới vào CSDL**:
  - Quá trình lưu bản ghi content mới vào CSDL gặp lỗi mạng hoặc sự cố máy chủ.
  - Hệ thống rollback giao dịch, không tạo bản ghi dở dang, hiển thị thông báo: *"Không thể lưu content mới, vui lòng thử lại"*.
  - Giữ nguyên toàn bộ kết quả và nội dung chỉnh sửa của Quản trị viên trên màn hình để thử lưu lại.
- **EXC-07 — Content gốc bị thay đổi trong lúc xử lý**:
  - Content gốc bị cập nhật bởi người dùng khác trong lúc Quản trị viên đang viết lại.
  - Hệ thống cảnh báo content gốc đã bị cập nhật, ngăn chặn lưu tự động để tránh mất đồng bộ và yêu cầu Quản trị viên đối chiếu.
- **EXC-08 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi Quản trị viên bấm tạo hoặc lưu.
  - Hệ thống từ chối yêu cầu, giữ nguyên dữ liệu trên CSDL và hiển thị thông báo yêu cầu đăng nhập lại.
- **EXC-09 — Không thể tải dữ liệu content gốc**:
  - Lỗi kết nối khiến hệ thống không tải được nội dung gốc hoặc danh sách nền tảng hỗ trợ.
  - Hệ thống không hiển thị màn hình thiếu dữ liệu, thông báo lỗi và cung cấp nút tải lại.

---

## Acceptance Criteria

- **AC-001 — Hiển thị dữ liệu content gốc và danh sách nền tảng**:
  - **Given**: Content gốc còn tồn tại trong hệ thống.
  - **When**: Quản trị viên chọn chức năng “Viết lại theo nền tảng”.
  - **Then**: Hệ thống hiển thị tiêu đề, nội dung, danh sách hashtag của content gốc và danh sách các nền tảng mạng xã hội được hỗ trợ.
  - **And**: Content gốc được hiển thị rõ ràng và chưa bị thay đổi bất kỳ ký tự nào.

- **AC-002 — Bắt buộc chọn ít nhất một nền tảng (EXC-02)**:
  - **Given**: Quản trị viên đang ở màn hình viết lại nhưng chưa tích chọn bất kỳ nền tảng nào.
  - **When**: Quản trị viên chọn “Tạo nội dung”.
  - **Then**: Hệ thống ngăn chặn gửi yêu cầu đến dịch vụ AI.
  - **And**: Hệ thống hiển thị cảnh báo lỗi: *"Vui lòng chọn ít nhất một nền tảng để viết lại nội dung"* và giữ nguyên dữ liệu trên màn hình.

- **AC-003 — Viết lại nội dung cho một nền tảng (ALT-01)**:
  - **Given**: Quản trị viên đã chọn content gốc hợp lệ và tích chọn đúng một nền tảng.
  - **When**: Quản trị viên chọn “Tạo nội dung”.
  - **Then**: Hệ thống gửi yêu cầu và nhận kết quả văn bản viết lại kèm hashtag đề xuất tối ưu cho nền tảng đó.
  - **And**: Hệ thống hiển thị kết quả cho Quản trị viên xem lại và giữ nguyên content gốc.

- **AC-004 — Viết lại nội dung cho nhiều nền tảng đồng thời (ALT-02)**:
  - **Given**: Quản trị viên đã chọn content gốc hợp lệ và tích chọn từ hai nền tảng trở lên.
  - **When**: Quản trị viên chọn “Tạo nội dung”.
  - **Then**: Hệ thống gửi và xử lý các yêu cầu độc lập cho từng nền tảng.
  - **And**: Mỗi kết quả được hiển thị trong khu vực riêng biệt ghi rõ tên nền tảng tương ứng.
  - **And**: Sự cố ở một nền tảng không làm ảnh hưởng đến kết quả của các nền tảng khác.

- **AC-005 — Chỉnh sửa kết quả do AI sinh trước khi lưu**:
  - **Given**: Hệ thống đã tạo kết quả viết lại thành công cho một nền tảng.
  - **When**: Quản trị viên chỉnh sửa nội dung văn bản hoặc danh sách hashtag của kết quả đó.
  - **Then**: Hệ thống lưu tạm các thay đổi của Quản trị viên trên giao diện và sẵn sàng để lưu vào CSDL khi bấm "Lưu".
  - **And**: Thao tác chỉnh sửa kết quả không làm thay đổi content gốc.

- **AC-006 — Lưu content mới thành công**:
  - **Given**: Quản trị viên có một kết quả hợp lệ trên màn hình và content gốc chưa bị thay đổi.
  - **When**: Quản trị viên nhấn nút “Lưu” tại kết quả của nền tảng đó.
  - **Then**: Hệ thống tạo một bản ghi content mới có ID riêng trong CSDL, kế thừa tiêu đề từ content gốc, lưu nền tảng tương ứng, nội dung, hashtag và lưu ID của content gốc (`parent_content_id`).
  - **And**: Hệ thống ghi nhận người tạo, thời gian tạo và hiển thị thông báo lưu thành công.

- **AC-007 — Đảm bảo bất biến cho content gốc**:
  - **Given**: Một hoặc nhiều content mới đã được lưu thành công từ kết quả viết lại.
  - **When**: Quá trình lưu hoàn tất.
  - **Then**: Content gốc ban đầu được giữ nguyên vẹn toàn bộ thuộc tính và không bị ghi đè.

- **AC-008 — Xử lý lỗi một phần khi tạo cho nhiều nền tảng (EXC-04)**:
  - **Given**: Quản trị viên yêu cầu tạo nội dung cho nhiều nền tảng cùng lúc.
  - **When**: Một nền tảng bị lỗi mạng/AI nhưng các nền tảng khác thành công.
  - **Then**: Hệ thống hiển thị kết quả thành công cho các nền tảng đạt và hiển thị thông báo lỗi kèm nút "Thử lại" riêng cho nền tảng thất bại.
  - **And**: Quản trị viên được phép lưu các kết quả thành công mà không bị chặn bởi nền tảng lỗi.

- **AC-009 — Xử lý lỗi khi lưu content mới vào CSDL (EXC-06)**:
  - **Given**: Quản trị viên nhấn nút "Lưu" tại một kết quả hợp lệ.
  - **When**: Quá trình tạo bản ghi content mới vào CSDL phát sinh lỗi hệ thống.
  - **Then**: Hệ thống rollback toàn bộ, không tạo ra bản ghi content dở dang hoặc thiếu liên kết.
  - **And**: Hệ thống hiển thị thông báo: *"Không thể lưu content mới, vui lòng thử lại"* và giữ nguyên kết quả trên màn hình để Quản trị viên thử lại.

- **AC-010 — Tạo lại nội dung cho một nền tảng riêng lẻ (ALT-03)**:
  - **Given**: Đã có kết quả hiển thị cho nhiều nền tảng.
  - **When**: Quản trị viên nhấn “Tạo lại” tại một nền tảng cụ thể.
  - **Then**: Hệ thống chỉ gửi yêu cầu sinh mới cho nền tảng đó và cập nhật kết quả mới khi hoàn thành.
  - **And**: Toàn bộ kết quả của các nền tảng khác được giữ nguyên vẹn.

- **AC-011 — Cảnh báo khi thoát mà còn kết quả chưa lưu (ALT-04)**:
  - **Given**: Còn ít nhất một kết quả AI chưa được Quản trị viên nhấn "Lưu".
  - **When**: Quản trị viên thao tác đóng hoặc chuyển trang.
  - **Then**: Hệ thống hiển thị hộp thoại cảnh báo có nội dung chưa được lưu.
  - **And**: Nếu chọn tiếp tục chỉnh sửa thì giữ nguyên màn hình; nếu chọn thoát thì loại bỏ kết quả chưa lưu mà không làm ảnh hưởng đến CSDL.

- **AC-012 — Xử lý phiên đăng nhập hết hạn (EXC-08)**:
  - **Given**: Quản trị viên đang ở màn hình viết lại nội dung theo nền tảng.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên thực hiện thao tác tạo hoặc lưu.
  - **Then**: Hệ thống từ chối yêu cầu, giữ nguyên dữ liệu CSDL và yêu cầu đăng nhập lại.

---

## References

### Business Rules
- [BR-068: Xử lý viết lại độc lập theo từng nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-068.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/34dcdf09-ae77-4289-a543-4dc358144acc))
- [BR-069: Kết quả viết lại (paraphrase) phải hợp lệ theo nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-069.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/6d7ccf54-c00a-47ac-8188-1f8e34bd84d4))
- [BR-070: Lưu kết quả viết lại thành Content mới](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-070.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/4b698021-f4a6-4a2f-aed8-ce74dfd9d546))
- [BR-071: Lưu toàn vẹn Content viết lại](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-071.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0c1b3d9d-e24c-465c-9d58-12563df8cb90))

---

## Non-Functional
- **Độc lập tiến trình (Process Isolation)**: Việc gọi API và xử lý kết quả cho từng nền tảng phải được tách biệt hoàn toàn; lỗi kết nối ở một nền tảng không được làm nghẽn hoặc hủy tiến trình của nền tảng khác.
- **Tính toàn vẹn liên kết dữ liệu**: Khi lưu content mới, mối quan hệ liên kết với content gốc (`parent_content_id`) phải được đảm bảo nhất quán và có kiểm tra khóa ngoại (Foreign Key integrity).

---

## Out of Scope
- Không bao gồm lên lịch hoặc tự động đăng bài lên các nền tảng (thuộc STORY-002, STORY-016).
- Không bao gồm chỉnh sửa hoặc ghi đè nội dung của bản ghi content gốc.
- Không bao gồm cấu hình quy tắc viết nội dung hoặc System Prompt của từng nền tảng (thuộc STORY-005).
