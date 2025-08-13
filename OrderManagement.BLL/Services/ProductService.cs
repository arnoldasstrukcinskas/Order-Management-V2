using OrderManagement.BLL.DTO;
using OrderManagement.BLL.Interfaces;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        public ProductService(IProductRepository productRepository, IDiscountRepository discountRepository)
        {
            _productRepository = productRepository;
            _discountRepository = discountRepository;
        }


        public async Task<Product> AddProductAsync(ProductDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };
            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
           return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _productRepository.GetProductByName(name);
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await _productRepository.GetProductsByName(name);
        }

        public async Task<Product> SetDiscount(string productName, string discountName)
        {
            var product = await _productRepository.GetProductByName(productName);
            var discount = await _discountRepository.FindDiscountByName(discountName);

            if (product == null)
            {
                throw new Exception("Product or discount was not found");
            }
            else if(discount == null)
            {
                throw new Exception("Discount was not found");
            }

            product.Discount = discount;
            await _productRepository.UpdateProductAsync(product);

            return product;
        }

        public async Task<Product> UpdateProductAsync(ProductDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };
            return await _productRepository.UpdateProductAsync(product);
        }
    }
}
