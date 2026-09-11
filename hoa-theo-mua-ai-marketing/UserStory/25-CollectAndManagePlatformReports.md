# STORY-025: Thu thập và quản lý báo cáo từ các nền tảng

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn hệ thống tự động lấy thông tin báo cáo từ các nền tảng đã kết nối vào thời điểm đã cấu hình, sau đó tập hợp và lưu thành các báo cáo trong hệ thống, để khi cần tôi có thể xem và tải báo cáo mà không phải truy cập từng nền tảng.
- **Context**: 
  - Hệ thống được kết nối với Facebook, Instagram, Zalo Official Account và các nền tảng khác được hỗ trợ. Mỗi nền tảng có ngày và giờ thu thập riêng.
  - Khi đến thời điểm đã cấu hình, hệ thống tự động lấy thông tin báo cáo mà tài khoản kết nối được phép xem.
  - Hệ thống tập hợp thông tin nhận được từ từng nền tảng thành báo cáo tương ứng và lưu dữ liệu cùng báo cáo vào cơ sở dữ liệu của hệ thống. Mỗi báo cáo được lưu kèm tên nền tảng, thời điểm thu thập, thời điểm tạo báo cáo, số lượng dữ liệu nhận được và trạng thái xử lý.
  - Khi cần, Quản trị viên truy cập chức năng Báo cáo để xem danh sách, xem nội dung chi tiết và tải báo cáo về thiết bị.
  - Hệ thống không hiển thị các báo cáo này trên trang tổng quan, còn gọi là Dashboard.
  - Báo cáo trong STORY-025 phản ánh dữ liệu lấy từ từng nền tảng. Việc tạo báo cáo tổng hợp định kỳ từ dữ liệu của nhiều nền tảng được thực hiện trong STORY-026.
  - Admin cấu hình giờ thu thập báo cáo theo ngày, hằng ngày, hoặc hàng tuần cho cả 3 nền tảng. Thời gian sử dụng múi giờ `Asia/Ho_Chi_Minh`.
  - Khi đến giờ đã cấu hình, hệ thống tự động lấy dữ liệu báo cáo từ nền tảng. Mỗi nền tảng chỉ được thực hiện một lần tại một thời điểm đã cấu hình.
  - Lịch “Hằng ngày” phải có giờ thực hiện.
  - Lịch “Hằng tuần” phải có thứ trong tuần và giờ thực hiện.
  - Lịch “Hằng tháng”, Admin phải chọn một ngày cụ thể trong tháng và giờ để thực hiện. Nếu ngày được chọn không tồn tại trong một tháng cụ thể, hệ thống sẽ thực hiện vào ngày cuối cùng của tháng đó mà không làm thay đổi ngày cấu hình ban đầu.
  - Định dạng của báo cáo tải xuống có dạng là **file XLSX (.xlsx)**.
- **Sprint**: S1
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Nháp
- **Cập nhật**: 11/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Nguyễn Anh Quân
- **Status**: Cần làm
- **Assignee**:
  - FE: Nguyễn Anh Quân
  - BE: Nguyễn Anh Quân
- **Creator**: Nguyễn Anh Quân
- **Feedback gần nhất**:
  > *"Cần thêm định dạng tệp tải xuống Nói rõ thời gian thu thập báo cáo Admin có thể config"* — Nguyễn Đức Bình · 10:17 03/09/2026
- **Thống kê tài liệu**: TDDs: 1 | Rules: 8 | Unit Tests: 0 | System Tests: 10

---

## Conditions

### Preconditions
- Ít nhất một nền tảng đã được kết nối với hệ thống và đang được bật để thu thập thông tin báo cáo.
- Thông tin kết nối của nền tảng còn hiệu lực và tài khoản kết nối có quyền xem thông tin báo cáo.
- Ngày và giờ thu thập thông tin báo cáo đã được cấu hình cho từng nền tảng.
- Quản trị viên đã đăng nhập.

