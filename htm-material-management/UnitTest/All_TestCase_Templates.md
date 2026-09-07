# Tổng hợp Form Điền Unit Test Trên Web (Chuẩn Form 100%)

Tài liệu này được định dạng **khớp 100% với giao diện các ô nhập liệu trên Web**, giúp bạn copy-paste trực tiếp từng ô một cách nhanh nhất.

---

# NHÓM US-006 (GetComboSpecificationAsync & GetMaterialsAsync)

---

## 1. Mã: UT-006-01
### Thông tin tài liệu
* **Tiêu đề \*:** `kh tìm thấy combo`
* **Ghi chú:** `Kiểm tra xử lý ngoại lệ khi combo không tồn tại hoặc đã xóa mềm`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem công thức định lượng của combo hoa`
* **Unit under test \*:** `GetComboSpecificationAsync`
* **Loại:** `Error`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock không có dữ liệu Combo nào hoặc Combo đã bị xoá (IsDeleted = true).
- Mock DbContext trả về DbSet Products rỗng.
```
* **Input:**
```text
Gọi hàm GetComboSpecificationAsync(comboId = Guid.NewGuid())
```
* **Expected output \*:**
```text
- Hàm NÉM RA NGOẠI LỆ NotFoundException với message là "Không tìm thấy combo với ID: {comboId}".
- (Trên API Controller sẽ bắt lỗi này và trả về HTTP 404 cho client).
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận hệ thống chặn các thao tác xem định lượng đối với combo không tồn tại hoặc đã bị xóa.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-04` | Ghi chú: `Xử lý lỗi 404 Not Found`

---

## 2. Mã: UT-006-02
### Thông tin tài liệu
* **Tiêu đề \*:** `Combo rỗng, trả về danh sách rỗng`
* **Ghi chú:** `Kiểm tra combo mới tạo chưa có cấu hình vật liệu`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem công thức định lượng của combo hoa`
* **Unit under test \*:** `GetComboSpecificationAsync`
* **Loại:** `Happy` (hoặc `Edge`)

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có dữ liệu Combo hợp lệ (IsDeleted = false, Id = comboId, ProductName = "Empty Combo").
- Combo này chưa cấu hình bất kỳ vật liệu nào (Bảng ComboSpecification rỗng).
```
* **Input:**
```text
Gọi hàm GetComboSpecificationAsync(comboId)
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về đúng thông tin SubId = comboId.ToString().
- Danh sách Materials trả về là mảng rỗng (Materials.Count == 0).
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo hệ thống xử lý đúng đối với các Combo mới tạo, chưa setup định lượng, không bị crash hệ thống.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-02` | Ghi chú: `Trả về danh sách rỗng`

---

## 3. Mã: UT-006-03
### Thông tin tài liệu
* **Tiêu đề \*:** `Tính toán số lượng MaxCombo và Sắp xếp đúng Role`
* **Ghi chú:** `Kiểm tra thuật toán tính tồn kho combo và sắp xếp Phụ liệu lên trước Chính liệu`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem công thức định lượng của combo hoa`
* **Unit under test \*:** `GetComboSpecificationAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 1 Combo với 3 vật liệu:
  + coreMat1: IsCore = true, Quantity = 2, AvailableForSale = 10 (IsDeleted = false)
  + coreMat2: IsCore = true, Quantity = 5, AvailableForSale = 40 (IsDeleted = false)
  + supportMat: IsCore = false, Quantity = 1, AvailableForSale = 0 (IsDeleted = false)
