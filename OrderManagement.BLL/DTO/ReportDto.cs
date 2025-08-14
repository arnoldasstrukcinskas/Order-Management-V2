using OrderManagement.BLL.DTO.Order;
using OrderManagement.BLL.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BLL.DTO
{
    public class ReportDto
    {
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public List<ResponseProductDto> Products { get; set; }
    }
}
