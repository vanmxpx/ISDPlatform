using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Message : Entity
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool isRead { get; set; }
        public long idChat { get; set; }
        public long idUser { get; set; }
    }
}
