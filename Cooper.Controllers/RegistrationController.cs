using Cooper.Controllers.ViewModels;
using Cooper.Models;
using Cooper.ORM;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cooper.Controllers
{
    [ApiController]
    [Route("api/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly ISmtpClient smtpClient;
        private readonly ITokenCleaner cleaner;
        private readonly ISocialAuth socialAuth;
        private readonly Cooper.Services.Interfaces.ISession session;

        private const string emailConfirmURL = "https://cooper.serve.games/confirm?token=";
 	public RegistrationController(IJwtHandlerService jwtHandler, ISocialAuth socialAuth, ITokenCleaner cleaner, ISmtpClient smtpClient, ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();

            userRepository = new UserRepository(jwtHandler, session);
            this.smtpClient = smtpClient;
            this.cleaner = cleaner;
            this.socialAuth = socialAuth;
        }

        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="user">User registration information</param>
        /// <returns>Registered user id</returns>
        /// <response code="200">If user is created</response>
        /// <response code="400">If the user is already created</response>  
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(UserRegistration user)
        {
            IActionResult result = BadRequest();
            // TODO: send the proper explanation for bad-request.
            user.Nickname = DbTools.SanitizeString(user.Nickname);
            user.Email = DbTools.SanitizeString(user.Email);

            session.StartSession();

            if (!userRepository.IfNicknameExists(user.Nickname)
                && !userRepository.IfEmailExists(user.Email))
            {
                user.Password = DbTools.SanitizeString(user.Password);
                if (user.Provider != null && socialAuth.getCheckAuth(user.Provider, user.Password, user.Nickname))
                {
                    if (user.Password.Length > 300)
                    {
                        user.Password = user.Password.Substring(0, 300);
                    }
                    result = Ok(userRepository.Create(user));
                }
                else if (user.Provider == null)
                {
                    var verify = new Verification();
                    verify.Email = user.Email;
                    verify.Token = Guid.NewGuid().ToString();
                    user.Email = verify.Token;
                    verify.EndVerifyDate = DateTime.Now.AddDays(3);

                    userRepository.Create(verify);
                    cleaner.TryToStart();
                    smtpClient.SendMail(verify.Email, "Register confirmation", $"Your activation link: {emailConfirmURL}{verify.Token}");
                    result = Ok(userRepository.Create(user));
                }
            }

            session.EndSession();
            return result;
        }
    }
}