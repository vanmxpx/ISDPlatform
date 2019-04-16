using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        IRepository<Game> gameRepository;

        public GameController()
        {
            gameRepository = new GameRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Game> GetAll()
        {
            return gameRepository.GetAll();
        }
        /*
        [HttpGet("{genre}")]
        public IEnumerable<Game> GetGamesByGenre(string genre)
        {
            // MISSED DAO
            return new List<Game>();
        }
        */
        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(404)]
        public IActionResult GetGameById(long id)
        {
            Game game = gameRepository.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }
        
        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (game.Id == 0)
            {
                long id = gameRepository.Create(game);
                game.Id = id;

                return Ok(game);
            }
            else
            {
                gameRepository.Update(game);

                return Ok(game);
            }
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            gameRepository.Delete(id);
            return Ok();
        }
    }
}
