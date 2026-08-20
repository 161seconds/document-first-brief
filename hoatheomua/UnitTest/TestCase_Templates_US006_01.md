# Các form Unit Test cho US-006 (Xem công thức định lượng Combo hoa)

Dưới đây là các form Unit Test chuẩn hóa dựa trên mã nguồn thực tế tại `Product.Service.cs` (`GetComboSpecificationAsync`) và bộ kiểm thử `GetComboSpecificationTests.cs`.

---

## 1. UT-006-01 (Case 1: Error - Không tìm thấy Combo)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Error / Exception`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock rỗng hoặc không tồn tại sản phẩm khớp với ID truyền vào (hoặc sản phẩm đã bị xóa IsDeleted = true).
  - Mock DbContext trả về DbSet Products và ComboSpecification rỗng.
  ```

- **Input:**
  ```text
  Guid comboId = Guid.NewGuid();
  service.GetComboSpecificationAsync(comboId);
  ```

- **Expected output *:**
  ```text
  - Hàm ném ra ngoại lệ NotFoundException với thông báo: "Không tìm thấy combo với ID: {comboId}".
  - Không trả về dữ liệu.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P1`
- **Owner:** Backend Team
- **Rationale *:** 
  ```text
  Xác nhận hệ thống bắt lỗi chuẩn xác và ném NotFoundException khi truy vấn một Combo không tồn tại hoặc đã bị xóa mềm.
  ```

- **TEST_LINKS:**
  ```text
  US-006
  TDD-006
  ```

- **Ghi chú:**
  ```text
  Tương ứng với test case GetComboSpecification_ProductNotFound_ThrowsNotFoundException trong mã nguồn kiểm thử.
  ```

---

## 2. UT-006-02 (Case 2: Happy / Edge - Combo chưa có công thức định lượng)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Happy / Edge`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 1 sản phẩm Combo hợp lệ (IsDeleted = false, Id = comboId, ProductName = "Empty Combo").
  - Bảng ComboSpecification không chứa bản ghi nào thuộc về comboId này (danh sách rỗng).
  ```

- **Input:**
  ```text
  Guid comboId = Guid.NewGuid();
  service.GetComboSpecificationAsync(comboId);
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về đối tượng ComboSpecificationResponse.
  - SubId bằng comboId.ToString().
  - Danh sách Materials trả về rỗng (Materials.Count == 0 / IsEmpty).
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P2`
- **Owner:** Backend Team
- **Rationale *:** 
  ```text
  Đảm bảo hệ thống xử lý an toàn, không bị crash (NullReference) đối với các sản phẩm Combo mới tạo chưa kịp cấu hình danh sách vật liệu cấu thành.
  ```

- **TEST_LINKS:**
  ```text
  US-006
  TDD-006
  ```

- **Ghi chú:**
  ```text
  Tương ứng với test case GetComboSpecification_NoMaterials_ReturnsEmptyMaterials trong mã nguồn kiểm thử.
  ```

---

## 3. UT-006-03 (Case 3: Happy - Tính toán MaxComboPossible và Sắp xếp Phụ liệu trước Chính liệu)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock gồm 1 Combo ("Full Combo") và 3 vật liệu:
    + coreMat1 (Vật liệu chính): AvailableForSale = 10, IsDeleted = false.
    + coreMat2 (Vật liệu chính): AvailableForSale = 40, IsDeleted = false.
    + supportMat (Phụ liệu): AvailableForSale = 0, IsDeleted = false.
  - Danh sách ComboSpecification cấu hình:
    + coreMat1: Quantity = 2, IsCore = true, IsActive = true.
    + coreMat2: Quantity = 5, IsCore = true, IsActive = true.
    + supportMat: Quantity = 1, IsCore = false, IsActive = true.
  ```

- **Input:**
  ```text
  Guid comboId = Guid.NewGuid();
  service.GetComboSpecificationAsync(comboId);
  ```

- **Expected output *:**
  ```text
  - Trả về đối tượng ComboSpecificationResponse với 3 vật liệu (Materials.Count == 3).
  - Sắp xếp theo Role tăng dần (false đứng trước true do OrderBy(x => x.Role)):
    + Materials[0].Role = false (Phụ liệu supportMat đứng đầu tiên).
    + Materials[1].Role = true, Materials[2].Role = true (Vật liệu chính coreMat1, coreMat2 đứng sau).
  - Giá trị MaxComboPossible của từng vật liệu được tính chính xác bằng floor(AvailableForSale / Quantity):
    + coreMat1: Math.Floor(10 / 2) = 5
    + coreMat2: Math.Floor(40 / 5) = 8
    + supportMat: AvailableForSale = 0 => MaxComboPossible = 0
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** Backend Team
- **Rationale *:** 
  ```text
  Xác nhận API tính toán chính xác khả năng cung ứng tối đa (MaxComboPossible) theo tồn kho của từng loại vật liệu và áp dụng đúng thứ tự ưu tiên hiển thị theo vai trò (Role: Phụ liệu -> Chính liệu).
  ```

- **TEST_LINKS:**
  ```text
  US-006
  TDD-006
  ```

- **Ghi chú:**
  ```text
  Tương ứng với test case GetComboSpecification_WithMaterials_CalculatesMaxComboAndSortsCorrectly trong mã nguồn kiểm thử.
  ```