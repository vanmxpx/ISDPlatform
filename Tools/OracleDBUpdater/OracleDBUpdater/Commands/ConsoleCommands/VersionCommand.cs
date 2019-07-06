using ConsoleHelper;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class VersionCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "version";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public void Execute(string[] args)
        {
            if (VersionHandler.TryGetCurrentDatabaseVersion(out double version))
            {
                ConsoleUtility.WriteLine($"Current database version: {version}.", Program.TextColor);
            }
            else
            {
                ConsoleUtility.WriteLine("Failed to get current database version.", Program.ErrorColor);
            }
        }
    }
}
