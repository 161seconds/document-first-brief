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
    │   └── MaterialManagement/                # Quy tắc nghiệp vụ Quản lý Vật liệu & Combo
    │       ├── BR-013.md                      # Giữ dòng tham chiếu vật liệu ngừng kinh doanh
    │       ├── BR-014.md                      # Thứ tự hiển thị theo vai trò
    │       ├── BR-018.md                      # Hoa phụ không giới hạn khả năng bán
    │       └── BR-019.md                      # Combo không đủ điều kiện thì bằng 0
    ├── ConfirmedDoc/                          # Tài liệu API & nghiệp vụ đã chốt
    │   ├── DanhSach_API.md                    # Tổng hợp toàn bộ các API đã làm & chức năng
    │   └── HTM_MATERIAL_API.md
    ├── Context/                               # Ngữ cảnh kiến trúc & thiết kế hệ thống
    │   └── FlouristAI/                        # Context & sơ đồ CSDL phân hệ FlouristAI
    │       ├── AI-Card-Context.md
    │       ├── AI-DB-Diagram.md
    │       ├── AI-Flower-Context.md
    │       ├── AI-Mockup-Context.md
    │       └── AI_CUSTOMIZE_DB.dbdiagram
    ├── UserStory/                             # Đặc tả yêu cầu người dùng (User Stories)
    │   ├── MaterialManagement/                # Phân hệ Quản lý Vật liệu & Định lượng Combo
    │   └── FlouristAI/                        # Phân hệ Thiết kế Hoa & Thiệp AI
    ├── TDD/                                   # Tài liệu Thiết kế Kỹ thuật (Technical Design)
    │   ├── MaterialManagement/                # TDD Phân hệ Vật liệu & Combo
    │   │   ├── TDD-010.md                     # Xem, tìm kiếm và filter theo nhãn vật liệu
    │   │   ├── TDD-011.md                     # Xem công thức định lượng của combo hoa
    │   │   ├── DiagramUS002.md
    │   │   └── DiagramUS006.md
    │   └── FlouristAI/                        # TDD Phân hệ Thiết kế Hoa & Thiệp AI
    │       ├── TDD-030/                       # Tạo mẫu hoa AI (18 UTs)
    │       ├── TDD-035/                       # Tạo thiệp thiết kế AI (19 UTs)
    │       ├── TDD-036/                       # Tạo lại thiệp từ lịch sử (12 UTs)
    │       ├── TDD-050/                       # Lấy danh sách Mockup (10 UTs)
    │       ├── TDD-051/                       # Thêm Mockup (14 UTs)
    │       ├── TDD-052/                       # Chuyển trạng thái Mockup (6 UTs)
    │       ├── TDD-053/                       # Xóa Mockup (7 UTs)
    │       ├── TDD-054/                       # Lấy danh sách Card Configs (8 UTs)
    │       ├── TDD-055/                       # Tạo Card Config (9 UTs)
    │       ├── TDD-056/                       # Xóa Card Config (8 UTs)
    │       └── TDD-062/                       # Cập nhật Card Config (10 UTs)
    └── UnitTest/                              # Kịch bản & Mẫu Form Unit Test
        └── MaterialManagement/                # Test Cases Phân hệ Vật liệu & Combo
            ├── All_TestCase_Templates.md      # Tổng hợp 11 Test Cases chuẩn theo form web
            ├── TestCase_Templates_US002.md
            ├── TestCase_Templates_US006_01.md
            ├── TestCase_Templates_US006_02.md
            └── TestCase_Templates_GetMaterials.md
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

#### A. Phân hệ Quản lý Vật liệu (`MaterialManagement`)
* **[`TDD-010.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-010.md)** — *Xem, tìm kiếm và filter theo sản phẩm có nhãn là vật liệu*:
  * Endpoint: `GET /api/v2/products`
  * Thuật toán: Phân nhánh dynamic `IQueryable` theo `productType` (`"thành phần"`, `"combo"`, `"sản phẩm"`), hỗ trợ tìm kiếm không phân biệt hoa thường và phân trang.
  * Liên kết: `STORY-006`, `STORY-012`, `UT-002-01` $\rightarrow$ `UT-002-04`.

* **[`TDD-011.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-011.md)** — *Xem công thức định lượng của combo hoa*:
  * Endpoint: `GET /api/v1/products/{id}/combo-specification`
  * Thuật toán: Truy vấn `ComboSpecification` có liên kết `Product (Mat)`, tính khả năng cung ứng `MaxComboPossible = Floor(AvailableForSale / Quantity)` và sắp xếp theo vai trò (`Role`).
  * Liên kết: `STORY-006`, `STORY-002`, `UT-006-01` $\rightarrow$ `UT-006-04`.

