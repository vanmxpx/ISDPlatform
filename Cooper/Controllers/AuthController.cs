using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Cooper.Models;
using Cooper.Controllers.ViewModels;
using Cooper.Repository;
using Cooper.Configuration;
using Cooper.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{

    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private UserRepository userRepository;
        public AuthController(IJwtHandlerService jwtService)
        {
            userRepository = new UserRepository(jwtService);
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Login user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            bool authValid = userRepository.CheckCredentials(user.Username, user.Password);

            if (authValid)
            {

                var signinCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);


                var identity = GetIdentity(user.Username);

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                        signingCredentials: signinCredentials);

                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private ClaimsIdentity GetIdentity(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim("username", username)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
