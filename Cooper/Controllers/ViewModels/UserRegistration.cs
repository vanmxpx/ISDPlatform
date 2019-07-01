﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Controllers.ViewModels
{
    public class UserRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string PhotoURL { get; set; }
        public string Provider { get; set; }
    }
}
