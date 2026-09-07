# STORY-023: Sửa ảnh đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chỉnh sửa thông tin của ảnh đã lưu trong hệ thống để cập nhật các thông tin chưa chính xác hoặc không còn phù hợp mà không cần xóa và tạo lại ảnh.
- **Context**: Hệ thống lưu trữ các ảnh được tải lên hoặc do AI tự động sinh ra (STORY-003) để phục vụ quá trình quản lý nội dung. Đối với ảnh do STORY-003 sinh ra, hệ thống chỉ khởi tạo tên ảnh mặc định và chưa có mô tả hay thẻ phân loại. Chức năng này cho phép Quản trị viên cập nhật thông tin quản lý (Metadata) của ảnh đã lưu (tên ảnh, mô tả, thẻ phân loại) nhằm phục vụ tra cứu và quản lý thư viện ảnh hiệu quả hơn. Chức năng này tuyệt đối không chỉnh sửa trực tiếp nội dung hình ảnh hay tệp đồ họa gốc.
- **Quy định các trường thông tin**:
  - **Tên ảnh (bắt buộc)**: Bắt buộc nhập, không được để trống hoặc chỉ chứa khoảng trắng, độ dài từ 1 đến 255 ký tự.
  - **Mô tả (tùy chọn)**: Độ dài tối đa 1.000 ký tự.
  - **Thẻ phân loại / Tags (tùy chọn)**: Cho phép thêm/xóa các thẻ từ khóa, mỗi thẻ tối đa 50 ký tự, hỗ trợ tìm kiếm tại STORY-021.
  - **Trường chỉ đọc do hệ thống quản lý**: ID ảnh, tệp ảnh gốc, tỷ lệ khung hình, kích thước px, dung lượng, người tạo và thời gian tạo ban đầu (không cho phép chỉnh sửa).
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
  > *"Ba trường sửa được (tên ảnh, mô tả, thẻ) đều không được STORY-003 khởi tạo. STR này nên có thêm Out Of Scope"* — Nguyễn Đức Bình · 10:14 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Ảnh cần chỉnh sửa thông tin còn tồn tại trong hệ thống.
  - Quản trị viên có quyền quản lý thư viện hình ảnh.
- **Trigger**:
  - Quản trị viên chọn một ảnh đã lưu và nhấn nút “Chỉnh sửa” (từ danh sách hoặc từ trang chi tiết ảnh).

---

## Flow

### Main Flow — Sửa thông tin ảnh đã lưu
1. Quản trị viên chọn “Chỉnh sửa” tại một hình ảnh đã lưu.
2. Hệ thống tải thông tin chi tiết và phiên bản hiện tại của bản ghi ảnh.
3. Hệ thống hiển thị màn hình chỉnh sửa với tên ảnh, mô tả và danh sách thẻ (tags) hiện tại được điền sẵn.
4. Hệ thống hiển thị mã định danh (ID), tệp ảnh thu nhỏ, tệp ảnh gốc, tỷ lệ, kích thước, người tạo và ngày tạo ở chế độ chỉ xem (Read-only).
5. Quản trị viên chỉnh sửa tên ảnh, mô tả hoặc cập nhật danh sách thẻ tags.
6. Quản trị viên nhấn nút “Lưu”.
7. Hệ thống kiểm tra tính hợp lệ của dữ liệu (tên ảnh không để trống, giới hạn ký tự hợp lệ).
8. Hệ thống kiểm tra lại quyền, tình trạng tồn tại và phiên bản của bản ghi tại thời điểm lưu (Optimistic Locking).
9. Hệ thống chỉ cập nhật các trường metadata được phép (tên, mô tả, thẻ tags, người cập nhật, thời gian cập nhật), giữ nguyên vẹn tệp ảnh gốc và các thuộc tính kỹ thuật.
10. Hệ thống hiển thị thông báo: *"Cập nhật thông tin ảnh thành công"*.
11. Hệ thống đóng màn hình chỉnh sửa và hiển thị thông tin mới nhất của ảnh.

### Alternative Flow
- **ALT-01 — Quản trị viên hủy bỏ chỉnh sửa**:
  - Màn hình chỉnh sửa đang mở, Quản trị viên nhấn nút “Hủy”.
  - Hệ thống đóng màn hình chỉnh sửa và không gửi yêu cầu cập nhật tới máy chủ.
  - Toàn bộ thông tin ảnh hiện tại được giữ nguyên vẹn.
- **ALT-02 — Chỉ sửa một phần thông tin**:
  - Quản trị viên chỉ thay đổi tên ảnh hoặc chỉ cập nhật thẻ tags.
  - Quản trị viên nhấn "Lưu". Hệ thống chỉ cập nhật các trường có thay đổi và tiếp tục từ bước 7.
- **ALT-03 — Không có thay đổi để lưu**:
  - Quản trị viên mở form chỉnh sửa nhưng không thay đổi bất kỳ ký tự nào và bấm "Lưu".
  - Hệ thống xác định không có thay đổi, không gửi truy vấn CSDL và hiển thị thông báo: *"Không có thay đổi để lưu"*.
