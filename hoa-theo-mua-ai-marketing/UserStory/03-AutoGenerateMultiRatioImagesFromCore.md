# STORY-003: Tự động sinh ảnh đa tỷ lệ từ ảnh core

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn tải lên một ảnh core và yêu cầu hệ thống tự động tạo các phiên bản ảnh theo tỷ lệ phù hợp với từng nền tảng để có thể sử dụng cùng một thiết kế trên Facebook, Instagram và Zalo OA mà không cần chỉnh từng ảnh thủ công.
- **Context**: Admin cần cung cấp ảnh core làm nguồn. Hệ thống sử dụng AI để điều chỉnh bố cục và mở rộng/cắt ảnh nhằm tạo các phiên bản tỷ lệ khác nhau, đồng thời phải giữ các yếu tố quan trọng như sản phẩm, logo và nội dung chính được định nghĩa thông qua khung input ghi chú/context thêm. Admin có thể xem kết quả, tạo lại nếu chưa phù hợp và lưu các phiên bản mong muốn.
  - **Định dạng hỗ trợ**: JPG, JPEG, PNG (BR-032)
  - **Dung lượng**: Tối đa 10 MB (BR-033)
  - **Tỷ lệ hỗ trợ**: (BR-034)
    - `1:1` — 1080×1080 (Square - Instagram/Facebook Feed)
    - `4:5` — 1080×1350 (Portrait - Instagram Post)
    - `9:16` — 1080×1920 (Story / Reel / TikTok)
    - `16:9` — 1920×1080 (Landscape - Facebook Banner / Youtube)
    - `2:1` — 1200×600 (Zalo OA / Web Banner)
  - **Cơ chế Phiên sinh ảnh (Generation Session)**:
    - Mỗi lần Quản trị viên yêu cầu tạo ảnh được ghi nhận là một phiên sinh ảnh.
    - Một phiên có thể chứa nhiều kết quả ảnh do AI tạo, bao gồm kết quả ban đầu và các kết quả từ thao tác "Tạo lại".
    - Trong thời gian làm việc tại màn hình tạo ảnh, hệ thống hiển thị toàn bộ kết quả thuộc phiên để Quản trị viên xem và lựa chọn.
    - Chỉ ảnh được Quản trị viên chọn "Lưu ảnh" mới xuất hiện trong danh sách ảnh đã lưu.
    - Khi Quản trị viên rời màn hình, các ảnh chưa được chọn không còn hiển thị trên giao diện nhưng vẫn được lưu trong cơ sở dữ liệu và liên kết với phiên sinh ảnh tương ứng để phục vụ lịch sử.
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
2. Hệ thống hiển thị khu vực tải ảnh nguồn, danh sách tỉ lệ được hỗ trợ và khung input nhập ghi chú context cho ảnh.
3. Quản trị viên tải lên một ảnh nguồn (ảnh core).
4. Hệ thống kiểm tra định dạng, dung lượng, chiều rộng và tỉ lệ của ảnh (BR-032, BR-033).
5. Quản trị viên chọn một tỉ lệ muốn tạo (BR-034, BR-050).
6. Quản trị viên nhập ghi chú/context cho ảnh nhằm chỉ định các đối tượng cần bảo tồn (logo, sản phẩm, chữ) (trường này không bắt buộc).
7. Quản trị viên chọn "Tạo ảnh".
8. Hệ thống kiểm tra lại tính hợp lệ của ảnh nguồn và tỉ lệ.
9. Hệ thống khởi tạo một **Phiên sinh ảnh (Generation Session)** cho yêu cầu hiện tại, chuyển sang trạng thái "Đang tạo ảnh" và chặn gửi lặp yêu cầu trong thời gian xử lý.
10. Hệ thống tạo ảnh theo tỉ lệ đã chọn thông qua dịch vụ AI (đảm bảo giữ vùng an toàn theo BR-035).
11. Hệ thống lưu ảnh kết quả vào phiên sinh ảnh trong cơ sở dữ liệu.
12. Hệ thống hiển thị toàn bộ ảnh thuộc phiên hiện tại trên khu vực kết quả để Quản trị viên xem và lựa chọn.
13. Quản trị viên xem và chọn một ảnh ưng ý để sử dụng.
14. Quản trị viên bấm nút "Lưu ảnh".
15. Hệ thống hiển thị modal nhập thông tin lưu: Tên ảnh (bắt buộc, 1-200 ký tự theo BR-036) và Mô tả ảnh (tùy chọn, $\le$ 1000 ký tự theo BR-037).
16. Quản trị viên nhập thông tin hợp lệ và xác nhận lưu.
17. Hệ thống lưu ảnh vào danh sách ảnh chính thức kèm đầy đủ metadata: tên, mô tả, tỉ lệ, kích thước, người tạo, thời gian tạo và liên kết phiên (BR-051).
18. Hệ thống đánh dấu ảnh được chọn là "Đã sử dụng", các ảnh chưa chọn vẫn được lưu trong lịch sử phiên sinh ảnh.
19. Hệ thống hiển thị thông báo: *"Lưu ảnh thành công"*.

