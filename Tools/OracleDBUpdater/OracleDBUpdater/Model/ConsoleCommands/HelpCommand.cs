using System;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class HelpCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "help";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            string result = "";
            try
            {
                string manual = ConsoleCommandRegistry.GetManual(args[args.Length - 1]);
                if (manual != null)
                {
                    result += manual;
                }
                else
                {
                    result += "Manual for this command was not found.";
                }

                if (args[args.Length - 1] == "help")
                {
                    foreach (ICommand command in ConsoleCommandRegistry.commands)
                    {
                        result += $" - {command.Name}\n";
                    }
                }
                result += "\n";
            }
            catch (Exception ex)
            {
                result = $"E: {ex.Message}";
            }

            return result;
        }
    }
}
