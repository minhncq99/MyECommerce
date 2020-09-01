using System;
using System.Collections.Generic;

namespace MyECommerce.Models
{
    public partial class Notifications
    {
        public long NotificationId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string AdminId { get; set; }
        public byte TypeUserId { get; set; }

        public virtual Admins Admin { get; set; }
        public virtual Roles TypeUser { get; set; }
    }
}
