
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
    public class GameReviewDAO
    {
        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public GameReviewDAO()
        {
            crud = new CRUD();
            logger = LogManager.GetLogger("CooperLoger");

            table = "GAMESREVIEWS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "IDREVIEWER", "IDGAME", "CONTENT", "CREATEDATE", "RATING"
            };
        }


        #region Get methods

        public GameReviewDb Get(long id)
        {
            GameReviewDb gameReview = null;

            EntityORM entity = crud.Read(id, idColumn, attributes, table);

            if (entity != null)
                EntityMapping.Map(entity, out gameReview);

            return gameReview;
        }

        /// <summary>
        /// Return the UserReviewDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameReviewDb GetExtended(long id)
        {
            GameReviewDb gameReview = Get(id);

            //userReview.PlayersList = GetPlayersList(id);

            return gameReview;
        }

        public IEnumerable<GameReviewDb> GetAll()
        {
            List<GameReviewDb> gameReviews = new List<GameReviewDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.ReadAll(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to userReviews
            {
                EntityMapping.Map(entity, out GameReviewDb userReview);
                gameReviews.Add(userReview);
            }

            return gameReviews;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion
        #endregion

        public long Save(GameReviewDb gameReview)
        {
            EntityORM entity = EntityMapping.Map(gameReview, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            long idGameReview = crud.Create(table, idColumn, entity);

            return idGameReview;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

            if (ifDeleted)
            {
                logger.Info($"Game review with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting gameReview with id={id} was failed.");
            }

        }

        public void Update(GameReviewDb gameReview)
        {
            EntityORM entity = EntityMapping.Map(gameReview, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            bool ifUpdated = crud.Update(gameReview.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"Game review with id={gameReview.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating gameReview with id={gameReview.Id} was failed.");
            }
        }

    }
}
