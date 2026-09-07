# STORY-019: Xóa content đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa content đã lưu không còn cần thiết để danh sách được quản lý gọn gàng và tránh sử dụng nhầm nội dung cũ
- **Context**: Admin cần loại bỏ các content đã lưu không còn giá trị sử dụng. Trước khi xóa, hệ thống phải kiểm tra quyền, trạng thái sử dụng và hiển thị popup xác nhận để tránh mất dữ liệu ngoài ý muốn. Chức năng này chỉ bao gồm xóa content đã lưu, không bao gồm tạo mới, xem, chỉnh sửa, lên lịch hoặc đăng bài.
- **Quy định điều kiện xóa content**:
  - Content **đủ điều kiện xóa** khi và chỉ khi:
    - Content không nằm trong bất kỳ bài đăng nào đã được lên lịch (trạng thái "Đã lên lịch" hoặc "Đang đăng").
    - Content không nằm trong bài đăng nháp hoặc lịch đăng bài chưa được thực thi.
    - Content không bị tham chiếu bởi bất kỳ đối tượng nghiệp vụ nào khác trong hệ thống.
  - Content **không được phép xóa** nếu đang được liên kết hoặc tham chiếu bởi ít nhất một lịch đăng bài hoặc bài đăng.
- **Sprint**: S2
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
  > *"Chưa có AC cho trường hợp xoá content hợp lệ - happy case. Cần định nghĩa rõ content đủ điều kiện xoá là thế nào."* — Nguyễn Đức Bình · 09:59 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị với quyền quản lý nội dung.
  - Content đã lưu còn tồn tại trong hệ thống.
- **Trigger**:
  - Quản trị viên nhấn nút “Xóa” tại một dòng trong danh sách content đã lưu hoặc tại màn hình chi tiết content.

---

## Flow

### Main Flow — Xóa content đã lưu (Happy Case)
1. Quản trị viên chọn nút “Xóa” tại bản ghi content cần xóa (từ danh sách hoặc từ màn hình chi tiết).
2. Hệ thống kiểm tra quyền hạn của Quản trị viên và kiểm tra trạng thái sử dụng của content (đảm bảo không bị tham chiếu bởi lịch đăng bài hoặc bài đăng nào).
3. Khi content đủ điều kiện xóa, hệ thống hiển thị popup xác nhận xóa bao gồm: Tên/tiêu đề content và cảnh báo: *"Thao tác này không thể hoàn tác. Bạn có chắc chắn muốn xóa content này?"*.
4. Quản trị viên nhấn nút “Xóa” trên popup để xác nhận.
5. Hệ thống kiểm tra lại quyền và trạng thái sử dụng của content lần hai ngay trước khi thực thi lệnh xóa (Double-check).
6. Hệ thống thực hiện xóa bản ghi content và toàn bộ hashtag liên kết trong cơ sở dữ liệu theo một giao dịch thống nhất (Database Transaction).
7. Hệ thống hiển thị thông báo: *"Xóa content thành công"*, đóng popup và loại bỏ bản ghi content đó khỏi danh sách hiển thị.

### Alternative Flow
- **ALT-01 — Quản trị viên hủy bỏ thao tác xóa**:
  - Tại bước 3, khi popup xác nhận đang hiển thị, Quản trị viên nhấn nút “Hủy” hoặc click ra ngoài popup.
  - Hệ thống đóng popup, không thực hiện bất kỳ truy vấn xóa nào.
  - Bản ghi content và hashtag liên kết được giữ nguyên trạng thái.
- **ALT-02 — Xóa từ màn hình chi tiết content**:
  - Quản trị viên đang xem màn hình chi tiết content và nhấn nút “Xóa”.
  - Hệ thống tiến hành quy trình kiểm tra và xác nhận tương tự như luồng chính từ bước 2.
  - Sau khi xóa thành công, hệ thống điều hướng Quản trị viên quay trở lại màn hình danh sách content đã lưu.

