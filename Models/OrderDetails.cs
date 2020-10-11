using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class OrderDetails
    {
        public long OrderDetailId { get; set; }
        public int Amount { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int? CouponId { get; set; }

        public virtual Coupons Coupon { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
