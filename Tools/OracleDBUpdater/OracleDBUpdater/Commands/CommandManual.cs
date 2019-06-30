using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OracleDBUpdater.Commands
{
    static class CommandManual
    {
        /// <summary> Register of commands and manuals for them. </summary>
        private static readonly Dictionary<string, string> manual;

        static CommandManual()
        {
            using (var stream = new MemoryStream(Properties.Resources.manual))
            {
                using (var reader = new StreamReader(stream))
                {
                    manual = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                }
            }
        }

        /// <summary> Checks whether the manual for the given command is contained. </summary>
        /// <returns> Returns true if there is a manual for the command. </returns>
        public static bool ContainCommand(string command)
        {
            return manual.ContainsKey(command);
        }

        /// <summary> Returns the manual for the command. </summary>
        /// <returns> Returns the manual for the command, if any, else returns null. </returns>
        public static string Manual(string command)
        {
            string result = null;
            if (ContainCommand(command))
            {
                result = manual[command];
            }
            return result;
        }
    }
}