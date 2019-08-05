using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class CommonMessage
    {
        public string Content { get; set; }
        public int CreateDate { get; set; }
        public User Author { get; set; }
    }
}
