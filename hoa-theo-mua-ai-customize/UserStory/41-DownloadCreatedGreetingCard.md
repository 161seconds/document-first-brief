# STORY-041: Tải xuống thiệp đã tạo (Download Created Greeting Card)

## Metadata
- **Story**: Là một khách hàng có thiệp AI đã được tạo ảnh thành công, tôi muốn tải file ảnh thiệp xuống thiết bị, để lưu trữ hoặc sử dụng bên ngoài hệ thống.
- **Context**: Khách hàng có thể truy cập màn hình Chi tiết thiệp từ:
  - Lịch sử tạo thiệp.
  - Chi tiết Order có liên kết với thiệp.
  - Chức năng tải xuống sử dụng chính file ảnh kết quả đã được tạo thành công trước đó.
  - Thao tác tải xuống:
    - Không gọi lại dịch vụ AI.
    - Không tạo ảnh mới.
    - Không tạo History item mới.
    - Không trừ quota AI.
    - Không làm thay đổi thiệp hoặc Order.
  - Một thiệp có thể: Chưa được liên kết với Order, được liên kết với một Order, hoặc được liên kết với nhiều Order do được tái sử dụng.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hoàng Thị Khánh Linh
- **Author**: Hoàng Thị Khánh Linh
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hoàng Thị Khánh Linh
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 17/08/2026)

## Conditions
- **Preconditions**:
  - Khách hàng đã đăng nhập.
  - History record của thiệp tồn tại và thuộc khách hàng hiện tại.
  - Thiệp đã có ít nhất một lần generate thành công và được ghi nhận trong History.
  - Trạng thái khả dụng của file được backend kiểm tra tại thời điểm tải xuống.
- **Trigger**: Khách hàng ấn chọn “Tải xuống thiệp” tại màn hình Chi tiết thiệp hoặc Chi tiết Order.

## Flow
### Main Flow
1. Khách hàng mở màn hình Chi tiết thiệp từ History hoặc từ một Order liên quan.
2. Hệ thống hiển thị thông tin thiệp và nút “Tải xuống thiệp”.
3. Khách hàng ấn chọn “Tải xuống thiệp”.
4. Backend kiểm tra History record của thiệp: Tồn tại, thuộc khách hàng hiện tại, có file ảnh output được ghi nhận.
5. Nếu thao tác được thực hiện từ Chi tiết Order, backend kiểm tra Order: Tồn tại, thuộc khách hàng hiện tại, có liên kết với thiệp đang được tải.
6. Backend kiểm tra file ảnh output còn khả dụng trong storage.
7. Hệ thống lấy chính file ảnh kết quả đã generate thành công.
8. Hệ thống xác định tên file theo ngữ cảnh tải xuống:
   - Nếu tải từ một Order cụ thể: sử dụng mã Order.
   - Nếu tải từ History chung: sử dụng mã thiệp.
9. Hệ thống trả file ảnh PNG cho khách hàng với tên file tương ứng.
10. Thiết bị của khách hàng bắt đầu quá trình tải file.
11. Hệ thống không: Gọi lại AI, tạo ảnh mới, sao chép file ảnh, tạo History item mới, trừ quota AI, hoặc thay đổi thiệp/Order.

### Alternative Flow
- **ALT-01 — Tải lại cùng một thiệp**:
  1. Khách hàng đã từng tải xuống một thiệp và file ảnh vẫn còn khả dụng.
  2. Khách hàng ấn chọn “Tải xuống thiệp” lần nữa.
  3. Backend thực hiện lại việc kiểm tra quyền sở hữu và trạng thái file.
  4. Hệ thống trả lại đúng file ảnh kết quả đã lưu; không giới hạn số lần tải xuống, không trừ quota và không tạo History item mới.
- **ALT-02 — Tải thiệp từ History chung**:
  1. Khách hàng mở Chi tiết thiệp từ History chung (không trong ngữ cảnh Order cụ thể).
  2. Khách hàng ấn chọn “Tải xuống thiệp”.
  3. Hệ thống sử dụng mã thiệp để đặt tên file: `thiep_[ma-thiep]_[yyyyMMdd_HHmmss].png`.
  4. Việc thiệp đã liên kết với một hoặc nhiều Order không làm thay đổi cách đặt tên trong trường hợp này.
- **ALT-03 — Tải thiệp từ Chi tiết Order**:
  1. Khách hàng đang xem một Order có liên kết với thiệp và mở tải thiệp trong ngữ cảnh Order đó.
  2. Backend kiểm tra Order thuộc khách hàng hiện tại và có liên kết với thiệp.
  3. Hệ thống sử dụng mã của Order đang xem để đặt tên file: `thiep_[ma-don-hang]_[yyyyMMdd_HHmmss].png`.
  4. Nếu thiệp được liên kết với nhiều Order, mã của các Order khác không được sử dụng trong lần tải này.

