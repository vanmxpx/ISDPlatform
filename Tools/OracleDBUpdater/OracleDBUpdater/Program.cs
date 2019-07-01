using ConsoleHelper;
using Oracle.ManagedDataAccess.Client;
using OracleDBUpdater.Commands.ConsoleCommands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OracleDBUpdater
{
    class Program
    {
        public static ConsoleColor ErrorColor { get; } = ConsoleColor.Red;
        public static ConsoleColor TextColor { get; } = ConsoleColor.White;

        private static void Main()
        {            
            CheckConfigurationVariables();

            // IT'S FOR TESTS
            if (!MyDataBase.GetDB().IsExistringTable("db_version"))
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("CREATE TABLE db_versions (verions INT)");
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("INSERT INTO db_versions VALUES (1.2505)");
            }
            else
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("UPDATE db_version SET version = 1.2505");
            }

            ShowMainMenu();
        }

        /// <summary> Check all configuration variables that will be needed the program for correctness. </summary>
        private static void CheckConfigurationVariables()
        {
            CheckConnectionString();
            CheckUpdateFolder();
        }

        /// <summary> Check connection string for correctness and require the user to enter the correct connection string if needed. </summary>
        private static void CheckConnectionString()
        {
            string connectionString = Configuration.GetVariable("ConnectionString");

            while (string.IsNullOrEmpty(connectionString) || !MyDataBase.GetDB().TestConnectionString(connectionString))
            {
                ConsoleUtility.WriteLine("Connection string not set or entered incorrectly.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter connection string:", TextColor);
                connectionString = ConsoleUtility.ReadLine(TextColor);
                Console.WriteLine();
            }

            Configuration.SetVariable("ConnectionString", connectionString);
        }

        /// <summary> Check update folder for correctness and require the user to enter the correct update folder if needed. </summary>
        private static void CheckUpdateFolder()
        {
            string updateFolder = Configuration.GetVariable("UpdateFolder");

            while (string.IsNullOrEmpty(updateFolder) || !Directory.Exists(updateFolder))
            {
                ConsoleUtility.WriteLine("Update folder not set or entered incorrectly.", TextColor);
                ConsoleUtility.WriteLine(">>>Enter the path to the folder in which the scripts for updating the database are stored:", TextColor);
                updateFolder = ConsoleUtility.ReadLine(TextColor);
                Console.WriteLine();
            }

            Configuration.SetVariable("UpdateFolder", updateFolder);
        }

        /// <summary> Show main menu. </summary>
        private static void ShowMainMenu()
        {
            while (true)
            {
                if (VersionHandler.TryGetCurrentDatabaseVersion(out double version))
                {
                    ConsoleUtility.Write(Environment.UserName, ConsoleColor.Yellow);
                    ConsoleUtility.WriteLine("->v" + version, ConsoleColor.Blue);
                    ConsoleUtility.Write("-> ", TextColor);
                    string command = ConsoleUtility.ReadLine(TextColor);

                    string[] args = command.Split(' ');
                    if (ConsoleCommandRegistry.ContainCommand(args[0]))
                    {
                        ConsoleCommandRegistry.ExecuteCommand(args[0], args);
                    }
                    else
                    {
                        ConsoleUtility.WriteLine("Unknown command.", ErrorColor);
                    }

                    Console.WriteLine();
                }
                else
                {
                    ConsoleUtility.WriteLine("Failed to get current database version.", ErrorColor);
                    Console.ReadKey();
                    return;
                }
            }
        }

        /// <summary> Return paths to update scripts. </summary>
        public static string[] GetUpdateScriptNames()
        {
            string[] updateScriptNames;

            try
            {
                updateScriptNames = Directory.GetFiles(Configuration.GetVariable("UpdateFolder"));
            }
            catch(Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to get update script names: {ex.Message}", ErrorColor);
                updateScriptNames = new string[0];
            }

            return updateScriptNames;
        }

        /// <summary> Return queries from string. </summary>
        public static List<string> GetQueriesFromString(string str)
        {
            List<string> queries = new List<string>();

            try
            {
                while (str.Contains(';'))
                {
                    string query = str.Substring(0, str.IndexOf(';'));
                    char[] escapeChars = new[] { '\n', '\a', '\r', '\t', '\f', '\v' };
                    queries.Add(new string(query.Where(c => !escapeChars.Contains(c)).ToArray()));
                    str = str.Remove(0, str.IndexOf(';') + 1);
                }

                if (!str.Contains(';') && !string.IsNullOrEmpty(str))
                {
                    queries.Add(str);
                }
            }
            catch (Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to get queries from string: {ex.Message}", ErrorColor);
            }

            return queries;
        }
    }
}
