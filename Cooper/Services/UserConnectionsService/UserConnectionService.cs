using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Repository;
using Cooper.Models;
using Cooper.Configuration;

namespace Cooper.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly IUserRepository userRepository;
        public UserConnectionService(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userRepository = new UserRepository(jwtService, configProvider);
        }

        public UserConnection CreateConnection(long userId, string subscriberToken, bool ban = false)
        {
            User user = new User() { Id = userId };

            User subscriber = userRepository.GetByJWToken(subscriberToken);

            UserConnection userConnection = new UserConnection() { User1 = user, User2 = subscriber, BlackListed = ban};

            return userConnection;
        }
    }
}
