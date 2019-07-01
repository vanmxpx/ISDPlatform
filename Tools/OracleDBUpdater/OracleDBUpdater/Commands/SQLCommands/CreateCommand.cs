using ConsoleHelper;
using System;

namespace OracleDBUpdater.Commands.SQLCommands
{
    class CreateCommand : ICommand
    {
        public string Query { get; }
        private QueryExecutor queryExecutor;

        public CreateCommand(string query, QueryExecutor queryExecutor)
        {
            Query = query;
            this.queryExecutor = queryExecutor;
        }

        public void Execute()
        {
            try
            {
                string[] tempArr = Query.Split(' ');
                string tableName = tempArr.Length > 2 ? tempArr[2].ToUpper() : null;

                // Create table
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer(Query);
                // After executing query, add data to the desired lists.
                queryExecutor.AddCreatedTableNames(tableName);
            }
            catch (Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to execute create command ({Query}): {ex.Message}.", Program.ErrorColor);
            }
        }
    }
}
