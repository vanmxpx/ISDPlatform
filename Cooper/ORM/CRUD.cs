using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using NLog;
using Cooper.Configuration;

namespace Cooper.ORM
{
    public class CRUD : ICRUD
    {
        private DbConnect dbConnect;
        private Logger logger;

        public CRUD(IConfigProvider configProvider)
        {
            dbConnect = new DbConnect(configProvider);
            logger = LogManager.GetLogger("CooperLoger");
        }
        
        public long Create(string table, string idColumn, EntityORM entity)
        {
            long insertId = 0;

            try
            {
                #region Creating SQL expression text

                string sqlExpression = String.Format("INSERT INTO {0} ({1}) VALUES ({2}) returning {3} into :id",
                    table,
                    String.Join(",", entity.attributeValue.Keys),
                    String.Join(",", entity.attributeValue.Values),
                    idColumn);

                    Console.WriteLine($"{sqlExpression}");
                #endregion

                insertId = long.Parse(dbConnect.ExecuteNonQuery(sqlExpression, getId: true).ToString());

            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return insertId;
        }

        public EntityORM Read(object attribute_value, string attribute_name, HashSet<string> attributes, string table)
        {
            EntityORM entity = null;

            try
            {
                string sqlExpression = $"SELECT * FROM {table} WHERE {attribute_name} = {attribute_value}";

                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    entity = new EntityORM();
                    foreach (string attribute in attributes)
                    {
                        object value = reader[attribute];
                        entity.attributeValue.Add(attribute, value);
                    }
                }

                reader.Close();
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

        public IEnumerable<EntityORM> ReadSubset(object attribute_value, string attribute_name, HashSet<string> attributes, string table)
        {
            EntityORM entity = null;
            List<EntityORM> entities = new List<EntityORM>();

            try
            {
                string sqlExpression = $"SELECT * from {table} where {attribute_name} = {attribute_value}";

                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, Connection);

                OracleDataReader reader = command.ExecuteReader();

                //reader.Read();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        entity = new EntityORM();
                        foreach (string attribute in attributes)
                        {
                            object value = reader[attribute];
                            entity.attributeValue.Add(attribute, value);
                        }
                    }
                    entities.Add(entity);
                }

                reader.Close();
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


        public IEnumerable<EntityORM> ReadAll(string table, HashSet<string> attributes)
        {
            List<EntityORM> entities = new List<EntityORM>();
            
            try
            {
                string sqlExpression = $"SELECT * from {table}";

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

            return entities;
        }

        public bool Update(long id, string table, string idColumn, EntityORM entity)
        {
            try
            {
                #region Forming SQL expression text

                string sqlExpression = $"UPDATE {table} SET ";

                foreach (KeyValuePair<string, object> aV in entity.attributeValue)  // tuning command content
                {
                    sqlExpression += $"{aV.Key} = {aV.Value}, ";
                }

                sqlExpression = sqlExpression.TrimEnd(' ', ',');
                sqlExpression += $" WHERE {idColumn} = {id}";

                #endregion

                dbConnect.ExecuteNonQuery(sqlExpression);
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
                return false;
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return true;
        }

        public bool Delete(long id, string table, string idColumn)
        {
            try
            {
                string sqlExpression = $"delete from {table} where {idColumn} = {id}";
                
                dbConnect.ExecuteNonQuery(sqlExpression);
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
                return false;
            }
            finally
            {
                dbConnect.CloseConnection();
            }

            return true;
        }
    }
}
