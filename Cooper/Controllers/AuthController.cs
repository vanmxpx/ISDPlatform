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
        public IActionResult Login([FromBody]UserAuth user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            bool authValid = userRepository.CheckCredentials(user.Username, user.Password);

            if (authValid)
            {

                var signinCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
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
    }
}
