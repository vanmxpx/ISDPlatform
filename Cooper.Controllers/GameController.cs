using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Cooper.Controllers
{
    //[ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IRepository<Game> gameRepository;
        private readonly ILogger<GameController> logger;

        public GameController(IConfigProvider configProvider, ILogger<GameController> logger)
        {
            gameRepository = new GameRepository(configProvider);
            this.logger = logger;
        }

        /// <summary>
        /// Returns all games.
        /// </summary>
        /// <returns>All games</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            logger.LogInformation("Getting all games");
            return Ok(gameRepository.GetAll());
        }

        /// <summary>
        /// Returns game by name.
        /// </summary>
        /// <returns>Game</returns>
        /// <response code="200">If the game exists</response>
        /// <response code="404">If the game does not exist</response>
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string name, long id)
        {
            Game game = null;
            if (id > 0)
            {
                game = gameRepository.Get(id);
            }
            else if (name != null)
            {
                game = gameRepository.Get(name);
            }
            
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        /// <summary>
        /// Creates game.
        /// </summary>
        /// <returns>Game</returns>
        /// <response code="200">If the game is created</response>
        /// <response code="400">If the model state is invalid</response>
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Game game, string Token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(Token);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (game.Id == 0)
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

        /// <summary>
        /// Deletes game.
        /// </summary>
        /// <returns>Status code 200</returns>
        /// <response code="200">Always</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(long id)
        {
            gameRepository.Delete(id);
            return Ok();
        }
    }
}