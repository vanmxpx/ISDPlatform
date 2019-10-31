using System;

namespace Cooper.DAO.Models
{
    public class StatisticsDb : EntityDb
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GameId { get; set; }
        public DateTime DateOfLastGame { get; set; }
        public long WinGames { get; set; }
        public long LoseGames { get; set; }
        public long BestScore { get; set; }
    }
}
