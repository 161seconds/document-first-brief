# US-02: Chỉnh sửa thông tin của sự kiện đã tạo

## Metadata

- **Story**: Với vai trò là một người tổ chức sự kiện, tôi muốn chỉnh sửa thông tin của sự kiện đã tạo để đảm bảo thông tin khớp với thực tế hiện tại.
- **Context**: Sau khi tạo sự kiện, người tổ chức (Admin) có thể cần điều chỉnh lại tên sự kiện, loại sự kiện hoặc danh sách các ngày đề xuất bình chọn để phản ánh kế hoạch thực tế. Chức năng này chỉ được phép truy cập từ Dashboard (khu vực chứa Heatmap) bởi tài khoản Admin đã đăng nhập. Khi thay đổi loại sự kiện, người tổ chức bắt buộc phải chọn lại danh sách ngày tương ứng.
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

- Sự kiện đã tồn tại trên hệ thống.
- Người dùng đã đăng nhập thành công vào tài khoản Admin của sự kiện đó.
- Người dùng đang ở màn hình Dashboard (nơi hiển thị Heatmap) của sự kiện.

### Trigger

- Admin nhấn nút "Chỉnh sửa sự kiện" (Edit Event) trên giao diện Dashboard.

---

## Flow

### Main Flow

1. Admin chọn chức năng "Chỉnh sửa sự kiện" trên Dashboard sự kiện.
2. Hệ thống kiểm tra quyền Admin của phiên đăng nhập hiện tại.
3. Hệ thống mở giao diện chỉnh sửa, điền sẵn toàn bộ dữ liệu hiện tại của sự kiện (Tên sự kiện, Loại sự kiện, Danh sách ngày bình chọn).
4. Admin thực hiện cập nhật các thông tin được phép chỉnh sửa:
   - Thay đổi tên sự kiện.
   - Thay đổi loại sự kiện (nếu cần).
   - Thêm hoặc bớt các ngày trong danh sách ngày bình chọn.
5. Admin nhấn nút "Lưu thay đổi".
6. Hệ thống xác thực tính hợp lệ của toàn bộ dữ liệu.
7. Hệ thống cập nhật thông tin sự kiện vào cơ sở dữ liệu.
8. Hệ thống thông báo cập nhật thành công và đóng màn hình chỉnh sửa.
9. Hệ thống cập nhật dữ liệu mới trên Dashboard sự kiện mà không cần tải lại toàn trang.

### Alternative Flow

- **ALT-01 — Thay đổi loại sự kiện**:
  1. Tại bước 4 của Main Flow, Admin thay đổi loại sự kiện (ví dụ: chuyển từ "Dates and Times" sang "Weekdays" hoặc ngược lại).
  2. Hệ thống cảnh báo thay đổi loại sự kiện và yêu cầu Admin chọn lại toàn bộ danh sách ngày/thứ bình chọn mới.
  3. Admin chọn danh sách ngày mới phù hợp với loại sự kiện vừa chọn rồi tiếp tục lưu.
- **ALT-02 — Hủy chỉnh sửa**:
  1. Tại bước 4 của Main Flow, Admin chọn "Hủy" hoặc "Quay lại".
  2. Hệ thống đóng form chỉnh sửa, không lưu các thay đổi chưa được xác nhận và giữ nguyên trạng thái Dashboard trước đó.

### Exception Flow

- **EXC-01 — Không có quyền Admin hoặc chưa đăng nhập**:
  1. Người dùng chưa đăng nhập hoặc không phải tài khoản Admin của sự kiện cố gắng gọi chức năng chỉnh sửa.
  2. Hệ thống không hiển thị nút chỉnh sửa trên Dashboard; nếu gọi qua API trực tiếp, hệ thống chặn với mã lỗi `403 Forbidden` kèm thông báo phù hợp.
- **EXC-02 — Xóa trắng trường thông tin bắt buộc**:
  1. Admin xóa dữ liệu của trường bắt buộc (ví dụ: xóa trắng Tên sự kiện hoặc xóa hết danh sách ngày).
  2. Hệ thống hiển thị thông báo lỗi yêu cầu nhập thông tin và vô hiệu hóa/chặn thao tác "Lưu".
- **EXC-03 — Dữ liệu sai định dạng / vi phạm quy tắc**:
  1. Admin nhập thông tin vi phạm quy tắc (ví dụ: chọn ngày quá khứ cho sự kiện Dates and Times).
  2. Hệ thống hiển thị thông báo lỗi cụ thể tại vị trí vi phạm và giữ nguyên dữ liệu đang nhập.
- **EXC-04 — Lỗi trong quá trình lưu dữ liệu**:
  1. Tại bước 7 của Main Flow, hệ thống gặp sự cố kết nối máy chủ hoặc cơ sở dữ liệu.
  2. Hệ thống hiển thị thông báo lỗi phù hợp và không xóa đi các dữ liệu Admin đang nhập dở.

---

## Acceptance Criteria

#### AC-001 — Quyền truy cập chức năng Chỉnh sửa sự kiện
- **Given**: Người dùng đã đăng nhập vào tài khoản Admin của sự kiện.
- **When**: Người dùng truy cập Dashboard sự kiện.
- **Then**: Người dùng có thể nhìn thấy và mở chức năng "Chỉnh sửa sự kiện".

#### AC-002 — Chặn quyền truy cập với người không phải Admin
- **Given**: Người dùng chưa đăng nhập hoặc không phải tài khoản Admin của sự kiện đó.
- **When**: Người dùng truy cập Dashboard hoặc tìm cách mở chức năng chỉnh sửa.
- **Then**: Hệ thống không cho phép mở chức năng Chỉnh sửa sự kiện.

