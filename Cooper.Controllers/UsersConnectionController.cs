using Cooper.Extensions;
using Cooper.Models;
using Cooper.Models.UserConnectionsEnumTypes;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/socialConnections")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsersConnectionController : ControllerBase
    {
        private readonly IUsersConnectionRepository userConnectionsRepository;
        private readonly IUsersConnectionService userConnectionService;
        private readonly ISessionService sessionService;

        public UsersConnectionController(IConfigProvider configProvider, IUsersConnectionService userConnectionService,
            ISessionService sessionService)
        {
            userConnectionsRepository = new UsersConnectionRepository(configProvider, sessionService);
            this.userConnectionService = userConnectionService;

            this.sessionService = sessionService;
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
            sessionService.StartSession();
            List<User> subscribersList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscribers);
            sessionService.EndSession();
            return Ok(subscribersList);
        }

        [HttpGet("subscriptions/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscriptionsList(long userId)
        {
            sessionService.StartSession();
            List<User> subscriptionsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscriptions);
            sessionService.EndSession();

            return Ok(subscriptionsList);
        }

        [HttpGet("friends/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserFriendsList(long userId)
        {
            sessionService.StartSession();
            List<User> friendsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Friends);
            sessionService.EndSession();

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

            sessionService.StartSession();

            bool isSubscribed = userConnectionsRepository.CreateSubscription(usersConnection);

            if (isSubscribed)
            {
                sessionService.Commit();
            }
            else
            {
                sessionService.Rollback();
            }

            sessionService.EndSession();

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

            sessionService.StartSession();

            bool isBanned = userConnectionsRepository.BanUser(usersConnection);
            
            if (isBanned)
            {
                sessionService.Commit();
            }
            else
            {
                sessionService.Rollback();
            }

            sessionService.EndSession();

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

            sessionService.StartSession();

            bool isUnbanned = userConnectionsRepository.UnbanUser(usersConnection);
            
            if (isUnbanned)
            {
                sessionService.Commit();
            }
            else
            {
                sessionService.Rollback();
            }

            sessionService.EndSession();

            return Ok(isUnbanned);
        }

        [HttpDelete("{userId}"), Authorize]
        public IActionResult Unsubscribe(long userId)
        {
            string subscriberToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            bool isUnsubscribed = userConnectionsRepository.Unsubscribe(usersConnection);

            if (isUnsubscribed)
            {
                sessionService.Commit();
            }
            else
            {
                sessionService.Rollback();
            }

            sessionService.EndSession();


            return Ok(isUnsubscribed);
        }
    }
}