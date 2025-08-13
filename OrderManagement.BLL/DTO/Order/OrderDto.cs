using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.DTO.Order
{
    public class OrderDto
    {
        public List<OrderItemDto> Items { get; set; }
    }
}
