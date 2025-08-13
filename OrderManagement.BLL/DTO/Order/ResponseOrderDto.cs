using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.DTO.Order
{
    public class ResponseOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int TotalItems { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }
}
