# STORY-002: Lên lịch và tự động đăng bài

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn chọn nội dung và hình ảnh đã được lưu, chọn nền tảng được hỗ trợ và thiết lập thời gian đăng để hệ thống tự động đăng bài đúng thời gian đã lên lịch mà không cần thao tác thủ công tại thời điểm đăng
- **Context**: Trước đây, sau khi nội dung và hình ảnh được tạo và lưu, Quản trị viên phải đăng bài thủ công lên từng nền tảng vào thời gian mong muốn. Khi số lượng bài viết và nền tảng tăng lên, việc đăng thủ công dễ dẫn đến quên lịch, đăng sai thời gian hoặc mất nhiều thời gian thao tác. Chức năng này cho phép Quản trị viên lựa chọn nội dung và hình ảnh đã được lưu, lựa chọn một hoặc nhiều nền tảng đã được kết nối với hệ thống và thiết lập thời gian đăng.
- **Quy định dữ liệu khi tạo lịch đăng bài**:
  1. **Nội dung**:
     - Bắt buộc chọn đúng một nội dung đã được lưu.
     - Nội dung phải còn tồn tại và thuộc phạm vi Quản trị viên được phép sử dụng (BR-046).
  2. **Hình ảnh**:
     - Hình ảnh không bắt buộc.
     - Mỗi lịch đăng được chọn một hoặc nhiều ảnh tối đa 10 ảnh đã lưu hoặc trên máy (BR-003).
     - Nếu là ảnh đã lưu thì phải còn tồn tại.
     - Ảnh phải có định dạng JPG, JPEG hoặc PNG và dung lượng không vượt quá 10 MB (BR-001, BR-002).
  3. **Nền tảng**:
     - Bắt buộc chọn ít nhất một nền tảng (BR-005).
     - Mỗi nền tảng chỉ được chọn một lần trong cùng một lịch đăng (BR-047).
     - Nền tảng phải được hệ thống hỗ trợ, đang được bật và đang kết nối hợp lệ (BR-047).
  4. **Ngày và giờ đăng**:
     - Bắt buộc nhập đầy đủ ngày và giờ đăng.
     - Ngày và giờ đăng phải lớn hơn thời điểm hiện tại của hệ thống (BR-004).
  5. **Kiểm tra lịch trùng**:
     - Một lịch được xem là trùng khi có cùng nội dung, cùng nền tảng và cùng thời điểm đăng với một lịch đang ở trạng thái "Đã lên lịch" (BR-006).
     - Nếu Quản trị viên chọn nhiều nền tảng, hệ thống kiểm tra trùng riêng cho từng nền tảng.
     - Khi phát hiện trùng, hệ thống không tạo lịch và phải chỉ rõ nền tảng cùng thời điểm đang bị trùng.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.2
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 09/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**: BE: Nguyễn Anh Quân | FE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Tại mainflow, bước 6 sẽ có trước hay bước 5 có trước? Chưa thấy có AC cho việc kiểm tra lịch trùng. Chưa có AC cho EXC-02, EXC-03, ALT-01, ALT-02."* — Nguyễn Đức Bình · 08:52 03/09/2026
  - **Phản hồi & Cập nhật**:
    1. Đã chuẩn hóa thứ tự Main Flow: Hệ thống hiển thị thông tin content và danh sách các nền tảng kết nối khả dụng (System action - bước 4) trước, sau đó Admin mới thực hiện chọn nền tảng (User action - bước 5). Đồng thời chuyển bước Quản trị viên nhấn "Lên lịch" (bước 7) lên trước bước Hệ thống kiểm tra và xác thực dữ liệu (bước 8).
    2. Đã bổ sung đầy đủ **AC-005** cho việc kiểm tra lịch trùng (tương ứng **EXC-02** và **BR-006**).
    3. Đã bổ sung và chuẩn hóa đầy đủ Acceptance Criteria: **AC-009** (tương ứng **ALT-01**), **AC-010** (tương ứng **ALT-02**), **AC-011** (tương ứng **EXC-03**), và **AC-012** (tương ứng **BR-048**).

