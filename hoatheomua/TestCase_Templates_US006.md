# Các form Unit Test cho US-006 (Công thức Combo)

Dưới đây là 4 case (1 Happy, 2 Edge, 1 Error) cho API Xem công thức định lượng (US-006). Bạn chỉ cần copy từng mục vào form hệ thống.

---

## 1. UT-006-01 (Case 1: Happy - Có vật liệu Core, tính đúng số lượng)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - Combo có 2 vật liệu đang Active (IsActive = true):
    + mat-01: IsCore = true, Quantity = 2, AvailableForSale = 100
    + mat-02: IsCore = false, Quantity = 1, AvailableForSale = 50
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về danh sách vật liệu đã được sắp xếp: vật liệu CORE (mat-01) phải nằm ở vị trí đầu tiên (index 0).
  - Thuộc tính SellableQuantity phải bằng 50 (vì mat-01 cần 2 cái để ra 1 combo, kho có 100 => 100 / 2 = 50).
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Rationale *:** Xác nhận API trả về danh sách công thức chính xác, xếp vật liệu Core lên đầu và tính toán khả năng bán đúng chuẩn.
- **TEST_LINKS:** US-006

---

## 2. UT-006-01 (Case 2: Edge - Combo không có vật liệu CORE)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Edge`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - Combo bị cấu hình thiếu lõi, chỉ có 1 vật liệu đang Active nhưng là phụ kiện:
    + mat-02: IsCore = false, Quantity = 1, AvailableForSale = 100
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, không văng lỗi.
  - Thuộc tính SellableQuantity trả về bằng 0 (do công thức không có vật liệu chính).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Rationale *:** Xác nhận hệ thống trả về số lượng bán = 0 nếu combo bị cấu hình sai (thiếu thành phần chính - CORE).
- **TEST_LINKS:** US-006

---

## 3. UT-006-01 (Case 3: Edge - Vật liệu CORE hết hàng)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Edge`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - Combo có vật liệu CORE đang Active, nhưng trong kho đã hết hàng:
    + mat-01: IsCore = true, Quantity = 1, AvailableForSale = 0
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, không văng lỗi.
  - Thuộc tính SellableQuantity trả về bằng 0.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Rationale *:** Đảm bảo khả năng cung ứng của combo hoa phụ thuộc hoàn toàn vào tồn kho của vật liệu chính.
- **TEST_LINKS:** US-006

---

## 4. UT-006-01 (Case 4: Error - Combo rỗng hoặc vật liệu bị vô hiệu hoá)

### Đơn vị kiểm thử
- **Module *:** Xem công thức định lượng của combo hoa
- **Unit under test *:** GetComboSpecificationAsync
- **Loại:** `Error / Exception`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - Combo không có bất kỳ vật liệu nào, HOẶC tất cả các vật liệu trong Combo đều đã bị vô hiệu hoá (IsActive = false).
  ```

- **Input:**
  ```text
  Gọi hàm GetComboSpecificationAsync(comboId)
  ```

- **Expected output *:**
  ```text
  - Hàm NÉM RA EXCEPTION với message là "NOT_FOUND".
  - (Trên API sẽ bắt lỗi này và trả về HTTP 404 cho front-end).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P1`
- **Rationale *:** Xác nhận hệ thống chặn các thao tác xem định lượng đối với combo trống (chưa setup) hoặc combo đã vô hiệu hoá toàn bộ vật liệu.
- **TEST_LINKS:** US-006
