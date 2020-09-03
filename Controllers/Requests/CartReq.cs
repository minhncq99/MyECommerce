using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Controllers.Requests
{
    public class CartReq
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public string Coupon { get; set; }
    }
}
