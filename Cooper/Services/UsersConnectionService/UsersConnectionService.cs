using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Repository;
using Cooper.Models;
using Cooper.Configuration;

namespace Cooper.Services
{
    public class UsersConnectionService : IUsersConnectionService
    {
        private readonly IUserRepository userRepository;
        public UsersConnectionService(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userRepository = new UserRepository(jwtService, configProvider);
        }

        public UsersConnection CreateConnection(long userId, string subscriberToken, bool ban = false)
        {
            User user = new User() { Id = userId };

            User subscriber = userRepository.GetByJWToken(subscriberToken);

            UsersConnection userConnection = new UsersConnection() { User1 = user, User2 = subscriber, BlackListed = ban};

            return userConnection;
        }
        public UsersConnection CreateConnection(string userToken, long subscriberId, bool ban = false)
        {
            User subscriber = new User() { Id = subscriberId };

            User user = userRepository.GetByJWToken(userToken);

            UsersConnection userConnection = new UsersConnection() { User1 = user, User2 = subscriber, BlackListed = ban };

            return userConnection;
        }

        public long GetUserId(string user_token)
        {
            User user = userRepository.GetByJWToken(user_token);

            return user.Id;
        }
    }
}
