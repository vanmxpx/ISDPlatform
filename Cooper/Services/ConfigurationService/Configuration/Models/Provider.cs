using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public class Provider : IProvider
    {
        public string AppID { get; set; }
        public string AppSecretKey { get; set; }
    }
}
