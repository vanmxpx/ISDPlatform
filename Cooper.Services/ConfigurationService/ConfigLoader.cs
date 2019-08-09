using Microsoft.Extensions.Configuration;
using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class ConfigLoader : IConfigLoader
    {
        public IConfigProvider GetConfigProvider(IConfiguration configuration)
        {
            IConfigProvider configProvider = new ConfigProvider()
            {
                ConnectionStrings = GetConfiguration<ConnectionStrings>(configuration, "ConnectionStrings"),
                FacebookProvider = GetConfiguration<Provider>(configuration, "FacebookProvider"),
                GmailProvider = GetConfiguration<Smtp>(configuration, "GmailProvider"),
                JwtToken = GetConfiguration<JwtToken>(configuration, "JwtToken")
            };

            return configProvider;
        }

        private T GetConfiguration<T>(IConfiguration configuration, string path)
        {
            return configuration.GetSection(path).Get<T>();    
        }
    }
}
