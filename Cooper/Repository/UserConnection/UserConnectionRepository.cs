using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.DAO.Models;
using Cooper.DAO;
using Cooper.Repository.Mapping;
using Cooper.Configuration;
namespace Cooper.Repository
{
    public class UserConnectionRepository : IUserConnectionRepository
    {
        private UserConnectionsDAO userConnectionDAO;
        private ModelsMapper mapper;
        

        public UserConnectionRepository(IConfigProvider configProvider)
        {
            userConnectionDAO = new UserConnectionsDAO(configProvider);
            mapper = new ModelsMapper();
        }
                
        public List<UserConnection> GetUserSubscribers(long userId)
        {
            return new List<UserConnection>();
        }

        public List<UserConnection> GetUserBlacklist(long userId)
        {
            return new List<UserConnection>();
        }

        public List<UserConnection> GetUserSubscribtions(long userId)
        {
            /*
            UserConnectionDb userConnection = userConnectionDAO.Get(id);
            UserConnection userConnection_newTyped = null;

            if (userConnection != null)
            {
                userConnection_newTyped = mapper.Map(userConnection);
            }
            */
            return new List<UserConnection>();
        }

        public bool CreateSubscription(UserConnection userConnection)
        {
            return false;
        }

        public bool BanUser(UserConnection userConnection)
        {
            return false;
        }

        public bool UnbanUser(UserConnection userConnection)
        {
            return false;
        }
        
        public bool Delete(UserConnection userConnection)
        {
            return false;
        }
        
    }
}
