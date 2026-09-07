# STORY-003: Tự động sinh ảnh đa tỷ lệ từ ảnh core

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn tải lên một ảnh core và yêu cầu hệ thống tự động tạo các phiên bản ảnh theo tỷ lệ phù hợp với từng nền tảng để có thể sử dụng cùng một thiết kế trên Facebook, Instagram và Zalo OA mà không cần chỉnh từng ảnh thủ công
- **Context**: Admin cần cung cấp ảnh core làm nguồn. Hệ thống sử dụng AI để điều chỉnh bố cục và mở rộng/cắt ảnh nhằm tạo các phiên bản tỷ lệ khác nhau, đồng thời phải giữ các yếu tố quan trọng như sản phẩm, logo và nội dung chính trong khu vực an toàn (safe zone). Admin có thể xem kết quả, tạo lại nếu chưa phù hợp và lưu các phiên bản mong muốn.
  - **Định dạng hỗ trợ**: JPG, JPEG, PNG
  - **Dung lượng**: Tối đa 10 MB
  - **Tỷ lệ hỗ trợ**:
    - `1:1` — 1080×1080 (Square - Instagram/Facebook Feed)
    - `4:5` — 1080×1350 (Portrait - Instagram Post)
    - `9:16` — 1080×1920 (Story / Reel / TikTok)
    - `16:9` — 1920×1080 (Landscape - Facebook Banner / Youtube)
    - `2:1` — 1200×600 (Zalo OA / Web Banner)
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

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin.
  - Hệ thống đã thiết lập cấu hình tỷ lệ ảnh cho các nền tảng.
- **Trigger**: 
  - Quản trị viên mở chức năng "Tự động sinh ảnh đa tỷ lệ".

---

## Flow

### Main Flow — Tạo và lưu ảnh đa tỷ lệ
1. Quản trị viên mở chức năng "Tạo ảnh đa tỷ lệ".
2. Hệ thống hiển thị khu vực tải ảnh nguồn và danh sách tỉ lệ được hỗ trợ.
3. Quản trị viên tải lên một ảnh nguồn (ảnh core).
4. Hệ thống kiểm tra định dạng, dung lượng, chiều rộng và tỉ lệ của ảnh.
5. Quản trị viên chọn một tỉ lệ muốn tạo.
6. Quản trị viên chọn "Tạo ảnh".
7. Hệ thống kiểm tra lại ảnh nguồn.
8. Hệ thống chuyển sang trạng thái "Đang tạo ảnh" và không cho phép gửi lại cùng một yêu cầu trong thời gian xử lý.
9. Hệ thống tạo một ảnh theo tỉ lệ đã chọn thông qua AI.
10. Hệ thống hiển thị ảnh kết quả trên giao diện xem trước.
11. Quản trị viên xem ảnh kết quả.
12. Quản trị viên chọn "Lưu".
13. Hệ thống hiển thị modal nhập tên và mô tả cho ảnh do AI trả về.
14. Quản trị viên nhập tên cho ảnh (bắt buộc, từ 1 đến 200 ký tự).
15. Quản trị viên nhập mô tả cho ảnh (không bắt buộc, tối đa 1000 ký tự).
16. Hệ thống lưu ảnh kết quả kèm thông tin: tên, tỉ lệ, kích thước, người tạo, thời gian tạo.
17. Hệ thống hiển thị thông báo: *"Lưu ảnh thành công"*.

### Alternative Flow
- **ALT-01 — Tạo lại ảnh**:
  - Tại bước 11 của Luồng chính, nếu chưa hài lòng với ảnh kết quả, Quản trị viên chọn "Tạo lại".
  - Hệ thống kiểm tra lại ảnh nguồn và tỉ lệ đã chọn.
  - Hệ thống chuyển sang trạng thái "Đang tạo ảnh".
  - Hệ thống tạo một ảnh mới theo cùng tỉ lệ.
  - Hệ thống thay thế ảnh đang hiển thị bằng ảnh vừa tạo lại (không lưu đè dở dang).
  - Quản trị viên tiếp tục từ bước 11 của Luồng chính.
- **ALT-02 — Thay đổi tỉ lệ trước khi tạo**:
  - Tại bước 5 của Luồng chính, Quản trị viên thay đổi tỉ lệ đã chọn.
  - Hệ thống bỏ lựa chọn trước đó và chỉ ghi nhận tỉ lệ được chọn cuối cùng.
  - Quản trị viên tiếp tục từ bước 6 của Luồng chính.
- **ALT-03 — Thay đổi ảnh nguồn**:
  - Trước khi chọn "Tạo ảnh", Quản trị viên tải lên ảnh nguồn khác.
  - Hệ thống thay thế ảnh nguồn đang chọn và kiểm tra lại ảnh nguồn mới.
  - Quản trị viên tiếp tục từ bước 5 của Luồng chính.

