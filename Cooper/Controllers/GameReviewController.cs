using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/game/reviews")]
    public class GameReviewController : ControllerBase
    {
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
            return new GameReview() { Id = 10, Content = "Bad game" };
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]GameReview gameReview)
        {
            return Ok(new GameReview());
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return BadRequest();
        }
    }
}
