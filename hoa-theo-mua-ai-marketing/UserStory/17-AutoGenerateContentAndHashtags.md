# STORY-017: Tự động viết content và hashtag

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn hệ thống tự động viết nội dung và hashtag từ thông tin đầu vào để tạo bài đăng nhanh chóng và nhất quán
- **Context**: Admin cần tạo nội dung đăng bài phù hợp với chủ đề và nền tảng. Hệ thống sử dụng AI để sinh nội dung và hashtag từ thông tin đầu vào, cho phép Admin xem lại, chỉnh sửa và yêu cầu tạo lại trước khi sử dụng. Chức năng này chỉ bao gồm sinh nội dung và hashtag, không bao gồm tạo ảnh, lên lịch hoặc đăng bài.
- **Quy định dữ liệu đầu vào**:
  - **Chủ đề (bắt buộc)**: Nhập chủ đề chính và thông tin bổ sung để AI hiểu rõ nội dung cần viết. Nội dung nhập phải có từ 5 đến 1.000 ký tự sau khi loại bỏ khoảng trắng ở đầu và cuối.
  - **Mục tiêu (bắt buộc)**: Bắt buộc chọn một trong các mục tiêu: *Giới thiệu sản phẩm*, *Khuyến mãi*, *Tăng nhận diện*, *Kêu gọi tương tác*, *Thông báo*, hoặc *Khác*. Nếu chọn *Khác*, phải nhập mô tả tối đa 200 ký tự.
  - **Đối tượng (bắt buộc)**: Nhập mô tả đối tượng mục tiêu, độ dài 5–300 ký tự.
  - **Nền tảng (bắt buộc)**: Bắt buộc chọn đúng một nền tảng đang được hỗ trợ và kết nối (Facebook, Instagram, Zalo OA,...). Việc tạo đồng thời cho nhiều nền tảng thuộc phạm vi STORY-024.
  - **Giọng văn (bắt buộc)**: Bắt buộc chọn một trong các giọng văn: *Chuyên nghiệp*, *Thân thiện*, *Truyền cảm hứng*, *Ngắn gọn*, *Vui vẻ*, hoặc *Khác*. Nếu chọn *Khác*, phải nhập mô tả tối đa 200 ký tự.
  - **Ghi chú (không bắt buộc)**: Có placeholder ghi chú là những thông tin thêm dành cho AI để xử lý.
- **Quy tắc Hashtag**:
  - Số lượng hashtag tối đa được quy định trong System Prompt (tối đa 30, tối thiểu 1).
  - Mỗi hashtag phải bắt đầu bằng ký tự `#`. Sau dấu `#` không được có khoảng trắng.
  - Hashtag chỉ được chứa chữ cái, chữ số và dấu gạch dưới `_`. Không cho phép ký tự đặc biệt khác như `! @ $ % & *`.
  - Mỗi hashtag có độ dài từ 2 đến 50 ký tự (không tính dấu `#`).
  - Cho phép hashtag bằng tiếng Việt có dấu, tiếng Việt không dấu hoặc tiếng Anh.
  - Hashtag phải liên quan đến nội dung được sinh, không tạo hashtag không liên quan chỉ để tăng số lượng.
  - Hệ thống phải tự động loại bỏ hashtag trùng lặp (không phân biệt chữ hoa/chữ thường).
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
  > *"Cần có thêm định nghĩa cho phần chủ đề trong context. Cái này thuộc SP S1. Cần có thêm quy tắc cho hashtag như số lượng, định dạng (có/không dấu #), độ dài, ngôn ngữ, khử trùng lặp. Chưa có AC cho Alt - 02"* — Nguyễn Đức Bình · 09:54 03/09/2026

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
4. Hệ thống kiểm tra tính hợp lệ của dữ liệu đầu vào theo quy định.
5. Hệ thống gửi yêu cầu sinh nội dung cùng các tham số đầu vào đến dịch vụ AI.
6. AI xử lý và sinh nội dung cùng danh sách hashtag phù hợp theo quy tắc.
7. Hệ thống hiển thị kết quả gồm nội dung và danh sách hashtag để Quản trị viên xem lại.
8. Quản trị viên kiểm tra và nhấn chọn “Sử dụng nội dung”.
9. Hệ thống lưu nội dung và danh sách hashtag thành một bản ghi content đã lưu trong cơ sở dữ liệu.
10. Hệ thống hiển thị thông báo thành công và chuyển hướng đến màn hình chi tiết content đã lưu.

