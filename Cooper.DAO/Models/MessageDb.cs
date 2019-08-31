using System;

namespace Cooper.DAO.Models
{
    public class MessageDb : EntityDb
    {
        #region Main attributes

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }

        #endregion

        #region Interop attributes

        public long IdChat { get; set; }
        public long IdSender { get; set; }

        #endregion
    }
}