#### AC-003 — Validation dữ liệu không đúng format hoặc quy tắc
- **Given**: Admin đang trong màn hình Chỉnh sửa sự kiện.
- **When**: Admin nhập dữ liệu không đúng định dạng hoặc vi phạm quy tắc nghiệp vụ.
- **Then**: Hệ thống hiển thị thông báo lỗi cụ thể tại trường vi phạm.

#### AC-004 — Vị trí hiển thị chức năng Chỉnh sửa sự kiện
- **Given**: Sự kiện đang hiển thị Dashboard.
- **When**: Kiểm tra giao diện Dashboard (khu vực chứa Heatmap).
- **Then**: Nút/chức năng "Chỉnh sửa sự kiện" chỉ được hiển thị tại Dashboard và chỉ hiển thị đối với tài khoản Admin của sự kiện đó.

#### AC-005 — Hiển thị dữ liệu hiện tại khi mở màn hình chỉnh sửa
- **Given**: Admin mở chức năng Chỉnh sửa sự kiện.
- **When**: Giao diện chỉnh sửa tải lên thành công.
- **Then**: Hệ thống hiển thị đầy đủ thông tin hiện tại của sự kiện và các trường có thể chỉnh sửa được điền sẵn (pre-filled) dữ liệu hiện tại.

#### AC-006 — Cập nhật các trường thông tin được phép chỉnh sửa
- **Given**: Admin đang ở form Chỉnh sửa sự kiện.
- **When**: Admin cập nhật các trường được phép: tên sự kiện, loại sự kiện, danh sách ngày bình chọn.
- **Then**: Hệ thống cho phép thay đổi các trường dữ liệu này.

#### AC-007 — Bắt buộc chọn lại ngày khi thay đổi loại sự kiện
- **Given**: Admin thay đổi Loại sự kiện (Event Type) trong form chỉnh sửa.
- **When**: Loại sự kiện được thay đổi.
- **Then**: Hệ thống bắt buộc Admin phải chọn lại danh sách ngày để bình chọn phù hợp với loại sự kiện mới.

#### AC-008 — Chặn lưu khi xóa dữ liệu của trường bắt buộc
- **Given**: Admin đang ở form Chỉnh sửa sự kiện.
- **When**: Admin xóa dữ liệu của trường hiện có (ví dụ: Tên sự kiện).
- **Then**: Hệ thống hiển thị thông báo yêu cầu nhập thông tin và không cho phép thực hiện hành động Lưu.

#### AC-009 — Lưu thành công và cập nhật hiển thị trên Dashboard
- **Given**: Toàn bộ thông tin chỉnh sửa đã hợp lệ.
- **When**: Admin nhấn "Lưu".
- **Then**: Hệ thống cập nhật thông tin sự kiện thành công và hiển thị dữ liệu mới cập nhật ngay trên Dashboard.

#### AC-010 — Hủy/Quay lại không lưu thay đổi
- **Given**: Admin đã sửa đổi một số thông tin trên form nhưng chưa nhấn Lưu.
- **When**: Admin chọn "Hủy" hoặc "Quay lại".
- **Then**: Hệ thống không lưu các thay đổi chưa được xác nhận và đưa người dùng quay về Dashboard sự kiện với dữ liệu gốc.

#### AC-011 — Giữ nguyên dữ liệu nhập khi xảy ra lỗi lưu
- **Given**: Admin nhấn Lưu với dữ liệu đã nhập.
- **When**: Xảy ra lỗi hệ thống hoặc đường truyền trong quá trình lưu/cập nhật.
- **Then**: Hệ thống hiển thị thông báo lỗi phù hợp và không xóa đi dữ liệu người dùng đang nhập trên form.

---

## Definition of Done (DoD)

- [ ] 1. Code chức năng chỉnh sửa sự kiện đã hoàn thành.
- [ ] 2. Đã implement validation cho các trường bắt buộc và quy tắc kiểm tra quyền Admin.
- [ ] 3. Đã xử lý trường hợp cập nhật thành công / thất bại, rollback nếu có lỗi.
- [ ] 4. Unit test đã được viết và pass toàn bộ.
- [ ] 5. Code đã được code review và phê duyệt.
- [ ] 6. QA đã test và pass toàn bộ 11 tiêu chí Acceptance Criteria.
- [ ] 7. Không còn bug Critical hoặc Blocker liên quan đến User Story.
- [ ] 8. Đã merge code vào branch theo quy định của team.
- [ ] 9. Đã deploy lên môi trường cần thiết (Staging/UAT).

---

## References

### Rules
- [`BR-01`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-01.md): Phân loại sự kiện (Dates and Times & Weekdays).
- [`BR-02`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-02.md): Danh sách ngày đối với sự kiện Dates and Times phải trong tương lai.
- [`BR-03`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-03.md): Mỗi sự kiện bắt buộc có $\ge 1$ ngày bình chọn.
- [`BR-04`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-04.md): ID sự kiện là duy nhất và bất biến sau khi tạo.
- [`BR-07`](file:///d:/VNZ/document-first-brief/meetly/BusinessRules/BR-07.md): Xóa bình chọn khi ngày bình chọn trong sự kiện bị xóa.

### Dependencies
- [`US-01`](file:///d:/VNZ/document-first-brief/meetly/UserStory/01-CreateSurveyEvent.md): Tạo sự kiện cần khảo sát.

---

## Notes

- Cần cảnh báo rõ ràng cho Admin khi xóa một ngày đã có người tham gia bình chọn (theo BR-07, dữ liệu bình chọn của ngày đó sẽ bị xóa).
