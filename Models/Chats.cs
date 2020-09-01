using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Chats
    {
        public Chats()
        {
            ChatDetails = new HashSet<ChatDetails>();
        }

        public long ChatId { get; set; }
        public string CustomerId { get; set; }
        public string ShopId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual ICollection<ChatDetails> ChatDetails { get; set; }
    }
}
