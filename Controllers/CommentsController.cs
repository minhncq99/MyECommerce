using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Controllers.Requests;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        #region
        [HttpGet("get-by-product-id")]
        public ActionResult<IEnumerable<Comments>> GetByProductId(long productId)
        {
            if (productId < 1)
            {
                return BadRequest();
            }
            else
            {
                var comments = _context.Comments
                    .Where(x => x.ProductId == productId)
                    .ToList();
                return Ok(comments);
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPost("create")]
        public ActionResult CreateComment([FromBody]CommentReq req)
        {
            if(req.Content == null || req.ProductId < 1)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IList<Claim> claims = identity.Claims.ToList();
                    string username = claims[0].Value;
                    string role = claims[2].Value;

                    Comments comment = new Comments()
                    {
                        Content = req.Content,
                        ProductId = req.ProductId,
                    };

                    if(role == "2")
                    {
                        comment.ShopId = username;
                    }
                    else if(role == "3")
                    {
                        comment.CustomerId = username;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    this._context.Comments.Add(comment);
                    this._context.SaveChanges();

                    return Ok();
                } 
                catch(Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }
        #endregion
        #region
        private readonly MyECommerceContext _context;
        public CommentsController(MyECommerceContext context)
        {
            this._context = context;
        }
        #endregion
    }
}
