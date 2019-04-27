using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;

namespace Cooper.DAO
{
    public class GameStatisticsDAO
    {
        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public GameStatisticsDAO()
        {
            crud = new CRUD();
            logger = LogManager.GetLogger("CooperLoger");

            table = "GAMESTATISTICS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "TIMESPENT", "USERRECORD", "RUNSAMOUNT", "IDUSER", "IDGAME"
            };
        }

        #region Get methods

        public StatisticsDb Get(long id)
        {
            StatisticsDb statistics = null;

            EntityORM entity = crud.Read(id, idColumn, attributes, table);

            if (entity != null)
                EntityMapping.Map(entity, out statistics);

            return statistics;
        }

        /// <summary>
        /// Return the MessageDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StatisticsDb GetExtended(long id)
        {
            StatisticsDb statistics = Get(id);

            return statistics;
        }

        public IEnumerable<StatisticsDb> GetAll()
        {
            List<StatisticsDb> statistics = new List<StatisticsDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.ReadAll(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to messages
            {
                EntityMapping.Map(entity, out StatisticsDb statistic);
                statistics.Add(statistic);
            }

            return statistics;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion
        #endregion

        public long Save(StatisticsDb statistics)
        {
            EntityORM entity = EntityMapping.Map(statistics, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            long idStatistic = crud.Create(table, idColumn, entity);

            return idStatistic;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

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

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            bool ifUpdated = crud.Update(statistics.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"Game statistics with id={statistics.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating statistics with id={statistics.Id} was failed.");
            }
        }
    }
}
