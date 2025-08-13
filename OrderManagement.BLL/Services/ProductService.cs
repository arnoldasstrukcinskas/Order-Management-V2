using OrderManagement.BLL.DTO.Product;
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


        public async Task<CreateProductDto> AddProductAsync(CreateProductDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };
            await _productRepository.AddProductAsync(product);

            return productDto;
        }

        public async Task<CreateProductDto> DeleteProductAsync(int id)
        {
            var product = await _productRepository.DeleteProductAsync(id);

            return new CreateProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ResponseProductDto> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            if(product == null)
            {
                throw new Exception("Product was not found");
            }

            return new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountId = product.DiscountId,
                Discount = product.Discount,
            };
        }

        public async Task<List<ResponseProductDto>> GetProductsByName(string name)
        {
            var products = await _productRepository.GetProductsByName(name);
            
            if(products == null)
            {
                throw new Exception("Products was not found.");
            }

            List<ResponseProductDto> productsDto = new List<ResponseProductDto>();

            foreach(var product in products)
            {
                ResponseProductDto productDto = new ResponseProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountId = product.DiscountId,
                    Discount = product.Discount
                };

                productsDto.Add(productDto);
            }

            return productsDto;
        }

        public async Task<ResponseProductDto> SetDiscount(string productName, string discountName)
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

            return new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountId = product.DiscountId,
                Discount = product.Discount
            };
        }

        public async Task<CreateProductDto> UpdateProductAsync(CreateProductDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };
            await _productRepository.UpdateProductAsync(product);

            return productDto;
        }
    }
}
