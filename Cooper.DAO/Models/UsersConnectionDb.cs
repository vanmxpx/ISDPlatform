using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.Configuration;

namespace Cooper.DAO.Models
{
    public class UsersConnectionDb : EntityDb
    {
        #region Main attributes

        public bool AreFriends { get; set; }
        public bool BlackListed { get; set; }

        #endregion

        #region Interop attributes

        public long IdUser1 { get; set; }
        public long IdUser2 { get; set; }

        #endregion
    }
}