### Trigger
- Luồng thu thập tự động bắt đầu khi đến ngày và giờ đã được cấu hình cho một nền tảng.
- Luồng xem và tải báo cáo bắt đầu khi Quản trị viên mở chức năng Báo cáo.

---

## Flow

### Main Flow — Thu thập, lưu, xem và tải báo cáo
1. Đến ngày và giờ đã được cấu hình, hệ thống bắt đầu một lần thu thập và ghi nhận trạng thái “Đang xử lý”.
2. Hệ thống xác định các nền tảng đang được bật để thu thập thông tin báo cáo.
3. Đối với từng nền tảng, hệ thống kiểm tra thông tin kết nối còn hiệu lực và tài khoản kết nối có quyền xem thông tin báo cáo.
4. Hệ thống gửi yêu cầu lấy các trường dữ liệu báo cáo được quy định cho Account/Page và từng Post/Article của nền tảng tại thời điểm thu thập; dữ liệu Post/Article chỉ thuộc các bài do hệ thống đã đăng.
5. Nền tảng trả về thông tin báo cáo cho hệ thống.
6. Hệ thống kiểm tra quá trình lấy thông tin đã hoàn tất và dữ liệu nhận được đầy đủ.
7. Hệ thống tiếp tục thực hiện các bước kiểm tra và thu thập đối với các nền tảng còn lại.
8. Sau khi hoàn tất việc thu thập, hệ thống tập hợp thông tin nhận được từ từng nền tảng thành báo cáo tương ứng.
9. Hệ thống lưu dữ liệu đã thu thập và báo cáo tương ứng vào cơ sở dữ liệu của hệ thống.
10. Hệ thống lưu kèm tên nền tảng, thời điểm thu thập, số lượng dữ liệu nhận được và trạng thái “Thành công”.
11. Khi cần xem báo cáo, Quản trị viên truy cập chức năng Báo cáo.
12. Hệ thống lấy tất cả báo cáo đã lưu và hiển thị danh sách báo cáo trong chức năng Báo cáo. Hệ thống không hiển thị các báo cáo này trên trang tổng quan.
13. Quản trị viên chọn một báo cáo và hệ thống hiển thị đầy đủ nội dung chi tiết của báo cáo đã chọn.
14. Khi Quản trị viên chọn tải báo cáo, hệ thống tạo tệp từ báo cáo đã lưu và cung cấp tệp để Quản trị viên tải về thiết bị. Định dạng XLSX.

### Alternative Flow

#### ALT-01 — Chỉ có một nền tảng cần thu thập
- Tại bước 2 của luồng chính, hệ thống xác định chỉ có một nền tảng đang được bật để thu thập thông tin báo cáo.
- Hệ thống chỉ lấy thông tin, tạo báo cáo và lưu kết quả của nền tảng đó.
- Trạng thái chung của lần thu thập được xác định theo kết quả xử lý của nền tảng duy nhất.

#### ALT-02 — Nền tảng không có thông tin báo cáo
- Tại bước 5 của luồng chính, nền tảng phản hồi thành công nhưng không có thông tin báo cáo.
- Hệ thống ghi nhận nền tảng đã được kiểm tra với số lượng dữ liệu nhận được bằng không và trạng thái “Không có dữ liệu”.
- Hệ thống không tạo báo cáo cho nền tảng đó vì không có thông tin để tập hợp.
- Hệ thống tiếp tục xử lý các nền tảng còn lại.

#### ALT-03 — Chưa có báo cáo được lưu
- Tại bước 12 của luồng chính, hệ thống không tìm thấy báo cáo nào đã được lưu.
- Hệ thống hiển thị thông báo “Chưa có báo cáo” trong chức năng Báo cáo.
- Hệ thống không tạo báo cáo trống và Quản trị viên vẫn có thể tiếp tục sử dụng các chức năng khác.

### Exception Flow

