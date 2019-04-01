﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Cooper.DAO.Models
{
    public class GameReviewDb : EntityDb
    {
        #region Main attributes

        public string Content { get; set; }
        DateTime Date { get; set; }
        public int Rating { get; set; }

        #endregion

        #region Interop attributes

        public long GameId { get; set; }
        public long ReviewerId { get; set; }

        #endregion
    }
}
