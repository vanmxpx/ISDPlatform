using System;

namespace Cooper.Models
{
    public class Message : Entity
    {

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }
        
        public long ChatId { get; set; }
        public long SenderId { get; set; }
    }
}