### Alternative Flow
- **ALT-01 — Tạo lại ảnh mới trong cùng phiên**:
  - Tại màn hình kết quả, nếu chưa ưng ý hoặc muốn có thêm phương án, Quản trị viên chọn "Tạo lại".
  - Hệ thống kiểm tra lại ảnh nguồn, tỉ lệ và context đã chọn, sau đó chuyển sang trạng thái "Đang tạo ảnh".
  - Hệ thống gọi AI tạo thêm ảnh mới, lưu vào cùng phiên sinh ảnh và hiển thị đồng thời với các ảnh đã tạo trước đó.
  - Ảnh cũ không bị ghi đè hoặc xóa khỏi lịch sử phiên. Quản trị viên có thể xem và lựa chọn bất kỳ ảnh nào trong phiên để lưu.
- **ALT-02 — Thay đổi tỉ lệ trước khi tạo**:
  - Tại bước 5 của Luồng chính, Quản trị viên thay đổi tỉ lệ đã chọn.
  - Hệ thống hủy chọn tỉ lệ trước đó và chỉ ghi nhận tỉ lệ được chọn cuối cùng.
  - Quản trị viên tiếp tục từ bước 6 của Luồng chính.
- **ALT-03 — Thay đổi ảnh nguồn**:
  - Trước khi chọn "Tạo ảnh", Quản trị viên tải lên ảnh nguồn khác.
  - Hệ thống thay thế ảnh nguồn đang chọn và kiểm tra lại tính hợp lệ của ảnh nguồn mới.
  - Quản trị viên tiếp tục từ bước 5 của Luồng chính.
- **ALT-04 — Thoát khi có ảnh chưa sử dụng**:
  - Quản trị viên yêu cầu rời màn hình tạo ảnh khi trong phiên vẫn còn ảnh chưa được lưu vào danh sách chính thức.
  - Hệ thống hiển thị modal yêu cầu xác nhận rời màn hình.
  - Khi Quản trị viên xác nhận rời màn hình, các ảnh chưa được chọn sẽ không còn hiển thị trên giao diện và không được thêm vào danh sách ảnh đã lưu.
  - Hệ thống vẫn lưu trữ toàn bộ ảnh trong cơ sở dữ liệu và liên kết với phiên sinh ảnh tương ứng để phục vụ tra cứu lịch sử.
