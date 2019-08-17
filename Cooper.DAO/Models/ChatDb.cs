using System.Collections.Generic;

namespace Cooper.DAO.Models
{
    public class ChatDb : EntityDb
    {
        #region Main attributes

        public string ChatName { get; set; }
        public bool IsOneToOneChat { get; set; }
        #endregion

        #region Interop attributes

        public List<long> UsersList { get; set; }
        public List<long> MessagesList { get; set; }

        #endregion
    }
}
