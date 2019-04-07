using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Statistics : Entity
    {
        #region Main attributes

        public decimal TimeSpent { get; set; }
        public int RunsAmount { get; set; }
        public int UserRecord { get; set; }

        #endregion

        #region Interop attributes

        public long IdUser { get; set; }
        public long IdGame { get; set; }

        #endregion
    }
}
