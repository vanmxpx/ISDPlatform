using System.Collections.Generic;

namespace Cooper.Models
{
    public class Chat : Entity
    {
        #region Main attributes
        public string ChatName { get; set; }
        public bool IsOnetoOneChat { get; set; }
        #endregion

        #region Interop attributes

        public List<User> UsersList { get; set; }
        public List<Message> MessagesList { get; set; }

        #endregion
    }
}
