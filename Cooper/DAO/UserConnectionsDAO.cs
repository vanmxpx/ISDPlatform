using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;
using Cooper.Configuration;

namespace Cooper.DAO
{
    public class UserConnectionsDAO : IDAO<UserConnectionDb>
    {
        private DbConnect dbConnect;
        private OracleConnection Connection;
        private Logger logger;

        private CRUD crud;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        private HashSet<string> unique_attributes;

        public UserConnectionsDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            dbConnect = new DbConnect(configProvider);
            Connection = dbConnect.GetConnection();
            logger = LogManager.GetLogger("CooperLoger");

            table = "USERSCONNECTIONS";
            idColumn = "ID";

            attributes = new HashSet<string>()
            {
                "ID", "AREFRIENDS", "BLACKLISTED", "IDUSER1", "IDUSER2"
            };

            unique_attributes = new HashSet<string>()
            {
                "ID"
            };
        }

        public UserConnectionDb Get(long id)
        {
            string attribute = "ID";

            //TODO: impelement getting process
            UserConnectionDb userConnection = null;

            return userConnection;
        }

        public IEnumerable<long> GetConnectionListByUserId(long userId)
        {
            string attribute = "ID";

            IEnumerable<long> userConnection = null;

            // TODO: GETTING USERS FROM DATABASE

            return userConnection;
        }

        public long Save(UserConnectionDb userConnection)
        {
            EntityORM entity = EntityMapping.Map(userConnection, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            long userConnection_id = crud.Create(table, idColumn, entity);

            logger.Info($"User with id = {userConnection_id} was created");

            return userConnection_id;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

            if (ifDeleted)
            {
                logger.Info($"User with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting userConnection with id={id} was failed.");
            }

        }

        public void Update(UserConnectionDb userConnection)
        {
            EntityORM entity = EntityMapping.Map(userConnection, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            bool ifUpdated = crud.Update(userConnection.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"User with id={userConnection.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating userConnection with id={userConnection.Id} was failed.");
            }
        }

        public IEnumerable<UserConnectionDb> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
