using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public long OrderId { get; set; }
        public DateTime TimeOrder { get; set; }
        public DateTime? TimeRecived { get; set; }
        public long? TemporarySum { get; set; }
        public int? ShippingFee { get; set; }
        public long? TotalPrice { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
