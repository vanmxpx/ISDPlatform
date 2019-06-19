using ConsoleHelper;
using Oracle.ManagedDataAccess.Client;
using OracleDBUpdater.Commands;
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
            // FOR TEST
            if (!MyDataBase.GetDB().IsExistringTable("db_version"))
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("CREATE TABLE db_versions (verions INT)");
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("INSERT INTO db_versions VALUES (1.2505)");
            }
            else
            {
                MyDataBase.GetDB().ExecuteQueryWithoutAnswer("UPDATE db_version SET version = 1.2505");
            }

            if (string.IsNullOrEmpty(Configuration.GetVariable("ConnectionString")))
            {
                ConsoleUtility.WriteLine(">>>Enter connection string:", TextColor);
                Configuration.SetVariable("ConnectionString", ConsoleUtility.ReadLine(TextColor));
                ConsoleUtility.WriteLine($"You have entered the connection string, you can change it at any time in the file {Configuration.fileName}.", TextColor);
            }

            if (string.IsNullOrEmpty(Configuration.GetVariable("UpdateFolder")))
            {
                ConsoleUtility.WriteLine(">>>Enter the path to the folder in which the scripts for updating the database are stored:", TextColor);
                Configuration.SetVariable("UpdateFolder", ConsoleUtility.ReadLine(TextColor));
                ConsoleUtility.WriteLine($"You have entered the path, you can change it at any time in the file {Configuration.fileName}.", TextColor);
            }

            while (true)
            {
                try
                {
                    if (VersionHandler.TryGetCurrentDatabaseVersion(out double version))
                    {
                        ConsoleUtility.Write(Environment.UserName, ConsoleColor.Yellow);
                        ConsoleUtility.WriteLine("->v" + version, ConsoleColor.Blue);
                        ConsoleUtility.Write("-> ", TextColor);
                        string command = ConsoleUtility.ReadLine(TextColor);

                        string[] args = command.Split(' ');
                        if (CommandRegistry.ContainCommand(args[0])) CommandRegistry.ExecuteCommand(args[0], args);
                        else ConsoleUtility.WriteLine("Unknown command.", ErrorColor);

                        Console.WriteLine();
                    }
                    else
                    {
                        ConsoleUtility.WriteLine("Failed to get current database version.", Program.ErrorColor);
                    }
                }
                catch (InvalidOperationException)
                {
                    ConsoleUtility.WriteLine("ConnectionString is invalid.\n", ErrorColor);
                }
                catch (DirectoryNotFoundException)
                {
                    ConsoleUtility.WriteLine("UpdateFolder or Rollback is invalid.\n", ErrorColor);
                }
                catch (OracleException)
                {
                    ConsoleUtility.WriteLine("There was an attempt to execute an invalid script.\n", ErrorColor);
                }
            }
        }

        /// <summary> Return paths to update scripts. </summary>
        public static string[] GetUpdateScriptNames()
        {
            return Directory.GetFiles(Configuration.GetVariable("UpdateFolder"));
        }

        /// <summary> Return queries from string. </summary>
        public static List<string> GetQueriesFromString(string str)
        {
            List<string> queries = new List<string>();
            while (str.Contains(';'))
            {
                string query = str.Substring(0, str.IndexOf(';'));
                char[] escapeChars = new[] { '\n', '\a', '\r', '\t', '\f', '\v' };
                queries.Add(new string(query.Where(c => !escapeChars.Contains(c)).ToArray()));
                str = str.Remove(0, str.IndexOf(';') + 1);
            }

            if (!str.Contains(';') && !string.IsNullOrEmpty(str)) queries.Add(str);

            return queries;
        }
    }
}
