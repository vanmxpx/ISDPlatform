using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            if (!ModelState.IsValid || user.Id != 0)
            {
                return BadRequest(ModelState);
            }

            long id = userRepository.Create(user);
            user.Id = id;

            return Ok(user);
        }
    }
}