- **ALT-04 — Thoát khi có thay đổi chưa lưu**:
  - Quản trị viên đã thay đổi dữ liệu nhưng nhấn đóng hoặc quay lại mà chưa bấm "Lưu".
  - Hệ thống hiển thị cảnh báo: *"Có thay đổi chưa được lưu. Bạn có muốn rời đi?"*.
  - Nếu chọn “Tiếp tục chỉnh sửa”, giữ nguyên màn hình và dữ liệu đang nhập.
  - Nếu chọn “Thoát không lưu”, loại bỏ các thay đổi và giữ nguyên thông tin ảnh cũ.

### Exception Flow
- **EXC-01 — Dữ liệu không hợp lệ**:
  - Tên ảnh bị để trống, chỉ chứa khoảng trắng hoặc vượt quá giới hạn 255 ký tự; mô tả vượt quá 1.000 ký tự.
  - Hệ thống từ chối cập nhật, đánh dấu vi phạm tại trường tương ứng kèm lý do, giữ nguyên dữ liệu đang nhập để Quản trị viên sửa lại.
- **EXC-02 — Ảnh không còn tồn tại**:
  - Ảnh đã bị xóa trước thời điểm Quản trị viên mở hoặc bấm lưu màn hình chỉnh sửa.
  - Hệ thống dừng thao tác, thông báo: *"Ảnh không còn tồn tại trong hệ thống"* và chuyển hướng người dùng về danh sách ảnh.
- **EXC-03 — Lỗi hệ thống khi cập nhật CSDL**:
  - Quá trình lưu thông tin ảnh vào CSDL gặp sự cố mạng hoặc lỗi máy chủ.
  - Hệ thống rollback giao dịch, không lưu một phần dữ liệu, thông báo: *"Không thể cập nhật thông tin ảnh, vui lòng thử lại"*. Dữ liệu đang nhập được giữ lại trên form.
- **EXC-04 — Xung đột phiên bản (Metadata đã bị sửa bởi người khác)**:
  - Quản trị viên A đang mở form thì Quản trị viên B đã cập nhật thông tin ảnh trước đó.
  - Quản trị viên A bấm Lưu -> Hệ thống phát hiện phiên bản đã đổi, ngăn chặn ghi đè, thông báo thông tin ảnh đã bị thay đổi và yêu cầu tải lại trang.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn trước khi Quản trị viên nhấn Lưu.
  - Hệ thống từ chối cập nhật, giữ nguyên dữ liệu và yêu cầu đăng nhập lại.
- **EXC-06 — Không thể tải thông tin hiện tại của ảnh**:
  - Hệ thống gặp lỗi kết nối không thể tải dữ liệu ảnh ban đầu vào form.
  - Hệ thống không hiển thị form thiếu dữ liệu, hiển thị thông báo lỗi và cung cấp nút tải lại.

---

## Acceptance Criteria

- **AC-001 — Hiển thị biểu mẫu chỉnh sửa thông tin ảnh**:
  - **Given**: Quản trị viên đang ở trang danh sách hoặc trang chi tiết một ảnh còn tồn tại.
  - **When**: Quản trị viên chọn “Chỉnh sửa”.
  - **Then**: Hệ thống mở màn hình chỉnh sửa với tên ảnh, mô tả và danh sách thẻ tags hiện tại được điền sẵn.
  - **And**: Mã định danh ảnh, ảnh thu nhỏ, tệp ảnh gốc, tỷ lệ, kích thước, người tạo và thời gian tạo được hiển thị ở chế độ chỉ xem.

- **AC-002 — Cập nhật thông tin ảnh thành công**:
  - **Given**: Quản trị viên đã nhập dữ liệu hợp lệ và thông tin ảnh chưa bị sửa bởi người khác.
  - **When**: Quản trị viên nhấn “Lưu”.
  - **Then**: Hệ thống cập nhật tên ảnh, mô tả và thẻ tags đã thay đổi vào cơ sở dữ liệu.
  - **And**: Hệ thống tự động ghi nhận tài khoản người cập nhật và thời gian cập nhật.
  - **And**: Tệp ảnh gốc vật lý và các biến thể tỷ lệ không bị thay đổi.
  - **And**: Hệ thống hiển thị thông báo: *"Cập nhật thông tin ảnh thành công"*.

- **AC-003 — Dữ liệu chỉnh sửa không hợp lệ (EXC-01)**:
  - **Given**: Quản trị viên đang ở màn hình chỉnh sửa thông tin ảnh.
  - **When**: Quản trị viên xóa trắng tên ảnh hoặc nhập dữ liệu vượt quá giới hạn và nhấn “Lưu”.
  - **Then**: Hệ thống từ chối cập nhật, hiển thị thông báo lỗi tại trường dữ liệu tương ứng và giữ lại dữ liệu đang nhập để Quản trị viên sửa.

