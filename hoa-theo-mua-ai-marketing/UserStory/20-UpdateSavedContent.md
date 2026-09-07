# STORY-020: Sửa content đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chỉnh sửa content và hashtag đã lưu để cập nhật nội dung phù hợp trước khi sử dụng
- **Context**: Content đã lưu có thể cần điều chỉnh khi mục tiêu, nền tảng hoặc yêu cầu truyền thông thay đổi. Hệ thống phải hiển thị dữ liệu hiện tại, cho phép Quản trị viên cập nhật các trường được phép và kiểm tra dữ liệu trước khi lưu. Chức năng này chỉ bao gồm sửa content đã lưu, không bao gồm tạo mới, xem, xóa, lên lịch hoặc đăng bài.
- **Quy định các trường dữ liệu khi chỉnh sửa**:
  - **Nội dung content (bắt buộc)**: Độ dài từ 1 đến 10.000 ký tự sau khi loại bỏ khoảng trắng ở đầu và cuối (bao gồm cả ký tự của hashtag).
  - **Hashtag (bắt buộc)**: Bắt buộc tối thiểu 1 hashtag và tối đa 30 hashtag (nhất quán với STORY-017). Mỗi hashtag bắt đầu bằng dấu `#`, dài 2–50 ký tự, không chứa ký tự đặc biệt ngoài `_`, không trùng lặp (không phân biệt hoa/thường).
  - **Trường chỉ đọc (Read-only)**: Tiêu đề gốc, nền tảng mục tiêu, nguồn tạo, người tạo và thời gian tạo ban đầu (không cho phép chỉnh sửa).
  - **Cơ chế lưu trữ**: Hệ thống cập nhật trực tiếp bản ghi content hiện tại, ghi nhận thông tin người cập nhật và thời gian cập nhật. Hệ thống không lưu trữ lịch sử các phiên bản cũ (No Version History).
  - **Kiểm soát đồng thời (Optimistic Locking)**: Hệ thống kiểm tra phiên bản (version/timestamp) của bản ghi tại thời điểm lưu để ngăn chặn xung đột ghi đè khi nhiều quản trị viên cùng thao tác.
- **Sprint**: S2
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 06/09/2026
- **Author**: Nguyễn Anh Quân
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Hashtag không bắt buộc - mâu thuẫn STORY-017 Giới hạn 1–10.000 ký tự mâu thuẫn STORY-017"* — Nguyễn Đức Bình · 10:01 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Bản ghi content còn tồn tại trong hệ thống và ở trạng thái cho phép chỉnh sửa (chưa bị khóa bởi lịch đăng đang chạy).
- **Trigger**:
  - Quản trị viên chọn nút “Sửa” tại một content đã lưu trên danh sách hoặc trang chi tiết.

---

## Flow

### Main Flow — Sửa content đã lưu
1. Quản trị viên chọn bản ghi content cần sửa và nhấn nút “Sửa”.
2. Hệ thống kiểm tra quyền của Quản trị viên, sự tồn tại của content và trạng thái cho phép chỉnh sửa.
3. Hệ thống tải dữ liệu hiện tại của content (nội dung, hashtag, thông tin chỉ đọc và mã phiên bản hiện tại).
4. Hệ thống hiển thị màn hình chỉnh sửa với đầy đủ dữ liệu được điền sẵn vào các ô nhập liệu tương ứng.
5. Quản trị viên chỉnh sửa nội dung văn bản hoặc danh sách hashtag.
6. Quản trị viên nhấn nút “Lưu”.
7. Hệ thống kiểm tra tính hợp lệ của dữ liệu đầu vào (độ dài 1–10.000 ký tự, tối thiểu 1 và tối đa 30 hashtag đúng quy tắc).
8. Hệ thống kiểm tra lại quyền và kiểm tra phiên bản bản ghi (Optimistic Lock) tại thời điểm lưu để đảm bảo không bị cập nhật bởi người khác trong lúc đang sửa.
9. Hệ thống cập nhật nội dung, danh sách hashtag, thời gian cập nhật và người cập nhật vào cơ sở dữ liệu trong một Transaction duy nhất.
10. Hệ thống hiển thị thông báo: *"Cập nhật content thành công"*.
11. Hệ thống đóng màn hình chỉnh sửa và hiển thị thông tin mới nhất của content.

