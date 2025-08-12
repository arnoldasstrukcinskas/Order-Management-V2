using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.BLL.DTO;
using OrderManagement.BLL.Interfaces;
using OrderManagement.BLL.Services;

namespace OrderManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Adds product to postgreSql database.
        /// </summary>
        /// <returns>Added product object with ID, name, description, and price.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var product = await _productService.AddProductAsync(productDto);

            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Failed to add product.");
            }
        }


        /// <summary>
        /// Retrieves a product by its name from the order management system.
        /// </summary>
        /// <param name="Enter product name">The name of the product to retrieve.</param>
        /// <returns>A product object with ID, name, description, and price.</returns>
        [HttpGet("single")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _productService.GetProductByName(name);

            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Product not found.");
            }
        }

        /// <summary>
        /// Retrieves a products by their name from the order management system.
        /// </summary>
        /// <param name="Enter products name">The name of the products to retrieve.</param>
        /// <returns>A product objects with IDs, names, descriptions, and prices.</returns>
        [HttpGet("multiple")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var products = await _productService.GetProductsByName(name);

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest("Products not found");
            }
        }

        /// <summary>
        /// Finds a product by name and deletes it.
        /// </summary>
        /// <param name="Enter product id">The id of the product to delete.</param>
        /// <returns>A deleted product object with ID, name, description, and price.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProductsById(int id)
        {
            var product = await _productService.DeleteProductAsync(id);

            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Failed to delete product");
            }
        }
    }
}
