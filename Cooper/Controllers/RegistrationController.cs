using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using System.Net;
using Cooper.Repository;
using Cooper.Controllers.ViewModels;
using Cooper.Services;
using Cooper.Configuration;

namespace Cooper.Controllers
{
    [Route("api/registration")]
    public class RegistrationController : Controller
    {
        UserRepository userRepository;
        private readonly ISmtpClient smtpClient;
        private readonly ITokenCleaner cleaner;
        private readonly ISocialAuth socialAuth;
        public RegistrationController(IJwtHandlerService jwtHandler, IConfigProvider configProvider, ISocialAuth socialAuth, ITokenCleaner cleaner, ISmtpClient smtpClient)
        {
            userRepository = new UserRepository(jwtHandler, configProvider);
            this.smtpClient = smtpClient;
            this.cleaner = cleaner;
            this.socialAuth = socialAuth;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserRegistration user, string Password)
        {
            IActionResult result = BadRequest(ModelState);
            // TODO: send the proper explanation for bad-request.
            user.Nickname = DbTools.SanitizeString(user.Nickname);
            user.Email = DbTools.SanitizeString(user.Email);

            if (ModelState.IsValid 
                && !userRepository.IfNicknameExists(user.Nickname) 
                && !userRepository.IfEmailExists(user.Email)) 
            {
                user.Password = DbTools.SanitizeString(user.Password);
                if (user.Provider != null && this.socialAuth.getCheckAuth(user.Provider, user.Password, user.Nickname)) {
                    if (user.Password.Length > 300) {
                        user.Password = user.Password.Substring(0, 300);
                    }
                    result = Ok(userRepository.Create(user));
                }
                else if (user.Provider == null) {
                    var verify = new Verification();
                    verify.Email = user.Email;
                    verify.Token = Guid.NewGuid().ToString();
                    user.Email = verify.Token;
                    verify.EndVerifyDate = DateTime.Now.AddDays(3);

                    userRepository.Create(verify);
                    cleaner.TryToStart();
                    this.smtpClient.SendMail(verify.Email, "Register confirmation", "", verify.Token);
                    result = Ok(userRepository.Create(user));
                }
            }

            return result;
        }
    }

    public class ConfirmationController : Controller 
    {
        UserRepository userRepository;
        private readonly IConfigProvider configProvider;

        public ConfirmationController(IJwtHandlerService jwtHandler, IConfigProvider configProvider, ISmtpClient smtpClient)
        {
            userRepository = new UserRepository(jwtHandler, configProvider);
            this.configProvider = configProvider;
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult Confirm() {
            IActionResult result;
            string token = Request.Query["token"];
            var email = userRepository.GetVerifyEmail($"\'{token}\'");

            if (email == null) {
                result = Redirect("/auth");
            } 
            else 
            {
                userRepository.ConfirmEmail(token, email);
                userRepository.DeleteToken($"\'{token}\'");
                
                result = Redirect("/auth");//TODO: Auth
            }
            
            return result;
        }
    }
}
