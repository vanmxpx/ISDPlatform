﻿using ConsoleHelper;
using System;

namespace OracleDBUpdater.Commands.SQLCommands
{
    class AlterCommand : BaseCommand
    {
        public AlterCommand(string query, QueryExecutor queryExecutor) : base(query, queryExecutor) { }

        public override string Execute()
        {
            string result = "OK!";
            try
            {
                string[] tempArr = Query.Split(' ');
                string tableName = tempArr.Length > 2 ? tempArr[2].ToUpper() : null;
                // Contains true if the table was created in this sql script
                bool isTableCreated = queryExecutor.ContainCreatedTableName(tableName);

                // Make a temporary table if it was not created.
                if (!isTableCreated && !MyDataBase.GetDB().IsExistringTable("temp_" + tableName))
                {
                    MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"CREATE TABLE {"temp_" + tableName} AS SELECT * FROM {tableName}");
                }
                // Alter an existing table
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer(Query);
                // After executing query, add data to the desired lists.
                if (!isTableCreated)
                {
                    if (!queryExecutor.ContainTableName(tableName))
                    {
                        queryExecutor.AddTableNames(tableName);
                    }
                    string undoQuery = $"DROP TABLE {tableName}";
                    if (!queryExecutor.ContainUndoQuery(undoQuery))
                    {
                        queryExecutor.AddUndoQueries(undoQuery);
                    }
                }
            }
            catch(Exception ex)
            {
                result = $"E: {ex.Message}";
            }

            return result;
        }
    }
}
