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
    [Route("api/forgot")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly ISocialAuth socialAuth;
        private readonly ILogger<ForgotPasswordController> logger;

        public ForgotPasswordController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider, ILogger<ForgotPasswordController> logger)
        {
            userRepository = new UserRepository(jwtService, configProvider);

            this.configProvider = configProvider;
            this.socialAuth = socialAuth;
            this.logger = logger;
        }

        /// <summary>
        /// Send letter to reset password.
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">If letter was sent</response>
        /// <response code="400">If letter was not sent</response>
        [HttpPost]
        [Route("send/{email}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendLetter(string email)
        {
            IActionResult result = BadRequest();

            if (email != null)
            {
                email = DbTools.SanitizeString(email);

                if (userRepository.IfEmailExists(email))
                {
                    // TODO: SENT EMAIL
                    logger.LogInformation("Password reset occurred for email {0}.", email);
                    result = Ok();
                }
                else
                {
                    logger.LogWarning("Password reset did not occur, because the passed email does not exist.");
                }
            }
            else
            {
                logger.LogWarning("Password reset did not occur, because the passed email was empty.");
            }

            return result;
        }
    }
}