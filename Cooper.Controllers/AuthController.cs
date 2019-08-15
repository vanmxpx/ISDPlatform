using Cooper.Controllers.ViewModels;
using Cooper.ORM;
using Cooper.Repositories;
using Cooper.Services.Authorization;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cooper.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly ISocialAuth socialAuth;
        private readonly ILogger<AuthController> logger;

        public AuthController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider, ILogger<AuthController> logger)
        {
            userRepository = new UserRepository(jwtService, configProvider);

            this.configProvider = configProvider;
            this.socialAuth = socialAuth;
            this.logger = logger;
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

        /// <summary>
        /// Send letter to reset password.
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">If letter was sent</response>
        /// <response code="400">If letter was not sent</response>
        [HttpPost]
        [Route("reset/send")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendLetter(TargetEmail email)
        {
            IActionResult result = BadRequest();

            if (email != null)
            {
                email.Email = DbTools.SanitizeString(email.Email);

                if (userRepository.IfEmailExists(email.Email))
                {
                    // TODO: SENT EMAIL
                    logger.LogInformation("Password reset email was sent for email {0}.", email.Email);
                    result = Ok();
                }
                else
                {
                    logger.LogWarning("Password reset email was not sent, because the passed email does not exist.");
                }
            }
            else
            {
                logger.LogWarning("Password reset email was not sent, because the passed email was empty.");
            }

            return result;
        }
    }
}