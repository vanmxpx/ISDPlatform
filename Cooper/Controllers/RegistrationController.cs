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

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userRepository.IfNicknameExists(user.Nickname)) return BadRequest(ModelState);
            if (userRepository.IfEmailExists(user.Email)) return BadRequest(ModelState);

            //this.smtpClient.SendMail()
            //userRepository.Create(user)

            var tmp = userRepository.GetAll();
            return Ok();
        }
    }

    public class ConfirmationController : Controller 
    {
        [Route("confirm")]
        public IActionResult Confirm() {
            //Request.QueryString["token"];

            return Redirect("/Home/Index");
        }
    }
}
