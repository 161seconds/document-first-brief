# STORY-017: Tự động viết content và hashtag

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn hệ thống tự động viết nội dung và hashtag từ thông tin đầu vào để tạo bài đăng nhanh chóng và nhất quán
- **Context**: Admin cần tạo nội dung đăng bài phù hợp với chủ đề và nền tảng. Hệ thống sử dụng AI để sinh nội dung và hashtag từ thông tin đầu vào, cho phép Admin xem lại, chỉnh sửa và yêu cầu tạo lại trước khi sử dụng. Chức năng này chỉ bao gồm sinh nội dung và hashtag, không bao gồm tạo ảnh, lên lịch hoặc đăng bài.
- **Quy định dữ liệu đầu vào**:
  - **Chủ đề (bắt buộc)**: Nhập chủ đề chính và thông tin bổ sung để AI hiểu rõ nội dung cần viết. Nội dung nhập phải có từ 5 đến 1.000 ký tự sau khi loại bỏ khoảng trắng ở đầu và cuối (BR-015).
  - **Mục tiêu (bắt buộc)**: Bắt buộc chọn một trong các mục tiêu: *Giới thiệu sản phẩm*, *Khuyến mãi*, *Tăng nhận diện*, *Kêu gọi tương tác*, *Thông báo*, hoặc *Khác*. Nếu chọn *Khác*, phải nhập mô tả tối đa 200 ký tự (BR-055).
  - **Đối tượng (bắt buộc)**: Nhập mô tả đối tượng mục tiêu, độ dài 5–300 ký tự (BR-016).
  - **Nền tảng (bắt buộc)**: Bắt buộc chọn đúng một nền tảng đang được hỗ trợ và kết nối (Facebook, Instagram, Zalo OA,...). Việc tạo đồng thời cho nhiều nền tảng thuộc phạm vi STORY-024 (BR-056).
  - **Giọng văn (bắt buộc)**: Bắt buộc chọn một trong các giọng văn: *Chuyên nghiệp*, *Thân thiện*, *Truyền cảm hứng*, *Ngắn gọn*, *Vui vẻ*, hoặc *Khác*. Nếu chọn *Khác*, phải nhập mô tả tối đa 200 ký tự (BR-055).
  - **Ghi chú (không bắt buộc)**: Có placeholder ghi chú là những thông tin thêm dành cho AI để xử lý.
- **Quy tắc Hashtag**:
  - Số lượng hashtag: tối thiểu 1, tối đa 30 hashtag (BR-017).
  - Mỗi hashtag bắt buộc phải bắt đầu bằng ký tự `#` (BR-018).
  - Sau dấu `#` tuyệt đối không được có khoảng trắng (BR-019).
  - Độ dài: Mỗi hashtag có độ dài từ 2 đến 50 ký tự (không tính dấu `#`) (BR-020).
  - Ký tự hợp lệ: Chỉ được chứa chữ cái, chữ số và dấu gạch dưới `_`. Không cho phép ký tự đặc biệt khác như `! @ $ % & *` (BR-021).
  - Ngôn ngữ: Cho phép hashtag bằng tiếng Việt có dấu, tiếng Việt không dấu hoặc tiếng Anh.
  - Tự động loại bỏ hashtag trùng lặp (không phân biệt chữ hoa/chữ thường) (BR-022).
