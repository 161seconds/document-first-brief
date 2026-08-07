# Dự án Hoa Theo Mùa - Tài liệu Đặc tả Hệ thống (Document First Brief)

Chào mừng bạn đến với kho lưu trữ tài liệu đặc tả yêu cầu (User Stories) và thiết kế kỹ thuật cho hệ thống quản lý **Hoa Theo Mùa**. 

Dự án này sử dụng phương pháp tiếp cận tài liệu đi trước (Document-First), giúp làm rõ các yêu cầu, quy trình nghiệp vụ và thiết kế hệ thống trước khi tiến hành phát triển phần mềm.

## Cấu trúc thư mục và Tài liệu

Toàn bộ các tài liệu hiện tại được lưu trữ trong thư mục `hoatheomua/`. Dưới đây là các User Story đã được chốt và đưa vào đặc tả cập nhật nhất:

### 1. Phân hệ Quản lý Vật liệu (Materials Management)
* `02-ViewSearchMaterialsList.md`: Xem và tìm kiếm danh sách vật liệu.

### 2. Phân hệ Quản lý Công thức Combo (Combo Formula Management)
* `06-ViewComboFormula.md`: Xem chi tiết cấu thành/công thức combo.
* `07-AddMaterialsToComboFormula.md`: Thêm vật liệu vào công thức combo.
* `08-UpdateMaterialsInComboFormula.md`: Sửa định lượng/vai trò vật liệu trong công thức.
* `09-RemoveMaterialsFromComboFormula.md`: Xóa vật liệu khỏi công thức.

### 3. Phân hệ Quản lý Tồn kho & Bán hàng (Inventory & Order Validation)
* `12-InventoryAndSellabilityAlerts.md`: Cảnh báo vật liệu sắp hết và combo hết khả năng bán.
* `13-ViewComboSellableQuantity.md`: Xem khả năng bán của combo hoa.
* `14-DisplayComboAvailabilityStatus.md`: Hiển thị trạng thái còn hàng của combo hoa.
* `15-ValidateSellableQuantityBeforeOrder.md`: Kiểm tra số lượng có thể bán trước khi tạo đơn.

### 4. Thiết kế Hệ thống
* `Design-US002-US006.md`: Tài liệu kỹ thuật, chứa Sequence Diagram, ERD và các kịch bản TDD cho các tính năng liên quan.

---

## Cách đọc và Sử dụng tài liệu (Cấu trúc chuẩn của một User Story)

Mỗi tập tin `.md` tương ứng với một **User Story** và được cấu trúc theo một form chuẩn để tất cả các bên (BA, Dev, QA) dễ dàng đối chiếu.

### 1. METADATA
Là các thông tin cơ bản để quản lý tiến độ và phân công công việc:
- **Story**: Một câu duy nhất mô tả nhu cầu theo format "Là <vai trò>, tôi muốn <hành động>, để <giá trị nhận được>". Là câu chốt để cả team nhìn về cùng một mục tiêu.
- **Context**: Ngữ cảnh nghiệp vụ giúp người đọc (Dev/QA mới) hiểu tại sao tính năng này tồn tại, các ràng buộc và đối tượng nghiệp vụ là gì.
- **Sprint**: Mã sprint (VD: S1, S2...) mà Story được cam kết hoàn thành.
- **Priority**:
  - **Must**: Bắt buộc phải xong. Nhằm vào những gì cốt lõi nhất.
  - **Should**: Quan trọng nhưng có cách xoay xở tạm. Cần làm khi sprint còn trống thời gian.
  - **Could**: Có thì tốt, không có cũng không ảnh hưởng mục tiêu.
  - **Won't**: Quyết định thống nhất KHÔNG làm trong đợt này.
- **Assignee**: Người chịu trách nhiệm thực thi theo từng vai trò (BE, FE, QA...).
- **Creator**: Người viết ra Story và chịu trách nhiệm giải thích nội dung của nó.
- **Status of User Story**:
  - **Cần làm**: Đã duyệt nội dung, đủ điều kiện bắt đầu.
  - **Đang làm**: Đang code, design, hoặc test.
  - **Bị chặn**: Không thể tiếp tục do nằm ngoài tầm kiểm soát (chờ API, chờ nghiệp vụ).
  - **Xong**: Đã pass toàn bộ Acceptance Criteria và được QA xác nhận.

### 2. CONDITIONS (Điều kiện)
- **Preconditions**: Các điều kiện tiền quyết định phải thỏa mãn trước khi thực hiện tính năng.
- **Trigger**: Hành động từ phía người dùng kích hoạt luồng xử lý.

### 3. FLOW (Luồng xử lý)
Liệt kê từng bước theo thứ tự (Step-by-step):
- **Main Flow**: Kịch bản thành công mặc định.
- **Alternative Flow**: Các rẽ nhánh thay thế (cũng dẫn đến thành công nhưng theo cách khác).
- **Exception Flow**: Các luồng báo lỗi, chặn nghiệp vụ khi có dữ liệu không hợp lệ.

### 4. ACCEPTANCE CRITERIA (Tiêu chí nghiệm thu)
- Các kịch bản viết theo cấu trúc BDD: **Given - When - Then - And**.
- Đây là công cụ đo lường duy nhất. Nếu tất cả các AC được thỏa mãn, Story mới được tính là **Xong**.

### 5. NON-FUNCTIONAL & OUT OF SCOPE
- **Non-Functional**: Yêu cầu về hiệu năng (thời gian phản hồi), bảo mật, hay khả năng chịu tải.
- **Out of Scope**: Ghi chú rõ những gì KHÔNG thuộc phạm vi thực hiện của Story này để tránh scope creep (phình to yêu cầu).
