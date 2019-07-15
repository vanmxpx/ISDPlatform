namespace OracleDBUpdater.Commands.ConsoleCommands
{
    interface ICommand
    {
        string Name { get; }
        string Manual { get; }
        string Execute(string[] args);
    }
}