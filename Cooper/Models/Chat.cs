using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Chat
    {
        [Key, Column("IDCHAT", TypeName = "int")]
        public int IDCHAT { get; set; }
        public string CHATNAME { get; set; }
    }
}
