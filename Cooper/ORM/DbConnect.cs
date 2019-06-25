using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using Cooper.Configuration;

namespace Cooper.ORM
{

    public class DbConnect
    {

        private string connectionString;

        private OracleConnection connection;

        private readonly IConfigProvider configProvider;

        public DbConnect(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;

            connectionString = configProvider.ConnectionStrings.LocalDatabase;

            connection = new OracleConnection(connectionString);

        }

        public object ExecuteNonQuery(string query, bool getId = false)
        {
            object result = null;

            OracleCommand command = new OracleCommand(query, connection);
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Decimal, ParameterDirection.ReturnValue));
            command.ExecuteNonQuery();

            if (getId)
                result = command.Parameters["id"].Value;
            
            return result;
        }

        public OracleConnection GetConnection()
        {
            return connection;
        }

        public void CloseConnection()
        {
            connection.Close();
        }
        public void OpenConnection()
        {
            connection.Open();
        }
    }

}
