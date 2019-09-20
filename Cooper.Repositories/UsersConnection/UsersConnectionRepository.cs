using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Models.UserConnectionsEnumTypes;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Cooper.Repositories
{
    public class UsersConnectionRepository : IUsersConnectionRepository
    {
        private readonly IUsersConnectionDAO userConnectionsDAO;
        private readonly ModelsMapper mapper;

        public UsersConnectionRepository(IConfigProvider configProvider, ISessionService sessionService)
        {
            userConnectionsDAO = new UsersConnectionDAO(configProvider, sessionService);
            mapper = new ModelsMapper();
        }
        public List<User> GetSpecifiedTypeUsersList(long userId, ConnectionType specifiedType)
        {
            List<UserDb> users = userConnectionsDAO.GetSpecifiedTypeUsersList(userId, specifiedType);
            List<User> users_newTyped = null;

            if (users != null)
            {
                users_newTyped = users.Select(x => mapper.Map(x)).ToList();
            }

            return users_newTyped;
        }
        
        public bool CreateSubscription(UsersConnection userConnections)
        {
            UsersConnectionDb userConnections_newType = mapper.Map(userConnections);

            bool isCreated = (userConnectionsDAO.CreateConnection(userConnections_newType) == true);

            return isCreated;
        }
        
        public bool Unsubscribe(UsersConnection usersConnection)
        {
            UsersConnectionDb usersConnection_newTyped = mapper.Map(usersConnection);

            bool isUnsubscribed = userConnectionsDAO.Unsubscribe(usersConnection_newTyped);

            return isUnsubscribed;
        }

        public bool BanUser(UsersConnection usersConnection)
        {
            UsersConnectionDb usersConnection_newTyped = mapper.Map(usersConnection);

            bool isBanned = userConnectionsDAO.BanUser(usersConnection_newTyped);

            return isBanned;
        }

        public bool UnbanUser(UsersConnection usersConnection)
        {

            UsersConnectionDb usersConnection_newTyped = mapper.Map(usersConnection);

            bool isUnbanned = userConnectionsDAO.UnbanUser(usersConnection_newTyped);

            return isUnbanned;
        }
        
    }
}
