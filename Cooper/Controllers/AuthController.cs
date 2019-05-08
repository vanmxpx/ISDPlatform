using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Cooper.Models;
using Cooper.Controllers.ViewModels;
using Cooper.Repository;
using Cooper.Configuration;
using Cooper.Services;
using Cooper.Services.Authorization;
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
        public IActionResult Login([FromBody]Login login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }

            bool authValid = userRepository.CheckCredentials(login.Username, login.Password);

            if (authValid)
            {
                string tokenString = new TokenFactory(login).GetTokenString();
                
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        
    }
}
