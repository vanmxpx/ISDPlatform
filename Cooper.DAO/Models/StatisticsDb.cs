namespace Cooper.DAO.Models
{
    public class StatisticsDb : EntityDb
    {
        #region Main attributes

        public decimal TimeSpent { get; set; }
        public long RunsAmount { get; set; }
        public long UserRecord { get; set; }

        #endregion

        #region Interop attributes

        public long IdUser { get; set; }
        public long IdGame { get; set; }

        #endregion
    }
}
