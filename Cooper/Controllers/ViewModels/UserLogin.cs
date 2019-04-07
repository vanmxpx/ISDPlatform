using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Controllers.ViewModels
{
    public class UserLogin
    {
        public string Username { get; set; }          // can be nickname or email (later)
        public string Password { get; set; }
    }
}
