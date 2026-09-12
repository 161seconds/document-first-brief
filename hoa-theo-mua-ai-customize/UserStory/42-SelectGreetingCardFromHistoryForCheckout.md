# STORY-042: Chọn thiệp từ History cho Checkout (Select Greeting Card from History for Checkout)

## Metadata
- **Story**: Là một khách hàng đã đăng nhập và đang thực hiện Checkout, tôi muốn chọn lại một thiệp đã tạo trong History, để sử dụng thiệp đó cho Checkout mà không phải gọi AI tạo lại.
- **Context**: Chức năng được mở từ Checkout thông qua lựa chọn “Chọn thiệp từ lịch sử”.
  - Hệ thống chỉ hiển thị các thiệp có History record thuộc khách hàng hiện tại và từng có ảnh output hợp lệ.
  - Một Checkout có tối đa 01 thiệp đang chọn.
  - Chọn một thiệp khác sẽ thay thế thiệp hiện tại nhưng không xóa bất kỳ History record nào.
  - Thiệp đã từng liên kết với Order vẫn được phép chọn cho Checkout mới.
  - Việc chọn lại không gọi AI, không sao chép ảnh, không tạo History record và không trừ quota.
  - Việc tạo Order, tính bill cuối và tạo liên kết Thiệp–Order thuộc STORY-039.
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
  - Checkout tồn tại, thuộc khách hàng hiện tại và chưa hoàn tất.
- **Trigger**: Khách hàng chọn “Chọn thiệp từ lịch sử” hoặc thao tác bỏ thiệp hiện tại tại Checkout.

## Flow
### Main Flow
1. Khách hàng mở chức năng “Chọn thiệp từ lịch sử” tại Checkout.
2. Backend kiểm tra Checkout tồn tại, thuộc khách hàng hiện tại và chưa hoàn tất.
3. Hệ thống lấy danh sách thiệp có History record thuộc khách hàng và có ảnh output hợp lệ.
4. Hệ thống hiển thị danh sách thiệp theo thời điểm generate gần nhất, mới nhất trước.
5. Khách hàng chọn một thiệp.
6. Backend kiểm tra quyền sở hữu và khả dụng của file ảnh.
7. Hệ thống gắn thiệp đã chọn làm thiệp đã được xác nhận và đang chọn của Checkout.
8. Nếu Checkout đã có thiệp khác, hệ thống gỡ thiệp cũ khỏi Checkout nhưng vẫn giữ thiệp đó trong History.
9. Hệ thống không gọi AI, không sao chép ảnh, không tạo History record và không trừ quota.
10. Order Summary hiển thị thiệp đang chọn và giá tạm tính hiện hành.

### Alternative Flow
- **ALT-01 — Không chọn thiệp**: Khách hàng đóng danh sách History và tiếp tục Checkout mà không thay đổi thiệp đang chọn. Nếu Checkout chưa có thiệp, khách hàng có thể tiếp tục không kèm thiệp.
- **ALT-02 — Thay đổi thiệp đang chọn**: Khách hàng chọn một thiệp khác từ History. Thiệp mới thay thế thiệp hiện tại của Checkout; các thiệp còn lại vẫn được giữ trong History.
- **ALT-03 — Chọn lại thiệp đã từng dùng cho Order**: Hệ thống vẫn cho phép chọn thiệp nếu thiệp thuộc khách hàng và file ảnh còn khả dụng. Các Order và liên kết trước đó không bị thay đổi.
- **ALT-04 — Template cũ không còn khả dụng**: Thiệp đã tạo thành công vẫn được phép chọn lại nếu file ảnh còn khả dụng. Việc Template nguồn ngừng khả dụng chỉ chặn generate mới bằng Template đó.
- **ALT-05 — Bỏ thiệp khỏi Checkout**: Khách hàng bỏ chọn thiệp hiện tại. Hệ thống gỡ thiệp khỏi Checkout nhưng vẫn giữ History record và cho phép khách hàng tiếp tục Checkout không có thiệp.

