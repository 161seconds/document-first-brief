# Các form Unit Test cho tính năng Lấy danh sách vật liệu (Thành phần)

**Mục tiêu:**
Mô tả các kịch bản kiểm thử Unit Test cho hàm `GetMaterialsAsync` thuộc `ProductController` / `Service.cs`.
API này dùng để lấy danh sách các vật liệu (Product có ProductLabel = "Thành phần") phục vụ cho việc chọn lựa khi cấu hình công thức Combo, được hỗ trợ phân trang (Pagination).

---

## 1. UT-MAT-01 (Case 1: Happy - Lọc chính xác theo ProductLabel)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách vật liệu (Thành phần)
- **Unit under test *:** GetMaterialsAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB có 4 sản phẩm:
    + Material 1: ProductLabel = "Thành phần", IsDeleted = false
    + Material 2: ProductLabel = "thành phần", IsDeleted = false (chữ thường)
    + Combo 1: ProductLabel = "Combo", IsDeleted = false
    + Deleted Material: ProductLabel = "Thành phần", IsDeleted = true
  ```

- **Input:**
  ```text
  Gọi hàm GetMaterialsAsync(request = { PageIndex = 1, PageSize = 10 })
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về PageIndex = 1.
  - Danh sách trả về (Items) khác rỗng (chứa Material 1 và Material 2).
  - Các sản phẩm có nhãn khác ("Combo") hoặc đã xóa (IsDeleted = true) bị loại bỏ khỏi danh sách.
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Rationale *:** Xác nhận API lấy danh sách vật liệu hoạt động chính xác dựa trên việc phân biệt nhãn "Thành phần", đồng thời bỏ qua các sản phẩm đã xoá hoặc sai loại.

---

## 2. UT-MAT-02 (Case 2: Happy - Phân trang hoạt động chính xác)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách vật liệu (Thành phần)
- **Unit under test *:** GetMaterialsAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB có 15 sản phẩm hợp lệ:
    + Tất cả đều có ProductLabel = "Thành phần", IsDeleted = false.
  ```

- **Input:**
  ```text
  Gọi hàm GetMaterialsAsync(request = { PageIndex = 2, PageSize = 5 })
  ```

- **Expected output *:**
  ```text
  - Hàm thực thi thành công, trả về đúng PageIndex = 2.
  - Danh sách Items trả về ở trang 2 có đúng số lượng phần tử bằng 5 (đúng với PageSize).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Rationale *:** Đảm bảo tính năng phân trang (Pagination) hoạt động đúng, giới hạn chính xác số lượng dữ liệu trả về theo thông số request gửi lên.