### Exception Flow
- **EXC-01 — Content không còn tồn tại**:
  - Tại bước 2 hoặc bước 5, content đã bị xóa bởi tài khoản khác trước đó.
  - Hệ thống dừng thao tác, không ghi nhận xóa, hiển thị thông báo: *"Content không còn tồn tại trong hệ thống"* và tự động tải lại danh sách.
- **EXC-02 — Content đang được sử dụng hoặc được tham chiếu**:
  - Tại bước 2 hoặc bước 5, hệ thống phát hiện content đang được gán vào một lịch đăng bài hoặc bài đăng.
  - Hệ thống từ chối xóa, giữ nguyên dữ liệu content và hiển thị thông báo lỗi: *"Không thể xóa: Content đang được sử dụng trong lịch đăng bài [Tên lịch/Mã lịch]. Vui lòng hủy lịch đăng trước khi xóa"*.
- **EXC-03 — Lỗi hệ thống khi xóa dữ liệu**:
  - Tại bước 6, xảy ra lỗi mạng hoặc CSDL trong quá trình xóa content/hashtag.
  - Hệ thống rollback toàn bộ giao dịch, không xóa một phần dữ liệu, ghi log lỗi hệ thống.
  - Hệ thống hiển thị thông báo: *"Không thể xóa content, vui lòng thử lại"* và giữ nguyên dữ liệu.
- **EXC-04 — Phiên đăng nhập hết hạn khi xác nhận xóa**:
  - Phiên làm việc của Quản trị viên hết hạn trước khi bấm xác nhận trên popup.
  - Hệ thống từ chối xóa dữ liệu, hiển thị thông báo phiên làm việc đã hết hạn và yêu cầu đăng nhập lại.
- **EXC-05 — Trạng thái content thay đổi trước khi xóa**:
  - Popup xác nhận đang hiển thị thì content bị đối tượng khác tham chiếu (ví dụ: một lịch đăng vừa được tạo gán content này).
  - Quản trị viên nhấn "Xóa" trên popup -> Hệ thống kiểm tra lần 2 phát hiện điều kiện không còn thỏa mãn.
  - Hệ thống từ chối xóa, hiển thị cảnh báo: *"Trạng thái content đã thay đổi, không còn đủ điều kiện xóa"* và tải lại thông tin mới nhất.

---

## Acceptance Criteria

- **AC-001 — Xóa content đủ điều kiện thành công (Happy Case)**:
  - **Given**: Content tồn tại trong hệ thống và đủ điều kiện xóa (không nằm trong bài đăng hoặc lịch đăng nào).
  - **When**: Quản trị viên nhấn nút "Xóa" tại content và nhấn nút "Xóa" trên popup xác nhận.
  - **Then**: Hệ thống xóa vĩnh viễn content và các hashtag liên quan trong một giao dịch duy nhất.
  - **And**: Hệ thống hiển thị thông báo: *"Xóa content thành công"* và loại bỏ content khỏi danh sách hiển thị.

- **AC-002 — Hủy thao tác xóa (ALT-01)**:
  - **Given**: Popup xác nhận xóa content đang hiển thị trên màn hình.
  - **When**: Quản trị viên nhấn nút "Hủy" hoặc đóng popup.
  - **Then**: Hệ thống đóng popup xác nhận và không gửi lệnh xóa đến máy chủ.
  - **And**: Content và dữ liệu hashtag liên quan được giữ nguyên vẹn.

