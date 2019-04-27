using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using Cooper.Controllers.ViewModels;

namespace Cooper.Controllers
{
    [Route("api/registration")]
    public class RegistrationController : Controller
    {

        UserRepository userRepository;

        public RegistrationController()
        {
            userRepository = new UserRepository();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserRegistration user, string Password)
        {
            bool nicknameExists = userRepository.IfNicknameExists(user.Nickname);
            bool emailExists = userRepository.IfEmailExists(user.Email);

            // TODO: divide this statement into three and send the proper explanation for bad-request.

            if (!ModelState.IsValid || nicknameExists || emailExists)
            {
                return BadRequest(ModelState);
            }

            long id = userRepository.Create(user);

            return Ok(id);
        }
    }
}
