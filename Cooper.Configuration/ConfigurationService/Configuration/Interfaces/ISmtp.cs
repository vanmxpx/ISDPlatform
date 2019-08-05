using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public interface ISmtp
    {
        string From { get; set; }
        string Password { get; set; }
    }
}