#### EXC-01 — Không thể kết nối với nền tảng
- Tại bước 3 hoặc bước 4 của luồng chính, hệ thống không thể kết nối với nền tảng hoặc thông tin kết nối đã hết hiệu lực.
- Hệ thống không lấy thông tin và không tạo báo cáo mới cho nền tảng đó.
- Hệ thống ghi nhận nền tảng ở trạng thái “Thất bại” và lưu nguyên nhân để Quản trị viên có thể kiểm tra.
- Hệ thống tiếp tục xử lý các nền tảng còn lại.

#### EXC-02 — Nền tảng không phản hồi hoặc trả về thông tin chưa đầy đủ
- Tại bước 4, bước 5 hoặc bước 6 của luồng chính, nền tảng xảy ra lỗi, không phản hồi hoặc chỉ trả về một phần thông tin.
- Hệ thống thực hiện lại yêu cầu theo số lần đã được thiết lập trước.
- Nếu vẫn không thể nhận đầy đủ thông tin, hệ thống ghi nhận nền tảng ở trạng thái “Thất bại” và lưu nguyên nhân.
- Hệ thống không sử dụng thông tin chưa đầy đủ để tạo báo cáo và tiếp tục xử lý các nền tảng còn lại.

#### EXC-03 — Không thể tạo hoặc lưu báo cáo
- Tại bước 8, bước 9 hoặc bước 10 của luồng chính, hệ thống không thể tập hợp thông tin, tạo báo cáo hoặc lưu kết quả vào cơ sở dữ liệu.
- Hệ thống không ghi nhận báo cáo đã được tạo thành công.
- Hệ thống không giữ dữ liệu hoặc báo cáo chưa hoàn chỉnh.
- Dữ liệu và báo cáo đã được lưu thành công trước đó được giữ nguyên.
- Hệ thống ghi nhận trạng thái “Thất bại”, lưu nguyên nhân và tiếp tục xử lý các nền tảng còn lại.

#### EXC-04 — Không thể mở báo cáo đã chọn
- Tại bước 13 của luồng chính, báo cáo mà Quản trị viên chọn không còn tồn tại hoặc hệ thống không thể đọc nội dung báo cáo.
- Hệ thống không hiển thị nội dung không đầy đủ hoặc nội dung của báo cáo khác.
- Hệ thống hiển thị thông báo không thể mở báo cáo và nêu rõ nguyên nhân nếu nguyên nhân đã được ghi nhận.
- Hệ thống đưa Quản trị viên trở lại danh sách báo cáo.
- Các báo cáo khác đã được lưu không bị thay đổi.
- Quản trị viên có thể chọn một báo cáo khác để xem.

#### EXC-05 — Không thể tải báo cáo
- Tại bước 14 của luồng chính, hệ thống không thể tạo hoặc cung cấp tệp báo cáo để tải xuống.
- Hệ thống không cung cấp tệp báo cáo chưa hoàn chỉnh.
- Hệ thống hiển thị thông báo tải báo cáo không thành công và nêu rõ nguyên nhân nếu nguyên nhân đã được ghi nhận.
- Báo cáo và dữ liệu đã lưu được giữ nguyên để Quản trị viên có thể thực hiện lại thao tác tải.

---

## Acceptance Criteria

- **AC-001 — Bắt đầu thu thập đúng thời điểm**:
  - **Given**: Ngày và giờ thu thập đã được cấu hình cho một nền tảng.
  - **When**: Đến ngày và giờ đã được cấu hình.
  - **Then**: Hệ thống bắt đầu một lần thu thập và ghi nhận trạng thái “Đang xử lý”.

- **AC-002 — Xác định và kiểm tra nền tảng cần thu thập**:
  - **Given**: Có một hoặc nhiều nền tảng đang được bật để thu thập thông tin báo cáo.
  - **When**: Hệ thống bắt đầu lần thu thập.
  - **Then**: Hệ thống xác định đầy đủ các nền tảng đang được bật.
  - **And**: Hệ thống kiểm tra thông tin kết nối của từng nền tảng.
  - **And**: Hệ thống chỉ gửi yêu cầu đến nền tảng có kết nối còn hiệu lực và tài khoản kết nối có quyền xem thông tin báo cáo.

