using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Admins
    {
        public Admins()
        {
            InverseFromAdminNavigation = new HashSet<Admins>();
        }

        public string AdminId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string FromAdmin { get; set; }
        public byte RoleId { get; set; }

        public virtual Admins FromAdminNavigation { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<Admins> InverseFromAdminNavigation { get; set; }
    }
}
