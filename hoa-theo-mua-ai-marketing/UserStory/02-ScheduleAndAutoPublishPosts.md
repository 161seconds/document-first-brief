# STORY-002: Lên lịch và tự động đăng bài

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chọn nội dung và hình ảnh đã được lưu, chọn nền tảng được hỗ trợ và thiết lập thời gian đăng để hệ thống tự động đăng bài đúng thời gian đã lên lịch mà không cần thao tác thủ công tại thời điểm đăng
- **Context**: Trước đây, sau khi nội dung và hình ảnh được tạo và lưu, Quản trị viên phải đăng bài thủ công lên từng nền tảng vào thời gian mong muốn. Khi số lượng bài viết và nền tảng tăng lên, việc đăng thủ công dễ dẫn đến quên lịch, đăng sai thời gian hoặc mất nhiều thời gian thao tác. Chức năng này cho phép Quản trị viên lựa chọn nội dung và hình ảnh đã được lưu, lựa chọn một hoặc nhiều nền tảng đã được kết nối với hệ thống và thiết lập thời gian đăng.
- **Quy định dữ liệu khi tạo lịch đăng bài**:
  1. **Nội dung**:
     - Bắt buộc chọn đúng một nội dung đã được lưu.
     - Nội dung phải còn tồn tại và thuộc phạm vi Quản trị viên được phép sử dụng.
  2. **Hình ảnh**:
     - Hình ảnh không bắt buộc.
     - Mỗi lịch đăng được chọn một hoặc nhiều ảnh tối đa 10 ảnh đã lưu hoặc trên máy.
     - Nếu là ảnh đã lưu thì phải còn tồn tại.
     - Ảnh phải có định dạng JPG, JPEG hoặc PNG và dung lượng không vượt quá 10 MB.
  3. **Nền tảng**:
     - Bắt buộc chọn ít nhất một nền tảng.
     - Mỗi nền tảng chỉ được chọn một lần trong cùng một lịch đăng.
     - Nền tảng phải được hệ thống hỗ trợ, đang được bật và đang kết nối hợp lệ.
  4. **Ngày và giờ đăng**:
     - Bắt buộc nhập đầy đủ ngày và giờ đăng.
     - Ngày và giờ đăng phải lớn hơn thời điểm hiện tại.
  5. **Kiểm tra lịch trùng**:
     - Một lịch được xem là trùng khi có cùng nội dung, cùng nền tảng và cùng thời điểm đăng với một lịch đang ở trạng thái "Đã lên lịch".
     - Nếu Quản trị viên chọn nhiều nền tảng, hệ thống kiểm tra trùng riêng cho từng nền tảng.
     - Khi phát hiện trùng, hệ thống không tạo lịch và phải chỉ rõ nền tảng cùng thời điểm đang bị trùng.
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
  > *"Tại mainflow, bước 6 sẽ có trước hay bước 5 có trước? Chưa thấy có AC cho việc kiểm tra lịch trùng. Chưa có AC cho EXC-02, EXC-03, ALT-01, ALT-02."* — Nguyễn Đức Bình · 08:52 03/09/2026

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin.
- **Trigger**: 
  - Quản trị viên chọn mục "Quản lí bài đăng" trên menu điều hướng của trang admin.

---

## Flow

### Main Flow
1. Admin truy cập màn hình danh sách các content.
2. Hệ thống hiển thị danh sách content để lên lịch đăng.
3. Admin chọn content muốn lên lịch.
4. Hệ thống hiển thị thông tin content và hình ảnh/media đã gắn với content để Admin kiểm tra.
5. Hệ thống hiển thị các nền tảng hiện đang được kết nối và có thể sử dụng.
6. Admin lựa chọn một hoặc nhiều nền tảng.
7. Admin chọn ngày và thời gian muốn đăng bài.
8. Hệ thống kiểm tra các điều kiện hợp lệ trước khi tạo:
   - Nội dung còn tồn tại, đã được lưu và không để trống.
   - Media còn tồn tại, đã được lưu và đúng định dạng hỗ trợ (JPG/JPEG/PNG, $\le$ 10MB, tối đa 10 ảnh).
   - Đã chọn ít nhất một nền tảng đang kết nối hợp lệ.
   - Ngày và giờ đăng lớn hơn thời điểm hiện tại.
   - Không có lịch đăng trùng nội dung, nền tảng và thời điểm với lịch đang ở trạng thái "Đã lên lịch".