- **ALT-05 — Tiếp tục lưu ảnh khác trong cùng phiên**:
  - Quản trị viên đã chọn "Lưu ảnh" và lưu thành công một ảnh từ phiên sinh ảnh.
  - Hệ thống đánh dấu ảnh đó là "Đã lưu/Đã sử dụng" và tiếp tục duy trì hiển thị các ảnh còn lại trong phiên.
  - Quản trị viên có thể tiếp tục chọn "Lưu ảnh" tại một ảnh khác trong phiên; hệ thống lưu thành một bản ghi ảnh riêng biệt trong danh sách ảnh đã lưu.
  - Ảnh đã lưu trước đó không bị ghi đè; mỗi ảnh chỉ được lưu một lần và có thể tiếp tục lưu các ảnh khác cho đến khi rời màn hình.

### Exception Flow
- **EXC-01 — Ảnh nguồn không hợp lệ**:
  - Tại bước 4 của Luồng chính, ảnh nguồn không đáp ứng điều kiện về định dạng, dung lượng (> 10MB), chiều rộng hoặc tỉ lệ.
  - Hệ thống chặn tiếp tục, hiển thị rõ điều kiện không hợp lệ để Quản trị viên tải lên ảnh nguồn khác.
- **EXC-02 — Chưa chọn tỉ lệ**:
  - Tại bước 7 của Luồng chính, nếu Quản trị viên chưa chọn tỉ lệ và bấm "Tạo ảnh", hệ thống không tạo ảnh và yêu cầu chọn một tỉ lệ được hỗ trợ.
- **EXC-03 — Tạo ảnh thất bại**:
  - Tại bước 10 của Luồng chính, nếu hệ thống hoặc AI Worker gặp sự cố không tạo được ảnh, hệ thống kết thúc trạng thái "Đang tạo ảnh", không hiển thị ảnh lỗi và thông báo: *"Không thể tạo ảnh. Vui lòng thử lại."*
- **EXC-04 — Lưu ảnh thất bại**:
  - Tại bước 17 của Luồng chính, nếu không thể lưu đầy đủ ảnh hoặc thông tin liên quan, hệ thống rollback (không lưu dữ liệu dở dang), không báo thành công mà thông báo: *"Không thể lưu ảnh. Vui lòng thử lại."*

---

## Acceptance Criteria

- **AC-001 — Mở màn hình chức năng**:
  - **Given**: Quản trị viên đã đăng nhập vào hệ thống quản trị.
  - **When**: Quản trị viên mở chức năng "Tạo ảnh đa tỷ lệ".
  - **Then**: Hệ thống hiển thị khu vực tải một ảnh nguồn, danh sách tỉ lệ được hỗ trợ và khung input thông tin thêm/ghi chú cho ảnh được tạo.

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
  - **And**: Hệ thống hiển thị rõ điều kiện không hợp lệ để Quản trị viên tải lại.

- **AC-005 — Thay đổi ảnh nguồn trước khi tạo**:
  - **Given**: Quản trị viên đã tải lên một ảnh nguồn nhưng chưa chọn "Tạo ảnh".
  - **When**: Quản trị viên tải lên ảnh nguồn khác.
  - **Then**: Hệ thống thay thế ảnh nguồn đang chọn bằng ảnh mới.
  - **And**: Hệ thống kiểm tra lại tính hợp lệ của ảnh nguồn mới.

- **AC-006 — Chọn một tỉ lệ duy nhất**:
  - **Given**: Ảnh nguồn đã hợp lệ.
  - **When**: Quản trị viên lựa chọn tỉ lệ.
  - **Then**: Hệ thống chỉ cho phép chọn duy nhất một tỉ lệ tại một thời điểm.

- **AC-007 — Bỏ qua tỉ lệ cũ khi chọn lại (ALT-02)**:
  - **Given**: Quản trị viên đã chọn một tỉ lệ trước đó.
  - **When**: Quản trị viên nhấn chọn một tỉ lệ khác.
  - **Then**: Hệ thống hủy chọn tỉ lệ trước đó và chỉ ghi nhận tỉ lệ được chọn cuối cùng.

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
  - **Then**: Hệ thống tạo đúng ảnh theo tỉ lệ đã chọn và gắn vào phiên sinh ảnh.
  - **And**: Hệ thống hiển thị ảnh kết quả trên giao diện để Quản trị viên lựa chọn.

