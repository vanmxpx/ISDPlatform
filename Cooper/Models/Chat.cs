using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Chat : Entity
    {
        #region Main attributes
        public string ChatName { get; set; }
        #endregion

        #region Interop attributes

        public List<User> UsersList { get; set; }
        public List<Message> MessagesList { get; set; }

        #endregion
    }
}
