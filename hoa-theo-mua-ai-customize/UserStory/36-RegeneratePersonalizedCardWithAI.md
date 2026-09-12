# STORY-036: Tạo lại thiệp cá nhân hóa bằng AI (Regenerate Personalized Greeting Card with AI)

## Metadata
- **Story**: Là một khách hàng đã đăng nhập, tôi muốn tạo lại từ một thiệp đã có, để có một mẫu thiệp cá nhân hóa phù hợp với nhu cầu của mình.
- **Context**: Khách hàng có thể chọn “Tạo lại” tại màn Chi tiết thiệp đã tạo.
  - Mỗi request Tạo lại hợp lệ phải đồng thời tuân theo giới hạn 03 lượt generate thiệp theo mẫu hoa và 10 lượt generate thiệp AI mỗi khách hàng mỗi ngày.
  - Khi request được chấp nhận, hệ thống ghi nhận 01 lượt vào cả hai giới hạn.
  - Nếu AI tạo ảnh thành công thì giữ nguyên; nếu AI Job thất bại sau toàn bộ retry và không có ảnh output hợp lệ thì hoàn lại các lượt đã ghi nhận.
  - Nếu thao tác được thực hiện từ History chung, thiệp mới chỉ được lưu vào History và không tự động gắn với Checkout.
  - Nếu khách hàng thực hiện Tạo lại trong ngữ cảnh Checkout, kết quả mới được lưu vào History và hiển thị để khách hàng xem. Kết quả mới không tự động trở thành thiệp hiện tại của Checkout. Chỉ khi khách hàng chọn “Xác nhận”, kết quả đó mới được chọn làm thiệp hiện tại của Checkout. Nếu Checkout đã có thiệp đang chọn, thiệp trước đó bị gỡ khỏi Checkout nhưng vẫn được giữ trong History.
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
      - Định dạng: PNG, JPG hoặc JPEG.
      - Dung lượng tối đa: 10 MB.
    - **Quy tắc đếm từ**:
      - Một từ là một chuỗi ký tự liên tục được phân tách bởi khoảng trắng, tab hoặc ký tự xuống dòng.
      - Khoảng trắng ở đầu và cuối nội dung không được tính.
      - Nhiều khoảng trắng, tab hoặc ký tự xuống dòng liên tiếp được tính là một dấu phân tách.
      - Dấu câu đi liền với một từ không được tính thành từ riêng.
      - Nội dung chỉ gồm khoảng trắng được tính là 0 từ.
    - **Dữ liệu sử dụng khi Tạo lại**:
      - Hệ thống sử dụng nguyên Người gửi, Người nhận, Lời chúc, Template, Size, Hình thức và ảnh đính kèm nếu có của lần generate hiện tại.
      - Khách hàng không chỉnh sửa các dữ liệu này trong thao tác Tạo lại.
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
  - Khi tạo lại từ History, thiệp nguồn phải tồn tại và thuộc khách hàng hiện tại.
  - Khi tạo lại trong ngữ cảnh Checkout, Checkout phải tồn tại, thuộc khách hàng hiện tại và chưa hoàn tất.
  - Có ít nhất 01 Template và 01 Size đang ở trạng thái khả dụng từ Core system.
- **Trigger**:
  - Khách hàng chọn “Tạo lại” khi xem một thiệp thuộc History của mình.
  - Khách hàng chọn “Tạo lại” đối với thiệp đang được chọn trong một Checkout hợp lệ.

## Flow
### Main Flow
1. Khách hàng đang xem một kết quả thiệp AI đã tạo.
2. Khách hàng chọn “Tạo lại”.
3. Backend kiểm tra quyền sở hữu, giới hạn 03 lượt theo mẫu hoa nguồn và quota 10 lượt/ngày.
4. Hệ thống sử dụng nguyên dữ liệu đầu vào của lần generate hiện tại.
5. Hệ thống tạo AI Job mới, ghi nhận 01 lượt vào quota tạo thiệp trong ngày và 01 lượt vào giới hạn generate thiệp của mẫu hoa nguồn.
6. Hệ thống gửi yêu cầu AI mới mà không mở biểu mẫu chỉnh sửa.
7. Nếu có ảnh output hợp lệ, hệ thống lưu ảnh chính thức dưới định dạng PNG, không gắn logo.
8. Hệ thống tạo đúng 01 History record mới và giữ nguyên thiệp nguồn.
9. Kết quả mới được hiển thị để khách hàng tiếp tục Tạo lại hoặc Xác nhận.
10. Kết quả mới không tự động trở thành thiệp hiện tại của Checkout cho đến khi khách hàng chọn Xác nhận.

