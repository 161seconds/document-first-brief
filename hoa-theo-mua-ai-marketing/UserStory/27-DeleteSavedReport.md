# STORY-027: Xóa báo cáo đã lưu

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn xóa báo cáo đã lưu không còn cần thiết để danh sách báo cáo được quản lý gọn gàng và tránh sử dụng nhầm dữ liệu báo cáo cũ.
- **Context**: 
  - Hệ thống lưu các báo cáo được tạo từ dữ liệu đã thu thập riêng theo từng nền tảng như Facebook, Instagram và Zalo Official Account (Zalo OA) (sinh tự động từ STORY-025).
  - Chức năng này cho phép Quản trị viên xóa một báo cáo đã lưu không còn cần thiết khỏi hệ thống.
  - **Điều kiện cho phép xóa báo cáo**:
    - Báo cáo phải còn tồn tại trong cơ sở dữ liệu hệ thống tại thời điểm kiểm tra.
    - Báo cáo **tuyệt đối không được** báo cáo tổng hợp (STORY-026) hoặc bất kỳ dữ liệu nghiệp vụ nào khác tham chiếu.
    - Quản trị viên phải có quyền quản trị/quản lý báo cáo hợp lệ và phiên làm việc còn hiệu lực.
  - **Cơ chế xóa toàn vẹn (Transactional / Atomic Deletion)**:
    - Khi xóa thành công, hệ thống xóa báo cáo và dữ liệu chỉ thuộc báo cáo đó trong cùng một giao dịch cơ sở dữ liệu (Database Transaction). Nếu xảy ra bất kỳ lỗi kỹ thuật nào, toàn bộ thao tác được rollback và giữ nguyên vẹn dữ liệu cũ.
  - **Nhật ký kiểm toán (Audit Logging)**:
    - Hệ thống lưu nhật ký xóa độc lập với bảng dữ liệu báo cáo, ghi nhận đầy đủ: Mã định danh Quản trị viên thực hiện, ID báo cáo đã xóa, thời gian thao tác, kết quả thao tác (Thành công/Thất bại) và lý do chi tiết nếu thất bại.
  - **Bảo toàn dữ liệu bên ngoài và dữ liệu dùng chung**:
    - Việc xóa chỉ diễn ra nội bộ trong hệ thống, hoàn toàn không làm thay đổi hoặc xóa dữ liệu trên các nền tảng mạng xã hội Facebook, Instagram, Zalo OA hoặc bên ngoài.
    - Tuyệt đối không xóa hoặc ngắt kết nối nền tảng (Token/Credential), không xóa dữ liệu thu thập dùng chung và không ảnh hưởng đến bất kỳ báo cáo độc lập nào khác.
    - Không tự động xóa báo cáo tổng hợp hoặc dữ liệu đang tham chiếu báo cáo.
    - Thao tác xóa là vĩnh viễn và không thể khôi phục sau khi đã xác nhận thành công.
- **Sprint**: S3
- **Priority**: Must
- **Phiên bản**: v0.1
- **Phê duyệt tài liệu**: Đang duyệt
- **Cập nhật**: 09/09/2026
- **Author**: Hồ Hoàng Nam
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Nguyễn Đức Bình
- **Owner**: Hồ Hoàng Nam
- **Status**: Cần làm
- **Assignee**: FE: Hồ Hoàng Nam | BE: Nguyễn Anh Quân
- **Creator**: Hồ Hoàng Nam
- **Unit Tests**: 0
- **System Tests**: 12

---

## Conditions
- **Preconditions**:
  - Quản trị viên đã đăng nhập vào hệ thống và tài khoản có quyền quản lý/xóa báo cáo.
  - Báo cáo cần xóa đang tồn tại trong hệ thống.
  - Báo cáo không được báo cáo tổng hợp hoặc bất kỳ dữ liệu nghiệp vụ nào khác tham chiếu.
- **Trigger**:
  - Quản trị viên chọn nút “Xóa” tại một dòng báo cáo trên danh sách báo cáo hoặc tại màn hình xem chi tiết báo cáo.

---

## Flow

