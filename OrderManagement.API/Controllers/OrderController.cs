using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.BLL.DTO.Order;
using OrderManagement.BLL.Interfaces;
using OrderManagement.BLL.Services;

namespace OrderManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _orderService;

        public OrderController (IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates Order in postgreSql database.
        /// </summary>
        /// <returns>Added order object with ID, Date, Price, Total items and list of items.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var newOrder = await _orderService.CreateOrder(orderDto);

            if(newOrder != null)
            {
                return Ok(newOrder);
            }
            else
            {
                return BadRequest("Failed to create order");
            }
        }

        /// <summary>
        /// Gets order from postgreSql database.
        /// </summary>
        /// <returns>Deleted order object with ID, Date, Price, Total items and list of items.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if(order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest("Order wat not found");
            }
        }

        /// <summary>
        /// Deletes Order in postgreSql database.
        /// </summary>
        /// <param id="Enter order id">Id of order to delete.</param>
        /// <returns>Found order object with ID, Date, Price, Total items and list of items.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            var order = await _orderService.DeleteOrderById(id);

            if(order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest("Controller: Order was not found");
            }
        }


    }
}
