using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;


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
        public IActionResult Post([FromBody]User user)
        {
            bool nicknameExists = userRepository.IfNicknameExists(user.Nickname);       // validation that we don't create user with the same nickname

            if (!ModelState.IsValid || user.Id != 0 || nicknameExists == true)
            {
                return BadRequest(ModelState);
            }

            long id = userRepository.Create(user);
            user.Id = id;

            return Ok(user);
        }
    }
}
