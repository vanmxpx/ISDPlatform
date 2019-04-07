using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Game : Entity
    {
        #region Main attributes

        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Link { get; set; }
        public string LogoURL { get; set; }
        public string CoverURL { get; set; }
        public bool IsVerified { get; set; }

        #endregion

        #region Interop attributes

        public List<User> PlayersList { get; set; }
        public List<Statistics> GameStatistics { get; set; }
        public List<GameReview> GameReviews { get; set; }
        public User Creator { get; set; }

        #endregion 

    }
}
