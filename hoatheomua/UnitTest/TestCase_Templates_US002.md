# Các form Unit Test cho UT-002-01 (Đã điền đầy đủ 3 case)

Dưới đây là 3 form đã được viết sẵn cho 3 trường hợp lọc khác nhau (Combo, Material, Product) theo đúng chuẩn văn phong ngắn gọn. Bạn chỉ việc copy và paste từng khung nhé!

---

## 1. UT-002-01 (Case 1: Lọc Combo)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 3 sản phẩm với 3 loại khác nhau: "Combo 1" (ProductType="Combo"), "Thành phần 1" (ProductType="Thành phần"), "Sản phẩm 1" (ProductType="Sản phẩm").
  - typeProduct = ProductFilterType.Combo.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = ProductFilterType.Combo
  }
  ```

- **Expected output *:**
  ```text
  Trả về danh sách sản phẩm thành công, danh sách chứa duy nhất các sản phẩm thuộc loại "Combo".
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE` (hoặc `REGRESSION`)
- **Priority:** `P1`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Xác nhận luồng chính cho phép người dùng lọc và tìm kiếm chính xác danh sách các sản phẩm theo loại Combo.
  ```

- **TEST_LINKS:**
  ```text
  US-002
  TDD-002
  ```

- **Ghi chú:**
  ```text
  Xác nhận hệ thống filter chính xác loại sản phẩm Combo, không hiển thị lẫn lộn Vật liệu hay Sản phẩm thường.
  ```

---

## 2. UT-002-01 (Case 2: Lọc Material - Vật liệu)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 3 sản phẩm với 3 loại khác nhau: "Combo 1" (ProductType="Combo"), "Thành phần 1" (ProductType="Thành phần"), "Sản phẩm 1" (ProductType="Sản phẩm").
  - typeProduct = ProductFilterType.Material.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = ProductFilterType.Material
  }
  ```

- **Expected output *:**
  ```text
  Trả về danh sách sản phẩm thành công, danh sách chứa duy nhất các sản phẩm thuộc loại "Thành phần".
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Xác nhận luồng chính cho phép người dùng lọc và tìm kiếm chính xác danh sách các sản phẩm theo loại Vật liệu (Thành phần).
  ```

- **TEST_LINKS:**
  ```text
  US-002
  TDD-002
  ```

- **Ghi chú:**
  ```text
  Xác nhận hệ thống filter chính xác loại sản phẩm Vật liệu, không hiển thị lẫn lộn Combo hay Sản phẩm thường.
  ```

---

## 3. UT-002-01 (Case 3: Lọc Product - Sản phẩm thường)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 3 sản phẩm với 3 loại khác nhau: "Combo 1" (ProductType="Combo"), "Thành phần 1" (ProductType="Thành phần"), "Sản phẩm 1" (ProductType="Sản phẩm").
  - typeProduct = ProductFilterType.Product.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = ProductFilterType.Product
  }
  ```

- **Expected output *:**
  ```text
  Trả về danh sách sản phẩm thành công, danh sách chứa duy nhất các sản phẩm thuộc loại "Sản phẩm" thông thường.
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Xác nhận luồng chính cho phép người dùng lọc và tìm kiếm chính xác danh sách các sản phẩm thông thường (Product).
  ```

- **TEST_LINKS:**
  ```text
  US-002
  TDD-002
  ```

- **Ghi chú:**
  ```text
  Xác nhận hệ thống filter chính xác loại sản phẩm Sản phẩm thường, không hiển thị lẫn lộn Combo hay Vật liệu.
  ```

---

## 4. UT-002-01 (Case 4: Boundary - Không truyền typeProduct)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Boundary`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock tồn tại 3 sản phẩm với 3 loại khác nhau: "Combo 1", "Thành phần 1", "Sản phẩm 1".
  - request không truyền (hoặc truyền null) thuộc tính typeProduct.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = null
  }
  ```

- **Expected output *:**
  ```text
  Trả về thành công danh sách chứa TẤT CẢ 3 sản phẩm mà không thực hiện thao tác lọc loại sản phẩm nào.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Đảm bảo hệ thống vẫn hiển thị tất cả các loại sản phẩm khi người dùng không chọn filter.
  ```

- **TEST_LINKS:**
  ```text
  US-002
  ```

- **Ghi chú:**
  ```text
  Kiểm tra tính chịu lỗi và default behavior khi API nhận request rỗng ở parameter.
  ```

---

## 5. UT-002-01 (Case 5: Boundary - Không có dữ liệu thoả mãn)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Edge`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock CHỈ tồn tại các sản phẩm loại "Thành phần" và "Sản phẩm".
  - KHÔNG có sản phẩm nào loại "Combo" trong DB.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = ProductFilterType.Combo
  }
  ```

- **Expected output *:**
  ```text
  - Hàm vẫn chạy thành công (Status 200 OK).
  - Danh sách trả về là một mảng rỗng (Items = []).
  - Thuộc tính TotalCount = 0.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Xác nhận hệ thống không bị crash hoặc ném exception khi kết quả query rỗng.
  ```

- **TEST_LINKS:**
  ```text
  US-002
  ```

- **Ghi chú:**
  ```text
  Trường hợp người dùng filter một danh mục hoặc loại sản phẩm chưa có dữ liệu tồn tại trong kho.
  ```

---

## 6. UT-002-01 (Case 6: Error - Lỗi kết nối CSDL / Timeout)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách sản phẩm theo loại
- **Unit under test *:** GetLocalProductsV2
- **Loại:** `Exception / Error`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - Mock DB Context ném ra Exception (TimeoutException hoặc SqlException) khi gọi hành động query dữ liệu.
  ```

- **Input:**
  ```text
  request = {
    typeProduct = ProductFilterType.Material
  }
  ```

- **Expected output *:**
  ```text
  - Hệ thống ném ra exception nội bộ và API trả về HTTP Status 503 (Internal Server Error / Service Unavailable).
  - Bắn ra message lỗi "Hệ thống đang bận" theo thiết kế.
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** (Tên của bạn)
- **Rationale *:** 
  ```text
  Xác nhận hệ thống xử lý ngoại lệ an toàn, không bị rò rỉ dữ liệu (data leak) hoặc crash toàn ứng dụng khi kết nối CSDL có sự cố.
  ```

- **TEST_LINKS:**
  ```text
  US-002
  ```

- **Ghi chú:**
  ```text
  Phần này test khả năng bắt lỗi (Try-Catch) của service.
  ```