---

## Conditions
- **Preconditions**: 
  - Quản trị viên đã đăng nhập vào trang admin.
- **Trigger**: 
  - Quản trị viên chọn mục "Quản lí bài đăng" trên menu điều hướng của trang admin.

---

## Flow

### Main Flow
1. Admin truy cập màn hình danh sách các content.
2. Hệ thống hiển thị danh sách content đã lưu để Admin lựa chọn lên lịch đăng.
3. Admin chọn một content muốn lên lịch đăng.
4. Hệ thống hiển thị giao diện thiết lập lịch đăng bài, bao gồm: thông tin chi tiết content & hình ảnh/media đính kèm để xem trước, đồng thời hiển thị danh sách các nền tảng mạng xã hội đang kết nối khả dụng và bộ chọn thời gian đăng.
5. Admin lựa chọn một hoặc nhiều nền tảng muốn đăng (Facebook, Instagram, Zalo OA...).
6. Admin chọn ngày và thời gian muốn đăng bài (thời gian trong tương lai).
7. Admin nhấn "Lên lịch".
8. Hệ thống thực hiện kiểm tra tính hợp lệ của toàn bộ dữ liệu:
   - Nội dung còn tồn tại, đã được lưu và không để trống (BR-046).
   - Media còn tồn tại, đã được lưu và đúng định dạng hỗ trợ (JPG/JPEG/PNG, $\le$ 10MB, tối đa 10 ảnh) (BR-001, BR-002, BR-003).
   - Đã chọn ít nhất một nền tảng đang kết nối hợp lệ, không trùng lặp nền tảng (BR-005, BR-047).
   - Ngày và giờ đăng lớn hơn thời điểm hiện tại của hệ thống (BR-004).
   - Không có lịch đăng trùng đồng thời nội dung, nền tảng và thời điểm với bất kỳ lịch nào đang ở trạng thái "Đã lên lịch" (BR-006).
9. Hệ thống tạo lịch đăng mới. Lịch được lưu với trạng thái ban đầu: **"Đã lên lịch"**.
10. Hệ thống hiển thị thông báo: *"Lên lịch đăng bài thành công"* và điều hướng về màn hình danh sách.

### Alternative Flow
- **ALT-01 — Admin thay đổi nền tảng trước khi lên lịch**:
  - Tại bước chọn nền tảng của Luồng chính, Admin thay đổi (chọn thêm hoặc bỏ bớt) các nền tảng mạng xã hội đã chọn.
  - Hệ thống cập nhật hiển thị lựa chọn tương ứng trên giao diện (client-side) và chưa gửi bất kỳ yêu cầu nào về Server.
  - Admin tiếp tục bước thiết lập thời gian và nhấn "Lên lịch".
- **ALT-02 — Admin hủy tạo lịch**:
  - Trước khi nhấn "Lên lịch", Admin chọn "Hủy".
  - Hệ thống hủy bỏ toàn bộ thao tác, không tạo lịch đăng và không gửi dữ liệu về Server.
  - Hệ thống điều hướng quay về màn hình danh sách content.

### Exception Flow
- **EXC-01 — Dữ liệu lên lịch không hợp lệ**:
  - Tại bước 8 của Luồng chính, nếu dữ liệu không thỏa mãn: không chọn nền tảng, hoặc không chọn thời gian đăng, hoặc thời gian đăng nhỏ hơn/bằng thời điểm hiện tại, hoặc content/media không còn tồn tại hoặc sai định dạng.
  - Hệ thống không tạo lịch đăng, hiển thị thông báo lỗi cụ thể tại các trường dữ liệu tương ứng và giữ nguyên dữ liệu hợp lệ mà Admin đã nhập.
