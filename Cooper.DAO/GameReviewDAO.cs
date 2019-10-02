using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;


namespace Cooper.DAO
{
    public class GameReviewDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public GameReviewDAO(ISession session)
        {
            crud = new CRUD(session);
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

            var whereRequest = new WhereRequest(idColumn, Operators.Equal, id.ToString());

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, whereRequest);

            if (entities.Any()) {
                EntityMapping.Map(entities[0], out gameReview);
            }

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

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes);

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

        public bool Delete(long id)
        {
            bool isDeleted = crud.Delete(id, table, idColumn);

            if (isDeleted)
            {
                logger.Info($"Game review with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting gameReview with id={id} was failed.");
            }

            return isDeleted;
        }

        public bool Update(GameReviewDb gameReview)
        {
            EntityORM entity = EntityMapping.Map(gameReview, attributes);

            entity.attributeValue.Remove("ID");     // getting sure that ID value is not touched

            var whereRequest = new WhereRequest(idColumn, Operators.Equal, gameReview.Id.ToString());

            bool isUpdated = crud.Update(table, entity, whereRequest);

            if (isUpdated)
            {
                logger.Info($"Game review with id={gameReview.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating gameReview with id={gameReview.Id} was failed.");
            }

            return isUpdated;
        }

    }
}
