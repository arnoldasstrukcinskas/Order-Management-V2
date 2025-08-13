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
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _discountService.DeleteDiscountById(id);

            if (discount != null)
            {
                return Ok(discount);
            }
            else
            {
                return BadRequest("Discount was not deleted");
            }
        }
    }
}
