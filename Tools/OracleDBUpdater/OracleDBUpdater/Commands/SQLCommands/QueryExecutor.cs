using System;
using System.Collections.Generic;

namespace OracleDBUpdater.Commands.SQLCommands
{
    class QueryExecutor
    {
        private List<ICommand> commands = new List<ICommand>();

        private readonly List<string> tableNames = new List<string>();
        private readonly List<string> undoQueries = new List<string>();
        private readonly List<string> createdTableNames = new List<string>();

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void AddTableNames(string tableName)
        {
            tableNames.Add(tableName);
        }

        public void AddUndoQueries(string undoQuery)
        {
            undoQueries.Add(undoQuery);
        }

        public void AddCreatedTableNames(string createdTableName)
        {
            createdTableNames.Add(createdTableName);
        }

        public bool ContainTableName(string tableName)
        {
            return tableNames.Contains(tableName);
        }

        public bool ContainUndoQuery(string undoQuery)
        {
            return undoQueries.Contains(undoQuery);
        }

        public bool ContainCreatedTableName(string createdTableName)
        {
            return createdTableNames.Contains(createdTableName);
        }

        public void Execute()
        {
            foreach(ICommand command in commands)
            {
                command.Execute();
            }

            // If all queries are successfully executed, then drop table with name that start with "_" (their names are stored in the "tableNames" list)
            foreach (string tableName in tableNames)
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"DROP TABLE {"temp_" + tableName}");
            }
        }

        public void Undo()
        {
            // Execute undo queries.
            foreach (string undoQuery in undoQueries)
            {
                if (MyDataBase.GetDB().IsExistringTable(undoQuery.Split(' ')[2].ToUpper()))
                {
                    MyDataBase.GetDB().ExecuteQueryWithoutAnswer(undoQuery);
                }
            }

            // Rename tables with underscore.
            foreach (string tableName in tableNames)
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"ALTER TABLE {"temp_" + tableName} RENAME TO {tableName}");
            }

            // Drop created tables.
            foreach (string tableName in createdTableNames)
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"DROP TABLE {tableName}");
            }
        }
    }
}