9. Admin nhấn "Lên lịch".
10. Hệ thống tạo lịch đăng. Lịch được lưu với trạng thái: **"Đã lên lịch"**.
11. Hệ thống hiển thị thông báo: *"Lên lịch đăng bài thành công"*.

### Alternative Flow
- **ALT-01 — Admin thay đổi nền tảng trước khi lên lịch**:
  - Tại bước chọn nền tảng, Admin thay đổi nền tảng đã chọn (Facebook, Zalo, Instagram...).
  - Hệ thống cập nhật lựa chọn trên giao diện.
  - Admin tiếp tục thiết lập thời gian và nhấn lên lịch.
- **ALT-02 — Admin hủy tạo lịch**:
  - Trước khi nhấn "Lên lịch", Admin chọn "Hủy".
  - Hệ thống không tạo lịch và không gửi dữ liệu về Server.
  - Hệ thống quay về màn hình danh sách content.

### Exception Flow
- **EXC-01 — Dữ liệu lên lịch không hợp lệ**:
  - Không chọn nền tảng, hoặc không chọn thời gian đăng, hoặc thời gian đăng nhỏ hơn/bằng thời điểm hiện tại, hoặc content/media không còn hợp lệ.
  - Hệ thống hiển thị thông báo lỗi tại trường tương ứng và không tạo lịch.
- **EXC-02 — Lịch bị trùng với một lịch khác**:
  - Tại bước kiểm tra, nếu lịch trùng nội dung, thời gian và nền tảng với một lịch đang "Đã lên lịch".
  - Hệ thống không gửi dữ liệu về Server, hiển thị thông báo lỗi nêu rõ nền tảng và thời điểm đang bị trùng.
- **EXC-03 — Token của nền tảng hết hạn**:
  - Nếu thông tin xác thực của một nền tảng đã hết hạn khi gửi bài đăng.
  - Hệ thống không thực hiện đăng lên nền tảng đó, cập nhật trạng thái nền tảng thành "Đăng thất bại", và hiển thị thông báo yêu cầu Admin kết nối lại.
- **EXC-04 — Nền tảng trả về rate-limit**:
  - Nếu nền tảng từ chối request do vượt giới hạn gọi API.
  - Hệ thống hiển thị nút "Thử lại". Nếu đăng lại thành công $\rightarrow$ cập nhật "Đăng thành công". Nếu đã thử lại đủ số lần quy định nhưng vẫn thất bại $\rightarrow$ cập nhật "Đăng thất bại", lưu lý do lỗi và thông báo cho Admin.
- **EXC-05 — Nội dung bị nền tảng từ chối**:
  - Nền tảng từ chối bài viết do vi phạm chính sách nội dung/media.
  - Hệ thống không tự động retry, cập nhật nền tảng thành "Đăng thất bại", lưu lý do và thông báo cho Admin.

---

## Acceptance Criteria

- **AC-001 — Chỉ hiển thị content đủ điều kiện**:
  - **Given**: Admin đã đăng nhập vào trang admin.
  - **When**: Admin truy cập chức năng lên lịch đăng bài.
  - **Then**: Hệ thống chỉ hiển thị các content hợp lệ, đã lưu để Admin lựa chọn.

- **AC-002 — Chưa chọn nền tảng và bấm đăng**:
  - **Given**: Admin đã chọn content và thời gian đăng hợp lệ.
  - **When**: Admin chưa chọn bất kỳ nền tảng nào và nhấn "Lên lịch".
  - **Then**: Hệ thống không tạo lịch đăng.
  - **And**: Hiển thị thông báo yêu cầu Admin chọn ít nhất một nền tảng.

- **AC-003 — Tự động thực hiện đăng đúng lịch**:
  - **Given**: Một lịch đăng hợp lệ đang ở trạng thái "Đã lên lịch".
  - **When**: Đến thời gian đăng đã thiết lập.
  - **Then**: Hệ thống tự động thực hiện tác vụ đăng bài lên các nền tảng được chọn.
  - **And**: Admin không cần thực hiện thêm thao tác đăng thủ công.

- **AC-004 — Đăng thành công**:
  - **Given**: Lịch đã đến thời điểm đăng.
  - **When**: Tất cả nền tảng được chọn trả về kết quả đăng thành công.
  - **Then**: Hệ thống cập nhật từng nền tảng thành "Đăng thành công".
  - **And**: Cập nhật trạng thái tổng của lịch thành "Đăng thành công".

