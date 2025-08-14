using Moq;
using OrderManagement.BLL.Services;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System.Diagnostics;
using System.Xml.Linq;


namespace OrderManagement.TESTS
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddProductToDatabaseTest()
        {
            var mockProductRepository = new Mock<IProductRepository>();

            var fakeProduct = new Product
            {
                Id = 1,
                Name = "example",
                Description = "Very long description",
                Price = 9.99,
                DiscountId = null,
                Discount = null
            };

            mockProductRepository
                .Setup(r => r.AddProductAsync(fakeProduct))
                .ReturnsAsync(fakeProduct);

            var result = await mockProductRepository.Object.AddProductAsync(fakeProduct);
            Assert.Equal("example", result.Name);
            Assert.Equal("Very long description", result.Description);
            Assert.Equal(9.99, result.Price);
            Assert.Null(result.Discount);

            Assert.NotNull(result);
        }
    }
}