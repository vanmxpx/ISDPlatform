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
        private readonly ISession session;

        public UsersConnectionController(IConfigProvider configProvider, IUsersConnectionService userConnectionService,
            ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();

            userConnectionsRepository = new UsersConnectionRepository(session);
            this.userConnectionService = userConnectionService;
        }

        [HttpGet("blacklist"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserBlackList()
        {
            string userToken = Request.GetUserToken();

            long userId = userConnectionService.GetUserId(userToken, session);

            List<User> blackList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Blacklist);

            session.EndSession();
            return Ok(blackList);
        }

        [HttpGet("subscribers/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscribersList(long userId)
        {
            List<User> subscribersList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscribers);
            
            session.EndSession();
            return Ok(subscribersList);
        }

        [HttpGet("subscriptions/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserSubscriptionsList(long userId)
        {
            List<User> subscriptionsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Subscriptions);

            session.EndSession();
            return Ok(subscriptionsList);
        }

        [HttpGet("friends/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UsersConnection))]
        [ProducesResponseType(404)]
        public IActionResult GetUserFriendsList(long userId)
        {
            List<User> friendsList = userConnectionsRepository.GetSpecifiedTypeUsersList(userId, ConnectionType.Friends);

            session.EndSession();
            return Ok(friendsList);
        }

        [HttpPost("subscribe/{userId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Subscribe(long userId)
        {

            string subscriberToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken, session);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            IActionResult result;

            session.StartSession();

            bool isSubscribed = userConnectionsRepository.CreateSubscription(usersConnection);

            if (isSubscribed)
            {
                session.Commit(endSession: true);
                result = Ok(isSubscribed);
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }

        [HttpPost("ban/{bannedUserId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Ban(long bannedUserId)
        {

            string userToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, session, ban: true);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            IActionResult result;

            session.StartSession();

            bool isBanned = userConnectionsRepository.BanUser(usersConnection);
            
            if (isBanned)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }

        [HttpPost("unban/{bannedUserId}"), Authorize]
        public IActionResult Unban(long bannedUserId)
        {

            string userToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userToken, bannedUserId, session, ban: false);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            IActionResult result;

            session.StartSession();

            bool isUnbanned = userConnectionsRepository.UnbanUser(usersConnection);
            
            if (isUnbanned)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }

        [HttpDelete("{userId}"), Authorize]
        public IActionResult Unsubscribe(long userId)
        {
            string subscriberToken = Request.GetUserToken();

            UsersConnection usersConnection = userConnectionService.CreateConnection(userId, subscriberToken, session);

            if (usersConnection.User1.Id == usersConnection.User2.Id)
            {
                return BadRequest();
            }

            IActionResult result;

            session.StartSession();

            bool isUnsubscribed = userConnectionsRepository.Unsubscribe(usersConnection);

            if (isUnsubscribed)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }


            return result;
        }
    }
}