### Alternative Flow
- **ALT-01 — Reload khi AI đang xử lý**:
  1. Yêu cầu AI đang được xử lý ngầm.
  2. Khách hàng reload hoặc rời khỏi màn hình.
  3. Job AI tiếp tục chạy và không bị khởi tạo lại.
  4. Yêu cầu hiển thị trạng thái “Đang tạo” tại màn lịch sử liên quan.
  5. Job chưa tạo History item khi chưa có ảnh output hợp lệ. Lượt quota đã được trừ khi request tạo lại được chấp nhận và chỉ được hoàn nếu AI Job thất bại sau toàn bộ retry.
- **ALT-02 — Tạo lại trong ngữ cảnh Checkout**:
  1. Khách hàng thực hiện tạo lại khi đang thao tác trong một Checkout hợp lệ.
  2. Hệ thống tạo một yêu cầu AI mới.
  3. Nếu có ảnh output hợp lệ, hệ thống lưu ảnh PNG không logo, giữ nguyên lượt đã ghi nhận và tạo History record mới.
  4. Hệ thống hiển thị kết quả mới nhưng chưa thay đổi thiệp hiện tại của Checkout.
  5. Chỉ khi khách hàng chọn “Xác nhận”, thiệp mới mới được chọn làm thiệp hiện tại của Checkout.
- **ALT-03 — Không có ảnh đính kèm**: Khách hàng vẫn được tạo thiệp nếu các dữ liệu bắt buộc còn lại hợp lệ.

### Exception Flow
- **EXC-01 — Hết quota**: Khách hàng đang xem một kết quả thiệp AI đã tạo. Khách hàng chọn “Tạo lại”. Backend kiểm tra và phát hiện khách hàng đã sử dụng đủ 10 lượt generate thiệp AI trong ngày. Hệ thống không tạo AI Job mới, không tạo History record mới, không ghi nhận thêm lượt quota ngày/lượt theo mẫu hoa và thông báo: “Bạn đã sử dụng hết 10 lượt tạo thiệp AI trong ngày.”
- **EXC-04 — Template hoặc Size không còn khả dụng**: Backend kiểm tra Template và Size của thiệp nguồn và phát hiện Template hoặc Size đã ngừng khả dụng. Hệ thống không tạo AI Job mới, không tạo History record mới, không ghi nhận quota ngày/lượt theo mẫu hoa và thông báo Template hoặc Size của thiệp nguồn không còn khả dụng để Tạo lại.
- **EXC-05 — AI không tạo được ảnh output**: Khách hàng đã gửi yêu cầu tạo lại thiệp hợp lệ nhưng AI không tạo được ảnh output sau các lần retry cho phép. Hệ thống kết thúc xử lý thất bại, không tạo History record mới, hoàn lại đúng 01 lượt quota ngày và 01 lượt generate theo mẫu hoa (tối đa 01 lần hoàn cho mỗi AI Job) và thông báo khách hàng thử lại sau.
- **EXC-06 — Request bị gửi trùng**: Khách hàng chọn “Tạo lại” nhiều lần liên tiếp hoặc request bị gửi lại do mạng. Backend nhận diện request trùng theo cơ chế idempotent, không gọi AI nhiều lần, không tạo nhiều ảnh/History record, không trừ quota nhiều lần và chỉ trả về kết quả tương ứng.
- **EXC-07 — Không có quyền với thiệp nguồn**: Backend phát hiện thiệp nguồn không thuộc khách hàng hiện tại. Hệ thống từ chối request tạo lại, không trả ảnh/metadata nhạy cảm của thiệp nguồn, không bắt đầu AI, không tạo History record mới và không trừ quota.
- **EXC-08 — Checkout không hợp lệ**: Khách hàng thực hiện tạo lại trong ngữ cảnh Checkout nhưng Checkout không tồn tại, không thuộc khách hàng hiện tại hoặc đã hoàn tất. Hệ thống từ chối thao tác, không bắt đầu AI, không tạo History record mới, không trừ quota và hiển thị lỗi phù hợp mà không để lộ dữ liệu nhạy cảm.
- **EXC-09 — Đã đủ 03 lượt generate cho mẫu hoa**: Khách hàng đang xem kết quả thiệp trong ngữ cảnh Checkout và chọn “Tạo lại”. Backend phát hiện mẫu hoa đã sử dụng đủ 03 lượt generate thiệp AI. Hệ thống không tạo AI Job mới, không trừ thêm quota ngày, không tạo History record mới và thông báo: “Bạn đã sử dụng hết số lượt tạo thiệp cho mẫu hoa này.”

## Acceptance Criteria
### AC-001: Tạo lại thành công
- **Given**: Thiệp nguồn thuộc khách hàng hiện tại.
- **When**: Khách hàng tạo lại và AI tạo được ảnh hợp lệ.
- **Then**: Hệ thống lưu ảnh kết quả chính thức dưới định dạng PNG và không gắn logo.
- **And**: Hệ thống tạo đúng 01 History record mới.
- **And**: Không ghi đè hoặc xóa thiệp nguồn.
- **And**: Kết quả mới không tự động trở thành thiệp hiện tại của Checkout khi khách hàng chưa chọn “Xác nhận”.

