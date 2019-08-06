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

        public GameReviewController(IConfigProvider configProvider)
        {
            gameReviewRepository = new GameReviewRepository(configProvider);
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public IEnumerable<GameReview> GetReviewsForGame(long Id)
        {
            return gameReviewRepository.GetReviewsForGame(Id);
        }

        [HttpGet]
        public IEnumerable<GameReview> GetUserReviews(long userId)
        {
            return gameReviewRepository.GetReviewsFromUser(userId);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (gameReview.Id == 0)
            {
                long id = gameReviewRepository.Create(gameReview);
                gameReview.Id = id;

                return Ok(gameReview);
            }
            else
            {
                gameReviewRepository.Update(gameReview);

                return Ok(gameReview);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            gameReviewRepository.Delete(id);
            return Ok();
        }
    }
}