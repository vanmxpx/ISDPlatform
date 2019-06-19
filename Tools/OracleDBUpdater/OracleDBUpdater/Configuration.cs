using OracleDBUpdater.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OracleDBUpdater
{
    /// <summary> Class for convenient work with variables that need to be saved. </summary>
    public static class Configuration
    {
        /// <summary> Config file name. </summary>
        public static readonly string fileName = "configure.txt";

        /// <summary> Return variable value with name variableName. </summary>
        /// <exception cref="ConfigurationException"> If the variableName has a space. </exception>
        public static string GetVariable(string variableName)
        {
            if (variableName.Contains(' ')) throw new ConfigurationException("Variable can not have a space in the name.");

            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    if (line.StartsWith($"{variableName} "))
                    {
                        return line.Substring(line.IndexOf(' ') + 1);
                    }
                }
            }

            return null;
        }

        /// <summary> Set value for variable with name variableName</summary>
        /// <exception cref="ConfigurationException"> If the variableName has a space. </exception>
        public static void SetVariable(string variableName, string value)
        {
            if (variableName.Contains(' ')) throw new ConfigurationException("Variable can not have a space in the name.");

            if (!File.Exists(fileName)) File.Create(fileName).Close();

            List<string> lines = File.ReadAllLines(fileName).ToList();
            bool isModified = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith($"{variableName}:"))
                {
                    lines[i] = $"{variableName} {value}";
                    isModified = true;
                    break;
                }
            }

            if (isModified) File.WriteAllLines(fileName, lines);
            else
            {
                lines.Add($"{variableName} {value}");
                File.WriteAllLines(fileName, lines);
            }
        }
    }
}
