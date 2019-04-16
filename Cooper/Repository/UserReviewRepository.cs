using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class UserReviewRepository
    {
        private UserReviewDAO userReviewDAO;

        public UserReviewRepository()
        {
            userReviewDAO = new UserReviewDAO();
        }

        public IEnumerable<UserReview> GetAll()
        {
            List<UserReviewDb> userReviews = (List<UserReviewDb>)userReviewDAO.GetAll();

            List<UserReview> userReviews_newType = new List<UserReview>();

            foreach (UserReviewDb userReview in userReviews)
            {
                //UserReview userReview_newType = mapper.Map<UserReview>(userReview);
                UserReview userReview_newType = UserReviewMap(userReview);

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
                userReview_newTyped = Mapper.Map<UserReview>(userReview);
                //UserReview userReview_newTyped = UserReviewMap(userReview);
            }

            return userReview_newTyped;
        }

        public long Create(UserReview userReview)
        {
            UserReviewDb userReviewDb = UserReviewMap(userReview);

            return userReviewDAO.Save(userReviewDb);
        }

        public void Update(UserReview userReview)
        {
            UserReviewDb userReviewDb = UserReviewMap(userReview);

            userReviewDAO.Update(userReviewDb);
        }

        public void Delete(long id)
        {
            userReviewDAO.Delete(id);
        }

        #region Mapping
        private UserReview UserReviewMap(UserReviewDb userReview)
        {
            UserReview userReview_newType = new UserReview();

            #region Transfer main attributes

            userReview_newType.Id = userReview.Id;
            userReview_newType.Content = userReview.Content;
            userReview_newType.CreateDate = userReview.CreateDate;
            userReview_newType.Rating = userReview.Rating;

            #endregion

            #region Transfering interop attributes

            userReview_newType.IdReviewer = new User() { Id = userReview.IdReviewer };
            userReview_newType.IdReviewed = new User() { Id = userReview.IdReviewed };

            #endregion

            return userReview_newType;
        }

        private UserReviewDb UserReviewMap(UserReview userReview)
        {
            UserReviewDb userReview_newType = new UserReviewDb();

            #region Transfer main attributes

            userReview_newType.Id = userReview.Id;
            userReview_newType.Content = userReview.Content;
            userReview_newType.CreateDate = userReview.CreateDate;
            userReview_newType.Rating = userReview.Rating;

            #endregion

            #region Transfering interop attributes
            
            userReview_newType.IdReviewer = userReview.IdReviewer.Id;
            userReview_newType.IdReviewed = userReview.IdReviewed.Id;

            #endregion

            return userReview_newType;
        }

        #endregion
    }
}