- ComboSpecification cấu hình tương ứng với 3 vật liệu trên (IsActive = true).
```
* **Input:**
```text
Gọi hàm GetComboSpecificationAsync(comboId)
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về danh sách gồm 3 vật liệu (Materials.Count == 3).
- Kiểm tra thứ tự sắp xếp: Phụ liệu (Role = false) nằm trước Vật liệu chính (Role = true) do OrderBy(x => x.Role) sắp xếp false -> true.
- Tính toán MaxComboPossible chính xác bằng floor(AvailableForSale / Quantity):
  + coreMat1 = 10 / 2 = 5
  + coreMat2 = 40 / 5 = 8
  + supportMat = 0 (AvailableForSale = 0)
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận API tính toán đúng khả năng bán tối đa của từng vật liệu và áp dụng chính xác thứ tự sắp xếp (phụ liệu hiện trước, chính liệu hiện sau).
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-01` | Ghi chú: `Tính toán MaxCombo và sắp xếp Role`

---

## 4. Mã: UT-006-04
### Thông tin tài liệu
* **Tiêu đề \*:** `Combo rỗng hoặc vật liệu bị vô hiệu hóa`
* **Ghi chú:** `Kiểm tra lọc bỏ các vật liệu có IsActive = false hoặc Mat.IsDeleted = true`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem công thức định lượng của combo hoa`
* **Unit under test \*:** `GetComboSpecificationAsync`
* **Loại:** `Edge`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 1 Combo hợp lệ (IsDeleted = false).
- Các bản ghi định lượng trong ComboSpecification đều có IsActive = false hoặc vật liệu liên quan đã bị xóa mềm (Mat.IsDeleted = true).
```
* **Input:**
```text
Gọi hàm GetComboSpecificationAsync(comboId)
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, query lọc điều kiện (c.IsActive && !c.Mat.IsDeleted) sẽ loại bỏ toàn bộ các bản ghi vô hiệu hóa.
- Danh sách Materials trả về là mảng rỗng (Materials.Count == 0).
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo hệ thống không hiển thị các vật liệu đã bị vô hiệu hóa hoặc đã xóa mềm trong công thức combo.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-03` | Ghi chú: `Loại bỏ vật liệu inactive`

---

## 5. Mã: UT-006-05
### Thông tin tài liệu
* **Tiêu đề \*:** `Lọc chính xác theo ProductLabel`
* **Ghi chú:** `Kiểm tra lấy danh sách vật liệu theo ProductLabel = "thành phần"`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (Thành phần)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 4 sản phẩm:
  + Mat 1: ProductLabel = "Thành phần", IsDeleted = false
  + Mat 2: ProductLabel = "thành phần", IsDeleted = false (chữ thường)
  + Combo 1: ProductLabel = "Combo", IsDeleted = false
  + Deleted Mat: ProductLabel = "Thành phần", IsDeleted = true
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10 })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về TotalCount = 2.
- Danh sách Items trả về chứa đúng 2 vật liệu (Mat 1 và Mat 2).
- Bỏ qua sản phẩm có nhãn khác ("Combo") và sản phẩm đã xóa mềm (IsDeleted = true).
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận API lấy danh sách vật liệu hoạt động chính xác dựa trên nhãn ProductLabel = "thành phần", đồng thời loại bỏ các sản phẩm sai loại hoặc đã xóa.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-05` | Ghi chú: `Lọc danh sách vật liệu`

---

## 6. Mã: UT-006-06
### Thông tin tài liệu
* **Tiêu đề \*:** `Phân trang hoạt động`
* **Ghi chú:** `Kiểm tra phân trang PageIndex, PageSize cho danh sách vật liệu`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách vật liệu (Thành phần)`
* **Unit under test \*:** `GetMaterialsAsync`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có 15 sản phẩm hợp lệ thỏa mãn (ProductLabel = "Thành phần", IsDeleted = false).
```
* **Input:**
```text
Gọi hàm GetMaterialsAsync(request = { PageIndex = 2, PageSize = 5 })
```
* **Expected output \*:**
```text
- Hàm thực thi thành công, trả về PageIndex = 2, PageSize = 5, TotalCount = 15.
- Danh sách Items ở trang 2 có chính xác 5 phần tử (đúng số lượng PageSize).
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo tính năng phân trang (Pagination) hoạt động đúng, giới hạn chính xác số lượng dữ liệu trả về theo thông số request.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-006` | Section: `AC-06` | Ghi chú: `Phân trang danh sách vật liệu`

---

# NHÓM US-002 (GetLocalProductsV2)

---

## 7. Mã: UT-002-01
### Thông tin tài liệu
* **Tiêu đề \*:** `xem và search list material`
* **Ghi chú:** `Kết hợp lọc typeProduct = "thành phần" và search từ khóa`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách sản phẩm theo loại`
* **Unit under test \*:** `GetLocalProductsV2`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock tồn tại các sản phẩm:
  + P1: ProductName = "Hoa hồng đỏ Đà Lạt", ProductLabel = "Thành phần", IsDeleted = false
  + P2: ProductName = "Hoa cúc họa mi", ProductLabel = "Thành phần", IsDeleted = false
  + P3: ProductName = "Hoa hồng Ecuador", ProductType = "Sản phẩm", IsDeleted = false
```
* **Input:**
```text
Gọi hàm GetLocalProductsV2(request = { productType = "thành phần", search = "hồng" })
```
* **Expected output \*:**
```text
- Trả về danh sách sản phẩm thành công (TotalCount = 1).
- Danh sách Items chỉ chứa P1 (vừa có nhãn Thành phần vừa khớp từ khóa "hồng").
- P2 bị loại vì không khớp search; P3 bị loại vì không phải loại thành phần.
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận luồng kết hợp giữa lọc theo loại thành phần (vật liệu) và tìm kiếm từ khóa hoạt động chính xác.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-002` | Section: `AC-01` | Ghi chú: `Xem và search danh sách vật liệu`

---

