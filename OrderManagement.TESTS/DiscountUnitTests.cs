using Moq;
using OrderManagement.BLL.DTO.Product;
using OrderManagement.BLL.Services;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using OrderManagement.DATA.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.TESTS
{
    public class DiscountUnitTests
    {

        private readonly Mock<IDiscountRepository> _mockDiscountRepository;
        private readonly Mock<IOrderItemRepository> _mockOrderItemRepository;
        private readonly DiscountService _discountService;

        public DiscountUnitTests()
        {
            _mockDiscountRepository = new Mock<IDiscountRepository>();
            _mockOrderItemRepository = new Mock<IOrderItemRepository>();
            _discountService = new DiscountService(_mockDiscountRepository.Object, _mockOrderItemRepository.Object);
        }

        [Fact]
        public async Task TestGetDiscountReportByName()
        {
            Discount discount = new Discount { Id = 1, Name = "Christmas", Percentage = 20, MinQuantity = 2 };
            var product1 = new Product {Id = 1, Name = "Tv", Price = 499.99};
            var product2 = new Product {Id = 2, Name = "Pc", Price = 399.99};
            var product3 = new Product {Id = 3, Name = "Laptop", Price = 259.99};
            var order1 = new Order { Id = 1 };
            var order2 = new Order { Id = 2 };

            List<OrderItem> orderItems = new List<OrderItem>
            {
                new OrderItem{
                    Id = 1,
                    OrderId = 1,
                    Order = order1,
                    ProductId = product1.Id,
                    Product = product1,
                    DiscountId = 1,
                    Discount = discount,
                    Quantity = 2,
                    UnitPrice = product1.Price,
                    TotalPrice = 0.8 * (2 * product1.Price)
                },
                new OrderItem{
                    Id = 2,
                    OrderId = 1,
                    Order = order1,
                    ProductId = product2.Id,
                    Product = product2,
                    DiscountId = 1,
                    Discount = discount,
                    Quantity = 1,
                    UnitPrice = product1.Price,
                    TotalPrice = 399.99
                },
                new OrderItem{
                    Id = 3,
                    OrderId = 2,
                    Order = order2,
                    ProductId = product3.Id,
                    Product = product3,
                    DiscountId = 1,
                    Discount = discount,
                    Quantity = 3,
                    UnitPrice = product1.Price,
                    TotalPrice = 0.8 * (3 * product3.Price)
                }
            };

            _mockOrderItemRepository
                .Setup(oi => oi.GetOrderItemsByDiscount(discount.Name))
                .ReturnsAsync(orderItems);

            var result = await _discountService.GetDiscountReportByName(discount.Name);
            int totalAmount = orderItems.Where(oi => oi.Quantity >= discount.MinQuantity).Sum(oi => oi.Quantity);
            double totalPrice = orderItems.Where(oi => oi.Quantity >= discount.MinQuantity).Sum(oi => oi.TotalPrice);

            Assert.NotNull(result);
            Assert.Equal(totalPrice, result.TotalAmount);
            Assert.Equal(5, result.TotalQuantity);
            Assert.Equal(product1.Name, result.Products[0].Name);
            Assert.Equal(product3.Name, result.Products[1].Name);
        }

        [Fact]
        public async Task CheckApplyDiscount_MeetsFirstDiscountRequirements()
        {
            var discount = new Discount { Id = 1, Name = "BlackFriday", Percentage = 30, MinQuantity = 3 };   
            var product1 = new ResponseProductDto { Id = 1, Name = "TV", DiscountId = 1, Discount = discount };
            
            var result = _discountService.ApplyDiscount(3, product1);            
           
            double discountPercentage = 1.0 - (discount.Percentage / 100.0);
            
            Assert.Equal(discountPercentage, result, 2);
        }

        [Fact]
        public async Task CheckApplyDiscount_MeetsSecondDiscountRequirements()
        {
            var discount = new Discount { Id = 2, Name = "Christmas", Percentage = 40, MinQuantity = 2 };
            var product = new ResponseProductDto { Id = 2, Name = "Pc", DiscountId = 1, Discount = discount };

            var result = _discountService.ApplyDiscount(3, product);


            double discountPercentage = 1.0 - (discount.Percentage / 100.0);

            Assert.Equal(discountPercentage, result, 2);

        }

        [Fact]
        public async Task CheckApplyDiscount_NotEnoughItems_FirstDiscountRequirements()
        {
            var discount = new Discount { Id = 1, Name = "BlackFriday", Percentage = 30, MinQuantity = 3 };
            var product = new ResponseProductDto { Id = 2, Name = "Pc", DiscountId = 1, Discount = discount };

            var result = _discountService.ApplyDiscount(1, product);


            double discountPercentage = 1.0 - (discount.Percentage / 100.0);

            Assert.Equal(1.0, result, 2);

        }

        [Fact]
        public async Task CheckApplyDiscount_NotEnoughItems_SecondDiscountRequirements()
        {
            var discount = new Discount { Id = 2, Name = "Christmas", Percentage = 40, MinQuantity = 2 };
            var product = new ResponseProductDto { Id = 2, Name = "Pc", DiscountId = 1, Discount = discount };

            var result = _discountService.ApplyDiscount(1, product);

            double discountPercentage = 1.0 - (discount.Percentage / 100.0);

            Assert.Equal(1.0, result, 2);
        }

        [Fact]
        public async Task CheckApplyDiscount_WhenDiscountIsNull()
        {
            var discount = new Discount { Id = 2, Name = "Christmas", Percentage = 40, MinQuantity = 2 };
            var product = new ResponseProductDto { Id = 2, Name = "Pc", DiscountId = 1, Discount = null };

            var result = _discountService.ApplyDiscount(5, product);

            Assert.Equal(1.0, result);
        }
    }
}
