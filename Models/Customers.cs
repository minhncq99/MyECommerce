using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Chats = new HashSet<Chats>();
            Comments = new HashSet<Comments>();
            Evaluates = new HashSet<Evaluates>();
            Orders = new HashSet<Orders>();
            Replies = new HashSet<Replies>();
        }

        public string CustomerId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public byte RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Chats> Chats { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Evaluates> Evaluates { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Replies> Replies { get; set; }
    }
}
