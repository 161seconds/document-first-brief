# STORY-028: Tạo nội dung và hình ảnh đa nền tảng bằng AI

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn hệ thống sử dụng nội dung gốc và cấu hình của từng nền tảng để tạo content, hashtag và hình ảnh phù hợp, giúp tôi chuẩn bị bài đăng đa nền tảng trong một quy trình thống nhất.
- **Context**: Sau khi Quản trị viên hoàn thành bài đăng gốc và cấu hình nền tảng, hệ thống cần tạo kết quả riêng cho Facebook, Instagram hoặc Zalo OA. Content, hashtag và hình ảnh phải bám theo dữ liệu nguồn, System Prompt cùng tỷ lệ đã chọn của từng nền tảng. Kết quả của các nền tảng được xử lý độc lập.
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
  - Khác: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Thống kê tài liệu**: Rules: 13 | Unit Tests: 0 | System Tests: 0

---

## Conditions

### Preconditions
- Bài đăng gốc có tiêu đề và nội dung hợp lệ hoặc có hình ảnh hợp lệ.
- Ít nhất một nền tảng đã được chọn và cấu hình hợp lệ.
- System Prompt của nền tảng đã được xác định.

### Trigger
- Quản trị viên chọn “Tạo bài đăng” sau khi hoàn tất cấu hình nền tảng.

---

## Flow

### Main Flow — Tạo nội dung và hình ảnh đa nền tảng
1. Hệ thống kiểm tra dữ liệu bài đăng gốc và cấu hình của từng nền tảng.
2. Hệ thống tạo một yêu cầu xử lý riêng cho từng nền tảng đã chọn.
3. Hệ thống sử dụng nội dung gốc và System Prompt tương ứng để tạo content cùng hashtag cho từng nền tảng.
4. Nếu nền tảng có cấu hình ảnh, hệ thống sử dụng ảnh nguồn và tỷ lệ đã chọn để tạo các phương án hình ảnh.
5. Hệ thống lưu các kết quả vào phiên tạo tương ứng của từng nền tảng.
6. Hệ thống ghi nhận trạng thái xử lý riêng cho từng nền tảng.
7. Hệ thống hiển thị các kết quả đã tạo trên màn hình xem trước.
8. Hệ thống không tự động đăng bài khi quá trình tạo hoàn tất.

### Alternative Flow

#### ALT-01 — Bài đăng được tạo không có hình ảnh
- Hệ thống chỉ tạo content và hashtag cho từng nền tảng.
- Khu vực kết quả hình ảnh hiển thị không có ảnh.
- Quản trị viên vẫn có thể tiếp tục xem lại và phát hành bài đăng.

#### ALT-02 — Một nền tảng chưa được cấu hình trước khi mở màn hình xem trước
- Hệ thống hiển thị nền tảng ở trạng thái chưa được cấu hình.
- Quản trị viên hoàn thiện cấu hình ngay trên màn hình xem trước.
- Hệ thống tạo kết quả riêng cho nền tảng vừa được cấu hình.

#### ALT-03 — Quản trị viên tạo lại content hoặc một ảnh theo feedback
- Khi tạo lại content và hashtag, Quản trị viên có thể nhập feedback tối đa 500 ký tự.
- Hệ thống sử dụng feedback cùng content hiện tại, System Prompt tạo content và System Prompt cấu hình của nền tảng để tạo phiên bản content và hashtag mới.
- Khi tạo lại một ảnh, Quản trị viên có thể nhập feedback riêng cho ảnh được chọn.
- Hệ thống sử dụng feedback cùng ảnh được chọn, nền tảng và tỷ lệ hiện tại để tạo một ảnh mới.
- Content, hashtag hoặc ảnh mới được thêm vào đúng nền tảng; kết quả cũ không bị ghi đè hoặc xóa.
- Nếu feedback để trống, hệ thống vẫn tạo lại dựa trên dữ liệu và cấu hình hiện tại.

### Exception Flow

#### EXC-01 — Dữ liệu đầu vào hoặc cấu hình nền tảng không còn hợp lệ
- Hệ thống không gửi yêu cầu AI cho nền tảng có dữ liệu không hợp lệ.
- Hệ thống hiển thị lý do và nội dung cần điều chỉnh.
- Dữ liệu hợp lệ của các nền tảng khác được giữ nguyên.

#### EXC-02 — AI không phản hồi hoặc không thể tạo kết quả cho một nền tảng
- Hệ thống ghi nhận nền tảng ở trạng thái thất bại.
- Hệ thống không hiển thị kết quả chưa hoàn chỉnh như kết quả hợp lệ.
- Kết quả thành công của các nền tảng khác được giữ nguyên.
- Quản trị viên có thể thử lại riêng nền tảng thất bại.

#### EXC-03 — Kết quả AI thiếu content, hashtag hoặc hình ảnh theo cấu hình bắt buộc
- Hệ thống đánh dấu kết quả không hợp lệ.
- Hệ thống không cho phép sử dụng kết quả đó để đăng.
- Quản trị viên có thể yêu cầu tạo lại.

---

## Acceptance Criteria

- **AC-001 — Xử lý riêng biệt từng nền tảng được chọn**:
  - **Given**: Bài đăng gốc và cấu hình của các nền tảng đã chọn đều hợp lệ.
  - **When**: Quản trị viên chọn “Tạo bài đăng”.
  - **Then**: Hệ thống tạo yêu cầu xử lý riêng cho từng nền tảng.
  - **And**: Chỉ các nền tảng được chọn được xử lý.

