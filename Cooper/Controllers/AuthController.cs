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
        private readonly IConfigProvider configProvider;

        public AuthController(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userRepository = new UserRepository(jwtService, configProvider);

            this.configProvider = configProvider;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Login login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }

            login.Username = SQLInjectionSecurity(login.Username);
            login.Password = SQLInjectionSecurity(login.Password);

            bool authValid = userRepository.CheckCredentials(login.Username, login.Password);

            if (!authValid) return Unauthorized();
            //TODO: Add error: please verify your account
            if (!userRepository.CheckVerifyByNickname(login.Username)) return Unauthorized();
                
            string tokenString = new TokenFactory(login, configProvider).GetTokenString();
                
            return Ok(new { Token = tokenString });
        }

        private string SQLInjectionSecurity(string value)
        {
            return value.Replace("'", "").Replace("\"", "");
        }
    }
}
