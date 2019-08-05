using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using Microsoft.AspNetCore.Http;
using Cooper.Services;
using Cooper.Extensions;
using Cooper.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/users")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ControllerBase
    {
        UserRepository userRepository;

        public UserController(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userRepository = new UserRepository(jwtService, configProvider);
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

        [HttpGet("nickname/{nickname}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByNickname(string nickname)
        {

            User user = userRepository.GetByNickname(nickname);
            var myUser = Request.GetAuthorizedUser(userRepository);

            if (myUser == null && user == null)
                return BadRequest();

            myUser.IsMy = true;

            if (nickname == myUser.Nickname || nickname == "my")
            {                
                return Ok(myUser);
            }
            
            if (user == null )
            {
                return NotFound();
            }
            user.IsMy = false;
            return Ok(user);
        }
        
        [HttpGet("token"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByJWToken()
        {
            User user = Request.GetAuthorizedUser(userRepository);

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
            if (!ModelState.IsValid || user.Id == 0)
            {
                return BadRequest(ModelState);
            }
            
            userRepository.Update(user);

            return Ok(user);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes user with specified id.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <response code="200">If the user has been deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(long id)
        {
            userRepository.Delete(id);
            return Ok();
        }
    }
}
