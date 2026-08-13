# Các form Unit Test cho US-006 (Công thức Combo)

Dưới đây là 3 case (1 Error, 2 Happy/Edge) cho API Xem công thức định lượng (US-006). Bạn chỉ cần copy từng mục vào form hệ thống.

---

## 1. UT-006-01 (Case 1: Error - Không tìm thấy Combo)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Error / Exception`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB không có dữ liệu Combo nào hoặc Combo đã bị xoá (IsDeleted = true).
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId = Guid.NewGuid())
  ```

- **Expected output *:**
  ```text
  - Hàm NÉM RA EXCEPTION với message là "Không tìm thấy".
  - (Trên API sẽ bắt lỗi này và trả về HTTP 404 cho front-end).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P1`
- **Rationale *:** Xác nhận hệ thống chặn các thao tác xem định lượng đối với combo không tồn tại hoặc đã bị xóa.
- **TEST_LINKS:** US-006

---

## 2. UT-006-02 (Case 2: Happy - Combo rỗng, trả về danh sách rỗng)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB có dữ liệu Combo (IsDeleted = false).
  - Combo này chưa cấu hình bất kỳ vật liệu nào (ComboSpecification rỗng).
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về đúng thông tin SubId của Combo.
  - Danh sách Materials trả về rỗng (0 items).
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P2`
- **Rationale *:** Đảm bảo hệ thống xử lý đúng đối với các Combo mới tạo, chưa setup định lượng.
- **TEST_LINKS:** US-006

---

## 3. UT-006-03 (Case 3: Happy - Tính toán số lượng MaxCombo và Sắp xếp đúng Role)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB có 1 Combo với 3 vật liệu:
    + coreMat1: IsCore = true, Quantity = 2, AvailableForSale = 10 (MaxCombo = 5)
    + coreMat2: IsCore = true, Quantity = 5, AvailableForSale = 40 (MaxCombo = 8)
    + supportMat: IsCore = false, Quantity = 1, AvailableForSale = 0 (MaxCombo = 0)
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về danh sách gồm 3 vật liệu.
  - Kiểm tra tính toán khả năng cung ứng (MaxComboPossible) chính xác: coreMat1 = 5, coreMat2 = 8, supportMat = 0.
  - Kiểm tra thứ tự sắp xếp: Vật liệu phụ (IsCore = false) nằm trước vật liệu chính (IsCore = true) do logic OrderBy sắp xếp false -> true.
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Rationale *:** Xác nhận API tính toán đúng khả năng bán tối đa của từng vật liệu và áp dụng chính xác thuật toán sắp xếp (phụ liệu hiện trước, chính liệu hiện sau).
- **TEST_LINKS:** US-006