#### B. Phân hệ Thiết kế Hoa & Thiệp AI (`FlouristAI`)
* **[`TDD-030_tao-yeu-cau-va-mau-hoa-ai.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-030/TDD-030_tao-yeu-cau-va-mau-hoa-ai.md)** — *Tạo mẫu hoa AI*:
  * Endpoint: `POST /api/ai-flowers` | Liên kết: `STORY-030`, `STORY-033`, `UT-030-01` $\rightarrow$ `UT-030-18`.
  * Nghiệp vụ: Xác thực Combo & Mockup, kiểm tra quota (3 lượt/ngày), gọi AI Module retry tối đa 2 lần, gắn logo và lưu kết quả `generated_flowers` + `client_histories`.

* **[`TDD-035_tao-thep-thiet-ke-ai.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-035/TDD-035_tao-thep-thiet-ke-ai.md)** — *Tạo thiệp thiết kế AI*:
  * Endpoint: `POST /api/ai-cards` | Liên kết: `STORY-035`, `UT-035-01` $\rightarrow$ `UT-035-19`.
  * Nghiệp vụ: Tạo thiệp AI tại checkout (`go_may` miễn phí, `calligraphy` tính phí theo rule và size config), quota 5 lượt/ngày, lưu `generated_cards` + `client_histories`.

* **[`TDD-036_tao-lai-thep-tu-lich-su.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-036/TDD-036_tao-lai-thep-tu-lich-su.md)** — *Tạo lại thiệp từ lịch sử*:
  * Endpoint: `POST /api/ai-cards/{id}/regenerate` | Liên kết: `STORY-036`, `UT-036-01` $\rightarrow$ `UT-036-12`.
  * Nghiệp vụ: Tái tạo thiệp từ bản ghi lịch sử, kế thừa tham số gốc, kiểm tra quyền sở hữu và quota.

* **[`TDD-050_lay-danh-sach-mockup.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-050/TDD-050_lay-danh-sach-mockup.md)** — *Lấy danh sách Mockup*:
  * Endpoint: `GET /api/mockups` | Liên kết: `UT-050-01` $\rightarrow$ `UT-050-10`.
  * Nghiệp vụ: Truy vấn danh sách Mockup phân trang (Customer tối đa 4 mockup/trang, chỉ lấy `is_active=true` và chưa xóa; Admin xem toàn bộ).

* **[`TDD-051_tao-mockup.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-051/TDD-051_tao-mockup.md)** — *Thêm Mockup mới*:
  * Endpoint: `POST /api/mockups` | Liên kết: `UT-051-01` $\rightarrow$ `UT-051-14`.
  * Nghiệp vụ: Quyền Admin, upload ảnh mockup (tối đa 10MB), validate tên (1-50 ký tự), mô tả (tối đa 200 ký tự).

* **[`TDD-052_chuyen-trang-thai-mockup.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-052/TDD-052_chuyen-trang-thai-mockup.md)** — *Chuyển trạng thái Mockup*:
  * Endpoint: `PATCH /api/mockups/{id}/status` | Liên kết: `UT-052-01` $\rightarrow$ `UT-052-06`.
  * Nghiệp vụ: Quyền Admin, bật/tắt `is_active` của Mockup.

* **[`TDD-053_xoa-mockup.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-053/TDD-053_xoa-mockup.md)** — *Xóa Mockup (Soft Delete)*:
  * Endpoint: `DELETE /api/mockups/{id}` | Liên kết: `UT-053-01` $\rightarrow$ `UT-053-07`.
  * Nghiệp vụ: Quyền Admin, xóa mềm mockup (`is_deleted=true`), giữ nguyên lịch sử liên kết đã tạo.

* **[`TDD-054_lay-danh-sach-card-configs.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-054/TDD-054_lay-danh-sach-card-configs.md)** — *Lấy danh sách Card Configs*:
  * Endpoint: `GET /api/v1/configs/content` | Liên kết: `UT-054-01` $\rightarrow$ `UT-054-08`.
  * Nghiệp vụ: Lấy cấu hình thiệp (size, calligraphy price rules, background styles) theo group và key.

