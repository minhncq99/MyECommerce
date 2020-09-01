using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Comments
    {
        public Comments()
        {
            Replies = new HashSet<Replies>();
        }

        public long CommentId { get; set; }
        public string Content { get; set; }
        public long ProductId { get; set; }
        public string ShopId { get; set; }
        public string CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Products Product { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual ICollection<Replies> Replies { get; set; }
    }
}
