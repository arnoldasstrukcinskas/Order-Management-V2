using OrderManagement.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.DTO.Product
{
    public class ResponseProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
    }
}
