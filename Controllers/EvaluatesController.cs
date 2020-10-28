using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyECommerce.Controllers.Requests;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluatesController : ControllerBase
    {
        #region -- Rest API --
        // Get by Product Id
        [HttpGet("get-by-product-id")]
        public ActionResult<IEnumerable<Evaluates>> GetByProductId(int productId)
        {
            if (productId < 1)
                return BadRequest();
            else
            {
                var e = _context.Evaluates.Where(x => x.ProductId == productId).ToList();
                if(e.Count != 0)
                {
                    return Ok(e);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("create")]
        // Create
        public ActionResult Create([FromBody] EvalueteReq req)
        {
            if(req.productId < 1 || req.numberStar < 0 || req.numberStar > 5)
            {
                return BadRequest();
            }
            else
            {
                // Take info about current user
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claims = identity.Claims.ToList();
                var username = claims[0].Value;

                // Check user bought
                var check = _context.Orders.Join(
                        _context.OrderDetails,
                        o => o.OrderId,
                        od => od.OrderId,
                        (o, od) => new
                        {
                            customerId = o.CustomerId,
                            productId = od.ProductId
                        }
                    ).Where(x => x.customerId == username && x.productId == req.productId);

                if(check.Count() < 1)
                {
                    return BadRequest("Bạn không thể đánh giá sản phẩm vì bạn chưa mua sản phẩm này");
                }
                else
                {
                    Evaluates evaluates = new Evaluates
                    {
                        CustomerId = username,
                        ProductId = req.productId,
                        NumberStar = req.numberStar
                    };


                    try
                    {
                        var res = _context.Evaluates.Add(evaluates);
                        _context.SaveChanges();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }

            }
        }
        #endregion

        #region -- Initial --
        private readonly MyECommerceContext _context;
        public EvaluatesController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