- **Cơ chế Phiên sinh Content (Content Generation Session)**:
  - Mỗi lần Quản trị viên yêu cầu tạo Content được ghi nhận là một phiên sinh Content.
  - Một phiên có thể chứa nhiều kết quả do AI tạo, bao gồm kết quả ban đầu và các kết quả được tạo lại.
  - Trong thời gian làm việc tại màn hình tạo Content, hệ thống hiển thị toàn bộ kết quả thuộc phiên để Quản trị viên lựa chọn.
  - Chỉ kết quả được Quản trị viên chọn "Lưu content" mới xuất hiện trong danh sách Content đã lưu.
  - Khi Quản trị viên rời màn hình, các kết quả không được chọn không còn hiển thị trên giao diện nhưng vẫn được lưu trong cơ sở dữ liệu và liên kết với phiên sinh Content tương ứng để phục vụ lịch sử.
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
  > *"Cần có thêm định nghĩa cho phần chủ đề trong context. Cái này thuộc SP S1. Cần có thêm quy tắc cho hashtag như số lượng, định dạng (có/không dấu #), độ dài, ngôn ngữ, khử trùng lặp. Chưa có AC cho Alt - 02"* — Nguyễn Đức Bình · 09:54 03/09/2026
  - **Phản hồi & Cập nhật**:
    1. **Định nghĩa Chủ đề trong Context**: Đã bổ sung rõ ràng tại Context: độ dài từ 5 đến 1.000 ký tự sau khi trim khoảng trắng (BR-015).
    2. **Xác nhận Sprint**: Đã kiểm tra và giữ Sprint S1.
    3. **Quy tắc Hashtag đầy đủ**: Đã bổ sung chi tiết: 1–30 tag (BR-017), bắt đầu bằng `#` (BR-018), không khoảng trắng (BR-019), độ dài 2–50 ký tự (BR-020), ký tự hợp lệ chữ/số/gạch dưới (BR-021), ngôn ngữ Việt/Anh, tự động khử trùng lặp (BR-022).
    4. **Acceptance Criteria cho ALT-02**: Đã có **AC-004 — Chỉnh sửa kết quả AI trước khi lưu (ALT-02)**. Đồng thời bổ sung đầy đủ các AC về phiên sinh Content (AC-014 đến AC-017).

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Ít nhất một nền tảng xã hội đang được kết nối hợp lệ.
  - Dịch vụ AI sinh nội dung đang hoạt động bình thường.
- **Trigger**:
  - Quản trị viên chọn chức năng “Tự động viết content và hashtag”.

---

## Flow

### Main Flow — Tự động viết content và hashtag
1. Quản trị viên mở chức năng “Tự động viết content và hashtag”.
2. Hệ thống hiển thị biểu mẫu nhập thông tin gồm: Chủ đề, Mục tiêu, Đối tượng, Nền tảng và Giọng văn (bắt buộc); Ghi chú (không bắt buộc).
3. Quản trị viên nhập thông tin đầy đủ, hợp lệ và nhấn chọn “Tạo nội dung”.
4. Hệ thống kiểm tra tính hợp lệ của dữ liệu đầu vào theo quy định (BR-015, BR-016, BR-055, BR-056).
5. Hệ thống gửi yêu cầu sinh nội dung cùng các tham số đầu vào đến dịch vụ AI.
6. AI sinh một hoặc nhiều kết quả Content và danh sách hashtag phù hợp theo quy tắc (BR-017 $\rightarrow$ BR-022), đồng thời tạo phiên sinh Content cho yêu cầu hiện tại.
7. Hệ thống lưu tất cả kết quả AI tạo vào phiên sinh Content và hiển thị toàn bộ kết quả thuộc phiên hiện tại để Quản trị viên lựa chọn.
8. Quản trị viên xem, chỉnh sửa và chọn một kết quả muốn sử dụng.
9. Quản trị viên chọn “Lưu content”; hệ thống lưu kết quả được chọn thành một Content riêng trong danh sách Content đã lưu (BR-058) và tiếp tục hiển thị các kết quả còn lại trong cùng phiên để Quản trị viên có thể lưu tiếp.
10. Hệ thống hiển thị thông báo lưu thành công.

### Alternative Flow
- **ALT-01 — Tạo lại kết quả (Regenerate)**:
  - Sau khi hệ thống hiển thị kết quả của phiên, nếu Quản trị viên chưa hài lòng, chọn “Tạo lại”.
  - Hệ thống gửi lại yêu cầu với cùng dữ liệu đầu vào.
  - Kết quả cũ không bị ghi đè hoặc xóa khỏi lịch sử phiên; hệ thống bổ sung kết quả mới vào cùng phiên sinh Content để Quản trị viên lựa chọn.
- **ALT-02 — Chỉnh sửa kết quả do AI tạo trước khi lưu**:
  - Tại màn hình xem kết quả, Quản trị viên trực tiếp chỉnh sửa nội dung văn bản hoặc thêm/bớt/sửa hashtag do AI tạo ra.
  - Hệ thống giữ lại các thay đổi của Quản trị viên trên giao diện và sẵn sàng lưu khi bấm "Lưu content".
