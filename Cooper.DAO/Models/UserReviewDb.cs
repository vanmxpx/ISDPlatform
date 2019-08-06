using System;

namespace Cooper.DAO.Models
{
    public class UserReviewDb : EntityDb
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public long IdReviewer { get; set; }
        public long IdReviewed { get; set; }

        #endregion
    }
}
