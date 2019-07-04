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
using NLog;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{

    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly ISocialAuth socialAuth;
        private readonly ILogger logger;

        public AuthController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider,
            ILogger logger)
        {
            userRepository = new UserRepository(jwtService, configProvider, logger);

            this.configProvider = configProvider;
            this.socialAuth = socialAuth;
            this.logger = logger;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Login login)
        {
            IActionResult result = Unauthorized();
            if (login == null)
            {
                result = BadRequest("Invalid client request");
            }
            else 
            {
                login.Username = DbTools.SanitizeString(login.Username);

                if (login.Provider != null && this.socialAuth.getCheckAuth(login.Provider, login.Password, login.ID))
                {
                    if ((login.Username = userRepository.GetByEmail(login.Username)?.Nickname) != null) {
                        result = Ok(new { Token = new TokenFactory(login, configProvider).GetTokenString() });
                    }
                    else {
                        result = BadRequest("Auth");
                    }
                }
                else if (login.Provider == null) {
                    login.Password = DbTools.SanitizeString(login.Password);

                    bool authValid = userRepository.CheckCredentials(login.Username, login.Password);

                    //TODO: Add error: please verify your account
                    if (authValid && (userRepository.CheckVerifyByNickname(login.Username) || userRepository.CheckVerifyByEmail(login.Username))) 
                    {
                        result = Ok(new { Token = new TokenFactory(login, configProvider).GetTokenString() });
                    }
                }
            }

            return result;
        }
    }
}
