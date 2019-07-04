﻿using System;
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
using Cooper.Extensions;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/interaction")]
    public class UsersConnectionController : ControllerBase
    {
        private IUsersConnectionRepository userConnectionsRepository;
        private readonly IUsersConnectionService userConnectionService;
        private readonly ILogger logger;

        public UsersConnectionController(IConfigProvider configProvider, IUsersConnectionService userConnectionService, ILogger logger)
        {
            userConnectionsRepository = new UsersConnectionRepository(configProvider, logger);
            this.userConnectionService = userConnectionService;
            this.logger = logger;
        }
        
        [HttpGet("blacklist"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserBlackList()
        {
            string userToken = Request.GetUserToken();
            
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

        [HttpGet("friends/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserFriendsList(long userId)
        {
            List<User> friendsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Friends);

            return Ok(friendsList);
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

            string subscriberToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }


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

            string userToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, ban: true);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

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

            string userToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, ban: false);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            bool isUnbanned = userConnectionsRepository.UnbanUser(usersConnection);

            return Ok(isUnbanned);
        }
        
        [HttpDelete("{id}"), Authorize]
        public IActionResult Unsubscribe(long userId)
        {
            string subscriberToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            bool isUnsubscribed = userConnectionsRepository.Unsubscribe(usersConnection);

            return Ok(isUnsubscribed);
        }
    }
}
