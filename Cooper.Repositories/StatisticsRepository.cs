using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class StatisticsRepository
    {
        private readonly GameStatisticsDAO statisticsDAO;
        private readonly ModelsMapper mapper;

        public StatisticsRepository(ISession session)
        {
            statisticsDAO = new GameStatisticsDAO(session);
            mapper = new ModelsMapper();
        }

        public IEnumerable<Statistics> GetAll()
        {
            List<StatisticsDb> statistics = (List<StatisticsDb>)statisticsDAO.GetAll();

            List<Statistics> statistics_newType = new List<Statistics>();

            foreach (StatisticsDb statistic in statistics)
            {
                Statistics statistic_newType = mapper.Map(statistic);

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
                statistics_newTyped = mapper.Map(statistics);
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
                    StatisticsByUser.Add(mapper.Map(statistics));
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
                    StatisticsByGame.Add(mapper.Map(statistics));
                }
            }

            return StatisticsByGame;
        }

        public long Create(Statistics statistics)
        {
            StatisticsDb statisticsDb = mapper.Map(statistics);

            return statisticsDAO.Save(statisticsDb);
        }

        public bool Update(Statistics statistics)
        {
            StatisticsDb statisticsDb = mapper.Map(statistics);

            bool isUpdated = statisticsDAO.Update(statisticsDb);

            return isUpdated;
        }

        public bool Delete(long id)
        {
            bool isDeleted = statisticsDAO.Delete(id);

            return isDeleted;
        }
    }
}
