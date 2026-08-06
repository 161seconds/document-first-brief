# Dự án Hoa Theo Mùa - Tài liệu Đặc tả Hệ thống (Document First Brief)

Chào mừng bạn đến với kho lưu trữ tài liệu đặc tả yêu cầu (User Stories) và logic nghiệp vụ cho hệ thống quản lý **Hoa Theo Mùa**. 

Dự án này sử dụng phương pháp tiếp cận tài liệu đi trước (Document-First), giúp làm rõ các yêu cầu, quy trình nghiệp vụ và các tiêu chí chấp nhận (Acceptance Criteria) trước khi tiến hành phát triển phần mềm.

## Cấu trúc thư mục và Tài liệu

Toàn bộ các tài liệu hiện tại được lưu trữ trong thư mục `hoatheomua/`, tập trung vào các phân hệ cốt lõi của hệ thống quản lý cửa hàng hoa:

### 1. Phân hệ Quản lý Vật liệu (Materials Management)
Bao gồm các đặc tả liên quan đến việc định nghĩa, tìm kiếm và quản lý vòng đời của vật liệu (hoa, lá, phụ kiện,...):
* `02-ViewSearchMaterialsList.md`: Xem và tìm kiếm danh sách vật liệu.
* `03-CreateNewMaterial.md`: Tạo mới vật liệu.
* `04-UpdateMaterialsInformation.md`: Cập nhật thông tin vật liệu.
* `05-DiscontinueOrDeleteMaterials.md`: Ngừng kinh doanh hoặc xóa vật liệu.

### 2. Phân hệ Quản lý Công thức Combo (Combo Formula Management)
Định nghĩa cách thức cấu thành các sản phẩm combo (bó hoa, lẵng hoa) từ các vật liệu đã có:
* `06-ViewComboFormula.md`: Xem chi tiết cấu thành/công thức combo.
* `07-AddMaterialsToComboFormula.md`: Thêm vật liệu mới vào công thức combo.
* `08-UpdateMaterialsInComboFormula.md`: Thay đổi số lượng/thuộc tính vật liệu trong công thức.
* `09-RemoveMaterialsFromComboFormula.md`: Xóa vật liệu khỏi công thức.
* `17-CopyComboFormula.md`: Nhân bản (sao chép) công thức combo để tạo sản phẩm mới nhanh chóng.

### 3. Phân hệ Quản lý Tồn kho & Bán hàng (Inventory & Order Validation)
Hệ thống logic phức tạp giúp tính toán, kiểm tra và hiển thị khả năng cung ứng dựa trên tồn kho của từng vật liệu cấu thành:
* `12-InventoryAndSellabilityAlerts.md`: Cảnh báo mức tồn kho thấp và khả năng bán.
* `13-ViewComboSellableQuantity.md`: Tính toán và xem số lượng có thể bán tối đa của một combo.
* `14-DisplayComboAvailabilityStatus.md`: Hiển thị trạng thái khả dụng (còn hàng/hết hàng) của combo.
* `15-ValidateSellableQuantityBeforeOrder.md`: Xác thực khả năng cung ứng (tồn kho) trước khi cho phép đặt hàng.
* `16-ManageMaterialsReservationAndInventoryLifecycle.md`: Quản lý vòng đời tồn kho và cơ chế "giữ chỗ" (Reservation) khi có đơn hàng mới.
* `18-AdjustMaterialsInventory.md`: Điều chỉnh tồn kho vật liệu (Kiểm kho/Cân bằng kho).
* `19-ViewMaterialsInventoryHistory.md`: Xem lịch sử giao dịch (xuất/nhập/điều chỉnh) của tồn kho vật liệu.

## Cách đọc và Sử dụng tài liệu

Mỗi tập tin `.md` tương ứng với một **User Story** (Câu chuyện người dùng) và được cấu trúc theo một chuẩn chung để dễ theo dõi:
- **Overview & Objective**: Tổng quan và mục tiêu của tính năng.
- **Preconditions**: Các điều kiện cần có trước khi thực hiện tính năng.
- **Main Features / Trigger**: Luồng tính năng chính và hành động kích hoạt.
- **Validation & Exception Handling**: Các quy tắc xác thực dữ liệu và cách hệ thống xử lý khi có lỗi hoặc ngoại lệ.
- **Acceptance Criteria (AC)**: Các tiêu chí nghiệm thu chi tiết. Dev và QA sẽ dựa vào đây để xác nhận tính năng đã hoàn thiện.

Tài liệu này là "nguồn sự thật duy nhất" (Single Source of Truth) cho Nhóm Phát triển (Dev), Kiểm thử viên (QA) và Phân tích viên nghiệp vụ (BA) trong quá trình xây dựng hệ thống Hoa Theo Mùa.
