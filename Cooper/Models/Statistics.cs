using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Statistics : Entity
    {
        public decimal TimeSpent { get; set; }
        public int RunsAmount { get; set; }
        public int UserRecord { get; set; }
        public long idUser { get; set; }
        public long idGame { get; set; }
    }
}
