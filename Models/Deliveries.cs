using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Deliveries
    {
        public long DeliveryId { get; set; }
        public DateTime Time { get; set; }
        public long OrderId { get; set; }
        public string ShipperId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Shippers Shipper { get; set; }
    }
}
