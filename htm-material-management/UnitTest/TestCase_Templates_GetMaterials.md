# Tổng hợp Form Điền Unit Test — GetMaterialsAsync

Tài liệu này được định dạng **khớp 100% với giao diện các ô nhập liệu trên Web**, dựa trên mã nguồn và bộ test case mới nhất của hàm `GetMaterialsAsync` (đã bổ sung chức năng tìm kiếm `Search` theo Tên và Mã vật liệu).

---

## 1. Mã: UT-MAT-01
### Thông tin tài liệu
* **Tiêu đề \*:** `Không truyền ProductLabel, trả về cả Thành phần và Phụ liệu`
* **Ghi chú:** `Kiểm tra mặc định khi không truyền nhãn sẽ lấy cả vật liệu "Thành phần" và "Phụ liệu", loại trừ sản phẩm đã xóa và các loại khác`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 8 sản phẩm:
  + 2 vật liệu mang nhãn "Thành phần" / "thành phần" (IsDeleted = false)
  + 2 vật liệu mang nhãn "Phụ liệu" / "phụ liệu" (IsDeleted = false)
  + 1 Combo (ProductLabel = "Combo", IsDeleted = false)
  + 1 Sản phẩm đơn (ProductLabel = "Sản phẩm", IsDeleted = false)
  + 2 vật liệu đã bị xóa mềm (IsDeleted = true)
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, ProductLabel = null })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 4, PageIndex = 1.
- Danh sách Items chứa đúng 4 vật liệu (2 Thành phần và 2 Phụ liệu).
- Loại bỏ hoàn toàn các sản phẩm không phải nguyên vật liệu và các bản ghi IsDeleted = true.
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo khi người dùng mở danh sách vật liệu mặc định, hệ thống gom đủ cả 2 nhóm nguyên vật liệu thô (Thành phần & Phụ liệu) và không để lọt dữ liệu rác.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-001` | Ghi chú: `Mặc định hiển thị danh sách vật liệu`

---

## 2. Mã: UT-MAT-02
### Thông tin tài liệu
* **Tiêu đề \*:** `Lọc chính xác theo nhãn "Thành phần"`
* **Ghi chú:** `Kiểm tra khi truyền ProductLabel = "Thành phần", API chỉ trả về các vật liệu là Thành phần (không phân biệt hoa/thường)`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 5 sản phẩm gồm hoa tươi (Thành phần), giấy gói (Phụ liệu) và vật liệu đã xóa mềm.
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, ProductLabel = "Thành phần" })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 2.
- 100% các phần tử trong Items đều có ProductLabel khớp "thành phần" (StringComparison.OrdinalIgnoreCase).
- Không lẫn các sản phẩm "Phụ liệu" hoặc sản phẩm đã xóa.
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận tính năng lọc riêng nhóm vật liệu chính (hoa tươi thành phần) hoạt động chính xác để phục vụ cấu hình công thức định lượng.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-003` | Ghi chú: `Lọc theo nhóm Thành phần`

---

