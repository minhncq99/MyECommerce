using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Shops
    {
        public Shops()
        {
            Chats = new HashSet<Chats>();
            Comments = new HashSet<Comments>();
            Coupons = new HashSet<Coupons>();
            Products = new HashSet<Products>();
            Replies = new HashSet<Replies>();
        }

        public string ShopId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public string ContractCode { get; set; }
        public byte RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Chats> Chats { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Coupons> Coupons { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Replies> Replies { get; set; }
    }
}
