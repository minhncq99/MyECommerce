using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Chats
    {
        public long ChatId { get; set; }
        public string CustomerId { get; set; }
        public string ShopId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public bool IsFromShop { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Shops Shop { get; set; }
    }
}
