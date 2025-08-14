using OrderManagement.BLL.DTO;
using OrderManagement.BLL.DTO.Product;
using OrderManagement.BLL.Interfaces;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderItemRepository _orderItemRepostitory;
        public DiscountService(IDiscountRepository discountRepository, IOrderItemRepository orderItemRepository)
        {
            _discountRepository = discountRepository;
            _orderItemRepostitory = orderItemRepository;
        }

        public async Task<DiscountDto> CreateDiscount(DiscountDto discountDto)
        {
            if(discountDto == null)
            {
                throw new Exception("There is no data of discount.");
            }

            Discount discount = new Discount
            {
                Name = discountDto.Name,
                Percentage = discountDto.Percentage,
                MinQuantity = discountDto.MinQuantity
            };
            await _discountRepository.CreateDiscount(discount);

            return discountDto;

        }

        public async Task<DiscountDto> DeleteDiscountByName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Wrong name, try one more time!");
            }

            var discount = await _discountRepository.DeleteDiscountByName(name);

            if(discount == null)
            {
                throw new Exception($"Discount with name: {name} was not found");
            }

            return new DiscountDto
            {
                Name = discount.Name,
                Percentage = discount.Percentage,
                MinQuantity = discount.MinQuantity
            };
        }

        public async Task<DiscountDto> FindDiscountByName(string name)
        {
            var discount = await _discountRepository.FindDiscountByName(name);

            if(discount == null)
            {
                throw new Exception($"No such discount with name: {name}");
            }

            return new DiscountDto
            {
                Name = discount.Name,
                Percentage = discount.Percentage,
                MinQuantity = discount.MinQuantity

            };
        }

        public async Task<DiscountDto> UpdateDiscount(DiscountDto discountDto)
        {
            if(discountDto == null)
            {
                throw new Exception("Discount data is empty");
            }

            Discount discount = new Discount
            {
                Name = discountDto.Name,
                Percentage = discountDto.Percentage,
                MinQuantity = discountDto.MinQuantity
            };

            await _discountRepository.UpdateDiscount(discount);

            return discountDto;
        }

        public async Task<List<ResponseProductDto>> GetDiscountReportByName(string name)
        {
            var orderItems = await _orderItemRepostitory.GetOrderItemsByDiscount(name);
            var products = new List<ResponseProductDto>();
            foreach (var orderItem in orderItems)
            {
                if (orderItem.Discount != null && orderItem.Discount.Name.Equals(name))
                {
                    ResponseProductDto product = new ResponseProductDto
                    {
                        Id = orderItem.ProductId,
                        Name = orderItem.Product.Name,
                        Description = orderItem.Product.Description,
                        Price = orderItem.UnitPrice,
                        DiscountId = orderItem.DiscountId,
                        Discount = orderItem.Discount
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        public double ApplyDiscount(int quantity, ResponseProductDto product)
        {
            if(product.Discount == null)
            {
                return 1.0;
            }

            if (quantity < product.Discount.MinQuantity)
            {
                return 1.0;
            }

            if (product.Discount.Percentage != null && product.Discount.Percentage >= 0 && product.Discount.Percentage <= 100)
            {
                return (100.0 - product.Discount.Percentage) / 100.0;
            }

            return 1.0;
        }
    }
}
