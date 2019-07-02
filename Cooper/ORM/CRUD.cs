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
        
        public long Create(string table, string idColumn, EntityORM entity, bool returning = true)
        {
            long insertId = 0;

            try
            {
                dbConnect.OpenConnection();

                #region Creating SQL expression text
                string sqlExpression = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                    table,
                    String.Join(",", entity.attributeValue.Keys),
                    String.Join(",", entity.attributeValue.Values));

                if (returning) {
                    sqlExpression += $" returning {idColumn} into :id";
                    Console.WriteLine($"{sqlExpression}");
                    insertId = long.Parse(dbConnect.ExecuteNonQuery(sqlExpression, getId: true).ToString());
                }
                else 
                {
                    Console.WriteLine($"{sqlExpression}");
                    dbConnect.ExecuteNonQuery(sqlExpression);
                }
                #endregion

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

        public IEnumerable<EntityORM> Read(string table, HashSet<string> attributes, DbTools.WhereRequest[] whereRequests = null)
        {   
            List<EntityORM> entities = new List<EntityORM>();
            try
            {
                string where = "";
                if (whereRequests != null) {
                    where = " WHERE";
                    foreach (DbTools.WhereRequest request in whereRequests)
                    {
                        where += String.Format(" {0} {1} {2},", request.variable_name, DbTools.GetOperatorString(request.request_operator), request.variable_value);
                    }
                    where = where.Remove(where.Length - 1);
                }
                string sqlExpression = String.Format("SELECT {0} FROM {1}{2}", 
                    (attributes == null)? string.Join(", ", attributes) : "*", 
                    table, where);

                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EntityORM entity = new EntityORM(); 
                    foreach (string attribute in attributes)
                    {
                        string attribute_variable = DbTools.GetVariableAttribute(attribute);
                        entity.attributeValue.Add(attribute_variable, reader[attribute_variable]);
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
                dbConnect.OpenConnection();
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

        public bool Delete(object id, string table, string idColumn)
        {
            try
            {
                dbConnect.OpenConnection();
                string sqlExpression = $"DELETE FROM {table} WHERE {idColumn} = {id}";
                
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