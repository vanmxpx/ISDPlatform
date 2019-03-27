using Oracle.ManagedDataAccess.Client;
using System;

namespace Cooper.ORM
{

    //public class DbConnect// : IDisposable
    //{
    //    private static DbConnect instance;

    //    private static string connectionString = "User Id=system;Password=QAZse4321;Data Source=localhost:1521/xe;";
    //    private static OracleConnection connection;

    //    private DbConnect()
    //    {
    //        connection = new OracleConnection(connectionString);
    //    }

    //    public static DbConnect getInstance()
    //    {
    //        if (instance == null)
    //        {
    //            instance = new DbConnect();
    //        }
    //        return instance;
    //    }

    //    public OracleConnection GetConnection()
    //    {
    //        return connection;
    //    }

    //    public static void CloseConnection()
    //    {
    //        connection.Close();
    //    }
    //    public static void OpenConnection()
    //    {
    //        connection.Open();
    //    }

    //    //public void Dispose()
    //    //{
    //    //    connection.Dispose();
    //    //}
    //}

    public static class DbConnecting
    {
        private static string connectionString = "User Id=system;Password=QAZse4321;Data Source=localhost:1521/xe;";
        //"User Id=system;Password=QAZse4321;Data Source=localhost:1521/xe;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }

    }

}
