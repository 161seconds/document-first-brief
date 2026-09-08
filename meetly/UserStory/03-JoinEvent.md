# US-03: Tham gia sự kiện

## Metadata

- **Story**: Với vai trò là người tham gia, tôi muốn tham gia vào một sự kiện thông qua đường dẫn được cung cấp hoặc bằng ShortCode để có thể xem và sử dụng các thông tin, chức năng dành cho người tham gia trong sự kiện.
- **Context**: Người tham gia (Participant) cần tiếp cận sự kiện khảo sát thời gian họp mà họ được mời. Họ có thể truy cập trực tiếp bằng cách mở URL được chia sẻ hoặc nhập mã ngắn (ShortCode) tại trang chủ. Sau khi tham gia, người dùng có thể xem thông tin sự kiện, danh sách người tham gia, trạng thái bận/rảnh hiện tại và Heatmap. Khi muốn bình chọn, hệ thống sẽ yêu cầu thực hiện bước định danh (US-04) nếu chưa có định danh hợp lệ. Người dùng vẫn có thể tham gia sự kiện kể cả khi sự kiện đã kết thúc (để xem kết quả).
- **Sprint**: 1
- **Priority**: Must
- **Status**: Todo
- **Creator**: Product Owner
- **Assignee**:
  - Backend: Chưa chỉ định
  - Frontend: Chưa chỉ định
  - QA: Chưa chỉ định

---

## Conditions

### Preconditions

- Sự kiện đã được tạo trong hệ thống Meetly và có URL/ShortCode hợp lệ.
- Người dùng có kết nối mạng Internet.

### Trigger

- Người dùng truy cập trực tiếp bằng đường link URL sự kiện, hoặc nhập ShortCode trên màn hình "Tham gia sự kiện".

---

## Flow

### Main Flow (Tham gia qua URL)

1. Người dùng nhấp vào đường link sự kiện (URL) được ban tổ chức gửi.
2. Hệ thống xác thực mã định danh sự kiện từ URL.
3. Hệ thống kiểm tra sự tồn tại của sự kiện trong cơ sở dữ liệu.
4. Hệ thống tải thông tin sự kiện và chuyển hướng người dùng đến Dashboard sự kiện.
5. Người dùng xem các thông tin và chức năng dành cho người tham gia: thông tin sự kiện, danh sách người tham gia, Heatmap tổng quan và trạng thái khả dụng.

### Alternative Flow

- **ALT-01 — Tham gia bằng cách nhập ShortCode**:
  1. Người dùng mở trang chủ Meetly và nhấn chọn "Tham gia sự kiện".
  2. Hệ thống hiển thị ô nhập ShortCode.
  3. Người dùng nhập mã ShortCode và nhấn "Tham gia".
  4. Hệ thống kiểm tra tính hợp lệ và tìm kiếm sự kiện tương ứng.
  5. Hệ thống chuyển hướng người dùng đến Dashboard sự kiện.
- **ALT-02 — Yêu cầu định danh khi thực hiện bình chọn**:
  1. Tại Dashboard, người dùng nhấn chức năng "Bình chọn thời gian".
  2. Hệ thống kiểm tra trạng thái định danh của người dùng trong phiên hiện tại.
  3. Nếu người dùng chưa có định danh hợp lệ trong sự kiện này, hệ thống hiển thị modal/màn hình yêu cầu thực hiện bước Định danh (chuyển sang luồng US-04).
- **ALT-03 — Tham gia sự kiện đã kết thúc**:
  1. Người dùng mở link hoặc nhập ShortCode của sự kiện đã qua ngày cuối cùng khảo sát.
  2. Hệ thống vẫn cho phép tham gia và hiển thị Dashboard để xem kết quả khảo sát cuối cùng (ở chế độ chỉ xem, không cho phép bình chọn thêm).

### Exception Flow

- **EXC-01 — ShortCode không tồn tại hoặc không hợp lệ**:
  1. Người dùng nhập ShortCode sai, chứa ký tự không hợp lệ hoặc mã không tồn tại trên hệ thống.
  2. Hệ thống hiển thị thông báo lỗi: *"Mã sự kiện không tồn tại hoặc không hợp lệ. Vui lòng kiểm tra lại."*
  3. Người dùng không được phép tham gia và vẫn ở lại màn hình nhập mã.
- **EXC-02 — Truy cập trái phép Dashboard**:
  1. Người dùng cố tình truy cập trực tiếp đường dẫn Dashboard mà không có định danh sự kiện hợp lệ hoặc sự kiện đã bị xóa.
  2. Hệ thống chặn truy cập và điều hướng người dùng về trang chủ kèm thông báo lỗi.

