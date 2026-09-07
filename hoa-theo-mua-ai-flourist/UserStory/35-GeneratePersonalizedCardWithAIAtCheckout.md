# STORY-035: Tạo thiệp cá nhân hóa bằng AI tại Checkout (Generate Personalized Greeting Card with AI at Checkout)

## Metadata
- **Story**: Là một khách hàng đã đăng nhập, tôi muốn tạo thiệp mới tại Checkout, để có một mẫu thiệp cá nhân hóa phù hợp với đơn hàng của mình.
- **Context**: Khách hàng có thể tạo thiệp mới tại Checkout.
  - Mỗi lần AI tạo được ảnh output hợp lệ, hệ thống tạo đúng 01 History record mới và ghi nhận 01 lượt generate đã sử dụng.
  - Mỗi mẫu hoa trong quá trình đặt hàng được generate tối đa 03 lần thiệp AI và mỗi khách hàng được sử dụng tối đa 10 lượt generate thiệp AI mỗi ngày.
  - Ảnh thiệp vừa generate không tự động trở thành thiệp hiện tại của Checkout. Khách hàng phải chọn “Xác nhận” đối với kết quả muốn sử dụng. Chỉ kết quả được xác nhận mới trở thành thiệp hiện tại của Checkout.
  - Nếu Checkout đã có thiệp, thiệp cũ chỉ bị gỡ khỏi Checkout và vẫn được giữ trong History.
  - Việc hoàn tất Checkout, tạo Order, tính bill cuối và tạo liên kết Thiệp-Order thuộc phạm vi khác.
  - **Input Data**:
    - **Thông tin bắt buộc**:
      - Người gửi: tối đa 20 từ.
      - Người nhận: tối đa 20 từ.
      - Lời chúc: tối đa 100 từ.
      - Template.
      - Size.
      - Hình thức: Calligraphy hoặc Gõ máy.
    - **Ảnh đính kèm**:
      - Không bắt buộc.
      - Tối đa 01 ảnh.
      - Định dạng: PNG, JPG.
      - Dung lượng tối đa: 10 MB.
    - **Quy tắc đếm từ**:
      - Một từ là một chuỗi ký tự liên tục được phân tách bởi khoảng trắng, tab hoặc ký tự xuống dòng.
      - Khoảng trắng ở đầu và cuối nội dung không được tính.
      - Nhiều khoảng trắng, tab hoặc ký tự xuống dòng liên tiếp được tính là một dấu phân tách.
      - Dấu câu đi liền với một từ không được tính thành từ riêng.
      - Nội dung chỉ gồm khoảng trắng được tính là 0 từ.
    - Frontend chỉ cho phép gửi yêu cầu khi dữ liệu hợp lệ. Backend phải kiểm tra lại toàn bộ dữ liệu, quyền sở hữu và điều kiện xử lý.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hoàng Thị Khánh Linh
- **Author**: Hoàng Thị Khánh Linh
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hoàng Thị Khánh Linh
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 19/08/2026)

## Conditions
- **Preconditions**:
  - Khách hàng đã đăng nhập.
  - Checkout phải tồn tại, thuộc khách hàng hiện tại và chưa hoàn tất.
  - Có ít nhất 01 Template và 01 Size đang ở trạng thái khả dụng từ Core DB.
- **Trigger**: Khách hàng chọn “Tạo thiệp” tại Checkout.

## Flow
### Main Flow
1. Khách hàng mở chức năng tạo thiệp tại Checkout.
2. Backend kiểm tra Checkout, số lượt generate thiệp đã sử dụng cho mẫu hoa nguồn và quota tạo thiệp trong ngày của khách hàng.
3. Hệ thống tải danh sách Template và Size hiện hành từ Core system.
4. Khách hàng nhập Người gửi, Người nhận, Lời chúc; chọn Template, Size, hình thức và có thể tải lên 01 ảnh.
5. Hệ thống lấy giá hiện hành của Size từ DB hệ thống và hiển thị giá tạm tính theo Size và Hình thức theo BR-054.
6. Khách hàng chọn “Tạo thiệp”.
7. Backend kiểm tra lại Checkout, dữ liệu đầu vào, Template, Size, giới hạn 03 lượt generate thiệp của mẫu hoa nguồn và quota 10 lượt generate thiệp trong ngày của khách hàng.
8. Hệ thống tạo AI Job cho request hợp lệ, ghi nhận 01 lượt vào quota tạo thiệp trong ngày và 01 lượt vào giới hạn generate thiệp của mẫu hoa nguồn.
9. Hệ thống gửi yêu cầu sang quá trình AI.
10. AI tạo ảnh theo Template và Hình thức đã chọn (tuân thủ BR-154).
11. Nếu có ảnh đính kèm, AI phải giữ nguyên nội dung gốc của ảnh; không cắt mất chủ thể, xoay, đổi màu, thêm/xóa hoặc làm biến dạng. Được phép scale đồng dạng hoặc thêm khoảng đệm để phù hợp Template.
12. Nếu có ảnh output hợp lệ, hệ thống lưu ảnh kết quả chính thức dưới định dạng PNG và không gắn logo.
13. Hệ thống giữ nguyên lượt đã ghi nhận và tạo đúng 01 History record mới.
14. Hệ thống hiển thị kết quả thiệp cùng các thao tác “Tạo lại” và “Xác nhận”.
15. Khách hàng xem kết quả thiệp.
16. Nếu khách hàng chọn “Xác nhận”, hệ thống chọn kết quả đó làm thiệp hiện tại của Checkout.
17. Nếu Checkout đã có thiệp khác, thiệp trước đó bị gỡ khỏi Checkout nhưng vẫn được giữ trong History.
18. Order Summary hiển thị thiệp đã được xác nhận và giá tạm tính tương ứng.