### Exception Flow
- **EXC-01 — Ảnh nguồn không hợp lệ**:
  - Tại bước 4, ảnh nguồn không đáp ứng điều kiện về định dạng, dung lượng (> 10MB), chiều rộng hoặc tỉ lệ.
  - Hệ thống chặn tiếp tục, hiển thị rõ điều kiện không hợp lệ để Quản trị viên tải lên ảnh nguồn khác.
- **EXC-02 — Chưa chọn tỉ lệ**:
  - Tại bước 6, nếu Quản trị viên chưa chọn tỉ lệ và bấm "Tạo ảnh", hệ thống không tạo ảnh và yêu cầu chọn một tỉ lệ được hỗ trợ.
- **EXC-03 — Tạo ảnh thất bại**:
  - Tại bước 9, nếu hệ thống hoặc AI Worker gặp lỗi không tạo được ảnh, hệ thống kết thúc trạng thái "Đang tạo ảnh", không hiển thị ảnh kết quả và thông báo: *"Không thể tạo ảnh. Vui lòng thử lại."*
- **EXC-04 — Lưu ảnh thất bại**:
  - Tại bước 16, nếu không thể lưu đầy đủ ảnh hoặc thông tin liên quan, hệ thống rollback (không lưu dữ liệu dở dang), không báo thành công mà thông báo: *"Không thể lưu ảnh. Vui lòng thử lại."*

---

## Acceptance Criteria

- **AC-001 — Mở màn hình chức năng**:
  - **Given**: Quản trị viên đã đăng nhập.
  - **When**: Quản trị viên mở chức năng "Tạo ảnh đa tỷ lệ".
  - **Then**: Hệ thống hiển thị khu vực tải một ảnh nguồn và danh sách tỉ lệ được hỗ trợ.

- **AC-002 — Giới hạn ảnh nguồn**:
  - **Given**: Quản trị viên đang sử dụng chức năng tạo ảnh đa tỷ lệ.
  - **When**: Quản trị viên tải ảnh nguồn lên.
  - **Then**: Hệ thống chỉ tiếp nhận đúng một ảnh nguồn tại một thời điểm.

- **AC-003 — Ảnh nguồn hợp lệ**:
  - **Given**: Ảnh nguồn đáp ứng đầy đủ điều kiện về định dạng (JPG, JPEG, PNG), dung lượng ($\le$ 10 MB), chiều rộng và tỉ lệ.
  - **When**: Hệ thống kiểm tra ảnh nguồn.
  - **Then**: Hệ thống cho phép Quản trị viên tiếp tục chọn tỉ lệ.

- **AC-004 — Ảnh nguồn không hợp lệ**:
  - **Given**: Ảnh nguồn không đáp ứng ít nhất một điều kiện về định dạng, dung lượng, chiều rộng hoặc tỉ lệ.
  - **When**: Hệ thống kiểm tra ảnh nguồn.
  - **Then**: Hệ thống không cho phép tiếp tục.
  - **And**: Hệ thống hiển thị thông báo lỗi nêu rõ điều kiện không hợp lệ.

- **AC-005 — Thay đổi ảnh nguồn trước khi tạo**:
  - **Given**: Quản trị viên đã tải lên một ảnh nguồn nhưng chưa chọn "Tạo ảnh".
  - **When**: Quản trị viên tải lên ảnh nguồn khác.
  - **Then**: Hệ thống thay thế ảnh nguồn đang chọn bằng ảnh mới.
  - **And**: Hệ thống kiểm tra tính hợp lệ của ảnh nguồn mới.

- **AC-006 — Chọn một tỉ lệ duy nhất**:
  - **Given**: Ảnh nguồn đã hợp lệ.
  - **When**: Quản trị viên lựa chọn tỉ lệ.
  - **Then**: Hệ thống chỉ cho phép chọn duy nhất một tỉ lệ tại một thời điểm.

- **AC-007 — Bỏ qua tỉ lệ cũ khi chọn lại**:
  - **Given**: Quản trị viên đã chọn một tỉ lệ trước đó.
  - **When**: Quản trị viên nhấn chọn một tỉ lệ khác.
  - **Then**: Hệ thống hủy chọn tỉ lệ trước đó và áp dụng tỉ lệ mới nhất.

- **AC-008 — Chưa chọn tỉ lệ và nhấn Tạo ảnh**:
  - **Given**: Ảnh nguồn hợp lệ nhưng Quản trị viên chưa chọn tỉ lệ.
  - **When**: Quản trị viên chọn "Tạo ảnh".
  - **Then**: Hệ thống không tạo ảnh.
  - **And**: Hệ thống yêu cầu Quản trị viên chọn một tỉ lệ được hỗ trợ.

- **AC-009 — Kiểm tra lại ảnh nguồn trước khi gọi AI**:
  - **Given**: Ảnh nguồn và tỉ lệ đã được chọn.
  - **When**: Quản trị viên chọn "Tạo ảnh".
  - **Then**: Hệ thống kiểm tra lại ảnh nguồn trước khi bắt đầu tạo ảnh.

