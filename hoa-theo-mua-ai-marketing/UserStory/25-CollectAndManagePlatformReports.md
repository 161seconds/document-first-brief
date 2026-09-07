# STORY-025: Thu thập và quản lý báo cáo từ các nền tảng

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn hệ thống tự động lấy thông tin báo cáo từ các nền tảng đã kết nối vào thời điểm đã cấu hình, sau đó tập hợp và lưu thành các báo cáo trong hệ thống, để khi cần tôi có thể xem và tải báo cáo mà không phải truy cập từng nền tảng.
- **Context**: 
  - Hệ thống được kết nối với Facebook, Instagram, Zalo Official Account (Zalo OA) và các nền tảng khác được hỗ trợ.
  - Quản trị viên có thể cấu hình lịch thu thập báo cáo độc lập cho từng nền tảng theo: **Hằng ngày**, **Hằng tuần**, hoặc **Hằng tháng** (sử dụng chuẩn múi giờ `Asia/Ho_Chi_Minh`).
    - *Hằng ngày*: Phải có giờ thực hiện (ví dụ: 08:00).
    - *Hằng tuần*: Phải có thứ trong tuần và giờ thực hiện (ví dụ: Thứ Hai, 09:00).
    - *Hằng tháng*: Chọn một ngày cụ thể trong tháng (từ ngày 1 đến ngày 31) và giờ thực hiện. Nếu ngày được chọn không tồn tại trong một tháng cụ thể (ví dụ: ngày 31 vào tháng có 30 ngày, hoặc ngày 29/30/31 vào tháng 2 năm không nhuận), hệ thống sẽ tự động thực hiện vào ngày cuối cùng của tháng đó mà không làm thay đổi ngày cấu hình ban đầu.
  - Mỗi nền tảng chỉ thực hiện một lần tại một thời điểm đã cấu hình.
  - Khi đến thời điểm, hệ thống tự động gọi API lấy thông tin báo cáo mà tài khoản kết nối được phép xem.
  - Hệ thống tập hợp dữ liệu nhận được từ từng nền tảng thành báo cáo tương ứng và lưu vào cơ sở dữ liệu kèm: Tên nền tảng, thời điểm thu thập, thời điểm tạo báo cáo, số lượng dữ liệu nhận được và trạng thái xử lý ("Thành công", "Không có dữ liệu", "Thất bại").
  - Quản trị viên truy cập chức năng Báo cáo để xem danh sách, xem chi tiết và tải báo cáo về máy tính dưới định dạng **file XLSX (.xlsx)**.
  - **Lưu ý**: Hệ thống không hiển thị các báo cáo này trên trang tổng quan (Dashboard). Báo cáo trong story này phản ánh dữ liệu từ từng nền tảng riêng rẽ. Việc tạo báo cáo tổng hợp định kỳ kết hợp nhiều nền tảng thuộc phạm vi STORY-026.
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
- **Feedback gần nhất**:
  > *"Cần thêm định dạng tệp tải xuống Nói rõ thời gian thu thập báo cáo Admin có thể config"* — Nguyễn Đức Bình · 10:17 03/09/2026

---

## Conditions
- **Preconditions**:
  - Ít nhất một nền tảng đã được kết nối với hệ thống và đang được bật để thu thập thông tin báo cáo.
  - Thông tin kết nối (Token/Credential) của nền tảng còn hiệu lực và tài khoản có quyền xem thông tin báo cáo.
  - Lịch thu thập (ngày/thứ/giờ) đã được Quản trị viên cấu hình cho từng nền tảng.
  - Quản trị viên đã đăng nhập vào hệ thống khi truy cập chức năng Báo cáo.
- **Trigger**:
  - **Luồng tự động**: Kích hoạt khi đến ngày/thứ/giờ đã được cấu hình cho một nền tảng (theo múi giờ `Asia/Ho_Chi_Minh`).
  - **Luồng xem/tải**: Bắt đầu khi Quản trị viên truy cập chức năng Báo cáo.

---

## Flow