* **[`TDD-055_tao-card-config.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-055/TDD-055_tao-card-config.md)** — *Tạo Card Config*:
  * Endpoint: `POST /api/v1/configs` | Liên kết: `UT-055-01` $\rightarrow$ `UT-055-09`.
  * Nghiệp vụ: Quyền Admin, tạo cấu hình thiệp mới với JSON value hợp lệ.

* **[`TDD-056_xoa-card-config.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-056/TDD-056_xoa-card-config.md)** — *Xóa Card Config*:
  * Endpoint: `DELETE /api/v1/configs/{id}` | Liên kết: `UT-056-01` $\rightarrow$ `UT-056-08`.
  * Nghiệp vụ: Quyền Admin, xóa mềm cấu hình thiệp.

* **[`TDD-062_cap-nhat-card-config.md`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-062/TDD-062_cap-nhat-card-config.md)** — *Cập nhật Card Config*:
  * Endpoint: `PUT /api/v1/configs/{id}` | Liên kết: `UT-062-01` $\rightarrow$ `UT-062-10`.
  * Nghiệp vụ: Quyền Admin, cập nhật thông số JSON value của cấu hình thiệp.

---

### 4. Bộ Kiểm thử Đơn vị (Unit Test Suite)

Toàn bộ các test cases được chuẩn hóa định dạng khớp 100% với giao diện nhập liệu Web Form quản lý kiểm thử:

* **Phân hệ Material Management ([`hoatheomua/UnitTest/MaterialManagement/`](file:///d:/VNZ/document-first-brief/hoatheomua/UnitTest/MaterialManagement/)):** 11 Test Cases chuẩn hóa cho US-006 và US-002.
* **Phân hệ Flourist AI (Tổng cộng 121 Test Cases theo từng TDD Module):**
  * [`TDD-030`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-030/): 18 UTs (`UT-030-01` $\rightarrow$ `UT-030-18`) — Tạo mẫu hoa AI
  * [`TDD-035`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-035/): 19 UTs (`UT-035-01` $\rightarrow$ `UT-035-19`) — Tạo thiệp AI tại checkout
  * [`TDD-036`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-036/): 12 UTs (`UT-036-01` $\rightarrow$ `UT-036-12`) — Tạo lại thiệp từ lịch sử
  * [`TDD-050`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-050/): 10 UTs (`UT-050-01` $\rightarrow$ `UT-050-10`) — Lấy danh sách Mockup
  * [`TDD-051`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-051/): 14 UTs (`UT-051-01` $\rightarrow$ `UT-051-14`) — Thêm Mockup mới
  * [`TDD-052`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-052/): 6 UTs (`UT-052-01` $\rightarrow$ `UT-052-06`) — Chuyển trạng thái Mockup
  * [`TDD-053`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-053/): 7 UTs (`UT-053-01` $\rightarrow$ `UT-053-07`) — Xóa Mockup
  * [`TDD-054`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-054/): 8 UTs (`UT-054-01` $\rightarrow$ `UT-054-08`) — Lấy danh sách Card Configs
  * [`TDD-055`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-055/): 9 UTs (`UT-055-01` $\rightarrow$ `UT-055-09`) — Tạo Card Config
  * [`TDD-056`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-056/): 8 UTs (`UT-056-01` $\rightarrow$ `UT-056-08`) — Xóa Card Config
  * [`TDD-062`](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/FlouristAI/TDD-062/): 10 UTs (`UT-062-01` $\rightarrow$ `UT-062-10`) — Cập nhật Card Config

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
2. Khi cập nhật logic API trong Backend, luôn đồng bộ lại **Sequence Diagram**, **API Contract** trong [TDD-010.md](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-010.md) và [TDD-011.md](file:///d:/VNZ/document-first-brief/hoatheomua/TDD/MaterialManagement/TDD-011.md), đồng thời cập nhật kịch bản kiểm thử trong [All_TestCase_Templates.md](file:///d:/VNZ/document-first-brief/hoatheomua/UnitTest/MaterialManagement/All_TestCase_Templates.md).
3. Thư mục mã nguồn backend trích xuất phục vụ kiểm thử (`hoatheomua/Code/`) đã được cấu hình tự động bỏ qua trong [.gitignore](file:///d:/VNZ/document-first-brief/.gitignore).
