using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public interface IProvider
    {
        string AppID { get; set; }
        string AppSecretKey { get; set; }
    }
}
