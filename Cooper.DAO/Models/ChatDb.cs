using System.Collections.Generic;

namespace Cooper.DAO.Models
{
    public class ChatDb : EntityDb
    {

        public string ChatName { get; set; }
        public string PhotoURL { get; set; }

        public List<long> Participants { get; set; }
        public List<long> Messages { get; set; }
        
    }
}
