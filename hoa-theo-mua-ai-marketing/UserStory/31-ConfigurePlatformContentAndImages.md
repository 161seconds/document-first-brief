# STORY-031: Cấu hình nội dung và hình ảnh theo từng nền tảng

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn chọn các nền tảng đăng bài và cấu hình System Prompt, tỷ lệ cùng hình ảnh riêng cho từng nền tảng để kết quả được tạo phù hợp với từng kênh.
- **Context**: Một bài đăng có thể được sử dụng trên Facebook, Instagram hoặc Zalo OA nhưng mỗi nền tảng có cách trình bày và tỷ lệ ảnh phù hợp khác nhau. Quản trị viên cần chọn nền tảng, xem hoặc điều chỉnh System Prompt, chọn tỷ lệ ảnh và gán ảnh nguồn cho từng tỷ lệ trước khi yêu cầu AI xử lý.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 11/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**:
  - Khác: Tân Trần
- **Creator**: Hồ Hoàng Nam
- **Thống kê tài liệu**: Rules: 5 | Unit Tests: 0 | System Tests: 0

---

## Conditions

### Preconditions
- Quản trị viên đã hoàn thành thông tin bài đăng gốc.
- Hệ thống đã cấu hình các nền tảng được hỗ trợ.
- Các nền tảng có thể sử dụng đang kết nối hợp lệ.

### Trigger
- Quản trị viên chuyển đến khu vực cấu hình nền tảng của bài đăng.

---

## Flow

### Main Flow — Cấu hình nền tảng
1. Hệ thống hiển thị danh sách nền tảng được hỗ trợ.
2. Quản trị viên chọn một hoặc nhiều nền tảng.
3. Hệ thống hiển thị trạng thái cấu hình của từng nền tảng được chọn.
4. Quản trị viên mở cấu hình của một nền tảng.
5. Hệ thống hiển thị System Prompt và danh sách tỷ lệ ảnh được hỗ trợ.
6. Nếu bài đăng có ảnh nguồn, Quản trị viên có thể chọn một hoặc nhiều tỷ lệ ảnh.
7. Với mỗi tỷ lệ đã chọn, Quản trị viên chọn ảnh nguồn cần sử dụng.
8. Hệ thống lưu lựa chọn trong phiên làm việc và cho phép Quản trị viên cấu hình nền tảng tiếp theo.
9. Quản trị viên chọn “Tạo bài đăng”.
10. Hệ thống kiểm tra cấu hình của tất cả nền tảng đã chọn.
11. Hệ thống chuyển sang bước tạo nội dung và hình ảnh bằng AI.

### Alternative Flow

#### ALT-01 — Quản trị viên thay đổi nền tảng đã chọn
- Quản trị viên chọn thêm hoặc bỏ chọn một nền tảng.
- Hệ thống cập nhật danh sách nền tảng sẽ được xử lý.
- Dữ liệu của các nền tảng còn được chọn được giữ nguyên.

#### ALT-02 — Cấu hình nền tảng cho bài đăng không có ảnh
- Bài đăng gốc không có ảnh nguồn.
- Hệ thống hiển thị khu vực cấu hình ảnh ở trạng thái không có ảnh nguồn.
- Hệ thống không bắt buộc Quản trị viên chọn tỷ lệ hoặc gán ảnh.
- Quản trị viên tiếp tục cấu hình System Prompt và các nền tảng khác.
- Hệ thống cho phép chuyển sang bước tạo kết quả nếu các dữ liệu bắt buộc còn lại hợp lệ.

#### ALT-03 — Quản trị viên xem hoặc điều chỉnh System Prompt
- Quản trị viên chọn “System prompt nền tảng”.
- Hệ thống hiển thị nội dung System Prompt hiện tại.
- Quản trị viên xem hoặc cập nhật nội dung trong phạm vi được phép.
- Hệ thống sử dụng nội dung đã xác nhận cho lần tạo kết quả tiếp theo.

### Exception Flow

#### EXC-01 — Quản trị viên chưa chọn nền tảng
- Hệ thống không chuyển sang bước tạo kết quả.
- Hệ thống yêu cầu chọn ít nhất một nền tảng.

#### EXC-02 — Cấu hình nền tảng chưa đầy đủ
- Khi bài đăng có ảnh nguồn, hệ thống xác định nền tảng có tỷ lệ đã chọn nhưng chưa được gán ảnh nguồn hoặc có cấu hình ảnh không hợp lệ.
- Hệ thống không cho phép tạo kết quả đối với nền tảng có cấu hình ảnh không hợp lệ.
- Hệ thống chỉ rõ nền tảng và nội dung cần bổ sung.
- Quản trị viên có thể hoàn thiện cấu hình và thử lại.

#### EXC-03 — Nền tảng không còn kết nối hợp lệ
- Hệ thống không gửi yêu cầu xử lý đến nền tảng đó.
- Hệ thống thông báo trạng thái kết nối không hợp lệ.
- Cấu hình của các nền tảng khác được giữ nguyên.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách nền tảng hỗ trợ**:
  - **Given**: Quản trị viên đã hoàn thành dữ liệu bài đăng gốc.
  - **When**: Quản trị viên mở khu vực cấu hình nền tảng.
  - **Then**: Hệ thống hiển thị các nền tảng được hỗ trợ cùng trạng thái lựa chọn.
  - **And**: Quản trị viên có thể chọn một hoặc nhiều nền tảng.

