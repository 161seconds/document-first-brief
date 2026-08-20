### **US 002:** **Xem, tìm kiếm và filter theo sản phẩm có nhãn là Vật liệu(Giống api cũ thêm field lọc)**
**URL**: **GET** /api/v2/products
**Request:**
- pageIndex (integer, optional): Chỉ số trang (1).
- pageSize (integer, optional): Kích thước trang (10).
- search (string, optional): Từ khóa tìm kiếm.
- typeProduct(enum): Search theo thành phần: combo, thành phần, sản phẩm
- priceFrom (number, optional): Giá từ.
- priceTo (number, optional): Giá đến.
- sortBy (array[string], optional): Sắp xếp (["price:asc"]).
- categoryIds (array[string], optional): Danh sách ID danh mục.
- isPin (boolean, optional): Sản phẩm ghim (true/false).
**Response**:
{
  "value": {
    "items": [
      {
        "id": "2066d62d-c314-4ee0-bbbd-90865e3779a6",
        "isPinned": true,
        "sourceRowNumber": 530,
        "nhanhProductId": "4933625",
        "parentProductId": null,
        "parentProductCode": null,
        "parentProductName": null,
        "barcode": "2000214269499",
        "productType": "Combo",
        "productCode": "HB_JBK_2026",
        "productName": "Hồng Julibee Bó Kiểu Mặt Xinh_2026",
        "otherName": null,
        "unit": "set",
        "coverImage": "https://pos.nvncdn.com/281991-126981/ps/20251229_0yynbkmHKI.jpeg?v=1766977492",
        "otherImages": null,
        "warrantyMonths": null,
        "weight": 11100,
        "importPrice": 67792,
        "importVat": null,
        "importPriceWithVat": 67792,
        "importVatMode": "Giá chưa bao gồm VAT",
        "price": 587000,
        "priceWithVat": 587000,
        "branchPrice": null,
        "branchPriceWithVat": null,
        "saleVatMode": "Giá chưa bao gồm VAT",
        "oldPrice": null,
        "vat": null,
        "profitPercentage": 88.4511073253833,
        "costPrice": 67792,
        "wholesalePrice": null,
        "wholesalePriceWithVat": null,
        "wholesaleBranchPrice": null,
        "wholesaleBranchPriceWithVat": null,
        "totalStock": 3,
        "stockTransferring": 0,
        "stock": 3,
        "deliveringStock": 0,
        "warehouseStock": null,
        "stockError": 0,
        "held": 0,
        "availableForSale": 3,
        "warrantyStock": 0,
        "heldComponents": 0,
        "pendingImportStock": null,
        "preOrder": null,
        "status": "Mới",
        "categoryCode": "SPTK",
        "categoryName": "HOA BÓ",
        "internalCategoryCode": null,
        "brand": null,
        "nhanhCategoryId": null,
        "nhanhBrandId": null,
        "nhanhTypeId": null,
        "supplier": null,
        "warrantyAddress": null,
        "slug": "/hong-julibee-bo-kieu-mat-xinh2026-p4933625.html",
        "length": null,
        "width": null,
        "height": null,
        "webOrder": null,
        "nhanhCreatedAt": "2025-12-26T04:14:17+00:00",
        "nhanhUpdatedAt": null,
        "productLabel": null,
        "branchPriceDuplicate": null,
        "wholesaleBranchPriceDuplicate": null,
        "priceLabel": null,
        "size": null,
        "color": null,
        "origin": null,
        "createdAt": "2026-07-24T13:30:35.204578+00:00",
        "updatedAt": "2026-08-07T09:43:43.490135+00:00",
        "categories": [
          {
            "categoryId": "ec6a8946-2bba-46fe-96e2-f932ae5e1135",
            "isPinned": false,
            "name": "Hoa bó",
            "name_En": "Hoa bó",
            "description": "Hoa bó",
            "description_En": "Hoa bó",
            "position": null,
            "metaData": "{\"isFilterable\": false}",
            "page": [],
            "startTime": null,
            "endTime": null,
            "type": "Fix",
            "isActive": true,
            "userId": "9ea83b22-cd9f-45c0-b8dd-e1c4e25ada11",
            "parentId": "d4fe681e-8cfd-424d-bd77-120b94d04a3b",
            "parent": null,
            "children": [],
            "productCount": null,
            "productIds": null
          }
        ]
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalItems": 6,
    "totalPages": 1
  },
  "isSuccess": true,
  "isFaild": false,
  "error": "Lấy danh sách sản phẩm thành công",
  "traceId": null,
  "timestampUtc": "0001-01-01T00:00:00"
}

### **US-006: Xem công thức định lượng của một combo hoa**
**URL**: **GET** /api/v1/products/{id}/combo-specification
**Request**:  **GET** /api/v1/products/2066d62d-c314-4ee0-bbbd-90865e3779a6/combo-specification

**Lưu ý:** Role trả int: Core là 1, Support là 2

**Response**:
{
  "value": {
    "subId": "4933388",
    "productCode": "HB_JBK_2026",
    "productName": "Hồng Julibee Bó Kiểu Mặt Xinh_2026",
    "materials": [
      {
        "materialId": "4933640",
        "materialName": "Hoa hồng Julibee",
        "role": "1",
        "quantityPerCombo": 10,
        "unit": "Cành",
        "availableForSale": 50,
        "maxComboPossible": 5,
      },
      {
        "materialId": "4933640",
        "materialName": "Hoa lá phụ Cẩm Chướng",
        "role": "1",
        "quantityPerCombo": 5,
        "unit": "Cành",
        "availableForSale": 40,
        "maxComboPossible": 8,
      },
      {
        "materialId": "4933640",
        "materialName": "Giấy gói hoa Hàn Quốc",
        "role": "2",
        "quantityPerCombo": 2,
        "unit": "Tờ",
        "availableForSale": 30,
        "maxComboPossible": 15,
      },
      {
        "materialId": "4933640",
        "role": "2",
        "roleName": "Phụ kiện",
        "quantityPerCombo": 1,
        "unit": "Sợi",
        "availableForSale": 100,
        "maxComboPossible": 100,
      }
    ]
  },
   "isSuccess": true,
  "isFaild": false,
  "error": "Tải công thức combo hoa thành công",
  "traceId": null,
  "timestampUtc": "0001-01-01T00:00:00"
}

**Error code**

| HTTP Code | Internal Code         | Message Tiếng Việt                                               |
| --------- | --------------------- | ---------------------------------------------------------------- |
| 400       | RECIPE_INVALID_INPUT  | Dữ liệu đầu vào không hợp lệ                                     |
| 404       | COMBO_NOT_FOUND       | Không tìm thấy combo hoa                                         |
| 404       | RECIPE_NOT_FOUND      | Combo hoa chưa có công thức, vui lòng thêm vật liệu              |
| 422       | MATERIAL_OUT_OF_STOCK | Cảnh báo: Tồn kho vật liệu không đủ để lắp ráp                   |
| 500       | SYSTEM_ERROR          | Không thể tải dữ liệu công thức combo hoa. Vui lòng thử lại sau. |
| 503       | SERVICE_UNAVAILABLE   | Dịch vụ tính toán kho tạm thời không khả dụng.                   |

### **US-007: Thêm vật liệu kèm số lượng và vai trò vào công thức của combo hoa**
**URL**: **POST** /api/v1/products/{id}/combo-specification
//thêm product có tag là vật liệu
//thêm mảng
**Request**:
{
  "subId": "",
  "materials": [
    {
      "matId": "mat-001",
      "quantity": 10,
      "isCore": true,
      "isActive": true
    },
    {
      "matId": "mat-002",
      "quantity": 2,
      "isCore": false,
      "isActive": true
    }
  ]
}
**Response**:
{
  "value": "Lưu công thức và tính lại số lượng có thể bán cho các size thành công"
  "isSuccess": true,
  "isFaild": false,
  "error": null.
  "traceId": null,
  "timestampUtc": "0001-01-01T00:00:00"
}

 **Error Codes**

| Status Code | Error Code / Category | Nội dung Message |
|-------------|------------------------|------------------|
| 200 OK | SUCCESS | Lưu công thức và tính lại số lượng có thể bán cho các size thành công |
| 400 Bad Request | DUPLICATE_MATERIAL | Vật liệu [{materialId}] đã tồn tại trong công thức, không thể thêm trùng |
| 400 Bad Request | INVALID_QUANTITY | Định lượng các hoa thành phần của Size S phải lớn hơn 0 |
| 400 Bad Request | INVALID_FACTOR | Hệ số quy đổi phải nằm trong khoảng từ 0.1 đến 10.0 |
| 404 Not Found | COMBO_NOT_FOUND | Không tìm thấy thông tin Combo hoa cha hoặc sản phẩm con tương ứng |
| 404 Not Found | MATERIAL_NOT_FOUND | Không tìm thấy vật liệu [{materialId}] trong hệ thống |
| 500 Internal Server Error | SYSTEM_ERROR | Lỗi hệ thống khi cập nhật công thức combo. Vui lòng thử lại sau. |

### **US-008: Sửa định lượng hoặc vai trò của vật liệu trong công thức**
**URL**:  **PUT** /api/v1/products/{id}/combo-specification/items
//update mảng
**Request**:
{
  "subId": "",
  "materials": [
    {
      "materialId": "mat-001",
      "quantity": 15,
      "role": 1
    },
    {
      "materialId": "mat-002",
      "quantity": 2,
      "role": 2
    }
  ]
}
**Response**:
{
  "value": "Cập nhật vật liệu trong công thức thành công"
  "isSuccess": true,
  "isFaild": false,
  "error": null.
  "traceId": null,
  "timestampUtc": "0001-01-01T00:00:00"
}
**Error Codes**

| Status Code               | ErrorCode / Category                | Nội dung Message                                                    |
| ------------------------- | ----------------------------------- | ------------------------------------------------------------------- |
| 200 OK                    | SUCCESS                             | Cập nhật vật liệu trong công thức thành công                        |
| 400 Bad Request           | INVALID_QUANTITY                    | Định lượng phải là số nguyên dương lớn hơn 0                        |
| 404 Not Found             | MATERIAL_NOT_IN_RECIPE              | Vật liệu [{materialId}] không tồn tại trong công thức của Combo này |
| 409 Conflict              | SELLABLE_QUANTITY_DECREASED_WARNING | Cảnh báo: Số lượng có thể bán bị giảm từ {old} xuống {new}          |
| 422 Unprocessable Entity  | CORE_MATERIAL_REQUIRED              | Công thức combo phải chứa ít nhất một vật liệu vai trò CORE         |
| 500 Internal Server Error | SYSTEM_ERROR                        | Lỗi hệ thống khi chỉnh sửa công thức. Vui lòng thử lại sau.         |
### **US-009:** **Xóa vật liệu khỏi công thức hoa**
**URL**:  **DELETE** /api/v1/products/{id}/combo-specification/items
//xóa mảng
**Request:**
{
  "subId": "4933640",
  "materialIds": [
    "4933943",
    "4932711"
  ]
}
**Response**:
{
  "value": "Xóa vật liệu khỏi công thức thành công"
  "isSuccess": true,
  "isFaild": false,
  "error": null.
  "traceId": null,
  "timestampUtc": "0001-01-01T00:00:00"
}

 **Error Codes**

| Status Code | ErrorCode / Category | Nội dung Message |
|-------------|----------------------|------------------|
| 200 OK | SUCCESS | Xóa vật liệu khỏi công thức thành công |
| 400 Bad Request | INVALID_INPUT | Danh sách vật liệu cần xóa không được để trống |
| 404 Not Found | MATERIAL_NOT_FOUND_IN_RECIPE | Dòng vật liệu chọn xóa không còn tồn tại trong công thức |
| 422 Unprocessable Entity | CORE_MATERIAL_REQUIRED | Công thức combo phải còn ít nhất một vật liệu vai trò CORE |
| 500 Internal Server Error | SYSTEM_ERROR | Lỗi hệ thống khi xóa vật liệu khỏi công thức. Vui lòng thử lại sau. |

## US-013: đang confirm với anh Danh
### US-013, US-014, US-015: đang confirm với anh Danh
Anh Danh đảm nhận phần user


```CSharp
public IQueryable<Product> ApplyTypeFilter(
    IQueryable<Product> query,
    ProductFilterType? typeProduct)
{
    if (typeProduct is null)
    {
        return query;
    }

    return typeProduct.Value switch
    {
        ProductFilterType.Combo => query.Where(product =>
            // Sản phẩm có con và tất cả các con đều là Combo
            (
                product.Children.Any() &&
                product.Children.All(child =>
                    child.ProductType == ProductType.Combo)
            )
            ||
            // Sản phẩm độc lập có ProductType là Combo
            (
                !product.Children.Any() &&
                product.ProductType == ProductType.Combo
            )),

        ProductFilterType.Material => query.Where(product =>
            // Material là sản phẩm độc lập
            !product.Children.Any() &&
            product.ProductType == ProductType.Material
        ),

        ProductFilterType.Product => query.Where(product =>
            // Sản phẩm có con nhưng không phải tất cả con đều là Combo
            (
                product.Children.Any() &&
                product.Children.Any(child =>
                    child.ProductType != ProductType.Combo)
            )
            ||
            // Sản phẩm độc lập có ProductType là Product
            (
                !product.Children.Any() &&
                product.ProductType == ProductType.Product
            )),

        _ => query
    };
}
```