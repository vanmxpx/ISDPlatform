using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/game/reviews")]
    public class GameReviewController : ControllerBase
    {
        GameReviewRepository gameReviewRepository;

        public GameReviewController()
        {
            gameReviewRepository = new GameReviewRepository();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<GameReview> GetReviewsForGame()      // get users reviews for game
        {
            return new List<GameReview>();
        }

        [HttpGet]
        public IEnumerable<GameReview> GetUserReviews()
        {
            return new List<GameReview>();      // get reviews for games from concrete user
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public GameReview Get(long id)
        {
            return gameReviewRepository.Get(id);
        }

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
            return gameReviewRepository.Delete(id);
        }
    }
}
