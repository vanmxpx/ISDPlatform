using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace Cooper.ORM
{

    public class DbConnect// : IDisposable
    {

        private static string connectionString = $"User Id=SYSTEM;Password={Environment.GetEnvironmentVariable("Coop")};Data Source=localhost:1521/xe;";

        private static DbConnect instance;

        private static OracleConnection connection;

        private DbConnect()
        {
            connection = new OracleConnection(connectionString);
        }

        public static DbConnect GetInstance()
        {
            if (instance == null)
            {
                instance = new DbConnect();
            }
            return instance;
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
