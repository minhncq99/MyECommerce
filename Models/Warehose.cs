using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Warehose
    {
        public long WarehoseId { get; set; }
        public DateTime Time { get; set; }
        public long OrderDetailId { get; set; }
        public string ShipperId { get; set; }

        public virtual OrderDetails OrderDetail { get; set; }
        public virtual Shippers Shipper { get; set; }
    }
}
