# STORY-047: Admin xem danh sách mẫu hoa Custom AI (Admin Views Custom AI Flower Designs List)

## Metadata
- **Story**: Là một Admin có quyền quản lý AI Custom, tôi muốn xem danh sách các mẫu hoa Custom AI được khách hàng tạo, để kiểm tra ảnh kết quả, Combo nguồn, khách hàng sở hữu và các Order liên quan.
- **Context**: Chức năng được truy cập tại **AI Custom → Mẫu hoa** trên sidebar của Website quản trị.
  - Mỗi kết quả generate có ảnh output hợp lệ được hiển thị thành đúng 01 item trong danh sách.
  - Một mẫu hoa vẫn chỉ xuất hiện đúng 01 lần dù được liên kết với nhiều Order.
  - Danh sách dùng để tra cứu nhanh. Thành phần Core, Support, Mockup nguồn, yêu cầu tùy chỉnh và Order liên kết được xem đầy đủ tại Chi tiết mẫu hoa.
  - Danh sách mẫu hoa không hiển thị giá hoặc bill. Giá được xem tại Chi tiết Order tương ứng.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hồ Hoàng Nam
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 18/08/2026)

## Conditions
- **Preconditions**:
  - Người dùng đã đăng nhập Website quản trị.
  - Người dùng có quyền xem dữ liệu AI Custom → Mẫu hoa.
- **Trigger**: Admin chọn **AI Custom → Mẫu hoa** trên sidebar.

## Flow
### Main Flow
1. Admin chọn **AI Custom → Mẫu hoa**.
2. Backend kiểm tra quyền truy cập của Admin.
3. Hệ thống lấy danh sách các kết quả Custom AI có ảnh output hợp lệ.
4. Hệ thống sắp xếp danh sách theo ngày tạo mới nhất trước.
5. Mỗi item trong bảng hiển thị:
   - Preview ảnh mẫu hoa.
   - Mã mẫu hoa.
   - Khách hàng.
   - Combo nguồn: mã hoặc tên Combo.
   - Ngày tạo.
   - Thao tác.
6. Admin có thể tìm kiếm, lọc, thay đổi số item trên trang hoặc chuyển trang.
7. Admin chọn Preview để mở Image Lightbox.
8. Admin chọn mã mẫu hoa hoặc “Xem chi tiết” trong menu thao tác để mở Chi tiết mẫu hoa.

### Alternative Flow
- **ALT-01 — Tìm kiếm mẫu hoa**: Admin nhập tên khách hàng, mã Order hoặc mã mẫu hoa. Hệ thống trả về các mẫu hoa phù hợp mà Admin có quyền xem. Khi tìm kiếm bằng mã Order, hệ thống trả về các mẫu hoa có liên kết với Order tương ứng (mã Order không bắt buộc hiển thị trên danh sách; xem đầy đủ tại Chi tiết mẫu hoa).
- **ALT-02 — Lọc theo Combo**: Admin mở bộ lọc Combo và chọn một hoặc nhiều Combo. Hệ thống chỉ hiển thị kết quả được tạo từ các Combo đã chọn. Admin có thể xóa từng điều kiện hoặc đặt lại bộ lọc.
- **ALT-03 — Thay đổi số item trên trang**: Danh sách mặc định hiển thị 10 item/trang. Admin có thể chọn: 5, 10, 20, 30, 40 hoặc 50 item/trang. Hệ thống tải lại danh sách theo số lượng đã chọn và đưa Admin về trang đầu tiên.
- **ALT-04 — Mẫu hoa liên kết nhiều Order**: Mẫu hoa vẫn chỉ xuất hiện đúng 01 item trong danh sách.
- **ALT-05 — Xem nhanh ảnh**: Admin chọn Preview tại danh sách. Hệ thống mở Image Lightbox, hiển thị ảnh theo đúng tỷ lệ và cho phép đóng để quay lại vị trí trước đó.

### Exception Flow
- **EXC-01 — Không có dữ liệu**: Khi chưa có mẫu hoa hoặc không có kết quả phù hợp với điều kiện tìm kiếm và bộ lọc, hệ thống hiển thị empty state. Hệ thống không hiển thị dữ liệu cũ hoặc dữ liệu thiếu.
- **EXC-02 — Không tải được dữ liệu**: Hệ thống hiển thị error state khi gặp lỗi hệ thống, lỗi mạng hoặc API không phản hồi và cho phép Admin tải lại danh sách.
- **EXC-03 — Không có quyền truy cập**: Backend từ chối request khi người dùng không có quyền, không trả ảnh, dữ liệu khách hàng, Combo, thành phần hoa, Order liên kết hoặc metadata nhạy cảm.
- **EXC-04 — File ảnh mẫu hoa không tải được**: Khi một file ảnh mẫu hoa trong danh sách không tải được, hệ thống vẫn giữ History record và metadata. Hệ thống chỉ hiển thị trạng thái lỗi tại vùng Preview của item tương ứng mà không làm hỏng toàn bộ danh sách, không tự động gọi AI tạo lại và cho phép Admin tải lại trang.

