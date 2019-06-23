using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.DAO.Models
{
    public class UserDb : EntityDb
    {
        #region Main attributes

        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoURL { get; set; }
        public bool IsVerified { get; set; }
        public bool IsCreator { get; set; }
        public bool IsBanned { get; set; }
        public DateTime EndBanDate { get; set; }
        public string PlatformLanguage { get; set; }
        public string PlatformTheme { get; set; }

        #endregion

        #region Interop attributes
        public List<long> Subscribers { get; set; }
        public List<long> Subscriptions { get; set; }
        public List<long> Blacklist { get; set; }

        
        public List<long> GamesList { get; set; }
        public List<long> ChatsList { get; set; }
        public List<long> MessagesList { get; set; }
        public List<long> GameStatisticsList { get; set; }
        public List<long> MadeUserReviewsList { get; set; }
        public List<long> GotUserReviewsList { get; set; }
        public List<long> GameReviewsList { get; set; }

        #endregion
    }
}
