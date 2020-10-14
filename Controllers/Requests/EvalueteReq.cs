using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Controllers.Requests
{
    public class EvalueteReq
    {
        public int productId { get; set; }
        public byte numberStar { get; set; }
    }
}
