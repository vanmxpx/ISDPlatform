using NLog;
using NLog.Config;
using NLog.Targets;

namespace Cooper.Logging
{
    public static class CooperLogger
    {
        static CooperLogger()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
            var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/file.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            // Step 3. Define rules
            config.AddRuleForAllLevels(fileTarget);
            config.AddRuleForAllLevels(consoleTarget);

            // Step 4. Activate the configuration
            LogManager.Configuration = config;
        }

        public static Logger GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }
    }
}
