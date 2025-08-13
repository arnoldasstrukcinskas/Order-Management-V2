using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Order order);
        public Task<Order> DeleteOrderById(int id);
        public Task<Order> GetOrderById(int id);
    }
}
