using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Models
{
    public class Game : Entity
    {
        public string Name { get; set; }
        public byte[] Description { get; set; }
        public string Genre { get; set; }
        public string Link { get; set; }
        public string LogoURL { get; set; }
        public string CoverURL { get; set; }
        public int idCreator { get; set; }
        public bool isVerified { get; set; }
    }
}