### Alternative Flow
- **ALT-01 — Reload khi AI đang xử lý**:
  1. Yêu cầu AI đang được xử lý ngầm.
  2. Khách hàng reload hoặc rời khỏi màn hình.
  3. Job AI tiếp tục chạy và không bị khởi tạo lại.
  4. Yêu cầu hiển thị trạng thái “Đang tạo” tại màn lịch sử liên quan.
  5. Job chưa tạo History item khi chưa có ảnh output hợp lệ. Lượt quota đã được trừ khi request được chấp nhận và chỉ được hoàn nếu AI Job kết thúc thất bại sau toàn bộ retry.
- **ALT-02 — Không có ảnh đính kèm**: Khách hàng vẫn được tạo thiệp nếu các dữ liệu bắt buộc còn lại hợp lệ.

### Exception Flow
- **EXC-01 — Hết quota**: Khách hàng đang ở màn tạo thiệp tại Checkout và đã nhập đầy đủ dữ liệu hợp lệ. Khách hàng ấn chọn “Tạo thiệp”. Backend kiểm tra và phát hiện khách hàng đã sử dụng hết quota tạo thiệp trong ngày. Hệ thống không gửi yêu cầu sang AI, không tạo History record, không ghi nhận thêm lượt quota và thông báo khách hàng đã sử dụng hết quota tạo thiệp trong ngày.
- **EXC-02 — Dữ liệu bắt buộc không hợp lệ**: Khách hàng nhập thiếu hoặc không hợp lệ một trong các trường bắt buộc (Người gửi, Người nhận, Lời chúc, Template, Size hoặc Hình thức). Khách hàng ấn chọn “Tạo thiệp”. Backend phát hiện dữ liệu không hợp lệ, từ chối request, không gửi sang AI, không tạo History record, không trừ quota và hiển thị lỗi tại trường tương ứng.
- **EXC-03 — File upload không hợp lệ**: File ảnh không thuộc định dạng PNG, JPG, hoặc có dung lượng vượt quá 10 MB. Hệ thống từ chối file ảnh, hiển thị lý do và cho phép khách hàng chọn file khác hoặc tiếp tục tạo thiệp không có ảnh.
- **EXC-04 — Template hoặc Size không còn khả dụng**: Template hoặc Size đã ngừng khả dụng trên Core system. Backend từ chối request, không gửi sang AI, không tạo History record, không trừ quota và yêu cầu khách hàng chọn Template hoặc Size hiện còn khả dụng.
- **EXC-05 — AI không tạo được ảnh output**: AI không tạo được ảnh output hợp lệ sau khi tự động thử lại theo chính sách kỹ thuật. Hệ thống kết thúc xử lý thất bại, không tạo History record, hoàn lại đúng 01 lượt quota ngày và 01 lượt generate theo mẫu hoa (tối đa 01 lần hoàn cho mỗi AI Job) và thông báo khách hàng thử lại sau.
- **EXC-06 — Có ảnh nhưng gắn với Checkout thất bại**: Khách hàng chọn “Xác nhận” cho kết quả thiệp đã tạo thành công nhưng hệ thống gặp lỗi khi cập nhật Checkout. History record vẫn được giữ lại, lượt generate đã sử dụng vẫn được ghi nhận, Checkout giữ thiệp trước đó (nếu có) và hiển thị thông báo để khách hàng thử lại thao tác Xác nhận.
- **EXC-07 — Request bị gửi trùng**: Khách hàng ấn “Tạo thiệp” nhiều lần liên tiếp hoặc request bị gửi lại do mạng. Backend nhận diện request trùng theo cơ chế idempotent, không gọi AI nhiều lần, không tạo nhiều ảnh/History record, không trừ quota nhiều lần và chỉ trả về kết quả tương ứng.
- **EXC-08 — Checkout không hợp lệ**: Checkout không tồn tại, không thuộc khách hàng hiện tại hoặc đã hoàn tất. Backend từ chối thao tác, không gửi sang AI, không tạo History record, không trừ quota và hiển thị lỗi phù hợp mà không để lộ dữ liệu nhạy cảm.
- **EXC-09 — Đã đủ 03 lượt cho mẫu hoa**: Khách hàng đã sử dụng đủ 03 lượt generate thiệp AI cho mẫu hoa nguồn của lần đặt hàng hiện tại. Backend phát hiện giới hạn theo mẫu hoa đã hết, không tạo AI Job, không trừ quota ngày, không tạo History record và thông báo khách hàng đã sử dụng hết số lượt tạo thiệp cho mẫu hoa này.

