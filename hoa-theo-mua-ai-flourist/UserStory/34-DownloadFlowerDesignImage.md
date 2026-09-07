# STORY-034: Tải ảnh mẫu hoa sau khi AI tạo (Download AI-Generated Flower Design Image)

## Metadata
- **Story**: Là một khách hàng đã đăng nhập, tôi muốn tải xuống mẫu hoa AI đã được tạo thành công, để lưu ảnh về thiết bị và sử dụng cho mục đích tham khảo hoặc chia sẻ sau này.
- **Context**: Khách hàng có thể tải xuống mẫu hoa AI từ:
  - Màn hình Kết quả bó hoa AI (kết quả mới nhất).
  - Màn hình Kết quả bó hoa AI được mở từ Lịch sử tạo (các kết quả cũ).
  - Chức năng tải xuống áp dụng cho kết quả AI mới nhất và các kết quả cũ trong lịch sử, miễn là kết quả đó đã được tạo thành công.
  - Mỗi lần tải xuống chỉ tải 1 ảnh về thiết bị.
  - Ảnh tải xuống được chuẩn hóa:
    - **Định dạng**: PNG.
    - **Chất lượng**: Giữ nguyên resolution và chất lượng gốc, không nén hay cắt giảm.
    - **Logo**: Là file kết quả chính thức đã được hệ thống gắn logo sau khi AI tạo ảnh hợp lệ và trước khi lưu kết quả; chức năng tải xuống không gắn lại logo.
    - **Tên file**: Theo định dạng `mauhoa_[ma-ket-qua]_[yyyyMMdd_HHmmss].png`, không dấu và không chứa ký tự không an toàn.
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
  - Yêu cầu tạo mẫu hoa tồn tại và thuộc khách hàng hiện tại.
  - Yêu cầu đã có ít nhất một kết quả mẫu hoa AI được tạo thành công.
  - Backend kiểm tra trạng thái của lần tạo AI và file ảnh tại thời điểm xử lý yêu cầu tải xuống.
- **Trigger**: Khách hàng thực hiện một trong các thao tác sau:
  - Ấn chọn “Tải xuống” tại màn hình Kết quả bó hoa AI.
  - Ấn chọn “Tải xuống” tại màn hình Kết quả bó hoa AI được mở từ Lịch sử tạo.

## Flow
### Main Flow
1. Khách hàng truy cập màn hình Kết quả bó hoa AI hoặc mở một kết quả AI từ Lịch sử tạo.
2. Hệ thống hiển thị nút “Tải xuống” cho kết quả AI hợp lệ.
3. Khách hàng ấn chọn “Tải xuống”.
4. Hệ thống kiểm tra kết quả AI có thuộc quyền sở hữu của khách hàng hiện tại hay không.
5. Hệ thống kiểm tra file ảnh kết quả còn khả dụng để tải xuống.
6. Hệ thống lấy file ảnh kết quả chính thức đã được lưu trong Core Storage.
7. File ảnh kết quả chính thức là ảnh đã được gắn logo sau khi AI tạo ảnh hợp lệ và trước khi lưu kết quả; chức năng tải xuống không gắn lại logo.
8. Hệ thống chuẩn bị file tải xuống theo định dạng PNG.
9. Hệ thống sinh tên file theo định dạng `mauhoa_[ma-ket-qua]_[yyyyMMdd_HHmmss].png` và chuyển tên file về dạng không dấu.
10. Hệ thống bắt đầu quá trình tải file về thiết bị của khách hàng.
11. Khách hàng nhận được file ảnh.

### Alternative Flow
- **ALT-01 — Tải xuống từ lịch sử tạo mẫu hoa**:
  1. Khách hàng truy cập màn hình Lịch sử tạo mẫu hoa.
  2. Khách hàng chọn một kết quả AI đã tạo trước đó.
  3. Hệ thống mở màn hình Kết quả bó hoa AI tương ứng với lần generate đã chọn.
  4. Hệ thống hiển thị nút “Tải xuống”.
  5. Khách hàng ấn “Tải xuống”.
  6. Hệ thống thực hiện quy trình tải file như Main Flow.