- **AC-013 — Tạo lại ảnh mới trong cùng phiên (ALT-01)**:
  - **Given**: Quản trị viên chưa hài lòng với ảnh kết quả và chọn "Tạo lại".
  - **When**: Quá trình tạo lại hoàn tất.
  - **Then**: Hệ thống tạo ảnh mới theo cùng tỉ lệ và lưu vào cùng phiên sinh ảnh hiện tại.
  - **And**: Hệ thống hiển thị đồng thời cả ảnh mới và các ảnh đã tạo trước đó trong phiên để Quản trị viên so sánh.
  - **And**: Không ghi đè hoặc xóa ảnh cũ khỏi lịch sử phiên.

- **AC-014 — Xử lý lỗi khi tạo ảnh thất bại**:
  - **Given**: Hệ thống hoặc AI Worker không thể tạo ảnh.
  - **When**: Quá trình tạo ảnh kết thúc.
  - **Then**: Hệ thống kết thúc trạng thái "Đang tạo ảnh".
  - **And**: Hệ thống không hiển thị ảnh kết quả lỗi.
  - **And**: Hệ thống thông báo: *"Không thể tạo ảnh. Vui lòng thử lại."*

- **AC-015 — Mở modal nhập thông tin lưu ảnh**:
  - **Given**: Hệ thống đang hiển thị các ảnh kết quả thuộc phiên sinh ảnh.
  - **When**: Quản trị viên chọn một ảnh và bấm nút "Lưu ảnh".
  - **Then**: Hệ thống hiển thị modal nhập tên và mô tả cho ảnh.
  - **And**: Tên ảnh là trường bắt buộc (từ 1 đến 200 ký tự).
  - **And**: Mô tả ảnh là trường tùy chọn (tối đa 1000 ký tự).

- **AC-016 — Lưu ảnh và dữ liệu hoàn chỉnh**:
  - **Given**: Quản trị viên nhập thông tin hợp lệ trên modal và bấm xác nhận lưu.
  - **When**: Quá trình lưu được thực hiện.
  - **Then**: Hệ thống lưu ảnh kết quả, tỉ lệ, kích thước, người tạo và thời gian tạo vào danh sách ảnh đã lưu trong cơ sở dữ liệu.

- **AC-017 — Thông báo lưu thành công**:
  - **Given**: Ảnh kết quả và toàn bộ thông tin liên quan đã được lưu thành công.
  - **When**: Quá trình lưu hoàn tất.
  - **Then**: Hệ thống chỉ hiển thị thông báo: *"Lưu ảnh thành công"*.

- **AC-018 — Xử lý khi lưu thất bại**:
  - **Given**: Có lỗi xảy ra trong quá trình lưu ảnh hoặc metadata.
  - **When**: Quá trình lưu không hoàn tất.
  - **Then**: Hệ thống không ghi nhận dữ liệu lưu dở dang (rollback).
  - **And**: Hệ thống không thông báo lưu thành công và thông báo: *"Không thể lưu ảnh. Vui lòng thử lại."*

- **AC-019 — Hiển thị toàn bộ ảnh của phiên sinh ảnh**:
  - **Given**: Quản trị viên đang thực hiện một phiên tạo ảnh.
  - **When**: AI trả về một hoặc nhiều ảnh, bao gồm ảnh từ các lần tạo lại.
  - **Then**: Hệ thống lưu tất cả ảnh vào cùng phiên sinh ảnh.
  - **And**: Hệ thống hiển thị toàn bộ ảnh thuộc phiên hiện tại để Quản trị viên lựa chọn.
  - **And**: Ảnh mới không ghi đè hoặc xóa ảnh đã tạo trước đó.

