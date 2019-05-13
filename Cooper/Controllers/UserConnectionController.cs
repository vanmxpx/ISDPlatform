using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using Cooper.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/users/connection")]
    public class UserConnectionController : ControllerBase
    {
        UserConnectionRepository userConnectionRepository;

        public UserConnectionController(IConfigProvider configProvider)
        {
            userConnectionRepository = new UserConnectionRepository(configProvider);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserConnectionById(long id)
        {
            UserConnection userConnection = userConnectionRepository.Get(id);
            if (userConnection == null)
            {
                return NotFound();
            }

            return Ok(userConnection);
        }

        // POST api/<controller>
        [HttpPost("request")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserConnection userConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userConnection.Id == 0)
            {
                long id = userConnectionRepository.Create(userConnection);
                userConnection.Id = id;

                return Ok(userConnection);
            }
            else
            {
                userConnectionRepository.Update(userConnection);

                return Ok(userConnection);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            userConnectionRepository.Delete(id);
            return Ok();
        }
    }
}
