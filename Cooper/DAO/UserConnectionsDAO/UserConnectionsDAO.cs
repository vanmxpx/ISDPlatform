using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;
using Cooper.Configuration;
using Cooper.Models.UserConnectionsEnumTypes;

namespace Cooper.DAO
{
    public class UserConnectionsDAO
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

        public List<UserConnectionsDb> GetSpecifiedTypeUsersList(object userId, ConnectionType connectionType)
        {
            List<EntityORM> usersConnections;
            List<UserConnectionsDb> usersConnections_newTyped = new List<UserConnectionsDb>();

            EntityORM whereAttributes = new EntityORM();
            string userAttribute = String.Empty;
            string banAttribute = "BLACKLISTED";

            switch (connectionType)
            {
                case ConnectionType.Subscribers:
                    {
                        userAttribute = "IDUSER1";
                        whereAttributes.attributeValue.Add(userAttribute, userId);

                        break;
                    }
                case ConnectionType.Subscriptions:
                    {
                        userAttribute = "IDUSER2";
                        whereAttributes.attributeValue.Add(userAttribute, userId);

                        break;
                    }
                case ConnectionType.Blacklist:
                    {
                        userAttribute = "IDUSER1";

                        whereAttributes.attributeValue.Add(userAttribute, userId);
                        whereAttributes.attributeValue.Add(banAttribute, "\'y\'");
                            
                        break;
                    }
                default:
                    break;                    
            }

            usersConnections = Get(table, attributes, whereAttributes);

            foreach (var userConnections in usersConnections)
            {
                UserConnectionsDb userConnections_newTyped = null;

                EntityMapping.Map(userConnections, out userConnections_newTyped);

                usersConnections_newTyped.Add(userConnections_newTyped);
            }

            return usersConnections_newTyped;
        }
        
        public List<EntityORM> Get(string table, HashSet<string> attributes, EntityORM where_attributes)
        {
            List<EntityORM> entities = new List<EntityORM>();

            try
            {
                dbConnect.OpenConnection();

                #region Forming SQL-Expression

                string sqlExpression = String.Format("SELECT * FROM {0} WHERE ", table);

                foreach (var attribute in where_attributes.attributeValue)
                {
                    sqlExpression += $"{attribute.Key} = {attribute.Value} AND ";
                }

                sqlExpression = sqlExpression.Substring(0, sqlExpression.Length - 4);

                #endregion

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

            return entities;
        }

        public List<UserConnectionsDb> GetUserBlacklist(object connectionId)
        {
            UserConnectionsDb userConnection = null;

            string attribute = "ID";

            //TODO: impelement getting process
            EntityORM entity = crud.Read(connectionId, attribute, attributes, table);

            if (entity != null)
            {
                EntityMapping.Map(entity, out userConnection);
            }

            return new List<UserConnectionsDb>();
        }
        
        public long Save(UserConnectionsDb userConnection)
        {
            EntityORM entity = EntityMapping.Map(userConnection, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            long userConnection_id = crud.Create(table, idColumn, entity);

            logger.Info($"User with id = {userConnection_id} was created");

            return userConnection_id;
        }

        public bool BanUser(UserConnectionsDb userConnections)
        {
            return false;
        }

        public bool UnbanUser(UserConnectionsDb userConnections)
        {
            return false;
        }

        public bool Delete(UserConnectionsDb userConnections)
        {
            /*
            bool isDeleted = crud.Delete(id, table, idColumn);

            if (isDeleted)
            {
                logger.Info($"User with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting userConnection with id={id} was failed.");
            }*/

            return false;
        }

        public void Update(UserConnectionsDb userConnection)
        {
            EntityORM entity = EntityMapping.Map(userConnection, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            bool isUpdated = crud.Update(userConnection.Id, table, idColumn, entity);

            if (isUpdated)
            {
                logger.Info($"User with id={userConnection.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating userConnection with id={userConnection.Id} was failed.");
            }
        }

    }
}