### AC-002: AI không tạo được ảnh hợp lệ
- **Given**: Yêu cầu đã được gửi sang AI.
- **When**: Quá trình xử lý kết thúc nhưng không có ảnh output hợp lệ.
- **Then**: Hệ thống không tạo History record.
- **And**: Hệ thống hoàn lại đúng 01 lượt quota tạo thiệp trong ngày và 01 lượt generate thiệp theo mẫu hoa đã ghi nhận cho AI Job đó.

### AC-003: Giữ nguyên nội dung văn bản
- **Given**: Khách hàng đã nhập Người gửi, Người nhận và Lời chúc hợp lệ ở lần trước.
- **When**: AI tạo thiệp.
- **Then**: Hệ thống phải giữ nguyên chính xác dữ liệu Người gửi, Người nhận và Lời chúc; việc render các nội dung này lên ảnh kết quả tuân theo BR-154.

### AC-004: Template hoặc Size ngừng khả dụng
- **Given**: Template hoặc Size của thiệp nguồn đã ngừng khả dụng trên hệ thống.
- **When**: Khách hàng gửi yêu cầu tạo lại.
- **Then**: Hệ thống không bắt đầu AI.
- **And**: Hệ thống thông báo Template hoặc Size của thiệp nguồn không còn khả dụng để Tạo lại.

### AC-005: Chống request trùng
- **Given**: Cùng một request tạo lại được gửi lại.
- **When**: Backend xử lý request trùng.
- **Then**: Chỉ có tối đa 01 lần gọi AI, 01 ảnh, 01 History record, 01 lượt quota ngày và 01 lượt generate theo mẫu hoa được ghi nhận.

### AC-006: Giữ AI Job khi reload
- **Given**: Job AI đang chạy ngầm.
- **When**: Khách hàng reload hoặc mở lại màn hình.
- **Then**: Job tiếp tục xử lý.
- **And**: Hệ thống hiển thị trạng thái “Đang tạo”.
- **And**: Chưa tạo History item khi chưa có ảnh output hợp lệ; lượt quota đã được trừ khi request được chấp nhận.

### AC-007: Kiểm tra quyền sở hữu thiệp nguồn
- **Given**: Thiệp nguồn không thuộc khách hàng hiện tại.
- **When**: Khách hàng yêu cầu tạo lại.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh hoặc metadata nhạy cảm.

### AC-008: Tạo lại trong ngữ cảnh Checkout
- **Given**: Khách hàng thực hiện tạo lại khi đang thao tác trong một Checkout hợp lệ.
- **When**: AI tạo được ảnh output hợp lệ.
- **Then**: Hệ thống hiển thị kết quả mới nhưng chưa thay đổi thiệp hiện tại của Checkout.
- **And**: Khi khách hàng chọn “Xác nhận”, hệ thống chọn kết quả mới làm thiệp hiện tại của Checkout.
- **And**: Thiệp đang chọn trước đó bị gỡ khỏi Checkout nhưng vẫn được giữ trong History.

### AC-009: Tạo lại với dữ liệu hiện tại
- **Given**: Thiệp nguồn tồn tại và thuộc khách hàng hiện tại.
- **When**: Khách hàng chọn “Tạo lại”.
- **Then**: Hệ thống sử dụng lại nguyên dữ liệu đầu vào của lần generate hiện tại để tạo một AI Job mới.
- **And**: Hệ thống không mở biểu mẫu chỉnh sửa trước khi gửi request mới.
- **And**: Thao tác Tạo lại tiêu hao 01 lượt nếu request được chấp nhận và tuân theo cả giới hạn 03 lượt theo mẫu hoa và 10 lượt/ngày.

### AC-010: Xử lý ảnh đính kèm khi tạo lại
- **Given**: Khách hàng đã chọn Template và thiệp nguồn có thể có ảnh đính kèm.
- **When**: AI thực hiện Tạo lại thiệp.
- **Then**: Giữ nguyên 100% ảnh đính kèm nếu có.
- **And**: Giữ nguyên nội dung gốc; được phép scale đồng dạng hoặc thêm khoảng đệm, không được cắt mất chủ thể, xoay, đổi màu hoặc làm biến dạng.

## References
- **Rules**:
  - [BR-044](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b4df6090-28ff-4fe4-a528-9123a7f5ad8e)
  - [BR-046](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/d1ba26c7-d034-4be5-849a-4d427ab9f4eb)
  - [BR-154](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5573cc50-4ff5-405c-a44f-08a102297e92)
  - [BR-048](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b04b77dd-11d1-42f5-8976-b360ba3e5014)
- **Dependencies**:
  - [STORY-038](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5cc87b78-e6bf-4996-8d67-787e0bc34bb4)
  - [STORY-045](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3f89b027-0641-4888-909e-bbb18795398d)

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
