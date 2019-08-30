using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.Models.UserConnectionsEnumTypes;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Cooper.DAO
{
    public class UsersConnectionDAO : IUsersConnectionDAO
    {
        private readonly DbConnect dbConnect;
        private readonly OracleConnection Connection;
        private readonly Logger logger;
        private readonly ICRUD crud;
        private readonly IUserDAO userDAO;

        private string idColumn;
        private string table;
        private HashSet<string> attributes;

        public UsersConnectionDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            dbConnect = new DbConnect(configProvider);
            Connection = dbConnect.GetConnection();
            logger = LogManager.GetLogger("CooperLoger");

            userDAO = new UserDAO(configProvider);

            table = "USERSCONNECTIONS";
            idColumn = "ID";

            attributes = new HashSet<string>()
            {
                "ID", "AREFRIENDS", "BLACKLISTED", "IDUSER1", "IDUSER2"
            };
        }

        public List<UserDb> GetSpecifiedTypeUsersList(object userId, ConnectionType connectionType)
        {
            List<UserDb> usersList = null;

            var where_attributes = new Dictionary<string, object>();

            string user1_attribute = String.Empty;
            string user2_attribute = String.Empty;

            switch (connectionType)
            {
                case ConnectionType.Subscribers:
                    {
                        user1_attribute = "IDUSER1";
                        where_attributes.Add(user1_attribute, userId);
                        where_attributes.Add("BLACKLISTED", "\'n\'");

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
                        where_attributes.Add("BLACKLISTED", "\'n\'");

                        user2_attribute = "IDUSER1";
                        break;
                    }
                case ConnectionType.Friends:
                    {
                        user1_attribute = "IDUSER1";

                        where_attributes.Add(user1_attribute, userId);
                        where_attributes.Add("AREFRIENDS", "\'y\'");

                        user2_attribute = "IDUSER2";
                        break;
                    }
                default:
                    break;                    
            }
            
            List<EntityORM> userConnections = GetAll(table, attributes, where_attributes);

            if (userConnections != null)
            {
                usersList = new List<UserDb>();

                foreach (var usersConnection in userConnections)
                {
                    long relatedUserId = Convert.ToInt64(usersConnection.attributeValue[user2_attribute]);
                    UserDb user = userDAO.Get(relatedUserId);

                    usersList.Add(user);
                }
            }

            return usersList;
        }

        public bool CreateConnection(UsersConnectionDb usersConnection)
        {
            Connection.Open();
            OracleTransaction transaction = Connection.BeginTransaction();
            
            bool isCreated = true;            

            try
            {
                if (ConnectionExists(usersConnection, transaction))
                    return false;

                UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

                var where_attributes = new Dictionary<string, object>()
                {
                    { "IDUSER1", symmetricConnection.IdUser1 },
                    { "IDUSER2", symmetricConnection.IdUser2 }
                };

                EntityORM entity = Get(table, attributes, where_attributes, transaction);

                if (entity != null)
                {
                    if (DbTools.ProcessBoolean(entity.attributeValue["BLACKLISTED"]))
                    {
                        isCreated = false;

                        return isCreated;
                    }

                    symmetricConnection.AreFriends = true;

                    EntityORM symmetricConnection_newTyped = Mapping.EntityMapping.Map(symmetricConnection, attributes);

                    // Making sure that ID value is not touched.
                    symmetricConnection_newTyped.attributeValue.Remove("ID");

                    Update(table, attributes, where_attributes, symmetricConnection_newTyped, transaction);

                    usersConnection.AreFriends = true;
                }

                where_attributes["IDUSER1"] = usersConnection.IdUser1;
                where_attributes["IDUSER2"] = usersConnection.IdUser2;

                EntityORM usersConnection_newTyped = Mapping.EntityMapping.Map(usersConnection, attributes);

                // Making sure that ID value is not touched.
                usersConnection_newTyped.attributeValue.Remove("ID");

                Create(table, usersConnection_newTyped, transaction);

                transaction.Commit();
            }
            catch (DbException ex)
            {
                logger.Info("Creating subscription user with id={1} on user with id={0} failed: {2}", usersConnection.IdUser1, usersConnection.IdUser2, ex.Message);
                transaction.Rollback();
            }
            finally
            {
                Connection.Close();
            }

            return isCreated;
        }

        private bool ConnectionExists(UsersConnectionDb usersConnection, OracleTransaction transaction)
        {
            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 }
            };


            EntityORM usersConnection_newTyped = Get(table, attributes, where_attributes, transaction);

            return usersConnection_newTyped != null;
        }

        public bool BanUser(UsersConnectionDb usersConnection)
        {

            bool isBanned = true;
            bool isUnsubscribed = false;
            

            Connection.Open();
            OracleTransaction transaction = Connection.BeginTransaction();

            try
            {
                UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

                var where_attributes = new Dictionary<string, object>()
                {
                    { "IDUSER1", symmetricConnection.IdUser1 },
                    { "IDUSER2", symmetricConnection.IdUser2 }
                };

                if (ConnectionExists(symmetricConnection, transaction))
                {
                    isUnsubscribed = true;
                    Delete(table, attributes, where_attributes, transaction);
                }

                where_attributes["IDUSER1"] = usersConnection.IdUser1;
                where_attributes["IDUSER2"] = usersConnection.IdUser2;



                EntityORM usersConnection_newTyped = Mapping.EntityMapping.Map(usersConnection, attributes);

                // Making sure that ID value is not touched.
                usersConnection_newTyped.attributeValue.Remove("ID");

                if (ConnectionExists(usersConnection, transaction))
                {
                    if (isUnsubscribed)
                    {
                        usersConnection.AreFriends = false;
                    }

                    Update(table, attributes, where_attributes, usersConnection_newTyped, transaction);
                }
                else
                {
                    Create(table, usersConnection_newTyped, transaction);
                }
                transaction.Commit();

            }
            catch (DbException ex)
            {
                logger.Info("Banning user with id={1} by user with id={0} failed: {2}", usersConnection.IdUser1, usersConnection.IdUser2, ex.Message);
                transaction.Rollback();
                isBanned = false;
            }
            finally
            {
                Connection.Close();
            }

            return isBanned;
        }

        public bool UnbanUser(UsersConnectionDb usersConnection)
        {
            bool isUnbanned = true;

            var where_attributes = new Dictionary<string, object>()
            {
                { "IDUSER1", usersConnection.IdUser1 },
                { "IDUSER2", usersConnection.IdUser2 }
            };

            Connection.Open();
            OracleTransaction transaction = Connection.BeginTransaction();

            try
            {
                Delete(table, attributes, where_attributes, transaction);
                transaction.Commit();
            }
            catch (DbException ex)
            {
                logger.Info("Unbanning user with id={1} by user with id={0} failed: {2}", usersConnection.IdUser1, usersConnection.IdUser2, ex.Message);
                isUnbanned = false;
            }
            finally
            {
                Connection.Close();
            }


            return isUnbanned;
        }

        public bool Unsubscribe(UsersConnectionDb usersConnection)
        {
            bool isUnsubscribed = true;
            
            Connection.Open();
            OracleTransaction transaction = Connection.BeginTransaction();

            try
            {
                var where_attributes = new Dictionary<string, object>()
                {
                    { "IDUSER1", usersConnection.IdUser1 },
                    { "IDUSER2", usersConnection.IdUser2 }
                };
                Delete(table, attributes, where_attributes, transaction);

                UsersConnectionDb symmetricConnection = new UsersConnectionDb() { IdUser1 = usersConnection.IdUser2, IdUser2 = usersConnection.IdUser1 };

                if (ConnectionExists(symmetricConnection, transaction))
                {
                    where_attributes["IDUSER1"] = symmetricConnection.IdUser1;
                    where_attributes["IDUSER2"] = symmetricConnection.IdUser2;
                    symmetricConnection.AreFriends = false;

                    EntityORM symmetricConnection_newTyped = Mapping.EntityMapping.Map(symmetricConnection, attributes);

                    // Making sure that ID value is not touched.
                    symmetricConnection_newTyped.attributeValue.Remove("ID");

                    Update(table, attributes, where_attributes, symmetricConnection_newTyped, transaction);
                }

                transaction.Commit();
                logger.Info($"User with id={usersConnection.IdUser1} has succesfully unsubscribed from user with id={usersConnection.IdUser2}");
            }
            catch (DbException ex)
            {
                logger.Info("Unsubcription user with id={0} from user with id={1} failed: {2}", usersConnection.IdUser1, usersConnection.IdUser2, ex.Message);
                transaction.Rollback();

                isUnsubscribed = false;
            }
            finally
            {
                Connection.Close();
            }

            return isUnsubscribed;
        }

        public void Create(string table, EntityORM entity, OracleTransaction transaction)
        {

            #region Creating SQL expression text
            string sqlExpression = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                table,
                String.Join(",", entity.attributeValue.Keys),
                String.Join(",", entity.attributeValue.Values));
            #endregion
            
            OracleCommand command = new OracleCommand(sqlExpression, Connection);
            command.Transaction = transaction;

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets all the records which satisfy sql-expression with WHERE keyword. In the other way returns null.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="attributes"></param>
        /// <param name="where_attributes"></param>
        /// <returns></returns>
        private List<EntityORM> GetAll(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes)
        {
            List<EntityORM> entities = null;

            try
            {
                dbConnect.OpenConnection();

                string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
                string sqlExpression = String.Format("SELECT * FROM {0} WHERE {1}", table, where_part);
                
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    entities = new List<EntityORM>();

                    do
                    {
                        EntityORM entity = new EntityORM();
                        foreach (string attribute in attributes)
                        {
                            object value = reader[attribute];
                            entity.attributeValue.Add(attribute, value);
                        }

                        entities.Add(entity);
                    } while (reader.Read());

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

        /// <summary>
        ///  Gets the record which satisfy sql-expression with WHERE keyword. In the other way returns null.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="attributes"></param>
        /// <param name="where_attributes"></param>
        /// <returns></returns>
        private EntityORM Get(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes, OracleTransaction transaction)
        {
            EntityORM entity = null;

            string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            string sqlExpression = String.Format("SELECT * FROM {0} WHERE {1}", table, where_part);

            OracleCommand command = new OracleCommand(sqlExpression, Connection);
            command.Transaction = transaction;

            OracleDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                entity = new EntityORM();

                foreach (string attribute in attributes)
                {
                    object value = reader[attribute];
                    entity.attributeValue.Add(attribute, value);
                }
            }

            return entity;
        }

        private void Delete(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes, OracleTransaction transaction)
        {
            string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            string sqlExpression = string.Format("DELETE FROM {0} WHERE {1}", table, where_part);
            

            OracleCommand command = new OracleCommand(sqlExpression, Connection);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
        }

        private void Update(string table, HashSet<string> attributes, Dictionary<string, object> where_attributes, EntityORM entity, OracleTransaction transaction)
        {
            
            #region Forming SQL-expression


            string set_part = string.Join(", ", entity.attributeValue.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            string where_part = string.Join(" AND ", where_attributes.Select(x => string.Format("{0}={1}", x.Key, x.Value)));

            string sqlExpression = String.Format("UPDATE {0} SET {1} WHERE {2}", table, set_part, where_part);
            #endregion


            var oracleCommand = new OracleCommand(sqlExpression, Connection);
            oracleCommand.Transaction = transaction;

            oracleCommand.ExecuteNonQuery();
        }

    }
}
