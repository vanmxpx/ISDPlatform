using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class GameReviewRepository
    {
        private GameReviewDAO gameReviewDAO;

        public GameReviewRepository()
        {
            gameReviewDAO = new GameReviewDAO();
        }

        public IEnumerable<GameReview> GetAll()
        {
            List<GameReviewDb> gameReviews = (List<GameReviewDb>)gameReviewDAO.GetAll();

            List<GameReview> gameReviews_newType = new List<GameReview>();

            foreach (GameReviewDb gameReview in gameReviews)
            {
                //UserReview userReview_newType = mapper.Map<UserReview>(userReview);
                GameReview gameReview_newType = GameReviewMap(gameReview);

                gameReviews_newType.Add(gameReview_newType);
            }

            return gameReviews_newType;
        }

        public GameReview Get(long id)
        {
            GameReviewDb gameReview = gameReviewDAO.GetExtended(id);
            GameReview gameReview_newTyped = null;

            if (gameReview != null)
            {
                gameReview_newTyped = Mapper.Map<UserReview>(gameReview);
                GameReview userReview_newTyped = GameReviewMap(gameReview);
            }

            return gameReview_newTyped;
        }

        public long Create(GameReview gameReview)
        {
            GameReviewDb gameReviewDb = GameReviewMap(gameReview);

            return gameReviewDAO.Save(gameReviewDb);
        }

        public void Update(GameReview gameReview)
        {
            GameReviewDb gameReviewDb = GameReviewMap(gameReview);

            gameReviewDAO.Update(gameReviewDb);
        }

        public void Delete(long id)
        {
            gameReviewDAO.Delete(id);
        }

        #region Mapping
        private GameReview GameReviewMap(GameReviewDb gameReview)
        {
            GameReview gameReview_newType = new GameReview();

            #region Transfer main attributes

            gameReview_newType.Id = gameReview.Id;
            gameReview_newType.Content = gameReview.Content;
            gameReview_newType.Date = gameReview.Date;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.IdReviewer = new User() { Id = gameReview.IdReviewer };
            gameReview_newType.IdGame = new Game() { Id = gameReview.GameId };

            #endregion

            return gameReview_newType;
        }

        private GameReviewDb GameReviewMap(GameReview gameReview)
        {
            GameReviewDb gameReview_newType = new GameReviewDb();

            #region Transfer main attributes

            gameReview_newType.Id = gameReview.Id;
            gameReview_newType.Content = gameReview.Content;
            gameReview_newType.Date = gameReview.Date;
            gameReview_newType.Rating = gameReview.Rating;

            #endregion

            #region Transfering interop attributes

            gameReview_newType.IdReviewer = gameReview.IdReviewer.Id;
            gameReview_newType.IdGame = gameReview.Game.Id;

            #endregion

            return gameReview_newType;
        }

        #endregion
    }
}