- **EXC-02 — Lịch bị trùng với một lịch khác**:
  - Tại bước 8 của Luồng chính, nếu phát hiện lịch đăng mới trùng đồng thời nội dung, nền tảng và thời gian với một lịch đang ở trạng thái "Đã lên lịch".
  - Hệ thống chặn tạo lịch, không lưu dữ liệu về Server, hiển thị thông báo lỗi chỉ rõ nền tảng và thời điểm đang bị trùng lặp, giữ nguyên dữ liệu trên biểu mẫu để Admin điều chỉnh.
- **EXC-03 — Token của nền tảng hết hạn khi đăng bài**:
  - Khi đến thời điểm tự động đăng bài, token xác thực của một nền tảng đã hết hạn hoặc bị thu hồi (`AUTH_EXPIRED`).
  - Hệ thống không thực hiện đăng lên nền tảng đó, không tự động retry, cập nhật trạng thái riêng của nền tảng đó thành "Đăng thất bại" kèm mã lỗi, giữ nguyên kết quả của các nền tảng khác, và gửi thông báo cho Admin yêu cầu kết nối lại tài khoản nền tảng.
- **EXC-04 — Nền tảng trả về rate-limit hoặc timeout**:
  - Khi hệ thống gọi API nền tảng để đăng bài, nền tảng từ chối request do vượt giới hạn gọi API (`RATE_LIMIT`) hoặc mất kết nối tạm thời (`TIMEOUT`).
  - Hệ thống kích hoạt cơ chế tự động thử lại (Retry) tối đa 3 lần. Nếu đăng lại thành công $\rightarrow$ cập nhật "Đăng thành công". Nếu đã thử lại đủ 3 lần mà vẫn thất bại $\rightarrow$ cập nhật trạng thái nền tảng thành "Đăng thất bại", lưu lý do lỗi và hiển thị nút "Thử lại" thủ công cho Admin.
- **EXC-05 — Nội dung bị nền tảng từ chối do vi phạm chính sách**:
  - Nền tảng từ chối bài viết do nội dung văn bản hoặc hình ảnh vi phạm tiêu chuẩn cộng đồng / chính sách (`POLICY_VIOLATION`).
  - Hệ thống tuyệt đối không tự động retry, cập nhật trạng thái nền tảng thành "Đăng thất bại", lưu lý do từ chối chi tiết và gửi thông báo cảnh báo cho Admin.

---

## Acceptance Criteria

- **AC-001 — Chỉ hiển thị content đủ điều kiện để lên lịch**:
  - **Given**: Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - **When**: Quản trị viên truy cập chức năng lên lịch đăng bài từ màn hình quản lý.
  - **Then**: Hệ thống chỉ hiển thị danh sách các content hợp lệ, đã được lưu trong hệ thống để Quản trị viên lựa chọn.

- **AC-002 — Chưa chọn nền tảng khi bấm lên lịch**:
  - **Given**: Quản trị viên đã chọn content và thời gian đăng hợp lệ.
  - **When**: Quản trị viên chưa chọn bất kỳ nền tảng nào và nhấn "Lên lịch".
  - **Then**: Hệ thống không tạo lịch đăng và không lưu dữ liệu vào cơ sở dữ liệu.
  - **And**: Hiển thị thông báo lỗi yêu cầu Quản trị viên chọn ít nhất một nền tảng mạng xã hội kết nối hợp lệ.

- **AC-003 — Tự động thực hiện đăng bài đúng thời gian đã lên lịch**:
  - **Given**: Một lịch đăng hợp lệ đang ở trạng thái "Đã lên lịch".
  - **When**: Đến đúng ngày và giờ đăng đã được thiết lập.
  - **Then**: Hệ thống tự động kích hoạt tiến trình gửi bài đăng lên các nền tảng mạng xã hội đã chọn.
  - **And**: Quản trị viên không cần thực hiện thêm bất kỳ thao tác thủ công nào tại thời điểm đăng bài.

