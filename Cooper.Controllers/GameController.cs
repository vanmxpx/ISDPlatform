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
        private readonly Cooper.Services.Interfaces.ISession session;

        public GameController(ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            gameRepository = new GameRepository(session);
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
            var games = gameRepository.GetAll();

            session.EndSession();
            return Ok(games);
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

            session.EndSession();

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
            IActionResult result;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(Token);

            session.StartSession();

            bool isSuccessfull = true;

            if (game.Id == 0)
            {
                long id = gameRepository.Create(game);
                game.Id = id;
                
                isSuccessfull &= (id != 0);
            }
            else
            {
                isSuccessfull = gameRepository.Update(game);
            }

            if (isSuccessfull)
            {
                session.Commit(endSession: true);
                result = Ok(game);
            }
            else
            {
                session.Rollback(endSession: false);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
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
            IActionResult result;
            bool isDeleted = gameRepository.Delete(id);

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