### Exception Flow
- **EXC-01 — File ảnh không còn khả dụng**: History record vẫn tồn tại nhưng file ảnh trong storage bị mất, hỏng hoặc không thể truy cập. Hệ thống giữ nguyên History record và metadata, không gọi AI tạo lại, vô hiệu hóa chức năng tải xuống và hiển thị thông báo: “Ảnh không còn khả dụng.”
- **EXC-02 — Không tìm thấy thiệp**: Khách hàng gửi yêu cầu tải xuống bằng mã thiệp hoặc URL nhưng không tìm thấy History record tương ứng. Hệ thống từ chối tải xuống, không gọi AI và hiển thị thông báo: “Không tìm thấy thiệp hoặc thiệp không còn tồn tại.”
- **EXC-03 — Khách hàng không có quyền tải thiệp**: Thiệp không thuộc khách hàng hiện tại. Backend từ chối request, không trả file ảnh, không trả storage URL hay metadata nhạy cảm, không tiết lộ sự tồn tại của thiệp và hiển thị: “Bạn không có quyền thực hiện thao tác này.”
- **EXC-04 — Order không hợp lệ với thiệp**: Khách hàng tải thiệp trong ngữ cảnh một Order nhưng Order không tồn tại, không thuộc khách hàng hoặc không có liên kết với thiệp. Backend từ chối tải theo ngữ cảnh Order và hiển thị: “Không thể tải thiệp từ đơn hàng này.”
- **EXC-05 — Lỗi trong quá trình cung cấp file**: Hệ thống gặp lỗi khi đọc hoặc truyền file. Hệ thống không trả file hỏng/thiếu, không gọi AI, không tạo History item mới, không trừ quota và thông báo: “Tải thiệp thất bại. Vui lòng thử lại.”

## Acceptance Criteria
### AC-001: Tải thiệp từ History chung
- **Given**: Thiệp thuộc khách hàng hiện tại, file còn khả dụng và khách hàng đang xem thiệp từ History chung.
- **When**: Khách hàng ấn chọn “Tải xuống thiệp”.
- **Then**: Hệ thống phải trả đúng file ảnh kết quả đã lưu.
- **And**: Tên file phải có định dạng: `thiep_[ma-thiep]_[yyyyMMdd_HHmmss].png`.

### AC-002: Tải thiệp từ Chi tiết Order
- **Given**: Order thuộc khách hàng hiện tại, Order có liên kết với thiệp và file ảnh thiệp còn khả dụng.
- **When**: Khách hàng tải thiệp trong ngữ cảnh Order đó.
- **Then**: Hệ thống phải trả đúng file ảnh kết quả đã lưu.
- **And**: Tên file phải có định dạng: `thiep_[ma-don-hang]_[yyyyMMdd_HHmmss].png`.

### AC-003: Thiệp được liên kết với nhiều Order
- **Given**: Một thiệp được liên kết với nhiều Order.
- **When**: Khách hàng tải thiệp từ Chi tiết một Order cụ thể.
- **Then**: Hệ thống phải sử dụng mã của Order đang xem để đặt tên file.
- **And**: Không sử dụng mã của Order khác.

### AC-004: Không gọi lại AI
- **Given**: Thiệp đã có file ảnh output.
- **When**: Khách hàng tải xuống thiệp.
- **Then**: Hệ thống không được gọi dịch vụ AI.
- **And**: Không được tạo ảnh mới.
- **And**: Không được tạo History item mới.
- **And**: Hệ thống không sao chép file ảnh trong storage.

### AC-005: Không ảnh hưởng quota
- **Given**: Khách hàng còn hoặc đã hết quota AI.
- **When**: Khách hàng tải hoặc tải lại thiệp.
- **Then**: Quota AI không được thay đổi.
- **And**: Trạng thái quota không được ngăn khách hàng tải file hợp lệ.

### AC-006: Tải lại không giới hạn
- **Given**: File ảnh còn khả dụng.
- **When**: Khách hàng tải lại cùng một thiệp nhiều lần.
- **Then**: Hệ thống phải tiếp tục cho phép tải xuống.
- **And**: Mỗi lần phải trả đúng file ảnh đã lưu.

### AC-007: File không còn khả dụng
- **Given**: History record còn tồn tại nhưng file ảnh bị mất, hỏng hoặc không thể truy cập.
- **When**: Khách hàng mở Chi tiết thiệp hoặc thực hiện tải xuống.
- **Then**: Hệ thống phải giữ nguyên History record.
- **And**: Hiển thị: “Ảnh không còn khả dụng.”
- **And**: Không trả file hoặc storage URL.
- **And**: Không tự động generate ảnh thay thế.

### AC-008: Không tìm thấy thiệp
- **Given**: History record được yêu cầu không tồn tại.
- **When**: Khách hàng gửi request tải xuống.
- **Then**: Hệ thống không được trả file.
- **And**: Không được gọi AI.
- **And**: Phải hiển thị trạng thái tài nguyên không tồn tại hoặc không thể truy cập.

