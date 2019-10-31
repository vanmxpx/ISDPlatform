using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;

namespace Cooper.Repositories
{
    public class StatisticsRepository
    {
        private readonly GameStatisticsDAO statisticsDAO;
        private readonly ModelsMapper mapper;

        public StatisticsRepository(IConfigProvider configProvider)
        {
            statisticsDAO = new GameStatisticsDAO(configProvider);
            mapper = new ModelsMapper();
        }

        public Statistics GetStatistics(long userId, long gameId)
        {
            StatisticsDb statistics = (StatisticsDb)statisticsDAO.Get(userId, gameId);

            return statistics == null? new Statistics() : mapper.Map(statistics);
        }

        public long Create(Statistics statistics)
        {
            StatisticsDb statisticsDb = mapper.Map(statistics);

            return statisticsDAO.Save(statisticsDb);
        }

        public void Update(Statistics statistics)
        {
            StatisticsDb statisticsDb = mapper.Map(statistics);

            statisticsDAO.Update(statisticsDb);
        }

        public void Delete(long id)
        {
            statisticsDAO.Delete(id);
        }
    }
}