- **AC-003 — Lấy đầy đủ thông tin báo cáo từ nền tảng**:
  - **Given**: Nền tảng đang được bật và thông tin kết nối còn hiệu lực.
  - **When**: Hệ thống gửi yêu cầu lấy thông tin báo cáo.
  - **Then**: Hệ thống nhận thông tin báo cáo mà nền tảng cung cấp tại thời điểm thu thập.
  - **And**: Nếu thông tin được trả về thành nhiều phần, hệ thống tiếp tục lấy cho đến khi hoàn tất.
  - **And**: Hệ thống chỉ ghi nhận nền tảng đã thu thập thành công sau khi nhận đầy đủ thông tin.

- **AC-004 — Xử lý riêng từng nền tảng**:
  - **Given**: Có nhiều nền tảng đang được bật để thu thập.
  - **When**: Hệ thống thực hiện lần thu thập.
  - **Then**: Hệ thống xử lý và ghi nhận kết quả riêng cho từng nền tảng.
  - **And**: Lỗi của một nền tảng không làm mất dữ liệu hoặc báo cáo đã tạo thành công từ nền tảng khác.

- **AC-005 — Tập hợp và lưu báo cáo thành công**:
  - **Given**: Hệ thống đã nhận đầy đủ thông tin báo cáo từ một nền tảng.
  - **When**: Hệ thống tập hợp và lưu kết quả.
  - **Then**: Hệ thống tạo báo cáo tương ứng và lưu báo cáo vào cơ sở dữ liệu của hệ thống.
  - **And**: Báo cáo được lưu kèm tên nền tảng, thời điểm thu thập, thời điểm tạo báo cáo, số lượng dữ liệu nhận được và trạng thái “Thành công”.

- **AC-006 — Nền tảng không có thông tin báo cáo**:
  - **Given**: Nền tảng phản hồi thành công nhưng không có thông tin báo cáo.
  - **When**: Hệ thống hoàn tất việc kiểm tra kết quả.
  - **Then**: Hệ thống ghi nhận số lượng dữ liệu nhận được bằng không, trạng thái “Không có dữ liệu” và không tạo báo cáo cho nền tảng đó.

- **AC-007 — Hiển thị danh sách báo cáo đúng nơi quy định**:
  - **Given**: Có ít nhất một báo cáo đã được lưu và.
  - **When**: Quản trị viên truy cập chức năng Báo cáo.
  - **Then**: Hệ thống hiển thị danh sách tất cả báo cáo đã lưu.
  - **And**: Danh sách hiển thị tên nền tảng, thời điểm tạo báo cáo và trạng thái của từng báo cáo.

- **AC-008 — Thông báo khi chưa có báo cáo**:
  - **Given**: Chưa có báo cáo nào được lưu.
  - **When**: Quản trị viên truy cập chức năng Báo cáo.
  - **Then**: Hệ thống hiển thị thông báo “Chưa có báo cáo”.
  - **And**: Hệ thống không tạo báo cáo trống.
  - **And**: Quản trị viên vẫn có thể tiếp tục sử dụng các chức năng khác.

- **AC-009 — Xem chi tiết báo cáo**:
  - **Given**: Báo cáo đã được lưu và.
  - **When**: Quản trị viên chọn một báo cáo trong danh sách.
  - **Then**: Hệ thống hiển thị đầy đủ nội dung của báo cáo đã chọn.
  - **And**: Nội dung hiển thị thuộc đúng báo cáo mà Quản trị viên đã chọn.
  - **And**: Hệ thống không thay đổi dữ liệu hoặc nội dung báo cáo trong quá trình hiển thị.
  - **And**: Quản trị viên có thể quay lại danh sách báo cáo sau khi xem.

- **AC-010 — Tải báo cáo thành công**:
  - **Given**: Báo cáo đã được lưu và.
  - **When**: Quản trị viên chọn tải báo cáo.
  - **Then**: Hệ thống tạo tệp từ đúng báo cáo đã chọn và cung cấp tệp để Quản trị viên tải về thiết bị.

