using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.DAO.Models
{
    public class VerificationDb : EntityDb
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime EndVerifyDate { get; set; }
    }
}