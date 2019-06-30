﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Cooper.Configuration
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
