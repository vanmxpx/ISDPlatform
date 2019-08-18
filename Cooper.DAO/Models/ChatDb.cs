using System.Collections.Generic;

namespace Cooper.DAO.Models
{
    public class ChatDb : EntityDb
    {

        public string ChatName { get; set; }
        public bool IsOneToOneChat { get; set; }

        public List<long> Participants { get; set; }
        public List<long> Messages { get; set; }
        
    }
}
