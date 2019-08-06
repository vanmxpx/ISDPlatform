using System;

namespace Cooper.DAO.Models
{
    public class GameReviewDb : EntityDb
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public long IdGame { get; set; }
        public long IdReviewer { get; set; }

        #endregion
    }
}