### Main Flow — Thu thập, lưu, xem và tải báo cáo
1. Đến ngày và giờ đã cấu hình, hệ thống bắt đầu một phiên thu thập tự động và ghi nhận trạng thái “Đang xử lý”.
2. Hệ thống xác định các nền tảng đang được bật cấu hình thu thập tại mốc thời gian này.
3. Đối với từng nền tảng, hệ thống kiểm tra token kết nối còn hiệu lực và tài khoản có quyền xem báo cáo.
4. Hệ thống gửi yêu cầu lấy dữ liệu báo cáo từ API của nền tảng.
5. Nền tảng phản hồi và trả về dữ liệu báo cáo cho hệ thống.
6. Hệ thống kiểm tra quá trình lấy thông tin đã hoàn tất và dữ liệu nhận được đầy đủ (nếu phân trang, lấy hết tất cả các trang dữ liệu).
7. Hệ thống tiếp tục thực hiện các bước kiểm tra và thu thập đối với các nền tảng còn lại (nếu có nhiều nền tảng chạy cùng giờ).
8. Sau khi hoàn tất việc thu thập, hệ thống tập hợp dữ liệu nhận được từ từng nền tảng thành báo cáo tương ứng.
9. Hệ thống lưu dữ liệu thô và bản ghi báo cáo vào cơ sở dữ liệu.
10. Hệ thống lưu kèm: Tên nền tảng, thời điểm thu thập, thời điểm tạo báo cáo, số lượng dữ liệu nhận được và cập nhật trạng thái “Thành công”.
11. Khi cần xem báo cáo, Quản trị viên truy cập chức năng “Báo cáo”.
12. Hệ thống truy vấn CSDL và hiển thị danh sách tất cả các báo cáo đã lưu (không hiển thị trên Dashboard).
13. Quản trị viên chọn một báo cáo cụ thể, hệ thống hiển thị đầy đủ nội dung chi tiết của báo cáo đó.
14. Khi Quản trị viên nhấn nút “Tải báo cáo”, hệ thống xuất dữ liệu của báo cáo đã chọn ra tệp định dạng **XLSX (.xlsx)** và cung cấp tệp tải về máy tính của Quản trị viên.

### Alternative Flow
- **ALT-01 — Chỉ có một nền tảng cần thu thập**:
  - Tại bước 2, hệ thống xác định chỉ có một nền tảng đến hạn thu thập.
  - Hệ thống chỉ lấy thông tin, tạo báo cáo và ghi nhận trạng thái riêng cho nền tảng đó.
- **ALT-02 — Nền tảng không có thông tin báo cáo**:
  - Tại bước 5, nền tảng phản hồi thành công (HTTP 200) nhưng số lượng bản ghi bằng 0 (không có dữ liệu).
  - Hệ thống ghi nhận số lượng dữ liệu bằng 0, lưu trạng thái “Không có dữ liệu”, không tạo báo cáo rỗng và tiếp tục xử lý các nền tảng khác.
- **ALT-03 — Chưa có báo cáo nào được lưu trong hệ thống**:
  - Tại bước 12, Quản trị viên mở trang Báo cáo nhưng chưa có lần thu thập thành công nào.
  - Hệ thống hiển thị thông báo: *"Chưa có báo cáo"* (Empty State), không tạo bản ghi rỗng và cho phép Quản trị viên sử dụng các chức năng khác bình thường.

### Exception Flow
- **EXC-01 — Không thể kết nối với nền tảng hoặc token hết hạn**:
  - Tại bước 3 hoặc 4, hệ thống không thể kết nối hoặc API nền tảng báo lỗi xác thực (Token Expired / Unauthorized).
  - Hệ thống dừng thu thập của nền tảng đó, không tạo báo cáo mới, ghi nhận trạng thái “Thất bại”, lưu lý do lỗi vào nhật ký hệ thống và tiếp tục xử lý các nền tảng khác.
- **EXC-02 — Nền tảng không phản hồi hoặc trả về thông tin chưa đầy đủ**:
  - Tại bước 4, 5 hoặc 6, API nền tảng bị timeout hoặc phản hồi thiếu dữ liệu dở dang.
  - Hệ thống tự động thử lại (Retry) theo số lần cấu hình trước (tối đa 3 lần). Nếu vẫn không thành công, hệ thống ghi nhận trạng thái “Thất bại” kèm lý do, không dùng dữ liệu chắp vá để tạo báo cáo.
- **EXC-03 — Không thể tạo hoặc lưu báo cáo vào CSDL**:
  - Tại bước 8, 9 hoặc 10, quá trình tập hợp hoặc lưu báo cáo gặp lỗi CSDL.
  - Hệ thống không ghi nhận báo cáo thành công, rollback dữ liệu dở dang, giữ nguyên vẹn dữ liệu các lần thu thập trước đó, ghi nhận trạng thái “Thất bại” và lưu nguyên nhân lỗi.
