using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Cooper.Configuration
{
    interface IConfigLoader
    {
        IConfigProvider GetConfigProvider(IConfiguration configuration);
    }
}