### Exception Flow
- **EXC-01 — Không có thiệp trong History**: Hệ thống không tìm thấy thiệp phù hợp trong History của khách hàng hiện tại. Hệ thống hiển thị empty state và cho phép khách hàng quay lại tạo thiệp mới hoặc tiếp tục Checkout không có thiệp.
- **EXC-02 — Checkout không hợp lệ**: Checkout không tồn tại, không thuộc khách hàng hiện tại hoặc đã hoàn tất. Backend từ chối thao tác và không thay đổi thiệp đang chọn.
- **EXC-03 — Không có quyền với thiệp**: History record không thuộc khách hàng hiện tại. Backend từ chối request, không gắn thiệp vào Checkout, không trả ảnh hoặc metadata nhạy cảm.
- **EXC-04 — File ảnh không còn khả dụng**: Backend phát hiện file ảnh của thiệp trong storage không còn khả dụng. Hệ thống không cho chọn thiệp, giữ nguyên History record/metadata và hiển thị: “Ảnh không còn khả dụng.”
- **EXC-05 — Không tải được danh sách History**: Hệ thống không tải được danh sách History. Hệ thống hiển thị error state, giữ nguyên thiệp đang chọn trong Checkout và cho phép khách hàng tải lại.
- **EXC-06 — Request chọn thiệp bị gửi trùng**: Khách hàng gửi request chọn thiệp nhiều lần hoặc request bị gửi lại. Backend xử lý theo cơ chế idempotent, không tạo nhiều liên kết tạm và không làm thay đổi dữ liệu History hay quota.

## Acceptance Criteria
### AC-001: Hiển thị thiệp thuộc khách hàng
- **Given**: Checkout hợp lệ.
- **When**: Khách hàng mở chức năng chọn thiệp từ History.
- **Then**: Hệ thống chỉ hiển thị các thiệp thuộc khách hàng hiện tại có ảnh output hợp lệ.

### AC-002: Chọn thiệp thành công
- **Given**: Thiệp thuộc khách hàng và file ảnh còn khả dụng.
- **When**: Khách hàng chọn thiệp.
- **Then**: Hệ thống gắn thiệp làm thiệp đã được xác nhận và đang chọn của Checkout.
- **And**: Order Summary hiển thị thiệp đã chọn.

### AC-003: Một thiệp trong Checkout
- **Given**: Checkout đã có thiệp A.
- **When**: Khách hàng chọn thiệp B.
- **Then**: B thay thế A trong Checkout.
- **And**: A vẫn được giữ trong History.

### AC-004: Không gọi AI và không ảnh hưởng quota
- **Given**: Thiệp đã có History record hợp lệ.
- **When**: Khách hàng chọn thiệp cho Checkout.
- **Then**: Hệ thống không gọi AI.
- **And**: Không sao chép ảnh.
- **And**: Không tạo History record mới.
- **And**: Không thay đổi quota.

### AC-005: Tái sử dụng thiệp đã từng đặt hàng
- **Given**: Thiệp đã từng liên kết với một hoặc nhiều Order.
- **When**: Khách hàng chọn thiệp cho Checkout mới.
- **Then**: Hệ thống vẫn cho phép chọn nếu thiệp thuộc khách hàng và file còn khả dụng.
- **And**: Các Order và liên kết trước đó không bị thay đổi.

### AC-006: Template nguồn không còn khả dụng
- **Given**: Template nguồn của thiệp đã ngừng khả dụng.
- **When**: Khách hàng chọn lại thiệp có file ảnh hợp lệ.
- **Then**: Hệ thống vẫn cho phép gắn thiệp vào Checkout.

### AC-007: File ảnh không còn khả dụng
- **Given**: History record còn tồn tại nhưng file ảnh mất hoặc hỏng.
- **When**: Khách hàng chọn thiệp.
- **Then**: Hệ thống không gắn thiệp vào Checkout.
- **And**: Hiển thị: “Ảnh không còn khả dụng.”

