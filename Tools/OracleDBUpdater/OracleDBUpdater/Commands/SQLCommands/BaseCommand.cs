namespace OracleDBUpdater.Commands.SQLCommands
{
    abstract class BaseCommand : ICommand
    {
        public string Query { get; }
        protected QueryExecutor queryExecutor;

        public BaseCommand(string query, QueryExecutor queryExecutor)
        {
            Query = query;
            this.queryExecutor = queryExecutor;
        }

        public abstract void Execute();
    }
}
