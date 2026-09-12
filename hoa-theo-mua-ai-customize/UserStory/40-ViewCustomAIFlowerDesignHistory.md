# STORY-040: Xem lịch sử các mẫu hoa Custom AI đã tạo (View History of AI-Generated Flower Designs)

## Metadata
- **Story**: Là một khách hàng đã sử dụng chức năng Custom AI, tôi muốn xem lại các yêu cầu tạo mẫu hoa và các kết quả AI thuộc từng yêu cầu, để tiếp tục sử dụng yêu cầu và xem lại những mẫu hoa đã tạo.
- **Context**: Chức năng được truy cập từ mục “Yêu cầu tạo mẫu hoa” trên sidebar; không có trang History tách riêng.
  - Mỗi yêu cầu tạo mẫu hoa là một nhóm; yêu cầu vẫn xuất hiện dù chưa có kết quả generate thành công.
  - Một yêu cầu có thể có 0, 1 hoặc nhiều kết quả AI có ảnh.
  - Mỗi lần generate có ảnh hợp lệ tạo một item lịch sử; generate không có ảnh không tạo item.
  - Danh sách yêu cầu và danh sách kết quả trong yêu cầu đều sắp xếp theo hoạt động/generate gần nhất, mới nhất trước.
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
- **Trigger**: Khách hàng mở “Yêu cầu tạo mẫu hoa” từ sidebar.

## Flow
### Main Flow
1. Backend lấy tất cả yêu cầu tạo mẫu hoa thuộc khách hàng hiện tại.
2. Hệ thống sắp xếp yêu cầu theo thời điểm generate gần nhất, mới nhất trước. Nếu yêu cầu chưa từng generate, hệ thống sử dụng thời điểm tạo yêu cầu để sắp xếp.
3. Mỗi card hiển thị thông tin yêu cầu, tổng số kết quả có ảnh hợp lệ và trạng thái của AI Job mới nhất nếu có. Trạng thái AI Job gồm: “Đang tạo”, “Đã tạo”, “Lỗi”. Nếu yêu cầu chưa từng có AI Job, hệ thống hiển thị “Chưa tạo”. Job đang chạy chưa được tính vào tổng số mẫu và chưa tạo History item.
4. Khách hàng chọn một yêu cầu.
5. Hệ thống hiển thị danh sách các kết quả có ảnh thuộc đúng yêu cầu, mới nhất trước.
6. Khách hàng chọn một kết quả để xem chi tiết.

### Alternative Flow
- **ALT-01 — Yêu cầu chưa có kết quả**:
  1. Hệ thống vẫn hiển thị yêu cầu trong danh sách.
  2. Nếu yêu cầu không có job AI đang chạy, hệ thống hiển thị “Chưa có mẫu AI được tạo”.
  3. Nếu yêu cầu có job AI đang chạy, hệ thống hiển thị “Đang tạo”.
  4. Tổng số mẫu bằng 0 cho đến khi có ảnh được tạo và lưu thành công.
  5. Hệ thống chỉ hiển thị chức năng tạo mẫu AI khi yêu cầu thỏa điều kiện tạo AI theo STORY-033 và hiện không có AI Job ở trạng thái “Đang tạo”.
- **ALT-02 — Yêu cầu có nhiều kết quả**:
  1. Yêu cầu có nhiều kết quả AI có ảnh.
  2. Hệ thống hiển thị mọi kết quả có ảnh.
  3. Hệ thống không ghi đè kết quả cũ.
  4. Hệ thống sắp xếp kết quả mới nhất trước.
- **ALT-03 — Yêu cầu có job AI đang chạy ngầm**:
  1. Job AI của yêu cầu đang được xử lý ngầm.
  2. Khách hàng reload hoặc mở lại màn hình “Yêu cầu tạo mẫu hoa”.
  3. Hệ thống hiển thị card yêu cầu với trạng thái “Đang tạo”.
  4. Tổng số mẫu chỉ tính các kết quả đã có ảnh hợp lệ; job đang chạy chưa được hiển thị như một History item.
  5. Các kết quả thành công trước đó (nếu có) vẫn được hiển thị bình thường.

### Exception Flow
- **EXC-01 — Không có yêu cầu**: Khách hàng mở “Yêu cầu tạo mẫu hoa”. Backend kiểm tra và không tìm thấy yêu cầu nào thuộc khách hàng hiện tại. Hệ thống hiển thị empty state.
- **EXC-02 — Không tải được dữ liệu**: Hệ thống không tải được danh sách yêu cầu hoặc danh sách kết quả. Hệ thống hiển thị error state kèm nút cho phép khách hàng tải lại.
- **EXC-03 — Không có quyền**: Khách hàng truy cập yêu cầu hoặc kết quả bằng ID/URL không thuộc quyền sở hữu của mình. Backend từ chối truy cập, không trả ảnh hoặc metadata nhạy cảm.
- **EXC-04 — File ảnh không còn khả dụng**: Khách hàng xem một item lịch sử từng có ảnh nhưng file ảnh không còn khả dụng trên storage. Hệ thống giữ History record và metadata, hiển thị “Ảnh không còn khả dụng” và không làm lỗi toàn bộ màn hình.

