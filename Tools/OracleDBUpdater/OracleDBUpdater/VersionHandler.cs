using ConsoleHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OracleDBUpdater
{
    /// <summary> Class for convenient work with database versions. </summary>
    public static class VersionHandler
    {
        /// <summary> Return version from file name. </summary>
        public static bool TryGetVersionFromPath(string path, out double version)
        {
            bool isGetted = false;
            version = -1;

            try
            {
                path = path.Substring(path.LastIndexOf("_v") + 2);
                path = path.Remove(path.LastIndexOf('.')).Replace('.', ',');
                version = double.Parse(path);
                isGetted = true;
            }
            catch { }

            return isGetted;
        }

        /// <summary> Return last requiredVersion of database. </summary>
        public static string GetVersionLastUpdateScript()
        {
            string lastUpdateScript;

            try
            {
                string[] fileNames = Directory.GetFiles(Configuration.GetVariable("UpdateFolder"));
                string file = fileNames[fileNames.Length - 1].Substring(fileNames[fileNames.Length - 1].LastIndexOf("_v") + 2);
                lastUpdateScript = file.Remove(file.LastIndexOf('.'));
            }
            catch (Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to get version last update script: {ex.Message}", Program.ErrorColor);
                lastUpdateScript = "";
            }

            return lastUpdateScript;
        }

        /// <summary> Return current version of database. </summary>
        public static bool TryGetCurrentDatabaseVersion(out double version)
        {
            bool isGetted = false;
            version = -1;

            try
            {
                version = double.Parse(MyDataBase.GetDB().ExecuteQueryWithAnswer("SELECT version FROM db_version"));
                isGetted = true;
            }
            catch { }

            return isGetted;
        }

        /// <summary> Checks if this version is exist. </summary>
        public static bool IsThereVersion(double requiredVersion)
        {
            return GetDatabaseVersions().Contains(requiredVersion);
        }

        /// <summary> Return all versions of database. </summary>
        public static double[] GetDatabaseVersions()
        {
            string[] updateScriptNames = Program.GetUpdateScriptNames();
            List<double> versions = new List<double>();

            foreach (string updateScriptName in updateScriptNames)
            {
                if (TryGetVersionFromPath(updateScriptName, out double version))
                {
                    versions.Add(version);
                }
            }

            return versions.ToArray();
        }
    }
}