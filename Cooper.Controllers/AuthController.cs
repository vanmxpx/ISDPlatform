using Cooper.Controllers.ViewModels;
using Cooper.Models;
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
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly ISocialAuth socialAuth;
        private readonly ILogger<AuthController> logger;
        private readonly ISmtpClient smtpClient;
        private readonly IResetPasswordService resetService;

        private const string passwordResetURL = "https://cooper.serve.games/confirm;token=";

        public AuthController(IJwtHandlerService jwtService, ISocialAuth socialAuth, IConfigProvider configProvider,
            ILogger<AuthController> logger, ISmtpClient smtpClient, IResetPasswordService resetService)
        {
            userRepository = new UserRepository(jwtService, configProvider);

            this.configProvider = configProvider;
            this.socialAuth = socialAuth;
            this.logger = logger;
            this.smtpClient = smtpClient;
            this.resetService = resetService;
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
                    if ((login.Username = socialAuth.tryGetUserNickname(login.Provider, login.ID)) != null)
                    {
                        result = Ok(new { Token = new TokenFactory(login, configProvider).GetTokenString() });
                    }
                    else
                    {
                        result = BadRequest("Auth");
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

            if (ModelState.IsValid && email != null)
            {
                email.Email = DbTools.SanitizeString(email.Email);

                if (userRepository.IfEmailExists(email.Email))
                {
                    string token = resetService.CreateToken(email.Email);
                    smtpClient.SendMail(email.Email, "Cooper reset password", $"Password reset link: {passwordResetURL}{token}");
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

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="resetPassword">Token with new password</param>
        /// <response code="200">If password was reset</response>
        /// <response code="400">If password was not reset</response>
        [HttpPost]
        [Route("reset")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            IActionResult result;

            if (ModelState.IsValid && resetPassword != null)
            {
                resetPassword.Token = DbTools.SanitizeString(resetPassword.Token);
                resetPassword.Password = DbTools.SanitizeString(resetPassword.Password);

                if (userRepository.IfResetTokenExists(resetPassword.Token))
                {
                    userRepository.ResetPassword(resetPassword.Token, resetPassword.Password);
                    logger.LogInformation("Password was reset successfully.");
                    result = Ok();
                }
                else
                {
                    logger.LogInformation("Password was not reset, because token does not exist.");
                    result = BadRequest("Token does not exist");
                }
            }
            else
            {
                logger.LogInformation("Password was not reset, because reset password is empty or invalid.");
                result = BadRequest("Token or password is empty or invalid");
            }

            return result;
        }
    }
}