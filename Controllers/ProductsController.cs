using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region -- Restfull API
        // Get All
        [HttpGet("get-all")]
        public ActionResult<IEnumerable<Products>> GetAll()
        {
            var result = _context.Products.ToList();
            return Ok(result);
        }

        // Get By Id
        [HttpGet("get-by-id")]
        public ActionResult<Products> GetById(int id)
        {
            var result = _context.Products.FirstOrDefault(x => x.ProductId == id);
            return Ok(result);
        }

        // Create

        // Update

        // Delete

        #endregion

        #region -- Initial --
        private readonly MyECommerceContext _context;
        public ProductsController(MyECommerceContext context)
        {
            _context = context;
        }
        #endregion
    }
}
