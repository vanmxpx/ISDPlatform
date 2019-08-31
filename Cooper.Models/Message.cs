using System;

namespace Cooper.Models
{
    public class Message : Entity
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }

        #endregion

        #region Interop attributes

        public Chat IdChat { get; set; }
        public User IdSender { get; set; }

        #endregion
    }
}
