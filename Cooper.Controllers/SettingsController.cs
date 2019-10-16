using Cooper.Controllers.ViewModels;
using Cooper.Extensions;
using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooper.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/settings/")]
    public class SettingsController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISocialAuth socialAuth;

        public SettingsController(ISocialAuth socialAuth, IConfigProvider configProvider, IJwtHandlerService jwtHandlerService)
        {
            this.socialAuth = socialAuth;
            userRepository = new UserRepository(jwtHandlerService, configProvider);
        }

        [HttpPost]
        [Route("email")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ChangeEmail(string newEmail)
        {
            IActionResult result;
            string userToken = Request.GetUserToken();
            User user = userRepository.GetByJWToken(userToken);

            if (user != null)
            {
                user.Email = newEmail;
                userRepository.Update(user);
                result = Ok("Email changed successful!");
            }
            else
            {
                result = BadRequest("Error credentials!");
            }

            return result;
        }

        [Route("delete")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteAccount()
        {
            IActionResult result;
            string userToken = Request.GetUserToken();
            User user = userRepository.GetByJWToken(userToken);

            if (user != null)
            {
                userRepository.Delete(user.Id);
                result = Ok("Account deleted!");
            }
            else
            {
                result = BadRequest("Error credentials!");
            }

            return result;
        }

        [HttpPost]
        [Route("social")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ConnectSoial(Login login)
        {
            IActionResult result;
            string userToken = Request.GetUserToken();
            User user = userRepository.GetByJWToken(userToken);

            if (user != null)
            {
                if (login.Provider != null && socialAuth.getCheckAuth(login.Provider, login.Password, login.ID))
                {
                    if (login.Provider == "google")
                    {
                        user.GoogleId = login.ID;
                    }
                    else
                    {
                        user.FacebookId = login.ID;
                    }
                    userRepository.Update(user);
                    result = Ok($"Social {login.Provider} connected successful!");
                }
                else
                {
                    result = BadRequest("Error credentials!");
                }
            }
            else
            {
                result = BadRequest("Error credentials!");
            }

            return result;
        }
    }
}