- **ALT-02 — Tải lại cùng một ảnh nhiều lần**:
  1. Khách hàng đã tải thành công một kết quả AI trước đó.
  2. Khách hàng quay lại màn hình kết quả hoặc lịch sử tạo.
  3. Khách hàng tiếp tục ấn “Tải xuống” cho cùng một kết quả.
  4. Hệ thống tiếp tục cho phép tải lại ảnh đó; mỗi lần tải xuống được xử lý như một thao tác độc lập.
- **ALT-03 — Dữ liệu nguồn không còn khả dụng**:
  1. Kết quả AI đã được tạo thành công và file ảnh kết quả vẫn còn khả dụng.
  2. Combo hoặc Mockup liên quan hiện không còn khả dụng trên hệ thống.
  3. Khách hàng mở lại kết quả AI cũ và ấn “Tải xuống”.
  4. Hệ thống vẫn cho phép tải file ảnh đã lưu theo Main Flow.

### Exception Flow
- **EXC-01 — Ảnh chưa sẵn sàng để tải**: Khách hàng truy cập màn hình kết quả khi AI job vẫn ở trạng thái “Đang tạo” hoặc file ảnh chưa được lưu thành công. Hệ thống không cho phép tải xuống, vô hiệu hóa nút “Tải xuống” và hiển thị thông báo: “Ảnh đang được tạo. Vui lòng đợi hoàn tất để tải xuống.”
- **EXC-02 — Kết quả AI không thuộc khách hàng hiện tại**: Khách hàng thực hiện thao tác tải xuống cho một kết quả AI không thuộc quyền sở hữu của mình. Hệ thống từ chối thao tác tải xuống, không trả file ảnh và hiển thị thông báo: “Bạn không có quyền tải xuống mẫu hoa này.”
- **EXC-03 — Không tìm thấy ảnh trong Core Storage**: Khách hàng ấn “Tải xuống”. Hệ thống kiểm tra bản ghi kết quả AI nhưng không lấy được dữ liệu ảnh tương ứng từ Core Storage. Hệ thống không thể tạo file tải xuống và hiển thị thông báo: “Không thể tải mẫu hoa. Vui lòng thử lại.”
- **EXC-04 — Lỗi khi chuẩn bị file tải xuống**: Hệ thống tìm thấy ảnh kết quả AI nhưng gặp lỗi trong quá trình chuẩn bị file PNG hoặc gắn metadata file tải xuống. Hệ thống không bắt đầu được quá trình tải và hiển thị thông báo: “Không thể tải mẫu hoa. Vui lòng thử lại.”
- **EXC-05 — Lỗi mạng hoặc tải xuống không thành công phía người dùng**: Hệ thống đã bắt đầu quá trình tải nhưng việc tải xuống không thành công do lỗi mạng hoặc phía trình duyệt của khách hàng. Hệ thống hiển thị thông báo: “Không thể tải mẫu hoa. Vui lòng thử lại.” Khách hàng có thể chủ động ấn “Tải xuống” lại.
- **EXC-06 — Khách hàng nhấn nhiều lần liên tiếp khi đang chuẩn bị tải**: Khách hàng ấn “Tải xuống”. Hệ thống đang chuẩn bị file tải xuống. Khách hàng tiếp tục ấn nút nhiều lần liên tiếp. Hệ thống disable nút “Tải xuống” trong thời gian chuẩn bị tải để chống duplicate action.

## Acceptance Criteria
### AC-001: Hiển thị nút tải xuống tại màn hình kết quả AI
- **Given**: Khách hàng đang xem một kết quả mẫu hoa AI đã được tạo thành công.
- **When**: Màn hình Kết quả bó hoa AI được hiển thị.
- **Then**: Hệ thống phải hiển thị nút “Tải xuống”.

