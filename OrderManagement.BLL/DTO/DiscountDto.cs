using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.DTO
{
    public class DiscountDto
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
        public int MinQuantity { get; set; }
    }
}
