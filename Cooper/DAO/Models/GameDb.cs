using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.DAO.Models
{
    public class GameDb : EntityDb
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

        public List<long> PlayersList { get; set; }
        public List<long> GameStatistics { get; set; }
        public List<long> GameReviews { get; set; }
        public long IdCreator { get; set; }

        #endregion
    }
}
