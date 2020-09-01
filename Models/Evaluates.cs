using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Evaluates
    {
        public long EvaluateId { get; set; }
        public byte NumberStar { get; set; }
        public long ProductId { get; set; }
        public string CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Products Product { get; set; }
    }
}