### Alternative Flow
- **ALT-01 — Hủy thao tác chỉnh sửa**:
  - Tại bước 5, Quản trị viên nhấn nút “Hủy”.
  - Hệ thống đóng màn hình chỉnh sửa, không gửi yêu cầu lưu dữ liệu, giữ nguyên vẹn nội dung và hashtag cũ của content.
- **ALT-02 — Chỉ sửa một phần thông tin**:
  - Tại bước 5, Quản trị viên chỉ chỉnh sửa nội dung văn bản (giữ nguyên hashtag) hoặc chỉ chỉnh sửa hashtag (giữ nguyên nội dung).
  - Quản trị viên nhấn "Lưu". Hệ thống chỉ cập nhật các trường có thay đổi và tiếp tục từ bước 7.
- **ALT-03 — Không có thay đổi để lưu**:
  - Quản trị viên mở màn hình chỉnh sửa nhưng không thay đổi bất kỳ ký tự nào và nhấn "Lưu".
  - Hệ thống phát hiện không có dữ liệu thay đổi, không thực hiện truy vấn cập nhật CSDL, hiển thị thông báo: *"Không có thay đổi để lưu"*.
- **ALT-04 — Thoát khi có thay đổi chưa lưu**:
  - Quản trị viên đã chỉnh sửa nhưng nhấn nút quay lại hoặc chuyển trang mà chưa nhấn "Lưu".
  - Hệ thống hiển thị cảnh báo: *"Dữ liệu chỉnh sửa chưa được lưu. Bạn có chắc chắn muốn rời đi?"*.
  - Nếu chọn "Tiếp tục chỉnh sửa", hệ thống giữ nguyên màn hình và dữ liệu đang nhập dở dang.
  - Nếu chọn "Thoát không lưu", loại bỏ các thay đổi và giữ nguyên content cũ.

### Exception Flow
- **EXC-01 — Content không còn khả dụng để sửa**:
  - Content đã bị xóa hoặc chuyển sang trạng thái bị khóa bởi tiến trình khác.
  - Hệ thống ngăn chặn mở biểu mẫu hoặc từ chối lưu, hiển thị thông báo: *"Content không còn khả dụng để sửa"* và điều hướng về danh sách.
- **EXC-02 — Dữ liệu chỉnh sửa không hợp lệ**:
  - Tại bước 7, nội dung bị để trống, vượt quá 10.000 ký tự, hoặc danh sách hashtag vi phạm quy tắc (ít hơn 1 hashtag, vượt quá 30 hashtag, sai định dạng `#`).
  - Hệ thống từ chối lưu, đánh dấu lỗi tại các trường vi phạm kèm thông báo nguyên nhân cụ thể, giữ nguyên dữ liệu đang nhập để Quản trị viên sửa lại.
- **EXC-03 — Lỗi hệ thống khi cập nhật CSDL**:
  - Tại bước 9, xảy ra lỗi mạng hoặc máy chủ không thể hoàn tất lưu.
  - Hệ thống rollback toàn bộ giao dịch, không lưu một phần dữ liệu, thông báo: *"Không thể cập nhật content, vui lòng thử lại"*. Dữ liệu đang nhập trên giao diện được giữ nguyên để Quản trị viên thử lại.
- **EXC-04 — Xung đột phiên bản (Content đã bị sửa bởi người khác)**:
  - Quản trị viên A đang sửa thì Quản trị viên B đã lưu thành công bản cập nhật mới trước đó.
  - Quản trị viên A nhấn "Lưu" -> Hệ thống phát hiện phiên bản bản ghi bị thay đổi, ngăn chặn ghi đè và thông báo: *"Content đã được cập nhật bởi người dùng khác. Vui lòng tải lại trang để xem nội dung mới nhất"*.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi Quản trị viên nhấn Lưu.
  - Hệ thống từ chối cập nhật, giữ nguyên bản ghi trên CSDL, hiển thị thông báo phiên hết hạn và yêu cầu đăng nhập lại.
