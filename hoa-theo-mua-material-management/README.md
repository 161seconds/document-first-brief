# HTM_Material_Management — Phân hệ Quản lý Vật liệu & Định lượng Combo (`hoa-theo-mua-material-management`)

> **Mã dự án (Key)**: `hoa-theo-mua-material-management`  
> **Tên dự án**: `HTM_Material_Management`  
> **Phương pháp tiếp cận**: Document-First System Specification  

---

## 📖 Tổng quan Phân hệ

**HTM_Material_Management** chịu trách nhiệm bóc tách nguyên vật liệu thô (hoa cành, hoa lá phụ, giấy gói, ruy băng, phụ kiện cắm hoa), thiết lập công thức định lượng (Bill of Materials - BOM) cho từng sản phẩm Combo, và tính toán số lượng tồn kho khả dụng có thể bán trong thời gian thực:
- **Quản lý Nguyên vật liệu**: Phân loại theo nhãn ("thành phần", "combo", "sản phẩm"), tìm kiếm đa tiêu chí, phân trang.
- **Công thức định lượng Combo**: Thiết lập vật liệu cấu thành, gán vai trò (`Chính` / `Phụ`), tính toán số lượng tối đa có thể bán `MaxComboPossible = Min(Floor(AvailableForSale / Quantity))` theo các vật liệu chính.
- **Vòng đời tồn kho & Giữ hàng (Reservation Lifecycle)**: Tạm giữ tồn kho khi khách hàng đặt hàng và giải phóng / trừ kho khi thanh toán hoặc hủy đơn.
- **Cảnh báo tồn kho**: Cảnh báo vật liệu chạm ngưỡng an toàn và combo hết hàng trên Dashboard quản trị.

---

## 📂 Cấu trúc Thư mục

```text
hoa-theo-mua-material-management/
├── README.md                              # Tài liệu tổng quan phân hệ
├── BusinessRules/                         # Quy tắc nghiệp vụ hệ thống (Business Rules)
│   ├── BR-013.md                          # Giữ dòng tham chiếu vật liệu ngừng kinh doanh
│   ├── BR-014.md                          # Thứ tự hiển thị theo vai trò
│   ├── BR-018.md                          # Hoa phụ không giới hạn khả năng bán
│   └── BR-019.md                          # Combo không đủ điều kiện thì bằng 0
├── ConfirmedDoc/                          # Hợp đồng API & tài liệu kỹ thuật đã chốt
│   ├── DanhSach_API.md                    # Tổng hợp toàn bộ API đã triển khai & DTOs
│   └── HTM_MATERIAL_API.md                # Đặc tả request/response chi tiết của API Material
├── Context/                               # Ngữ cảnh kiến trúc
├── UserStory/                             # 13 Tài liệu User Stories (STORY-002, STORY-006 -> STORY-019)
├── TDD/                                   # Thiết kế kỹ thuật & Sequence Diagrams
│   ├── TDD-010.md                         # Xem, tìm kiếm và filter theo nhãn vật liệu (GET /api/v2/products)
│   ├── TDD-011.md                         # Xem công thức định lượng combo (GET /api/v1/products/{id}/combo-specification)
│   ├── DiagramUS002.md                    # Sequence Diagram tìm kiếm & lọc vật liệu
│   └── DiagramUS006.md                    # Sequence Diagram xem công thức combo hoa
├── UnitTest/                              # Kịch bản kiểm thử chuẩn hóa theo Form Web
│   ├── All_TestCase_Templates.md          # 11 Test Cases chuẩn hóa
│   ├── TestCase_Templates_US002.md        # Test cases tìm kiếm sản phẩm/vật liệu
│   ├── TestCase_Templates_US006_01.md     # Test cases xem công thức định lượng (Phần 1)
│   ├── TestCase_Templates_US006_02.md     # Test cases xem công thức định lượng (Phần 2)
│   └── TestCase_Templates_GetMaterials.md # Test cases lấy danh sách vật liệu
└── Code/                                  # Mã nguồn trích xuất phục vụ kiểm thử backend
    ├── Service/                           # Triển khai Product.cs
    └── ServiceTest/                       # Unit Test C# tương ứng
```

---

## 📑 Danh mục User Stories & TDD

| Mã Story | Tên tài liệu | TDD liên kết | Mô tả tóm tắt |
| :--- | :--- | :--- | :--- |
| **STORY-002** | [`02-ViewSearchMaterialsList.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/02-ViewSearchMaterialsList.md) | [TDD-010](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/TDD/TDD-010.md) | Xem, tìm kiếm và lọc danh sách nguyên vật liệu có nhãn "thành phần". |
| **STORY-006** | [`06-ViewComboFormula.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/06-ViewComboFormula.md) | [TDD-011](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/TDD/TDD-011.md) | Xem chi tiết công thức định lượng và số lượng có thể bán của Combo hoa. |
| **STORY-007** | [`07-AddMaterialsToComboFormula.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/07-AddMaterialsToComboFormula.md) | — | Thêm nguyên vật liệu mới vào công thức Combo hoa. |
| **STORY-008** | [`08-UpdateMaterialsInComboFormula.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/08-UpdateMaterialsInComboFormula.md) | — | Chỉnh sửa định lượng và vai trò (Chính / Phụ) của vật liệu trong Combo. |
| **STORY-009** | [`09-RemoveMaterialsFromComboFormula.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/09-RemoveMaterialsFromComboFormula.md) | — | Xóa nguyên vật liệu khỏi công thức cấu thành Combo hoa. |
| **STORY-012** | [`12-InventoryAndSellabilityAlerts.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/12-InventoryAndSellabilityAlerts.md) | [TDD-010](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/TDD/TDD-010.md) | Hiển thị cảnh báo vật liệu sắp hết tồn kho và combo hết hàng trên Dashboard. |
| **STORY-013** | [`13-ViewComboSellableQuantity.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/13-ViewComboSellableQuantity.md) | [TDD-011](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/TDD/TDD-011.md) | Xem số lượng tối đa có thể bán của combo hoa theo thời gian thực. |
| **STORY-014** | [`14-DisplayComboAvailabilityStatus.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/14-DisplayComboAvailabilityStatus.md) | — | Hiển thị trạng thái còn hàng / hết hàng của Combo trên giao diện. |
| **STORY-015** | [`15-ValidateSellableQuantityBeforeOrder.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/15-ValidateSellableQuantityBeforeOrder.md) | — | Kiểm tra và chặn đặt hàng vượt quá số lượng tồn kho khả dụng. |
| **STORY-016** | [`16-ManageMaterialsReservationAndInventoryLifecycle.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/16-ManageMaterialsReservationAndInventoryLifecycle.md) | — | Quản lý vòng đời giữ hàng (Reservation) và trừ tồn kho vật liệu. |
| **STORY-017** | [`17-CopyComboFormula.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/17-CopyComboFormula.md) | — | Sao chép công thức định lượng từ Combo này sang Combo khác. |
| **STORY-018** | [`18-AdjustMaterialsInventory.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/18-AdjustMaterialsInventory.md) | — | Điều chỉnh tồn kho thực tế của nguyên vật liệu. |
| **STORY-019** | [`19-ViewMaterialsInventoryHistory.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-material-management/UserStory/19-ViewMaterialsInventoryHistory.md) | — | Xem lịch sử biến động tồn kho nguyên vật liệu. |
