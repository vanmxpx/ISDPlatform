using System;

namespace Cooper.DAO.Models
{
    public class ResetTokenDb : EntityDb
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}