using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooper.Controllers
{
    [ApiController]
    public class ConfirmationEmailController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly Cooper.Services.Interfaces.ISession session;

        public ConfirmationEmailController(IJwtHandlerService jwtHandler, ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            userRepository = new UserRepository(jwtHandler, session);
        }

        /// <summary>
        /// Confirms email by token.
        /// </summary>
        /// <remarks>
        /// If the user is confirmed, then a redirect to "/auth".
        /// If the user is not confirmed, then he is confirmed and a redirect to "/auth".
        /// </remarks>
        /// <returns>A redirect to "/auth"</returns>
        /// <response code="302">Always</response>  
        [HttpPost]
        [Route("confirm")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public IActionResult Confirm()
        {
            IActionResult result;

            session.StartSession();

            string token = Request.Query["token"];
            string email = userRepository.GetVerifyEmail($"\'{token}\'");

            if (email == null)
            {
                result = Redirect("/auth");
            }
            else
            {
                bool confirmed = userRepository.ConfirmEmail(token, email);
                bool deleted = userRepository.DeleteToken($"\'{token}\'");

                if (deleted && confirmed)
                {
                    session.Commit(endSession: true);
                }
                else
                {
                    session.Rollback(endSession: false);
                }

                result = Redirect("/auth");
                //TODO: Auth
            }

            return result;
        }
    }
}