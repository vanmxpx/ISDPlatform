using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Cooper.Models
{
    public class UserReview : Entity
    {
        #region Main attributes

        public string Content { get; set; }
        DateTime Date { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public User User { get; set; }
        public User Reviewer { get; set; }

        #endregion
    }
}