### Main Flow — Xóa báo cáo đã lưu thành công (Happy Case)
1. Quản trị viên chọn “Xóa” tại một báo cáo trong danh sách báo cáo đã lưu (hoặc từ màn hình chi tiết báo cáo).
2. Hệ thống kiểm tra quyền hạn của Quản trị viên, kiểm tra sự tồn tại và tình trạng tham chiếu hiện tại của báo cáo trong cơ sở dữ liệu.
3. Khi báo cáo đủ điều kiện xóa (tồn tại và không bị tham chiếu), hệ thống hiển thị cửa sổ (popup/modal) xác nhận xóa gồm: Tên nền tảng (Facebook, Instagram, Zalo OA), thời điểm tạo báo cáo, thời điểm thu thập dữ liệu và thông điệp cảnh báo: *"Thao tác này không thể hoàn tác. Bạn có chắc chắn muốn xóa báo cáo này?"*.
4. Quản trị viên chọn nút “Xóa” trên cửa sổ xác nhận để đồng ý xóa.
5. Hệ thống thực hiện kiểm tra lại sự tồn tại và ràng buộc tham chiếu của báo cáo lần hai (Double-check trước khi xóa vật lý).
6. Hệ thống thực hiện xóa bản ghi báo cáo cùng toàn bộ dữ liệu chi tiết chỉ thuộc báo cáo đó trong cùng một giao dịch cơ sở dữ liệu (Database Transaction).
7. Hệ thống bảo toàn nguyên vẹn dữ liệu thu thập dùng chung, cấu hình kết nối và các báo cáo khác trong hệ thống.
8. Hệ thống ghi nhận nhật ký kiểm toán (Audit Log) lưu lại: ID Quản trị viên, ID báo cáo, thời gian thực hiện và kết quả "Thành công".
9. Hệ thống đóng cửa sổ xác nhận và hiển thị thông báo: *"Xóa báo cáo thành công"*.
10. Báo cáo không còn xuất hiện trong danh sách báo cáo hiển thị trên giao diện.

### Alternative Flow
- **ALT-01 — Quản trị viên hủy xóa**:
  - Khi cửa sổ xác nhận đang hiển thị tại bước 3, Quản trị viên chọn “Hủy” hoặc click ra ngoài cửa sổ xác nhận.
  - Hệ thống đóng cửa sổ xác nhận và không gửi yêu cầu xóa đến máy chủ.
  - Bản ghi báo cáo và toàn bộ dữ liệu liên quan được giữ nguyên trạng.
- **ALT-02 — Xóa từ màn hình chi tiết báo cáo**:
  - Quản trị viên chọn “Xóa” khi đang xem màn hình chi tiết của một báo cáo.
  - Hệ thống thực hiện các bước kiểm tra tính hợp lệ và hiển thị cửa sổ xác nhận tương tự như khi xóa từ danh sách.
  - Sau khi xóa thành công và thông báo, hệ thống tự động điều hướng Quản trị viên quay trở lại danh sách báo cáo đã lưu và cập nhật danh sách mới nhất.

### Exception Flow
- **EXC-01 — Báo cáo không còn tồn tại**:
  - Tại thời điểm Quản trị viên bấm xóa hoặc khi hệ thống kiểm tra, bản ghi báo cáo đã bị xóa trước đó bởi người dùng khác.
  - Hệ thống dừng xử lý, không ghi nhận xóa thành công.
  - Hệ thống hiển thị thông báo: *"Báo cáo không còn tồn tại."* và tự động tải lại danh sách báo cáo hiện tại.
- **EXC-02 — Báo cáo đang được báo cáo tổng hợp hoặc dữ liệu nghiệp vụ khác tham chiếu**:
  - Tại bước 2 hoặc bước 5, hệ thống phát hiện báo cáo đang được sử dụng trong ít nhất một báo cáo tổng hợp (STORY-026) hoặc luồng nghiệp vụ khác.
  - Hệ thống từ chối yêu cầu xóa và giữ nguyên vẹn dữ liệu báo cáo.
  - Hệ thống hiển thị thông báo lỗi chi tiết: *"Không thể xóa báo cáo do đang được sử dụng trong báo cáo tổng hợp [Tên/Mã báo cáo tổng hợp]. Vui lòng gỡ bỏ liên kết trước khi thực hiện xóa."*.
- **EXC-03 — Không thể xóa báo cáo do lỗi hệ thống (Lỗi CSDL / Transaction)**:
  - Trong quá trình xóa bản ghi báo cáo hoặc dữ liệu phụ thuộc, hệ thống gặp sự cố kết nối CSDL hoặc lỗi máy chủ.
  - Hệ thống hoàn tác (rollback) toàn bộ giao dịch, không xóa một phần dữ liệu, giữ nguyên vẹn dữ liệu cũ và không thông báo thành công.
  - Hệ thống ghi nhật ký xóa thất bại kèm nguyên nhân lỗi kỹ thuật và hiển thị thông báo: *"Không thể xóa báo cáo, vui lòng thử lại sau"*.
