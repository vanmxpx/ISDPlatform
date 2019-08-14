using Cooper.Controllers.ViewModels;
using Cooper.ORM;
using Cooper.Repositories;
using Cooper.Services.Authorization;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Cooper.Controllers
{
    [ApiController]
    [Route("api/reset")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly ILogger<ResetPasswordController> logger;

        public ResetPasswordController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider, ILogger<ResetPasswordController> logger)
        {
            userRepository = new UserRepository(jwtService, configProvider);
            this.logger = logger;
        }

        /// <summary>
        /// Send letter to reset password.
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">If letter was sent</response>
        /// <response code="400">If letter was not sent</response>
        [HttpPost]
        [Route("send")]
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
                    logger.LogInformation("Password reset occurred for email {0}.", email.Email);
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