using System;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class VersionsCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "versions";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            
            string result = "All database versions:";
            try
            {
                foreach (VersionHandler.Version version in VersionHandler.GetDatabaseVersions())
                {
                    result += $"\n{version.ToString()}";
                }
            }
            catch (Exception ex)
            {
                result = $"E: {ex.Message}";
            }

            return result;
        }
    }
}
