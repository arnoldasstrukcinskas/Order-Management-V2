using OrderManagement.BLL.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseOrderDto> CreateOrder(OrderDto orderDto);
        Task<ResponseOrderDto> GetOrderById(int id);
        Task<ResponseOrderDto> DeleteOrderById(int id);
    }
}
