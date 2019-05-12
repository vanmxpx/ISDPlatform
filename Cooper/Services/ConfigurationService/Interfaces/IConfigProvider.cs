using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public interface IConfigProvider
    {
        IConnectionStrings ConnectionStrings { get; set; }
        IJwtToken JwtToken { get; set; }
    }
}