- **ALT-03 — Thay đổi đầu vào và tạo lại kết quả**:
  - Khi đang có kết quả AI, Quản trị viên thay đổi một hoặc nhiều thông tin đầu vào ở biểu mẫu.
  - Hệ thống xác định kết quả đang hiển thị không còn tương ứng với dữ liệu mới và hiển thị thông báo: *"Đầu vào đã bị thay đổi so với ban đầu"* kèm nút “Tạo lại”.
  - Quản trị viên có thể chọn “Tạo nội dung” / “Tạo lại” để gửi yêu cầu mới với đầu vào cập nhật, hoặc chọn “Lưu content” để lưu kết quả hiện tại.
- **ALT-04 — Thoát khi có kết quả chưa sử dụng**:
  - Quản trị viên đã nhập dữ liệu hoặc có kết quả AI nhưng chưa nhấn “Lưu content”.
  - Quản trị viên đóng màn hình hoặc yêu cầu quay lại.
  - Hệ thống hiển thị cảnh báo nội dung chưa được lưu.
  - Nếu Quản trị viên chọn tiếp tục, hệ thống giữ màn hình, dữ liệu đầu vào và kết quả đang hiển thị.
  - Nếu Quản trị viên chọn thoát không lưu, các kết quả chưa được chọn không còn hiển thị trên giao diện và không được thêm vào danh sách Content đã lưu; hệ thống vẫn giữ các kết quả này trong cơ sở dữ liệu gắn với phiên để phục vụ lịch sử.
- **ALT-05 — Tiếp tục lưu kết quả khác trong cùng phiên**:
  - Quản trị viên đã chọn “Lưu content” và lưu thành công một kết quả.
  - Hệ thống đánh dấu kết quả đó là đã được sử dụng và tiếp tục hiển thị các kết quả còn lại trong cùng phiên sinh Content.
  - Quản trị viên có thể chọn “Lưu content” tại một kết quả khác.
  - Hệ thống lưu kết quả được chọn thành một Content riêng trong danh sách Content đã lưu.
  - Các Content đã lưu trước đó không bị ghi đè hoặc thay đổi; Quản trị viên có thể tiếp tục lưu các kết quả khác cho đến khi rời màn hình.

### Exception Flow
- **EXC-01 — Dữ liệu đầu vào không hợp lệ**:
  - Tại bước 4, nếu thiếu trường bắt buộc hoặc dữ liệu vi phạm giới hạn độ dài/định dạng.
  - Hệ thống chặn gửi yêu cầu đến AI, đánh dấu trường không hợp lệ và hiển thị lý do chi tiết.
  - Hệ thống giữ lại toàn bộ dữ liệu hợp lệ đã nhập để Quản trị viên chỉnh sửa và thử lại.
- **EXC-02 — AI không phản hồi hoặc timeout**:
  - Tại bước 5, nếu AI không phản hồi trong thời gian quy định (60 giây) hoặc gặp lỗi kết nối.
  - Hệ thống dừng xử lý, hiển thị thông báo: *"Không thể tạo nội dung, vui lòng thử lại"* kèm nút "Thử lại".
  - Giữ nguyên toàn bộ dữ liệu đầu vào đã nhập, không lưu nội dung hoặc hashtag rác.
- **EXC-03 — Không thể lưu nội dung vào CSDL**:
  - Tại bước 9, quá trình lưu nội dung hoặc hashtag xảy ra lỗi.
  - Hệ thống xử lý việc lưu nội dung và hashtag như một giao dịch thống nhất (Atomicity), không lưu một phần dữ liệu.
  - Giữ lại kết quả AI và các chỉnh sửa trên màn hình để Quản trị viên thử lại.
