using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// =========================================================================
// TDD cho US-006: Xem công thức định lượng của combo hoa
// File test: UT-006-01
// =========================================================================

namespace HoaTheoMua.Service.Test.ComboTest
{
    public class ComboSpecification_UT_006_01_Test
    {
        [Fact]
        public async Task GetComboSpecification_ValidSubId_ReturnsCalculatedFormulaAndSortsByCore()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var comboId = Guid.NewGuid();
            var coreMat = new ProductEntity { Id = Guid.NewGuid(), NhanhProductId = "mat-01", AvailableForSale = 100 };
            var supportMat = new ProductEntity { Id = Guid.NewGuid(), NhanhProductId = "mat-02", AvailableForSale = 50 };

            var specs = new List<SpecificationEntity>
            {
                new SpecificationEntity { SubId = comboId.ToString(), MatId = supportMat.NhanhProductId, Quantity = 1, IsCore = false, IsActive = true, MaterialProduct = supportMat },
                new SpecificationEntity { SubId = comboId.ToString(), MatId = coreMat.NhanhProductId, Quantity = 2, IsCore = true, IsActive = true, MaterialProduct = coreMat }
            };

            await dbContext.Products.AddRangeAsync(coreMat, supportMat);
            await dbContext.Specifications.AddRangeAsync(specs);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetComboSpecificationAsync(comboId);

            // 3. Assert
            // CORE phải lên đầu
            Assert.True(result.Items[0].IsCore);
            
            // Tính số lượng bán = 100 / 2 = 50
            Assert.Equal(50, result.SellableQuantity);
        }

        [Fact]
        public async Task GetComboSpecification_NoCoreItems_CalculatesSellableQuantityAsZero()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var comboId = Guid.NewGuid();
            var supportMat = new ProductEntity { Id = Guid.NewGuid(), NhanhProductId = "mat-02", AvailableForSale = 100 };

            var specs = new List<SpecificationEntity>
            {
                new SpecificationEntity { SubId = comboId.ToString(), MatId = supportMat.NhanhProductId, Quantity = 1, IsCore = false, IsActive = true, MaterialProduct = supportMat }
            };

            await dbContext.Products.AddAsync(supportMat);
            await dbContext.Specifications.AddRangeAsync(specs);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetComboSpecificationAsync(comboId);

            // 3. Assert
            // Không có CORE => SellableQuantity = 0
            Assert.Equal(0, result.SellableQuantity);
        }

        [Fact]
        public async Task GetComboSpecification_CoreItemOutOfStock_CalculatesSellableQuantityAsZero()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var comboId = Guid.NewGuid();
            // Core material hết hàng (AvailableForSale = 0)
            var coreMat = new ProductEntity { Id = Guid.NewGuid(), NhanhProductId = "mat-01", AvailableForSale = 0 };

            var specs = new List<SpecificationEntity>
            {
                new SpecificationEntity { SubId = comboId.ToString(), MatId = coreMat.NhanhProductId, Quantity = 1, IsCore = true, IsActive = true, MaterialProduct = coreMat }
            };

            await dbContext.Products.AddAsync(coreMat);
            await dbContext.Specifications.AddRangeAsync(specs);
            await dbContext.SaveChangesAsync();

            // 2. Act
            var result = await service.GetComboSpecificationAsync(comboId);

            // 3. Assert
            Assert.Equal(0, result.SellableQuantity);
        }

        [Fact]
        public async Task GetComboSpecification_AllSpecificationsInactive_ThrowsExceptionNotFound()
        {
            // 1. Arrange
            using var dbContext = CreateInMemoryDbContext();
            var service = CreateService(dbContext);

            var comboId = Guid.NewGuid();
            var mat = new ProductEntity { Id = Guid.NewGuid(), NhanhProductId = "mat-01", AvailableForSale = 100 };

            var specs = new List<SpecificationEntity>
            {
                // Vật liệu đã bị disable (IsActive = false)
                new SpecificationEntity { SubId = comboId.ToString(), MatId = mat.NhanhProductId, Quantity = 1, IsCore = true, IsActive = false, MaterialProduct = mat }
            };

            await dbContext.Products.AddAsync(mat);
            await dbContext.Specifications.AddRangeAsync(specs);
            await dbContext.SaveChangesAsync();

            // 2. Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.GetComboSpecificationAsync(comboId));
            Assert.Equal("NOT_FOUND", ex.Message);
        }
    }
}
