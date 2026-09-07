# STORY-018: Xem content đã lưu lại

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xem danh sách và chi tiết content đã lưu để kiểm tra và sử dụng lại nội dung khi cần
- **Context**: Admin cần tra cứu các content và hashtag đã lưu trong quá trình tạo nội dung. Hệ thống phải hiển thị danh sách, hỗ trợ tìm kiếm và cho phép xem đầy đủ thông tin của từng content. Hệ thống chỉ lưu trữ và hiển thị phiên bản hiện tại mới nhất của từng content; không hỗ trợ quản lý lịch sử các phiên bản (Version History) hay khôi phục nội dung cũ. Chức năng này chỉ bao gồm xem dữ liệu đã lưu ở chế độ chỉ đọc (Read-only), không bao gồm tạo mới, chỉnh sửa, xóa, lên lịch hoặc đăng bài.
- **Sprint**: S2
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
  > *"Sai định dạng ở AC 007, 008, 009 Cần làm rõ Content có version history hay không?"* — Nguyễn Đức Bình · 09:57 03/09/2026

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - Quản trị viên có quyền truy cập vào chức năng quản lý nội dung.
- **Trigger**:
  - Quản trị viên chọn menu “Content đã lưu”.

---

## Flow

### Main Flow — Xem danh sách và chi tiết content đã lưu
1. Quản trị viên mở chức năng “Content đã lưu”.
2. Hệ thống truy vấn CSDL và hiển thị danh sách content đã lưu, mặc định sắp xếp theo thời gian cập nhật mới nhất (giảm dần).
3. Mỗi dòng/card content hiển thị: Tiêu đề, nội dung rút gọn (phần đầu văn bản được cắt gọn theo giới hạn hiển thị), danh sách hashtag, nền tảng mục tiêu và thời gian cập nhật gần nhất.
4. Quản trị viên chọn một bản ghi content trong danh sách để xem chi tiết.
5. Hệ thống truy xuất và hiển thị phiên bản mới nhất của content được chọn ở chế độ chỉ xem (Read-only).
6. Hệ thống hiển thị đầy đủ thông tin: Toàn bộ nội dung văn bản, danh sách hashtag đầy đủ, nền tảng, người tạo, ngày tạo và ngày cập nhật mới nhất.
7. Quản trị viên nhấn nút "Đóng" hoặc "Quay lại", hệ thống quay trở về màn hình danh sách và giữ nguyên các điều kiện tìm kiếm/lọc hiện tại.

### Alternative Flow
- **ALT-01 — Không có content nào trong danh sách**:
  - Tại bước 2, nếu hệ thống chưa có bản ghi content nào được lưu.
  - Hệ thống hiển thị giao diện trạng thái trống (Empty State) kèm thông điệp: *"Chưa có content nào được lưu"* và nút gợi ý dẫn tới chức năng *"Tự động viết content và hashtag"*.
- **ALT-02 — Tìm kiếm hoặc lọc content**:
  - Tại màn hình danh sách, Quản trị viên nhập từ khóa tìm kiếm (theo tiêu đề hoặc nội dung) hoặc áp dụng bộ lọc (theo Hashtag, Nền tảng, Khoảng thời gian cập nhật).
  - Hệ thống tự động lọc và hiển thị danh sách các content thỏa mãn điều kiện cùng số lượng kết quả tìm thấy.
  - Quản trị viên có thể xóa bộ lọc/từ khóa để hệ thống tải lại danh sách đầy đủ mặc định.

### Exception Flow
- **EXC-01 — Lỗi hệ thống khi tải danh sách**:
  - Tại bước 2, hệ thống không thể tải danh sách do lỗi kết nối mạng hoặc CSDL.
  - Hệ thống không hiển thị danh sách rỗng hay dữ liệu thiếu sót, mà hiển thị thông báo: *"Không thể tải danh sách content, vui lòng thử lại"* kèm nút "Tải lại".
- **EXC-02 — Content không còn tồn tại hoặc đã bị xóa**:
  - Tại bước 4, content được chọn đã bị xóa bởi quản trị viên khác trong phiên làm việc.
  - Hệ thống ngăn chặn mở trang chi tiết, hiển thị thông báo: *"Content không còn khả dụng hoặc đã bị xóa"* và tự động làm mới lại danh sách.
- **EXC-03 — Lỗi hệ thống khi tải chi tiết content**:
  - Tại bước 5, không thể tải chi tiết bản ghi do lỗi máy chủ.
  - Hệ thống giữ nguyên vị trí màn hình danh sách, hiển thị thông báo: *"Không thể tải chi tiết content, vui lòng thử lại"*.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách content theo thời gian cập nhật mới nhất**:
  - **Given**: Có ít nhất một content đã được lưu trong cơ sở dữ liệu.
  - **When**: Quản trị viên truy cập chức năng “Content đã lưu”.
  - **Then**: Hệ thống hiển thị danh sách các content được sắp xếp theo thời gian cập nhật mới nhất giảm dần.
  - **And**: Mỗi mục trong danh sách hiển thị đầy đủ: Tiêu đề, nội dung rút gọn, danh sách hashtag, nền tảng và thời gian cập nhật.

- **AC-002 — Xem chi tiết một content đã lưu**:
  - **Given**: Danh sách content đã lưu đang hiển thị.
  - **When**: Quản trị viên chọn một bản ghi content cụ thể.
  - **Then**: Hệ thống mở màn hình chi tiết và hiển thị phiên bản mới nhất của content ở chế độ chỉ xem.
  - **And**: Màn hình chi tiết hiển thị toàn bộ nội dung, danh sách hashtag, nền tảng, tác giả tạo, ngày tạo và ngày cập nhật.