- **EXC-04 — Kết quả AI không đúng định dạng quy chuẩn**:
  - AI trả về dữ liệu rỗng, thiếu hashtag hoặc sai cấu trúc yêu cầu (BR-057).
  - Hệ thống không hiển thị kết quả đó như một kết quả hợp lệ, giữ nguyên dữ liệu đầu vào và cho phép Quản trị viên thử lại.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi Quản trị viên nhấn tạo lại hoặc lưu nội dung.
  - Hệ thống từ chối yêu cầu, thông báo phiên đăng nhập đã hết hạn và chuyển hướng đăng nhập lại.

---

## Acceptance Criteria

- **AC-001 — Hiển thị biểu mẫu nhập thông tin**:
  - **Given**: Quản trị viên đã đăng nhập vào hệ thống.
  - **When**: Quản trị viên mở chức năng "Tự động viết content và hashtag".
  - **Then**: Hệ thống hiển thị biểu mẫu nhập thông tin gồm: Chủ đề (5–1.000 ký tự), Mục tiêu, Đối tượng (5–300 ký tự), Nền tảng và Giọng văn (bắt buộc); Ghi chú (tùy chọn).
  - **And**: Nút "Tạo nội dung" chỉ khả dụng khi đã nhập đủ và đúng các trường bắt buộc.

- **AC-002 — Sinh nội dung và hashtag thành công**:
  - **Given**: Quản trị viên đã nhập đầy đủ dữ liệu hợp lệ vào form.
  - **When**: Quản trị viên nhấn "Tạo nội dung".
  - **Then**: Hệ thống sinh nội dung và danh sách hashtag phù hợp với thông tin đầu vào.
  - **And**: Danh sách hashtag tuân thủ đúng: bắt đầu bằng `#`, 2–50 ký tự, không chứa khoảng trắng, không trùng lặp, số lượng từ 1 đến 30 (BR-017 $\rightarrow$ BR-022).
  - **And**: Hệ thống hiển thị kết quả để Quản trị viên xem lại.

- **AC-003 — Tạo lại kết quả với cùng dữ liệu đầu vào (ALT-01)**:
  - **Given**: Kết quả AI đang hiển thị trên màn hình.
  - **When**: Quản trị viên chọn "Tạo lại".
  - **Then**: Hệ thống tạo kết quả mới với cùng dữ liệu đầu vào và lưu vào cùng phiên sinh Content.
  - **And**: Hệ thống giữ nguyên thông tin Quản trị viên đã nhập và không ghi đè kết quả trước đó.

- **AC-004 — Chỉnh sửa kết quả AI trước khi lưu (ALT-02)**:
  - **Given**: Kết quả AI đang hiển thị trên màn hình.
  - **When**: Quản trị viên chỉnh sửa nội dung hoặc danh sách hashtag và chọn "Lưu content".
  - **Then**: Hệ thống giữ nguyên các thay đổi của Quản trị viên.
  - **And**: Hệ thống tạo một bản ghi Content đã lưu gồm nội dung và hashtag đúng theo những gì Quản trị viên đã chỉnh sửa.

- **AC-005 — Xử lý dữ liệu đầu vào không hợp lệ (EXC-01)**:
  - **Given**: Quản trị viên đã nhập thông tin nhưng dữ liệu không đáp ứng các điều kiện hợp lệ.
  - **When**: Quản trị viên nhấn "Tạo nội dung".
  - **Then**: Hệ thống không tạo content, không gửi yêu cầu đến AI và hiển thị thông báo lỗi chi tiết tại các trường tương ứng.
  - **And**: Hệ thống giữ nguyên dữ liệu Quản trị viên đã nhập để chỉnh sửa lại.

- **AC-006 — Xử lý khi AI không phản hồi hoặc timeout (EXC-02)**:
  - **Given**: Quản trị viên đã gửi yêu cầu tạo nội dung hợp lệ.
  - **When**: AI không phản hồi sau 60 giây hoặc trả về lỗi kết nối.
  - **Then**: Hệ thống không tạo content và hiển thị thông báo lỗi phù hợp, cho phép Quản trị viên bấm thử lại.
  - **And**: Giữ nguyên dữ liệu đầu vào và không lưu bất kỳ bản ghi rác nào.

