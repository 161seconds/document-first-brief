# STORY-033: Tạo mẫu hoa bằng AI (Generate Flower Design with AI)

## Metadata
- **Story**: Là một khách hàng đã đăng nhập và có yêu cầu tạo mẫu hoa, tôi muốn sử dụng AI để tạo ra hình ảnh mẫu hoa dựa trên yêu cầu tạo mẫu hoa đã khởi tạo, giúp tôi hình dung kết quả trước khi quyết định tải xuống, thêm vào giỏ hàng hoặc đặt hàng.
- **Context**: Khách hàng có thể tạo mẫu hoa bằng AI từ màn hình Chi tiết yêu cầu tạo mẫu hoa. Nếu yêu cầu tạo mẫu hoa chưa có kết quả AI, hệ thống hiển thị nút “Tạo bó hoa AI ngay”. Nếu yêu cầu tạo mẫu hoa đã có ít nhất một kết quả AI trước đó, hệ thống hiển thị nút “Tạo lại”. Mỗi lần AI tạo thành công, hệ thống tạo ra 1 ảnh mẫu hoa và lưu kết quả thành một bản ghi mới trong lịch sử tạo AI.
  - **Dữ liệu đầu vào sử dụng để tạo mẫu hoa bằng AI gồm**:
    - Combo đã chọn.
    - Danh sách hoa / thành phần trong Combo.
    - Số lượng từng loại hoa.
    - Dịp sử dụng.
    - Phong cách.
    - Ngân sách.
    - Kích thước.
    - Ghi chú / Yêu cầu thêm.
    - Mockup.
    - Giấy gói.
    - Ruy băng.
  - Mockup được sử dụng làm dữ liệu tham chiếu để AI bám theo kiểu dáng, bố cục hoặc bối cảnh thể hiện của mẫu hoa.
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
  - Yêu cầu tạo mẫu hoa tồn tại.
  - Yêu cầu tạo mẫu hoa thuộc về khách hàng hiện tại.
  - Yêu cầu tạo mẫu hoa có đầy đủ dữ liệu cần thiết để tạo mẫu hoa bằng AI.
  - AI Job tạo mẫu hoa không ở trạng thái “Đang tạo”.
  - Khách hàng còn ít nhất 1 lượt AI trong ngày.
- **Trigger**: Khách hàng thực hiện một trong các thao tác sau:
  - Ấn chọn “Tạo bó hoa AI ngay” khi yêu cầu tạo mẫu hoa chưa có kết quả AI.
  - Ấn chọn “Tạo lại” khi yêu cầu tạo mẫu hoa đã có kết quả AI trước đó.

## Flow
### Main Flow
1. Khách hàng truy cập màn hình Chi tiết yêu cầu tạo mẫu hoa.
2. Hệ thống kiểm tra trạng thái và lịch sử tạo AI của yêu cầu tạo mẫu hoa.
3. Hệ thống hiển thị chức năng tạo AI tương ứng.
4. Khách hàng ấn chọn “Tạo bó hoa AI ngay” hoặc “Tạo lại”.
5. Hệ thống kiểm tra yêu cầu tạo mẫu hoa tồn tại và thuộc khách hàng hiện tại.
6. Hệ thống kiểm tra yêu cầu tạo mẫu hoa hiện tại không có tiến trình AI đang chạy và số lượt AI trong ngày.
7. Hệ thống thu thập dữ liệu đầu vào của yêu cầu tạo mẫu hoa.
8. Hệ thống tạo nội dung đầu vào cho AI theo các Business Rule về prompt.
9. Hệ thống tạo AI job cho lần generate hiện tại và cập nhật trạng thái AI job thành “Đang tạo”.
10. Hệ thống trừ 1 lượt AI trong ngày của khách hàng.
11. Hệ thống gửi yêu cầu tạo ảnh sang AI service.
12. AI xử lý dữ liệu và tạo 1 ảnh mẫu hoa.
13. Hệ thống nhận và kiểm tra kết quả AI.
14. Nếu ảnh output hợp lệ, hệ thống gắn logo theo quy tắc cấu hình.
15. Hệ thống lưu ảnh kết quả đã gắn logo.
16. Hệ thống tạo một bản ghi kết quả mới trong lịch sử tạo AI.
17. Hệ thống lưu snapshot dữ liệu nguồn tại thời điểm generate, gồm Combo, Core/Support và số lượng, Mockup và yêu cầu tùy chỉnh.
18. Hệ thống cập nhật AI job thành “Đã tạo”.
19. Hệ thống chuyển khách hàng đến màn hình Kết quả bó hoa AI.
20. Hệ thống hiển thị ảnh AI vừa được tạo.
21. Hệ thống hiển thị các chức năng “Tải xuống”, “Tạo lại”, “Đặt hàng ngay”, “Thêm giỏ hàng”.

