using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {

        #region Restfull API
        [HttpGet("get-all")]
        public ActionResult<IEnumerable<Business>> GetAll()
        {
            var result = _context.Business.ToList();
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public ActionResult<Business> GetById(int id)
        {
            var result = _context.Business.FirstOrDefault(x => x.BusinessId == id);
            return Ok(result);
        }

        // Create

        // Update
        
        // Delete
        #endregion
        #region -- Initial --
        private readonly MyECommerceContext _context;
        public BusinessController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