## Acceptance Criteria
### AC-001: Tạo thiệp thành công
- **Given**: Checkout hợp lệ và khách hàng còn quota.
- **When**: AI tạo được ảnh output hợp lệ.
- **Then**: Hệ thống lưu ảnh kết quả chính thức dưới định dạng PNG và không gắn logo.
- **And**: Tạo đúng 01 History record.
- **And**: Ghi nhận đúng 01 lượt generate đã sử dụng.
- **And**: Chưa tự động chọn thiệp mới làm thiệp hiện tại của Checkout khi khách hàng chưa xác nhận.

### AC-002: AI không tạo được ảnh hợp lệ
- **Given**: Yêu cầu đã được gửi sang AI.
- **When**: Quá trình xử lý kết thúc nhưng không có ảnh output hợp lệ sau các lần thử lại.
- **Then**: Hệ thống hoàn lại đúng 01 lượt quota tạo thiệp trong ngày và 01 lượt generate thiệp của mẫu hoa nguồn đã ghi nhận.
- **And**: Mỗi AI Job chỉ được hoàn mỗi loại lượt tối đa 01 lần.

### AC-003: Nội dung thiệp Gõ máy
- **Given**: Khách hàng chọn Hình thức Gõ máy và đã nhập Người gửi, Người nhận, Lời chúc hợp lệ.
- **When**: AI tạo thiệp.
- **Then**: Hệ thống phải render nguyên văn Người gửi, Người nhận và Lời chúc lên ảnh kết quả.
- **And**: Không được tự động sửa, rút gọn, dịch hoặc thay đổi Người gửi, Người nhận và Lời chúc.

### AC-004: Giới hạn số từ
- **Given**: Một trường vượt quá giới hạn số từ quy định (Người gửi > 20 từ, Người nhận > 20 từ, Lời chúc > 100 từ).
- **When**: Khách hàng gửi yêu cầu.
- **Then**: Backend từ chối dữ liệu.
- **And**: Không bắt đầu AI.
- **And**: Không trừ quota.

### AC-005: Template và ảnh đính kèm
- **Given**: Khách hàng đã chọn Template và có ảnh đính kèm.
- **When**: AI tạo thiệp.
- **Then**: AI giữ nguyên nội dung gốc của ảnh; không cắt mất chủ thể, xoay, đổi màu, thêm/xóa hoặc làm biến dạng. Được phép scale đồng dạng hoặc thêm khoảng đệm để phù hợp Template.
- **And**: Giữ nguyên 100% ảnh đính kèm nếu có.
- **And**: Không sửa, cắt, xoay, đổi màu, thêm, xóa hoặc làm biến dạng nội dung ảnh.

### AC-006: File ảnh không hợp lệ
- **Given**: File không đúng định dạng (không phải PNG, JPG) hoặc vượt quá 10 MB.
- **When**: Khách hàng tải file lên.
- **Then**: Hệ thống từ chối file.
- **And**: Cho phép chọn file khác hoặc tiếp tục không có ảnh.

### AC-007: Template hoặc Size không khả dụng
- **Given**: Template hoặc Size đã ngừng khả dụng trên hệ thống.
- **When**: Khách hàng gửi yêu cầu.
- **Then**: Hệ thống không bắt đầu AI.
- **And**: Yêu cầu chọn dữ liệu hiện còn khả dụng.

### AC-008: Không gắn được thiệp sau khi Xác nhận
- **Given**: Khách hàng đang xem một kết quả thiệp AI đã generate thành công.
- **When**: Khách hàng chọn “Xác nhận” nhưng hệ thống không thể chọn thiệp đó làm thiệp hiện tại của Checkout.
- **Then**: History record vẫn được giữ.
- **And**: Các lượt generate đã sử dụng vẫn được ghi nhận.
- **And**: Checkout giữ thiệp trước đó nếu có.

