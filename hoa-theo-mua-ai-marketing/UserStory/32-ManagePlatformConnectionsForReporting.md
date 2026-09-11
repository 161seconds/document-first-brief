# STORY-032: Quản lý kết nối nền tảng để thu thập dữ liệu báo cáo

## Metadata

- **Story**: Là một Quản trị viên, tôi muốn kết nối hệ thống với các nền tảng (Facebook, Instagram, Zalo OA) để cấp quyền cho hệ thống tự động thu thập dữ liệu báo cáo.
- **Context**: Để hệ thống có thể lấy dữ liệu tự động từ các mạng xã hội, Quản trị viên cần thực hiện cấp quyền (Authorization) thông qua giao thức an toàn (OAuth 2.0 / API Key). Chức năng này giúp quản lý trạng thái liên kết và vòng đời của token kết nối.
- **Sprint**: S3
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 11/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Nguyễn Đức Bình
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**:
  - FE: Minh Nguyễn
- **Creator**: Hồ Hoàng Nam
- **Thống kê tài liệu**: Rules: 2 | Unit Tests: 0 | System Tests: 0

---

## Conditions

### Preconditions
- Quản trị viên đã đăng nhập vào hệ thống quản trị.
- Hệ thống đã được cấu hình sẵn các thông số môi trường (App ID, App Secret, Webhook URL, Callback URL) của các nền tảng Facebook, Instagram, Zalo OA hợp lệ.

### Trigger
- Admin truy cập màn hình "Quản lý kết nối" và chọn nút "Kết nối" tại một trong các nền tảng (Facebook, Instagram hoặc Zalo OA).

---

## Flow

### Main Flow — Kết nối nền tảng mạng xã hội thành công
1. Admin chọn "Kết nối" tại nền tảng mong muốn.
2. Hệ thống chuyển hướng Admin đến trang xác thực OAuth của nền tảng tương ứng.
3. Admin đăng nhập vào nền tảng nếu chưa đăng nhập và chấp nhận cấp các quyền truy cập dữ liệu được yêu cầu.
4. Nền tảng chuyển hướng trở lại hệ thống kèm theo Authorization Code.
5. Hệ thống gọi API của nền tảng để đổi Authorization Code lấy Access Token.
6. Hệ thống kiểm tra các quyền bắt buộc đã được cấp.
7. Hệ thống gọi API lấy thông tin định danh và danh sách Trang/OA mà tài khoản có quyền quản lý.
8. Hệ thống hiển thị thông báo "Kết nối [Tên nền tảng] thành công" và cập nhật trạng thái nền tảng thành "Đã kết nối".

### Alternative Flow

#### ALT-01 — Cập nhật lại kết nối đã hết hạn hoặc làm mới Token
- Admin chọn "Kết nối lại" tại một nền tảng có trạng thái "Đã hết hạn".
- Hệ thống thực hiện lại quy trình xác thực của nền tảng tương ứng.
- Admin hoàn thành xác thực và cấp đủ quyền bắt buộc.
- Hệ thống cập nhật thời gian hết hạn và trạng thái hiển thị thành "Đã kết nối".

#### ALT-02 — Admin chủ động ngắt kết nối nền tảng đang hoạt động
- Admin chọn "Ngắt kết nối" tại nền tảng đang ở trạng thái "Đã kết nối".
- Hệ thống hiển thị cửa sổ xác nhận ngắt kết nối.
- Admin chọn "Xác nhận".
- Hệ thống vô hiệu hóa Access Token, đổi trạng thái thành "Chưa kết nối" và ngừng các tiến trình thu thập dữ liệu của nền tảng này.
- Hệ thống hiển thị thông báo ngắt kết nối thành công.

### Exception Flow

#### EXC-02 — Lỗi kết nối API hoặc Timeout từ phía nền tảng
- Quá trình hệ thống gọi API đổi Authorization Code hoặc xác minh thông tin kết nối bị lỗi HTTP 5xx hoặc timeout.
- Hệ thống ghi log lỗi hệ thống.
- Hệ thống không lưu Access Token mới.
- Hệ thống hiển thị thông báo: *"Kết nối đến nền tảng đang bị gián đoạn. Vui lòng thử lại sau."*
- Trạng thái kết nối của nền tảng giữ nguyên như trước khi thao tác.

#### EXC-05 — Authorization Code không hợp lệ hoặc không thể đổi lấy Token
- Hệ thống nhận Authorization Code nhưng nền tảng từ chối đổi Code lấy Token.
- Hệ thống không lưu Access Token.
- Hệ thống ghi log lỗi.
- Hệ thống giữ nguyên trạng thái kết nối cũ.
- Hệ thống hiển thị thông báo yêu cầu Admin thực hiện lại thao tác kết nối.

#### EXC-09 — Phiên đăng nhập của Admin hết hạn trong quá trình kết nối
- Admin bắt đầu thao tác kết nối khi phiên đăng nhập còn hợp lệ nhưng phiên hết hạn trước khi callback được xử lý hoàn tất.
- Hệ thống không lưu Access Token mới.
- Hệ thống yêu cầu Admin đăng nhập lại.
- Hệ thống giữ nguyên trạng thái kết nối trước thao tác.

---

## Acceptance Criteria

- **AC-001 — Hiển thị danh sách và trạng thái kết nối các nền tảng**:
  - **Given**: Admin truy cập màn hình Quản lý kết nối.
  - **When**: Màn hình được tải hoàn tất.
  - **Then**: Hệ thống hiển thị danh sách các nền tảng hỗ trợ gồm Facebook, Instagram và Zalo OA.
  - **And**: Hiển thị đúng trạng thái kết nối hiện tại của từng nền tảng gồm "Chưa kết nối", "Đã kết nối" hoặc "Đã hết hạn".

