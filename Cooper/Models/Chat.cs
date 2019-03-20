using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Chat : Entity
    {
        public string ChatName { get; set; }
        public List<User> UsersList { get; set; }
    }
}
