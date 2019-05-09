using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using Cooper.Configuration;

namespace Cooper.ORM
{

    public class DbConnect
    {

        private static string connectionString;

        private static DbConnect instance;

        private static OracleConnection connection;

        private readonly IConfigProvider configProvider;

        public DbConnect(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;

            connectionString = configProvider.ConnectionStrings.LocalDatabase;

            connection = new OracleConnection(connectionString);

        }
        
        public OracleConnection GetConnection()
        {
            return connection;
        }

        ///<summary>
        /// Executes query that doesn't need to return anything. Takes responsibility for managing pool of connections.
        ///</summary>
        public object ExecuteNonQuery(string query, bool getId = false)
        {
            object result = null;

            connection.Open();

            OracleCommand command = new OracleCommand(query, connection);
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Decimal, ParameterDirection.ReturnValue));
            command.ExecuteNonQuery();

            if (getId)
                result = command.Parameters["id"].Value;
            
            connection.Close();

            return result;
        }

        public static void CloseConnection()
        {
            connection.Close();
        }
        public static void OpenConnection()
        {
            connection.Open();
        }
    }

}
