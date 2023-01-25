using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TokenGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token(Credentials credentials)
        {
            if (!IsAdmin(credentials) && !IsUser(credentials))
            {
                return Unauthorized();
            }

            var secretKey = "MySuperSecretKeyForOnlyTest";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                            claims: BuildClaims(credentials),
                            expires: DateTime.Now.AddMinutes(15),
                            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(token);
        }

        private Claim[] BuildClaims(Credentials credentials)
        {
            return new[]
            {
                new Claim("userType", IsAdmin(credentials) ? "admin" : "user")
            };
        }

        private bool IsUser(Credentials credentials)
        {
            return credentials.UserName == "user" && credentials.Password == "user";
        }

        private bool IsAdmin(Credentials credentials)
        {
            return credentials.UserName == "admin" && credentials.Password == "admin";
        }
    }

    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
