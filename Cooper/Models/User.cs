using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class User : Entity
    {
        #region Main attributes
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PhotoURL { get; set; }
        public bool IsVerified { get; set; }
        public bool IsCreator { get; set; }
        public bool IsBanned { get; set; }
        public DateTime EndBanDate { get; set; }
        public string PlatformLanguage { get; set; }
        public string PlatformTheme { get; set; }
        #endregion 

        #region Interop attributes

        public List<UserConnection> ConnectionsList { get; set; }
        public List<Game> GamesList { get; set; }
        public List<Chat> ChatsList { get; set; }
        public List<Message> MessagesList { get; set; }
        public List<Statistics> GameStatisticsList { get; set; }
        public List<UserReview> MadeUserReviewsList { get; set; }
        public List<UserReview> GotUserReviewsList { get; set; }
        public List<GameReview> GameReviewsList { get; set; }
        
        #endregion 
    }
}
