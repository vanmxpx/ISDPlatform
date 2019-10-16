using Cooper.Extensions;
using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cooper.Controllers
{
    [Route("api/media")]
    public class MediaserverController : ControllerBase
    {
        private readonly IMediaserver mediaserver;
        private readonly UserRepository userRepository;
        private readonly string getApiImage;
        private readonly Cooper.Services.Interfaces.ISession session;

        public MediaserverController(IMediaserver mediaserver, IJwtHandlerService jwtService, IConfigProvider configProvider, ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();

            this.mediaserver = mediaserver;
            this.getApiImage = configProvider.MediaserverConf.GetApiUrl;
            userRepository = new UserRepository(jwtService, session);
        }

        [HttpPost, Authorize]
        [Route("avatar")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> uploadProfileImage()
        {
            IActionResult result = BadRequest("Files not found!");
            IFormFileCollection files = HttpContext.Request.Form.Files;
            session.StartSession();

            if (files.Count != 0)
            {
                string response = await mediaserver.UploadImageAsync(HttpContext.Request.Form.Files[0]);
                if (response != null)
                {
                    var json_result = JObject.Parse(response);
                    string error = (string)json_result.SelectToken("errorMessage");
                    if (error == null)
                    {
                        User user = Request.GetAuthorizedUser(userRepository);
                        if (user != null)
                        {
                            string url = $"{getApiImage}{ (string)json_result.SelectToken("fileName") }";
                            userRepository.UpdateAvatar(url, user.Id);
                            result = Ok(response);
                        }
                        else
                        {
                            result = BadRequest("User not found!");
                        }
                    }
                    else
                    {
                        result = BadRequest(error);
                    }
                }
                else
                {
                    result = BadRequest("Media server upload error!");
                }
            }

            session.EndSession();
            return result;
        }
    }
}