### Alternative Flow
- **ALT-01 — Khách hàng rời trang trong khi AI đang tạo**:
  1. Khách hàng đã bắt đầu quá trình tạo mẫu hoa bằng AI.
  2. AI Job tạo mẫu hoa đang ở trạng thái “Đang tạo”.
  3. Khách hàng reload hoặc rời khỏi trang.
  4. Job AI vẫn tiếp tục được xử lý.
  5. Khi khách hàng quay lại, hệ thống hiển thị trạng thái hiện tại của AI Job.
  6. Nếu quá trình AI đã hoàn thành, hệ thống hiển thị kết quả đã tạo.
- **ALT-02 — Tạo lại mẫu hoa**:
  1. Yêu cầu tạo mẫu hoa đã có ít nhất một kết quả AI trước đó.
  2. Khách hàng ấn chọn “Tạo lại”.
  3. Hệ thống xử lý đây là một request generate mới.
  4. Nếu request được chấp nhận, hệ thống trừ 1 lượt AI của khách hàng.
  5. Hệ thống tiếp tục xử lý generate theo Main Flow.
  6. Nếu generate thành công, hệ thống lưu kết quả mới thành một bản ghi lịch sử riêng.
  7. Các kết quả AI trước đó vẫn được giữ lại.
  8. Nếu generate thất bại, hệ thống hoàn lại 1 lượt AI đã trừ cho request đó.

### Exception Flow
- **EXC-01 — Hết lượt AI trong ngày**: Khách hàng không còn lượt AI trong ngày. Khách hàng truy cập chức năng tạo mẫu hoa bằng AI. Hệ thống không cho phép gửi yêu cầu AI mới, disable nút tạo AI, thông báo khách hàng đã hết lượt AI trong ngày và thông báo lượt AI sẽ được cấp lại vào ngày hôm sau.
- **EXC-02 — Đã có job AI đang chạy**: AI Job tạo mẫu hoa đang ở trạng thái “Đang tạo”. Khách hàng tiếp tục ấn nút tạo AI hoặc gửi nhiều request liên tiếp. Hệ thống chỉ chấp nhận request đầu tiên, không tạo thêm job AI mới và tiếp tục hiển thị trạng thái “Đang tạo”.
- **EXC-03 — AI service gặp lỗi**: AI service gặp lỗi hoặc không thể tạo ảnh. Hệ thống tự động thử lại tối đa 2 lần và không trừ thêm lượt AI. Trong thời gian thử lại, AI job giữ trạng thái “Đang tạo”. Nếu vẫn lỗi sau các lần thử lại, hệ thống cập nhật AI job thành “Lỗi”, hoàn lại 01 lượt AI và không tạo History item.
- **EXC-04 — Yêu cầu tạo mẫu hoa không hợp lệ**: Yêu cầu không tồn tại hoặc không thuộc khách hàng hiện tại. Hệ thống từ chối thao tác, không tạo job AI, không tự động thử lại, không trừ lượt AI và không trả dữ liệu của khách hàng khác.

## Acceptance Criteria
### AC-001: Hiển thị nút tạo AI lần đầu
- **Given**: Khách hàng đang xem một yêu cầu tạo mẫu hoa chưa có kết quả AI.
- **When**: Màn hình Chi tiết yêu cầu tạo mẫu hoa được hiển thị.
- **Then**: Hệ thống phải hiển thị nút “Tạo bó hoa AI ngay”.

### AC-002: Hiển thị nút Tạo lại
- **Given**: Yêu cầu tạo mẫu hoa đã có ít nhất một kết quả AI.
- **When**: Khách hàng xem màn hình kết quả AI.
- **Then**: Hệ thống phải hiển thị nút “Tạo lại”.

