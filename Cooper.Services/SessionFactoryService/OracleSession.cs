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
            connection.Open();
            transaction = connection.BeginTransaction();
        }
        private void EndSession()
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
        
    }
}