- **AC-004 — Hủy bỏ chỉnh sửa (ALT-01)**:
  - **Given**: Màn hình chỉnh sửa thông tin ảnh đang hiển thị.
  - **When**: Quản trị viên chọn “Hủy”.
  - **Then**: Hệ thống đóng màn hình chỉnh sửa, không gửi yêu cầu cập nhật và giữ nguyên thông tin ảnh cũ.

- **AC-005 — Xử lý khi không có thay đổi (ALT-03)**:
  - **Given**: Quản trị viên mở màn hình chỉnh sửa nhưng không thay đổi bất kỳ trường nào.
  - **When**: Quản trị viên nhấn “Lưu”.
  - **Then**: Hệ thống không gửi truy vấn cập nhật và hiển thị thông báo: *"Không có thay đổi để lưu"*.

- **AC-006 — Xử lý thoát khi chưa lưu (ALT-04)**:
  - **Given**: Quản trị viên đã thay đổi dữ liệu nhưng chưa lưu.
  - **When**: Quản trị viên thao tác đóng hoặc rời màn hình.
  - **Then**: Hệ thống hiển thị hộp thoại xác nhận cảnh báo dữ liệu chưa lưu.
  - **And**: Nếu chọn tiếp tục chỉnh sửa thì giữ nguyên màn hình; nếu chọn thoát thì hủy bỏ thay đổi và giữ nguyên thông tin cũ.

- **AC-007 — Xử lý ảnh không còn tồn tại (EXC-02)**:
  - **Given**: Ảnh đã bị xóa trong hệ thống trước thời điểm lưu.
  - **When**: Quản trị viên mở hoặc nhấn lưu màn hình chỉnh sửa.
  - **Then**: Hệ thống từ chối cập nhật, hiển thị thông báo ảnh không còn tồn tại và cho phép quay lại danh sách.

- **AC-008 — Đảm bảo tính toàn vẹn khi lưu gặp sự cố (EXC-03)**:
  - **Given**: Quản trị viên đang lưu dữ liệu hợp lệ.
  - **When**: Quá trình cập nhật CSDL gặp sự cố.
  - **Then**: Hệ thống không ghi nhận cập nhật thành công, không có thông tin nào bị cập nhật dở dang một phần và giữ lại dữ liệu đang nhập để người dùng thử lại.

- **AC-009 — Ngăn chặn xung đột ghi đè dữ liệu (EXC-04)**:
  - **Given**: Quản trị viên đang mở form sửa dựa trên phiên bản cũ.
  - **When**: Thông tin ảnh đã được quản trị viên khác cập nhật trước thời điểm bấm "Lưu".
  - **Then**: Hệ thống chặn ghi đè, hiển thị thông báo thông tin ảnh đã bị thay đổi và yêu cầu tải lại dữ liệu mới nhất.

- **AC-010 — Bảo toàn nguyên vẹn tệp ảnh gốc và thuộc tính kỹ thuật**:
  - **Given**: Quản trị viên chỉnh sửa thông tin metadata của ảnh thành công.
  - **When**: Hệ thống hoàn tất quá trình lưu.
  - **Then**: Tệp ảnh gốc và các tệp biến thể theo tỷ lệ không bị sửa đổi, thay thế hoặc nén lại.
  - **And**: Mã định danh (ID), người tạo và thời gian tạo gốc được giữ nguyên vẹn.

---

## References
- **Rules**:
  - [BR-066](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/017d3150-35dc-47e7-91c9-39b0dac8e258)
  - [BR-067](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/d1115968-e111-4cde-9899-80fd76368feb)
- **Dependencies**:
  - STORY-003 (Tự động sinh ảnh đa tỷ lệ từ ảnh core - ảnh nguồn ban đầu chưa có thẻ và mô tả).
  - STORY-021 (Xem ảnh đã lưu lại).

---

## Non-Functional
- **Thời gian phản hồi**: Thời gian tải form và phản hồi thao tác cập nhật dưới 2 giây trong điều kiện hoạt động bình thường.
- **Bảo mật và phân quyền**: Backend bắt buộc kiểm tra quyền quản lý thư viện hình ảnh trước khi cho phép cập nhật.
- **Tính bất biến của tệp đồ họa**: Việc cập nhật thông tin siêu dữ liệu (Metadata) tuyệt đối không tác động hoặc làm thay đổi tệp nhị phân của ảnh gốc.

---

## Out of Scope
- Không chỉnh sửa trực tiếp nội dung hình ảnh (không can thiệp pixel).
- Không cắt xén (crop), xoay, nén hoặc thay đổi kích thước/tỷ lệ của ảnh.
- Không thay đổi định dạng tệp (không chuyển đổi PNG sang JPG hoặc ngược lại).
- Không thay thế tệp ảnh hiện tại bằng tệp ảnh khác.
- Không gắn hoặc sử dụng ảnh trong content, lịch đăng bài hoặc bài đăng.
- Không hỗ trợ chỉnh sửa hàng loạt đồng thời nhiều ảnh.
- Không tạo ảnh mới hoặc tạo lại ảnh bằng AI.