---

## Acceptance Criteria

#### AC-001 — Tham gia sự kiện bằng URL
- **Given**: Người dùng nhận được đường dẫn (URL) hợp lệ của sự kiện.
- **When**: Người dùng mở đường dẫn trên trình duyệt web.
- **Then**: Hệ thống xác thực và đưa người dùng tham gia vào sự kiện thành công.

#### AC-002 — Tham gia sự kiện bằng ShortCode
- **Given**: Người dùng đang ở màn hình tham gia sự kiện.
- **When**: Người dùng nhập mã ShortCode hợp lệ và nhấn xác nhận.
- **Then**: Hệ thống kiểm tra mã và đưa người dùng tham gia vào sự kiện tương ứng.

#### AC-003 — Báo lỗi khi ShortCode không tồn tại hoặc không hợp lệ
- **Given**: Người dùng đang ở màn hình nhập ShortCode.
- **When**: Người dùng nhập mã ShortCode không tồn tại trong hệ thống hoặc chứa ký tự sai quy cách.
- **Then**: Hệ thống hiển thị thông báo lỗi phù hợp và không cho phép người dùng tham gia sự kiện.

#### AC-004 — Chuyển hướng đến Dashboard sự kiện
- **Given**: Người dùng tham gia sự kiện thành công qua URL hoặc ShortCode.
- **When**: Quá trình kiểm tra mã sự kiện hoàn tất.
- **Then**: Hệ thống tự động chuyển hướng người dùng đến màn hình Dashboard của sự kiện đó.

#### AC-005 — Xem thông tin và chức năng dành cho người tham gia
- **Given**: Người dùng đã tham gia vào Dashboard sự kiện.
- **When**: Người dùng duyệt các khu vực trên giao diện.
- **Then**: Người dùng có thể xem đầy đủ thông tin sự kiện, danh sách người tham gia, các nội dung/hoạt động hiện có và trạng thái Available hiện tại.

#### AC-006 — Yêu cầu định danh khi bình chọn
- **Given**: Người dùng đang ở Dashboard sự kiện và chưa có tài khoản định danh hợp lệ trong sự kiện đó.
- **When**: Người dùng thực hiện chức năng Bình chọn.
- **Then**: Hệ thống yêu cầu người dùng thực hiện bước Định danh trong sự kiện trước khi được phép thao tác.

#### AC-007 — Chặn truy cập Dashboard khi chưa tham gia sự kiện
- **Given**: Người dùng chưa tham gia sự kiện hoặc không có phiên truy cập hợp lệ.
- **When**: Người dùng tìm cách truy cập Dashboard hoặc các tài nguyên thuộc về sự kiện đó.
- **Then**: Hệ thống chặn truy cập và không hiển thị nội dung sự kiện.

#### AC-008 — Cho phép tham gia kể cả khi sự kiện đã kết thúc
- **Given**: Một sự kiện đã vượt quá thời gian bình chọn (đã kết thúc).
- **When**: Người dùng tham gia vào sự kiện bằng URL hoặc ShortCode.
- **Then**: Hệ thống vẫn cho phép người dùng tham gia để xem thông tin và kết quả khảo sát của sự kiện.

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng tham gia sự kiện (qua URL & ShortCode) đã hoàn thành.
- [ ] 2. Đã implement validation cho mã ShortCode và đường link sự kiện.
- [ ] 3. Đã xử lý trường hợp tham gia thành công / thất bại, mã không tồn tại.
- [ ] 4. Unit test đã được viết và pass toàn bộ.
- [ ] 5. Code đã được code review và phê duyệt.
- [ ] 6. QA đã test và pass toàn bộ 8 tiêu chí Acceptance Criteria.
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story.
- [ ] 8. Đã merge code vào branch theo quy định của team.
- [ ] 9. Đã deploy lên môi trường cần thiết (Staging/UAT).

---

## References

### Rules
- [`BR-01`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-01.md): Phân loại sự kiện.
- [`BR-04`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-04.md): ID sự kiện là duy nhất và bất biến.
- [`BR-12`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-12.md): Không được phép bình chọn khi vượt quá ngày cuối cùng trong danh sách (chỉ được xem).

### Dependencies
- [`US-01`](file:///d:/VNZ/document-first-brief/meetly/UserStory/01-CreateSurveyEvent.md): Tạo sự kiện cần khảo sát (sinh URL và ShortCode).

---

## Notes

- Giao diện nhập ShortCode nên hỗ trợ tính năng tự động chuyển chữ hoa (auto-uppercase) và tự động xóa dấu cách thừa để người dùng tiện thao tác.
