using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyECommerce.Controllers.Requests;
using MyECommerce.Models;
using MyECommerce.Models.MyModels;
using Newtonsoft.Json;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        #region -- Api Cart --
        [HttpGet("get-cart")]
        public IActionResult GetCart()
        {
            string jsonCart = HttpContext.Session.GetString(_keySessionCart);
            List<Carts> carts = null;
            if (jsonCart != null)
            {
                carts = JsonConvert.DeserializeObject<List<Carts>>(jsonCart);
            }

            return Ok(carts);
        }

        [HttpPost("add-item")]
        public IActionResult AddItem([FromBody]CartReq req)
        {
            // Is Productid valid?
            if(_context.Products.FirstOrDefault(x => x.ProductId == req.ProductId) == null)
            {
                return BadRequest("Invalid");
            }

            // Check Cart in Session
            List<Carts> carts = new List<Carts>();
            var _session = HttpContext.Session.GetString(_keySessionCart);

            if(_session == null)
            {
                // Add cart to carts
                var cart = new Carts()
                {
                    ProductId = req.ProductId,
                    ProductName = _context.Products.FirstOrDefault(x => x.ProductId == req.ProductId).Name,
                    Amount = req.Amount,
                    Coupon = req.Coupon,
                    TotalPrice = total(req.ProductId, req.Amount, req.Coupon) 
                };

                carts.Add(cart);

                HttpContext.Session.SetString(_keySessionCart, JsonConvert.SerializeObject(carts));
            }
            else
            {
                carts = JsonConvert.DeserializeObject<List<Carts>>(_session);

                // Product not in old carts
                if(carts.FirstOrDefault(x => x.ProductId == req.ProductId) == null)
                {
                    // Create new cart
                    Carts cart = new Carts()
                    {
                        ProductId = req.ProductId,
                        ProductName = _context.Products.FirstOrDefault(x => x.ProductId == req.ProductId).Name,
                        Amount = req.Amount,
                        Coupon = req.Coupon,
                        TotalPrice = total(req.ProductId, req.Amount, req.Coupon)
                    };
                    carts.Add(cart);
                }
                else
                {
                    Carts cart = carts.FirstOrDefault(x => x.ProductId == req.ProductId);
                    cart.Amount += req.Amount;
                    cart.TotalPrice = total(req.ProductId, cart.Amount + req.Amount, req.Coupon);
                }
            }

            HttpContext.Session.SetString(_keySessionCart, JsonConvert.SerializeObject(carts));
            return Ok("Success");
        }

        [HttpPut("update-item")]
        public ActionResult UpdateCart([FromBody]CartReq req)
        {
            var _session = HttpContext.Session.GetString(_keySessionCart);
            // No Carts
            if(_session == null)
            {
                return BadRequest();
            }

            List<Carts> carts = JsonConvert.DeserializeObject<List<Carts>>(_session);
            Carts cart = carts.FirstOrDefault(x => x.ProductId == req.ProductId);
            
            // Product id not exits in carts
            if(cart == null)
            {
                return BadRequest();
            }

            cart.Amount = req.Amount;
            cart.Coupon = req.Coupon;
            cart.TotalPrice = total(req.ProductId, req.Amount, req.Coupon);

            HttpContext.Session.SetString(_keySessionCart, JsonConvert.SerializeObject(carts));
            return Ok();
        }

        [HttpDelete("delete-item")]
        public ActionResult Delete(long id)
        {
            var _session = HttpContext.Session.GetString(_keySessionCart);
            // No Carts
            if (_session == null)
            {
                return BadRequest();
            }

            List<Carts> carts = JsonConvert.DeserializeObject<List<Carts>>(_session);
            Carts cart = carts.FirstOrDefault(x => x.ProductId == id);

            // Product id not exits in carts
            if (cart == null)
            {
                return BadRequest();
            }

            carts.Remove(cart);

            HttpContext.Session.SetString(_keySessionCart, JsonConvert.SerializeObject(carts));
            return Ok();
        }

        #endregion

        #region -- Share Method --
        private double total(long ProductId, int Amount, string Coupon)
        {
            double total = 0;
            var _product = _context.Products.FirstOrDefault(x => x.ProductId == ProductId);
            if (_product != null)
            {
                total = (_product.Price - _product.Discount) * Amount;
                // Check Coupon
                var _coupon = _context.Coupons.FirstOrDefault(x => x.Code == Coupon);
                if (_coupon != null && _coupon.Minimize < total)
                {
                    total = total - _coupon.Discount;
                }
            }

            return total;
        }

        #endregion

        #region -- Initial --
        static private readonly string _keySessionCart = "session_cart";
        private readonly MyECommerceContext _context;

        public CartsController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
