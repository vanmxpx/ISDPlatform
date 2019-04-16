using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using NLog;
namespace Cooper.ORM
{
    public class CRUD : ICRUD
    {
        private DbConnect dbConnect;
        private OracleConnection Connection;
        private Logger logger;

        public CRUD()
        {
            dbConnect = DbConnect.GetInstance();
            Connection = dbConnect.GetConnection();
            logger = LogManager.GetLogger("CooperLoger");
        }
        
        public long Create(string table, string idColumn, EntityORM entity)
        {
            long insertId = 0;

            try
            {
                #region Creating SQL expression text

                string sqlExpression = $"INSERT INTO {table} ";
                string attributes = "( ";
                string values = "VALUES ( ";

                foreach (KeyValuePair<string, object> aV in entity.attributeValue)  // tuning command content
                {
                    attributes += $"{aV.Key}, ";
                    values += $"{aV.Value}, ";
                }

                attributes = attributes.TrimEnd(',', ' ') + ')';
                values = values.TrimEnd(',', ' ') + ')';

                sqlExpression += $"{attributes} {values} returning {idColumn} into :id";
                //sqlExpression = "INSERT INTO USERS ( NAME, NICKNAME, EMAIL, PASSWORD, PHOTOURL, ISVERIFIED, ISCREATOR, ISBANNED, ENDBANDATE, PLATFORMLANGUAGE, PLATFORMTHEME) VALUES(\'Test User\', \'test_user\', \'\', \'qwertynya\', \'photoURL\', \'n\', \'n\', \'n\', \'13-JAN-2000\', \'English\', \'Dark\') returning ID into :id";
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
                Connection.Close();
            }

            return insertId;
        }

        public EntityORM Read(object attribute_value, string attribute_name, HashSet<string> attributes, string table)
        {
            EntityORM entity = null;

            try
            {
                string sqlExpression = $"SELECT * from {table} where {attribute_name} = {attribute_value}";

                Connection.Open();
                OracleCommand command = new OracleCommand(sqlExpression, Connection);

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
                Connection.Close();
            }


            return entity;
        }

        public IEnumerable<EntityORM> ReadAll(string table, HashSet<string> attributes)
        {
            List<EntityORM> entities = new List<EntityORM>();
            
            try
            {
                string sqlExpression = $"SELECT * from {table}";

                Connection.Open();
                OracleCommand command = new OracleCommand(sqlExpression, Connection);

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
                Connection.Close();
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
                Connection.Close();
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
                Connection.Close();
            }

            return true;
        }
    }
}