- **EXC-04 — Báo cáo phát sinh liên kết tham chiếu mới ngay trước khi xác nhận**:
  - Khi cửa sổ xác nhận đang mở, một tiến trình khác tạo báo cáo tổng hợp có tham chiếu đến báo cáo này.
  - Quản trị viên nhấn "Xóa" trên cửa sổ xác nhận -> Bước 5 (Double-check) phát hiện ràng buộc tham chiếu mới phát sinh.
  - Hệ thống từ chối xóa, đóng cửa sổ xác nhận, hiển thị cảnh báo ràng buộc mới và làm mới lại trạng thái dữ liệu.
- **EXC-05 — Phiên đăng nhập hết hạn**:
  - Phiên làm việc của Quản trị viên hết hiệu lực trước khi gửi yêu cầu hoặc tại thời điểm nhấn xác nhận xóa.
  - Hệ thống từ chối yêu cầu xóa, giữ nguyên trạng báo cáo trên cơ sở dữ liệu và yêu cầu Quản trị viên đăng nhập lại.
- **EXC-06 — Quản trị viên không có quyền xóa báo cáo**:
  - Tài khoản Quản trị viên không được cấp quyền quản lý hoặc xóa dữ liệu báo cáo.
  - Hệ thống từ chối thao tác, thông báo lỗi: *"Bạn không có quyền thực hiện xóa báo cáo này"* và giữ nguyên dữ liệu.
- **EXC-07 — Báo cáo đã được xóa đồng thời (Concurrent Delete)**:
  - Hai Quản trị viên cùng mở cửa sổ xác nhận cho một báo cáo; Quản trị viên thứ nhất đã xác nhận xóa thành công trước đó.
  - Khi Quản trị viên thứ hai chọn "Xóa", hệ thống kiểm tra tại bước 5 phát hiện báo cáo đã bị xóa.
  - Hệ thống không thực hiện xóa lần thứ hai, không ghi trùng nhật ký thành công, hiển thị thông báo: *"Báo cáo không còn tồn tại."* và làm mới lại danh sách báo cáo.

---

## Acceptance Criteria

- **AC-001 — Hiển thị cửa sổ xác nhận xóa báo cáo đủ điều kiện**:
  - **Given**: Báo cáo tồn tại trong hệ thống, không bị báo cáo tổng hợp hoặc dữ liệu khác tham chiếu, và Quản trị viên có quyền xóa.
  - **When**: Quản trị viên chọn “Xóa” tại báo cáo đó.
  - **Then**: Hệ thống hiển thị cửa sổ xác nhận bao gồm: Tên nền tảng, thời điểm tạo báo cáo, thời điểm thu thập dữ liệu và cảnh báo thao tác không thể hoàn tác.
  - **And**: Báo cáo chưa bị xóa và hệ thống chưa gửi lệnh xóa dữ liệu đến máy chủ.

- **AC-002 — Quản trị viên hủy thao tác xóa (ALT-01)**:
  - **Given**: Cửa sổ xác nhận xóa báo cáo đang hiển thị.
  - **When**: Quản trị viên chọn “Hủy” hoặc đóng cửa sổ xác nhận.
  - **Then**: Hệ thống đóng cửa sổ xác nhận, không gửi yêu cầu xóa đến máy chủ.
  - **And**: Báo cáo và toàn bộ dữ liệu liên quan được giữ nguyên vẹn.

- **AC-003 — Xóa báo cáo thành công trong một giao dịch (Happy Case)**:
  - **Given**: Báo cáo đủ điều kiện xóa và Quản trị viên đã bấm “Xóa” trên cửa sổ xác nhận.
  - **When**: Hệ thống xử lý yêu cầu xóa.
  - **Then**: Hệ thống xóa bản ghi báo cáo cùng toàn bộ dữ liệu chỉ thuộc báo cáo đó trong cùng một giao dịch cơ sở dữ liệu (Transaction).
  - **And**: Hệ thống bảo toàn nguyên vẹn dữ liệu thu thập dùng chung, các báo cáo khác và thông tin kết nối nền tảng.
  - **And**: Hệ thống ghi nhận nhật ký kiểm toán và hiển thị thông báo: *"Xóa báo cáo thành công"*.
  - **And**: Báo cáo được loại bỏ khỏi danh sách hiển thị.

