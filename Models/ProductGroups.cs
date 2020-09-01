using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class ProductGroups
    {
        public ProductGroups()
        {
            Products = new HashSet<Products>();
        }

        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
