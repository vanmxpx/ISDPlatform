using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }
        
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(long id)
        {
            return Ok(userRepository.Get(id));
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByEmail(string email)
        {
            return Ok(userRepository.GetByEmail(email));
        }

        [HttpGet("nickname/{nickname}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByNickname(string nickname)
        {
            return Ok(userRepository.GetByNickname(nickname));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            return Ok(user);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            // DAO MISSED
            return Ok();
        }
    }
}
