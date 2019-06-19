using System;

namespace OracleDBUpdater.Exceptions
{
    class ConfigurationException : Exception
    {
        public ConfigurationException(string msg) : base(msg) { }
    }
}
