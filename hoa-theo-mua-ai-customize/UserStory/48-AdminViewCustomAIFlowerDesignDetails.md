# STORY-048: Admin xem chi tiết mẫu hoa Custom AI (Admin Views Custom AI Flower Design Details)

## Metadata
- **Story**: Là một Admin có quyền quản lý AI Custom, tôi muốn xem thông tin chi tiết các mẫu hoa Custom AI được khách hàng tạo, để kiểm tra ảnh kết quả, Combo nguồn, thành phần hoa, yêu cầu tùy chỉnh, khách hàng sở hữu và các Order liên quan.
- **Context**: Chức năng được truy cập tại **AI Custom → Mẫu hoa** trên sidebar của Website quản trị.
  - Thành phần Core, Support, Mockup nguồn, yêu cầu tùy chỉnh và Order liên kết được xem đầy đủ tại Chi tiết mẫu hoa.
  - Chi tiết mẫu hoa không hiển thị giá hoặc bill. Giá được xem tại Chi tiết Order tương ứng.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hồ Hoàng Nam
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Nguyễn Đức Bình
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Version**: v0.1 (Đang duyệt - Cập nhật 15/08/2026)

## Conditions
- **Preconditions**:
  - Người dùng đã đăng nhập Website quản trị.
  - Người dùng có quyền xem dữ liệu AI Custom → Mẫu hoa.
- **Trigger**:
  - Admin mở trực tiếp Chi tiết mẫu hoa bằng mã mẫu hợp lệ.
  - Hoặc Admin chọn mã mẫu hoa / “Xem chi tiết” trong menu thao tác từ STORY-047.

## Flow
### Main Flow
1. Backend kiểm tra quyền truy cập và kết quả mẫu hoa.
2. Hệ thống hiển thị:
   - Ảnh kết quả mẫu hoa.
   - Mã mẫu hoa.
   - Thông tin khách hàng: họ tên, số điện thoại và email (nếu có).
   - Mã, tên và thông tin Combo nguồn.
   - Danh sách thành phần Core và số lượng từng loại tại thời điểm tạo.
   - Danh sách thành phần Support và số lượng từng loại tại thời điểm tạo.
   - Mockup nguồn được sử dụng.
   - Yêu cầu hoặc ghi chú tùy chỉnh.
   - Ngày tạo.
   - Trạng thái file.
   - Danh sách đầy đủ các Order liên kết.
3. Admin có thể chọn ảnh kết quả hoặc Mockup để mở Image Lightbox.
4. Nếu có Order liên kết, Admin có thể chọn mã Order để mở Chi tiết Order.

### Alternative Flow
- **ALT-01 — Mẫu hoa liên kết nhiều Order**: Trang Chi tiết mẫu hoa hiển thị toàn bộ Order liên kết; mỗi Order giữ thông tin bill riêng tại Chi tiết Order.
- **ALT-02 — Xem nhanh ảnh**: Admin chọn ảnh tại trang chi tiết. Hệ thống mở Image Lightbox, hiển thị ảnh theo đúng tỷ lệ và cho phép đóng để quay lại vị trí trước đó.

### Exception Flow
- **EXC-01 — Không tải được dữ liệu**: Khi hệ thống không tải được dữ liệu chi tiết do lỗi hệ thống, lỗi mạng hoặc API không phản hồi, hệ thống hiển thị error state, không hiển thị dữ liệu thiếu/cũ như kết quả hoàn chỉnh và cho phép Admin tải lại.
- **EXC-02 — Không có quyền truy cập**: Khi người dùng không có quyền, backend từ chối request, không trả ảnh, dữ liệu khách hàng, Combo, thành phần hoa, Order liên kết hoặc metadata nhạy cảm và hiển thị thông báo không có quyền truy cập.
- **EXC-03 — Không tìm thấy mẫu hoa**: Khi mã mẫu hoa không tồn tại hoặc không thể truy cập, backend không trả dữ liệu chi tiết, hiển thị thông báo trạng thái tài nguyên không tồn tại và cho phép Admin quay lại danh sách mẫu hoa.
- **EXC-04 — File ảnh mẫu hoa không tải được**: Hệ thống giữ History record và metadata. Trang chi tiết hiển thị trạng thái lỗi tại vùng ảnh, cho phép Admin tải lại trang và không tự động gọi AI tạo lại ảnh.

