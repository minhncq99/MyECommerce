using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Controllers.Requests
{
    public class CommentReq
    {
        public string Content { get; set; }
        public long ProductId { get; set; }
    }
}
