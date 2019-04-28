using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Cooper.Models
{
    public class GameReview : Entity
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public Game Game { get; set; }
        public User Reviewer { get; set; }

        #endregion

    }
}
