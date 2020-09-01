using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Products
    {
        public Products()
        {
            Comments = new HashSet<Comments>();
            Evaluates = new HashSet<Evaluates>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public long ProductId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public string Unit { get; set; }
        public int Weight { get; set; }
        public string Brand { get; set; }
        public string Origin { get; set; }
        public string Size { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string ShopId { get; set; }
        public bool? Status { get; set; }
        public int ProductGroupId { get; set; }

        public virtual ProductGroups ProductGroup { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Evaluates> Evaluates { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
