# STORY-022: Xóa ảnh đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa ảnh đã lưu không còn cần thiết để thư viện ảnh được quản lý gọn gàng và tránh sử dụng nhầm hình ảnh cũ
- **Context**: Quản trị viên cần loại bỏ các ảnh đã lưu không còn giá trị sử dụng. Trước khi xóa, hệ thống phải kiểm tra quyền, trạng thái sử dụng và hiển thị popup xác nhận để tránh mất dữ liệu ngoài ý muốn. Chức năng này chỉ bao gồm xóa ảnh đã lưu, không bao gồm tạo mới, xem, chỉnh sửa, tải xuống hoặc sử dụng ảnh trong bài đăng.
- **Quy định quan hệ cha - con và điều kiện xóa ảnh**:
  - **Quan hệ giữa ảnh gốc (Core Image) và các phiên bản tỷ lệ (Ratio Variants)**:
    - Ảnh tải lên hoặc ảnh gốc dùng làm nguồn sinh ảnh được coi là **Ảnh cha (Parent / Core Image)**.
    - Các ảnh được sinh tự động theo tỷ lệ (1:1, 4:5, 16:9,...) là **Ảnh con (Child Variants)** trực thuộc ảnh cha tương ứng.
    - **Khi xóa ảnh gốc (Ảnh cha)**: Hệ thống sẽ xóa đồng thời ảnh gốc cùng toàn bộ các biến thể con theo tỷ lệ liên kết với nó (Cascade Delete) trong một giao dịch thống nhất.
    - **Khi xóa một biến thể con riêng lẻ**: Cho phép xóa biến thể con được chọn nếu biến thể đó không bị ràng buộc; ảnh gốc cha và các biến thể con khác vẫn được giữ nguyên.
  - **Điều kiện cho phép xóa**:
    - Cả ảnh gốc và các biến thể con liên quan không bị bất kỳ bài đăng, lịch đăng bài nào tham chiếu hoặc đang sử dụng.
    - Nếu ảnh đang được sử dụng ở bất kỳ bài đăng nào, hệ thống từ chối xóa và yêu cầu Quản trị viên xử lý liên kết trước.
- **Sprint**: S3
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
  > *"Admin không được truy cập chức năng xóa ảnh nếu chưa đăng nhập hoặc tài khoản không có quyền quản lý thư viện ảnh. Câu này đặt ở Preconditions chưa hợp lý, Precondition là các điều phải đúng, phải có để thực thi chứ không chứa quy tắc. Cần làm rõ quan hệ cha-con của các loại ảnh được sinh ra từ ảnh gốc, định nghĩa rõ tụi nó có quan hệ với nhau không hay độc lập."* — Nguyễn Đức Bình · 10:13 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Tài khoản của Quản trị viên có quyền quản lý thư viện hình ảnh.
  - Bản ghi hình ảnh cần xóa còn tồn tại trong hệ thống.
- **Trigger**:
  - Quản trị viên nhấn nút “Xóa” tại một hình ảnh trên danh sách ảnh đã lưu hoặc tại màn hình xem chi tiết ảnh.

---

## Flow

### Main Flow — Xóa ảnh đã lưu (Happy Case)
1. Quản trị viên chọn nút “Xóa” tại hình ảnh cần xóa.
2. Hệ thống kiểm tra quyền hạn của Quản trị viên và kiểm tra tình trạng sử dụng của ảnh (xác nhận ảnh gốc và các biến thể con liên quan không bị tham chiếu bởi lịch đăng bài hoặc bài đăng nào).
3. Khi ảnh đủ điều kiện xóa, hệ thống hiển thị cửa sổ (popup) xác nhận gồm: Ảnh thu nhỏ, tên ảnh, số lượng biến thể tỷ lệ đi kèm (nếu là ảnh gốc) và cảnh báo: *"Thao tác xóa không thể hoàn tác. Bạn có chắc chắn muốn xóa ảnh này?"*.
4. Quản trị viên chọn nút “Xóa” trên cửa sổ xác nhận.
5. Hệ thống kiểm tra lại quyền hạn, sự tồn tại và ràng buộc tham chiếu của ảnh lần hai (Double-check trước khi xóa vật lý/dữ liệu).
6. Hệ thống thực hiện xóa ảnh, các biến thể con theo tỷ lệ liên kết và toàn bộ siêu dữ liệu liên quan khỏi cơ sở dữ liệu và kho lưu trữ tệp trong một Transaction duy nhất.
7. Hệ thống hiển thị thông báo: *"Xóa ảnh thành công"*.
8. Hệ thống đóng popup và loại bỏ ảnh khỏi danh sách hiển thị.

