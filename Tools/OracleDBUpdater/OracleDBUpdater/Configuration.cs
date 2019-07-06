using ConsoleHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OracleDBUpdater
{
    /// <summary> Class for convenient work with variables that need to be saved. </summary>
    public static class Configuration
    {
        /// <summary> Dictionary of all configuration variables. </summary>
        private static Dictionary<string, string> configurationVariables;

        /// <summary> Config file name. </summary>
        public static readonly string fileName = "config.json";

        static Configuration()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        configurationVariables = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUtility.WriteLine($"Failed to get configuration: {ex.Message}.", Program.ErrorColor);                
            }

            if(configurationVariables == null) configurationVariables = new Dictionary<string, string>();
        }

        /// <summary> Return variable value with name variableName. </summary>
        public static string GetVariable(string variableName)
        {
            string result = null;
            if (ContainVariable(variableName))
            {
                result = configurationVariables[variableName]; ;
            }
            return result;
        }

        /// <summary> Set value for variable with name variableName</summary>
        public static void SetVariable(string variableName, string value)
        {
            configurationVariables[variableName] = value;

            using (var writer = new StreamWriter(fileName))
            {
                string json = JsonConvert.SerializeObject(configurationVariables);
                writer.WriteLine(json);
            }
        }

        /// <summary> Checks whether the configuration variables for the given variable is contained. </summary>
        /// <returns> Returns true if variables is exists. </returns>
        public static bool ContainVariable(string variableName)
        {
            return configurationVariables.ContainsKey(variableName);
        }
    }
}
