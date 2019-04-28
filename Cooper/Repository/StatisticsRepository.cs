using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;

namespace Cooper.Repository
{
    public class StatisticsRepository
    {
        private GameStatisticsDAO statisticsDAO;

        public StatisticsRepository()
        {
            statisticsDAO = new GameStatisticsDAO();
        }

        public IEnumerable<Statistics> GetAll()
        {
            List<StatisticsDb> statistics = (List<StatisticsDb>)statisticsDAO.GetAll();

            List<Statistics> statistics_newType = new List<Statistics>();

            foreach (StatisticsDb statistic in statistics)
            {
                Statistics statistic_newType = StatisticsMap(statistic);

                statistics_newType.Add(statistic_newType);
            }

            return statistics_newType;
        }

        public Statistics Get(long id)
        {
            StatisticsDb statistics = statisticsDAO.GetExtended(id);
            Statistics statistics_newTyped = null;

            if (statistics != null)
            {
                statistics_newTyped = StatisticsMap(statistics);
            }

            return statistics_newTyped;
        }

        public IEnumerable<Statistics> GetStatisticsByUser(long userId)
        {
            List<StatisticsDb> allStatistics = (List<StatisticsDb>)statisticsDAO.GetAll();
            List<Statistics> StatisticsByUser = new List<Statistics>();

            foreach (StatisticsDb statistics in allStatistics)
            {
                if (statistics.IdUser == userId)
                {
                    StatisticsByUser.Add(StatisticsMap(statistics));
                }
            }

            return StatisticsByUser;
        }

        public IEnumerable<Statistics> GetStatisticsByGame(long gameId)
        {
            List<StatisticsDb> allStatistics = (List<StatisticsDb>)statisticsDAO.GetAll();
            List<Statistics> StatisticsByGame = new List<Statistics>();

            foreach (StatisticsDb statistics in allStatistics)
            {
                if (statistics.IdGame == gameId)
                {
                    StatisticsByGame.Add(StatisticsMap(statistics));
                }
            }

            return StatisticsByGame;
        }

        public long Create(Statistics statistics)
        {
            StatisticsDb statisticsDb = StatisticsMap(statistics);

            return statisticsDAO.Save(statisticsDb);
        }

        public void Update(Statistics statistics)
        {
            StatisticsDb statisticsDb = StatisticsMap(statistics);

            statisticsDAO.Update(statisticsDb);
        }

        public void Delete(long id)
        {
            statisticsDAO.Delete(id);
        }

        #region Mapping
        private Statistics StatisticsMap(StatisticsDb statistics)
        {
            Statistics statistics_newType = new Statistics();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.User = new User() { Id = statistics.IdUser };
            statistics_newType.Game = new Game() { Id = statistics.IdGame };

            #endregion

            return statistics_newType;
        }

        private StatisticsDb StatisticsMap(Statistics statistics)
        {
            StatisticsDb statistics_newType = new StatisticsDb();

            #region Transfer main attributes

            statistics_newType.Id = statistics.Id;
            statistics_newType.TimeSpent = statistics.TimeSpent;
            statistics_newType.RunsAmount = statistics.RunsAmount;
            statistics_newType.UserRecord = statistics.UserRecord;

            #endregion

            #region Transfering interop attributes

            statistics_newType.IdUser = statistics.User.Id;
            statistics_newType.IdGame = statistics.Game.Id;

            #endregion

            return statistics_newType;
        }

        #endregion
    }
}
