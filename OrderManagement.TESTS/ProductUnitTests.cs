using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using OrderManagement.BLL.DTO;
using OrderManagement.BLL.DTO.Product;
using OrderManagement.BLL.Services;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System.Diagnostics;
using System.Xml.Linq;


namespace OrderManagement.TESTS
{
    public class ProductUnitTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IDiscountRepository> _mockDiscountRepository;
        private readonly ProductService _productService;

        public ProductUnitTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockDiscountRepository = new Mock<IDiscountRepository>();
            _productService = new ProductService(_mockProductRepository.Object, _mockDiscountRepository.Object);
        }
          
        [Fact]
        public async Task AddProductToDatabaseTest()
        {

            var fakeProduct = new Product
            {
                Id = 1,
                Name = "example",
                Description = "Very long description",
                Price = 9.99,
            };

            var fakeProductDto = new CreateProductDto
            {
                Name = fakeProduct.Name,
                Description = fakeProduct.Description,
                Price = fakeProduct.Price
            };

            _mockProductRepository
                .Setup(p => p.AddProductAsync(fakeProduct))
                .ReturnsAsync(fakeProduct);

            var result = await _productService.AddProductAsync(fakeProductDto);
            Assert.NotNull(result);
            Assert.Equal("example", result.Name);
            Assert.Equal("Very long description", result.Description);
            Assert.Equal(9.99, result.Price);
        }

        [Fact]
        public async Task GetProductByNameTest()
        {
            string name = "tv";

            var fakeProduct = new Product
            {
                Id = 1,
                Name = name,
                Description = "Very long description",
                Price = 99.99,
                DiscountId = null,
                Discount = null
            };

            _mockProductRepository
                .Setup(p => p.GetProductByName(name))
                .ReturnsAsync(fakeProduct);

            var result = await _productService.GetProductByName(name);
            Assert.NotNull(result);
            Assert.Equal(fakeProduct.Name, result.Name);
            Assert.Equal(fakeProduct.Description, result.Description);
            Assert.Equal(fakeProduct.Price, result.Price);
            Assert.Null(result.Discount);
        }

        [Fact]
        public async Task SetDiscountTest()
        {
            var discount = new Discount
            {
                Name = "Christmas",
                Percentage = 20,
                MinQuantity = 2
            };

            var product = new Product
            {
                Id = 1,
                Name = "pc",
                Description = "Best pc ever",
                Price = 49.99,
            };

            _mockProductRepository.Setup(p => p.GetProductByName(product.Name)).ReturnsAsync(product);
            _mockDiscountRepository.Setup(d => d.FindDiscountByName(discount.Name)).ReturnsAsync(discount);

            var result = await _productService.SetDiscount(product.Name, discount.Name);

            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.DiscountId, result.DiscountId);
            Assert.Equal(product.Discount.Name, result.Discount.Name);
            Assert.Equal(product.Discount, result.Discount);
        }
    }
}