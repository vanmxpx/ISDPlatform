using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

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
            if (_instance == null) _instance = new MyDataBase();
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
            OpenConnection();

            // Names of tables for which temporary tables are created with the same name that starts with an underscore
            List<string> tableNames = new List<string>();
            List<string> undoQueries = new List<string>();
            List<string> createdTableNames = new List<string>();

            try
            {
                foreach (string query in queries)
                {
                    string[] tempArr = query.Split(' ');
                    string sqlOperator = tempArr.Length > 0 ? tempArr[0].ToUpper() : null;
                    string tableName = tempArr.Length >= 2 ? tempArr[2].ToUpper() : null;
                    // Contains true if the table was created in this sql script
                    bool isTableCreated = createdTableNames.Contains(tableName);

                    if (sqlOperator == "CREATE")
                    {
                        // Create table
                        ExecuteQueryWithoutAnswer(query);
                        // After executing query, add data to the desired lists.
                        createdTableNames.Add(tableName);
                    }
                    else if (sqlOperator == "ALTER")
                    {
                        // Make a temporary table if it was not created.
                        if (!isTableCreated && !IsExistringTable("temp_" + tableName))
                        {
                            ExecuteQueryWithoutAnswer($"CREATE TABLE {"temp_" + tableName} AS SELECT * FROM {tableName}");
                        }
                        // Alter an existing table
                        ExecuteQueryWithoutAnswer(query);
                        // After executing query, add data to the desired lists.
                        if (!isTableCreated)
                        {
                            if (!tableNames.Contains(tableName)) tableNames.Add(tableName);
                            string undoQuery = $"DROP TABLE {tableName}";
                            if (!undoQueries.Contains(undoQuery)) undoQueries.Add(undoQuery);
                        }
                    }
                    else if (sqlOperator == "DROP")
                    {
                        // Make a temporary table if it was not created.
                        if (!isTableCreated && !IsExistringTable("temp_" + tableName))
                        {
                            ExecuteQueryWithoutAnswer($"CREATE TABLE {"temp_" + tableName} AS SELECT * FROM {tableName}");
                        }
                        // Drop an existing table
                        ExecuteQueryWithoutAnswer(query);
                        // After executing query, add data to the desired lists.
                        if (!isTableCreated)
                        {
                            if (!tableNames.Contains(tableName)) tableNames.Add(tableName);
                        }
                    }
                    else if (sqlOperator == "INSERT")
                    {
                        // Make a temporary table if it was not created.
                        if (!isTableCreated && !IsExistringTable("temp_" + tableName))
                        {
                            ExecuteQueryWithoutAnswer($"CREATE TABLE {"temp_" + tableName} AS SELECT * FROM {tableName}");
                        }
                        // Insert data to existing table
                        ExecuteQueryWithoutAnswer(query);
                        // After executing query, add data to the desired lists.
                        if (!isTableCreated)
                        {
                            if (!tableNames.Contains(tableName)) tableNames.Add(tableName);
                            string undoQuery = $"DROP TABLE {tableName}";
                            if (!undoQueries.Contains(undoQuery)) undoQueries.Add(undoQuery);
                        }
                    }
                    else if (sqlOperator == "DELETE")
                    {
                        // Make a temporary table if it was not created.
                        if (!isTableCreated && !IsExistringTable("temp_" + tableName))
                        {
                            ExecuteQueryWithoutAnswer($"CREATE TABLE {"temp_" + tableName} AS SELECT * FROM {tableName}");
                        }
                        // Delete data from existing table
                        ExecuteQueryWithoutAnswer(query);
                        // After executing query, add data to the desired lists.
                        if (!isTableCreated)
                        {
                            if (!tableNames.Contains(tableName)) tableNames.Add(tableName);
                            string undoQuery = $"DROP TABLE {tableName}";
                            if (!undoQueries.Contains(undoQuery)) undoQueries.Add(undoQuery);
                        }
                    }
                    else
                    {
                        throw new Exception($"No execution processing for this operator '{sqlOperator}'.");
                    }
                }

                // If all queries are successfully executed, then drop table with name that start with "_" (their names are stored in the "tableNames" list)
                foreach (string tableName in tableNames)
                {
                    ExecuteQueryWithoutAnswer($"DROP TABLE {"temp_" + tableName}");
                }

                return true;
            }
            catch
            {
                // Execute undo queries.
                foreach (string undoQuery in undoQueries)
                {
                    if (IsExistringTable(undoQuery.Split(' ')[2].ToUpper()))
                    {
                        ExecuteQueryWithoutAnswer(undoQuery);
                    }
                }

                // Rename tables with underscore.
                foreach (string tableName in tableNames)
                {
                    ExecuteQueryWithoutAnswer($"ALTER TABLE {"temp_" + tableName} RENAME TO {tableName}");
                }

                // Drop created tables.
                foreach (string tableName in createdTableNames)
                {
                    ExecuteQueryWithoutAnswer($"DROP TABLE {tableName}");
                }

                return false;
            }
            finally
            {
                CloseConnection();
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
    }
}
