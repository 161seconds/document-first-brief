# Dự án Hoa Theo Mùa — Tài liệu Đặc tả Hệ thống (Document-First Repository)

Chào mừng bạn đến với kho lưu trữ tài liệu đặc tả yêu cầu nghiệp vụ (**User Stories**), thiết kế kỹ thuật (**Technical Design Documents - TDD**) và kịch bản kiểm thử (**Unit Test Suite**) cho hệ thống **Hoa Theo Mùa**.

Dự án áp dụng phương pháp tiếp cận **Document-First** (Tài liệu đi trước mã nguồn), giúp làm rõ toàn bộ luồng nghiệp vụ, giao diện, API Contract, và kịch bản kiểm thử trước khi tiến hành lập trình.

---

## 📂 Cấu trúc Thư mục Dự án

```text
document-first-brief/
├── .gitignore
├── README.md                                  # Tài liệu tổng quan dự án
└── hoatheomua/
    ├── BusinessRules/                         # Quy tắc nghiệp vụ hệ thống (Business Rules)
    │   ├── BR-013.md                          # Giữ dòng tham chiếu vật liệu ngừng kinh doanh
    │   ├── BR-014.md                          # Thứ tự hiển thị theo vai trò
    │   ├── BR-018.md                          # Hoa phụ không giới hạn khả năng bán
    │   └── BR-019.md                          # Combo không đủ điều kiện thì bằng 0
    ├── ConfirmedDoc/                          # Tài liệu API & nghiệp vụ đã chốt
    │   └── HTM_MATERIAL_API.md
    ├── UserStory/                             # Đặc tả yêu cầu người dùng (User Stories)
    │   ├── MaterialManagement/                # Phân hệ Quản lý Vật liệu & Định lượng Combo
    │   └── FlouristAI/                        # Phân hệ Thiết kế Hoa & Thiệp AI
    ├── TDD/                                   # Tài liệu Thiết kế Kỹ thuật (Technical Design)
    │   ├── TDD-010.md                         # Xem, tìm kiếm và filter theo nhãn vật liệu
    │   ├── TDD-011.md                         # Xem công thức định lượng của combo hoa
    │   ├── DiagramUS002.md
    │   └── DiagramUS006.md
    └── UnitTest/                              # Kịch bản & Mẫu Form Unit Test
        ├── All_TestCase_Templates.md          # Tổng hợp 11 Test Cases chuẩn theo form web
        ├── TestCase_Templates_US002.md
        ├── TestCase_Templates_US006_01.md
        └── TestCase_Templates_US006_02.md
```

---

## 📑 Danh mục Tài liệu Chi tiết

### 1. Phân hệ Quản lý Vật liệu & Định lượng Combo (`MaterialManagement`)
Phân hệ phụ trách việc bóc tách nguyên vật liệu thô (hoa cành, giấy gói, phụ kiện), thiết lập công thức định lượng cho sản phẩm Combo, và quản lý tồn kho khả dụng.

