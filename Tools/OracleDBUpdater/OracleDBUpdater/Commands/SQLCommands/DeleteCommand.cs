namespace OracleDBUpdater.Commands.SQLCommands
{
    class DeleteCommand : ICommand
    {
        public string Query { get; }
        private QueryExecutor queryExecutor;

        public DeleteCommand(string query, QueryExecutor queryExecutor)
        {
            Query = query;
            this.queryExecutor = queryExecutor;
        }

        public void Execute()
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
            // Delete data from existing table
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
    }
}
