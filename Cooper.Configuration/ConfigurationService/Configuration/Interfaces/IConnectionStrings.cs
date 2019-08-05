using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public interface IConnectionStrings
    {
        string LocalDatabase { get; set; }
    }
}
