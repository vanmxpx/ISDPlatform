using Cooper.Models;
using Cooper.Extensions;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/chats")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;

        public ChatController(IConfigProvider configProvider, IJwtHandlerService jwtHandlerService)
        {
            userRepository = new UserRepository(jwtHandlerService, configProvider);
            chatRepository = new ChatRepository(configProvider, userRepository as IRepository<User>);
        }

        [Authorize]
        [HttpGet("one-to-one-chats")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(404)]
        public IActionResult GetChatByUserId()
        {
            string userToken = Request.GetUserToken();
            long userId = userRepository.GetByJWToken(userToken).Id;

            IList<Chat> chats = chatRepository.GetPersonalChatsByUserId(userId);

            if (chats == null)
            {
                return NotFound();
            }

            return Ok(chats);
        }
        
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (chat.Id == 0)
            {
                long id = chatRepository.Create(chat);
                chat.Id = id;

                return Ok(chat);
            }
            else
            {
                chatRepository.Update(chat);

                return Ok(chat);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            chatRepository.Delete(id);
            return Ok();
        }
    }
}