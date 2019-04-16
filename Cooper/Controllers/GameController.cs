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
        public Game GetGameById(long id)
        {
            return gameRepository.Get(id);
        }
        
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Game game)
        {
            // MISSED DAO
            return Ok(game);
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Ok();
        }
    }
}