- **AC-004 — Đăng bài thành công trên tất cả nền tảng**:
  - **Given**: Lịch đăng đã đến thời điểm thực hiện tác vụ tự động đăng bài.
  - **When**: Tất cả các nền tảng mạng xã hội được chọn đều phản hồi kết quả đăng thành công.
  - **Then**: Hệ thống cập nhật trạng thái riêng của từng nền tảng thành "Đăng thành công".
  - **And**: Cập nhật trạng thái tổng thể của toàn bộ lịch đăng thành "Đăng thành công".

- **AC-005 — Kiểm tra chống trùng lặp lịch đăng bài**:
  - **Given**: Trong hệ thống đã tồn tại một lịch đăng ở trạng thái "Đã lên lịch" với cùng Content, cùng Nền tảng và cùng Thời điểm đăng.
  - **When**: Quản trị viên thiết lập một lịch đăng mới có trùng đồng thời cả 3 thông tin trên và nhấn "Lên lịch".
  - **Then**: Hệ thống chặn tạo lịch đăng mới và không gửi lưu dữ liệu về server.
  - **And**: Hiển thị thông báo lỗi chỉ rõ nền tảng cùng thời điểm đang bị trùng lặp để Quản trị viên điều chỉnh.

- **AC-006 — Xử lý lỗi rate-limit từ nền tảng khi tự động đăng bài**:
  - **Given**: Hệ thống đang gọi API gửi bài đăng lên nền tảng mạng xã hội theo lịch.
  - **When**: Nền tảng trả về mã lỗi giới hạn tần suất gọi API (`RATE_LIMIT`) hoặc mất kết nối tạm thời (`TIMEOUT`).
  - **Then**: Hệ thống tự động kích hoạt cơ chế thử lại (Retry) với tối đa 3 lần.
  - **And**: Nếu thử lại thành công thì cập nhật trạng thái "Đăng thành công"; nếu sau 3 lần vẫn thất bại thì cập nhật trạng thái nền tảng thành "Đăng thất bại", lưu lý do lỗi và hiển thị nút "Thử lại" thủ công cho Quản trị viên.

- **AC-007 — Xử lý bài đăng bị nền tảng từ chối do vi phạm chính sách**:
  - **Given**: Hệ thống gửi nội dung và hình ảnh đến nền tảng mạng xã hội theo lịch.
  - **When**: Nền tảng từ chối đăng bài do nội dung hoặc hình ảnh vi phạm tiêu chuẩn cộng đồng / chính sách (`POLICY_VIOLATION`).
  - **Then**: Hệ thống tuyệt đối không tự động thử lại (no retry).
  - **And**: Cập nhật trạng thái nền tảng tương ứng thành "Đăng thất bại", lưu chi tiết nguyên nhân từ chối và gửi thông báo cảnh báo cho Quản trị viên.

- **AC-008 — Kiểm tra tính hợp lệ của toàn bộ dữ liệu trước khi tạo lịch**:
  - **Given**: Quản trị viên đang ở màn hình thiết lập lịch đăng bài.
  - **When**: Quản trị viên nhấn nút "Lên lịch".
  - **Then**: Hệ thống chỉ tạo lịch thành công khi:
    - Nội dung còn tồn tại, đã lưu và không để trống.
    - Hình ảnh đính kèm đúng định dạng (JPG/JPEG/PNG), dung lượng $\le$ 10MB và số lượng $\le$ 10 ảnh.
    - Đã chọn ít nhất một nền tảng đang kết nối hợp lệ.
    - Ngày và giờ đăng được nhập đầy đủ và lớn hơn thời điểm hiện tại của hệ thống.
  - **And**: Nếu có bất kỳ trường thông tin nào không hợp lệ, hệ thống không tạo lịch, hiển thị thông báo lỗi tại trường tương ứng và giữ nguyên toàn bộ dữ liệu hợp lệ Quản trị viên đã nhập.

