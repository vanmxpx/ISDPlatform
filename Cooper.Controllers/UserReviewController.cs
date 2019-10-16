using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/user/reviews")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserReviewController : ControllerBase
    {
        // GET: api/<controller>
        private readonly UserReviewRepository userReviewRepository;
        private readonly ISession session;
        public UserReviewController(ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            userReviewRepository = new UserReviewRepository(session);
        }

        // TODO: get all reviews for reviewed user method
        // TODO: get all reviews for user reviewer method

        [HttpGet]
        public IEnumerable<UserReview> GetAll()
        {
            var usersReviews = userReviewRepository.GetAll();

            session.EndSession();

            return usersReviews;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserReview))]
        [ProducesResponseType(404)]
        public IActionResult GetUserReviewById(long id)
        {
            UserReview userReview = userReviewRepository.Get(id);
            session.EndSession();

            if (userReview == null)
            {
                return NotFound();
            }

            return Ok(userReview);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserReview userReview)
        {
            IActionResult result;

            session.StartSession();

            bool isSuccessfull = true;

            if (userReview.Id == 0)
            {
                long id = userReviewRepository.Create(userReview);
                userReview.Id = id;

                isSuccessfull &= (userReview.Id != 0);
            }
            else
            {
                isSuccessfull = userReviewRepository.Update(userReview);
            }

            if (isSuccessfull)
            {
                session.Commit(endSession: true);
                result = Ok(userReview);
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            IActionResult result;
            bool isDeleted = userReviewRepository.Delete(id);
            
            if (isDeleted)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }
    }
}