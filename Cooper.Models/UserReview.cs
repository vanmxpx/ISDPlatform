using System;

namespace Cooper.Models
{
    public class UserReview : Entity
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public User IdReviewer { get; set; }
        public User IdReviewed { get; set; }

        #endregion
    }
}
