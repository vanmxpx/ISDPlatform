using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;
using Cooper.Configuration;
using NLog;

namespace Cooper.Repository
{
    public class GameReviewRepository
    {
        private GameReviewDAO gameReviewDAO;
        private ModelsMapper mapper;
        private readonly ILogger logger;

        public GameReviewRepository(IConfigProvider configProvider, ILogger logger)
        {
            gameReviewDAO = new GameReviewDAO(configProvider, logger);
            mapper = new ModelsMapper();

            this.logger = logger;
        }

        public IEnumerable<GameReview> GetAll()
        {
            List<GameReviewDb> gameReviews = (List<GameReviewDb>)gameReviewDAO.GetAll();

            List<GameReview> gameReviews_newType = new List<GameReview>();

            foreach (GameReviewDb gameReview in gameReviews)
            {
                GameReview gameReview_newType = mapper.Map(gameReview);

                gameReviews_newType.Add(gameReview_newType);
            }

            return gameReviews_newType;
        }

        public IEnumerable<GameReview> GetReviewsForGame(long gameId)
        {
            List<GameReviewDb> allReviews = (List<GameReviewDb>)gameReviewDAO.GetAll();
            List<GameReview> reviewsForGame = new List<GameReview>();

            foreach(GameReviewDb reviewForGame in allReviews)
            {
                if (reviewForGame.IdGame == gameId)
                {
                    reviewsForGame.Add(mapper.Map(reviewForGame));
                }
            }

            return reviewsForGame;
        }

        public IEnumerable<GameReview> GetReviewsFromUser(long userId)
        {
            List<GameReviewDb> allReviews = (List<GameReviewDb>)gameReviewDAO.GetAll();
            List<GameReview> reviewsFromUser = new List<GameReview>();

            foreach (GameReviewDb reviewFromUser in allReviews)
            {
                if (reviewFromUser.IdReviewer == userId)
                {
                    reviewsFromUser.Add(mapper.Map(reviewFromUser));
                }
            }

            return reviewsFromUser;
        }

        public GameReview Get(long id)
        {
            GameReviewDb gameReview = gameReviewDAO.GetExtended(id);
            GameReview gameReview_newTyped = null;

            if (gameReview != null)
            {
                gameReview_newTyped = mapper.Map(gameReview);
            }

            return gameReview_newTyped;
        }

        public long Create(GameReview gameReview)
        {
            GameReviewDb gameReviewDb = mapper.Map(gameReview);

            return gameReviewDAO.Save(gameReviewDb);
        }

        public void Update(GameReview gameReview)
        {
            GameReviewDb gameReviewDb = mapper.Map(gameReview);

            gameReviewDAO.Update(gameReviewDb);
        }

        public void Delete(long id)
        {
            gameReviewDAO.Delete(id);
        }
    }
}
