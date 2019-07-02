using ConsoleHelper;
using System;

namespace OracleDBUpdater.Commands.SQLCommands
{
    class DropCommand : BaseCommand
    {
        public DropCommand(string query, QueryExecutor queryExecutor) : base(query, queryExecutor) { }

        public override void Execute()
        {
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
                // Drop an existing table
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer(Query);
                // After executing query, add data to the desired lists.
                if (!isTableCreated)
                {
                    if (!queryExecutor.ContainTableName(tableName)) queryExecutor.AddTableNames(tableName);
                }
            }
            catch (Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to execute drop command ({Query}): {ex.Message}.", Program.ErrorColor);
            }
        }
    }
}
