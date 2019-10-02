using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class OracleSession : ISession
    {
        private readonly OracleConnection connection;
        private OracleTransaction transaction;
        private IOracleSessionFactory sessionFactory;

        public OracleSession(IConfigProvider configProvider, IOracleSessionFactory sessionFactory)
        {
            string connectionString = configProvider.ConnectionStrings.LocalDatabase;
            connection = new OracleConnection(connectionString);

            this.sessionFactory = sessionFactory;
        }

        public void StartSession()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            if (transaction == null)
            {
                transaction = connection.BeginTransaction();
            }
        }
        public void EndSession()
        {
            connection.Close();
            sessionFactory.ReturnSession(this);
        }
        public void Commit(bool endSession)
        {
            transaction.Commit();

            if (endSession)
            {
                EndSession();
            }
        }
        public void Rollback(bool endSession)
        {
            transaction.Rollback();

            if (endSession)
            {
                EndSession();
            }
        }
        public IDbTransaction GetTransaction()
        {
            return transaction;
        }
        public IDbConnection GetConnection()
        {
            return connection;
        }

        public object ExecuteNonQuery(string query, bool getId = false)
        {
            object result = null;

            OracleCommand command = new OracleCommand(query, connection)
            {
                Transaction = transaction
            };
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Decimal, ParameterDirection.ReturnValue));
            command.ExecuteNonQuery();

            if (getId)
                result = command.Parameters["id"].Value;

            return result;
        }

    }
}
