using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Services
{
    public interface IJwtHandlerService
    {
        string GetPayloadAttributeValue(string attribute, string token);

    }
}