## 8. Mã: UT-002-02
### Thông tin tài liệu
* **Tiêu đề \*:** `Lọc Product - sản phẩm thường`
* **Ghi chú:** `Lọc productType = "Sản phẩm" và loại trừ cha có toàn bộ con là Combo`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách sản phẩm theo loại`
* **Unit under test \*:** `GetLocalProductsV2`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có các sản phẩm:
  + P_IND: ProductType = "Sản phẩm", không có sản phẩm con (IsDeleted = false).
  + P_MIXED (Cha): ProductType = "Sản phẩm", có con Con_1 (Combo) và Con_2 (Sản phẩm).
  + P_ALL_COMBO (Cha): ProductType = "Sản phẩm", có con Con_3 (Combo) và Con_4 (Combo).
  + P_COMBO: ProductType = "Combo", không có con.
```
* **Input:**
```text
Gọi hàm GetLocalProductsV2(request = { productType = "Sản phẩm" })
```
* **Expected output \*:**
```text
- Trả về danh sách gồm 2 sản phẩm: P_IND và P_MIXED (TotalCount = 2).
- P_ALL_COMBO bị loại trừ vì toàn bộ con là Combo (thuộc nhóm Combo).
- P_COMBO bị loại trừ vì là loại Combo.
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận hệ thống lọc chính xác các sản phẩm thông thường và loại trừ đúng nhóm Combo cha-con.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-002` | Section: `AC-02` | Ghi chú: `Lọc sản phẩm thường`

---

## 9. Mã: UT-002-02-01
### Thông tin tài liệu
* **Tiêu đề \*:** `Lọc Material - vật liệu`
* **Ghi chú:** `Lọc productType = "thành phần" theo ProductLabel LIKE %thành phần%`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách sản phẩm theo loại`
* **Unit under test \*:** `GetLocalProductsV2`
* **Loại:** `Happy`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock có các sản phẩm:
  + M1: ProductLabel = "Thành phần 1", IsDeleted = false
  + M2: ProductLabel = "Vật liệu và thành phần", IsDeleted = false
  + P1: ProductLabel = "Hoa hồng", ProductType = "Sản phẩm", IsDeleted = false
```
* **Input:**
```text
Gọi hàm GetLocalProductsV2(request = { productType = "thành phần" })
```
* **Expected output \*:**
```text
- Trả về danh sách gồm đúng 2 vật liệu: M1 và M2 (khớp điều kiện LIKE %thành phần%).
- TotalCount = 2.
- Không chứa sản phẩm P1.
```

### Phân loại và trách nhiệm
* **Suite:** `SMOKE` | **Priority:** `P1` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận hệ thống filter chính xác loại sản phẩm Vật liệu, không hiển thị lẫn lộn Combo hay Sản phẩm thường.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-002` | Section: `AC-03` | Ghi chú: `Lọc vật liệu`

---

## 10. Mã: UT-002-03
### Thông tin tài liệu
* **Tiêu đề \*:** `kh truyền typeProduct`
* **Ghi chú:** `Kiểm tra default behavior khi param productType null`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách sản phẩm theo loại`
* **Unit under test \*:** `GetLocalProductsV2`
* **Loại:** `Boundary`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock tồn tại 3 sản phẩm với 3 loại khác nhau: "Combo 1", "Thành phần 1", "Sản phẩm 1" (IsDeleted = false).
- request không truyền (hoặc truyền null) thuộc tính productType.
```
* **Input:**
```text
Gọi hàm GetLocalProductsV2(request = { productType = null })
```
* **Expected output \*:**
```text
- Trả về thành công danh sách chứa TẤT CẢ 3 sản phẩm mà không thực hiện thao tác lọc loại sản phẩm nào.
- TotalCount = 3.
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Đảm bảo hệ thống vẫn hiển thị tất cả các loại sản phẩm khi người dùng không chọn filter loại sản phẩm.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-002` | Section: `AC-04` | Ghi chú: `Mặc định không lọc`

---

## 11. Mã: UT-002-04
### Thông tin tài liệu
* **Tiêu đề \*:** `kh có dữ liệu thỏa mãn`
* **Ghi chú:** `Kiểm tra khi filter không có bản ghi nào khớp`

### Metadata quản trị tài liệu
* **Phiên bản:** `v0.1` | **Author \*:** `Quoc Bao` | **Reviewer:** `Tân Trần` | **Approver:** `Chưa chỉ định` | **Owner \*:** `Quoc Bao`

### Đơn vị kiểm thử
* **Module \*:** `Xem và tìm kiếm danh sách sản phẩm theo loại`
* **Unit under test \*:** `GetLocalProductsV2`
* **Loại:** `Edge`

### Dữ liệu test
* **Precondition / Mock setup:**
```text
- DB Mock CHỈ tồn tại các sản phẩm loại "Thành phần" và "Sản phẩm".
- KHÔNG có sản phẩm nào loại "Combo" trong DB.
```
* **Input:**
```text
Gọi hàm GetLocalProductsV2(request = { productType = "Combo" })
```
* **Expected output \*:**
```text
- Hàm vẫn chạy thành công, không ném exception hay lỗi.
- Danh sách trả về là một mảng rỗng (Items = []).
- Thuộc tính TotalCount = 0.
```

### Phân loại và trách nhiệm
* **Suite:** `REGRESSION` | **Priority:** `P2` | **Owner:** `Quoc Bao`
* **Rationale \*:**
```text
Xác nhận hệ thống không bị crash hoặc ném exception khi kết quả query rỗng.
```
* **TEST_LINKS:**
  * Type: `User Story` | Mã: `US-002` | Section: `AC-05` | Ghi chú: `Kết quả truy vấn rỗng`