- **AC-011 — Xử lý lỗi kết nối hoặc thông tin chưa đầy đủ**:
  - **Given**: Nền tảng không thể kết nối, không phản hồi hoặc chỉ trả về một phần thông tin.
  - **When**: Hệ thống không thể hoàn tất việc thu thập sau số lần thực hiện lại đã được thiết lập.
  - **Then**: Hệ thống ghi nhận trạng thái “Thất bại”, lưu nguyên nhân, không tạo báo cáo từ thông tin chưa đầy đủ và tiếp tục xử lý các nền tảng còn lại.

- **AC-012 — Không lưu báo cáo chưa hoàn chỉnh**:
  - **Given**: Hệ thống đang tập hợp, tạo hoặc lưu báo cáo.
  - **When**: Quá trình tập hợp, tạo hoặc lưu báo cáo xảy ra lỗi.
  - **Then**: Hệ thống không ghi nhận báo cáo thành công, không giữ báo cáo chưa hoàn chỉnh và giữ nguyên dữ liệu cùng báo cáo đã lưu thành công trước đó.

- **AC-013 — Xử lý lỗi khi mở hoặc tải báo cáo**:
  - **Given**: Quản trị viên đã chọn một báo cáo để xem hoặc tải.
  - **When**: Hệ thống không thể đọc nội dung, tạo tệp hoặc cung cấp tệp báo cáo.
  - **Then**: Hệ thống hiển thị thông báo phù hợp, không cung cấp nội dung hoặc tệp chưa hoàn chỉnh và giữ nguyên báo cáo đã lưu.

- **AC-014 — Lịch hằng ngày**:
  - **Given**: Admin chọn loại lịch “Hằng ngày”.
  - **When**: Admin nhập giờ hợp lệ và lưu.
  - **Then**: Hệ thống thực hiện thu thập vào giờ đó mỗi ngày.

- **AC-015 — Lịch hằng tuần**:
  - **Given**: Admin chọn loại lịch “Hằng tuần”.
  - **When**: Admin chọn thứ trong tuần, nhập giờ và lưu.
  - **Then**: Hệ thống thực hiện thu thập vào đúng thứ và giờ đã chọn mỗi tuần.

- **AC-016 — Lịch hằng tháng với ngày tồn tại**:
  - **Given**: Admin đã cấu hình lịch hằng tháng vào ngày 15 lúc 08:00.
  - **When**: Đến ngày 15 của tháng.
  - **Then**: Hệ thống bắt đầu thu thập lúc 08:00.

- **AC-017 — Lịch hằng tháng với ngày không tồn tại**:
  - **Given**: Admin đã cấu hình lịch hằng tháng vào một ngày không tồn tại trong tháng hiện tại.
  - **When**: Đến ngày cuối cùng của tháng tại giờ đã cấu hình.
  - **Then**: Hệ thống bắt đầu thu thập báo cáo.
  - **And**: Hệ thống không chờ sang tháng tiếp theo.
  - **And**: Ngày cấu hình ban đầu không bị thay đổi.

- **AC-018 — Thu thập dữ liệu Instagram Account**:
  - **Given**: Kết nối Instagram còn hiệu lực và đến thời điểm thu thập báo cáo.
  - **When**: Hệ thống thu thập dữ liệu ở cấp Account.
  - **Then**: Hệ thống lấy và lưu đầy đủ các trường `views`, `reach`, `accounts_engaged`, `total_interactions`, `likes`, `comments`, `shares`, `saves` và `follows_and_unfollows`.

- **AC-019 — Thu thập dữ liệu từng Instagram Post**:
  - **Given**: Hệ thống đã đăng ít nhất một Post lên Instagram và đã lưu mã định danh của Post trên nền tảng.
  - **When**: Hệ thống thu thập dữ liệu báo cáo theo từng Post.
  - **Then**: Hệ thống chỉ lấy dữ liệu của những Post do hệ thống đăng.
  - **And**: Mỗi Post được lấy các trường `views`, `reach`, `likes`, `comments`, `shares`, `saved` và `total_interactions`.

