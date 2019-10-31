using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Cooper.DAO
{
    public class GameStatisticsDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        const string columnId = "ID";
        private HashSet<string> attributes;

        public GameStatisticsDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetLogger("CooperLoger");

            table = "STATISTICS";
            attributes = new HashSet<string>()
            {
                "ID", "GAMEID", "USERID", "DATEOFLASTGAME", "WINGAMES", "LOSEGAMES", "BESTSCORE"
            };
        }

        #region Get methods

        public StatisticsDb Get(long userId, long gameId)
        {
            StatisticsDb statistics = null;

            var whereRequest = new WhereRequest("USERID", Operators.Equal, userId.ToString()).And("GAMEID", Operators.Equal, gameId.ToString());

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, whereRequest);

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out statistics);
            }

            return statistics;
        }

        public long Save(StatisticsDb statistics)
        {
            EntityORM entity = EntityMapping.Map(statistics, attributes);

            entity.attributeValue.Remove(columnId);

            long idStatistic = crud.Create(table, columnId, entity);

            return idStatistic;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, columnId);

            if (ifDeleted)
            {
                logger.Info($"Game statistics with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting statistics with id={id} was failed.");
            }

        }

        public void Update(StatisticsDb statistics)
        {
            EntityORM entity = EntityMapping.Map(statistics, attributes);

            entity.attributeValue.Remove(columnId);

            var whereRequest = new WhereRequest(columnId, Operators.Equal, statistics.Id.ToString());

            bool ifUpdated = crud.Update(table, entity, whereRequest);

            if (ifUpdated)
            {
                logger.Info($"Game statistics with id={statistics.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating statistics with id={statistics.Id} was failed.");
            }
        }
        #endregion
    }
}