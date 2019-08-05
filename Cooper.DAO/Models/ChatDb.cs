using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.DAO.Models
{
    public class ChatDb : EntityDb
    {
        #region Main attributes

        public string ChatName { get; set; }

        #endregion

        #region Interop attributes

        public List<long> UsersList { get; set; }
        public List<long> MessagesList { get; set; }

        #endregion
    }
}