### Alternative Flow
- **ALT-01 — Tạo lại kết quả (Regenerate)**:
  - Tại bước 7, nếu Quản trị viên chưa hài lòng, chọn “Tạo lại”.
  - Hệ thống gửi lại yêu cầu với cùng dữ liệu đầu vào.
  - Hệ thống hiển thị kết quả mới và quay lại bước 7.
- **ALT-02 — Chỉnh sửa kết quả do AI tạo trước khi lưu**:
  - Tại bước 7, Quản trị viên trực tiếp chỉnh sửa nội dung văn bản hoặc thêm/bớt/sửa hashtag do AI tạo ra.
  - Hệ thống giữ lại các thay đổi của Quản trị viên trên giao diện và tiếp tục đến bước 8.
- **ALT-03 — Thay đổi đầu vào và tạo lại kết quả**:
  - Tại bước 7, Quản trị viên thay đổi một hoặc nhiều thông tin đầu vào ở form.
  - Hệ thống xác định kết quả đang hiển thị không còn tương ứng với dữ liệu mới và hiển thị cảnh báo: *"Đầu vào đã bị thay đổi so với ban đầu"* kèm nút “Tạo lại”.
  - Quản trị viên có thể chọn “Tạo nội dung” / “Tạo lại” để gửi yêu cầu mới với đầu vào cập nhật, hoặc chọn “Sử dụng nội dung” để lưu kết quả hiện tại.
- **ALT-04 — Thoát khi có kết quả chưa sử dụng**:
  - Quản trị viên đã nhập dữ liệu hoặc có kết quả AI nhưng chưa nhấn “Sử dụng nội dung”.
  - Quản trị viên đóng màn hình hoặc chuyển sang chức năng khác.
  - Hệ thống hiển thị cảnh báo: *"Nội dung chưa được lưu. Bạn có chắc chắn muốn rời đi?"*.
  - Nếu Quản trị viên chọn "Tiếp tục", hệ thống giữ nguyên màn hình, dữ liệu và kết quả.
  - Nếu Quản trị viên chọn "Thoát không lưu", hệ thống hủy bỏ dữ liệu tạm thời và không lưu vào CSDL.

### Exception Flow
- **EXC-01 — Dữ liệu đầu vào không hợp lệ**:
  - Tại bước 4, nếu thiếu trường bắt buộc hoặc dữ liệu không đúng định dạng/độ dài quy định.
  - Hệ thống chặn gửi yêu cầu đến AI, đánh dấu các trường lỗi kèm thông báo lý do cụ thể.
  - Hệ thống giữ nguyên các dữ liệu hợp lệ đã nhập để Quản trị viên điều chỉnh và thử lại.
- **EXC-02 — AI không phản hồi hoặc timeout**:
  - Tại bước 5, nếu AI không phản hồi trong thời gian quy định (60 giây) hoặc trả về lỗi kết nối.
  - Hệ thống dừng xử lý, hiển thị thông báo: *"Không thể tạo nội dung, vui lòng thử lại"* và cung cấp nút "Thử lại".
  - Giữ nguyên toàn bộ dữ liệu đầu vào đã nhập, không lưu nội dung rỗng/lỗi vào hệ thống.
- **EXC-03 — Không thể lưu nội dung vào CSDL**:
  - Tại bước 9, quá trình lưu nội dung hoặc hashtag gặp lỗi hệ thống / CSDL.
  - Hệ thống xử lý việc lưu như một giao dịch thống nhất (Transaction), rollback hoàn toàn nếu thất bại.
  - Không lưu một phần nội dung hoặc hashtag dở dang.
  - Hệ thống giữ lại kết quả AI và các chỉnh sửa của Quản trị viên trên màn hình, thông báo lỗi để Quản trị viên thử lưu lại.
