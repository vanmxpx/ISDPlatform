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
    public class UserConnectionRepository : IRepository<UserConnection>
    {
        private UserConnectionsDAO userConnectionDAO;
        private ModelsMapper mapper;
        

        public UserConnectionRepository(IConfigProvider configProvider)
        {
            userConnectionDAO = new UserConnectionsDAO(configProvider);
            mapper = new ModelsMapper();
        }

        #region Main methods
        
        public UserConnection Get(long id)
        {
            UserConnectionDb userConnection = userConnectionDAO.Get(id);
            UserConnection userConnection_newTyped = null;

            if (userConnection != null)
            {
                userConnection_newTyped = mapper.Map(userConnection);
            }

            return userConnection_newTyped;
        }

        public long Create(UserConnection userConnection)
        {
            UserConnectionDb userConnectionDb = mapper.Map(userConnection);

            return userConnectionDAO.Save(userConnectionDb);
        }

        public void Update(UserConnection userConnection)
        {
            UserConnectionDb userConnectionDb = mapper.Map(userConnection);

            userConnectionDAO.Update(userConnectionDb);
        }

        public void Delete(long id)
        {
            userConnectionDAO.Delete(id);
        }

        public IEnumerable<UserConnection> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
