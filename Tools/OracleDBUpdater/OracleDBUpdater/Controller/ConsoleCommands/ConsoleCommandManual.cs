using Newtonsoft.Json;
using System.Collections.Generic;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    static class ConsoleCommandManual
    {
        /// <summary> Register of commands and manuals for them. </summary>
        private static readonly Dictionary<string, string> manual;

        static ConsoleCommandManual()
        {
            try
            {
                manual = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Resources.manual);
            }
            catch
            {
                // Initialize the variable with an empty collection in order to avoid NullReferenceException.
                manual = new Dictionary<string, string>();
            }
        }

        /// <summary> Returns the manual for the command. </summary>
        /// <returns> Returns the manual for the command, if any, else returns null. </returns>
        public static string Manual(string command)
        {
            string result = null;
            if (manual.ContainsKey(command))
            {
                result = manual[command];
            }
            return result;
        }
    }
}