- **AC-010 — Hiển thị trạng thái xử lý**:
  - **Given**: Ảnh nguồn và tỉ lệ đã chọn đều hợp lệ.
  - **When**: Hệ thống bắt đầu tạo ảnh.
  - **Then**: Hệ thống hiển thị trạng thái "Đang tạo ảnh".

- **AC-011 — Chống gửi yêu cầu trùng lặp (Idempotency)**:
  - **Given**: Một yêu cầu tạo ảnh đang được xử lý.
  - **When**: Quản trị viên gửi lại yêu cầu với cùng ảnh nguồn và cùng tỉ lệ.
  - **Then**: Hệ thống không tiếp nhận thêm yêu cầu trùng.

- **AC-012 — Hoàn tất tạo ảnh thành công**:
  - **Given**: Ảnh nguồn và tỉ lệ đã chọn đều hợp lệ.
  - **When**: Quá trình tạo ảnh hoàn tất.
  - **Then**: Hệ thống tạo đúng một ảnh theo tỉ lệ đã chọn.
  - **And**: Hệ thống hiển thị đúng một ảnh kết quả.

- **AC-013 — Tạo lại ảnh mới**:
  - **Given**: Quản trị viên chưa hài lòng với ảnh kết quả.
  - **When**: Quản trị viên chọn "Tạo lại".
  - **Then**: Hệ thống tạo một ảnh mới theo cùng tỉ lệ.
  - **And**: Hệ thống thay ảnh đang hiển thị bằng ảnh vừa tạo lại.
  - **And**: Hệ thống không giữ đồng thời cả ảnh cũ và ảnh mới trên vùng xem trước.

- **AC-014 — Xử lý lỗi khi tạo ảnh thất bại**:
  - **Given**: Hệ thống hoặc AI Worker không thể tạo ảnh.
  - **When**: Quá trình tạo ảnh kết thúc.
  - **Then**: Hệ thống kết thúc trạng thái "Đang tạo ảnh".
  - **And**: Hệ thống không hiển thị ảnh kết quả rác.
  - **And**: Hệ thống thông báo: *"Không thể tạo ảnh. Vui lòng thử lại."*

- **AC-015 — Mở modal nhập thông tin lưu ảnh**:
  - **Given**: Hệ thống đã tạo xong ảnh kết quả và đang hiển thị.
  - **When**: Quản trị viên bấm nút "Lưu".
  - **Then**: Hệ thống hiển thị modal nhập tên và mô tả cho ảnh.
  - **And**: Trường Tên là bắt buộc (từ 1 đến 200 ký tự).
  - **And**: Trường Mô tả là tùy chọn (tối đa 1000 ký tự).

- **AC-016 — Lưu ảnh và dữ liệu hoàn chỉnh**:
  - **Given**: Quản trị viên nhập thông tin hợp lệ trên modal và bấm "Lưu".
  - **When**: Quá trình lưu được thực hiện.
  - **Then**: Hệ thống lưu ảnh kết quả, tỉ lệ, kích thước, người tạo và thời gian tạo vào cơ sở dữ liệu.

- **AC-017 — Thông báo lưu thành công**:
  - **Given**: Ảnh kết quả và toàn bộ thông tin liên quan đã được lưu thành công.
  - **When**: Quá trình lưu hoàn tất.
  - **Then**: Hệ thống chỉ hiển thị thông báo: *"Lưu ảnh thành công"*.

- **AC-018 — Xử lý khi lưu thất bại**:
  - **Given**: Có lỗi xảy ra trong quá trình lưu ảnh hoặc metadata.
  - **When**: Quá trình lưu không hoàn tất.
  - **Then**: Hệ thống rollback dữ liệu, không ghi nhận dữ liệu dở dang.
  - **And**: Hệ thống thông báo: *"Không thể lưu ảnh. Vui lòng thử lại."*

---

## References

### Business Rules
- [BR-032](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/422cb56a-ad99-48c9-8598-7d518003905c)
- [BR-033](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a35a33f1-0c45-4939-97fd-6c9b231db023)
- [BR-034](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/975e2c2e-6677-4299-a158-632a1a8a7317)
- [BR-035](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9e4b0677-442e-4a96-9a9e-ff0f17eabf9f)
- [BR-036](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/bfeef817-f4cd-4491-b81b-8fde08a6a752)
- [BR-037](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/861df50a-8c52-4214-bd53-aaa6e06fd86a)
- [BR-050](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a3b24a75-f5c8-430c-8b8a-70d840b1ee7a)
- [BR-051](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/39702271-8c70-45ec-befc-8dd7e5b20aa0)

---

## Non-Functional
- Phải hiển thị trạng thái "Đang tạo ảnh" trong suốt thời gian AI xử lý.
- Không được ghi đè lên ảnh core gốc.
- Mỗi ảnh kết quả phải liên kết đúng với tỷ lệ tương ứng.
- Không cho phép gửi lặp nhiều yêu cầu trong khi cùng một yêu cầu đang được xử lý.

---

## Out of Scope
- Không có