- **AC-007 — Lưu content và hashtag thành công**:
  - **Given**: Hệ thống đã tạo content thành công và hiển thị kết quả cho Quản trị viên.
  - **When**: Quản trị viên thực hiện thao tác "Lưu content".
  - **Then**: Hệ thống lưu content thành công và hiển thị thông báo xác nhận.
  - **And**: Nội dung và hashtag được lưu đầy đủ trong cơ sở dữ liệu (BR-058).
  - **And**: Hệ thống không tự động đăng bài hoặc lên lịch đăng.

- **AC-008 — Thay đổi dữ liệu đầu vào khi đang có kết quả (ALT-03)**:
  - **Given**: Kết quả AI đang hiển thị trên màn hình.
  - **When**: Quản trị viên thay đổi một hoặc nhiều thông tin đầu vào.
  - **Then**: Hệ thống xác định kết quả hiện tại không còn tương ứng với dữ liệu mới và hiển thị cảnh báo đầu vào đã thay đổi.
  - **And**: Quản trị viên có thể bấm "Tạo lại" để sinh kết quả mới theo đầu vào mới, hoặc bấm "Lưu content" để lưu kết quả hiện tại.

- **AC-009 — Cảnh báo khi thoát mà chưa lưu kết quả (ALT-04)**:
  - **Given**: Quản trị viên đã nhập dữ liệu hoặc có kết quả AI nhưng chưa chọn "Lưu content".
  - **When**: Quản trị viên yêu cầu đóng hoặc rời màn hình.
  - **Then**: Hệ thống hiển thị hộp thoại xác nhận thoát.
  - **And**: Chọn tiếp tục phải giữ nguyên dữ liệu đầu vào và kết quả đang hiển thị.
  - **And**: Chọn thoát không lưu sẽ không thêm kết quả vào danh sách Content đã lưu.

- **AC-010 — Xử lý kết quả AI không đúng định dạng quy chuẩn (EXC-04)**:
  - **Given**: Quản trị viên đã nhập dữ liệu hợp lệ và AI có phản hồi.
  - **When**: Kết quả không có nội dung, không có hashtag hoặc không đúng cấu trúc yêu cầu (BR-057).
  - **Then**: Hệ thống không hiển thị kết quả lỗi đó, giữ nguyên dữ liệu đầu vào và cho phép Quản trị viên thử lại.

- **AC-011 — Đảm bảo tính toàn vẹn khi lưu CSDL (EXC-03)**:
  - **Given**: Quản trị viên đã chọn "Lưu content" với một kết quả hợp lệ.
  - **When**: Quá trình lưu nội dung hoặc hashtag xảy ra lỗi kỹ thuật.
  - **Then**: Hệ thống không ghi nhận lưu thành công, không lưu dữ liệu một phần (rollback).
  - **And**: Kết quả và các chỉnh sửa của Quản trị viên được giữ lại trên form để thử lại.

- **AC-012 — Xử lý hết hạn phiên làm việc (EXC-05)**:
  - **Given**: Quản trị viên đang ở màn hình tạo nội dung.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên bấm tạo lại hoặc lưu nội dung.
  - **Then**: Hệ thống từ chối yêu cầu, thông báo phiên đăng nhập đã hết hạn và yêu cầu đăng nhập lại.

- **AC-013 — Lưu toàn bộ kết quả vào phiên sinh Content**:
  - **Given**: Quản trị viên đang thực hiện một phiên tạo Content.
  - **When**: AI trả về một hoặc nhiều kết quả, bao gồm kết quả từ các lần tạo lại.
  - **Then**: Hệ thống lưu tất cả kết quả vào cùng phiên sinh Content và hiển thị toàn bộ kết quả thuộc phiên hiện tại để Quản trị viên lựa chọn.
  - **And**: Kết quả mới không ghi đè hoặc xóa kết quả đã tạo trước đó trong lịch sử phiên.

- **AC-014 — Chỉ lưu kết quả được chọn vào danh sách Content đã lưu**:
  - **Given**: Phiên tạo Content có nhiều kết quả hợp lệ.
  - **When**: Quản trị viên chọn "Lưu content" tại một kết quả.
  - **Then**: Chỉ kết quả được chọn được thêm vào danh sách Content đã lưu; các kết quả còn lại không xuất hiện trong danh sách đó nhưng vẫn được giữ trong lịch sử phiên.