- **EXC-06 — Không thể tải dữ liệu hiện tại của content**:
  - Tại bước 3, lỗi kết nối khiến hệ thống không tải được đầy đủ thông tin ban đầu.
  - Hệ thống không mở biểu mẫu thiếu dữ liệu, hiển thị thông báo lỗi và cung cấp nút tải lại.

---

## Acceptance Criteria

- **AC-001 — Hiển thị biểu mẫu chỉnh sửa với dữ liệu điền sẵn**:
  - **Given**: Content còn tồn tại và ở trạng thái cho phép chỉnh sửa.
  - **When**: Quản trị viên chọn nút "Sửa" tại content đó.
  - **Then**: Hệ thống mở màn hình chỉnh sửa và điền sẵn chính xác toàn bộ nội dung văn bản và danh sách hashtag hiện tại.
  - **And**: Các thông tin chỉ đọc (tiêu đề, nền tảng, tác giả, ngày tạo) được hiển thị ở chế độ không cho phép chỉnh sửa.

- **AC-002 — Cập nhật content thành công (Main Flow)**:
  - **Given**: Quản trị viên đã nhập dữ liệu hợp lệ (nội dung 1–10.000 ký tự, 1–30 hashtag đúng quy tắc) và bản ghi chưa bị thay đổi bởi người khác.
  - **When**: Quản trị viên nhấn "Lưu".
  - **Then**: Hệ thống cập nhật nội dung, danh sách hashtag, thời gian cập nhật và người cập nhật vào cơ sở dữ liệu trong một Transaction duy nhất.
  - **And**: Hệ thống hiển thị thông báo: *"Cập nhật content thành công"* và hiển thị dữ liệu mới nhất.

- **AC-003 — Hủy bỏ chỉnh sửa (ALT-01)**:
  - **Given**: Màn hình chỉnh sửa content đang hiển thị.
  - **When**: Quản trị viên nhấn nút "Hủy".
  - **Then**: Hệ thống đóng màn hình chỉnh sửa và không gửi yêu cầu cập nhật tới máy chủ.
  - **And**: Bản ghi content cũ trong CSDL được giữ nguyên vẹn.

- **AC-004 — Xử lý dữ liệu không hợp lệ (EXC-02)**:
  - **Given**: Quản trị viên đang ở màn hình chỉnh sửa.
  - **When**: Quản trị viên xóa trắng nội dung, nhập vượt quá 10.000 ký tự, hoặc để trống hashtag / nhập quá 30 hashtag và nhấn "Lưu".
  - **Then**: Hệ thống từ chối lưu, hiển thị thông báo lỗi cụ thể tại các trường vi phạm và giữ nguyên dữ liệu đang nhập để Quản trị viên điều chỉnh.

- **AC-005 — Xử lý khi không có thay đổi (ALT-03)**:
  - **Given**: Màn hình chỉnh sửa đang mở nhưng Quản trị viên không thay đổi bất kỳ nội dung nào.
  - **When**: Quản trị viên nhấn "Lưu".
  - **Then**: Hệ thống không gửi truy vấn cập nhật CSDL và hiển thị thông báo: *"Không có thay đổi để lưu"*.

- **AC-006 — Cảnh báo khi thoát mà chưa lưu (ALT-04)**:
  - **Given**: Quản trị viên đã thay đổi dữ liệu trên biểu mẫu nhưng chưa bấm "Lưu".
  - **When**: Quản trị viên thao tác quay lại hoặc đóng trang.
  - **Then**: Hệ thống hiển thị hộp thoại cảnh báo có thay đổi chưa được lưu.
  - **And**: Nếu chọn tiếp tục, giữ nguyên dữ liệu đang nhập; nếu chọn thoát không lưu, loại bỏ các thay đổi và giữ nguyên content cũ.

