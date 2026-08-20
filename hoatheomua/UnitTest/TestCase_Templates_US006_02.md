# Các form Unit Test cho tính năng Lấy danh sách vật liệu (GetMaterialsAsync)

Mô tả các kịch bản kiểm thử Unit Test cho hàm `GetMaterialsAsync` trong `Product.Service.cs`. 
API này lọc ra các sản phẩm có `ProductLabel.ToLower() == "thành phần"` không bị xóa (`!IsDeleted`) và hỗ trợ phân trang (Pagination).

---

## 1. UT-MAT-01 (Case 1: Happy - Lọc chính xác theo nhãn ProductLabel)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách vật liệu (Thành phần)
- **Unit under test *:** GetMaterialsAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock có 4 sản phẩm:
    + Material 1: ProductLabel = "Thành phần", IsDeleted = false, ProductName = "Hoa Hồng Cành"
    + Material 2: ProductLabel = "thành phần", IsDeleted = false, ProductName = "Giấy Gói Hàn Quốc"
    + Combo 1: ProductLabel = "Combo", IsDeleted = false, ProductName = "Bó Hoa Tình Yêu"
    + Deleted Material: ProductLabel = "Thành phần", IsDeleted = true, ProductName = "Ruy Băng Đỏ"
  ```

- **Input:**
  ```text
  request = new Request.GetMaterialsRequest {
      PageIndex = 1,
      PageSize = 10
  }
  ```

- **Expected output *:**
  ```text
  - Trả về BasePaginationResponse thành công.
  - PageIndex = 1, PageSize = 10, TotalCount = 2.
  - Danh sách Items trả về chứa đúng 2 vật liệu (Material 1 và Material 2).
  - Không chứa sản phẩm Combo 1 (khác nhãn) và Deleted Material (đã bị xóa mềm).
  ```

### Phân loại và trách nhiệm
- **Suite:** `SMOKE`
- **Priority:** `P1`
- **Owner:** Backend Team
- **Rationale *:** 
  ```text
  Xác nhận hàm lọc chính xác các bản ghi có nhãn ProductLabel là "thành phần" (không phân biệt hoa/thường), đồng thời loại bỏ các bản ghi đã bị xóa mềm hoặc sai nhãn.
  ```

- **TEST_LINKS:**
  ```text
  US-006
  API-MAT-01
  ```

---

## 2. UT-MAT-02 (Case 2: Happy - Phân trang hoạt động chính xác)

### Đơn vị kiểm thử
- **Module *:** Xem và tìm kiếm danh sách vật liệu (Thành phần)
- **Unit under test *:** GetMaterialsAsync
- **Loại:** `Happy`

### Dữ liệu test
- **Precondition / Mock setup:**
  ```text
  - DB Mock có 15 sản phẩm hợp lệ thỏa mãn (ProductLabel = "Thành phần", IsDeleted = false).
  ```

- **Input:**
  ```text
  request = new Request.GetMaterialsRequest {
      PageIndex = 2,
      PageSize = 5
  }
  ```

- **Expected output *:**
  ```text
  - Trả về BasePaginationResponse với:
    + PageIndex = 2
    + PageSize = 5
    + TotalCount = 15
  - Danh sách Items ở trang 2 có chính xác 5 phần tử (bỏ qua 5 phần tử đầu tiên của trang 1).
  ```

### Phân loại và trách nhiệm
- **Suite:** `REGRESSION`
- **Priority:** `P2`
- **Owner:** Backend Team
- **Rationale *:** 
  ```text
  Đảm bảo thuật toán phân trang (Skip & Take) hoạt động chính xác theo đúng PageIndex và PageSize mà client yêu cầu.
  ```

- **TEST_LINKS:**
  ```text
  US-006
  API-MAT-01
  ```