| Mã Story | Tên tài liệu | Mô tả tóm tắt |
| :--- | :--- | :--- |
| **STORY-002** | [`02-ViewSearchMaterialsList.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/02-ViewSearchMaterialsList.md) | Xem, tìm kiếm và lọc danh sách nguyên vật liệu có nhãn "thành phần". |
| **STORY-006** | [`06-ViewComboFormula.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/06-ViewComboFormula.md) | Xem chi tiết công thức định lượng và số lượng có thể bán của Combo hoa. |
| **STORY-007** | [`07-AddMaterialsToComboFormula.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/07-AddMaterialsToComboFormula.md) | Thêm nguyên vật liệu mới vào công thức Combo hoa. |
| **STORY-008** | [`08-UpdateMaterialsInComboFormula.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/08-UpdateMaterialsInComboFormula.md) | Chỉnh sửa định lượng và vai trò (Chính / Phụ) của vật liệu trong Combo. |
| **STORY-009** | [`09-RemoveMaterialsFromComboFormula.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/09-RemoveMaterialsFromComboFormula.md) | Xóa nguyên vật liệu khỏi công thức cấu thành Combo hoa. |
| **STORY-012** | [`12-InventoryAndSellabilityAlerts.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/12-InventoryAndSellabilityAlerts.md) | Hiển thị cảnh báo vật liệu sắp hết tồn kho và combo hết hàng trên Dashboard. |
| **STORY-013** | [`13-ViewComboSellableQuantity.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/13-ViewComboSellableQuantity.md) | Xem số lượng tối đa có thể bán của combo hoa theo thời gian thực. |
| **STORY-014** | [`14-DisplayComboAvailabilityStatus.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/14-DisplayComboAvailabilityStatus.md) | Hiển thị trạng thái còn hàng / hết hàng của Combo trên giao diện. |
| **STORY-015** | [`15-ValidateSellableQuantityBeforeOrder.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/15-ValidateSellableQuantityBeforeOrder.md) | Kiểm tra và chặn đặt hàng vượt quá số lượng tồn kho khả dụng. |
| **STORY-016** | [`16-ManageMaterialsReservationAndInventoryLifecycle.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/16-ManageMaterialsReservationAndInventoryLifecycle.md) | Quản lý vòng đời giữ hàng (Reservation) và trừ tồn kho vật liệu. |
| **STORY-017** | [`17-CopyComboFormula.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/17-CopyComboFormula.md) | Sao chép công thức định lượng từ Combo này sang Combo khác. |
| **STORY-018** | [`18-AdjustMaterialsInventory.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/18-AdjustMaterialsInventory.md) | Điều chỉnh tồn kho thực tế của nguyên vật liệu. |
| **STORY-019** | [`19-ViewMaterialsInventoryHistory.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/MaterialManagement/19-ViewMaterialsInventoryHistory.md) | Xem lịch sử biến động tồn kho nguyên vật liệu. |

---

### 2. Phân hệ Thiết kế Hoa AI & Thiệp Chúc Mừng (`FlouristAI`)
Phân hệ cung cấp trải nghiệm mua sắm thông minh: tạo mẫu hoa tùy chỉnh bằng AI, tạo thiệp cá nhân hóa tại trang thanh toán và quản lý lịch sử thiết kế.

| Nhóm chức năng | Các tài liệu User Story liên quan |
| :--- | :--- |
| **Duyệt Combo & Khởi tạo** | [`03-ViewCombos.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/03-ViewCombos.md), [`30-InitializeFlowerDesignRequest.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/30-InitializeFlowerDesignRequest.md) |
| **Tạo mẫu hoa với AI** | [`33-GenerateFlowerDesignWithAI.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/33-GenerateFlowerDesignWithAI.md), [`34-DownloadFlowerDesignImage.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/34-DownloadFlowerDesignImage.md) |
| **Thiệp chúc mừng AI tại Checkout**| [`35-GeneratePersonalizedCardWithAIAtCheckout.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/35-GeneratePersonalizedCardWithAIAtCheckout.md), [`36-RegeneratePersonalizedCardWithAI.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/36-RegeneratePersonalizedCardWithAI.md), [`41-DownloadCreatedGreetingCard.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/41-DownloadCreatedGreetingCard.md), [`42-SelectGreetingCardFromHistoryForCheckout.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/42-SelectGreetingCardFromHistoryForCheckout.md) |
| **Thanh toán & Đơn hàng** | [`38-InitializeCheckoutFromFlowerDesign.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/38-InitializeCheckoutFromFlowerDesign.md), [`39-CompleteCheckoutCreateAndPayOrder.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/39-CompleteCheckoutCreateAndPayOrder.md) |
| **Lịch sử người dùng (User History)**| [`40-ViewCustomAIFlowerDesignHistory.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/40-ViewCustomAIFlowerDesignHistory.md), [`45-ViewCreatedGreetingCardsHistory.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/45-ViewCreatedGreetingCardsHistory.md) |
| **Quản trị Admin (Portal Management)**| [`43-AdminViewAIGreetingCardDetails.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/43-AdminViewAIGreetingCardDetails.md), [`44-AdminViewAIGreetingCardsList.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/44-AdminViewAIGreetingCardsList.md), [`46-AdminDownloadAIGreetingCard.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/46-AdminDownloadAIGreetingCard.md), [`47-AdminViewCustomAIFlowerDesignsList.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/47-AdminViewCustomAIFlowerDesignsList.md), [`48-AdminViewCustomAIFlowerDesignDetails.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/48-AdminViewCustomAIFlowerDesignDetails.md) |

---

### 3. Thiết kế Kỹ thuật (Technical Design Documents - TDD)

Tài liệu thiết kế chi tiết kiến trúc, ERD, Sequence Diagram, API Contract và thuật toán xử lý:

* **[`TDD-010.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/TDD-010.md)** — *Xem, tìm kiếm và filter theo sản phẩm có nhãn là vật liệu*:
  * Endpoint: `GET /api/v2/products`
  * Thuật toán: Phân nhánh dynamic `IQueryable` theo `productType` (`"thành phần"`, `"combo"`, `"sản phẩm"`), hỗ trợ tìm kiếm không phân biệt hoa thường và phân trang.
  * Liên kết: `STORY-006`, `STORY-012`, `UT-002-01` $\rightarrow$ `UT-002-04`.

* **[`TDD-011.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/TDD-011.md)** — *Xem công thức định lượng của combo hoa*:
  * Endpoint: `GET /api/v1/products/{id}/combo-specification`
  * Thuật toán: Truy vấn `ComboSpecification` có liên kết `Product (Mat)`, tính khả năng cung ứng `MaxComboPossible = Floor(AvailableForSale / Quantity)` và sắp xếp theo vai trò (`Role`).
  * Liên kết: `STORY-006`, `STORY-002`, `UT-006-01` $\rightarrow$ `UT-006-04`.

