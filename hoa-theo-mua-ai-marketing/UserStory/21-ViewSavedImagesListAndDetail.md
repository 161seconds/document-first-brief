# STORY-021: Xem ảnh đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem danh sách và chi tiết ảnh đã lưu để kiểm tra và lựa chọn hình ảnh phù hợp khi cần
- **Context**: Quản trị viên cần tra cứu các ảnh đã lưu từ quá trình tải lên hoặc do AI tự động sinh (STORY-003). Hệ thống phải hiển thị ảnh thu nhỏ, thông tin cơ bản và bản xem chi tiết truy cập ở chế độ chỉ đọc (Read-only). Đối với ảnh do AI sinh ra tự động từ ảnh core (STORY-003), hệ thống tự động gán tên mặc định theo mã định danh hoặc tên ảnh gốc. Để phục vụ tìm kiếm toàn diện, hệ thống hỗ trợ tìm kiếm theo tên ảnh, mã định danh hoặc thẻ (tags) đính kèm. Chức năng này chỉ bao gồm xem dữ liệu ảnh đã lưu, không bao gồm tạo mới, chỉnh sửa, xóa, tải xuống hoặc sử dụng ảnh trong bài đăng.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Nháp
- **Cập nhật**: 04/09/2026
- **Author**: Nguyễn Anh Quân
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"NFR và Out of Scope không có? Ảnh do STORY-003 sinh ra không có tên nhưng tìm kiếm ở đây chỉ theo tên? Cái này thuộc SP S1"* — Nguyễn Đức Bình · 10:02 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Quản trị viên có quyền truy cập vào thư viện hình ảnh.
- **Trigger**:
  - Quản trị viên chọn menu “Ảnh đã lưu”.

---

## Flow

### Main Flow — Xem danh sách và chi tiết ảnh đã lưu
1. Quản trị viên mở chức năng “Ảnh đã lưu”.
2. Hệ thống tải danh sách ảnh thuộc phạm vi Quản trị viên được phép truy cập.
3. Hệ thống sắp xếp danh sách theo thời gian cập nhật từ mới nhất đến cũ nhất.
4. Hệ thống hiển thị dạng lưới (Grid) hoặc danh sách, mỗi ảnh bao gồm: Ảnh thu nhỏ (thumbnail), tên ảnh (hoặc tên mặc định được sinh tự động), tỷ lệ khung hình (1:1, 4:5, 16:9,...), kích thước chiều rộng x chiều cao (px), định dạng tệp và thời gian cập nhật.
5. Quản trị viên chọn một ảnh trong danh sách.
6. Hệ thống kiểm tra ảnh còn tồn tại và tải bản xem lớn cùng toàn bộ thông tin chi tiết.
7. Hệ thống hiển thị bản xem lớn và các thông tin: Tên ảnh, mô tả, tỷ lệ ảnh, kích thước, định dạng tệp, dung lượng tệp, nguồn tạo (tải lên / AI sinh từ STORY-003), người tạo, ngày tạo, người cập nhật, ngày cập nhật và danh sách các phiên bản theo tỷ lệ liên kết hiện có.
8. Thông tin chi tiết được hiển thị ở chế độ chỉ xem (Read-only).
9. Quản trị viên đóng màn hình chi tiết. Hệ thống quay lại danh sách và giữ nguyên từ khóa tìm kiếm cùng các điều kiện lọc trước đó.

### Alternative Flow
- **ALT-01 — Không có ảnh đã lưu trong hệ thống**:
  - Hệ thống không tìm thấy ảnh nào trong cơ sở dữ liệu.
  - Hệ thống hiển thị giao diện trạng thái trống (Empty State) kèm hướng dẫn Quản trị viên tải lên hoặc sinh ảnh từ AI.
- **ALT-02 — Tìm kiếm ảnh theo tên, mã định danh hoặc thẻ (tags)**:
  - Quản trị viên nhập từ khóa tìm kiếm vào ô tìm kiếm.
  - Hệ thống tìm kiếm và hiển thị các ảnh có tên, mã định danh (ID) hoặc thẻ gắn liền phù hợp với từ khóa.
  - Kết quả được sắp xếp theo thời gian cập nhật từ mới nhất đến cũ nhất.
- **ALT-03 — Lọc danh sách ảnh theo tiêu chí**:
  - Quản trị viên chọn một hoặc nhiều điều kiện lọc gồm: Tỷ lệ ảnh, định dạng tệp, khoảng thời gian lưu hoặc người tạo/cập nhật.
  - Hệ thống áp dụng đồng thời các điều kiện và hiển thị danh sách ảnh phù hợp.
- **ALT-04 — Không có kết quả phù hợp khi tìm kiếm/lọc**:
  - Không có ảnh nào thỏa mãn từ khóa hoặc bộ lọc đã chọn.
  - Hệ thống hiển thị thông báo: *"Không tìm thấy hình ảnh phù hợp"*, giữ nguyên bộ lọc/từ khóa để Quản trị viên điều chỉnh.