- **AC-005 — Kiểm tra lịch trùng**:
  - **Given**: Đã tồn tại một lịch đăng ở trạng thái "Đã lên lịch" với cùng Content, Nền tảng và Thời điểm đăng.
  - **When**: Admin thiết lập lịch đăng mới bị trùng và nhấn "Lên lịch".
  - **Then**: Hệ thống không tạo lịch đăng.
  - **And**: Hiển thị thông báo lỗi chỉ rõ nền tảng cùng thời điểm đang bị trùng.

- **AC-006 — Xử lý rate-limit từ nền tảng**:
  - **Given**: Hệ thống đang gọi API nền tảng để đăng bài theo lịch.
  - **When**: Nền tảng trả về mã lỗi rate-limit.
  - **Then**: Hệ thống hiển thị trạng thái và nút "Thử lại".
  - **And**: Khi Admin nhấn thử lại (hoặc auto-retry) thành công, cập nhật trạng thái "Đăng thành công".

- **AC-007 — Nội dung bị nền tảng từ chối**:
  - **Given**: Hệ thống gửi content và media đến nền tảng mạng xã hội.
  - **When**: Nền tảng từ chối bài viết do nội dung hoặc media vi phạm chính sách.
  - **Then**: Hệ thống không tự động retry.
  - **And**: Cập nhật trạng thái nền tảng thành "Đăng thất bại", lưu lý do từ chối và gửi thông báo cho Admin.

- **AC-008 — Kiểm tra dữ liệu hợp lệ trước khi tạo lịch**:
  - **Given**: Quản trị viên đang thiết lập lịch đăng bài.
  - **When**: Quản trị viên chọn "Lên lịch".
  - **Then**: Hệ thống chỉ tạo lịch khi:
    - Nội dung còn tồn tại, đã được lưu và không để trống.
    - Ảnh còn tồn tại, đúng định dạng (JPG/JPEG/PNG $\le$ 10MB, $\le$ 10 ảnh).
    - Đã chọn ít nhất một nền tảng đang kết nối hợp lệ.
    - Ngày và giờ đăng được nhập đầy đủ và lớn hơn thời điểm hiện tại.
  - **And**: Nếu có dữ liệu không hợp lệ, hệ thống không tạo lịch, hiển thị lỗi tại trường tương ứng và giữ nguyên dữ liệu đã nhập.

- **AC-009 — Thay đổi nền tảng trước khi lên lịch**:
  - **Given**: Admin đang trong giao diện thiết lập lịch đăng bài.
  - **When**: Admin chọn thêm hoặc bỏ chọn nền tảng và chưa nhấn "Lên lịch".
  - **Then**: Không có thông tin hay request nào được gửi về Server cho tới khi Admin nhấn "Lên lịch".

- **AC-010 — Hủy thiết lập lịch đăng**:
  - **Given**: Admin đang thiết lập lịch đăng bài.
  - **When**: Admin nhấn "Hủy".
  - **Then**: Hệ thống không tạo lịch, hủy bỏ thay đổi và quay về màn hình danh sách content.

- **AC-011 — Token của nền tảng bị hết hạn**:
  - **Given**: Hệ thống đã liên kết với các nền tảng mạng xã hội.
  - **When**: Token xác thực của một nền tảng bị hết hạn tại thời điểm chuẩn bị gửi bài.
  - **Then**: Hệ thống hiển thị thông báo lỗi hết hạn Token cho Admin và đề xuất Admin kết nối lại tài khoản nền tảng.

---

## References

### Business Rules
- [BR-001](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f47d6261-cae6-4ed8-b879-249b26d17464)
- [BR-002](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/4f9d052b-085c-404f-a824-3aba19aff7a1)
- [BR-003](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/829d7c04-6572-41e0-99ab-bde96247c61f)
- [BR-004](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0b6c451c-d250-4513-aa5d-7354e6e4537b)
- [BR-005](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/b69f2298-64eb-4355-ab2d-d8f18afdc46e)
- [BR-006](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/94ad04a1-10ed-4d41-9345-aa73112ca1cb)
- [BR-046](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a90db8e1-0a6f-4bac-90d1-637449f6c024)
- [BR-047](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f8a54fb3-59bc-4856-a49f-ad5155633da0)
- [BR-048](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/853f7ce5-9493-499a-8304-68c56403ac74)
- [BR-049](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/841ae103-814f-49d5-bba4-2f3a7e354756)

---

## Non-Functional
- Không có

---

## Out of Scope
- Không có
