using System;

namespace Cooper.Models
{
    public class Verification : Entity
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime EndVerifyDate { get; set; }
    }
}