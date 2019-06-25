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
using Cooper.Models.UserConnectionsEnumTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/subscription")]
    public class UsersConnectionController : ControllerBase
    {
        IUsersConnectionRepository userConnectionsRepository;
        private readonly IUsersConnectionService userConnectionService;

        public UsersConnectionController(IConfigProvider configProvider, IUsersConnectionService userConnectionService)
        {
            userConnectionsRepository = new UsersConnectionRepository(configProvider);
            this.userConnectionService = userConnectionService;
        }
        
        [HttpGet("blacklist"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserBlackList()
        {
            string userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            long userId = userConnectionService.GetUserId(userToken);

            List<User> blackList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Blacklist);

            return Ok(blackList);
        }

        [HttpGet("subscribers/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscribersList(long userId)
        {
            List<User> subscribersList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscribers);

            return Ok(subscribersList);
        }

        [HttpGet("subscriptions/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscriptionsList(long userId)
        {
            List<User> subscriptionsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscriptions);

            return Ok(subscriptionsList);
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

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            bool isSubscribed = userConnectionsRepository.CreateSubscription(usersConnection);
                
            return Ok(isSubscribed);
        }
        
        [HttpPost("ban/{bannedUserId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Ban(long bannedUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, ban: true);

            bool isBanned = userConnectionsRepository.BanUser(usersConnection);
            
            return Ok(isBanned);
        }
       
        [HttpPost("unban/{bannedUserId}"), Authorize]
        public IActionResult Unban(long bannedUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, ban: false);

            bool isUnbanned = userConnectionsRepository.UnbanUser(usersConnection);

            return Ok(isUnbanned);
        }
        
        [HttpDelete("{id}"), Authorize]
        public IActionResult Unsubscribe(long userId)
        {
            string subscriberToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            bool isUnsubscribed = userConnectionsRepository.Unsubscribe(usersConnection);

            return Ok(isUnsubscribed);
        }
    }
}
