using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyECommerce.Controllers.Requests;
using MyECommerce.Models;

namespace MyECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        #region -- Login --

        // Login admin
        [HttpPost("admin")]
        public ActionResult LoginAdmin([FromBody]LoginReq req)
        {
            ActionResult response = Unauthorized();

            // Check Username and Password
            var model = _context.Admins.FirstOrDefault(a => a.AdminId == req.username);
            bool isPass = (model != null && model.Password == req.password) ? true : false;
            if (isPass)
            {
                var tokenStr = GenerateJSONWebToken(req, model.RoleId);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        [HttpPost("shop")]
        public ActionResult LoginShop(LoginReq req)
        {
            ActionResult response = Unauthorized();

            var model = _context.Shops.FirstOrDefault(x => x.ShopId == req.username);
            var isPass = (model != null && model.Password == req.password) ? true : false;
            if(isPass)
            {
                var tokenStr = GenerateJSONWebToken(req, model.RoleId);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        [HttpPost("customer")]
        public ActionResult LoginCustomer(LoginReq req)
        {
            ActionResult response = Unauthorized();

            var model = _context.Customers.FirstOrDefault(x => x.CustomerId == req.username);
            bool isPass = (model != null && model.Password == req.password) ? true : false;
            if (isPass)
            {
                var tokenStr = GenerateJSONWebToken(req, model.RoleId);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        [HttpPost("shipper")]
        public ActionResult LoginShipper(LoginReq req)
        {
            ActionResult response = Unauthorized();

            var model = _context.Shippers.FirstOrDefault(x => x.ShipperId == req.username);
            bool isPass = (model != null && model.Password == req.password) ? true : false;
            if (isPass)
            {
                var tokenStr = GenerateJSONWebToken(req, model.RoleId);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        [HttpGet("test")]
        [Authorize(Roles = "2")]
        public ActionResult Test()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var user = new
            {
                username = claim[0].Value,
                roles = claim[2].Value
            };
            return Ok(user);
        }
        #endregion

        #region -- Sharing Method --
        // Create Json Web Token
        private string GenerateJSONWebToken(LoginReq userInfor, byte roleId)
        {
            // Get security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Create credentials with security above and algorithms
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create Claim user
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfor.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("roles", roleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
        #endregion

        #region -- Initial --
        private readonly IConfiguration _config;
        private readonly MyECommerceContext _context;
        public LoginsController(IConfiguration configuration, MyECommerceContext context)
        {
            _config = configuration;
            _context = context;
        }
        #endregion
    }
}
