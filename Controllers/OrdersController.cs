using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Models;
using MyECommerce.Models.MyModels;
using Newtonsoft.Json;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="3")]
    public class OrdersController : ControllerBase
    {
        [HttpPost("add-order")]
        public ActionResult AddOrder(string comment)
        {
            // --- Add Order ---
            // Take info about current user
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            var username = claims[0].Value;
            var userinfo = _context.Customers.FirstOrDefault(x => x.CustomerId == username);

            // Take carts
            string _session = HttpContext.Session.GetString(_keySessionCart);
            List<Carts> carts = JsonConvert.DeserializeObject<List<Carts>>(_session);

            long temp = total(carts);
            Orders order = new Orders()
            {
                TimeOrder = DateTime.Now,
                TimeRecived = DateTime.Now.AddDays(5),
                TemporarySum = temp,
                ShippingFee = 35000,
                TotalPrice = temp + 35000,
                Status = "Shops tiếp nhận đơn hàng",
                Comment = comment,
                CustomerId = username
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // --- Add Order Detail ---
            foreach(Carts c in carts)
            {
                // Take coupon
                Coupons coupon = _context.Coupons.FirstOrDefault(x => x.Code == c.Coupon);

                OrderDetails od = new OrderDetails()
                {
                    Amount = c.Amount,
                    OrderId = order.OrderId,
                    ProductId = c.ProductId
                };
                if(coupon != null)
                {
                    od.CouponId = coupon.CouponId;
                }

                _context.OrderDetails.Add(od);
            }
            _context.SaveChanges();
            HttpContext.Session.SetString(_keySessionCart, "");

            return Ok("Success");
        }

        /// <summary>
        /// Get All Order for current customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-order-current")]
        public ActionResult GetOrder()
        {
            // Get info of current user
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            string username = claims[0].Value;

            var orders = _context.Orders.Where(x => x.CustomerId == username);

            return Ok(orders);
        }


        #region -- Share Method --
        private long total(List<Carts> carts)
        {
            long result = 0;

            
            foreach(Carts c in carts)
            {
                result += c.TotalPrice;
            }

            return result;
        }
        #endregion

        #region -- Initial --
        static private readonly string _keySessionCart = "session_cart";
        private readonly MyECommerceContext _context;
        public OrdersController(MyECommerceContext context)
        {
            _context = context;
        }

        #endregion
    }
}
