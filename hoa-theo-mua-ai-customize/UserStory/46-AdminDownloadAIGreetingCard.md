# STORY-046: Admin tải xuống thiệp AI (Admin Downloads AI Greeting Card)

## Metadata
- **Story**: Là một Admin có quyền quản lý AI Custom, tôi muốn tải xuống file ảnh thiệp AI của khách hàng, để phục vụ kiểm tra, đối soát hoặc hỗ trợ nghiệp vụ.
- **Context**: Admin có thể thực hiện tải xuống từ menu thao tác tại danh sách thiệp hoặc từ trang Chi tiết thiệp.
  - Chức năng sử dụng chính file ảnh thiệp đã generate thành công trước đó.
  - Thao tác tải không gọi AI, không tạo ảnh mới, không tạo History record và không thay đổi quota của khách hàng.
  - US này chỉ áp dụng cho Admin có quyền tải thiệp AI; không thay thế quyền tải của khách hàng trong STORY-041.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hồ Hoàng Nam
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 17/08/2026)

## Conditions
- **Preconditions**:
  - Người dùng đã đăng nhập Website quản trị.
  - Người dùng có quyền tải thiệp trong module AI Custom.
  - History record của thiệp từng được tạo thành công.
- **Trigger**: Admin chọn “Tải ảnh thiệp xuống” tại danh sách hoặc Chi tiết thiệp.

## Flow
### Main Flow
1. Admin chọn “Tải ảnh thiệp xuống”.
2. Backend kiểm tra quyền tải của Admin.
3. Backend kiểm tra History record tồn tại và có file ảnh output được ghi nhận.
4. Backend kiểm tra file ảnh còn khả dụng trong storage.
5. Hệ thống lấy chính file ảnh thiệp đã generate thành công.
6. Hệ thống tạo tên file an toàn theo định dạng: `thiep_[ma-thiep]_[yyyyMMdd_HHmmss].png`.
7. Backend bắt đầu cung cấp file PNG cho thiết bị của Admin.
8. Thao tác không gọi AI, không tạo file ảnh mới trong storage, không tạo History record và không thay đổi quota của khách hàng.

### Alternative Flow
- **ALT-01 — Tải lại**: Admin được tải lại không giới hạn số lần nếu còn quyền truy cập và file còn khả dụng. Mỗi lần tải đều được kiểm tra lại quyền và trạng thái file.
- **ALT-02 — Tải từ danh sách**: Admin mở menu ba chấm của một item và chọn “Tải ảnh thiệp xuống”. Hệ thống thực hiện cùng quy trình kiểm tra như khi tải từ Chi tiết thiệp.
- **ALT-03 — Tải từ Chi tiết thiệp**: Admin chọn nút “Tải ảnh thiệp xuống” tại trang Chi tiết thiệp. File và tên file luôn sử dụng mã thiệp, không sử dụng mã Order.

### Exception Flow
- **EXC-01 — Không tìm thấy thiệp**: Backend kiểm tra mã thiệp hoặc History record được yêu cầu. Nếu thiệp không tồn tại, đã bị xóa logic hoặc không thể truy cập, backend không trả file, không trả storage URL hay metadata và hiển thị thông báo trạng thái tài nguyên không tồn tại.
- **EXC-02 — Không có quyền tải**: Backend kiểm tra quyền tải thiệp AI của người dùng hiện tại. Nếu không có quyền, backend từ chối request, không trả file/storage URL/đường dẫn nội bộ/metadata nhạy cảm và hiển thị thông báo không có quyền tải.
- **EXC-03 — Lỗi cung cấp file**: Backend gặp lỗi khi đọc file, stream file hoặc tạo response download. Hệ thống không ghi nhận tải thành công, không trả file hỏng, hiển thị “Tải thiệp thất bại. Vui lòng thử lại” và cho phép Admin thử lại.
- **EXC-04 — File ảnh không còn khả dụng**: History record của thiệp vẫn tồn tại nhưng file ảnh trong storage bị mất, hỏng hoặc không thể truy cập. Hệ thống không trả file/storage URL, giữ nguyên History record/metadata, không gọi AI tạo lại và hiển thị: “Ảnh không còn khả dụng.”

