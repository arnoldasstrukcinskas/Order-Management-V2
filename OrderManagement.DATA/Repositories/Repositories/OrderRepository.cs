using Microsoft.EntityFrameworkCore;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> DeleteOrderById(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if(order == null)
            {
                throw new Exception("No such order");
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }
    }
}
