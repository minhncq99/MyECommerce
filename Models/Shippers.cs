using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Shippers
    {
        public string ShipperId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public string ContractCode { get; set; }
        public byte RoleId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