### AC-003: Kiểm tra quota AI
- **Given**: Khách hàng đang ở màn hình có chức năng tạo mẫu hoa bằng AI.
- **When**: Khách hàng ấn chọn “Tạo bó hoa AI ngay” hoặc “Tạo lại”.
- **Then**: Hệ thống phải kiểm tra số lượt AI còn lại trong ngày trước khi chấp nhận request.
- **And**: Nếu khách hàng không còn lượt AI trong ngày, hệ thống không được tạo job AI mới.

### AC-004: Thu thập dữ liệu đầu vào
- **Given**: Yêu cầu tạo mẫu hoa hợp lệ và khách hàng còn ít nhất 01 lượt AI.
- **When**: Khách hàng yêu cầu tạo mẫu hoa bằng AI.
- **Then**: Hệ thống sử dụng dữ liệu của yêu cầu tạo mẫu hoa làm đầu vào gồm: Combo, danh sách hoa/thành phần và số lượng, dịp sử dụng, phong cách, ngân sách, kích thước, ghi chú/yêu cầu thêm, Mockup.

### AC-005: Áp dụng quy tắc Prompt
- **Given**: Hệ thống đã thu thập đầy đủ dữ liệu đầu vào.
- **When**: Hệ thống xây dựng nội dung đầu vào gửi sang AI.
- **Then**: Hệ thống phải áp dụng thứ tự ưu tiên: Combo > Mockup > Ghi chú/Yêu cầu thêm > Phong cách.
- **And**: Hệ thống phải truyền danh sách loại hoa và số lượng tương ứng từ Combo nguồn cho AI.
- **And**: AI phải cố gắng tạo kết quả bám sát loại hoa, số lượng, kiểu dáng, bố cục hoặc bối cảnh của dữ liệu đầu vào và Mockup đã chọn.
- **And**: Ảnh AI chỉ mang tính chất minh họa và không được sử dụng làm dữ liệu chính thức để xác định thành phần hoặc số lượng nguyên vật liệu của Combo.

### AC-006: Chuyển trạng thái khi bắt đầu tạo
- **Given**: Yêu cầu tạo mẫu hoa đủ điều kiện sử dụng AI và khách hàng còn ít nhất 1 lượt AI.
- **When**: Request tạo mẫu hoa bằng AI được hệ thống chấp nhận.
- **Then**: Hệ thống phải tạo AI job cho lần generate và cập nhật trạng thái AI job thành “Đang tạo”.

### AC-007: Trừ lượt khi request được chấp nhận
- **Given**: Khách hàng còn ít nhất 1 lượt AI và yêu cầu tạo mẫu hoa đủ điều kiện generate.
- **When**: Request tạo mẫu hoa bằng AI được hệ thống chấp nhận.
- **Then**: Hệ thống phải trừ đúng 1 lượt AI của khách hàng.
- **And**: Số lượt AI còn lại phải được cập nhật tương ứng.

### AC-008: Không tạo duplicate job
- **Given**: Yêu cầu tạo mẫu hoa đã có một AI Job đang ở trạng thái “Đang tạo”.
- **When**: Hệ thống nhận thêm một hoặc nhiều request generate cho cùng yêu cầu, kể cả request đồng thời từ nhiều tab hoặc client.
- **Then**: Hệ thống không được tạo AI Job mới.
- **And**: Tại mọi thời điểm, một yêu cầu tạo mẫu hoa chỉ được có tối đa 01 AI Job đang xử lý.
- **And**: Các request bị từ chối không được trừ thêm lượt AI.

### AC-009: Tạo mẫu hoa AI thành công
- **Given**: Request AI đã được chấp nhận và đã trừ 01 lượt AI.
- **When**: AI trả về ảnh output hợp lệ, hệ thống lưu ảnh thành công và tạo bản ghi lịch sử thành công.
- **Then**: Hệ thống lưu đúng 01 ảnh mẫu hoa cho lần generate đó.
- **And**: Hệ thống tạo đúng 01 History item cho lần generate.
- **And**: AI job được cập nhật thành “Đã tạo”.
- **And**: Hệ thống không hoàn lại lượt AI đã trừ.