### AC-002: Hiển thị nút tải xuống cho kết quả mở từ lịch sử tạo
- **Given**: Khách hàng đang xem một kết quả AI được mở từ Lịch sử tạo mẫu hoa.
- **When**: Màn hình kết quả tương ứng được hiển thị.
- **Then**: Hệ thống phải hiển thị nút “Tải xuống”.

### AC-003: Chỉ chủ sở hữu được tải ảnh
- **Given**: Một kết quả mẫu hoa AI tồn tại trong hệ thống.
- **When**: Khách hàng ấn chọn “Tải xuống”.
- **Then**: Hệ thống chỉ cho phép tải xuống nếu kết quả AI đó thuộc về yêu cầu tạo mẫu hoa của khách hàng hiện tại.

### AC-004: Cho phép tải kết quả mới nhất và kết quả cũ trong lịch sử
- **Given**: Yêu cầu tạo mẫu hoa đã có ít nhất một kết quả AI thành công.
- **When**: Khách hàng truy cập kết quả mới nhất hoặc một kết quả cũ trong lịch sử.
- **Then**: Hệ thống phải cho phép tải xuống cả kết quả mới nhất và các kết quả cũ trong lịch sử, miễn là các kết quả đó đã được tạo thành công.

### AC-005: Chỉ tải 1 ảnh cho mỗi lần thao tác
- **Given**: Khách hàng đang xem một kết quả AI hợp lệ.
- **When**: Khách hàng ấn “Tải xuống”.
- **Then**: Hệ thống chỉ được tải xuống đúng 1 ảnh tương ứng với kết quả AI đã chọn.

### AC-006: Định dạng file tải xuống
- **Given**: Khách hàng tải xuống một kết quả AI hợp lệ.
- **When**: Hệ thống chuẩn bị file tải xuống.
- **Then**: Hệ thống phải trả file theo định dạng PNG.

### AC-007: Chất lượng ảnh tải xuống
- **Given**: Khách hàng tải xuống một kết quả AI hợp lệ.
- **When**: Hệ thống trả file ảnh.
- **Then**: Ảnh tải xuống phải giữ nguyên resolution.
- **And**: Ảnh tải xuống không được bị cắt giảm chất lượng.

### AC-008: Ảnh tải xuống có gắn logo
- **Given**: Khách hàng tải xuống một kết quả AI hợp lệ.
- **When**: Hệ thống trả file ảnh.
- **Then**: Ảnh tải xuống phải là file kết quả chính thức đã được hệ thống gắn logo trong quá trình tạo mẫu hoa AI.
- **And**: Chức năng tải xuống không được gắn lại hoặc thay đổi logo.

### AC-009: Quy tắc đặt tên file
- **Given**: Khách hàng tải xuống một kết quả AI hợp lệ.
- **When**: Hệ thống sinh tên file tải xuống.
- **Then**: Tên file phải theo định dạng `mauhoa_[ma-ket-qua]_[yyyyMMdd_HHmmss].png`.
- **And**: Tên file không chứa ký tự không an toàn cho tên file và ở dạng không dấu.

### AC-010: Được tải nhiều lần
- **Given**: Một kết quả AI đã được tạo thành công.
- **When**: Khách hàng thực hiện tải xuống nhiều lần cho cùng một kết quả.
- **Then**: Hệ thống phải cho phép tải lại nhiều lần.
- **And**: Việc tải xuống không bị giới hạn số lần.

### AC-011: Tải xuống không trừ lượt AI
- **Given**: Khách hàng thực hiện tải xuống một kết quả AI thành công.
- **When**: Quá trình tải xuống được thực hiện.
- **Then**: Hệ thống không được trừ lượt AI của khách hàng.

### AC-012: Kết quả cũ vẫn tải được khi Combo hết khả dụng
- **Given**: Một kết quả AI cũ đã được tạo thành công trước đó.
- **When**: Combo hiện tại không còn khả dụng nhưng khách hàng mở lại kết quả AI cũ và ấn “Tải xuống”.
- **Then**: Hệ thống vẫn phải cho phép tải xuống ảnh đó.

