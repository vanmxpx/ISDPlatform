using ConsoleHelper;
using OracleDBUpdater.Commands.ConsoleCommands;
using System;

namespace OracleDBUpdater
{
    class Program
    {
        private static ConsoleColor TextColor = ConsoleColor.White;
        private static ConsoleColor ErrorColor = ConsoleColor.Red;

        private static void Main()
        {
            if (Configuration.ContainVariable("TextColor"))
            {
                try
                {
                    TextColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Configuration.GetVariable("TextColor"));
                }
                catch { }
            }

            if (Configuration.ContainVariable("ErrorColor"))
            {
                try
                {
                    ErrorColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Configuration.GetVariable("ErrorColor"));
                }
                catch { }
            }

            VersionHandler.db_table = Configuration.GetVariable("DB_VERSION_TABLE");
            while (string.IsNullOrEmpty(VersionHandler.db_table))
            {
                ConsoleUtility.WriteLine("\nDB_TABLE incorrect.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter DB_TABLE name:", TextColor);
                VersionHandler.db_table = ConsoleUtility.ReadLine(TextColor);
            }
            Configuration.SetVariable("DB_VERSION_TABLE", VersionHandler.db_table);

            VersionHandler.db_field = Configuration.GetVariable("DB_VERSION_FIELD");
            while (string.IsNullOrEmpty(VersionHandler.db_field))
            {
                ConsoleUtility.WriteLine("\nDB_FIELD incorrect.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter DB_FIELD name:", TextColor);
                VersionHandler.db_field = ConsoleUtility.ReadLine(TextColor);
            }
            Configuration.SetVariable("DB_VERSION_FIELD", VersionHandler.db_field);

            string connectionString = Configuration.GetVariable("ConnectionString");

            while (Configuration.CheckConnectionString(connectionString))
            {
                ConsoleUtility.WriteLine("\nConnection string not set or entered incorrectly.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter connection string:", TextColor);
                connectionString = ConsoleUtility.ReadLine(TextColor);
            }
            Configuration.SetVariable("ConnectionString", connectionString);


            string updateFolder = Configuration.GetVariable("UpdateFolder");

            while (Configuration.CheckUpdateFolder(updateFolder))
            {
                ConsoleUtility.WriteLine("\nUpdate folder not set or entered incorrectly.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter the path to the folder in which the scripts for updating the database are stored:", TextColor);
                updateFolder = ConsoleUtility.ReadLine(TextColor);
                Console.WriteLine();
            }
            Configuration.SetVariable("UpdateFolder", updateFolder);

            ShowMainMenu();

            Console.ReadKey();
        }

        /// <summary> Show main menu. </summary>
        private static void ShowMainMenu()
        {
            if (VersionHandler.TryGetCurrentDatabaseVersion(out VersionHandler.Version version))
            {
                ConsoleUtility.Write(Environment.UserName, ConsoleColor.Yellow);
                ConsoleUtility.WriteLine("->v" + version, ConsoleColor.Blue);
                ConsoleUtility.Write("-> ", TextColor);
                string command = ConsoleUtility.ReadLine(TextColor);
                string result = ConsoleCommandRegistry.TryExecuteCommand(command);

                if (result != null)
                {
                    if (result.Length > 2 && result[0] == 'E' && result[1] == ':')
                    {
                        ConsoleUtility.WriteLine(result, ErrorColor);
                    }
                    else
                    {
                        ConsoleUtility.WriteLine(result, TextColor);
                    }
                }
                else
                {
                    ConsoleUtility.WriteLine("Command not found!", ErrorColor);
                }
                Console.WriteLine();
            }
            else
            {
                ConsoleUtility.WriteLine("Failed to get current database version. Check whether you have created a table that contains the current version of the database.", ErrorColor);
                return;
            }

            ShowMainMenu();
        }
    }
}