- **AC-007 — Đảm bảo tính nguyên tử khi lưu gặp sự cố (EXC-03)**:
  - **Given**: Dữ liệu nhập hợp lệ và Quản trị viên nhấn "Lưu".
  - **When**: Quá trình cập nhật cơ sở dữ liệu phát sinh lỗi.
  - **Then**: Hệ thống rollback toàn bộ, không có trường dữ liệu nào bị cập nhật một phần.
  - **And**: Hệ thống hiển thị thông báo: *"Không thể cập nhật content, vui lòng thử lại"* và giữ lại dữ liệu đang nhập trên form.

- **AC-008 — Ngăn chặn xung đột ghi đè dữ liệu (EXC-04)**:
  - **Given**: Quản trị viên A đang mở màn hình sửa content dựa trên phiên bản cũ.
  - **When**: Quản trị viên B đã cập nhật và lưu thành công content đó trước khi Quản trị viên A nhấn "Lưu".
  - **Then**: Hệ thống kiểm tra phiên bản, từ chối ghi đè dữ liệu của Quản trị viên A.
  - **And**: Hệ thống hiển thị thông báo: *"Content đã được cập nhật bởi người dùng khác"* và yêu cầu tải lại dữ liệu mới nhất.

- **AC-009 — Xử lý khi content không còn được phép sửa (EXC-01)**:
  - **Given**: Content đã bị xóa hoặc chuyển sang trạng thái không được phép sửa sau khi màn hình được mở.
  - **When**: Quản trị viên nhấn "Lưu".
  - **Then**: Hệ thống từ chối cập nhật, thông báo content không còn khả dụng và chuyển hướng người dùng về danh sách.

- **AC-010 — Xử lý phiên đăng nhập hết hạn (EXC-05)**:
  - **Given**: Quản trị viên đang ở màn hình chỉnh sửa content.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên nhấn "Lưu".
  - **Then**: Hệ thống chặn yêu cầu cập nhật, giữ nguyên dữ liệu trên CSDL và hiển thị thông báo yêu cầu đăng nhập lại.

---

## References
- **Rules**:
  - [BR-025](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/421d8701-2950-41f8-908f-4a6838c5b7cb)
  - [BR-026](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/900ead81-d7af-4e4e-87a8-b093e883833d)
  - [BR-027](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/d1720b9d-f09b-4541-9c1e-d305c6fb22a8)
  - [BR-061](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/94d6e933-928d-47fd-a918-f8e0f9f0935b)
  - [BR-062](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9376331f-36e6-4f4d-9ff4-2b6514d88fa8)
- **Dependencies**:
  - STORY-017 (Tự động viết content và hashtag - quy tắc định dạng nội dung & hashtag).
  - STORY-018 (Xem content đã lưu).

---

## Non-Functional
- **Kiểm soát đồng thời (Concurrency Control)**: Backend bắt buộc phải kiểm tra phiên bản bản ghi (Version / ETag / UpdatedAt) trước khi thực thi lệnh UPDATE.
- **Tính toàn vẹn giao dịch (ACID)**: Nội dung, hashtag và thông tin cập nhật phải được lưu trong cùng một giao dịch thống nhất.
- **Thời gian phản hồi**: 95% yêu cầu cập nhật phải hoàn tất và trả về kết quả trong vòng dưới 2 giây.
- **Audit Log**: Hệ thống tự động ghi nhật ký bao gồm: Tài khoản người sửa, thời điểm sửa và các trường đã thay đổi.

---

## Out of Scope
- Không bao gồm tạo content mới (thuộc STORY-017).
- Không bao gồm xem danh sách hoặc chi tiết content (thuộc STORY-018).
- Không bao gồm xóa content đã lưu (thuộc STORY-019).
- Không bao gồm lên lịch hoặc đăng bài (thuộc STORY-002, STORY-016).
- Không bao gồm lưu trữ hoặc khôi phục lịch sử các phiên bản cũ của content (No Version History).
