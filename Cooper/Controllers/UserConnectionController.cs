using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cooper.Models;
using Cooper.Repository;
using Cooper.Configuration;
using Cooper.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/subscription")]
    public class UserConnectionController : ControllerBase
    {
        UserConnectionRepository userConnectionRepository;
        private readonly IUserConnectionService userConnectionService;

        public UserConnectionController(IConfigProvider configProvider, IUserConnectionService userConnectionService)
        {
            userConnectionRepository = new UserConnectionRepository(configProvider);
            this.userConnectionService = userConnectionService;
        }
        
        [HttpGet("blacklist"), Authorize]
        [ProducesResponseType(200, Type = typeof(UserConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserBlackList(long userId)
        {
            return Ok();
        }

        [HttpGet("subscribers"), Authorize]
        [ProducesResponseType(200, Type = typeof(UserConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscribersList(long userId)
        {
            return Ok();
        }

        [HttpGet("subscriptions"), Authorize]
        [ProducesResponseType(200, Type = typeof(UserConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscribtionsList(long userId)
        {
            return Ok();
        }

        [HttpPost("subscribe/{userId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Subscribe(long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string subscriberToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UserConnection userConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            return Ok();
        }
        
        [HttpPost("ban/{userId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Ban(long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string subscriberToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UserConnection userConnection = userConnectionService.CreateConnection(userId, subscriberToken, ban: true);

            return Ok();
        }
       
        [HttpPost("unban/{userId}"), Authorize]
        public IActionResult Unban(long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string subscriberToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UserConnection userConnection = userConnectionService.CreateConnection(userId, subscriberToken, ban: false);

            return Ok();
        }
        
        [HttpDelete("{id}"), Authorize]
        public IActionResult Unsubscribe(long id)
        {
            return Ok();
        }
    }
}