- **AC-003 — Hiển thị trạng thái danh sách trống (ALT-01)**:
  - **Given**: Cơ sở dữ liệu chưa có bản ghi content nào được lưu.
  - **When**: Quản trị viên mở chức năng “Content đã lưu”.
  - **Then**: Hệ thống hiển thị thông báo: *"Chưa có content nào được lưu"*.
  - **And**: Hệ thống hiển thị nút điều hướng hướng dẫn Quản trị viên tạo nội dung mới.

- **AC-004 — Tìm kiếm content theo từ khóa (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình danh sách content đã lưu.
  - **When**: Quản trị viên nhập từ khóa tìm kiếm theo tiêu đề hoặc nội dung văn bản.
  - **Then**: Hệ thống lọc và hiển thị các bản ghi content có tiêu đề hoặc nội dung chứa từ khóa tương ứng.
  - **And**: Nếu không có kết quả phù hợp, hệ thống hiển thị thông báo: *"Không tìm thấy content phù hợp"*.

- **AC-005 — Lọc content theo nền tảng, hashtag hoặc thời gian (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình danh sách content đã lưu.
  - **When**: Quản trị viên chọn một hoặc nhiều tiêu chí lọc gồm: Hashtag, Nền tảng hoặc Khoảng thời gian cập nhật.
  - **Then**: Hệ thống hiển thị các content thỏa mãn tất cả các tiêu chí lọc được chọn cùng tổng số lượng bản ghi tìm thấy.
  - **And**: Khi Quản trị viên bấm nút "Đặt lại bộ lọc", hệ thống khôi phục hiển thị danh sách mặc định.

- **AC-006 — Xử lý lỗi khi không thể tải danh sách (EXC-01)**:
  - **Given**: Quản trị viên đang truy cập trang danh sách content đã lưu.
  - **When**: Quá trình truy vấn dữ liệu gặp lỗi kết nối hoặc sự cố máy chủ.
  - **Then**: Hệ thống hiển thị thông báo lỗi: *"Không thể tải danh sách content, vui lòng thử lại"*.
  - **And**: Hệ thống cung cấp nút "Tải lại" để Quản trị viên thực hiện lại thao tác.

- **AC-007 — Xử lý khi xem content không còn khả dụng (EXC-02)**:
  - **Given**: Quản trị viên bấm chọn một content trên danh sách.
  - **When**: Bản ghi content đó đã bị xóa hoặc không còn khả dụng trong cơ sở dữ liệu.
  - **Then**: Hệ thống không mở màn hình chi tiết, hiển thị thông báo: *"Content không còn khả dụng hoặc đã bị xóa"*.
  - **And**: Hệ thống tự động làm mới lại danh sách content.

- **AC-008 — Xử lý lỗi khi không thể tải chi tiết content (EXC-03)**:
  - **Given**: Quản trị viên bấm chọn một content trong danh sách.
  - **When**: Quá trình truy xuất dữ liệu chi tiết của bản ghi gặp sự cố máy chủ.
  - **Then**: Hệ thống giữ nguyên vị trí danh sách hiện tại và hiển thị thông báo: *"Không thể tải chi tiết content, vui lòng thử lại"*.

- **AC-009 — Duy trì trạng thái bộ lọc khi quay lại từ màn hình chi tiết**:
  - **Given**: Quản trị viên đã thực hiện tìm kiếm hoặc áp dụng bộ lọc và đang mở xem chi tiết một content.
  - **When**: Quản trị viên đóng màn hình chi tiết để quay lại danh sách.
  - **Then**: Hệ thống giữ nguyên từ khóa tìm kiếm, các giá trị bộ lọc đã chọn và vị trí trang hiện tại.

---

## References

### Business Rules
- [BR-023: Sắp xếp danh sách Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-023.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/d8931e9c-6c78-4fed-be86-63cc33ab6ee2))
- [BR-024: Xem chi tiết Content (Read-only)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-024.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/6d8234f3-f64e-4b81-8417-937717758ab5))
- [BR-030: Tìm kiếm Content theo từ khóa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-030.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/915eefb3-ff1d-42fe-b434-a0fbf3da89d6))
- [BR-031: Lọc Content theo Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-031.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/863f2aac-4f15-4862-8f19-d916ba861707))
- [BR-059: Bộ lọc Content đã lưu](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-059.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9fa969b5-cc09-4247-b9a0-852771d49c18))

---

## Non-Functional
- **Phân trang và tải dữ liệu**: Danh sách content hỗ trợ phân trang (Pagination) hoặc Lazy Load để tối ưu hiệu năng hiển thị khi có số lượng lớn bản ghi.
- **Tính an toàn dữ liệu**: Chế độ xem chi tiết là hoàn toàn chỉ đọc (Read-only), không làm thay đổi trạng thái hay thuộc tính của content.

---

## Out of Scope
- Không bao gồm tạo content mới (thuộc STORY-017).
- Không bao gồm chỉnh sửa nội dung hoặc hashtag (thuộc STORY-020).
- Không bao gồm xóa content đã lưu (thuộc STORY-019).
- Không bao gồm lên lịch hoặc đăng content lên mạng xã hội (thuộc STORY-002, STORY-016).
- Không bao gồm lưu trữ, xem hoặc khôi phục lịch sử các phiên bản (Version History) của content; hệ thống chỉ lưu trữ và hiển thị nội dung phiên bản hiện tại.
