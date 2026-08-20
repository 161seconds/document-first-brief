# STORY-045: Xem lịch sử các thiệp đã tạo (View History of Created Greeting Cards)

## Metadata
- **Story**: Là một khách hàng đã sử dụng chức năng tạo thiệp AI, tôi muốn xem lại tất cả thiệp có ảnh đã tạo, để xem thông tin chi tiết và truy cập các thao tác liên quan đến thiệp đã tạo.
- **Context**: Chức năng được truy cập từ mục **“Thiệp thiết kế”** trên sidebar.
  - History hiển thị dạng bảng:

| Ảnh | Mã thiệp | Thông tin thiệp | Thời điểm tạo | Thao tác |
| :--- | :--- | :--- | :--- | :--- |
| Preview | CARD-00125 | Cổ điển · 10×15 · Viết tay | 17/08/2026 10:30 | Xem chi tiết |

  - Một thiệp có thể:
    - Chưa từng liên kết với Order.
    - Đang liên kết với một Order.
    - Được tái sử dụng và có nhiều record liên kết Thiệp–Order.
  - Mỗi History item đại diện cho đúng một lần generate có ảnh output hợp lệ.
  - Việc chọn lại hoặc tải xuống không tạo History item mới. Generate không có ảnh output không xuất hiện trong History.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hoàng Thị Khánh Linh
- **Author**: Hoàng Thị Khánh Linh
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hoàng Thị Khánh Linh
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 18/08/2026)

## Conditions
- **Preconditions**:
  - Khách hàng đã đăng nhập.
- **Trigger**: Khách hàng mở mục **“Thiệp thiết kế”** trên sidebar.

## Flow
### Main Flow
1. Khách hàng mở mục **“Thiệp thiết kế”** trên sidebar.
2. Backend lấy các History record có ảnh output thuộc khách hàng hiện tại.
3. Hệ thống sắp xếp danh sách theo thời điểm generate, mới nhất trước.
4. Hệ thống hiển thị mỗi thiệp với: Ảnh preview, Mã thiệp, Thông tin thiệp, Thời điểm tạo, Thao tác.
5. Nếu thiệp có liên kết Order, hệ thống hiển thị các mã Order liên quan.
6. Nếu chưa liên kết Order, hệ thống vẫn hiển thị thiệp bình thường (để trống thông tin Order).
7. Khách hàng chọn một thiệp để xem chi tiết.
8. Màn hình Chi tiết hiển thị: Ảnh kết quả, Metadata còn khả dụng, Các Order liên quan, Chức năng tải xuống và Chức năng tạo lại thiệp.

### Alternative Flow
- **ALT-01 — Thiệp chưa thuộc Order**: Khách hàng mở danh sách hoặc chi tiết một thiệp History. Hệ thống xác định thiệp chưa liên kết với Order nào. Hệ thống vẫn hiển thị thiệp trong History với đầy đủ thông tin metadata và trường Order để trống.
- **ALT-02 — Thiệp liên kết nhiều Order**:
  1. Khách hàng mở chi tiết một thiệp History đã được sử dụng cho nhiều Order.
  2. Hệ thống hiển thị danh sách các mã Order liên quan và chỉ hiển thị đúng 01 History item của ảnh gốc (không nhân bản item).
  3. Khách hàng có thể ấn vào mã Order để điều hướng qua trang lịch sử của Order đó.

### Exception Flow
- **EXC-01 — Không có History**: Backend không tìm thấy History record có ảnh output thuộc khách hàng hiện tại. Hệ thống hiển thị empty state kèm hướng dẫn khách hàng tạo thiệp mới.
- **EXC-02 — Không tải được danh sách**: Hệ thống không tải được danh sách History. Hệ thống không hiển thị dữ liệu không đầy đủ, hiển thị error state và cho phép khách hàng tải lại danh sách.
- **EXC-03 — Thiệp không thuộc khách hàng**: Khách hàng truy cập trực tiếp vào một thiệp bằng ID hoặc URL. Backend kiểm tra quyền sở hữu và từ chối truy cập nếu thiệp không thuộc khách hàng hiện tại (không trả ảnh hoặc metadata).
- **EXC-04 — File không còn khả dụng**: File ảnh của thiệp trong storage không còn khả dụng. Hệ thống vẫn giữ History record, hiển thị các metadata còn lại kèm thông báo: “Ảnh không còn khả dụng” và vô hiệu hóa chức năng tải xuống.