### AC-013: Kết quả cũ vẫn tải được khi Mockup bị disable hoặc xóa
- **Given**: Một kết quả AI cũ đã được tạo thành công trước đó.
- **When**: Mockup liên quan không còn khả dụng nhưng khách hàng mở lại kết quả AI cũ và ấn “Tải xuống”.
- **Then**: Hệ thống vẫn phải cho phép tải xuống ảnh đó.

### AC-014: Disable nút khi ảnh chưa sẵn sàng
- **Given**: AI job tạo mẫu hoa đang ở trạng thái “Đang tạo” hoặc ảnh chưa sẵn sàng để tải.
- **When**: Khách hàng xem màn hình kết quả.
- **Then**: Hệ thống phải disable nút “Tải xuống”.
- **And**: Hệ thống phải hiển thị thông báo: “Ảnh đang được tạo. Vui lòng đợi hoàn tất để tải xuống.”

### AC-015: Xử lý lỗi tải xuống
- **Given**: Khách hàng ấn “Tải xuống” cho một kết quả AI hợp lệ.
- **When**: Hệ thống không thể lấy file ảnh từ Core Storage hoặc không thể chuẩn bị file tải xuống.
- **Then**: Hệ thống phải hiển thị thông báo: “Không thể tải mẫu hoa. Vui lòng thử lại.”

### AC-016: Disable nút khi đang chuẩn bị tải
- **Given**: Khách hàng đã ấn “Tải xuống”.
- **When**: Hệ thống đang chuẩn bị file tải xuống.
- **Then**: Hệ thống phải disable nút “Tải xuống” trong thời gian xử lý.
- **And**: Hệ thống phải hiển thị loading hoặc trạng thái đang xử lý.

## References
- **Rules**:
  - [BR-058](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/bf88673b-07b9-4dec-bef4-e1f1fe122300)
  - [BR-059](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/74fd66fe-25f6-42a9-9b38-e244d75b2596)
  - [BR-060](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e1b7a369-d212-4e00-815a-998a056f7396)
  - [BR-061](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/7b0e0cb0-9fec-4b88-a572-742337545d3c)
  - [BR-062](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/8ed20ca7-5cf0-48b9-a5e9-a47bcbed9fb3)
  - [BR-063](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a4788ceb-deb5-41e4-a18a-f7bec273f8e6)
  - [BR-064](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3017eb6d-1dc4-4780-ba80-8b9d66c34db4)
  - [BR-065](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2f295523-5fdb-4752-97db-8b27165606b3)
  - [BR-066](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/adf2011a-6691-4634-8303-d9dab75a9a10)
- **Dependencies**:
  - [STORY-030](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/225510e4-396c-403b-9ae4-59f00859a2ce) / [30-InitializeFlowerDesignRequest.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/30-InitializeFlowerDesignRequest.md)
  - [STORY-033](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e1cdce1d-5295-4945-930a-95f9e8bee6b9) / [33-GenerateFlowerDesignWithAI.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/33-GenerateFlowerDesignWithAI.md)

## Non-Functional
- Thao tác tải xuống phản hồi rõ ràng cho khách hàng và không làm mất trạng thái màn hình kết quả hiện tại.
- Ảnh tải xuống giữ nguyên chất lượng ảnh kết quả AI đã được hệ thống lưu.
- Tên file tải xuống không dấu để hạn chế lỗi khi lưu trên các thiết bị khác nhau.
- Trong thời gian hệ thống đang chuẩn bị file tải xuống, giao diện phải disable nút “Tải xuống” để hạn chế thao tác lặp từ cùng màn hình.

## Out of Scope
- Không bao gồm chức năng chia sẻ ảnh trực tiếp lên mạng xã hội hoặc ứng dụng bên thứ ba.
- Không bao gồm chỉnh sửa, crop, nén hoặc thay đổi chất lượng ảnh trước khi tải xuống.
- Không bao gồm tạo mới mẫu hoa bằng AI.
- Không bao gồm thêm mẫu hoa vào giỏ hàng hoặc đặt hàng từ ảnh đã tải xuống.