### AC-008: Kiểm tra Ownership
- **Given**: Thiệp thuộc khách hàng khác.
- **When**: User gửi request chọn thiệp bằng ID hoặc URL.
- **Then**: Backend từ chối request.
- **And**: Không gắn thiệp vào Checkout.
- **And**: Không trả ảnh hoặc metadata nhạy cảm.

### AC-009: Checkout không hợp lệ
- **Given**: Checkout không tồn tại, không thuộc khách hàng hoặc đã hoàn tất.
- **When**: Khách hàng chọn thiệp.
- **Then**: Backend từ chối request.
- **And**: Không thay đổi thiệp đang chọn.

### AC-010: Bỏ thiệp khỏi Checkout
- **Given**: Checkout đang có một thiệp được chọn.
- **When**: Khách hàng chọn bỏ thiệp.
- **Then**: Hệ thống gỡ thiệp khỏi Checkout.
- **And**: Vẫn giữ thiệp trong History.
- **And**: Cho phép tiếp tục Checkout không có thiệp.

### AC-011: Không tải được danh sách History
- **Given**: Checkout hợp lệ và khách hàng mở chức năng chọn thiệp từ History.
- **When**: Hệ thống không tải được danh sách History.
- **Then**: Hệ thống hiển thị error state.
- **And**: Giữ nguyên thiệp đang chọn trong Checkout.
- **And**: Cho phép khách hàng tải lại.

### AC-012: Request chọn thiệp bị gửi trùng
- **Given**: Khách hàng chọn một thiệp hợp lệ cho Checkout.
- **When**: Request chọn thiệp bị gửi lặp lại.
- **Then**: Backend xử lý idempotent.
- **And**: Không tạo nhiều liên kết tạm cho cùng một thiệp trong Checkout.
- **And**: Không thay đổi History hoặc quota.

## References
- **Rules**:
  - [BR-110](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/7673ca62-beb9-4542-9d77-483c2ecbd865)
  - [BR-111](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/de42c1af-ff3c-46d2-a597-b5c33311de4e)
  - [BR-112](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e3417531-fad6-4974-8c70-2c26555684f5)
  - [BR-113](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/415b72c7-5124-4ab3-8155-50c7c0e6e500)
  - [BR-114](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/ed7d5cf8-f07a-4ff2-ac26-5d50bf6791da)
  - [BR-115](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/f4478f91-dd86-4d5a-a825-ea423e0480d8)
  - [BR-116](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/97ef5916-4ca6-460f-a02d-a28b26d6823b)
  - [BR-117](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/8f6b23f3-7321-4b92-a7e6-1ccedf83e959)
- **Dependencies**:
  - [STORY-038](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5cc87b78-e6bf-4996-8d67-787e0bc34bb4) / [38-InitializeCheckoutFromFlowerDesign.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/38-InitializeCheckoutFromFlowerDesign.md)
  - [STORY-045](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3f89b027-0641-4888-909e-bbb18795398d)

## Non-Functional
- Danh sách History được phân trang phía server và sắp xếp mới nhất trước.
- Tải danh sách History p95 ≤ 1.5 giây với page size cấu hình, không tính thời gian tải ảnh từ storage.
- Backend kiểm tra ownership của Checkout và History record trong mọi request.
- API danh sách không trả binary ảnh hoặc expose đường dẫn storage nội bộ.
- UI có đầy đủ loading, empty và error state.
- Một file ảnh lỗi không được làm hỏng toàn bộ danh sách.

## Out of Scope
- Generate hoặc tạo lại thiệp bằng AI.
- Hoàn tất Checkout, tạo Order và thanh toán.
- Tạo liên kết Thiệp–Order và tính bill cuối.
- Tải xuống, chia sẻ, chỉnh sửa hoặc xóa thiệp.
