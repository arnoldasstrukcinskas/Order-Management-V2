using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Discount> CreateDiscount(Discount discount);
        Task<Discount> UpdateDiscount(Discount discount);
        Task<Discount> FindDiscountByName(string name);
        Task<Discount> DeleteDiscountById(int id);
    }
}
