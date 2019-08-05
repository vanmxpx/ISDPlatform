using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Verification : Entity
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime EndVerifyDate { get; set; }
    }
}