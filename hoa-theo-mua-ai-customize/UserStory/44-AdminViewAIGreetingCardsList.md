# STORY-044: Admin xem danh sách thiệp AI (Admin Views AI Greeting Cards List)

## Metadata
- **Story**: Là một Admin có quyền quản lý AI Custom, tôi muốn xem danh sách các thiệp AI được khách hàng tạo, để kiểm tra ảnh kết quả, dữ liệu tạo thiệp, khách hàng sở hữu và các Order có liên quan.
- **Context**: Chức năng được truy cập tại **AI Custom → Thiệp** trên sidebar của Website quản trị.
  - Mỗi History record có ảnh output hợp lệ được hiển thị thành đúng 01 item trong danh sách.
  - Một thiệp vẫn chỉ xuất hiện đúng 01 lần dù được liên kết với nhiều Order.
  - Danh sách phục vụ tra cứu nhanh; Admin có thể chọn ảnh Preview để mở Image Lightbox mà không cần rời khỏi danh sách.
  - Danh sách thiệp không hiển thị giá. Giá và bill snapshot được xem tại Chi tiết Order tương ứng.
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
  - Người dùng đã đăng nhập Website quản trị.
  - Người dùng có quyền xem dữ liệu AI Custom → Thiệp.
- **Trigger**: Admin chọn **AI Custom → Thiệp** trên sidebar.

## Flow
### Main Flow
1. Admin chọn **AI Custom → Thiệp**.
2. Backend kiểm tra quyền truy cập của Admin.
3. Hệ thống lấy danh sách các History record thiệp có ảnh output hợp lệ.
4. Hệ thống sắp xếp danh sách theo ngày tạo mới nhất trước.
5. Mỗi item trong bảng hiển thị:
   - Preview ảnh thiệp.
   - Mã thiệp.
   - Khách hàng.
   - Template.
   - Size.
   - Hình thức: Calligraphy hoặc Gõ máy.
   - Ngày tạo.
   - Thao tác.
6. Admin có thể tìm kiếm, lọc, thay đổi số item trên trang hoặc chuyển trang.
7. Admin chọn ảnh Preview để mở Image Lightbox.
8. Admin chọn mã thiệp hoặc “Xem chi tiết” trong menu thao tác để mở Chi tiết thiệp.

### Alternative Flow
- **ALT-01 — Tìm kiếm thiệp**: Admin nhập tên khách hàng, mã Order hoặc mã thiệp vào ô tìm kiếm. Hệ thống trả về các thiệp phù hợp mà Admin có quyền xem. Điều kiện tìm kiếm được giữ nguyên khi chuyển trang.
- **ALT-02 — Lọc danh sách**: Admin mở bộ lọc và có thể lọc theo: Size, Hình thức (Calligraphy hoặc Gõ máy). Hệ thống áp dụng đồng thời các điều kiện lọc đã chọn. Admin có thể xóa từng điều kiện hoặc đặt lại toàn bộ bộ lọc.
- **ALT-03 — Thay đổi số item trên trang**: Danh sách mặc định hiển thị 10 item/trang. Admin có thể chọn: 5, 10, 20, 30, 40 hoặc 50 item/trang. Hệ thống tải lại danh sách theo số lượng đã chọn và đưa Admin về trang đầu tiên.
- **ALT-04 — Thiệp liên kết nhiều Order**: Thiệp vẫn chỉ xuất hiện đúng 01 item trong danh sách.
- **ALT-05 — Xem nhanh ảnh**: Admin chọn Preview tại danh sách. Hệ thống mở Image Lightbox, hiển thị ảnh theo đúng tỷ lệ và cho phép đóng để quay lại vị trí trước đó.

### Exception Flow
- **EXC-01 — Không có dữ liệu**: Hệ thống hiển thị empty state khi chưa có thiệp hoặc không có kết quả phù hợp với điều kiện tìm kiếm và bộ lọc.
- **EXC-02 — Không tải được dữ liệu**: Hệ thống hiển thị error state, không hiển thị dữ liệu thiếu như kết quả hoàn chỉnh và cho phép Admin tải lại.
- **EXC-03 — Không có quyền truy cập**: Backend từ chối request, không trả ảnh, thông tin khách hàng, dữ liệu thiệp, Order liên kết hoặc metadata nhạy cảm.
- **EXC-04 — File ảnh thiệp không còn khả dụng**: Hệ thống giữ History record và metadata của thiệp. Tại danh sách, Preview được thay bằng placeholder. Hệ thống không tự động gọi AI để tạo lại ảnh.
- **EXC-05 — Một ảnh tải lỗi tạm thời trong danh sách**: Lỗi của một file chỉ làm Preview tương ứng hiển thị placeholder và không được làm hỏng toàn bộ danh sách. Hệ thống cho phép Admin tải lại trang hoặc tải lại ảnh tương ứng.