- **ALT-05 — Xóa điều kiện tìm kiếm và lọc**:
  - Quản trị viên bấm nút "Đặt lại" hoặc xóa ô tìm kiếm và các bộ lọc.
  - Hệ thống tải lại danh sách ảnh mặc định ban đầu.

### Exception Flow
- **EXC-01 — Không thể tải danh sách ảnh**:
  - Hệ thống gặp sự cố mạng hoặc máy chủ không thể tải dữ liệu danh sách ảnh.
  - Hệ thống không hiển thị danh sách lỗi hay dữ liệu thiếu, hiển thị thông báo: *"Không thể tải danh sách ảnh, vui lòng thử lại"* kèm nút tải lại.
- **EXC-02 — Ảnh không còn khả dụng khi mở chi tiết**:
  - Ảnh đã bị xóa bởi quản trị viên khác trước khi người dùng mở chi tiết.
  - Hệ thống không hiển thị chi tiết, thông báo: *"Ảnh không còn khả dụng hoặc đã bị xóa"* và tự động làm mới lại danh sách.
- **EXC-03 — Không thể tải thông tin chi tiết hoặc file ảnh gốc**:
  - Không thể tải tệp hình ảnh lớn hoặc dữ liệu chi tiết của ảnh đã chọn.
  - Hệ thống giữ nguyên trạng thái danh sách, hiển thị thông báo: *"Không thể tải thông tin chi tiết của ảnh, vui lòng thử lại"*.
- **EXC-04 — Lỗi không tải được ảnh thu nhỏ (Thumbnail)**:
  - Tệp thumbnail bị lỗi hoặc đường dẫn ảnh hỏng.
  - Hệ thống hiển thị hình ảnh thay thế (Placeholder / Fallback Image) tại vị trí đó, các thông tin và ảnh khác vẫn hiển thị bình thường.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc hết hạn khi đang xem danh sách hoặc chi tiết ảnh.
  - Hệ thống dừng tải dữ liệu được bảo vệ, hiển thị thông báo phiên hết hạn và yêu cầu đăng nhập lại.
- **EXC-06 — Không thể tải kết quả tìm kiếm hoặc lọc**:
  - Quá trình tìm kiếm/lọc gặp lỗi máy chủ.
  - Hệ thống giữ nguyên từ khóa và bộ lọc, thông báo: *"Không thể tải kết quả, vui lòng thử lại"*.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách ảnh đã lưu**:
  - **Given**: Có ít nhất một ảnh đã lưu trong hệ thống.
  - **When**: Quản trị viên mở chức năng “Ảnh đã lưu”.
  - **Then**: Hệ thống hiển thị danh sách ảnh được sắp xếp theo thời gian cập nhật mới nhất giảm dần.
  - **And**: Mỗi thẻ ảnh hiển thị đầy đủ: Ảnh thu nhỏ, tên ảnh, tỷ lệ ảnh, kích thước chiều rộng x chiều cao, định dạng tệp và thời gian cập nhật.

- **AC-002 — Xem chi tiết ảnh đã lưu**:
  - **Given**: Danh sách ảnh đã lưu đang hiển thị và ảnh được chọn còn tồn tại.
  - **When**: Quản trị viên chọn một ảnh cụ thể.
  - **Then**: Hệ thống hiển thị bản xem lớn của ảnh và toàn bộ thông tin chi tiết ở chế độ chỉ xem (Read-only).
  - **And**: Thông tin gồm: Tên ảnh, mô tả, tỷ lệ ảnh, kích thước, định dạng tệp, dung lượng, nguồn tạo (Tải lên / AI sinh), tác giả, ngày tạo, ngày cập nhật và danh sách các phiên bản đa tỷ lệ liên kết.

- **AC-003 — Đóng chi tiết và giữ nguyên trạng thái danh sách**:
  - **Given**: Quản trị viên đang mở xem chi tiết một ảnh từ danh sách đang có bộ lọc hoặc từ khóa tìm kiếm.
  - **When**: Quản trị viên đóng màn hình chi tiết.
  - **Then**: Hệ thống quay lại danh sách và giữ nguyên từ khóa tìm kiếm, các giá trị bộ lọc cùng vị trí cuộn trang trước đó.

- **AC-004 — Hiển thị trạng thái không có ảnh (ALT-01)**:
  - **Given**: Hệ thống chưa có ảnh nào được lưu.
  - **When**: Quản trị viên mở chức năng “Ảnh đã lưu”.
  - **Then**: Hệ thống hiển thị giao diện danh sách trống kèm hướng dẫn tạo hoặc tải lên ảnh.
  - **And**: Trạng thái này không được hiển thị như một lỗi hệ thống.

- **AC-005 — Tìm kiếm ảnh theo tên, mã định danh hoặc thẻ (ALT-02)**:
  - **Given**: Danh sách ảnh đã lưu đang hiển thị.
  - **When**: Quản trị viên nhập từ khóa tìm kiếm theo tên ảnh, ID hoặc thẻ và nhấn tìm kiếm.
  - **Then**: Hệ thống hiển thị các ảnh có tên, ID hoặc thẻ phù hợp với từ khóa, sắp xếp theo thời gian cập nhật mới nhất.

