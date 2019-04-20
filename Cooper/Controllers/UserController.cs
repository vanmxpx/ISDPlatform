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
            User user = userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByEmail(string email)
        {
            User user = userRepository.GetByEmail(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("nickname/{nickname}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByNickname(string nickname)
        {
            User user = userRepository.GetByNickname(nickname);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user.Id == 0)
            {
                long id = userRepository.Create(user);
                user.Id = id;

                return Ok(user);
            }
            else
            {
                userRepository.Update(user);

                return Ok(user);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            userRepository.Delete(id);
            return Ok();
        }
    }
}