## Acceptance Criteria
### AC-001: Hiển thị danh sách thiệp đã tạo
- **Given**: Khách hàng có các thiệp generate có ảnh output hợp lệ.
- **When**: Mở “Thiệp thiết kế”.
- **Then**: Hệ thống hiển thị danh sách chung của khách hàng.
- **And**: Sắp xếp theo thời điểm tạo mới nhất trước.

### AC-002: Generate không có ảnh output
- **Given**: Một lần generate kết thúc.
- **When**: Không tạo được ảnh output hợp lệ.
- **Then**: Không tạo History item trong danh sách.

### AC-003: Thiệp chưa liên kết Order
- **Given**: Một thiệp có ảnh output nhưng chưa liên kết Order nào.
- **When**: Khách hàng xem History.
- **Then**: Thiệp vẫn xuất hiện trong danh sách.
- **And**: Khách hàng có thể xem chi tiết, tải xuống hoặc Tạo lại thiệp.

### AC-004: Thiệp liên kết nhiều Order
- **Given**: Một thiệp đã được dùng cho Order A và Order B.
- **When**: Khách hàng xem History.
- **Then**: Chỉ có đúng 01 History item ảnh gốc.
- **And**: Có hai liên kết Order tương ứng hiển thị trong chi tiết.

### AC-005: Xem chi tiết thiệp
- **Given**: Khách hàng chọn một thiệp của mình trong danh sách.
- **When**: Mở Chi tiết thiệp.
- **Then**: Hệ thống hiển thị ảnh kết quả và đầy đủ metadata.
- **And**: Hiển thị các mã Order liên quan nếu có.

### AC-006: File ảnh không còn khả dụng
- **Given**: History record tồn tại nhưng file ảnh bị mất hoặc hỏng.
- **When**: Mở Chi tiết thiệp.
- **Then**: Record vẫn tồn tại và hiển thị metadata.
- **And**: Hiển thị thông báo: “Ảnh không còn khả dụng.”
- **And**: Vô hiệu hóa chức năng tải xuống.

### AC-007: Kiểm tra quyền sở hữu
- **Given**: Thiệp thuộc khách hàng khác.
- **When**: User truy cập trực tiếp bằng ID hoặc URL.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh hoặc metadata nhạy cảm.

## References
- **Rules**:
  - [BR-133](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c6e43277-f57a-495b-8f0e-59929386c30e)
  - [BR-134](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/14e5617a-93d9-4b7f-b79a-81ffa9997305)
  - [BR-135](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/77859db4-5755-4ef7-ad89-18093ffa450b)
  - [BR-136](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/f0f4ef94-8cc7-4e23-8d2d-2b4a932417fc)
  - [BR-137](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/562e4749-d01e-4244-94b8-5bd3db56ec78)
  - [BR-138](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a36b64f9-2e28-4fa1-8336-01082bd8a7c5)
  - [BR-139](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0c1312b1-89a3-4eaf-ab39-ef084d66e00b)
- **Dependencies**:
  - [STORY-035](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/54a960a5-99fa-4037-b1d8-7e931f2b7e43) / [35-GeneratePersonalizedCardWithAIAtCheckout.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/35-GeneratePersonalizedCardWithAIAtCheckout.md)
  - [STORY-036](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a9328fef-ec31-4e66-beb6-569d5383a338) / [36-RegeneratePersonalizedCardWithAI.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/36-RegeneratePersonalizedCardWithAI.md)
  - [STORY-041](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e81e3a1f-ef07-4e0c-99d7-54877ebcfb22) / [41-DownloadCreatedGreetingCard.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/41-DownloadCreatedGreetingCard.md)

## Non-Functional
- API danh sách phải phân trang phía server và không trả binary ảnh trong payload metadata.
- Mục tiêu phản hồi p95 ≤ 2 giây với page size được cấu hình, không tính thời gian tải ảnh từ storage.
- Quyền sở hữu (ownership) phải được kiểm tra nghiêm ngặt tại backend.
- Một file ảnh không khả dụng không được làm hỏng toàn bộ danh sách.
- UI có các trạng thái loading, empty và error rõ ràng.

## Out of Scope
- Generate / tạo lại thiệp.
- Chỉnh sửa hoặc xóa History.
- Thanh toán và quản lý Order.
- Chia sẻ thiệp.
