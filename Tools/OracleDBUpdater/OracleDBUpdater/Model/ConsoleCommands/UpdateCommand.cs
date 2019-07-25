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

        public string Execute(string[] args)
        {
            string result;
            if (args.Length == 2)
            {
                if (VersionHandler.IsThereVersion(args[1]))
                {
                    result = UpdateDatabase(args[1]);
                }
                else
                {
                    result = $"E: Version {args[1]} is not found.";
                }
            }
            else
            {
                result = "E: Invalid number of arguments passed for updating database.";
            }

            return result;
        }

        /// <summary> Updates the database to the required version. </summary>
        private string UpdateDatabase(string strVersion)
        {
            string result = "";
            VersionHandler.TryParseVersion(strVersion, out VersionHandler.Version requiredVersion);
            if (VersionHandler.TryGetCurrentDatabaseVersion(out VersionHandler.Version currentVersion))
            {
                if (currentVersion < requiredVersion)
                {
                    if (VersionHandler.IsThereVersion(requiredVersion))
                    {
                        result += "Need to run scripts:\n";

                        string[] fileNames = VersionHandler.GetFilesByPattern();
                        List<string> updateScripts = new List<string>();

                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            if (VersionHandler.TryGetVersionFromPath(fileNames[i], out VersionHandler.Version version))
                            {
                                if (currentVersion < version && version <= requiredVersion)
                                {
                                    updateScripts.Add(fileNames[i]);
                                    result += $"{Path.GetFileName(fileNames[i])}\n";
                                }
                            }
                        }

                        result += "\nScript execution begins...\n";

                        for (int i = 0; i < updateScripts.Count; i++)
                        {
                            result += $"\nExecuting {Path.GetFileName(updateScripts[i])}...\n";

                            string file = File.ReadAllText(updateScripts[i]);
                            // I did not manage to run the * .sql file due to the fact that the ExecuteNonQuery() method can perform only 1 query.
                            // Now I have solved this problem by splitting the file into separate requests and sequentially performing them.
                            List<string> queries = SQLCommandsUtils.GetQueriesFromString(file);

                            if (MyDataBase.GetDB().ExecuteQueries(queries.ToArray()))
                            {
                                if (VersionHandler.TryGetVersionFromPath(Path.GetFileName(updateScripts[i]), out VersionHandler.Version version))
                                {
                                    VersionHandler.TrySetCurrentDatabaseVersion(version);
                                    result += $"Script {Path.GetFileName(updateScripts[i])} executed successfully.";
                                }
                            }
                            else
                            {
                                result = $"E: An error occurred while executing the script {Path.GetFileName(updateScripts[i])}.";
                            }
                        }
                    }
                    else
                    {
                        result = $"E: Version {requiredVersion} does not exist.";
                    }
                }
                else if (currentVersion > requiredVersion)
                {
                    result = "E: You cannot update the database to a version that is smaller than the version of the current database. Use rollback command.";
                }
                else
                {
                    result = "E: Your database already has this version.";
                }
            }
            else
            {
                result = "E: Failed to get current database version.";
            }

            return result;
        }
    }
}
