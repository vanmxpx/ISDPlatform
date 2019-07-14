namespace OracleDBUpdater.Commands.SQLCommands
{
    interface ICommand
    {
        string Query { get; }
        string Execute();
    }
}
