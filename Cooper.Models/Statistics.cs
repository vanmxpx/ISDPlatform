namespace Cooper.Models
{
    public class Statistics : Entity
    {
        #region Main attributes

        public decimal TimeSpent { get; set; }
        public long RunsAmount { get; set; }
        public long UserRecord { get; set; }

        #endregion

        #region Interop attributes

        public User User { get; set; }
        public Game Game { get; set; }

        #endregion
    }
}