---

### 4. Bộ Kiểm thử Đơn vị (Unit Test Suite)

Toàn bộ các test cases được chuẩn hóa định dạng khớp 100% với giao diện nhập liệu Web Form quản lý kiểm thử:

* **[`All_TestCase_Templates.md`](file:///d:/VNZ/document-first-brief/hoatheomua/UnitTest/All_TestCase_Templates.md)**: File tổng hợp đầy đủ **11 Test Cases** chuẩn hóa (bao gồm Precondition, Input, Expected Output, Rationale, Test Links):
  * **Nhóm US-006 (Công thức Combo & Danh sách Vật liệu):**
    * `UT-006-01`: kh tìm thấy combo (`GetComboSpecificationAsync`)
    * `UT-006-02`: Combo rỗng, trả về danh sách rỗng (`GetComboSpecificationAsync`)
    * `UT-006-03`: Tính toán số lượng MaxCombo và Sắp xếp đúng Role (`GetComboSpecificationAsync`)
    * `UT-006-04`: Combo rỗng hoặc vật liệu bị vô hiệu hóa (`GetComboSpecificationAsync`)
    * `UT-006-05`: Lọc chính xác theo ProductLabel (`GetMaterialsAsync`)
    * `UT-006-06`: Phân trang hoạt động (`GetMaterialsAsync`)
  * **Nhóm US-002 (Danh sách & Bộ lọc Sản phẩm):**
    * `UT-002-01`: xem và search list material (`GetLocalProductsV2`)
    * `UT-002-02`: Lọc Product - sản phẩm thường (`GetLocalProductsV2`)
    * `UT-002-02-01`: Lọc Material - vật liệu (`GetLocalProductsV2`)
    * `UT-002-03`: kh truyền typeProduct (`GetLocalProductsV2`)
    * `UT-002-04`: kh có dữ liệu thỏa mãn (`GetLocalProductsV2`)

---

## 📐 Chuẩn cấu trúc của một tài liệu User Story

Mỗi tập tin User Story (`.md`) tuân thủ nghiêm ngặt cấu trúc chuẩn sau:

1. **METADATA**:
   - `Story`: *"Là <vai trò>, tôi muốn <hành động>, để <giá trị nhận được>"*.
   - `Context`: Ngữ cảnh nghiệp vụ và đối tượng liên quan.
   - `Sprint`, `Priority` (*Must / Should / Could / Won't*), `Assignee`, `Creator`, `Status`.
2. **CONDITIONS**:
   - `Preconditions`: Điều kiện tiên quyết trước khi thực thi.
   - `Trigger`: Sự kiện hoặc hành vi kích hoạt luồng.
3. **FLOW**:
   - `Main Flow`: Luồng thực thi thành công mặc định.
   - `Alternative Flow`: Luồng rẽ nhánh thay thế.
   - `Exception Flow`: Luồng bắt lỗi và xử lý ngoại lệ.
4. **ACCEPTANCE CRITERIA (AC)**:
   - Viết theo chuẩn BDD: `Given ... When ... Then ...`.
5. **NON-FUNCTIONAL & OUT OF SCOPE**:
   - Các ràng buộc về tốc độ tải, bảo mật và phạm vi giới hạn của tính năng.

---

## 🛠 Hướng dẫn Đóng góp & Cập nhật Tài liệu

1. Khi thêm mới hoặc chỉnh sửa User Story, đảm bảo đối chiếu chéo các liên kết trong `TDD` và `UnitTest`.
2. Khi cập nhật logic API trong Backend, luôn đồng bộ lại **Sequence Diagram**, **API Contract** trong [TDD-010.md](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/TDD-010.md) và [TDD-011.md](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/TDD-011.md), đồng thời cập nhật kịch bản kiểm thử trong [All_TestCase_Templates.md](file:///d:/VNZ/document-first-brief/hoatheomua/UnitTest/All_TestCase_Templates.md).
3. Thư mục mã nguồn backend trích xuất phục vụ kiểm thử (`hoatheomua/Code/`) đã được cấu hình tự động bỏ qua trong [.gitignore](file:///d:/VNZ/document-first-brief/.gitignore).
