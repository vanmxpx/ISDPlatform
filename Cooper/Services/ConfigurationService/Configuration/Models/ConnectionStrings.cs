using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public class ConnectionStrings : IConnectionStrings
    {
        public string LocalDatabase { get; set; }
    }
}
