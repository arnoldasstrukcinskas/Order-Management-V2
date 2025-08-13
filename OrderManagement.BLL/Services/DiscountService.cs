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
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
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

        public async Task<DiscountDto> DeleteDiscountById(int id)
        {
            if(id <= 0)
            {
                throw new Exception("Wrong Id, try one more time!");
            }

            var discount = await _discountRepository.DeleteDiscountById(id);

            if(discount == null)
            {
                throw new Exception($"Discount with id: {id} was not found");
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

        public double ApplyDiscount(int quantity, ResponseProductDto product)
        {
            double percentage = (100.0 - product.Discount.Percentage) / 100.0;

            if(quantity >= product.Discount.MinQuantity)
            {
                return percentage;
            }

            return 1.0;
        }
    }
}
