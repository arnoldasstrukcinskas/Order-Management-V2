using OrderManagement.BLL.DTO;
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
        Task <Product> AddProductAsync(ProductDto productDto);
        Task <Product> UpdateProductAsync(ProductDto productDto);
        Task <Product> DeleteProductAsync(int id);
        Task <Product> GetProductByName(string name);
        Task <List<Product>> GetProductsByName(string name);
    }
}