- **AC-020 — Chỉ sử dụng và lưu ảnh được chọn**:
  - **Given**: Phiên sinh ảnh có nhiều ảnh hợp lệ.
  - **When**: Quản trị viên chọn "Lưu ảnh" tại một ảnh.
  - **Then**: Chỉ ảnh được chọn được thêm vào danh sách ảnh đã lưu.
  - **And**: Ảnh được chọn được đánh dấu là đã sử dụng; các ảnh còn lại không tự động được thêm vào danh sách ảnh đã lưu.
  - **And**: Toàn bộ ảnh vẫn được giữ trong lịch sử phiên sinh ảnh trong cơ sở dữ liệu.

- **AC-021 — Rời màn hình nhưng vẫn giữ lịch sử (ALT-04)**:
  - **Given**: Phiên sinh ảnh có các ảnh chưa được chọn lưu.
  - **When**: Quản trị viên rời màn hình tạo ảnh.
  - **Then**: Các ảnh chưa được chọn không còn hiển thị trên giao diện.
  - **And**: Các ảnh chưa được chọn không được thêm vào danh sách ảnh đã lưu.
  - **And**: Hệ thống vẫn lưu toàn bộ ảnh trong cơ sở dữ liệu và liên kết đúng với phiên sinh ảnh tương ứng để phục vụ lịch sử.

- **AC-022 — Tiếp tục lưu sau lần lưu đầu tiên (ALT-05)**:
  - **Given**: Phiên sinh ảnh có nhiều ảnh hợp lệ và Quản trị viên đã lưu thành công một ảnh.
  - **When**: Quản trị viên chọn "Lưu ảnh" tại một ảnh khác trong cùng phiên.
  - **Then**: Hệ thống lưu ảnh đó thành một bản ghi ảnh riêng biệt trong danh sách ảnh đã lưu.
  - **And**: Ảnh đã lưu trước đó không bị ghi đè hoặc thay đổi; ảnh vừa lưu được đánh dấu là đã lưu; các ảnh còn lại vẫn hiển thị để tiếp tục lựa chọn; mỗi ảnh chỉ được lưu một lần trong cùng phiên.

---

## References

### Business Rules
- [BR-032: Định dạng file ảnh gốc (Core Image)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-032.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/422cb56a-ad99-48c9-8598-7d518003905c))
- [BR-033: Dung lượng ảnh gốc tối đa](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-033.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a35a33f1-0c45-4939-97fd-6c9b231db023))
- [BR-034: Hỗ trợ các tỷ lệ sinh ảnh AI](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-034.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/975e2c2e-6677-4299-a158-632a1a8a7317))
- [BR-035: Ràng buộc giữ vùng an toàn (Safe Zone)](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-035.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/9e4b0677-442e-4a96-9a9e-ff0f17eabf9f))
- [BR-036: Độ dài tên hình ảnh lưu trữ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-036.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/bfeef817-f4cd-4491-b81b-8fde08a6a752))
- [BR-037: Độ dài mô tả hình ảnh](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-037.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/861df50a-8c52-4214-bd53-aaa6e06fd86a))
- [BR-050: Một yêu cầu sinh ảnh chỉ có một ảnh nguồn và một tỷ lệ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-050.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/a3b24a75-f5c8-430c-8b8a-70d840b1ee7a))
- [BR-051: Lưu toàn vẹn ảnh kết quả và thông tin liên quan](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-051.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/39702271-8c70-45ec-befc-8dd7e5b20aa0))

---

## Non-Functional
- Phải hiển thị trạng thái "Đang tạo ảnh" trong suốt thời gian AI xử lý.
- Không được ghi đè lên ảnh core gốc.
- Mỗi ảnh kết quả phải liên kết đúng với tỷ lệ tương ứng.
- Không cho phép gửi lặp nhiều yêu cầu trong khi cùng một yêu cầu đang được xử lý.

---

## Out of Scope
- Không có
