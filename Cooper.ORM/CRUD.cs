using Cooper.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace Cooper.ORM
{
    public class CRUD : ICRUD
    {
        private readonly ISession session;
        private readonly Logger logger;

        public CRUD(ISession session)
        {
            this.session = session;
            logger = LogManager.GetCurrentClassLogger();
        }

        public long Create(string table, string idColumn, EntityORM entity, bool returning = true)
        {
            long insertId = 0;

            try
            {

                #region Creating SQL expression text
                string sqlExpression = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                    table,
                    String.Join(",", entity.attributeValue.Keys),
                    String.Join(",", entity.attributeValue.Values));

                if (returning)
                {
                    sqlExpression += $" returning {idColumn} into :id";
                    Console.WriteLine($"{sqlExpression}");
                    insertId = long.Parse(session.ExecuteNonQuery(sqlExpression, getId: true).ToString());
                }
                else
                {
                    Console.WriteLine($"{sqlExpression}");
                    session.ExecuteNonQuery(sqlExpression);
                }
                #endregion

            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
            }

            return insertId;
        }

        public IEnumerable<EntityORM> Read(string table, HashSet<string> attributes, WhereRequest whereRequest = null)
        {
            List<EntityORM> entities = new List<EntityORM>();
            try
            {
                var connection = (OracleConnection)session.GetConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string sqlExpression = DbTools.CreateSelectQuery(table, attributes, whereRequest);
                
                OracleCommand command = new OracleCommand(sqlExpression, connection);

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

            return entities;
        }

        public bool Update(string table, EntityORM entity, WhereRequest whereRequest = null)
        {

            string sqlExpression = DbTools.CreateUpdateQuery(table, entity, whereRequest);

            try
            {
                var oracleCommand = new OracleCommand(sqlExpression, (OracleConnection)session.GetConnection())
                {
                    Transaction = (OracleTransaction)session.GetTransaction()
                };

                oracleCommand.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool Delete(object id, string table, string idColumn)
        {
            // TODO: Inject WHERE-Request

            try
            {
                string sqlExpression = $"DELETE FROM {table} WHERE {idColumn} = '{id}'";

                session.ExecuteNonQuery(sqlExpression);
            }
            catch (DbException ex)
            {
                logger.Info("Exception.Message: {0}", ex.Message);
                return false;
            }

            return true;
        }
    }
}