- **AC-002 — Tạo content và hashtag bám theo System Prompt**:
  - **Given**: Một nền tảng có System Prompt hợp lệ.
  - **When**: AI tạo kết quả cho nền tảng đó.
  - **Then**: Hệ thống tạo content và hashtag phù hợp với dữ liệu gốc cùng System Prompt.
  - **And**: Nội dung gốc không bị thay đổi.

- **AC-003 — Tạo hình ảnh theo đúng tỷ lệ cấu hình**:
  - **Given**: Một nền tảng có ảnh nguồn và tỷ lệ đã được cấu hình.
  - **When**: Hệ thống tạo hình ảnh.
  - **Then**: Hệ thống tạo các phương án ảnh theo đúng tỷ lệ đã chọn.
  - **And**: Kết quả được liên kết đúng nền tảng.

- **AC-004 — Cô lập lỗi giữa các nền tảng**:
  - **Given**: Một nền tảng tạo kết quả thất bại nhưng nền tảng khác thành công.
  - **When**: Quá trình xử lý hoàn tất.
  - **Then**: Hệ thống hiển thị kết quả và trạng thái riêng của từng nền tảng.
  - **And**: Lỗi của một nền tảng không làm mất kết quả thành công của nền tảng khác.

- **AC-005 — Giữ lịch sử phiên bản khi tạo lại**:
  - **Given**: Nền tảng đã có ít nhất một kết quả.
  - **When**: Quản trị viên yêu cầu tạo lại.
  - **Then**: Hệ thống tạo thêm một phiên bản mới cho nền tảng đó.
  - **And**: Kết quả trước đó vẫn được giữ trong lịch sử phiên bản.

- **AC-006 — Chặn tự động phát hành bài đăng**:
  - **Given**: Quá trình tạo kết quả đã hoàn tất.
  - **When**: Hệ thống hiển thị màn hình xem trước.
  - **Then**: Không có bài đăng nào được tự động phát hành.
  - **And**: Quản trị viên phải chọn phương thức phát hành ở bước tiếp theo.

---

## References

### Rules
- [BR-017: Giới hạn số lượng hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-017.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f2b1c31e-65cf-4fe1-be04-b4fe035b0779))
- [BR-018: Định dạng bắt đầu của Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-018.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8b93f9ae-a327-48be-8e1d-78f40bdd9c23))
- [BR-019: Không chứa khoảng trắng trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-019.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/2dd59a4b-f4ed-489b-8788-ab3684cd7da4))
- [BR-020: Độ dài cho phép của một Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-020.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/8c0f117b-25fe-4070-9e3f-682a955ddd4c))
- [BR-021: Ký tự hợp lệ trong Hashtag](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-021.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/af6d57ef-0350-4009-a454-7f8f04f37eaf))
- [BR-022: Loại bỏ Hashtag trùng lặp](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-022.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/1b8d95f0-954e-48d3-8a45-fee5ddcfe830))
- [BR-025: Giới hạn độ dài nội dung Content](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-025.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/421d8701-2950-41f8-908f-4a6838c5b7cb))
- [BR-032: Định dạng file ảnh gốc (Core Image)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-032.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/422cb56a-ad99-48c9-8598-7d518003905c))
- [BR-033: Dung lượng ảnh gốc tối đa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-033.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a35a33f1-0c45-4939-97fd-6c9b231db023))
- [BR-034: Hỗ trợ các tỷ lệ sinh ảnh AI](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-034.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/975e2c2e-6677-4299-a158-632a1a8a7317))
- [BR-035: Ràng buộc giữ vùng an toàn (Safe Zone)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-035.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9e4b0677-442e-4a96-9a9e-ff0f17eabf9f))
- [BR-050: Một yêu cầu sinh ảnh chỉ có một ảnh nguồn và một tỷ lệ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-050.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a3b24a75-f5c8-430c-8b8a-70d840b1ee7a))
- [BR-057: Kết quả AI hợp lệ trước khi sử dụng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-057.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/918f53c2-dd63-4962-98d7-b4383ae5cf2e))

### Dependencies
- [STORY-026: Nhập nội dung và hình ảnh cho bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/26-InputPostContentAndImages.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/88a3de35-75de-430c-b102-de509ef54eb2))
- [STORY-031: Cấu hình nội dung và hình ảnh theo từng nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/31-ConfigurePlatformContentAndImages.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/7b27c7df-20fb-4067-8815-ccd529e80cdb))

---

## Non-Functional

- Yêu cầu của từng nền tảng phải được xử lý độc lập.
- Hệ thống không được hiển thị dữ liệu tạo dở dang như một kết quả hợp lệ.
- Feedback là không bắt buộc.
- Feedback tối đa 500 ký tự và được loại bỏ khoảng trắng thừa ở đầu/cuối.
- Feedback content chỉ áp dụng cho lần tạo lại content và hashtag hiện tại.
- Feedback ảnh chỉ áp dụng cho đúng ảnh được chọn.
- Sau khi tạo lại thành công, hệ thống xóa nội dung trong ô feedback.
- Feedback không được làm thay đổi nền tảng hoặc tỷ lệ của ảnh được chọn.

---

## Out of Scope

- Không quản lý kết nối tài khoản nền tảng.
- Không tự động đăng bài sau khi tạo kết quả.
- Không xóa các phiên bản đã tạo trước đó.
