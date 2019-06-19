using ConsoleHelper;

namespace OracleDBUpdater.Commands
{
    class HelpCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "help";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => CommandManual.Manual(Name);

        public void Execute(string[] args)
        {
            if (args.Length == 1)
            {
                string manual = CommandRegistry.GetManual(args[0]);
                if (manual != null)
                {
                    ConsoleUtility.WriteLine(manual, Program.TextColor);
                }
                else
                {
                    ConsoleUtility.WriteLine("Manual for this command was not found.", Program.ErrorColor);
                }
            }
            else if (args.Length == 2)
            {
                string manual = CommandRegistry.GetManual(args[1]);
                if (manual != null)
                {
                    ConsoleUtility.WriteLine(manual, Program.TextColor);
                }
                else
                {
                    ConsoleUtility.WriteLine("Manual for this command was not found.", Program.ErrorColor);
                }
            }
            else
            {
                ConsoleUtility.WriteLine("Invalid number of arguments passed.", Program.ErrorColor);
            }
        }
    }
}