## Acceptance Criteria
### AC-001: Mẫu hoa liên kết nhiều Order
- **Given**: Một mẫu hoa liên kết với nhiều Order.
- **When**: Admin xem Chi tiết mẫu hoa.
- **Then**: Chi tiết mẫu hoa hiển thị đầy đủ các Order liên kết.

### AC-002: Mở Image Lightbox
- **Given**: File ảnh còn khả dụng.
- **When**: Admin chọn ảnh tại trang chi tiết.
- **Then**: Hệ thống mở Image Lightbox.
- **And**: Hiển thị ảnh đúng tỷ lệ.

### AC-003: Xem chi tiết mẫu hoa thành công
- **Given**: Admin có quyền và mẫu hoa tồn tại.
- **When**: Admin chọn mã mẫu hoặc “Xem chi tiết”.
- **Then**: Hệ thống hiển thị đầy đủ dữ liệu khách hàng, Combo nguồn, Core, Support, Mockup, yêu cầu tùy chỉnh và Order liên kết.

### AC-004: File ảnh không tải được
- **Given**: History record có ảnh output hợp lệ nhưng hệ thống không tải được file ảnh tại thời điểm hiển thị.
- **When**: Admin xem chi tiết.
- **Then**: Trang chi tiết vẫn hiển thị metadata.
- **And**: Hiển thị trạng thái lỗi tại vùng ảnh.
- **And**: Cho phép Admin tải lại trang.
- **And**: Không tự động generate ảnh thay thế.

### AC-005: Không hiển thị giá
- **Given**: Một mẫu hoa có thể liên kết với nhiều Order có giá khác nhau.
- **When**: Admin xem Chi tiết mẫu hoa.
- **Then**: Hệ thống không hiển thị giá hoặc bill snapshot.
- **And**: Admin xem giá tại Chi tiết Order tương ứng.

### AC-006: Phân quyền truy cập
- **Given**: Người dùng không có quyền xem AI Custom.
- **When**: Người dùng gọi API chi tiết.
- **Then**: Backend từ chối request.
- **And**: Không trả ảnh, dữ liệu mẫu hoa hoặc thông tin khách hàng.

## References
- **Rules**:
  - [BR-147](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/d7ba22e1-39f3-4c7a-8033-fd13c480478a)
  - [BR-148](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/71698060-d5a9-4f17-8009-8dfe848db5b9)
  - [BR-149](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/96d99b5e-1045-4ec3-a8a8-4f91cd38e2ff)
  - [BR-150](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/0ff0a844-39dd-4502-b928-f0b458cb3312)
  - [BR-151](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/35519137-c90d-42ae-a6b5-32460cefb38f)
  - [BR-132](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/556f0aa7-fe97-49c4-82fb-d64b637f4577)
- **Dependencies**:
  - [STORY-030](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/225510e4-396c-403b-9ae4-59f00859a2ce) / [30-InitializeFlowerDesignRequest.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/30-InitializeFlowerDesignRequest.md)
  - [STORY-033](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e1cdce1d-5295-4945-930a-95f9e8bee6b9) / [33-GenerateFlowerDesignWithAI.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/33-GenerateFlowerDesignWithAI.md)
  - [STORY-039](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/fc14ad2e-0f75-40f9-806d-f935e31d34f6) / [39-CompleteCheckoutCreateAndPayOrder.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/39-CompleteCheckoutCreateAndPayOrder.md)
  - [STORY-040](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/eed51ca8-012b-420e-a576-1eb053081d3e) / [40-ViewCustomAIFlowerDesignHistory.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/40-ViewCustomAIFlowerDesignHistory.md)

## Non-Functional
- Chi tiết mẫu hoa đạt mục tiêu phản hồi p95 ≤ 2 giây trong điều kiện bình thường, không tính thời gian tải ảnh từ storage.
- API metadata không nhúng binary/Base64 và không expose đường dẫn storage nội bộ.
- Backend kiểm tra phân quyền trong mọi request, không chỉ dựa trên sidebar hoặc UI.
- UI có đầy đủ loading, empty và error state.

## Out of Scope
- Generate, tạo lại, chỉnh sửa hoặc xóa mẫu hoa.
- Kiểm tra tồn kho hoặc trạng thái khả dụng hiện tại của Combo.
- Tải xuống mẫu hoa Custom AI.
- Thay đổi dữ liệu khách hàng.
- Thay đổi Order hoặc bill.
- Export Excel.
