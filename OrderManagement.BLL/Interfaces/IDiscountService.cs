using OrderManagement.BLL.DTO;
using OrderManagement.BLL.DTO.Product;
using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountDto> CreateDiscount(DiscountDto discountDto);
        Task<DiscountDto> UpdateDiscount(DiscountDto discountDto);
        Task<DiscountDto> FindDiscountByName(string name);
        Task<DiscountDto> DeleteDiscountByName(string name);
        Task<List<ResponseProductDto>> GetDiscountReportByName(string name);
        double ApplyDiscount(int quantity, ResponseProductDto product);
    }
}