## Acceptance Criteria
### AC-001: Hiển thị mọi yêu cầu
- **Given**: Khách hàng có yêu cầu tạo mẫu hoa thuộc tài khoản mình.
- **When**: Mở “Yêu cầu tạo mẫu hoa”.
- **Then**: Hệ thống hiển thị yêu cầu dù có hoặc chưa có kết quả AI thành công.

### AC-002: Yêu cầu chưa có ảnh
- **Given**: Yêu cầu chưa có kết quả generate có ảnh.
- **When**: Mở chi tiết yêu cầu.
- **Then**: Hệ thống hiển thị trạng thái chưa có mẫu.
- **And**: Tổng số mẫu là 0.

### AC-003: Lưu item có ảnh
- **Given**: Một lần generate tạo được ảnh hợp lệ.
- **When**: Quá trình hoàn tất.
- **Then**: Hệ thống tạo đúng 01 item lịch sử thuộc đúng yêu cầu.

### AC-004: Không có ảnh
- **Given**: Một lần generate không tạo được ảnh output.
- **When**: Quá trình kết thúc.
- **Then**: Không tạo item lịch sử.
- **And**: Không tính vào tổng số mẫu.

### AC-005: Sắp xếp yêu cầu
- **Given**: Khách hàng có nhiều yêu cầu.
- **When**: Danh sách hiển thị.
- **Then**: Yêu cầu có hoạt động/generate gần nhất được xếp trước.

### AC-006: Sắp xếp kết quả
- **Given**: Một yêu cầu có nhiều kết quả.
- **When**: Xem chi tiết.
- **Then**: Kết quả generate gần nhất được xếp trước.

### AC-007: File ảnh mất
- **Given**: Item từng có ảnh nhưng file hiện không truy cập được.
- **When**: Khách hàng xem.
- **Then**: Record vẫn tồn tại.
- **And**: Hiển thị “Ảnh không còn khả dụng.”
- **And**: Lỗi của file đó không làm lỗi toàn bộ danh sách hoặc màn hình.

### AC-008: Kiểm tra Ownership
- **Given**: Yêu cầu/kết quả thuộc khách hàng khác.
- **When**: User truy cập trực tiếp bằng ID/URL.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh hoặc metadata.

### AC-009: Hiển thị yêu cầu đang tạo sau khi reload
- **Given**: Yêu cầu có job AI đang chạy ngầm.
- **When**: Khách hàng reload hoặc mở lại màn hình “Yêu cầu tạo mẫu hoa”.
- **Then**: Card yêu cầu hiển thị trạng thái “Đang tạo”.
- **And**: Tổng số mẫu chỉ tính các kết quả đã có ảnh hợp lệ.
- **And**: Job đang chạy chưa tạo History item.
- **And**: Các kết quả thành công trước đó vẫn được hiển thị.

### AC-010: Không có yêu cầu
- **Given**: Khách hàng không có yêu cầu tạo mẫu hoa nào.
- **When**: Mở “Yêu cầu tạo mẫu hoa”.
- **Then**: Hệ thống hiển thị empty state cho biết khách hàng chưa có yêu cầu tạo mẫu hoa.

### AC-011: Không tải được dữ liệu
- **Given**: Hệ thống không tải được danh sách yêu cầu hoặc danh sách kết quả.
- **When**: Quá trình tải thất bại.
- **Then**: Hệ thống hiển thị error state và cho phép khách hàng tải lại dữ liệu.

## References
- **Rules**:
  - [BR-095](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/9d83c7a2-8d9f-41b8-833d-f4c6e550a1e9)
  - [BR-096](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5d2f87a0-b9f8-4a88-97a3-ca6c941f22e5)
  - [BR-097](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a616dc95-b28a-4370-94f5-dff3cddab2ea)
  - [BR-098](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/b8dabb0d-98cf-4142-a86e-7e74681705ec)
  - [BR-099](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c34f1cbc-902b-49e9-bfeb-b0cf83e585dd)
  - [BR-100](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0fc0831c-0ef8-421f-8732-543580b05027)
- **Dependencies**:
  - [STORY-030](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/225510e4-396c-403b-9ae4-59f00859a2ce) / [30-InitializeFlowerDesignRequest.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/30-InitializeFlowerDesignRequest.md)
  - [STORY-033](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e1cdce1d-5295-4945-930a-95f9e8bee6b9) / [33-GenerateFlowerDesignWithAI.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/33-GenerateFlowerDesignWithAI.md)

## Non-Functional
- API danh sách phân trang phía server và không trả binary ảnh trong payload metadata.
- Mục tiêu phản hồi p95 ≤ 2 giây với page size cấu hình, không tính thời gian tải ảnh từ storage.
- Quyền sở hữu (ownership) được kiểm tra chặt chẽ tại backend.
- UI có đầy đủ loading, empty và error state.
- Một file ảnh lỗi không được làm hỏng toàn bộ danh sách.

## Out of Scope
- Generate / tạo lại mẫu hoa AI.
- Tải xuống mẫu hoa.
- Xóa History.
- Checkout và Order.
