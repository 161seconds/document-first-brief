# Các form Unit Test cho US-002 (GetLocalProductsV2)

Dưới đây là các form Unit Test chuẩn hóa khớp 100% với danh sách quản lý kiểm thử trên hệ thống.

---

## 1. UT-002-01 (xem và search list material)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** `GetLocalProductsV2`
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại các sản phẩm với Barcode, NhanhProductId, ProductName khác nhau:
    + P1: ProductName = "Hoa hồng đỏ Đà Lạt", ProductLabel = "Thành phần", IsDeleted = false
    + P2: ProductName = "Hoa cúc họa mi", ProductLabel = "Thành phần", IsDeleted = false
    + P3: ProductName = "Hoa hồng Ecuador", ProductType = "Sản phẩm", IsDeleted = false
  ```

- **Input:**
  ```text
  request = new Request.LocalProductQueryRequest {
      productType = "thành phần",
      search = "hồng"
  }
  ```

- **Expected output *:**
  ```text
  - Trả về BasePaginationResponse thành công.
  - Danh sách Items chỉ chứa P1 (vừa có ProductLabel chứa "thành phần" vừa khớp từ khóa search "hồng").
  - P2 bị loại vì không khớp search; P3 bị loại vì không phải loại "thành phần".
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** Quoc Bao
- **Rationale *:** 
  ```text
  Xác nhận luồng kết hợp giữa lọc theo loại thành phần (vật liệu) và tìm kiếm từ khóa hoạt động chính xác.
  ```
- **TEST_LINKS:** `US-002`, `TDD-002`

---

## 2. UT-002-02 (Lọc Product - sản phẩm thường)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** `GetLocalProductsV2`
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock có các sản phẩm:
    + P_IND: ProductType = "Sản phẩm", không có sản phẩm con.
    + P_MIXED (Cha): ProductType = "Sản phẩm", có con Con_1 (Combo) và Con_2 (Sản phẩm).
    + P_ALL_COMBO (Cha): ProductType = "Sản phẩm", có con Con_3 (Combo) và Con_4 (Combo).
    + P_COMBO: ProductType = "Combo", không có con.
  ```

- **Input:**
  ```text
  request = new Request.LocalProductQueryRequest {
      productType = "Sản phẩm"
  }
  ```

- **Expected output *:**
  ```text
  - Trả về danh sách gồm 2 sản phẩm: P_IND và P_MIXED.
  - P_ALL_COMBO bị loại trừ vì toàn bộ con là Combo (thuộc nhóm Combo).
  - P_COMBO bị loại trừ vì là loại Combo.
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** Quoc Bao
- **Rationale *:** 
  ```text
  Xác nhận hệ thống lọc chính xác các sản phẩm thông thường và loại trừ đúng nhóm Combo cha-con.
  ```
- **TEST_LINKS:** `US-002`, `TDD-002`

---

## 3. UT-002-02-01 (Lọc Material - vật liệu)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** `GetLocalProductsV2`
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock có các sản phẩm:
    + M1: ProductLabel = "Thành phần 1", IsDeleted = false
    + M2: ProductLabel = "Vật liệu và thành phần", IsDeleted = false
    + P1: ProductLabel = "Hoa hồng", ProductType = "Sản phẩm", IsDeleted = false
  ```

- **Input:**
  ```text
  request = new Request.LocalProductQueryRequest {
      productType = "thành phần"
  }
  ```

- **Expected output *:**
  ```text
  - Trả về danh sách gồm đúng 2 vật liệu: M1 và M2 (khớp điều kiện LIKE %thành phần%).
  - TotalCount = 2.
  - Không chứa sản phẩm P1.
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** Quoc Bao
- **Rationale *:** 
  ```text
  Xác nhận hệ thống lọc chính xác danh sách vật liệu cấu thành dựa trên ProductLabel.
  ```
- **TEST_LINKS:** `US-002`, `TDD-002`

---

## 4. UT-002-03 (kh truyền typeProduct)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** `GetLocalProductsV2`
- **Loại:** `Boundary`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 3 sản phẩm hợp lệ với các loại khác nhau (Combo, Thành phần, Sản phẩm thường), IsDeleted = false và không phải sản phẩm con.
  ```

- **Input:**
  ```text
  request = new Request.LocalProductQueryRequest {
      productType = null
  }
  ```

- **Expected output *:**
  ```text
  - Trả về thành công danh sách chứa toàn bộ 3 sản phẩm mà không lọc theo loại.
  - TotalCount = 3.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** Quoc Bao
- **Rationale *:** 
  ```text
  Đảm bảo hành vi mặc định (default behavior) khi không truyền tham số productType thì hiển thị đầy đủ danh sách sản phẩm.
  ```
- **TEST_LINKS:** `US-002`

---

## 5. UT-002-04 (kh có dữ liệu thỏa mãn)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** `GetLocalProductsV2`
- **Loại:** `Edge`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock chỉ tồn tại các sản phẩm loại "Sản phẩm thường" và "Thành phần".
  - Không tồn tại bất kỳ sản phẩm nào thuộc loại "Combo".
  ```

- **Input:**
  ```text
  request = new Request.LocalProductQueryRequest {
      productType = "Combo"
  }
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, không ném exception hay lỗi.
  - TotalCount = 0.
  - Danh sách Items trả về là một mảng rỗng (Items = []).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** Quoc Bao
- **Rationale *:** 
  ```text
  Xác nhận hệ thống xử lý mượt mà khi kết quả truy vấn rỗng, không gây lỗi 500 hay crash hệ thống.
  ```
- **TEST_LINKS:** `US-002`