- **EXC-04 — Kết quả AI không đúng định dạng quy chuẩn**:
  - AI trả về dữ liệu rỗng, thiếu hashtag hoặc sai cấu trúc JSON/Markdown quy định.
  - Hệ thống không hiển thị kết quả lỗi, thông báo cho Quản trị viên và cho phép thử lại với dữ liệu đầu vào hiện tại.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi Quản trị viên nhấn tạo lại hoặc lưu nội dung.
  - Hệ thống từ chối yêu cầu, thông báo phiên đăng nhập hết hạn và chuyển hướng đăng nhập lại.

---

## Acceptance Criteria

- **AC-001 — Hiển thị biểu mẫu nhập thông tin**:
  - **Given**: Quản trị viên đã đăng nhập vào hệ thống.
  - **When**: Quản trị viên mở chức năng "Tự động viết content và hashtag".
  - **Then**: Hệ thống hiển thị biểu mẫu nhập thông tin gồm: Chủ đề (5–1.000 ký tự), Mục tiêu (dropdown chọn 1), Đối tượng (5–300 ký tự), Nền tảng (chọn 1 nền tảng kết nối), Giọng văn (chọn 1), Ghi chú (tùy chọn).
  - **And**: Nút "Tạo nội dung" chỉ khả dụng khi đã nhập đủ và đúng các trường bắt buộc.

- **AC-002 — Sinh nội dung và hashtag thành công**:
  - **Given**: Quản trị viên đã nhập đầy đủ dữ liệu hợp lệ vào form.
  - **When**: Quản trị viên nhấn "Tạo nội dung".
  - **Then**: Hệ thống gửi yêu cầu tới AI và nhận kết quả gồm nội dung và danh sách hashtag phù hợp.
  - **And**: Các hashtag tuân thủ đúng quy tắc: bắt đầu bằng `#`, từ 2-50 ký tự, không chứa ký tự đặc biệt, không trùng lặp, số lượng từ 1 đến 30.
  - **And**: Hệ thống hiển thị nội dung và danh sách hashtag trên giao diện xem lại.

- **AC-003 — Tạo lại kết quả với cùng dữ liệu đầu vào (ALT-01)**:
  - **Given**: Kết quả do AI sinh đang hiển thị trên màn hình.
  - **When**: Quản trị viên chọn "Tạo lại".
  - **Then**: Hệ thống gửi yêu cầu sinh mới đến AI với cùng bộ dữ liệu đầu vào.
  - **And**: Hệ thống cập nhật hiển thị kết quả mới và giữ nguyên dữ liệu đầu vào ban đầu.

- **AC-004 — Chỉnh sửa kết quả AI trước khi lưu (ALT-02)**:
  - **Given**: Kết quả nội dung và hashtag do AI sinh đang hiển thị.
  - **When**: Quản trị viên trực tiếp chỉnh sửa nội dung văn bản hoặc thêm/xóa/sửa hashtag trên giao diện.
  - **Then**: Hệ thống cho phép chỉnh sửa trực tiếp trên vùng văn bản và cập nhật danh sách hashtag hiển thị theo các thao tác của Quản trị viên.
  - **And**: Dữ liệu sau khi chỉnh sửa sẵn sàng để được lưu khi Quản trị viên nhấn "Sử dụng nội dung".

- **AC-005 — Lưu content và hashtag thành công**:
  - **Given**: Kết quả AI (gốc hoặc đã qua chỉnh sửa tại ALT-02) đang hiển thị hợp lệ.
  - **When**: Quản trị viên chọn "Sử dụng nội dung".
  - **Then**: Hệ thống lưu đầy đủ nội dung và hashtag thành một bản ghi content mới vào cơ sở dữ liệu.
  - **And**: Hệ thống hiển thị thông báo lưu thành công và không tự động đăng bài hoặc lên lịch đăng.

- **AC-006 — Xử lý dữ liệu đầu vào không hợp lệ (EXC-01)**:
  - **Given**: Quản trị viên chưa điền đủ các trường bắt buộc hoặc dữ liệu vi phạm ràng buộc độ dài/định dạng.
  - **When**: Quản trị viên nhấn "Tạo nội dung".
  - **Then**: Hệ thống ngăn chặn gửi yêu cầu đến AI, hiển thị thông báo lỗi chi tiết tại từng trường không hợp lệ.
  - **And**: Giữ nguyên các nội dung hợp lệ đã nhập để Quản trị viên chỉnh sửa.

