namespace OracleDBUpdater.Commands
{
    interface ICommand : IExecutable
    {
        string Name { get; }
        string Manual { get; }
    }
}