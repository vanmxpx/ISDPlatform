using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Controllers.ViewModels
{
    public class Login
    {
        public string Username { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
    }
}
