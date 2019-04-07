using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Controllers.ViewModels
{
    public class UserLogin
    {
        public string Nickname { get; set; }          // can be nickname or email
        public string Password { get; set; }       
    }
}
