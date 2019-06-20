using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.DAO.Models;
using Cooper.DAO;
using Cooper.Repository.Mapping;
using Cooper.Configuration;
using Cooper.UserConnectionsTypes;

namespace Cooper.Repository
{
    public class UserConnectionsRepository : IUserConnectionsRepository
    {
        private UserConnectionsDAO userConnectionsDAO;
        private ModelsMapper mapper;
        

        public UserConnectionsRepository(IConfigProvider configProvider)
        {
            userConnectionsDAO = new UserConnectionsDAO(configProvider);
            mapper = new ModelsMapper();
        }
                
        public List<UserConnections> GetUserSubscribers(long userId)
        {
            List<UserConnectionsDb> userConnections = userConnectionsDAO.Get(userId, ConnectionType.Subscribers);
            List<UserConnections> userConnections_newTyped = null;

            if (userConnections != null)
            {
                foreach(var userConnection in userConnections)
                {
                    userConnections_newTyped.Add(mapper.Map(userConnection));
                }
            }

            return userConnections_newTyped;
        }

        public List<UserConnections> GetUserBlacklist(long userId)
        {
            List<UserConnectionsDb> userConnections = userConnectionsDAO.GetUserBlacklist(userId);
            List<UserConnections> userConnections_newTyped = null;

            if (userConnections != null)
            {
                foreach (var userConnection in userConnections)
                {
                    userConnections_newTyped.Add(mapper.Map(userConnection));
                }
            }

            return userConnections_newTyped;
        }

        public List<UserConnections> GetUserSubscriptions(long userId)
        {
            List<UserConnectionsDb> userConnections = userConnectionsDAO.Get(userId, ConnectionType.Subscriptions);
            List<UserConnections> userConnections_newTyped = null;

            if (userConnections != null)
            {
                foreach (var userConnection in userConnections)
                {
                    userConnections_newTyped.Add(mapper.Map(userConnection));
                }
            }

            return userConnections_newTyped;
        }

        public bool CreateSubscription(UserConnections userConnections)
        {
            UserConnectionsDb userConnections_newType = mapper.Map(userConnections);

            bool ifCreated = (userConnectionsDAO.Save(userConnections_newType) != 0);

            return ifCreated;
        }

        public bool BanUser(UserConnections userConnections)
        {
            UserConnectionsDb userConnections_newTyped = new UserConnectionsDb();

            bool ifBanned = userConnectionsDAO.BanUser(userConnections_newTyped);

            return ifBanned;
        }

        public bool UnbanUser(UserConnections userConnection)
        {

            UserConnectionsDb userConnections_newTyped = new UserConnectionsDb();

            bool ifUnbanned = userConnectionsDAO.UnbanUser(userConnections_newTyped);

            return ifUnbanned;
        }
        
        public bool Delete(UserConnections userConnection)
        {
            UserConnectionsDb userConnections_newTyped = new UserConnectionsDb();

            bool ifDeleted = userConnectionsDAO.Delete(userConnections_newTyped);

            return ifDeleted;
        }
        
    }
}
