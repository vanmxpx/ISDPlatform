using System.Collections.Generic;
using System.Linq;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    static class ConsoleCommandRegistry
    {
        /// <summary> Registry of all commands that can be called. </summary>
        public static readonly List<ICommand> commands = new List<ICommand>
        {
            { new UpdateCommand() },
            { new VersionCommand() },
            { new VersionsCommand() },
            { new GitHubCommand() },
            { new HelpCommand() },
            { new ExitCommand() }
        };

        /// <summary> Executes the command. </summary>
        /// <param name="command"> Command that will be execute. </param>
        public static string TryExecuteCommand(string command)
        {
            string[] args = command.Split(' ');
            ICommand getCommand = commands.SingleOrDefault(c => c.Name == args[0]);

            return getCommand?.Execute(args);
        }

        /// <summary> Returns the manual for the command. </summary>
        /// <returns> Returns the manual for the command, if any, else returns null. </returns
        public static string GetManual(string command)
        {
            return commands.SingleOrDefault(c => c.Name == command)?.Manual;
        }
    }
}