using System.Collections.Generic;
using System.IO;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class ExportBDCommand : ICommand
    {
        public string Name => "export";

        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            string result = "Unknown error!";
            if (args.Length > 1)
            {
                try
                {
                    File.WriteAllLines(
                        $"{Configuration.GetVariable("UpdateFolder")}/{args[1]}.sql".Replace("//", "/"), 
                        MyDataBase.GetDB().ReadQueryAnswer(args[1], args.Length == 2 ? null : args[2].Split(','))
                    );
                    result = "OK!";
                }
                catch
                {
                    result = "E: Can't access file or file is corupted!";
                }
            }
            else { result = "E: Not enough arguments!"; }
            return result;
        }
    }
}