- **EXC-04 — Không thể mở báo cáo đã chọn**:
  - Tại bước 13, báo cáo bị lỗi định dạng dữ liệu hoặc đã bị xóa.
  - Hệ thống không hiển thị dữ liệu sai lệch hoặc nội dung của báo cáo khác, hiển thị thông báo lỗi: *"Không thể mở báo cáo. Vui lòng thử lại sau"* và đưa Quản trị viên trở lại danh sách báo cáo.
- **EXC-05 — Không thể tạo tệp hoặc tải báo cáo XLSX**:
  - Tại bước 14, quá trình tạo tệp XLSX gặp sự cố trên máy chủ.
  - Hệ thống không cung cấp tệp hỏng/thiếu dữ liệu, hiển thị thông báo lỗi: *"Không thể tạo tệp tải xuống, vui lòng thử lại"* và giữ nguyên báo cáo trên CSDL để Quản trị viên tải lại.

---

## Acceptance Criteria

- **AC-001 — Kích hoạt thu thập đúng thời điểm cấu hình**:
  - **Given**: Ngày và giờ thu thập đã được cấu hình cho một nền tảng.
  - **When**: Đến đúng ngày và giờ đã cấu hình (theo múi giờ `Asia/Ho_Chi_Minh`).
  - **Then**: Hệ thống bắt đầu tiến trình thu thập và ghi nhận trạng thái “Đang xử lý”.

- **AC-002 — Xác định và kiểm tra tính hợp lệ của nền tảng cần thu thập**:
  - **Given**: Có một hoặc nhiều nền tảng đang được bật để thu thập.
  - **When**: Hệ thống bắt đầu phiên thu thập.
  - **Then**: Hệ thống xác định danh sách các nền tảng đến hạn và kiểm tra token/quyền kết nối.
  - **And**: Hệ thống chỉ gửi yêu cầu API đến nền tảng có kết nối còn hiệu lực và tài khoản có quyền xem báo cáo.

- **AC-003 — Thu thập đầy đủ thông tin báo cáo từ nền tảng**:
  - **Given**: Nền tảng đang bật và thông tin kết nối còn hiệu lực.
  - **When**: Hệ thống gửi yêu cầu lấy thông tin báo cáo.
  - **Then**: Hệ thống nhận dữ liệu báo cáo mà nền tảng cung cấp tại thời điểm thu thập.
  - **And**: Nếu dữ liệu phân trang, hệ thống tự động gọi tiếp cho đến khi hoàn tất 100% dữ liệu.
  - **And**: Hệ thống chỉ đánh dấu thu thập thành công sau khi nhận đủ dữ liệu trọn vẹn.

- **AC-004 — Xử lý độc lập riêng từng nền tảng**:
  - **Given**: Có nhiều nền tảng cùng được cấu hình thu thập tại một thời điểm.
  - **When**: Hệ thống thực hiện thu thập.
  - **Then**: Hệ thống xử lý và ghi nhận kết quả độc lập cho từng nền tảng.
  - **And**: Sự cố ở một nền tảng không làm gián đoạn hoặc mất dữ liệu báo cáo của các nền tảng khác.

- **AC-005 — Tập hợp và lưu báo cáo thành công**:
  - **Given**: Hệ thống đã nhận trọn vẹn thông tin báo cáo từ một nền tảng.
  - **When**: Hệ thống tập hợp và lưu kết quả.
  - **Then**: Hệ thống tạo bản ghi báo cáo tương ứng và lưu vào cơ sở dữ liệu.
  - **And**: Báo cáo được lưu kèm: Tên nền tảng, thời điểm thu thập, thời điểm tạo báo cáo, số lượng dữ liệu nhận được và trạng thái “Thành công”.

- **AC-006 — Xử lý khi nền tảng không có thông tin báo cáo (ALT-02)**:
  - **Given**: Nền tảng phản hồi thành công nhưng số lượng dữ liệu bằng 0.
  - **When**: Hệ thống kiểm tra kết quả trả về.
  - **Then**: Hệ thống ghi nhận số lượng dữ liệu bằng 0, trạng thái “Không có dữ liệu” và không tạo bản ghi báo cáo trống.

