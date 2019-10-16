using Cooper.Extensions;
using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/users")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly Cooper.Services.Interfaces.ISession session;

        public UserController(IJwtHandlerService jwtService, ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();

            userRepository = new UserRepository(jwtService, session);
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            var users = userRepository.GetAll();
            session.EndSession();

            return users;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(long id)
        {
            User user = userRepository.Get(id);
            session.EndSession();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByEmail(string email)
        {
            User user = userRepository.GetByEmail(email);
            session.EndSession();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("nickname/{nickname}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByNickname(string nickname)
        {

            User user = userRepository.GetByNickname(nickname);
            session.EndSession();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("token"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByJWToken()
        {
            User user = Request.GetAuthorizedUser(userRepository);
            session.EndSession();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]User user)
        {

            if (user.Id == 0)
            {
                return BadRequest();
            }

            IActionResult result;

            session.StartSession();

            bool isUpdated = userRepository.Update(user);

            if (isUpdated)
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

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes user with specified id.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <response code="200">If the user has been deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(long id)
        {
            IActionResult result;
            session.StartSession();

            bool isDeleted = userRepository.Delete(id);

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