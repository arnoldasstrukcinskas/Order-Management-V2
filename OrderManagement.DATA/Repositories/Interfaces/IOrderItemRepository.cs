using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<List<OrderItem>> GetOrderItemsByDiscount(string name);
    }
}