- **AC-006 — Lọc danh sách ảnh theo nhiều tiêu chí (ALT-03)**:
  - **Given**: Danh sách ảnh đã lưu đang hiển thị.
  - **When**: Quản trị viên chọn một hoặc nhiều điều kiện lọc (tỷ lệ ảnh, định dạng tệp, khoảng thời gian, người tạo).
  - **Then**: Hệ thống hiển thị danh sách các ảnh đáp ứng đồng thời tất cả các điều kiện lọc đã chọn.

- **AC-007 — Hiển thị thông báo khi không có kết quả tìm kiếm/lọc (ALT-04)**:
  - **Given**: Quản trị viên đã nhập từ khóa hoặc áp dụng bộ lọc.
  - **When**: Không có ảnh nào thỏa mãn điều kiện.
  - **Then**: Hệ thống hiển thị thông báo: *"Không tìm thấy hình ảnh phù hợp"*, đồng thời giữ nguyên các giá trị tìm kiếm để người dùng điều chỉnh.

- **AC-008 — Đặt lại điều kiện tìm kiếm và lọc (ALT-05)**:
  - **Given**: Danh sách đang được lọc hoặc tìm kiếm.
  - **When**: Quản trị viên bấm nút đặt lại bộ lọc.
  - **Then**: Hệ thống xóa toàn bộ từ khóa và tiêu chí lọc, tải lại danh sách ảnh mặc định ban đầu.

- **AC-009 — Xử lý lỗi khi không thể tải danh sách ảnh (EXC-01)**:
  - **Given**: Quản trị viên đang mở trang danh sách ảnh.
  - **When**: Hệ thống gặp lỗi kết nối không thể tải dữ liệu.
  - **Then**: Hệ thống hiển thị thông báo: *"Không thể tải danh sách ảnh, vui lòng thử lại"* và cung cấp nút tải lại.

- **AC-010 — Xử lý ảnh không còn khả dụng (EXC-02)**:
  - **Given**: Ảnh đã bị xóa trước thời điểm Quản trị viên bấm mở chi tiết.
  - **When**: Quản trị viên chọn ảnh đó từ danh sách.
  - **Then**: Hệ thống không mở màn hình chi tiết, hiển thị thông báo ảnh không còn khả dụng và tự động làm mới danh sách.

- **AC-011 — Xử lý lỗi không tải được ảnh thu nhỏ (EXC-04)**:
  - **Given**: Danh sách ảnh đang hiển thị.
  - **When**: Tệp ảnh thu nhỏ của một ảnh bị lỗi đường dẫn hoặc không tải được.
  - **Then**: Hệ thống hiển thị ảnh placeholder thay thế tại vị trí tương ứng; các ảnh khác và thông tin văn bản vẫn hiển thị bình thường.

- **AC-012 — Xử lý phiên đăng nhập hết hạn (EXC-05)**:
  - **Given**: Quản trị viên đang xem danh sách hoặc chi tiết ảnh.
  - **When**: Phiên đăng nhập hết hạn.
  - **Then**: Hệ thống ngừng truy xuất dữ liệu ảnh, hiển thị thông báo phiên hết hạn và yêu cầu đăng nhập lại.

---

## References

### Business Rules
- [BR-038: Sắp xếp danh sách Ảnh lưu trữ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-038.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a474fc1f-80af-466d-b19c-a026976db2d2))
- [BR-063: Tìm kiếm và lọc ảnh đã lưu](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-063.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f377e9a9-7114-47cc-885c-6b0934720ec7))
- [BR-064: Chi tiết ảnh ở chế độ chỉ xem](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-064.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1a495c4d-33e8-423a-9116-ce2b21e362e4))

---

## Non-Functional
- **Hiệu năng hiển thị**: Danh sách ảnh phải hiển thị trong vòng 2 giây trong điều kiện hoạt động bình thường.
- **Tải ảnh chi tiết**: Chi tiết ảnh và bản xem lớn phải hiển thị trong vòng 2 giây sau khi Quản trị viên chọn ảnh.
- **Tối ưu băng thông**: Sử dụng kỹ thuật Lazy Loading và phân trang (Pagination) để chỉ tải thumbnail các ảnh hiển thị trong khung nhìn (Viewport), tránh tải ồ ạt toàn bộ thư viện ảnh.

---

## Out of Scope
- Không bao gồm tải ảnh mới lên hệ thống.
- Không bao gồm chỉnh sửa đồ họa của ảnh (cắt xén, xoay, chỉnh sửa màu sắc,...).
- Không bao gồm xóa ảnh đã lưu (thuộc STORY-022).
- Không bao gồm sửa thông tin mô tả/thẻ ảnh (thuộc STORY-023).
- Không bao gồm gán ảnh vào bài đăng hoặc lên lịch đăng bài (thuộc STORY-002, STORY-016).
- Không bao gồm quản lý hoặc khôi phục lịch sử các phiên bản tệp ảnh.
