using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Business
    {
        public Business()
        {
            ProductGroups = new HashSet<ProductGroups>();
        }

        public int BusinessId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductGroups> ProductGroups { get; set; }
    }
}
