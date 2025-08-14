using Microsoft.EntityFrameworkCore;
using OrderManagement.DATA.Entities;
using OrderManagement.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Repositories.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<OrderItem>> GetOrderItemsByDiscount(string name)
        {
            return await _dbContext.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Discount)
                .Where(oi => oi.Discount != null && oi.Discount.Name.Equals(name))
                .Where(oi => oi.Quantity >= oi.Discount.MinQuantity)
                .ToListAsync();
        }
    }
}
