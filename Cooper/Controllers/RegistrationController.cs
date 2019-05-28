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
using Cooper.Services.Authorization;

namespace Cooper.Controllers
{
    [Route("api/registration")]
    public class RegistrationController : Controller
    {
        UserRepository userRepository;
        ISmtpClient smtpClient;
        public RegistrationController(IJwtHandlerService jwtHandler, IConfigProvider configProvider, ISmtpClient smtpClient)
        {
            userRepository = new UserRepository(jwtHandler, configProvider);
            this.smtpClient = smtpClient;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserRegistration user, string Password)
        {
            // TODO: send the proper explanation for bad-request.
            user.Email = SQLInjectionSecurity(ref user.Email);
            user.Nickname = SQLInjectionSecurity(ref user.Nickname);
            Password = SQLInjectionSecurity(ref Password);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userRepository.IfNicknameExists(user.Nickname)) return BadRequest(ModelState);
            if (userRepository.IfEmailExists(user.Email)) return BadRequest(ModelState);

            var verify = new Verification();
            verify.Email = user.Email;
            verify.Token = Guid.NewGuid().ToString();
            user.Email = verify.Token;
            verify.EndVerifyDate = DateTime.Now.AddDays(3);

            userRepository.Create(verify);
            //this.smtpClient.SendMail(verify.Email, "Register confirmation", "", verify.Token);
            return Ok(userRepository.Create(user));
        }

        private string SQLInjectionSecurity(ref string value)
        {
            return value.Replace("'", "").Replace("\"", "");
        }
    }

    public class ConfirmationController : Controller 
    {
        UserRepository userRepository;
        private readonly IConfigProvider configProvider;
        private readonly IJwtHandlerService jwt;

        public ConfirmationController(IJwtHandlerService jwtHandler, IConfigProvider configProvider, ISmtpClient smtpClient)
        {
            userRepository = new UserRepository(jwtHandler, configProvider);
            this.configProvider = configProvider;
        }

        [Route("confirm")]
        public IActionResult Confirm() {
            string token = Request.Query["token"];
            var email = userRepository.GetVerifyEmail($"\'{token}\'");

            if (email == null) return Redirect("/auth");
            
            userRepository.ConfirmEmail(token, email);
            userRepository.DeleteToken($"\'{token}\'");
                
            return Redirect("/myPage");
        }
    }
}
