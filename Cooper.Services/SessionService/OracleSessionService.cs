using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class OracleSessionService : ISessionService
    {
        readonly OracleConnection connection;
        OracleTransaction transaction;

        public OracleSessionService(IConfigProvider configProvider)
        {
            string connectionString = configProvider.ConnectionStrings.LocalDatabase;
            connection = new OracleConnection(connectionString);
        }

        public void StartSession()
        {
            connection.Open();
            transaction = connection.BeginTransaction();
        }
        public void EndSession()
        {
            connection.Close();
        }
        public void Commit()
        {
            transaction.Commit();
        }
        public void Rollback()
        {
            transaction.Rollback();
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
