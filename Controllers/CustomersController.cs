using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="3")]
    public class CustomersController : ControllerBase
    {
        [HttpGet("current-customer")]
        public ActionResult GetCurrentCustomer()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var model = _context.Customers.FirstOrDefault(x => x.CustomerId == claim[0].Value);

            return Ok(new
            {
                CustomerId = model.CustomerId,
                Name = model.Name,
                Email = model.Email,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            });
        }

        #region -- Initial --
        private readonly MyECommerceContext _context;
        public CustomersController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
