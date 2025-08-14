using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.BLL.DTO;
using OrderManagement.BLL.Interfaces;

namespace OrderManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }


        /// <summary>
        /// Adds Discount to postgreSql database.
        /// </summary>
        /// <param discount="Enter discoutn data">Name, percentage, min q of discount to create.</param>
        /// <returns>Added DTO of discount with ID, discount name, percentage and minimum quantity.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(DiscountDto discountDto)
        {
           var discount = await _discountService.CreateDiscount(discountDto);

            if(discount != null)
            {
                return Ok(discount);
            }
            else
            {
                return BadRequest("Discount was not added.");
            }
        }

        /// <summary>
        /// Updates discounts name, percentage and minimum quantity.
        /// </summary>
        /// <returns>Updated DTO of discount with name, percentage, minimum quantity.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(DiscountDto discountDto)
        {
            var discount = await _discountService.UpdateDiscount(discountDto);

            if(discount != null)
            {
                return Ok(discount);
            }
            else
            {
                return BadRequest("Discount was not updated.");
            }
        }

        /// <summary>
        /// Deletes Discount from postgreSql database.
        /// </summary>
        /// <returns>DTO of deleted discount from databse .</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountByName(string name)
        {
            var discount = await _discountService.DeleteDiscountByName(name);

            if (discount != null)
            {
                return Ok(discount);
            }
            else
            {
                return BadRequest("Discount was not deleted");
            }
        }

        /// <summary>
        /// Gets Report of discounted products by specified discount.
        /// </summary>
        /// /// <param discountName="Enter discount name">Name of discount was applied to products.</param>
        /// <returns>List of products with soecified discount</returns>
        [HttpGet]
        public async Task<IActionResult> GenerateRerportByDiscount(string discountName)
        {
            var products = await _discountService.GetDiscountReportByName(discountName);
            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest("Report was not formated.");
            }
        }
    }
}
