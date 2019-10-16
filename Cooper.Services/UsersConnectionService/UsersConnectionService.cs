using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class UsersConnectionService : IUsersConnectionService
    {
        private readonly IJwtHandlerService jwtService;

        public UsersConnectionService(IJwtHandlerService jwtService)
        {
            this.jwtService = jwtService;
        }

        public UsersConnection CreateConnection(long userId, string subscriberToken, ISession session, bool ban = false)
        {
            User user = new User() { Id = userId };

            User subscriber = new UserRepository(jwtService, session).GetByJWToken(subscriberToken);

            UsersConnection userConnection = new UsersConnection() { User1 = user, User2 = subscriber, BlackListed = ban};

            return userConnection;
        }
        public UsersConnection CreateConnection(string userToken, long subscriberId, ISession session, bool ban = false)
        {
            User subscriber = new User() { Id = subscriberId };

            User user = new UserRepository(jwtService, session).GetByJWToken(userToken);

            UsersConnection userConnection = new UsersConnection() { User1 = user, User2 = subscriber, BlackListed = ban };

            return userConnection;
        }

        public long GetUserId(string user_token, ISession session)
        {
            User user = new UserRepository(jwtService, session).GetByJWToken(user_token);

            return user.Id;
        }
    }
}
