using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // Get products new by product groups with pagination
        [HttpGet("get-by-product-group")]
        public ActionResult<List<Products>> GetByProductGroup(int id, int page, int size)
        {
            var result = _context.Products.Where(x => x.ProductGroupId == id)
                .OrderByDescending(x => x.ProductId).Skip((page -1) * size).Take(size).ToList();

            return Ok(result);
        }

        // Get new products
        [HttpGet("get-new")]
        public ActionResult<List<Products>> GetNew(int size)
        {
            var result = _context.Products.OrderByDescending(x => x.ProductId).Take(size).ToList();
            return Ok(result);
        }

        // Search product by name have pagination
        [HttpGet("search")]
        public ActionResult<List<Products>> Search(string name, int page, int size)
        {
            var result = _context.Products.Where(x => x.Name.Contains(name)).Skip((page - 1) * size).Take(size);

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