## 3. Mã: UT-MAT-03
### Thông tin tài liệu
* **Tiêu đề \*:** `Lọc chính xác theo nhãn "Phụ liệu"`
* **Ghi chú:** `Kiểm tra khi truyền ProductLabel = "phụ liệu", API chỉ trả về các sản phẩm phụ liệu đóng gói`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có các sản phẩm Thành phần, Phụ liệu (Giấy gói, Ruy băng) và Phụ liệu đã xóa.
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, ProductLabel = "phụ liệu" })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 2.
- Tất cả phần tử trong Items đều có ProductLabel mang giá trị "phụ liệu" (không phân biệt hoa/thường).
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận tính năng lọc riêng nhóm phụ kiện/phụ liệu hoạt động chính xác.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-003` | Ghi chú: `Lọc theo nhóm Phụ liệu`

---

## 4. Mã: UT-MAT-04
### Thông tin tài liệu
* **Tiêu đề \*:** `Truyền ProductLabel không tồn tại, trả về danh sách rỗng`
* **Ghi chú:** `Kiểm tra khi tìm kiếm theo nhãn không có trong DB thì trả về TotalCount = 0`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Edge`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock chỉ có các vật liệu có nhãn "Thành phần" và "Phụ liệu".
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, ProductLabel = "Nhãn không tồn tại" })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 0.
- Items là danh sách rỗng (Items.Count == 0), không gây crash ứng dụng.
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo hệ thống trả về kết quả rỗng một cách an toàn khi bộ lọc không khớp với bất kỳ bản ghi nào.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-004` | Ghi chú: `Không có kết quả khớp bộ lọc`

---

## 5. Mã: UT-MAT-05
### Thông tin tài liệu
* **Tiêu đề \*:** `Phân trang hoạt động chính xác với Skip/Take`
* **Ghi chú:** `Kiểm tra tính toán phân trang khi số lượng vật liệu lớn hơn 1 trang`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 15 nguyên vật liệu hợp lệ thỏa mãn điều kiện.
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 2, PageSize = 5 })
```
* **Expected output \*:**
```text
- Hàm trả về PageIndex = 2, PageSize = 5, TotalCount = 15.
- Items ở trang 2 có đúng 5 phần tử (tương ứng Skip 5 và Take 5).
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo giải thuật phân trang trên IQueryable thực hiện đúng trên CSDL, không tải thừa dữ liệu.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-006` | Ghi chú: `Phân trang danh sách vật liệu`

---

## 6. Mã: UT-MAT-06
### Thông tin tài liệu
* **Tiêu đề \*:** `Tự động điều chỉnh tham số phân trang không hợp lệ`
* **Ghi chú:** `Kiểm tra tự động đưa PageIndex <= 0 về 1, PageSize <= 0 về 10, PageSize > 100 về tối đa 100`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Edge`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 1 vật liệu mẫu hợp lệ.
```
* **Input:**
```text
- Test Case 1: request = { PageIndex = -1, PageSize = 0 }
- Test Case 2: request = { PageIndex = 1, PageSize = 500 }
```
* **Expected output \*:**
```text
- Test Case 1: Trả về PageIndex = 1, PageSize = 10 (tự động điều chỉnh về giá trị mặc định an toàn).
- Test Case 2: Trả về PageIndex = 1, PageSize = 100 (giới hạn chặn trên tối đa 100 dòng).
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Bảo vệ server khỏi các request độc hại hoặc tham số âm làm treo truy vấn cơ sở dữ liệu.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-001` | Ghi chú: `Kiểm soát tham số phân trang`

---

