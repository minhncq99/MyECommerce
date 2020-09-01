using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Coupons
    {
        public Coupons()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int CouponId { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public int? Minimize { get; set; }
        public string ShopId { get; set; }

        public virtual Shops Shop { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
