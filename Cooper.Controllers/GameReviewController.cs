using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/game/reviews")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GameReviewController : ControllerBase
    {
        private readonly GameReviewRepository gameReviewRepository;
        private readonly ISession session;

        public GameReviewController(ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            gameReviewRepository = new GameReviewRepository(session);
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public IEnumerable<GameReview> GetReviewsForGame(long Id)
        {
            var reviews = gameReviewRepository.GetReviewsForGame(Id);

            session.EndSession();

            return reviews;
        }

        [HttpGet]
        public IEnumerable<GameReview> GetGameReviews(long userId)
        {
            var reviews = gameReviewRepository.GetReviewsFromUser(userId);

            session.EndSession();

            return reviews;
        }

        // GET api/<controller>/5
        // [HttpGet("{id}")]
        // public GameReview Get(long id)
        // {
        //     return gameReviewRepository.Get(id);
        // }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]GameReview gameReview)
        {
            
            IActionResult result;

            session.StartSession();

            bool isSuccessfull = true;

            if (gameReview.Id == 0)
            {
                long id = gameReviewRepository.Create(gameReview);
                gameReview.Id = id;

                isSuccessfull &= (gameReview.Id != 0);
            }
            else
            {
                isSuccessfull = gameReviewRepository.Update(gameReview);
            }

            if (isSuccessfull)
            {
                session.Commit(endSession: true);
                result = Ok(gameReview);
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
            bool isDeleted = gameReviewRepository.Delete(id);

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