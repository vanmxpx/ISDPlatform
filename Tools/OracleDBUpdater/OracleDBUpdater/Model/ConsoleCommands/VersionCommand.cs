namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class VersionCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "version";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            string result;
            if (VersionHandler.TryGetCurrentDatabaseVersion(out VersionHandler.Version version))
            {
                result = $"Current database version: {version.ToString()}.";
            }
            else
            {
                result = $"E: Failed to get current database version.";
            }

            return result;
        }
    }
}
