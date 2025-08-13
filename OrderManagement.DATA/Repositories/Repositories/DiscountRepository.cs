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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _dbContext;

        public DiscountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discount> CreateDiscount(Discount discount)
        {
            await _dbContext.Discounts.AddAsync(discount);
            await _dbContext.SaveChangesAsync();

            return discount;
        }

        public async Task<Discount> DeleteDiscountById(int id)
        {
            var discount = await _dbContext.Discounts.FindAsync(id);

            if(discount == null)
            {
                throw new Exception($"Discount with {id} was not found.");
            }

            _dbContext.Discounts.Remove(discount);
            await _dbContext.SaveChangesAsync();

            return discount;
        }

        public async Task<Discount> FindDiscountByName(string name)
        {
            return await _dbContext.Discounts.FirstOrDefaultAsync(d => d.Name.Equals(name));
        }

        public async Task<Discount> UpdateDiscount(Discount discount)
        {
            _dbContext.Discounts.Update(discount);
            await _dbContext.SaveChangesAsync();

            return discount;

        }
    }
}
