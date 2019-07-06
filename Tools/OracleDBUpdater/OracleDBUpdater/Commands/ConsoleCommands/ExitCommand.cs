using System;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class ExitCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "exit";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public void Execute(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
