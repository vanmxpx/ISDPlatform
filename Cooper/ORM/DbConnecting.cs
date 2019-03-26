using Oracle.ManagedDataAccess.Client;

namespace Cooper.ORM
{

    public class DbConnecting
    {
        private static DbConnecting instance;

        private static string connectionString = "User Id=system;Password=QAZse4321;Data Source=localhost:1521/xe;";
        private static OracleConnection connection;

        private DbConnecting()
        {
            connection = new OracleConnection(connectionString);
        }

        public static DbConnecting getInstance()
        {
            if (instance == null)
            {
                instance = new DbConnecting();
            }
            return instance;
        }

        public static OracleConnection GetConnection()
        {
            return connection;
        }

        public void CloseConnection()
        {
            connection.Close();
        }
        public static void OpenConnection()
        {
            connection.Open();
        }

    }

}