### AC-009: Chống request trùng
- **Given**: Cùng một request được gửi lại (nhiều lần click hoặc lỗi mạng).
- **When**: Backend xử lý request trùng.
- **Then**: Chỉ có tối đa 01 lần gọi AI, 01 ảnh, 01 History record và 01 lượt quota được ghi nhận.

### AC-010: Checkout không hợp lệ
- **Given**: Checkout không tồn tại, không thuộc khách hàng hiện tại hoặc đã hoàn tất.
- **When**: Khách hàng yêu cầu tạo thiệp trong Checkout.
- **Then**: Backend từ chối request.
- **And**: Không bắt đầu AI.

### AC-011: Giữ AI Job khi reload
- **Given**: Job AI đang chạy ngầm.
- **When**: Khách hàng reload hoặc mở lại màn hình.
- **Then**: Job tiếp tục xử lý.
- **And**: Hệ thống hiển thị trạng thái “Đang tạo”.
- **And**: Chưa tạo History item khi chưa có ảnh output hợp lệ; lượt quota đã được trừ khi request được chấp nhận.

### AC-012: Tạo thiệp không có ảnh đính kèm
- **Given**: Checkout hợp lệ, khách hàng còn quota và các trường bắt buộc khác hợp lệ.
- **When**: Khách hàng không chọn ảnh đính kèm.
- **Then**: Hệ thống vẫn phải cho phép tạo thiệp bằng AI.
- **And**: Nếu tạo thành công, hệ thống tạo ảnh kết quả và xử lý History/quota/Checkout như Main Flow.

### AC-013: Nội dung thiệp Calligraphy
- **Given**: Khách hàng chọn Hình thức Calligraphy và đã nhập Người gửi, Người nhận, Lời chúc hợp lệ.
- **When**: AI tạo thiệp thành công.
- **Then**: Ảnh output không hiển thị Người gửi, Người nhận và Lời chúc.
- **And**: Các nội dung này vẫn được lưu cùng History record của thiệp.

### AC-014: Xác nhận kết quả thiệp
- **Given**: Khách hàng đang xem một kết quả thiệp AI đã generate thành công trong Checkout.
- **When**: Khách hàng chọn “Xác nhận”.
- **Then**: Nếu Checkout đã có thiệp đang chọn, thiệp trước đó bị gỡ khỏi Checkout nhưng vẫn được giữ trong History.
- **And**: Thao tác Xác nhận không gọi AI, không tạo History record mới và không ghi nhận thêm lượt generate.

## References
- **Rules**:
  - [BR-048](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b04b77dd-11d1-42f5-8976-b360ba3e5014)
  - [BR-049](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/49820cc0-a191-4b04-a33b-649f93ff21bc)
  - [BR-050](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2690bf4b-da62-4e2d-b772-c7951354a661)
  - [BR-051](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/d8ba05bd-9cc4-4522-9c99-fe5f2bd1ca7a)
  - [BR-052](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c17b8c1c-62a5-42c0-95cd-91620de4366d)
  - [BR-053](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/4e460995-5b6c-45b3-8491-13c0c1c80b05)
  - [BR-054](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b93ed86e-be68-4fbe-a7ab-8f1532f369bb)
  - [BR-055](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/cdd69ab1-bcf7-43f9-9ec2-97b398d0bc15)
  - [BR-056](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3c9e47f4-edaa-45ea-9230-9ebdb7d4637f)
  - [BR-057](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/de8033dd-598c-4438-9da1-46460040d068)
  - [BR-154](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5573cc50-4ff5-405c-a44f-08a102297e92)
- **Dependencies**:
  - [STORY-038](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5cc87b78-e6bf-4996-8d67-787e0bc34bb4)
  - [STORY-042](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/35d883ea-e9bc-42c0-93e8-6dfcb380bd39)

## Non-Functional
- Mở biểu mẫu tạo thiệp p95 ≤ 1 giây trong điều kiện bình thường.
- Tải Template và Size p95 ≤ 1.5 giây, không tính thời gian Core system bị chậm.
- Backend kiểm tra ownership, quota và idempotency.
- Trạng thái job AI phải có thể được truy xuất lại sau khi khách hàng reload.
- API metadata không trả binary ảnh hoặc expose đường dẫn storage nội bộ.
- Thông báo lỗi không được chứa stack trace hoặc thông tin kỹ thuật nhạy cảm.

## Out of Scope
- Chọn thiệp đã có từ History cho Checkout.
- Hoàn tất Checkout, tạo Order và thanh toán.
- Tạo liên kết Thiệp-Order và tính bill cuối.
- Tải xuống, chia sẻ, chỉnh sửa hoặc xóa thiệp.