- **AC-002 — Hiển thị System Prompt và tỷ lệ ảnh tương ứng**:
  - **Given**: Một nền tảng đã được chọn.
  - **When**: Quản trị viên mở cấu hình nền tảng.
  - **Then**: Hệ thống hiển thị System Prompt và các tỷ lệ ảnh được hỗ trợ.
  - **And**: Cấu hình của nền tảng khác không bị thay đổi.

- **AC-003 — Gán ảnh nguồn cho tỷ lệ đã chọn**:
  - **Given**: Bài đăng có ảnh nguồn và Quản trị viên đã chọn một tỷ lệ.
  - **When**: Quản trị viên chọn ảnh từ thư viện ảnh nguồn.
  - **Then**: Hệ thống gán các ảnh được chọn cho đúng nền tảng và tỷ lệ.
  - **And**: Hệ thống hiển thị số lượng ảnh đã gán.

- **AC-004 — Cho phép cấu hình bài đăng không có ảnh nguồn**:
  - **Given**: Bài đăng gốc không có ảnh nguồn.
  - **When**: Quản trị viên cấu hình một nền tảng.
  - **Then**: Hệ thống không bắt buộc chọn tỷ lệ hoặc gán ảnh nguồn.
  - **And**: Quản trị viên vẫn có thể chuyển sang bước tạo Content nếu cấu hình còn lại hợp lệ.

- **AC-005 — Yêu cầu chọn ít nhất một nền tảng**:
  - **Given**: Quản trị viên chưa chọn nền tảng.
  - **When**: Quản trị viên chọn “Tạo bài đăng”.
  - **Then**: Hệ thống không gửi yêu cầu tạo kết quả.
  - **And**: Hệ thống yêu cầu chọn ít nhất một nền tảng.

- **AC-006 — Chuyển tiếp dữ liệu hợp lệ sang bước AI**:
  - **Given**: Tất cả nền tảng được chọn có cấu hình hợp lệ.
  - **When**: Quản trị viên chọn “Tạo bài đăng”.
  - **Then**: Hệ thống chuyển dữ liệu của từng nền tảng sang bước tạo kết quả.
  - **And**: Chỉ các nền tảng được chọn được xử lý.

- **AC-007 — Bắt buộc chọn ảnh nguồn cho tỷ lệ đã chọn**:
  - **Given**: Bài đăng có ảnh nguồn và Quản trị viên đã chọn một tỷ lệ.
  - **When**: Tỷ lệ đó chưa được gán ảnh nguồn.
  - **Then**: Hệ thống xác định cấu hình ảnh chưa hoàn chỉnh.
  - **And**: Hệ thống yêu cầu chọn ít nhất một ảnh nguồn cho tỷ lệ trước khi tạo kết quả.

- **AC-008 — Cho phép chỉ tạo Content khi không cấu hình ảnh**:
  - **Given**: Bài đăng có ảnh nguồn nhưng Quản trị viên không yêu cầu tạo ảnh cho một nền tảng.
  - **When**: Các dữ liệu bắt buộc để tạo Content của nền tảng đã hợp lệ.
  - **Then**: Hệ thống cho phép tiếp tục tạo Content và hashtag.
  - **And**: Hệ thống không gửi yêu cầu tạo hình ảnh cho nền tảng đó.

---

## References

### Rules
- [BR-034: Hỗ trợ các tỷ lệ sinh ảnh AI](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-034.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/975e2c2e-6677-4299-a158-632a1a8a7317))
- [BR-041: Độ dài System Prompt](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-041.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/7fcca618-4169-41e1-9191-35ad76d245d6))
- [BR-042: Định dạng text của System Prompt](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-042.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8e47c852-d79c-4345-946a-e3fd36e9ab53))
- [BR-045: Xử lý xung đột khi nhiều người sửa Prompt (Optimistic Locking)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-045.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/aa2f2bab-03a7-4455-a2e5-c638749cb6f4))
- [BR-052: Không tạo phiên bản Prompt khi nội dung không thay đổi](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-052.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0bc478ae-bb1f-4f44-ba13-46f161d807de))

### Dependencies
- [STORY-026: Nhập nội dung và hình ảnh cho bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/26-InputPostContentAndImages.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/88a3de35-75de-430c-b102-de509ef54eb2))

---

## Non-Functional

- Việc mở hoặc đóng khu vực cấu hình không được làm mất dữ liệu đã nhập.
- Dữ liệu cấu hình phải được tách riêng theo từng nền tảng.
- Việc cấu hình tỷ lệ và ảnh nguồn chỉ bắt buộc khi Quản trị viên yêu cầu tạo hình ảnh bằng AI.
- Nếu bài đăng không có ảnh nguồn, Quản trị viên không phải chọn tỷ lệ hoặc gán ảnh.
- Một nền tảng vẫn được xem là cấu hình hợp lệ khi không có cấu hình ảnh nhưng có dữ liệu cần thiết để tạo Content.
- Nếu đã chọn một tỷ lệ thì phải gán ít nhất một ảnh nguồn hợp lệ cho tỷ lệ đó.
- Việc bỏ cấu hình ảnh của một nền tảng không làm thay đổi System Prompt hoặc cấu hình của nền tảng khác.

---

## Out of Scope

- Không tạo kết quả AI trong Story này.
- Không đăng hoặc lên lịch bài đăng trong Story này.
- Không quản lý kết nối tài khoản nền tảng.
