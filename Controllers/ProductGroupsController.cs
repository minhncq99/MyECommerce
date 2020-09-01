using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupsController : ControllerBase
    {
        #region -- Restfull API --
        [HttpGet("get-all")]
        public ActionResult<IEnumerable<ProductGroups>> GetAll()
        {
            var result = _context.ProductGroups.ToList();
            return Ok(result);
        }

        // Get By Id
        [HttpGet("get-by-id")]
        public ActionResult<ProductGroups> GetById(int id)
        {
            var result = _context.ProductGroups.FirstOrDefault(x => x.ProductGroupId == id);
            return Ok(result);
        }

        // Create

        // Update

        // Delete

        #endregion

        #region -- Initial --
        private readonly MyECommerceContext _context;

        public ProductGroupsController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