- **AC-004 — Báo cáo không còn tồn tại tại thời điểm kiểm tra (EXC-01)**:
  - **Given**: Báo cáo không còn tồn tại trong cơ sở dữ liệu tại thời điểm kiểm tra.
  - **When**: Quản trị viên thực hiện thao tác xóa hoặc xác nhận xóa.
  - **Then**: Hệ thống không gửi lệnh xóa lần nữa, không ghi nhận xóa thành công.
  - **And**: Hệ thống hiển thị thông báo: *"Báo cáo không còn tồn tại."* và tự động tải lại danh sách báo cáo hiện tại.

- **AC-005 — Từ chối xóa khi báo cáo đang được tham chiếu (EXC-02)**:
  - **Given**: Báo cáo đang được ít nhất một báo cáo tổng hợp hoặc dữ liệu nghiệp vụ khác tham chiếu.
  - **When**: Quản trị viên chọn "Xóa" hoặc gửi yêu cầu xóa báo cáo đó.
  - **Then**: Hệ thống từ chối thực hiện xóa, giữ nguyên trạng báo cáo và mọi dữ liệu liên quan.
  - **And**: Hệ thống hiển thị thông báo lỗi rõ ràng nêu rõ báo cáo đang được sử dụng và yêu cầu gỡ liên kết trước.

- **AC-006 — Đảm bảo tính nguyên tử khi có lỗi xóa báo cáo hoặc dữ liệu (EXC-03)**:
  - **Given**: Báo cáo đủ điều kiện xóa và Quản trị viên đã xác nhận xóa.
  - **When**: Xảy ra lỗi kỹ thuật trong quá trình xóa bản ghi báo cáo hoặc dữ liệu thuộc báo cáo.
  - **Then**: Hệ thống rollback toàn bộ giao dịch, giữ nguyên trạng toàn bộ dữ liệu cũ và không thông báo xóa thành công.
  - **And**: Hệ thống ghi nhật ký xóa thất bại kèm lý do và thông báo Quản trị viên thử lại sau.

- **AC-007 — Kiểm tra lại sự tồn tại và tính hợp lệ tại thời điểm xác nhận (Double-check)**:
  - **Given**: Cửa sổ xác nhận xóa đang hiển thị trên giao diện.
  - **When**: Quản trị viên nhấn nút “Xóa” để xác nhận.
  - **Then**: Hệ thống bắt buộc phải kiểm tra lại sự tồn tại, quyền hạn và trạng thái tham chiếu của báo cáo tại Backend trước khi thực thi lệnh xóa vật lý; chỉ thực hiện xóa khi dữ liệu hoàn toàn hợp lệ.

- **AC-008 — Bảo vệ toàn vẹn dữ liệu mạng xã hội và kết nối ngoài**:
  - **Given**: Quản trị viên thực hiện xóa báo cáo nội bộ trong hệ thống.
  - **When**: Yêu cầu xóa được xử lý và ghi nhận thành công.
  - **Then**: Hệ thống tuyệt đối không gửi bất kỳ yêu cầu xóa hoặc thay đổi nào đến API của các nền tảng mạng xã hội bên ngoài (Facebook, Instagram, Zalo OA).
  - **And**: Thông tin kết nối nền tảng (Token/Credential) và dữ liệu gốc trên các kênh mạng xã hội được bảo toàn nguyên trạng.

- **AC-009 — Xóa báo cáo từ màn hình chi tiết (ALT-02)**:
  - **Given**: Quản trị viên đang ở màn hình xem chi tiết của một báo cáo đủ điều kiện xóa.
  - **When**: Quản trị viên chọn "Xóa", xác nhận xóa trên cửa sổ xác nhận và hệ thống xử lý thành công.
  - **Then**: Hệ thống xóa báo cáo thành công, hiển thị thông báo: *"Xóa báo cáo thành công"* và tự động điều hướng Quản trị viên quay trở lại màn hình danh sách báo cáo.

- **AC-010 — Xử lý phiên đăng nhập hết hạn khi xác nhận xóa (EXC-05)**:
  - **Given**: Cửa sổ xác nhận xóa đang mở trên màn hình.
  - **When**: Phiên đăng nhập của Quản trị viên hết hạn và Quản trị viên nhấn nút xác nhận xóa.
  - **Then**: Hệ thống từ chối yêu cầu xóa, giữ nguyên trạng dữ liệu trên CSDL, hiển thị thông báo yêu cầu đăng nhập lại và chuyển hướng tới màn hình đăng nhập.

