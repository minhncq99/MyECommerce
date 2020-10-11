using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Admins = new HashSet<Admins>();
            Customers = new HashSet<Customers>();
            Shippers = new HashSet<Shippers>();
            Shops = new HashSet<Shops>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Admins> Admins { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Shippers> Shippers { get; set; }
        public virtual ICollection<Shops> Shops { get; set; }
    }
}