- **AC-007 — Hiển thị danh sách báo cáo đúng nơi quy định**:
  - **Given**: Có ít nhất một báo cáo đã được lưu trong hệ thống.
  - **When**: Quản trị viên truy cập chức năng Báo cáo.
  - **Then**: Hệ thống hiển thị danh sách tất cả các báo cáo đã lưu kèm: Tên nền tảng, thời điểm tạo báo cáo, số lượng dữ liệu và trạng thái xử lý.
  - **And**: Hệ thống tuyệt đối không hiển thị các báo cáo này trên trang tổng quan (Dashboard).

- **AC-008 — Hiển thị thông báo khi chưa có báo cáo nào (ALT-03)**:
  - **Given**: Chưa có báo cáo nào được lưu trong CSDL.
  - **When**: Quản trị viên truy cập chức năng Báo cáo.
  - **Then**: Hệ thống hiển thị thông báo: *"Chưa có báo cáo"*.
  - **And**: Hệ thống không tạo bản ghi báo cáo rỗng và Quản trị viên có thể tiếp tục sử dụng các chức năng khác bình thường.

- **AC-009 — Xem chi tiết báo cáo**:
  - **Given**: Báo cáo đã được lưu thành công trong CSDL.
  - **When**: Quản trị viên chọn một báo cáo trong danh sách.
  - **Then**: Hệ thống hiển thị đầy đủ các chỉ số và dữ liệu chi tiết thuộc đúng báo cáo đã chọn ở chế độ chỉ xem.
  - **And**: Hệ thống không làm thay đổi dữ liệu báo cáo trong quá trình hiển thị.
  - **And**: Quản trị viên có thể bấm quay lại danh sách báo cáo bất kỳ lúc nào.

- **AC-010 — Tải báo cáo định dạng XLSX thành công**:
  - **Given**: Báo cáo đã được lưu trong hệ thống.
  - **When**: Quản trị viên nhấn nút “Tải báo cáo”.
  - **Then**: Hệ thống kết xuất dữ liệu báo cáo thành tệp định dạng **XLSX (.xlsx)** chuẩn Excel.
  - **And**: Tệp tải về có cấu trúc bảng biểu, tiêu đề cột và nội dung trùng khớp chính xác với báo cáo đã chọn.

- **AC-011 — Xử lý lỗi kết nối hoặc dữ liệu chưa đầy đủ (EXC-01, EXC-02)**:
  - **Given**: Nền tảng không thể kết nối, token hết hạn, hoặc dữ liệu trả về bị lỗi/chưa đầy đủ sau số lần thử lại tối đa.
  - **When**: Hệ thống kết thúc lần thu thập.
  - **Then**: Hệ thống ghi nhận trạng thái “Thất bại”, lưu lý do chi tiết vào nhật ký, không tạo báo cáo từ dữ liệu chắp vá và tiếp tục xử lý các nền tảng khác.

- **AC-012 — Không lưu dữ liệu báo cáo dở dang khi gặp lỗi (EXC-03)**:
  - **Given**: Hệ thống đang trong quá trình tập hợp hoặc lưu báo cáo.
  - **When**: Quá trình lưu gặp lỗi CSDL.
  - **Then**: Hệ thống rollback toàn bộ, không lưu bản ghi dở dang, ghi nhận trạng thái “Thất bại” và bảo toàn dữ liệu các báo cáo đã lưu trước đó.

- **AC-013 — Xử lý lỗi khi mở hoặc tải báo cáo (EXC-04, EXC-05)**:
  - **Given**: Quản trị viên đã chọn một báo cáo để xem hoặc tải về.
  - **When**: Hệ thống không thể đọc nội dung hoặc không thể kết xuất tệp XLSX.
  - **Then**: Hệ thống hiển thị thông báo lỗi phù hợp, không cung cấp tệp lỗi/thiếu dữ liệu và giữ nguyên dữ liệu báo cáo trên hệ thống.

- **AC-014 — Lịch thu thập hằng ngày**:
  - **Given**: Quản trị viên cấu hình loại lịch “Hằng ngày”.
  - **When**: Quản trị viên nhập giờ hợp lệ (ví dụ: 08:00) và lưu cấu hình.
  - **Then**: Hệ thống tự động thực hiện thu thập báo cáo vào đúng giờ đó mỗi ngày.

