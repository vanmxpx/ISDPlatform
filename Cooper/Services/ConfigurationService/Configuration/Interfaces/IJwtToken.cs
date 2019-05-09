﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Configuration
{
     public interface IJwtToken
    {
         string Issuer { get; set; }
         string Audience { get; set; }
         string Key { get; set; }
         int Lifetime { get; set; }
    }
}
