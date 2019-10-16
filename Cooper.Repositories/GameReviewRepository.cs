﻿using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class GameReviewRepository
    {
        private readonly GameReviewDAO gameReviewDAO;
        private readonly ModelsMapper mapper;

        public GameReviewRepository(ISession session)
        {
            gameReviewDAO = new GameReviewDAO(session);
            mapper = new ModelsMapper();
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

        public bool Update(GameReview gameReview)
        {
            GameReviewDb gameReviewDb = mapper.Map(gameReview);

            bool isUpdated = gameReviewDAO.Update(gameReviewDb);

            return isUpdated;
        }

        public bool Delete(long id)
        {
            bool isDeleted = gameReviewDAO.Delete(id);

            return isDeleted;
        }
    }
}
