using System;
using System.Collections.Generic;
using System.Linq;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class SQLCommandsUtils
    {
        /// <summary> Return queries from string. </summary>
        public static List<string> GetQueriesFromString(string str)
        {
            List<string> queries = new List<string>();

            try
            {
                while (str.Contains(';'))
                {
                    string query = str.Substring(0, str.IndexOf(';'));//TODO: REWORKING!!!
                    char[] escapeChars = new[] { '\n', '\a', '\r', '\t', '\f', '\v' };
                    queries.Add(new string(query.Where(c => !escapeChars.Contains(c)).ToArray()));
                    str = str.Remove(0, str.IndexOf(';') + 1);
                }

                if (!str.Contains(';') && !string.IsNullOrEmpty(str))
                {
                    queries.Add(str);
                }
            }
            catch
            {
                queries = null;
            }

            return queries;
        }
    }
}
