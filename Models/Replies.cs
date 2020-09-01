using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Replies
    {
        public long ReplyId { get; set; }
        public string Content { get; set; }
        public long CommentId { get; set; }
        public string ShopId { get; set; }
        public string CustomerId { get; set; }

        public virtual Comments Comment { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Shops Shop { get; set; }
    }
}
