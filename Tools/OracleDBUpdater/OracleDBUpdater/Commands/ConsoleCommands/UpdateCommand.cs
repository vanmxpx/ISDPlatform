using ConsoleHelper;
using System.Collections.Generic;
using System.IO;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class UpdateCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "update";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public void Execute(string[] args)
        {
            if (args.Length == 2)
            {
                if (double.TryParse(args[1], out double requiredVersion))
                {
                    if (VersionHandler.IsThereVersion(requiredVersion))
                    {
                        UpdateDatabase(requiredVersion);
                    }
                    else
                    {
                        ConsoleUtility.WriteLine($"Version {requiredVersion} is not found.", Program.ErrorColor);
                    }
                }
                else
                {
                    ConsoleUtility.WriteLine("Invalid database version.", Program.ErrorColor);
                }
            }
            else
            {
                ConsoleUtility.WriteLine("Invalid number of arguments passed for updating database.", Program.ErrorColor);
            }
        }

        /// <summary> Updates the database to the required version. </summary>
        private void UpdateDatabase(double requiredVersion)
        {
            if (VersionHandler.TryGetCurrentDatabaseVersion(out double currentVersion))
            {
                if (currentVersion < requiredVersion)
                {
                    if (VersionHandler.IsThereVersion(requiredVersion))
                    {
                        ConsoleUtility.WriteLine("Need to run scripts:", Program.TextColor);

                        string[] fileNames = Program.GetUpdateScriptNames();
                        List<string> updateScripts = new List<string>();

                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            if (VersionHandler.TryGetVersionFromPath(fileNames[i], out double version))
                            {
                                if (currentVersion < version && version <= requiredVersion)
                                {
                                    updateScripts.Add(fileNames[i]);
                                    ConsoleUtility.WriteLine(Path.GetFileName(fileNames[i]), Program.TextColor);
                                }
                            }
                        }

                        ConsoleUtility.WriteLine("\nScript execution begins...", Program.TextColor);

                        for (int i = 0; i < updateScripts.Count; i++)
                        {
                            ConsoleUtility.WriteLine($"\nExecuting {Path.GetFileName(updateScripts[i])}...", Program.TextColor);

                            string file = File.ReadAllText(updateScripts[i]);
                            // I did not manage to run the * .sql file due to the fact that the ExecuteNonQuery() method can perform only 1 query.
                            // Now I have solved this problem by splitting the file into separate requests and sequentially performing them.
                            // Probably, it's a temporary solution.
                            List<string> queries = Program.GetQueriesFromString(file);

                            if (MyDataBase.GetDB().ExecuteQueries(queries.ToArray()))
                            {
                                if (VersionHandler.TryGetVersionFromPath(Path.GetFileName(updateScripts[i]), out double version))
                                {
                                    MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"UPDATE db_version SET version = {version}");
                                    ConsoleUtility.WriteLine($"Script {Path.GetFileName(updateScripts[i])} executed successfully.", Program.TextColor);
                                }
                            }
                            else
                            {
                                ConsoleUtility.WriteLine($"An error occurred while executing the script {Path.GetFileName(updateScripts[i])}.", Program.ErrorColor);
                            }
                        }
                    }
                    else
                    {
                        ConsoleUtility.WriteLine($"Version {requiredVersion} does not exist.", Program.ErrorColor);
                    }
                }
                else if (currentVersion > requiredVersion)
                {
                    ConsoleUtility.WriteLine("You cannot update the database to a version that is smaller than the version of the current database. Use rollback command.", Program.ErrorColor);
                }
                else
                {
                    ConsoleUtility.WriteLine("Your database already has this version.", Program.ErrorColor);
                }
            }
            else
            {
                ConsoleUtility.WriteLine("Failed to get current database version.", Program.ErrorColor);
            }
        }
    }
}
