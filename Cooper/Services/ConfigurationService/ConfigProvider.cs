﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public class ConfigProvider : IConfigProvider
    {
        public IConnectionStrings ConnectionStrings { get; set; }
        public IJwtToken JwtToken { get; set; } 
    }
}