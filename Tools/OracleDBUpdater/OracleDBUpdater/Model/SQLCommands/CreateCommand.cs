using System;

namespace OracleDBUpdater.Commands.SQLCommands
{
    class CreateCommand : BaseCommand
    {
        public CreateCommand(string query, QueryExecutor queryExecutor) : base(query, queryExecutor) { }

        public override string Execute()
        {
            string result = "OK!";
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
                result = $"E: {ex.Message}";
            }

            return result;
        }
    }
}