- **AC-015 — Rời màn hình nhưng vẫn giữ lịch sử phiên**:
  - **Given**: Phiên tạo Content có các kết quả chưa được Quản trị viên chọn lưu.
  - **When**: Quản trị viên rời màn hình tạo Content.
  - **Then**: Các kết quả chưa chọn không còn hiển thị trên giao diện và không được thêm vào danh sách Content đã lưu, nhưng vẫn được lưu trong cơ sở dữ liệu gắn với phiên sinh Content tương ứng.

- **AC-016 — Tiếp tục lưu kết quả khác trong cùng phiên sinh Content (ALT-05)**:
  - **Given**: Phiên sinh Content có nhiều kết quả hợp lệ và Quản trị viên đã lưu thành công một kết quả.
  - **When**: Quản trị viên chọn "Lưu content" tại một kết quả khác trong cùng phiên.
  - **Then**: Hệ thống lưu kết quả đó thành một Content riêng biệt.
  - **And**: Content đã lưu trước đó không bị ghi đè; kết quả vừa lưu được đánh dấu đã sử dụng; các kết quả còn lại vẫn hiển thị để tiếp tục lựa chọn; mỗi kết quả chỉ được lưu một lần trong cùng phiên.

---

## References

### Business Rules
- [BR-015: Độ dài Chủ đề (Topic) khi sinh content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-015.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/bd997782-4580-4519-b96d-91ef12536a20))
- [BR-016: Độ dài Đối tượng (Target Audience)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-016.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/87f9f8f0-515f-48c1-8f46-4aa57ffef4bf))
- [BR-017: Giới hạn số lượng hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-017.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f2b1c31e-65cf-4fe1-be04-b4fe035b0779))
- [BR-018: Định dạng bắt đầu của Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-018.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8b93f9ae-a327-48be-8e1d-78f40bdd9c23))
- [BR-019: Không chứa khoảng trắng trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-019.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/2dd59a4b-f4ed-489b-8788-ab3684cd7da4))
- [BR-020: Độ dài cho phép của một Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-020.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8c0f117b-25fe-4070-9e3f-682a955ddd4c))
- [BR-021: Ký tự hợp lệ trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-021.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/af6d57ef-0350-4009-a454-7f8f04f37eaf))
- [BR-022: Loại bỏ Hashtag trùng lặp](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-022.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1b8d95f0-954e-48d3-8a45-fee5ddcfe830))
- [BR-055: Giá trị Mục tiêu và Giọng văn khi tạo Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-055.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/960037a7-033a-4ea1-8ab1-3cf6820e5e7b))
- [BR-056: Chỉ tạo Content cho một nền tảng hợp lệ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-056.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8d6b4823-8fd0-4af2-a63e-e13ad7e5844c))
- [BR-057: Kết quả AI hợp lệ trước khi sử dụng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-057.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/918f53c2-dd63-4962-98d7-b4383ae5cf2e))
- [BR-058: Lưu toàn vẹn Content và hashtag do AI tạo](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-058.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/6a28f2d2-9f3e-44e4-9d4a-8855cd933f99))

---

## Non-Functional
- **Thời gian phản hồi AI**: Thời gian tối đa cho một lượt gọi API sinh nội dung là 60 giây.
- **Tính toàn vẹn dữ liệu**: Quá trình lưu nội dung và hashtag phải đảm bảo tính nguyên tử (Atomicity); không được lưu một phần dữ liệu dở dang.
- **Bảo mật và kiểm soát**: Hệ thống không tự động lưu vào CSDL hoặc tự động đăng bài trước khi Quản trị viên bấm xác nhận "Lưu content".

---

## Out of Scope
- Không bao gồm tạo hoặc chỉnh sửa hình ảnh đính kèm (thuộc STORY-003, STORY-021, STORY-023).
- Không bao gồm lên lịch hoặc tự động đăng bài lên mạng xã hội (thuộc STORY-002, STORY-016).
- Không bao gồm viết lại nội dung cho nhiều nền tảng cùng lúc (thuộc STORY-024).
- Không bao gồm quản lý cấu hình sâu hoặc huấn luyện mô hình AI.