### AC-009: Kiểm tra quyền sở hữu
- **Given**: Thiệp thuộc khách hàng khác.
- **When**: Khách hàng gọi API tải xuống bằng ID hoặc URL.
- **Then**: Backend phải từ chối request.
- **And**: Không được trả file, storage URL hoặc metadata nhạy cảm.

### AC-010: Kiểm tra liên kết Thiệp–Order
- **Given**: Khách hàng tải thiệp trong ngữ cảnh một Order và Order không có liên kết với thiệp đó.
- **When**: Backend xử lý request tải xuống.
- **Then**: Hệ thống từ chối tải file theo ngữ cảnh Order.
- **And**: Không được sử dụng mã Order để đặt tên file.

### AC-011: Lỗi cung cấp file
- **Given**: File được ghi nhận là còn khả dụng.
- **When**: Hệ thống gặp lỗi trong quá trình đọc hoặc truyền file.
- **Then**: Hệ thống phải thông báo tải thất bại.
- **And**: Cho phép khách hàng thử lại.
- **And**: Không làm thay đổi quota, History, thiệp hoặc Order.

### AC-012: Tên file an toàn
- **Given**: Mã thiệp hoặc mã Order chứa ký tự không an toàn cho tên file.
- **When**: Hệ thống tạo tên file tải xuống.
- **Then**: Hệ thống phải loại bỏ hoặc thay thế ký tự không an toàn.
- **And**: Tên file cuối cùng phải có phần mở rộng `.png`.

## References
- **Rules**:
  - [BR-101](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/d63586ff-117e-4ab3-9df5-762b91a601ad)
  - [BR-102](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/7d5b6967-1ae9-40e3-a1ca-a0133c0d73dc)
  - [BR-103](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/59e6dc23-cbc6-4e17-9b60-43ab083bba01)
  - [BR-104](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2d92e46f-82a0-4e33-89c4-f07a30ad3b17)
  - [BR-105](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/ace93ff9-8975-48dd-93a8-5a98dbc3d52e)
  - [BR-106](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/dcc6b70e-c0bd-4b31-b75c-5c3eb5ce7fd3)
  - [BR-107](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/ec5f4d36-7e58-4bb2-8061-cbb0b4a741c4)
  - [BR-108](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/f1950c70-8305-42a7-9b0d-7674e8b71f8d)
  - [BR-109](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/10c92b89-ea26-4783-bc04-5ed60e84d11b)
- **Dependencies**:
  - [STORY-035](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/54a960a5-99fa-4037-b1d8-7e931f2b7e43) / [35-GeneratePersonalizedCardWithAIAtCheckout.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/35-GeneratePersonalizedCardWithAIAtCheckout.md)
  - [STORY-036](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a9328fef-ec31-4e66-beb6-569d5383a338) / [36-RegeneratePersonalizedCardWithAI.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/36-RegeneratePersonalizedCardWithAI.md)
  - [STORY-039](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/fc14ad2e-0f75-40f9-806d-f935e31d34f6) / [39-CompleteCheckoutCreateAndPayOrder.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/39-CompleteCheckoutCreateAndPayOrder.md)

## Non-Functional
- Backend không được expose đường dẫn nội bộ, thông tin xác thực hoặc permanent storage URL.
- API metadata không được nhúng binary hoặc Base64 của file ảnh.
- File phải được cung cấp qua response download hoặc URL tạm thời có thời hạn ngắn.
- Ownership phải được kiểm tra chặt chẽ tại backend, không chỉ dựa vào trạng thái hiển thị của frontend.
- Với file còn khả dụng, hệ thống bắt đầu phản hồi download ở mức p95 ≤ 2 giây trong điều kiện vận hành bình thường, không tính thời gian truyền toàn bộ file đến thiết bị.
- Chức năng tải xuống không được kích hoạt AI hoặc xử lý ảnh không cần thiết.
- Tên file phải loại bỏ hoặc thay thế các ký tự không an toàn từ mã Order hoặc mã thiệp.
- Response phải khai báo đúng MIME type của file PNG (`image/png`).
- Lỗi của một file không được làm ảnh hưởng đến khả năng xem hoặc tải các thiệp khác.
- Backend phải chống truy cập trái phép bằng cách thay đổi ID thiệp hoặc ID Order trên request.

## Out of Scope
- Generate hoặc tạo lại thiệp.
- Chỉnh sửa nội dung hoặc hình ảnh thiệp.
- Xóa thiệp hoặc History record.
- Chia sẻ thiệp cho tài khoản khác.
- Khôi phục file ảnh đã mất hoặc hỏng.
- Chuyển đổi ảnh sang định dạng khác PNG.
- Tải xuống mẫu hoa Custom AI.
- Quản lý hoặc thay đổi Order.
- Tạo liên kết Thiệp–Order.
