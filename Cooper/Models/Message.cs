using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public long IdChat { get; set; }
        public long IdUser { get; set; }

        #endregion
    }
}
