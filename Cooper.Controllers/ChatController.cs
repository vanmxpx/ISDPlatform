using Cooper.Models;
using Cooper.Extensions;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Cooper.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Cooper.Controllers
{
    [Route("api/chats")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;

        private readonly IHubContext<ChatHub, ITypedHubClient> hubContext;

        public ChatController(IHubContext<ChatHub, ITypedHubClient> hubContext, IConfigProvider configProvider, IJwtHandlerService jwtHandlerService)
        {
            userRepository = new UserRepository(jwtHandlerService, configProvider);
            messageRepository = new MessageRepository(configProvider);
            chatRepository = new ChatRepository(configProvider, messageRepository, userRepository as IRepository<User>);

            this.hubContext = hubContext;
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

        [HttpPost("send-message")]
        [Authorize]
        public IActionResult Post([FromBody]Message message)
        {
            if ((message.Content == null) || (message.Content == ""))
            {
                return BadRequest();
            }

            messageRepository.Create(message);
            hubContext.Clients.All.BroadcastMessage(message);

            return Ok();
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