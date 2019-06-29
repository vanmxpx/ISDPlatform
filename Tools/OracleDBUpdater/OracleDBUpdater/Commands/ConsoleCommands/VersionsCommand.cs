using ConsoleHelper;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class VersionsCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "versions";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public void Execute(string[] args)
        {
            ConsoleUtility.WriteLine($"All database versions:", Program.TextColor);
            double[] versions = VersionHandler.GetDatabaseVersions();
            foreach (double version in versions)
            {
                ConsoleUtility.WriteLine(version.ToString(), Program.TextColor);
            }
        }
    }
}
