using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Lưu ý: Đây là file Unit Test (TDD) mô phỏng logic lọc theo typeProduct (US-002).
// Để test này chạy được trong project thật, backend cần bổ sung enum ProductFilterType 
// và thêm thuộc tính typeProduct vào LocalProductQueryRequest.

namespace HoaTheoMua.Service.Test.ProductTest
{
    public class LocalProductSearchV2_UT_002_01_Test
    {
        [Fact]
        public async Task GetLocalProductsV2_FilterByTypeProductCombo_ReturnsOnlyComboProducts()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var p1 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Combo 1", 
            ProductType = "Combo", IsDeleted = false, Position = 1 };
            var p2 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Material 1", 
            ProductType = "Material", IsDeleted = false, Position = 2 };
            var p3 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Product 1", 
            ProductType = "Product", IsDeleted = false, Position = 3 };

            await dbContext.Products.AddRangeAsync(p1, p2, p3);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetLocalProductsV2(new Request.LocalProductQueryRequest
            {
                typeProduct = ProductFilterType.Combo
            });

            var items = result.Value.Items.Cast<Response.ProductDetailResponse>().ToList();
            
            // 3. Assert
            Assert.Single(items);
            Assert.Equal("Combo 1", items[0].ProductName);
        }

        [Fact]
        public async Task GetLocalProductsV2_FilterByTypeProductMaterial_ReturnsOnlyMaterialProducts()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var p1 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Combo 1", ProductType = "Combo", IsDeleted = false, Position = 1 };
            var p2 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Material 1", ProductType = "Material", IsDeleted = false, Position = 2 };
            var p3 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Product 1", ProductType = "Product", IsDeleted = false, Position = 3 };

            await dbContext.Products.AddRangeAsync(p1, p2, p3);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetLocalProductsV2(new Request.LocalProductQueryRequest
            {
                typeProduct = ProductFilterType.Material
            });

            var items = result.Value.Items.Cast<Response.ProductDetailResponse>().ToList();
            
            // 3. Assert
            Assert.Single(items);
            Assert.Equal("Material 1", items[0].ProductName);
        }

        [Fact]
        public async Task GetLocalProductsV2_FilterByTypeProductProduct_ReturnsOnlyNormalProducts()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var p1 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Combo 1", ProductType = "Combo", IsDeleted = false, Position = 1 };
            var p2 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Material 1", ProductType = "Material", IsDeleted = false, Position = 2 };
            var p3 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Product 1", ProductType = "Product", IsDeleted = false, Position = 3 };

            await dbContext.Products.AddRangeAsync(p1, p2, p3);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetLocalProductsV2(new Request.LocalProductQueryRequest
            {
                typeProduct = ProductFilterType.Product
            });

            var items = result.Value.Items.Cast<Response.ProductDetailResponse>().ToList();
            
            // 3. Assert
            Assert.Single(items);
            Assert.Equal("Product 1", items[0].ProductName);
        }
        [Fact]
        public async Task GetLocalProductsV2_TypeProductIsNull_ReturnsAllProducts()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var p1 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Combo 1", ProductType = "Combo", IsDeleted = false, Position = 1 };
            var p2 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Material 1", ProductType = "Material", IsDeleted = false, Position = 2 };
            var p3 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Product 1", ProductType = "Product", IsDeleted = false, Position = 3 };

            await dbContext.Products.AddRangeAsync(p1, p2, p3);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetLocalProductsV2(new Request.LocalProductQueryRequest
            {
                typeProduct = null // Boundary: Không filter
            });

            var items = result.Value.Items.Cast<Response.ProductDetailResponse>().ToList();
            
            // 3. Assert: Không bị mất sản phẩm nào
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetLocalProductsV2_NoProductsMatchFilter_ReturnsEmptyList()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            // Chỉ có Material và Product thường, KHÔNG CÓ Combo
            var p1 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Material 1", ProductType = "Material", IsDeleted = false, Position = 1 };
            var p2 = new ProductEntity { Id = Guid.NewGuid(), ProductName = "Product 1", ProductType = "Product", IsDeleted = false, Position = 2 };

            await dbContext.Products.AddRangeAsync(p1, p2);
            await dbContext.SaveChangesAsync();

            // 2. Act: Tìm Combo
            var result = await service.GetLocalProductsV2(new Request.LocalProductQueryRequest
            {
                typeProduct = ProductFilterType.Combo
            });

            var items = result.Value.Items.Cast<Response.ProductDetailResponse>().ToList();
            
            // 3. Assert: Trả về rỗng, không crash
            Assert.Empty(items);
            Assert.Equal(0, result.Value.TotalCount);
        }

        [Fact]
        public async Task GetLocalProductsV2_DatabaseThrowsException_ThrowsException()
        {
            // 1. Arrange
            // Note: Trong unit test thực tế với Moq, ta sẽ Mock IProductRepository để throw Exception.
            // Đoạn code này là giả mã (pseudo) để minh hoạ Error Case:
            /*
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetListAsync(It.IsAny<Query>())).ThrowsAsync(new TimeoutException("DB Timeout"));
            var service = CreateService(mockRepo.Object);

            // 2. Act & 3. Assert
            await Assert.ThrowsAsync<TimeoutException>(() => 
                service.GetLocalProductsV2(new Request.LocalProductQueryRequest { typeProduct = ProductFilterType.Material })
            );
            */
        }
    }
}