- **AC-002 — Chuyển hướng xác thực OAuth nền tảng**:
  - **Given**: Một nền tảng đang ở trạng thái "Chưa kết nối".
  - **When**: Admin chọn "Kết nối".
  - **Then**: Hệ thống chuyển hướng Admin đến trang xác thực của đúng nền tảng tương ứng.

- **AC-003 — Kết nối thành công khi cấp đủ quyền**:
  - **Given**: Admin đã hoàn thành xác thực trên nền tảng.
  - **When**: Hệ thống nhận Authorization Code hợp lệ, đổi Token thành công.
  - **Then**: Trạng thái nền tảng cập nhật thành "Đã kết nối".
  - **And**: Hệ thống hiển thị thông báo "Kết nối thành công".

- **AC-004 — Từ chối kết nối khi không cấp đủ quyền**:
  - **Given**: Admin đang ở trang xác thực của nền tảng.
  - **When**: Admin bấm Hủy hoặc không cấp đủ các quyền bắt buộc.
  - **Then**: Hệ thống từ chối lưu Access Token mới.
  - **And**: Hiển thị thông báo "Kết nối thất bại. Vui lòng cấp đủ quyền yêu cầu".
  - **And**: Giữ nguyên trạng thái kết nối cũ của nền tảng.

- **AC-008 — Ràng buộc quyền quản lý Trang/OA**:
  - **Given**: Hệ thống nhận được Access Token hợp lệ.
  - **When**: Hệ thống gọi API lấy danh sách Trang/OA quản lý.
  - **Then**: Hệ thống chỉ hoàn tất kết nối nếu tồn tại ít nhất một Trang/OA mà tài khoản có quyền quản lý.

- **AC-010 — Kết nối lại nền tảng đã hết hạn**:
  - **Given**: Một nền tảng đang ở trạng thái "Đã hết hạn".
  - **When**: Admin chọn "Kết nối lại" và xác thực thành công.
  - **Then**: Cập nhật thời gian hết hạn mới và trạng thái thành "Đã kết nối".

- **AC-011 — Tự động cập nhật trạng thái khi Token hết hạn**:
  - **Given**: Access Token hiện tại đã hết hạn hoặc không còn hợp lệ.
  - **When**: Hệ thống phát hiện trạng thái Token không còn hợp lệ.
  - **Then**: Hệ thống cập nhật trạng thái nền tảng thành "Đã hết hạn".
  - **And**: Không tiếp tục sử dụng Token này cho việc thu thập dữ liệu.
  - **And**: Hiển thị hành động "Kết nối lại".

- **AC-012 — Ngắt kết nối nền tảng chủ động**:
  - **Given**: Admin thao tác ngắt kết nối một nền tảng đang ở trạng thái "Đã kết nối".
  - **When**: Admin xác nhận ngắt kết nối.
  - **Then**: Hệ thống xóa hoặc vô hiệu hóa Token định danh trong cơ sở dữ liệu.
  - **And**: Trạng thái nền tảng chuyển thành "Chưa kết nối".
  - **And**: Hệ thống ngừng các tiến trình thu thập dữ liệu sử dụng kết nối này.
  - **And**: Hệ thống hiển thị thông báo ngắt kết nối thành công.

- **AC-013 — Hủy thao tác ngắt kết nối**:
  - **Given**: Admin đã mở cửa sổ xác nhận ngắt kết nối.
  - **When**: Admin chọn "Hủy".
  - **Then**: Hệ thống đóng cửa sổ xác nhận và không thay đổi Token.
  - **And**: Giữ nguyên trạng thái kết nối hiện tại.

- **AC-014 — Xử lý lỗi hệ thống hoặc timeout từ API nền tảng**:
  - **Given**: Hệ thống đang gọi API nền tảng trong quá trình kết nối.
  - **When**: API trả lỗi HTTP 5xx hoặc timeout.
  - **Then**: Hệ thống ghi log lỗi và không lưu Access Token mới.
  - **And**: Giữ nguyên trạng thái kết nối trước thao tác.
  - **And**: Hiển thị thông báo "Kết nối đến nền tảng đang bị gián đoạn. Vui lòng thử lại sau."

---

## References

### Rules
- [BR-075: Điều kiện nền tảng được thu thập báo cáo](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-075.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/02a51e91-1297-48e7-851a-7aeed98a73ba))
- [BR-076: Thu thập báo cáo độc lập theo từng nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-076.md) ([Link gốc](https://document-first.vnzdna.com/projects/11303185-e537-4531-bf3f-a90af664ff77/documents/2d88ab0a-7bad-4790-b5fc-69e45531f229))

### Dependencies
- Phụ thuộc vào hệ thống App đã được đăng ký và duyệt quyền tại trang Developer của Facebook, Zalo.

---

## Non-Functional

- Access Token và Secret Key phải được mã hóa (Encrypt) an toàn trước khi lưu vào Cơ sở dữ liệu.
- URL Callback/Redirect tích hợp OAuth bắt buộc phải sử dụng giao thức HTTPS.
- Access Token và Secret Key không được ghi ở dạng rõ vào application log.

---

## Out of Scope

- Việc cài đặt lịch đồng bộ (Cronjob) và xử lý logic tải dữ liệu thực tế không thuộc phạm vi Story này.
