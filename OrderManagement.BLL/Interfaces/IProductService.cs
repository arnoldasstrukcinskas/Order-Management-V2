using OrderManagement.BLL.DTO.Product;
using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.Interfaces
{
    public interface IProductService
    {
        Task<CreateProductDto> AddProductAsync(CreateProductDto productDto);
        Task<CreateProductDto> UpdateProductAsync(CreateProductDto productDto);
        Task<CreateProductDto> DeleteProductAsync(int id);
        Task<ResponseProductDto> GetProductByName(string name);
        Task<List<ResponseProductDto>> GetProductsByName(string name);
        Task<ResponseProductDto> SetDiscount(string productName, string discountName);
    }
}
