using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;
using Cooper.Configuration;

namespace Cooper.DAO
{
    public class UserReviewDAO
    {
        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        
        public UserReviewDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetLogger("CooperLoger");

            table = "USERSREVIEWS";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "IDREVIEWER", "IDREVIEWED", "CONTENT", "CREATEDATE", "RATING"
            };
        }


        #region Get methods

        public UserReviewDb Get(long id)
        {
            UserReviewDb userReview = null;

            var entity = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entity.Count != 0)
                EntityMapping.Map(entity[0], out userReview);

            return userReview;
        }

        /// <summary>
        /// Return the UserReviewDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserReviewDb GetExtended(long id)
        {
            UserReviewDb userReview = Get(id);

            //userReview.PlayersList = GetPlayersList(id);

            return userReview;
        }

        public IEnumerable<UserReviewDb> GetAll()
        {
            List<UserReviewDb> userReviews = new List<UserReviewDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes);

            foreach (EntityORM entity in entities) 
            {
                EntityMapping.Map(entity, out UserReviewDb userReview);
                userReviews.Add(userReview);
            }

            return userReviews;
        }


        #region Interop properties info reading
        // Here they will be

        #endregion
        #endregion

        public long Save(UserReviewDb userReview)
        {
            EntityORM entity = EntityMapping.Map(userReview, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            long idUserReview = crud.Create(table, idColumn, entity);

            return idUserReview;
        }

        public void Delete(long id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

            if (ifDeleted)
            {
                logger.Info($"Game with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting userReview with id={id} was failed.");
            }

        }

        public void Update(UserReviewDb userReview)
        {
            EntityORM entity = EntityMapping.Map(userReview, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID"); 

            bool ifUpdated = crud.Update(userReview.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"Game with id={userReview.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating userReview with id={userReview.Id} was failed.");
            }
        }

    }
}
