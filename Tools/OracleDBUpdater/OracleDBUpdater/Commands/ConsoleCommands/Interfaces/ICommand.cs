namespace OracleDBUpdater.Commands.ConsoleCommands
{
    interface ICommand
    {
        string Name { get; }
        string Manual { get; }
        void Execute(string[] args);
    }
}