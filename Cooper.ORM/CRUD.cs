using Cooper.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Cooper.ORM
{
    public class CRUD : ICRUD
    {
        private readonly DbConnect dbConnect;
        private readonly Logger logger;

        public CRUD(IConfigProvider configProvider)
        {
            dbConnect = new DbConnect(configProvider);
            logger = LogManager.GetCurrentClassLogger();
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

                if (returning)
                {
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
                string sqlExpression = DbTools.CreateQuery(table, attributes, whereRequests);

                dbConnect.OpenConnection();
                OracleCommand command = new OracleCommand(sqlExpression, dbConnect.GetConnection());

                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EntityORM entity = new EntityORM();
                    foreach (string attribute in attributes)
                    {
                        entity.attributeValue.Add(DbTools.GetVariableAttribute(attribute), reader[attribute]);
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