- **AC-007 — Xử lý khi AI không phản hồi hoặc timeout (EXC-02)**:
  - **Given**: Quản trị viên đã gửi yêu cầu tạo nội dung hợp lệ.
  - **When**: AI không phản hồi sau 60 giây hoặc trả về lỗi kết nối.
  - **Then**: Hệ thống dừng xử lý, hiển thị thông báo: *"Không thể tạo nội dung, vui lòng thử lại"*.
  - **And**: Toàn bộ dữ liệu đầu vào được giữ nguyên và Quản trị viên có thể nhấn thử lại.
  - **And**: Không có dữ liệu rác hoặc bản ghi dở dang nào được lưu vào CSDL.

- **AC-008 — Thay đổi dữ liệu đầu vào khi đang có kết quả (ALT-03)**:
  - **Given**: Kết quả AI đang hiển thị trên màn hình.
  - **When**: Quản trị viên thay đổi bất kỳ trường thông tin đầu vào nào.
  - **Then**: Hệ thống hiển thị cảnh báo thông tin đầu vào đã thay đổi kèm nút "Tạo lại".
  - **And**: Quản trị viên có thể bấm "Tạo lại" để sinh kết quả mới theo đầu vào mới, hoặc bấm "Sử dụng nội dung" để lưu kết quả hiện tại.

- **AC-009 — Cảnh báo khi thoát mà chưa lưu kết quả (ALT-04)**:
  - **Given**: Quản trị viên đã nhập thông tin hoặc đang có kết quả AI nhưng chưa nhấn "Sử dụng nội dung".
  - **When**: Quản trị viên thao tác đóng form hoặc chuyển trang.
  - **Then**: Hệ thống hiển thị hộp thoại cảnh báo: *"Nội dung chưa được lưu. Bạn có chắc chắn muốn rời đi?"*.
  - **And**: Nếu chọn "Tiếp tục", giữ nguyên màn hình và dữ liệu hiện tại.
  - **And**: Nếu chọn "Thoát không lưu", đóng form và không lưu nội dung vào CSDL.

- **AC-010 — Xử lý kết quả AI sai quy chuẩn (EXC-04)**:
  - **Given**: AI phản hồi kết quả nhưng nội dung rỗng hoặc không sinh được hashtag.
  - **When**: Hệ thống kiểm tra tính hợp lệ của kết quả trả về.
  - **Then**: Hệ thống từ chối hiển thị kết quả lỗi, thông báo lỗi kỹ thuật và cho phép Quản trị viên gửi lại yêu cầu.

- **AC-011 — Đảm bảo tính toàn vẹn khi lưu (EXC-03)**:
  - **Given**: Quản trị viên đã nhấn "Sử dụng nội dung".
  - **When**: Quá trình lưu gặp lỗi CSDL.
  - **Then**: Hệ thống rollback toàn bộ giao dịch, không lưu bản ghi dở dang.
  - **And**: Thông báo lỗi lưu thất bại và giữ nguyên nội dung trên màn hình để Quản trị viên thử lại.

- **AC-012 — Xử lý hết hạn phiên làm việc (EXC-05)**:
  - **Given**: Quản trị viên đang ở màn hình tạo nội dung.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên thực hiện thao tác tạo lại hoặc lưu.
  - **Then**: Hệ thống từ chối yêu cầu, thông báo phiên hết hạn và yêu cầu đăng nhập lại mà không làm mất mát dữ liệu bất thường.

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
- **Bảo mật và kiểm soát**: Hệ thống không tự động lưu vào CSDL hoặc tự động đăng bài trước khi Quản trị viên bấm xác nhận "Sử dụng nội dung".

---

## Out of Scope
- Không bao gồm tạo hoặc chỉnh sửa hình ảnh đính kèm (thuộc STORY-003, STORY-021, STORY-023).
- Không bao gồm lên lịch hoặc tự động đăng bài lên mạng xã hội (thuộc STORY-002, STORY-016).
- Không bao gồm viết lại nội dung cho nhiều nền tảng cùng lúc (thuộc STORY-024).
- Không bao gồm quản lý cấu hình sâu hoặc huấn luyện mô hình AI.