### AC-010: Hoàn lượt khi tạo thất bại
- **Given**: Request AI đã được chấp nhận và đã trừ 1 lượt AI.
- **When**: Job vẫn thất bại sau khi hoàn tất các lần tự động thử lại.
- **Then**: Hệ thống phải hoàn lại đúng 1 lượt AI.
- **And**: Không tạo History item.
- **And**: Mỗi job chỉ được hoàn lượt tối đa một lần.

### AC-011: Lưu lịch sử tạo
- **Given**: AI đã tạo và lưu ảnh thành công.
- **When**: Quá trình tạo kết thúc thành công.
- **Then**: Hệ thống phải lưu kết quả AI thành một bản ghi lịch sử mới.
- **And**: Các kết quả AI trước đó không được bị ghi đè hoặc xóa.

### AC-012: Giữ job khi khách hàng rời trang
- **Given**: AI job của lần generate hiện tại đang ở trạng thái “Đang tạo”.
- **When**: Khách hàng reload hoặc rời khỏi trang.
- **Then**: Job AI vẫn phải tiếp tục xử lý.
- **And**: Khi khách hàng quay lại, hệ thống phải hiển thị đúng trạng thái hiện tại hoặc kết quả cuối cùng.

### AC-013: Tự động thử lại khi gặp lỗi kỹ thuật
- **Given**: Request AI đã được chấp nhận và đã trừ 1 lượt AI.
- **When**: AI service lỗi, không trả ảnh hợp lệ hoặc hệ thống không lưu được kết quả.
- **Then**: Hệ thống tự động thử lại tối đa 2 lần trong cùng job.
- **And**: Không trừ thêm lượt AI và AI Job tiếp tục giữ trạng thái “Đang tạo”.
- **And**: Chỉ khi vẫn thất bại sau các lần thử lại, hệ thống mới chuyển trạng thái AI Job thành “Lỗi”, hoàn lại 1 lượt AI và không tạo History item.

### AC-014: Tạo lại
- **Given**: Yêu cầu tạo mẫu hoa đã có ít nhất một kết quả AI trước đó và khách hàng còn ít nhất 1 lượt AI.
- **When**: Khách hàng ấn chọn “Tạo lại”.
- **Then**: Hệ thống phải xử lý đây là một lần generate mới.
- **And**: Nếu generate thành công, hệ thống phải tạo một kết quả AI mới và giữ lại các kết quả cũ trong lịch sử.
- **And**: Nếu lần generate mới thất bại sau các lần retry, hệ thống hoàn lại 01 lượt AI đã trừ và không tạo History item.

### AC-015: Hiển thị kết quả AI
- **Given**: AI đã tạo và lưu ảnh thành công.
- **When**: Khách hàng được chuyển đến màn hình Kết quả bó hoa AI.
- **Then**: Hệ thống phải hiển thị ảnh AI vừa được tạo.
- **And**: Hệ thống phải hiển thị các chức năng “Tải xuống”, “Tạo lại”, “Đặt hàng ngay”, “Thêm giỏ hàng”.

### AC-016: Hết lượt AI
- **Given**: Khách hàng không còn lượt AI trong ngày.
- **When**: Hệ thống hiển thị chức năng tạo mẫu hoa bằng AI.
- **Then**: Nút “Tạo bó hoa AI ngay” hoặc “Tạo lại” phải ở trạng thái disabled.
- **And**: Hệ thống phải thông báo khách hàng đã hết lượt AI trong ngày.

### AC-017: Hiển thị cảnh báo về ảnh AI
- **Given**: Khách hàng đang xem màn hình Kết quả bó hoa AI.
- **When**: Hệ thống hiển thị ảnh mẫu hoa do AI tạo.
- **Then**: Hệ thống phải hiển thị thông báo cho biết ảnh AI chỉ mang tính chất minh họa và có thể có sai khác so với Combo thực tế.
- **And**: Nếu UI sử dụng thông tin “độ tương đồng khoảng 80%”, hệ thống phải thể hiện đây là mức ước lượng/tham khảo, không phải cam kết độ chính xác tuyệt đối.

