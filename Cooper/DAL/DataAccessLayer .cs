using Oracle.ManagedDataAccess.Client;

namespace Cooper.DAL
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
