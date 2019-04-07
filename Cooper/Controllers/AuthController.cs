using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Controllers.ViewModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Cooper.Models;
using Cooper.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private UserRepository userRepository;
        public AuthController()
        {
            userRepository = new UserRepository();
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserLogin user_auth)
        {
            if (user_auth == null)
            {
                return BadRequest("Invalid client request");
            }

            User user = userRepository.GetByNickname(user_auth.Username);


            if (user != null && user.Password == user_auth.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:50613/",
                    audience: "http://localhost:50613/",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                user.Token = tokenString;

                return Ok(user);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
