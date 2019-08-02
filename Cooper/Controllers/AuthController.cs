using Cooper.Configuration;
using Cooper.Controllers.ViewModels;
using Cooper.Repository;
using Cooper.Services;
using Cooper.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooper.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly ISocialAuth socialAuth;

        public AuthController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider)
        {
            userRepository = new UserRepository(jwtService, configProvider);

            this.configProvider = configProvider;
            this.socialAuth = socialAuth;
        }

        /// <summary>
        /// Checks if a user is registered.
        /// </summary>
        /// <param name="login">Login information</param>
        /// <returns>Token if user is registered</returns>
        /// <response code="200">If user is registered</response>
        /// <response code="400">If client request or invalid model is invalid</response>  
        /// <response code="401">If the user is not registered</response>  
        [HttpPost]
        [Route("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(Login login)
        {
            IActionResult result = Unauthorized();

            if (!ModelState.IsValid)
            {
                result = BadRequest();
            }
            else if (login == null)
            {
                result = BadRequest();
            }
            else 
            {
                login.Username = DbTools.SanitizeString(login.Username);

                if (login.Provider != null && socialAuth.getCheckAuth(login.Provider, login.Password, login.ID))
                {
                    if ((login.Username = userRepository.GetByEmail(login.Username)?.Nickname) != null)
                    {
                        result = Ok(new { Token = new TokenFactory(login, configProvider).GetTokenString() });
                    }
                    else
                    {
                        result = BadRequest();
                    }
                }
                else if (login.Provider == null)
                {
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