### Alternative Flow
- **ALT-01 — Quản trị viên hủy bỏ thao tác xóa**:
  - Tại bước 3, khi cửa sổ xác nhận đang mở, Quản trị viên chọn “Hủy” hoặc nhấp ra ngoài.
  - Hệ thống đóng cửa sổ xác nhận, không gửi lệnh xóa.
  - Ảnh gốc, các biến thể theo tỷ lệ và toàn bộ thông tin ảnh được giữ nguyên vẹn.
- **ALT-02 — Xóa ảnh từ màn hình chi tiết**:
  - Quản trị viên đang xem thông tin chi tiết của một ảnh và nhấn nút “Xóa”.
  - Hệ thống thực hiện quy trình kiểm tra và xác nhận tương tự luồng chính từ bước 2.
  - Sau khi xóa thành công, hệ thống chuyển hướng Quản trị viên quay trở lại danh sách ảnh đã lưu và duy trì các bộ lọc/từ khóa trước đó.

### Exception Flow
- **EXC-01 — Ảnh không còn tồn tại**:
  - Tại thời điểm Quản trị viên bấm xóa hoặc xác nhận, ảnh đã bị xóa bởi tài khoản khác.
  - Hệ thống dừng thao tác, không ghi nhận xóa thành công, thông báo: *"Ảnh không còn tồn tại trong hệ thống"* và tự động tải lại danh sách.
- **EXC-02 — Ảnh đang được sử dụng hoặc được tham chiếu**:
  - Hệ thống phát hiện ảnh gốc hoặc bất kỳ biến thể con nào đang được đính kèm trong bài đăng hoặc lịch đăng bài.
  - Hệ thống từ chối xóa, giữ nguyên dữ liệu ảnh và hiển thị thông báo lỗi: *"Không thể xóa ảnh do đang được sử dụng trong lịch đăng bài [Mã/Tên lịch]. Vui lòng gỡ ảnh khỏi lịch đăng trước khi xóa"*.
- **EXC-03 — Lỗi hệ thống khi xóa tệp hoặc dữ liệu CSDL**:
  - Quá trình xóa bản ghi CSDL hoặc xóa file trên bộ lưu trữ gặp sự cố.
  - Hệ thống rollback toàn bộ giao dịch, không xóa một phần dữ liệu, ghi log lỗi hệ thống.
  - Hệ thống thông báo: *"Không thể xóa ảnh, vui lòng thử lại"* và giữ nguyên hiện trạng ảnh.
- **EXC-04 — Phiên đăng nhập hết hạn khi xác nhận xóa**:
  - Phiên làm việc hết hạn trước khi Quản trị viên nhấn nút xác nhận trên popup.
  - Hệ thống từ chối yêu cầu xóa, thông báo phiên làm việc đã hết hạn và yêu cầu đăng nhập lại.
- **EXC-05 — Tình trạng ảnh thay đổi trước khi xác nhận**:
  - Popup xác nhận đang mở thì ảnh bị một lịch đăng bài khác tham chiếu.
  - Quản trị viên nhấn "Xóa" trên popup -> Hệ thống kiểm tra lần hai phát hiện ràng buộc mới, từ chối xóa và hiển thị cảnh báo trạng thái đã thay đổi kèm tải lại dữ liệu mới nhất.

---

## Acceptance Criteria

- **AC-001 — Hiển thị popup xác nhận xóa ảnh đủ điều kiện**:
  - **Given**: Ảnh còn tồn tại trong hệ thống và không bị dữ liệu khác tham chiếu.
  - **When**: Quản trị viên nhấn nút “Xóa”.
  - **Then**: Hệ thống hiển thị cửa sổ xác nhận gồm ảnh thu nhỏ, tên ảnh, số lượng biến thể tỷ lệ liên quan và cảnh báo thao tác không thể hoàn tác.
  - **And**: Ảnh chưa bị xóa và hệ thống chưa gửi yêu cầu xóa tới máy chủ.

- **AC-002 — Hủy bỏ thao tác xóa ảnh (ALT-01)**:
  - **Given**: Cửa sổ xác nhận xóa ảnh đang hiển thị.
  - **When**: Quản trị viên nhấn “Hủy” hoặc đóng cửa sổ.
  - **Then**: Hệ thống đóng cửa sổ xác nhận và không thực hiện xóa dữ liệu.
  - **And**: Ảnh gốc và các biến thể theo tỷ lệ được giữ nguyên vẹn.

- **AC-003 — Xóa ảnh gốc thành công kèm xóa cascade các biến thể con**:
  - **Given**: Ảnh gốc (Core Image) đủ điều kiện xóa và Quản trị viên mở popup xác nhận.
  - **When**: Quản trị viên nhấn “Xóa” trên popup xác nhận.
  - **Then**: Hệ thống xóa vĩnh viễn ảnh gốc cùng toàn bộ các biến thể con đa tỷ lệ liên kết khỏi CSDL và kho lưu trữ tệp trong một Transaction duy nhất.
  - **And**: Hệ thống hiển thị thông báo: *"Xóa ảnh thành công"* và loại bỏ ảnh khỏi danh sách hiển thị.