## Acceptance Criteria
### AC-001: Tải đúng file ảnh
- **Given**: Admin có quyền và file thiệp còn khả dụng.
- **When**: Admin chọn “Tải ảnh thiệp xuống”.
- **Then**: Hệ thống trả đúng file ảnh thiệp đã generate.
- **And**: Tên file sử dụng mã thiệp theo định dạng quy chuẩn.

### AC-002: Không gọi lại AI và không đổi quota
- **Given**: Thiệp đã có file output.
- **When**: Admin tải hoặc tải lại thiệp.
- **Then**: Hệ thống không gọi AI.
- **And**: Không tạo ảnh hoặc History record mới.
- **And**: Không thay đổi quota của khách hàng.

### AC-003: Tải lại không giới hạn
- **Given**: File còn khả dụng và Admin còn quyền truy cập.
- **When**: Admin tải nhiều lần.
- **Then**: Hệ thống tiếp tục cung cấp đúng file ảnh đã lưu.

### AC-004: Phân quyền truy cập
- **Given**: Người dùng không có quyền tải thiệp.
- **When**: Người dùng gọi API bằng mã thiệp.
- **Then**: Backend từ chối request.
- **And**: Không trả file hoặc storage URL.

### AC-005: Tên file an toàn
- **Given**: Mã thiệp chứa ký tự không an toàn.
- **When**: Hệ thống tạo tên file tải xuống.
- **Then**: Ký tự không an toàn được loại bỏ hoặc thay thế.
- **And**: Tên file kết thúc bằng `.png`.

## References
- **Rules**:
  - [BR-140](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/e6b1d971-1140-4a78-9173-e415327dce18)
  - [BR-141](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/51379482-5a1a-44c9-bc4f-64970e316a00)
  - [BR-142](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/37e14851-8725-4cc1-b578-e095dec06a4a)
  - [BR-143](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/ecef43eb-67f5-4c95-9e5c-4867d94cf326)
- **Dependencies**:
  - [STORY-035](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/54a960a5-99fa-4037-b1d8-7e931f2b7e43) / [35-GeneratePersonalizedCardWithAIAtCheckout.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/35-GeneratePersonalizedCardWithAIAtCheckout.md)
  - [STORY-036](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/a9328fef-ec31-4e66-beb6-569d5383a338) / [36-RegeneratePersonalizedCardWithAI.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/36-RegeneratePersonalizedCardWithAI.md)
  - [STORY-043](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/6e28060f-6d59-430e-81e6-1b4549ff1003) / [43-AdminViewAIGreetingCardDetails.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/43-AdminViewAIGreetingCardDetails.md)
  - [STORY-044](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/1a84a816-6d73-4bbd-abf8-5fca7fd25cb3) / [44-AdminViewAIGreetingCardsList.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-customize/UserStory/44-AdminViewAIGreetingCardsList.md)

## Non-Functional
- Backend không expose permanent storage URL, thông tin xác thực hoặc đường dẫn nội bộ.
- API metadata không nhúng binary/Base64 của ảnh.
- Với file còn khả dụng, hệ thống bắt đầu phản hồi download p95 ≤ 2 giây trong điều kiện bình thường, không tính thời gian truyền toàn bộ file.
- Response phải khai báo đúng MIME type `image/png`.
- Backend phải kiểm tra quyền ở mỗi lần tải, kể cả khi Admin biết hoặc đoán được mã thiệp.
- Mỗi lần Admin tải xuống ảnh thiệp của khách hàng, hệ thống phải ghi audit log gồm tối thiểu: Admin thực hiện, thiệp được tải và thời điểm thực hiện.

## Out of Scope
- Generate, tạo lại hoặc chỉnh sửa thiệp.
- Tải xuống mẫu hoa Custom AI.
- Chỉnh sửa hoặc xóa History record.
- Thay đổi Order hoặc quota.
- Export Excel.
