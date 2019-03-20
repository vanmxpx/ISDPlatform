using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoURL { get; set; }
        public bool isVerified { get; set; }
        public bool isCreator { get; set; }
        public bool isBanned { get; set; }
        public DateTime EndBanDate { get; set; }
        public string platformLanguage { get; set; }
        public string platformTheme { get; set; }
        public List<Game> GamesList { get; set; }
    }
}
