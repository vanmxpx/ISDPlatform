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
    public class UsersConnectionDAO : IUsersConnectionDAO
    {
        private DbConnect dbConnect;
        private OracleConnection Connection;
        private Logger logger;

        private ICRUD crud;

        private string idColumn;
        private string table;
        private HashSet<string> attributes;

        public UsersConnectionDAO(IConfigProvider configProvider)
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
        }

        public List<UserDb> GetSpecifiedTypeUsersList(object userId, ConnectionType connectionType)
        {
            List<UserDb> usersList = new List<UserDb>();

            var where_attributes = new Dictionary<string, object>();

            string user1_attribute = String.Empty;
            string user2_attribute = String.Empty;

            switch (connectionType)
            {
                case ConnectionType.Subscribers:
                    {
                        user1_attribute = "IDUSER1";
                        where_attributes.Add(user1_attribute, userId);
                        
                        user2_attribute = "IDUSER2";

                        break;
                    }
                case ConnectionType.Blacklist:
                    {
                        user1_attribute = "IDUSER1";

                        where_attributes.Add(user1_attribute, userId);
                        where_attributes.Add("BLACKLISTED", "\'y\'");

                        user2_attribute = "IDUSER2";
                        break;
                    }
                case ConnectionType.Subscriptions:
                    {
                        user1_attribute = "IDUSER2";
                        where_attributes.Add(user1_attribute, userId);

                        user2_attribute = "IDUSER1";
                        break;
                    }
                default:
                    break;                    
            }
            
            List<EntityORM> userConnections = GetAll(table, attributes, where_attributes);

            foreach (var usersConnection in userConnections)
            {
                UserDb user = new UserDb() {Id = Convert.ToInt64(usersConnection.attributeValue[user2_attribute]) };

                usersList.Add(user);
            }

            return usersList;
        }

        public bool CreateConnection(UsersConnectionDb usersConnection)
        {
            bool isCreated = false;
            UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

            if (ConnectionExists(symmetricConnection))
            {
                Dictionary<string, object> where_attributes = new Dictionary<string, object>()
                {
                    { "IDUSER1", usersConnection.IdUser1 },
                    { "IDUSER2", usersConnection.IdUser2 },
                    { "AREFRIENDS", "\'y\'"}
                };

                EntityORM symmetricConnection_newTyped = Mapping.EntityMapping.Map(symmetricConnection, attributes);
                Update(table, attributes, where_attributes, symmetricConnection_newTyped);

                usersConnection.AreFriends = true;
            }

            isCreated = Save(usersConnection);

            return isCreated;
        }

        private bool ConnectionExists(UsersConnectionDb usersConnection)
        {
            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 }
            };


            EntityORM usersConnection_newTyped = Get(table, attributes, where_attributes);

            return usersConnection_newTyped.attributeValue.Count != 0;
        }

        public bool BanUser(UsersConnectionDb usersConnection)
        {
            bool is_banned = false;
            bool isUnsubscribed = false;
            UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 },
                { "BLACKLISTED", usersConnection.BlackListed}
            };

            if (ConnectionExists(symmetricConnection))
            {
                isUnsubscribed = Unsubscribe(usersConnection);
            }

            if (ConnectionExists(usersConnection))
            {
                if (isUnsubscribed)
                {
                    where_attributes.Add("AREFRIEND", "\'n\'");
                }

                EntityORM usersConnection_newTyped = Mapping.EntityMapping.Map(usersConnection, attributes);
                is_banned = Update(table, attributes, where_attributes, usersConnection_newTyped);
            }
            else
            {
                is_banned = Save(usersConnection);
            }

            return is_banned;
        }

        public bool UnbanUser(UsersConnectionDb usersConnection)
        {
            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 }
            };

            bool isUnbanned = Delete(table, attributes, where_attributes);

            return isUnbanned;
        }

        public bool Unsubscribe(UsersConnectionDb usersConnection)
        {
            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 }
            };

            bool isUnsubscribed = Delete(table, attributes, where_attributes);

            UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

            if (ConnectionExists(symmetricConnection))
            {
                where_attributes["IDUSER1"] = symmetricConnection.IdUser1;
                where_attributes["IDUSER2"] = symmetricConnection.IdUser2;
                where_attributes.Add("AREFRIENDS", "\'n\'");

                EntityORM symmetricConnection_newTyped = Mapping.EntityMapping.Map(symmetricConnection, attributes);
                Update(table, attributes, where_attributes, symmetricConnection_newTyped);
            }

            return isUnsubscribed;
        }
        
        public bool Save(UsersConnectionDb usersConnection)
        {
            bool isSaved = false;

            EntityORM entity = EntityMapping.Map(usersConnection, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            long userConnection_id = crud.Create(table, idColumn, entity);

            if (userConnection_id != 0)
            {
                logger.Info($"UsersConnection with id = {userConnection_id} was created");
                isSaved = true;
            }

            return isSaved;
        }

        private List<EntityORM> GetAll(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes)
        {
            List<EntityORM> entities = new List<EntityORM>();

            try
            {
                dbConnect.OpenConnection();

                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string sqlExpression = String.Format("SELECT * FROM {0} WHERE {1}", table, where_part);
                
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

        private EntityORM Get(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes)
        {
            EntityORM entity = new EntityORM();

            try
            {
                dbConnect.OpenConnection();
                
                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string sqlExpression = String.Format("SELECT * FROM {0} WHERE {1}", table, where_part);                
                
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();

                reader.Read();

                foreach (string attribute in attributes)
                {
                    object value = reader[attribute];
                    entity.attributeValue.Add(attribute, value);
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

            return entity;
        }

        private bool Delete(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes)
        {
            bool isDeleted = false;
            
            try
            {
                dbConnect.OpenConnection();
                
                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string sqlExpression = string.Format("DELETE FROM {0} WHERE {1}", table, where_part);


                dbConnect.ExecuteNonQuery(sqlExpression);

                isDeleted = true;
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return isDeleted;
        }

        private bool Update(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes, EntityORM entity)
        {
            bool isUpdated = false;

            try
            {
                dbConnect.OpenConnection();

                #region Forming SQL-expression


                string set_part = string.Join(", ", entity.attributeValue.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));

                string sqlExpression = String.Format("UPDATE {0} SET {1} WHERE {2}", table, set_part, where_part);
                #endregion

                dbConnect.ExecuteNonQuery(sqlExpression);

                isUpdated = true;

            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return isUpdated;
        }

    }
}
