using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
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
        ISmtpClient smtpClient;
        ITokenCleaner cleaner;
        public RegistrationController(IJwtHandlerService jwtHandler, IConfigProvider configProvider, ITokenCleaner cleaner, ISmtpClient smtpClient)
        {
            userRepository = new UserRepository(jwtHandler, configProvider);
            this.smtpClient = smtpClient;
            this.cleaner = cleaner;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserRegistration user, string Password)
        {
            IActionResult result;
            // TODO: send the proper explanation for bad-request.
            user.Password = DbTools.SanitizeString(user.Password);
            user.Nickname = DbTools.SanitizeString(user.Nickname);
            user.Email = DbTools.SanitizeString(user.Email);

            if (!ModelState.IsValid 
                || userRepository.IfNicknameExists(user.Nickname) 
                || userRepository.IfEmailExists(user.Email)) 
            {
                result = BadRequest(ModelState);
            }
            else 
            {
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
