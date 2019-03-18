using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Cooper.DAO
{

    public static class DataAccessLayer
    {
        private static string connectionString = "User Id=system;Password=QAZse4321;Data Source=localhost:1521/xe;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }

    }

}