## Acceptance Criteria
### AC-001: Hiển thị danh sách mẫu hoa
- **Given**: Admin có quyền xem AI Custom.
- **When**: Admin mở AI Custom → Mẫu hoa.
- **Then**: Hệ thống hiển thị mỗi kết quả Custom AI có ảnh hợp lệ thành đúng 01 item.
- **And**: Sắp xếp theo ngày tạo mới nhất trước.

### AC-002: Mẫu hoa liên kết nhiều Order
- **Given**: Một mẫu hoa liên kết với nhiều Order.
- **When**: Danh sách mẫu hoa hiển thị.
- **Then**: Mẫu hoa chỉ xuất hiện đúng 01 item trong danh sách.

### AC-003: Tìm kiếm mẫu hoa
- **Given**: Danh sách có dữ liệu phù hợp.
- **When**: Admin tìm theo tên khách hàng, mã Order hoặc mã mẫu hoa.
- **Then**: Hệ thống chỉ hiển thị các mẫu hoa thỏa điều kiện tìm kiếm.
- **And**: Nếu tìm bằng mã Order, kết quả trả về phải là mẫu hoa có liên kết với Order đó.

### AC-004: Lọc theo Combo
- **Given**: Admin chọn một hoặc nhiều Combo.
- **When**: Áp dụng bộ lọc.
- **Then**: Hệ thống chỉ hiển thị các mẫu hoa được tạo từ các Combo đã chọn.

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

### AC-007: File ảnh không tải được
- **Given**: History record có ảnh output hợp lệ nhưng hệ thống không tải được file ảnh tại thời điểm hiển thị.
- **When**: Admin xem danh sách.
- **Then**: Danh sách hiển thị trạng thái lỗi tại vùng ảnh tương ứng.
- **And**: Cho phép Admin tải lại trang.
- **And**: Không tự động generate ảnh thay thế.
- **And**: Các item khác trong danh sách vẫn hiển thị bình thường.

### AC-008: Không hiển thị giá
- **Given**: Một mẫu hoa có thể liên kết với nhiều Order có giá khác nhau.
- **When**: Admin xem danh sách.
- **Then**: Hệ thống không hiển thị giá hoặc bill snapshot.
- **And**: Admin xem giá tại Chi tiết Order tương ứng.

### AC-009: Phân quyền truy cập
- **Given**: Người dùng không có quyền xem AI Custom.
- **When**: Người dùng gọi API danh sách.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh, dữ liệu mẫu hoa hoặc thông tin khách hàng.

## References
- **Rules**:
  - [BR-144](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/d40c4ad7-0723-43ab-ba2f-c52bf73b3cf1)
  - [BR-145](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/96489393-fd4e-4d3c-895a-5a0fd8d27bcd)
  - [BR-146](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/c57c1f24-ded5-4593-9b67-b049dedb4a4b)
  - [BR-150](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0ff0a844-39dd-4502-b928-f0b458cb3312)
  - [BR-151](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/35519137-c90d-42ae-a6b5-32460cefb38f)
  - [BR-152](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e98ce2c8-f7f8-4ff9-aae4-4cf51ab5296b)
  - [BR-132](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/556f0aa7-fe97-49c4-82fb-d64b637f4577)
- **Dependencies**:
  - [STORY-039](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/fc14ad2e-0f75-40f9-806d-f935e31d34f6) / [39-CompleteCheckoutCreateAndPayOrder.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/39-CompleteCheckoutCreateAndPayOrder.md)
  - [STORY-040](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/eed51ca8-012b-420e-a576-1eb053081d3e) / [40-ViewCustomAIFlowerDesignHistory.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/40-ViewCustomAIFlowerDesignHistory.md)

## Non-Functional
- API danh sách phải phân trang phía server.
- Danh sách đạt mục tiêu phản hồi p95 ≤ 2 giây với tối đa 50 item/trang trong điều kiện bình thường, không tính thời gian tải ảnh từ storage.
- API metadata không nhúng binary/Base64 và không expose đường dẫn storage nội bộ.
- Backend kiểm tra phân quyền trong mọi request, không chỉ dựa trên sidebar hoặc UI.
- UI có đầy đủ loading, empty và error state.
- Điều kiện tìm kiếm, bộ lọc và số item/trang được giữ khi Admin chuyển trang và quay lại từ trang chi tiết trong cùng phiên làm việc.
- Một file ảnh lỗi không làm hỏng toàn bộ danh sách.

## Out of Scope
- Generate, tạo lại, chỉnh sửa hoặc xóa mẫu hoa.
- Kiểm tra tồn kho hoặc trạng thái khả dụng hiện tại của Combo.
- Tải xuống mẫu hoa Custom AI.
- Thay đổi dữ liệu khách hàng.
- Thay đổi Order hoặc bill.
- Export Excel.