## Acceptance Criteria
### AC-001: Hiển thị danh sách thiệp
- **Given**: Admin có quyền xem AI Custom.
- **When**: Admin mở AI Custom → Thiệp.
- **Then**: Hệ thống hiển thị mỗi History record thiệp thành đúng 01 item.
- **And**: Sắp xếp ngày tạo mới nhất trước.

### AC-002: Thiệp liên kết nhiều Order
- **Given**: Một thiệp liên kết với nhiều Order.
- **When**: Danh sách thiệp hiển thị.
- **Then**: Thiệp chỉ xuất hiện đúng 01 item trong danh sách.

### AC-003: Tìm kiếm thiệp
- **Given**: Danh sách có dữ liệu phù hợp.
- **When**: Admin tìm theo tên khách hàng, mã Order hoặc mã thiệp.
- **Then**: Hệ thống chỉ hiển thị các thiệp thỏa điều kiện tìm kiếm.

### AC-004: Lọc thiệp
- **Given**: Admin chọn Size và/hoặc hình thức.
- **When**: Áp dụng bộ lọc.
- **Then**: Hệ thống chỉ hiển thị các thiệp thỏa toàn bộ điều kiện đã chọn.

### AC-005: Phân trang danh sách
- **Given**: Danh sách có nhiều hơn số item trên một trang.
- **When**: Admin chọn 5, 10, 20, 30, 40 hoặc 50 item/trang.
- **Then**: Hệ thống phân trang theo số lượng đã chọn.
- **And**: Mặc định là 10 item/trang.

### AC-006: Mở Image Lightbox
- **Given**: File ảnh còn khả dụng.
- **When**: Admin chọn Preview.
- **Then**: Hệ thống mở Image Lightbox.
- **And**: Hiển thị ảnh đúng tỷ lệ.

### AC-007: File ảnh không còn khả dụng
- **Given**: History record còn nhưng file ảnh mất, hỏng hoặc không thể truy cập.
- **When**: Admin xem danh sách.
- **Then**: Danh sách hiển thị placeholder cho item tương ứng.

### AC-008: Không hiển thị giá/bill
- **Given**: Một thiệp có thể liên kết với nhiều Order có giá khác nhau.
- **When**: Admin xem danh sách.
- **Then**: Hệ thống không hiển thị giá thiệp hoặc bill snapshot.
- **And**: Admin xem giá tại Chi tiết Order tương ứng.

### AC-009: Không có quyền truy cập
- **Given**: Người dùng không có quyền xem AI Custom.
- **When**: Người dùng gọi API danh sách.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh, dữ liệu thiệp hoặc thông tin khách hàng.

## References
- **Rules**:
  - [BR-126](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/8b3abc1f-d5cc-4ea4-9217-38dfe0c52c35)
  - [BR-127](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5fc21993-cf12-4c58-bf64-db5e9b4f2763)
  - [BR-128](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/bb1e1878-9fac-44b1-9b14-1137cc43bc0b)
  - [BR-129](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3fda951f-01f8-40e1-8129-5e2ee16ea7f3)
  - [BR-130](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/2c5fddec-4c32-49d6-abe8-477073969fd6)
  - [BR-131](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/270060ac-9a31-4c28-b059-3879c464bace)
  - [BR-132](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/556f0aa7-fe97-49c4-82fb-d64b637f4577)
- **Dependencies**:
  - [STORY-035](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/54a960a5-99fa-4037-b1d8-7e931f2b7e43) / [35-GeneratePersonalizedCardWithAIAtCheckout.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/35-GeneratePersonalizedCardWithAIAtCheckout.md)

## Non-Functional
- API danh sách phải phân trang phía server.
- Danh sách đạt mục tiêu phản hồi p95 ≤ 2 giây với tối đa 50 item/trang trong điều kiện bình thường, không tính thời gian tải ảnh từ storage.
- API metadata không nhúng binary/Base64 và không expose đường dẫn storage nội bộ.
- Backend kiểm tra phân quyền trong mọi request, không chỉ dựa trên sidebar hoặc UI.
- UI có đầy đủ loading, empty và error state.
- Điều kiện tìm kiếm, bộ lọc và số item/trang được giữ khi Admin chuyển trang và quay lại từ trang chi tiết trong cùng phiên làm việc.
- Một file ảnh tải lỗi tạm thời không làm hỏng toàn bộ danh sách.

## Out of Scope
- Tạo, tạo lại, chỉnh sửa hoặc xóa thiệp.
- Thay đổi dữ liệu khách hàng.
- Thay đổi Order hoặc bill.
- Export Excel.
- Tải xuống thiệp ngoài phạm vi STORY-046.
