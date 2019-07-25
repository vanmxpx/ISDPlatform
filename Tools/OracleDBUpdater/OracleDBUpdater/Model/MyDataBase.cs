using Oracle.ManagedDataAccess.Client;
using OracleDBUpdater.Commands.SQLCommands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace OracleDBUpdater
{
    class MyDataBase
    {
        private static MyDataBase _instance;
        private static OracleConnection _connection;
        private static OracleCommand _command;

        private MyDataBase() { }

        /// <summary> Returns a database object. </summary>
        public static MyDataBase GetDB()
        {
            if (_instance == null)
            {
                _instance = new MyDataBase();
            }
            return _instance;
        }

        /// <summary> Open connection to database. </summary>
        private void OpenConnection()
        {
            _connection = new OracleConnection(Configuration.GetVariable("ConnectionString"));
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        /// <summary> Close connection to database. </summary>
        public void CloseConnection()
        {
            _connection.Close();
            _command.Dispose();
        }

        /// <summary> Check connection string for correctness. </summary>
        /// <returns> Returns true if the connection string is valid. </returns>
        public bool TestConnectionString(string connectionString)
        {
            bool isConnectionOpen = false;
            OracleConnection connection = null;

            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();
                isConnectionOpen = true;
            }
            catch
            {
                connection?.Close();
            }

            return isConnectionOpen;
        }

        /// <summary> Execute query without answer. </summary>
        public void ExecuteQueryWithoutAnswer(string query)
        {
            OpenConnection();

            _command.CommandText = query;
            _command.ExecuteNonQuery();

            CloseConnection();
        }

        /// <summary> Execute queries. </summary>
        /// <returns> Returns true if all queries succeeded. </returns>
        public bool ExecuteQueries(string[] queries)
        {
            QueryExecutor queryExecutor = new QueryExecutor();

            try
            {
                foreach (string query in queries)
                {
                    string[] tempArr = query.Split(' ');
                    string sqlOperator = tempArr.Length > 0 ? tempArr[0].ToUpper() : null;

                    if (sqlOperator == "CREATE")
                    {
                        queryExecutor.AddCommand(new CreateCommand(query, queryExecutor));
                    }
                    else if (sqlOperator == "ALTER")
                    {
                        queryExecutor.AddCommand(new AlterCommand(query, queryExecutor));
                    }
                    else if (sqlOperator == "DROP")
                    {
                        queryExecutor.AddCommand(new DropCommand(query, queryExecutor));
                    }
                    else if (sqlOperator == "INSERT")
                    {
                        queryExecutor.AddCommand(new InsertCommand(query, queryExecutor));
                    }
                    else if (sqlOperator == "DELETE")
                    {
                        queryExecutor.AddCommand(new DeleteCommand(query, queryExecutor));
                    }
                    else
                    {
                        throw new Exception($"No execution processing for this operator '{sqlOperator}'.");
                    }
                }

                queryExecutor.Execute();

                return true;
            }
            catch
            {
                queryExecutor.Undo();
                return false;
            }
        }

        /// <summary> Checks if a table exists. </summary>
        /// <returns> Returns true if the table exists. </returns>
        public bool IsExistringTable(string tableName)
        {
            return ExecuteQueryWithAnswer($"SELECT TABLE_NAME FROM ALL_TABLES WHERE TABLE_NAME = '{tableName.ToUpper()}'") != null;
        }

        /// <summary> Execute query with answer. </summary>
        /// <returns> Returns the value of 1 row 1 column. </returns>
        public string ExecuteQueryWithAnswer(string query)
        {
            OpenConnection();

            _command.CommandText = query;
            object answer = _command.ExecuteScalar();

            CloseConnection();

            return answer?.ToString();
        }

        public List<string> ReadQueryAnswer(string table, string[] skip_fields)
        {
            List<string> entities = new List<string>();
            try
            {
                OpenConnection();
                OracleCommand command = new OracleCommand($"SELECT * FROM {table}", _connection);

                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string names = "";
                    string values = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        if (skip_fields == null || !skip_fields.Contains(name))
                        {
                            names += $"{name},";
                            TypeCode typeCode = Type.GetTypeCode(reader.GetFieldType(i));
                            if (typeCode == TypeCode.String)
                            {
                                values += $"\'{reader.GetValue(i)}\',";
                            }
                            else if (typeCode == TypeCode.DateTime)
                            {
                                values += $"TO_TIMESTAMP(\'{Convert.ToDateTime(reader.GetValue(i)).ToString("dd.MM.yyyy HH:mm:ss")}\','DD.MM.YYYY HH24:MI:SS'),";
                            }
                            else
                            {
                                values += $"{reader.GetValue(i)},";
                            }
                        }
                    }
                    //Remove last symobl(',')
                    names = names.Remove(names.Length - 1);
                    values = values.Remove(values.Length - 1);

                    entities.Add($"INSERT INTO {table} ({names}) VALUES ({values})");
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return entities;
        }
    }
}