- **AC-011 — Xử lý xung đột khi xóa đồng thời (EXC-07)**:
  - **Given**: Hai Quản trị viên cùng mở cửa sổ xác nhận cho cùng một bản ghi báo cáo.
  - **When**: Một người đã xác nhận xóa trước thành công và người còn lại xác nhận xóa sau.
  - **Then**: Hệ thống phát hiện báo cáo đã bị xóa, không thực hiện xóa lần thứ hai, không ghi trùng nhật ký thành công.
  - **And**: Hệ thống thông báo: *"Báo cáo không còn tồn tại."* và tự động tải lại danh sách báo cáo cho người dùng thứ hai.

- **AC-012 — Lưu trữ nhật ký kiểm toán xóa độc lập (Audit Log)**:
  - **Given**: Một yêu cầu xóa báo cáo được thực thi (dù thành công hay thất bại).
  - **When**: Quá trình xử lý xóa kết thúc.
  - **Then**: Hệ thống ghi một bản ghi nhật ký kiểm toán vào bảng lưu trữ độc lập với bảng báo cáo, chứa đầy đủ: ID Quản trị viên thực hiện, ID báo cáo, thời gian thực thi, kết quả ("Thành công" hoặc "Thất bại") và mã/thông điệp lỗi nếu có.

---

## References

### Business Rules
- [BR-077: Chỉ tạo báo cáo từ dữ liệu đầy đủ](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-077.md)
- [BR-078: Thông tin bắt buộc của báo cáo nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-078.md)
- [BR-079: Định dạng tệp báo cáo tải xuống](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-079.md)
- [BR-080: Ràng buộc dữ liệu khi xóa báo cáo](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-080.md)
- [BR-081: Xóa toàn vẹn báo cáo và ghi nhật ký độc lập](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/BusinessRules/BR-081.md)

### User Stories liên quan
- [STORY-025: Thu thập và quản lý báo cáo từ các nền tảng](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/25-CollectAndManagePlatformReports.md) — Nguồn sinh và quản lý các báo cáo đã lưu.
- STORY-026: Tạo báo cáo tổng hợp định kỳ — Báo cáo tổng hợp có thể tham chiếu các báo cáo đơn lẻ này.

---

## Non-Functional
- **Kiểm tra đa tầng phía Backend**: Hệ thống phải kiểm tra lại quyền của Quản trị viên, sự tồn tại và tình trạng tham chiếu của báo cáo tại thời điểm máy chủ nhận lệnh xác nhận xóa (không phụ thuộc vào kiểm tra tại Frontend).
- **Tính toàn vẹn giao dịch (ACID)**: Báo cáo và toàn bộ dữ liệu chỉ thuộc báo cáo đó phải được xóa trong cùng một Database Transaction; nếu phát sinh bất kỳ lỗi nào thì toàn bộ dữ liệu cũ được giữ nguyên vẹn qua cơ chế Rollback tự động.
- **Lưu trữ kiểm toán độc lập (Audit Trail)**: Bảng nhật ký xóa phải được thiết kế và lưu trữ độc lập với bảng báo cáo, đảm bảo bản ghi nhật ký không bị xóa theo khi báo cáo bị xóa và lưu trữ tối thiểu: ID Quản trị viên, ID báo cáo, thời điểm thực hiện, kết quả và lý do lỗi nếu thất bại.

---

## Out of Scope
- Không bao gồm tạo mới, xem chi tiết, chỉnh sửa hoặc tải xuống báo cáo (thuộc STORY-025 và STORY-026).
- Không bao gồm xóa hoặc thay đổi bất kỳ dữ liệu nào trên các nền tảng bên ngoài (Facebook, Instagram, Zalo OA).
- Không bao gồm xóa cấu hình kết nối nền tảng (Credentials/Tokens), dữ liệu thu thập dùng chung hoặc các báo cáo khác trong hệ thống.
- Không tự động xóa báo cáo tổng hợp hoặc dữ liệu đang tham chiếu báo cáo (phải xóa/gỡ tham chiếu thủ công trước).
- Không hỗ trợ khôi phục báo cáo sau khi đã thực hiện xóa thành công.
