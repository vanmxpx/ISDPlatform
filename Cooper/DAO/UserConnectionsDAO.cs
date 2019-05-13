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
using System.Data.Common;

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

        public UserConnectionDb Get(long connectionId)
        {
            UserConnectionDb userConnection = null;

            string attribute = "ID";

            //TODO: impelement getting process
            EntityORM entity = crud.Read(connectionId, attribute, attributes, table);
            
            if (entity != null)
            {
                EntityMapping.Map(entity, out userConnection);
            }

            return userConnection;
        }

        public IEnumerable<long> GetConnectionsListByUserId(long userId)
        {
            string[] query_attributes = { "IDUSER1", "IDUSER2" };


            #region Processing a query

            List<UserConnectionDb> userConnectionsDb = new List<UserConnectionDb>();

            List<EntityORM> entities = new List<EntityORM>();

            try
            {
                string sqlExpression = $"SELECT * from {table} where {query_attributes[0]} = {userId} UNION " +
                    $"SELECT * from {table} where {query_attributes[1]} = {userId}";

                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EntityORM entity = new EntityORM();
                    foreach (string attribute in attributes)
                    {
                        object value = reader[attribute];
                        entity.attributeValue.Add(attribute, value);
                    }

                    entities.Add(entity);
                }
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            #endregion

            #region Mapping entity

            foreach (EntityORM entity in entities)
            {
                EntityMapping.Map(entity, out UserConnectionDb userConnection);
                userConnectionsDb.Add(userConnection);
            }
            
            #endregion


            List<long> userConnections = new List<long>();

            foreach (var connection in userConnectionsDb)
            {
                userConnections.Add(connection.Id);
            }

            return userConnections;
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
