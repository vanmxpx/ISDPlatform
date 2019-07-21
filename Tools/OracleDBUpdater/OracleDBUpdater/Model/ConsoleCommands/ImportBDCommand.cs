using System.IO;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class ImportBDCommand : ICommand
    {
        public string Name => "import";

        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            string result = "Unknown error!";
            string path = $"{Configuration.GetVariable("UpdateFolder")}/{args[1]}.sql".Replace("//", "/");
            if (args.Length > 1 && File.Exists(path))
            {
                try
                {
                    MyDataBase.GetDB().ExecuteQueries(File.ReadAllLines(path));
                    result = "OK!";
                }
                catch { result = "E: Can't access file or file is corupted!"; }
            }
            else { result = "Incorrect path!"; }
            return result;
        }
    }
}
