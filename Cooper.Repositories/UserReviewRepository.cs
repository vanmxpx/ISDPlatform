using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class UserReviewRepository
    {
        private readonly UserReviewDAO userReviewDAO;
        private readonly ModelsMapper mapper;

        public UserReviewRepository(ISession session)
        {
            userReviewDAO = new UserReviewDAO(session);
            mapper = new ModelsMapper();
        }

        public IEnumerable<UserReview> GetAll()
        {
            List<UserReviewDb> userReviews = (List<UserReviewDb>)userReviewDAO.GetAll();

            List<UserReview> userReviews_newType = new List<UserReview>();

            foreach (UserReviewDb userReview in userReviews)
            {
                UserReview userReview_newType = mapper.Map(userReview);

                userReviews_newType.Add(userReview_newType);
            }

            return userReviews_newType;
        }

        public UserReview Get(long id)
        {
            UserReviewDb userReview = userReviewDAO.GetExtended(id);
            UserReview userReview_newTyped = null;

            if (userReview != null)
            {
                userReview_newTyped = mapper.Map(userReview);
            }

            return userReview_newTyped;
        }

        public long Create(UserReview userReview)
        {
            UserReviewDb userReviewDb = mapper.Map(userReview);

            return userReviewDAO.Save(userReviewDb);
        }

        public bool Update(UserReview userReview)
        {
            UserReviewDb userReviewDb = mapper.Map(userReview);

            bool isUpdated = userReviewDAO.Update(userReviewDb);

            return isUpdated;
        }

        public bool Delete(long id)
        {
            bool isDeleted = userReviewDAO.Delete(id);

            return isDeleted;
        }

    }
}