- **AC-009 — Thay đổi lựa chọn nền tảng trước khi nhấn lên lịch**:
  - **Given**: Quản trị viên đang ở màn hình thiết lập lịch đăng bài.
  - **When**: Quản trị viên thay đổi việc chọn hoặc bỏ chọn một hoặc nhiều nền tảng và chưa nhấn "Lên lịch".
  - **Then**: Hệ thống cập nhật hiển thị trạng thái chọn trên giao diện người dùng.
  - **And**: Không có dữ liệu hay yêu cầu tạo lịch nào được gửi về máy chủ cho đến khi Quản trị viên nhấn nút "Lên lịch".

- **AC-010 — Hủy thiết lập lịch đăng bài**:
  - **Given**: Quản trị viên đang thao tác thiết lập lịch đăng bài.
  - **When**: Quản trị viên nhấn nút "Hủy".
  - **Then**: Hệ thống hủy bỏ toàn bộ thao tác, không tạo lịch đăng và không lưu bất kỳ dữ liệu nào.
  - **And**: Hệ thống điều hướng quay trở lại màn hình danh sách content ban đầu.

- **AC-011 — Xử lý lỗi Token nền tảng hết hạn khi đăng bài**:
  - **Given**: Lịch đăng đã được lên lịch thành công cho nhiều nền tảng mạng xã hội.
  - **When**: Đến thời điểm đăng bài, Token xác thực của một trong các nền tảng đã hết hạn hoặc bị thu hồi (`AUTH_EXPIRED`).
  - **Then**: Hệ thống hủy thao tác đăng trên nền tảng bị lỗi token, cập nhật trạng thái nền tảng đó thành "Đăng thất bại" và không tự động thử lại.
  - **And**: Hệ thống hiển thị thông báo lỗi hết hạn Token kèm khuyến nghị Quản trị viên kết nối lại tài khoản nền tảng.

- **AC-012 — Ghi nhận kết quả đăng bài độc lập theo từng nền tảng**:
  - **Given**: Một lịch đăng được thiết lập gửi lên đồng thời nhiều nền tảng (ví dụ: Facebook và Instagram).
  - **When**: Đến giờ đăng, một nền tảng đăng thành công và một nền tảng gặp lỗi (thất bại).
  - **Then**: Hệ thống ghi nhận trạng thái "Đăng thành công" cho nền tảng thành công và "Đăng thất bại" kèm chi tiết lỗi cho nền tảng thất bại.
  - **And**: Lỗi của nền tảng này không làm gián đoạn hay ảnh hưởng đến kết quả thành công của nền tảng khác, và trạng thái tổng thể của lịch được cập nhật là "Thành công một phần".

---

## References

### Business Rules
- [BR-001: Định dạng file ảnh đính kèm bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-001.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f47d6261-cae6-4ed8-b879-249b26d17464))
- [BR-002: Dung lượng tối đa của ảnh đính kèm](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-002.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/4f9d052b-085c-404f-a824-3aba19aff7a1))
- [BR-003: Giới hạn số lượng ảnh đính kèm bài đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-003.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/829d7c04-6572-41e0-99ab-bde96247c61f))
- [BR-004: Thời điểm hẹn giờ đăng bài hợp lệ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-004.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/0b6c451c-d250-4513-aa5d-7354e6e4537b))
- [BR-005: Bắt buộc chọn nền tảng mạng xã hội](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-005.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/b69f2298-64eb-4355-ab2d-d8f18afdc46e))
- [BR-006: Chống trùng lặp lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-006.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/94ad04a1-10ed-4d41-9345-aa73112ca1cb))
- [BR-046: Nội dung hợp lệ của lịch đăng bài](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-046.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a90db8e1-0a6f-4bac-90d1-637449f6c024))
- [BR-047: Nền tảng hợp lệ và không trùng trong lịch đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-047.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f8a54fb3-59bc-4856-a49f-ad5155633da0))
- [BR-048: Trạng thái đăng được ghi nhận riêng theo nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-048.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/853f7ce5-9493-499a-8304-68c56403ac74))
- [BR-049: Xử lý lỗi từ nền tảng khi tự động đăng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-049.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/841ae103-814f-49d5-bba4-2f3a7e354756))

---

## Non-Functional
- Không có

---

## Out of Scope
- Không có