## 7. Mã: UT-MAT-07
### Thông tin tài liệu
* **Tiêu đề \*:** `Ánh xạ đầy đủ và chính xác các trường MaterialResponse`
* **Ghi chú:** `Kiểm tra mapping từ Product Entity sang MaterialResponse DTO (Id, Mã, Tên, ĐVT, Tồn kho, Ảnh, Nhãn)`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 1 vật liệu có đầy đủ các thông tin:
  + Id = materialId (Guid)
  + ProductCode = "PL001"
  + ProductName = "Ruy băng đỏ 2cm"
  + Unit = "Cuộn"
  + AvailableForSale = 45.5
  + CoverImage = "https://cdn.example.com/ruy-bang-do.png"
  + ProductLabel = "Phụ liệu"
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10 })
```
* **Expected output \*:**
```text
- Items[0] chứa đầy đủ các trường khớp 100%:
  + MaterialId == materialId.ToString()
  + MaterialCode == "PL001"
  + MaterialName == "Ruy băng đỏ 2cm"
  + Unit == "Cuộn"
  + AvailableForSale == 45.5m
  + CoverImage == "https://cdn.example.com/ruy-bang-do.png"
  + ProductLabel == "Phụ liệu"
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo API cung cấp đủ dữ liệu cho Frontend hiển thị danh sách vật liệu (mã, tên, hình ảnh, đơn vị, tồn kho).
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-005` | Ghi chú: `Hiển thị đầy đủ thông tin dòng vật liệu`

---

## 8. Mã: UT-MAT-08
### Thông tin tài liệu
* **Tiêu đề \*:** `Cơ sở dữ liệu hoàn toàn không có vật liệu nào`
* **Ghi chú:** `Kiểm tra khi CSDL chỉ có Combo và Sản phẩm thông thường thì trả về mảng rỗng không lỗi`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Edge`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock chỉ chứa các sản phẩm có ProductLabel = "Combo" hoặc "Sản phẩm thông thường" (không có nhãn "Thành phần" hay "Phụ liệu").
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10 })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, TotalCount = 0.
- Items là danh sách rỗng `[]`, không xảy ra NullReferenceException.
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo hệ thống hoạt động ổn định khi database chưa có bất kỳ dữ liệu nguyên vật liệu nào.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-004` | Ghi chú: `Trạng thái rỗng an toàn`

---

## 9. Mã: UT-MAT-09
### Thông tin tài liệu
* **Tiêu đề \*:** `Tìm kiếm theo tên vật liệu (Search theo ProductName)`
* **Ghi chú:** `Kiểm tra tìm kiếm vật liệu theo từ khóa tên sản phẩm không phân biệt hoa/thường`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 4 sản phẩm:
  + HH01: "Hoa Hồng Đỏ", nhãn "Thành phần"
  + HH02: "Hoa Hồng Trắng", nhãn "Thành phần"
  + HC01: "Hoa Cúc Mẫu Đơn", nhãn "Thành phần"
  + PL01: "Giấy Gói", nhãn "Phụ liệu"
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, Search = "hồng" })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 2.
- Items chứa đúng 2 vật liệu có tên chứa chữ "Hồng" ("Hoa Hồng Đỏ" và "Hoa Hồng Trắng").
- Bỏ qua các vật liệu không chứa từ khóa ("Hoa Cúc", "Giấy Gói").
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận tính năng tìm kiếm theo tên vật liệu hoạt động chính xác với cơ chế so khớp không phân biệt hoa thường.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-002` | Ghi chú: `Tìm kiếm theo tên vật liệu`

---

## 10. Mã: UT-MAT-10
### Thông tin tài liệu
* **Tiêu đề \*:** `Tìm kiếm theo mã vật liệu (Search theo ProductCode)`
* **Ghi chú:** `Kiểm tra tìm kiếm vật liệu theo mã sản phẩm (ProductCode) không phân biệt hoa/thường`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có các sản phẩm:
  + MAT_001: "Hoa Hồng Đỏ", nhãn "Thành phần"
  + MAT_002: "Hoa Cúc", nhãn "Thành phần"
  + ACC_001: "Giấy Gói", nhãn "Phụ liệu"
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10, Search = "acc_001" })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 1.
- Items[0].MaterialCode == "ACC_001".
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo quản trị viên có thể tra cứu nhanh vật liệu chính xác thông qua mã SKU/mã quản lý (ProductCode).
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-002` | Ghi chú: `Tìm kiếm theo mã vật liệu`

---

## 11. Mã: UT-MAT-11
### Thông tin tài liệu
* **Tiêu đề \*:** `Kết hợp đồng thời ProductLabel và Search theo tên/mã`
* **Ghi chú:** `Kiểm tra bộ lọc giao thoa: vừa lọc theo nhóm nhãn vừa tìm kiếm theo từ khóa`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (GetMaterialsAsync)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có:
  + HH01: "Hoa Hồng Đỏ", nhãn "Thành phần"
  + PL01: "Nơ Ruy Băng Hồng", nhãn "Phụ liệu"
  + PL02: "Giấy Gói Xanh", nhãn "Phụ liệu"
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = {
    PageIndex = 1,
    PageSize = 10,
    ProductLabel = "Phụ liệu",
    Search = "hồng"
})
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 1.
- Items[0].MaterialName == "Nơ Ruy Băng Hồng" và Items[0].ProductLabel == "Phụ liệu".
- Bỏ qua "Hoa Hồng Đỏ" (vì là Thành phần) và "Giấy Gói Xanh" (vì không chứa từ khóa "hồng").
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo điều kiện AND giữa ProductLabel và Search hoạt động chuẩn xác trên IQueryable, không trả về nhầm loại sản phẩm.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `STORY-002` | Section: `AC-003` | Ghi chú: `Kết hợp lọc nhóm và tìm kiếm`