## References
- **Rules**:
  - [BR-020](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/19df37d9-dd95-4cfd-bf58-cedda0daceb5)
  - [BR-021](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/904674d6-8dcd-4f8e-bdd7-4385ce7a9aba)
  - [BR-022](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2fb20e00-ff18-4cb9-815e-8d18ef4d2ac7)
  - [BR-023](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e59ab341-da80-43fa-9127-9d27e91a61fc)
  - [BR-024](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c4b6e286-44b5-4c50-9c56-5909816d54d3)
  - [BR-025](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/fdde7f40-004d-484b-925f-81aae8917bbb)
  - [BR-026](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/4f4f6bad-810a-449d-88e6-d6626ae04ec6)
  - [BR-028](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2d405952-3d9d-4c82-8640-a348c9ef8cd8)
  - [BR-029](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/8dca1a5b-f00f-4840-be33-b14658d53526)
  - [BR-030](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/77ac0f5a-7ccc-4a7a-b171-296afa1599df)
  - [BR-031](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a3b98880-74cd-4516-a30c-79e8b8dc724f)
  - [BR-032](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/7816fafe-0980-408b-9227-b17595c35dbf)
  - [BR-027](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/efbc6956-6fcd-44f7-9bbe-417b0c3ffe0b)
  - [BR-155](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b5a30cd0-97a5-433b-ac63-0661748348a7)
- **Dependencies**:
  - [STORY-030](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/225510e4-396c-403b-9ae4-59f00859a2ce) / [30-InitializeFlowerDesignRequest.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/30-InitializeFlowerDesignRequest.md)

## Non-Functional
- Sau khi khách hàng ấn “Tạo bó hoa AI ngay” hoặc “Tạo lại”, hệ thống phải phản hồi việc request đã được chấp nhận hoặc bị từ chối trong p95 ≤ 1.5 giây, không bao gồm thời gian AI generate ảnh.
- Sau khi request AI được chấp nhận, giao diện phải chuyển sang trạng thái “Đang tạo” trong vòng ≤ 500 ms kể từ khi frontend nhận response xác nhận từ backend.
- Việc kiểm tra quota AI phải hoàn tất trong p95 ≤ 500 ms.
- Việc cập nhật số lượt AI sau khi trừ hoặc hoàn lượt phải được phản ánh cho khách hàng trong vòng ≤ 2 giây kể từ khi hệ thống xác nhận thao tác tương ứng.
- Hệ thống phải đảm bảo 100% request trùng lặp trong cùng một job đang chạy không tạo thêm AI job hoặc trừ thêm quota.
- Mỗi yêu cầu tạo mẫu hoa chỉ được có tối đa 1 job AI ở trạng thái đang xử lý tại cùng một thời điểm.
- Khi khách hàng reload hoặc rời khỏi trang, job AI đã được backend chấp nhận phải tiếp tục xử lý và không được bị hủy do mất kết nối phía client.
- Khi khách hàng quay lại màn hình trong lúc job đang chạy, trạng thái hiện tại phải được tải và hiển thị trong p95 ≤ 2 giây.
- Sau khi AI trả ảnh thành công, việc lưu ảnh và cập nhật lịch sử phải hoàn tất trong p95 ≤ 3 giây, không tính thời gian AI generate.
- Một kết quả AI chỉ được đánh dấu “Đã tạo” sau khi ảnh và bản ghi lịch sử đã được lưu thành công.
- Sau khi tất cả lần tự động thử lại đều thất bại, việc cập nhật trạng thái sang “Lỗi” và hoàn lại quota phải hoàn tất trong p95 ≤ 1.5 giây kể từ khi backend xác định job thất bại cuối cùng.
- Hệ thống phải đảm bảo mỗi AI job chỉ được trừ tối đa 1 lượt và hoàn tối đa 1 lượt, kể cả khi có retry, callback lặp hoặc request trùng.
- Lịch sử các lần tạo AI thành công phải được lưu bền vững; một lần “Tạo lại” không được ghi đè kết quả trước đó.
- Response lỗi gửi về client không được chứa stack trace, SQL error, connection string, API key, prompt nội bộ hoặc thông tin kỹ thuật nhạy cảm.
- Các API liên quan đến generate AI, quota và lịch sử phải yêu cầu xác thực; người dùng chỉ được truy cập dữ liệu AI thuộc tài khoản của mình.

## Out of Scope
- Xóa lịch sử kết quả AI.
- Mua thêm lượt AI.
- Nâng cấp gói AI.
- Quản trị model AI.
- Cho khách hàng chỉnh sửa prompt trực tiếp.
- Quy trình tải xuống ảnh.
- Quy trình thêm giỏ hàng.
- Quy trình đặt hàng.