- **AC-020 — Thu thập dữ liệu Facebook Page**:
  - **Given**: Kết nối Facebook còn hiệu lực và đến thời điểm thu thập báo cáo.
  - **When**: Hệ thống thu thập dữ liệu ở cấp Page.
  - **Then**: Hệ thống lấy và lưu đầy đủ các trường `followers_count`, `page_media_view`, `page_total_media_view_unique`, `page_post_engagements` và `page_views_total`.

- **AC-021 — Thu thập dữ liệu từng Facebook Post**:
  - **Given**: Hệ thống đã đăng ít nhất một Post lên Facebook và đã lưu mã định danh của Post trên nền tảng.
  - **When**: Hệ thống thu thập dữ liệu báo cáo theo từng Post.
  - **Then**: Hệ thống chỉ lấy dữ liệu của những Post do hệ thống đăng.
  - **And**: Mỗi Post được lấy các trường `post_media_view`, `post_total_media_view_unique`, `post_reactions_by_type_total`, `comments`, `shares` và `post_clicks`.

- **AC-022 — Thu thập và tính biến động follower của Zalo OA**:
  - **Given**: Kết nối Zalo OA còn hiệu lực và đến thời điểm thu thập báo cáo.
  - **When**: Hệ thống thu thập dữ liệu ở cấp Account.
  - **Then**: Hệ thống lấy và lưu trường `num_follower`.
  - **And**: Hệ thống tính mức tăng hoặc giảm follower bằng chênh lệch `num_follower` giữa lần thu thập thành công hiện tại và lần thu thập thành công liền trước.

- **AC-023 — Thu thập dữ liệu từng Zalo Article**:
  - **Given**: Hệ thống đã đăng ít nhất một Article lên Zalo OA và đã lưu mã định danh của Article trên nền tảng.
  - **When**: Hệ thống thu thập dữ liệu báo cáo theo từng Article.
  - **Then**: Hệ thống chỉ lấy dữ liệu của những Article do hệ thống đăng.
  - **And**: Mỗi Article được lấy các trường `id`, `type`, `title`, `author`, `cover`, `description`, `status`, `body`, `related_medias` và `comment`.

---

## References

### TDDs
- [TDD-025: Thu thập và quản lý báo cáo từ các nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-025-CollectAndManagePlatformReports.md)

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

- Hệ thống chỉ ghi nhận một nền tảng đã thu thập thành công sau khi nhận đầy đủ thông tin mà nền tảng cung cấp.
- Lỗi của một nền tảng không được làm mất dữ liệu hoặc báo cáo đã tạo thành công từ nền tảng khác.
- Hệ thống không được lưu báo cáo hoặc tệp báo cáo chưa hoàn chỉnh.
- Hệ thống phải ghi nhận thời điểm bắt đầu, thời điểm kết thúc, trạng thái và nguyên nhân lỗi của mỗi lần thu thập, tạo báo cáo và tải báo cáo.
- Tệp tải về phải có nội dung đúng với báo cáo mà Quản trị viên đã chọn.
- Dữ liệu báo cáo theo từng Post/Article chỉ được thu thập từ những bài do hệ thống đăng và đã lưu mã định danh của bài trên nền tảng.
- Mức tăng hoặc giảm follower của Zalo OA được tính từ chênh lệch `num_follower` giữa lần thu thập thành công hiện tại và lần thu thập thành công liền trước.

---

## Out of Scope

- Không tạo hoặc thay đổi thông tin kết nối của nền tảng.
- Không tạo hoặc thay đổi ngày và giờ thu thập của nền tảng.
- Không chỉnh sửa trực tiếp dữ liệu hoặc nội dung của báo cáo đã lưu.
- Không hiển thị báo cáo hoặc số liệu báo cáo trên trang tổng quan, còn gọi là Dashboard.
- Không tạo báo cáo tổng hợp định kỳ từ dữ liệu của nhiều nền tảng; nội dung này thuộc STORY-026.
