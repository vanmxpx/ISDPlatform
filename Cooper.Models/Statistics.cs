using System;

namespace Cooper.Models
{
    public class Statistics : Entity
    {
        public User User { get; set; }
        public Game Game { get; set; }
        public DateTime DateOfLastGame { get; set; }
        public long WinGames { get; set; }
        public long LoseGames { get; set; }
        public long BestScore { get; set; }
    }
}
