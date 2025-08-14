using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DATA.Entities
{
    public class Report
    {
        public string name { get; set; }
        public int discount { get; set; }
        public int numberOfOrders { get; set; }
        public double totalAmount { get; set; }
    }
}
