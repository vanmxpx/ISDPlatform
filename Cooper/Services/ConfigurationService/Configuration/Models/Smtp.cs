using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
    public class Smtp : ISmtp
    {
        public string From { get; set; }
        public string Password { get; set; }
    }
}
