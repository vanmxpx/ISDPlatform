using System.Collections.Generic;
using System.Linq;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    static class ConsoleCommandRegistry
    {
        /// <summary> Registry of all commands that can be called. </summary>
        private static readonly List<ICommand> commands = new List<ICommand>
        {
            { new UpdateCommand() },
            { new VersionCommand() },
            { new VersionsCommand() },
            { new HelpCommand() },
            { new ExitCommand() }
        };

        /// <summary> Checks if a command contains in a command registry. </summary>
        /// <returns> Returns true if the command is contained in the registry. </returns>
        public static bool ContainCommand(string command)
        {
            return commands.Select(c => c.Name).Contains(command);
        }

        /// <summary> Executes the command. </summary>
        /// <param name="command"> The name of the command to be executed. </param>
        /// <param name="args"> Arguments that will be passed to the command for its execution. </param>
        public static void ExecuteCommand(string command, string[] args)
        {
            commands.SingleOrDefault(c => c.Name == command)?.Execute(args);
        }

        /// <summary> Returns the manual for the command. </summary>
        /// <returns> Returns the manual for the command, if any, else returns null. </returns
        public static string GetManual(string command)
        {
            return commands.SingleOrDefault(c => c.Name == command)?.Manual;
        }
    }
}