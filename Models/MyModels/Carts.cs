using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models.MyModels
{
    public class Carts
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Coupon { get; set; }
        public double TotalPrice { get; set; }
    }
}