- **AC-004 — Xóa ảnh từ màn hình chi tiết (ALT-02)**:
  - **Given**: Quản trị viên đang xem chi tiết một ảnh đủ điều kiện xóa.
  - **When**: Quản trị viên chọn "Xóa" và xác nhận trên popup.
  - **Then**: Hệ thống xóa thành công ảnh và điều hướng Quản trị viên quay trở lại danh sách ảnh đã lưu.

- **AC-005 — Từ chối xóa khi ảnh đang được sử dụng (EXC-02)**:
  - **Given**: Ảnh hoặc ít nhất một biến thể tỷ lệ của ảnh đang được liên kết trong bài đăng hoặc lịch đăng bài.
  - **When**: Quản trị viên chọn "Xóa" hoặc xác nhận xóa ảnh đó.
  - **Then**: Hệ thống từ chối thao tác xóa, giữ nguyên toàn bộ ảnh và dữ liệu liên quan.
  - **And**: Hệ thống hiển thị thông báo chi tiết về bài đăng/lịch đăng đang tham chiếu cần được xử lý trước.

- **AC-006 — Đảm bảo tính nguyên tử khi quá trình xóa gặp sự cố (EXC-03)**:
  - **Given**: Quản trị viên đã xác nhận xóa ảnh đủ điều kiện.
  - **When**: Xảy ra lỗi trong quá trình xóa dữ liệu CSDL hoặc tệp lưu trữ.
  - **Then**: Hệ thống rollback toàn bộ, không có ảnh gốc hay biến thể con nào bị xóa dở dang một phần.
  - **And**: Hệ thống hiển thị thông báo: *"Không thể xóa ảnh, vui lòng thử lại"* và giữ nguyên dữ liệu để người dùng thao tác lại.

- **AC-007 — Xử lý khi ảnh không còn tồn tại (EXC-01)**:
  - **Given**: Ảnh đã bị xóa bởi tài khoản khác trước đó.
  - **When**: Quản trị viên nhấn "Xóa" hoặc xác nhận xóa ảnh đó.
  - **Then**: Hệ thống dừng thao tác, không ghi nhận xóa thành công, thông báo ảnh không còn tồn tại và tự động làm mới danh sách.

- **AC-008 — Xử lý kiểm tra lại trước khi xóa khi trạng thái thay đổi (EXC-05)**:
  - **Given**: Cửa sổ xác nhận xóa đang mở.
  - **When**: Ảnh bắt đầu được một lịch đăng bài tham chiếu trước khi Quản trị viên bấm xác nhận xóa.
  - **Then**: Hệ thống kiểm tra lại lần hai, từ chối xóa, thông báo tình trạng ảnh đã thay đổi và tải lại dữ liệu mới nhất.

- **AC-009 — Xử lý phiên đăng nhập hết hạn (EXC-04)**:
  - **Given**: Cửa sổ xác nhận xóa đang hiển thị.
  - **When**: Phiên đăng nhập hết hạn và Quản trị viên nhấn xác nhận xóa.
  - **Then**: Hệ thống từ chối yêu cầu, giữ nguyên toàn bộ dữ liệu và yêu cầu đăng nhập lại.

---

## References
- **Rules**:
  - [BR-039](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/590b277d-94d2-4b88-9adb-469ac97b7554)
  - [BR-065](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/69f06c2b-30d2-4cca-95bb-624a2f01b648)
- **Dependencies**:
  - STORY-003 (Tự động sinh ảnh đa tỷ lệ từ ảnh core - quan hệ phân cấp Parent Core Image và Child Variants).
  - STORY-002 / STORY-016 (Lên lịch đăng bài - ràng buộc tham chiếu ảnh).

---

## Non-Functional
- **Bảo mật và phân quyền phía Backend**: Máy chủ bắt buộc kiểm tra quyền quản lý thư viện ảnh của tài khoản và xác thực ràng buộc trước khi thực hiện xóa.
- **Tính toàn vẹn giao dịch và tệp tin**: Việc xóa siêu dữ liệu trong CSDL và xóa tệp vật lý trên Storage phải được đồng bộ thống nhất; nếu xóa thất bại, hệ thống không để lại tệp rác mồ côi (Orphan files).

---

## Out of Scope
- Không bao gồm tải ảnh mới lên hệ thống.
- Không bao gồm chỉnh sửa thông tin hoặc đồ họa của ảnh.
- Không bao gồm tính năng khôi phục ảnh đã xóa (Không hỗ trợ thùng rác Recycle Bin).
- Không bao gồm gán hoặc gỡ ảnh khỏi lịch đăng bài.