- **AC-003 — Xóa content từ màn hình chi tiết (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình chi tiết của một content đủ điều kiện xóa.
  - **When**: Quản trị viên nhấn nút "Xóa" và xác nhận xóa trên popup.
  - **Then**: Hệ thống xóa thành công content, thông báo thành công và tự động chuyển hướng Quản trị viên về màn hình danh sách content đã lưu.

- **AC-004 — Từ chối xóa khi content đang được sử dụng (EXC-02)**:
  - **Given**: Content đang được liên kết với ít nhất một lịch đăng bài hoặc bài đăng.
  - **When**: Quản trị viên nhấn "Xóa" tại content đó.
  - **Then**: Hệ thống từ chối thao tác xóa và giữ nguyên dữ liệu.
  - **And**: Hệ thống hiển thị thông báo rõ ràng về việc content đang được sử dụng kèm thông tin đối tượng tham chiếu cần xử lý trước.

- **AC-005 — Xử lý khi content không còn tồn tại (EXC-01)**:
  - **Given**: Content đã bị xóa bởi thao tác khác trước đó.
  - **When**: Quản trị viên nhấn "Xóa" hoặc xác nhận xóa content đó.
  - **Then**: Hệ thống dừng thao tác, không ghi nhận xóa thành công.
  - **And**: Hệ thống hiển thị thông báo: *"Content không còn tồn tại"* và tự động làm mới lại danh sách.

- **AC-006 — Đảm bảo tính nguyên tử khi xóa gặp lỗi (EXC-03)**:
  - **Given**: Quản trị viên đã xác nhận xóa một content đủ điều kiện.
  - **When**: Quá trình xóa gặp sự cố kỹ thuật hoặc lỗi cơ sở dữ liệu.
  - **Then**: Hệ thống rollback toàn bộ, không để xảy ra tình trạng xóa mất một phần dữ liệu (content bị xóa nhưng hashtag còn hoặc ngược lại).
  - **And**: Hệ thống hiển thị thông báo lỗi: *"Không thể xóa content, vui lòng thử lại"* và cho phép Quản trị viên thao tác lại.

- **AC-007 — Kiểm tra trạng thái thay đổi trước khi xóa (EXC-05)**:
  - **Given**: Popup xác nhận xóa đang hiển thị.
  - **When**: Content phát sinh liên kết mới (được gán vào lịch đăng bài) trước thời điểm Quản trị viên bấm nút xác nhận xóa.
  - **Then**: Hệ thống kiểm tra lần hai, phát hiện vi phạm điều kiện và từ chối xóa.
  - **And**: Hệ thống hiển thị cảnh báo trạng thái đã thay đổi và làm mới lại dữ liệu hiển thị.

- **AC-008 — Xử lý phiên đăng nhập hết hạn khi xác nhận xóa (EXC-04)**:
  - **Given**: Quản trị viên đang mở popup xác nhận xóa.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên nhấn nút xác nhận xóa.
  - **Then**: Hệ thống chặn yêu cầu xóa, giữ nguyên dữ liệu và hiển thị thông báo yêu cầu đăng nhập lại.

---

## References

### Business Rules
- [BR-028: Ràng buộc dữ liệu khi xóa Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-028.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f57b8dc5-f3cc-49ad-9833-41018e846869))
- [BR-029: Xóa toàn vẹn dữ liệu Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-029.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/efe66a56-d882-4eb1-b50c-dbe53e03197d))
- [BR-060: Kiểm tra lại và ghi nhận thao tác xóa Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-060.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/be5d785f-b838-4112-b336-bfd9bef893a6))

---

## Non-Functional
- **Kiểm tra an toàn phía Backend**: Phía Backend bắt buộc phải kiểm tra quyền của Quản trị viên và xác minh trạng thái tham chiếu của content trước khi thực thi lệnh DELETE.
- **Tính toàn vẹn giao dịch (ACID)**: Content và dữ liệu hashtag liên kết phải được xóa trong cùng một Database Transaction; nếu xảy ra lỗi phải rollback toàn bộ.

---

## Out of Scope
- Không bao gồm tạo content mới (thuộc STORY-017).
- Không bao gồm xem danh sách hoặc chi tiết content (thuộc STORY-018).
- Không bao gồm chỉnh sửa nội dung hoặc hashtag (thuộc STORY-020).
- Không bao gồm lên lịch hoặc đăng bài (thuộc STORY-002, STORY-016).
