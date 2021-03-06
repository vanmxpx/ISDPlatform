﻿using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class ConfigProvider : IConfigProvider
    {
        public IConnectionStrings ConnectionStrings { get; set; }
        public IProvider FacebookProvider { get; set; }
        public IJwtToken JwtToken { get; set; }
        public ISmtp GmailProvider { get; set; }
    }
}