- **AC-015 — Lịch thu thập hằng tuần**:
  - **Given**: Quản trị viên cấu hình loại lịch “Hằng tuần”.
  - **When**: Quản trị viên chọn thứ trong tuần (ví dụ: Thứ Hai), nhập giờ hợp lệ và lưu cấu hình.
  - **Then**: Hệ thống tự động thực hiện thu thập báo cáo vào đúng thứ và giờ đã chọn mỗi tuần.

- **AC-016 — Lịch thu thập hằng tháng với ngày tồn tại trong tháng**:
  - **Given**: Quản trị viên cấu hình lịch hằng tháng vào ngày 15 lúc 08:00.
  - **When**: Đến ngày 15 của tháng tại thời điểm 08:00.
  - **Then**: Hệ thống tự động bắt đầu thu thập báo cáo cho nền tảng.

- **AC-017 — Lịch thu thập hằng tháng với ngày không tồn tại trong tháng hiện tại**:
  - **Given**: Quản trị viên cấu hình lịch hằng tháng vào ngày không tồn tại trong tháng hiện tại (ví dụ: ngày 31 đối với tháng có 30 ngày, hoặc ngày 29/30/31 đối với tháng 2 năm không nhuận).
  - **When**: Đến ngày cuối cùng của tháng đó tại đúng giờ đã cấu hình.
  - **Then**: Hệ thống tự động bắt đầu thu thập báo cáo của nền tảng trong ngày cuối cùng đó mà không chờ sang tháng tiếp theo.
  - **And**: Ngày cấu hình gốc của Quản trị viên (ngày 31) được giữ nguyên vẹn cho các tháng sau.

---

## References

### Business Rules
- [BR-072: Loại lịch thu thập báo cáo và dữ liệu bắt buộc](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-072.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/869a0fdc-0466-49ed-974b-4b32e56637a8))
- [BR-073: Múi giờ và ngày thực hiện lịch hằng tháng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-073.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/dff4b12d-1e46-495d-84fa-24e0b40acafd))
- [BR-074: Một lần thu thập cho mỗi nền tảng tại một thời điểm](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-074.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/ebee11e8-268d-45ff-bcef-26e35f4e25db))
- [BR-075: Điều kiện nền tảng được thu thập báo cáo](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-075.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/02a51e91-1297-48e7-851a-7aeed98a73ba))
- [BR-076: Thu thập báo cáo độc lập theo từng nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-076.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/2d88ab0a-7bad-4790-b5fc-69e45531f229))
- [BR-077: Chỉ tạo báo cáo từ dữ liệu đầy đủ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-077.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/63972f8f-2185-44ae-9bf6-e1b0b1dbdfc1))
- [BR-078: Thông tin bắt buộc của báo cáo nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-078.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/5b8ac8cd-3750-4a12-a0c1-6b8dc3a7f317))
- [BR-079: Định dạng tệp báo cáo tải xuống](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-079.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/f58fe6a9-b92b-44c6-bb0b-84b3505b9366))

---

## Non-Functional
- **Tính trọn vẹn dữ liệu**: Hệ thống chỉ ghi nhận một nền tảng đã thu thập thành công sau khi nhận được đầy đủ 100% dữ liệu mà API cung cấp.
- **Tính cô lập lỗi**: Sự cố của một nền tảng tuyệt đối không được làm ảnh hưởng hoặc làm mất dữ liệu của nền tảng khác.
- **Không lưu dữ liệu rác**: Tuyệt đối không lưu bản ghi báo cáo hoặc tạo tệp tải xuống bị lỗi hoặc chưa hoàn chỉnh.
- **Audit Logging**: Hệ thống phải tự động ghi nhật ký thời điểm bắt đầu, thời điểm kết thúc, trạng thái xử lý và nguyên nhân lỗi chi tiết của mỗi phiên thu thập, tạo báo cáo và tải tệp.
- **Tính chính xác của tệp XLSX**: Tệp XLSX tải về phải phản ánh chính xác số liệu báo cáo đã lưu của đúng phiên được chọn.

---

## Out of Scope
- Không tạo hoặc thay đổi thông tin xác thực kết nối của nền tảng.
- Không chỉnh sửa trực tiếp dữ liệu hoặc nội dung của báo cáo đã lưu.
- Không hiển thị báo cáo hoặc biểu đồ số liệu trên trang tổng quan (Dashboard).
- Không tạo báo cáo tổng hợp định kỳ kết hợp từ nhiều nền tảng (thuộc phạm vi STORY-026).
